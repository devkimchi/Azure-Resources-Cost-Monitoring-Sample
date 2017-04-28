using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

using CostMonitoring.EntityModels;
using CostMonitoring.Extensions;
using CostMonitoring.Helpers;
using CostMonitoring.Models;
using CostMonitoring.Settings;
using CostMonitoring.Wrappers;

using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Rest;

using Newtonsoft.Json;

using Subscription = CostMonitoring.Models.Subscription;

namespace CostMonitoring.Services
{
    /// <summary>
    /// This represents the service entity for Azure billing cost aggregation by resource group.
    /// </summary>
    public class CostAggregationService : ICostAggregationService
    {
        private const string EndpointSubscriptionsKey = "Subscriptions";

        private readonly ICostMonitoringSettings _settings;
        private readonly IMonitoringDbContext _dbContext;
        private readonly IAzureAuthenticationHelperWrapper _auth;
        private readonly IAzureBillingApiClientHelper _billing;
        private readonly IHttpClientHelper _httpClient;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostAggregationService"/> class.
        /// </summary>
        /// <param name="settings"><see cref="ICostMonitoringSettings"/> instance.</param>
        /// <param name="dbContext"><see cref="IMonitoringDbContext"/> instance.</param>
        /// <param name="auth"><see cref="IAzureAuthenticationHelperWrapper"/> instance.</param>
        /// <param name="billing"><see cref="IAzureBillingApiClientHelper"/> instance.</param>
        /// <param name="httpClient"><see cref="IHttpClientHelper"/> instance.</param>
        public CostAggregationService(ICostMonitoringSettings settings, IMonitoringDbContext dbContext, IAzureAuthenticationHelperWrapper auth, IAzureBillingApiClientHelper billing, IHttpClientHelper httpClient)
        {
            this._settings = settings.ThrowIfNullOrEmpty();
            this._dbContext = dbContext.ThrowIfNullOrEmpty();
            this._auth = auth.ThrowIfNullOrEmpty();
            this._billing = billing.ThrowIfNullOrEmpty();
            this._httpClient = httpClient.ThrowIfNullOrEmpty();
        }

        /// <summary>
        /// Processes the data aggregation.
        /// </summary>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <returns>Returns the number of records processed.</returns>
        public async Task<int> ProcessAsync(DateTime dateStart, DateTime dateEnd)
        {
            var authToken = this._auth.GetOAuthTokenFromAad(this._settings);
            var subscriptions = await this.GetSubscriptionsAsync(authToken).ConfigureAwait(false);

            var records = 0;
            foreach (var subscription in subscriptions)
            {
                var resourceGroupCosts = await this.GetResourceGroupCostsAsync(subscription, dateStart, dateEnd, authToken)
                                                   .ConfigureAwait(false);

                var count = resourceGroupCosts.Count();
                records += count;

                var resourceGroups = await this.GetResourceGroupsAsync(subscription.SubscriptionId, authToken)
                                               .ConfigureAwait(false);

                var resourceGroupCostsResults = resourceGroupCosts.Select(p => ResourceGroupCostResult.GetResourceGroupCostResult(p, resourceGroups, this._settings.Tags))
                                                                  .GroupBy(ResourceGroupCostResultKey.GetResourceGroupCostResultKey, ResourceGroupCostResult.GetResourceGroupCostResult)
                                                                  .OrderByDescending(p => p.Cost);


                await this.SaveResourceGroupCostsResultsAsync(subscription, resourceGroupCostsResults)
                          .ConfigureAwait(false);
            }

            return records;
        }

        /// <summary>
        /// Gets the list of subscriptions.
        /// </summary>
        /// <param name="authToken">Authentication token.</param>
        /// <returns>Returns the list of <see cref="Models.Subscription"/> instances.</returns>
        /// <exception cref="InvalidOperationException">Endpoint not exist</exception>
        public async Task<IEnumerable<Subscription>> GetSubscriptionsAsync(string authToken)
        {
            this._httpClient.BaseAddress = new Uri(this._settings.Resources.BaseUrl.TrimEnd('/'));

            var endpoint = this._settings.Resources.Endpoints.SingleOrDefault(p => p.Name.IsEquivalentTo(EndpointSubscriptionsKey));
            if (endpoint.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Endpoint not exist");
            }

            var result = await this._httpClient.GetStringAsync(endpoint.Url.TrimStart('/'), authToken).ConfigureAwait(false);

            var subscriptions = JsonConvert.DeserializeObject<Subscriptions>(result);

            return subscriptions.Value;
        }

        /// <summary>
        /// Gets the list of <see cref="ResourceGroupCost"/> instances.
        /// </summary>
        /// <param name="subscription"><see cref="Subscription"/> instance.</param>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <param name="authToken">Authentication token value.</param>
        /// <returns>Returns the list of <see cref="Models.ResourceGroupCost"/> instances.</returns>
        public async Task<IEnumerable<ResourceGroupCost>> GetResourceGroupCostsAsync(Subscription subscription, DateTime dateStart, DateTime dateEnd, string authToken)
        {
            var costs = await this._billing.GetResourceCostsAsync(subscription.SubscriptionId, dateStart, dateEnd, authToken).ConfigureAwait(false);

            var grouped = costs.GroupBy(
                                        p => new
                                                 {
                                                     DateStart = p.UsageValue.Properties.UsageStartTime,
                                                     DateEnd = p.UsageValue.Properties.UsageEndTime,
                                                     ResourceGroupName = ResourceGroupDateKey.GetResourceGroupKey(p)
                                                 })
                               .Select(
                                       p => new ResourceGroupCost()
                                                {
                                                    ResourceGroupName = p.Key.ResourceGroupName,
                                                    DateStart = DateTimeOffset.Parse(p.Key.DateStart),
                                                    DateEnd = DateTimeOffset.Parse(p.Key.DateEnd),
                                                    Cost = p.Sum(q => q.CalculatedCosts)
                                                })
                               .OrderBy(p => p.ResourceGroupName)
                               .ThenBy(p => p.DateStart)
                               .ThenBy(p => p.DateEnd);

            return grouped;
        }

        /// <summary>
        /// Gets the list of <see cref="ResourceGroup"/> instances.
        /// </summary>
        /// <param name="subscriptionId">Subscription Id where resources belong.</param>
        /// <param name="authToken">Authentication token value.</param>
        /// <returns>Returns the list of <see cref="ResourceGroup"/> instance.</returns>
        public async Task<IEnumerable<ResourceGroup>> GetResourceGroupsAsync(string subscriptionId, string authToken)
        {
            var credentials = new TokenCredentials(authToken);
            using (var client = new ResourceManagementClient(credentials) { SubscriptionId = subscriptionId })
            {
                var rgs = await client.ResourceGroups.ListAsync().ConfigureAwait(false);

                return rgs.ToArray();
            }
        }

        /// <summary>
        /// Saves the resource group cost results to database.
        /// </summary>
        /// <param name="subscription">Subscription name where resources belong.</param>
        /// <param name="results">List of <see cref="ResourceGroupCostResult"/> instances.</param>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task SaveResourceGroupCostsResultsAsync(Subscription subscription, IEnumerable<ResourceGroupCostResult> results)
        {
            var now = DateTimeOffset.UtcNow;

            foreach (var result in results)
            {
                var totalSpendLimit = result.TotalSpendLimit.IsNullOrWhiteSpace() ? this._settings.Billing.TotalSpendLimit : Convert.ToDecimal(result.TotalSpendLimit);
                var dailySpendLimit = result.DailySpendLimit.IsNullOrWhiteSpace() ? this._settings.Billing.DailySpendLimit : Convert.ToDecimal(result.DailySpendLimit);
                var overspendAction = result.OverspendAction.IsNullOrWhiteSpace() ? this._settings.Billing.OverspendAction : result.OverspendAction;

                var record = await this._dbContext.ResourceGroupCostHistories
                                       .SingleOrDefaultAsync(p => p.Subscription.Equals(subscription.DisplayName, StringComparison.CurrentCultureIgnoreCase) &&
                                                                  p.ResourceGroupName.Equals(result.ResourceGroupName, StringComparison.CurrentCultureIgnoreCase) &&
                                                                  p.Owners.Equals(result.OwnerEmails, StringComparison.CurrentCultureIgnoreCase) &&
                                                                  p.DateStart == result.DateStart &&
                                                                  p.DateEnd == result.DateEnd)
                                       .ConfigureAwait(false);

                if (record == null)
                {
                    record = new ResourceGroupCostHistory()
                                 {
                                     ResourceGroupCostHistoryId = Guid.NewGuid(),
                                     Subscription = subscription.DisplayName,
                                     SubscriptionId = Guid.Parse(subscription.SubscriptionId),
                                     ResourceGroupName = result.ResourceGroupName,
                                     Owners = result.OwnerEmails,
                                     DateStart = result.DateStart,
                                     DateEnd = result.DateEnd,
                                     DateCreated = now
                                 };
                }

                record.Cost = Convert.ToDecimal(result.Cost);
                record.TotalSpendLimit = totalSpendLimit;
                record.DailySpendLimit = dailySpendLimit;
                record.OverspendAction = overspendAction;
                record.DateUpdated = now;

                this._dbContext.ResourceGroupCostHistories.AddOrUpdate(record);
            }

            await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}

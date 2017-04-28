using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CodeHollow.AzureBillingApi;
using CodeHollow.AzureBillingApi.Usage;

using CostMonitoring.Extensions;
using CostMonitoring.Models;
using CostMonitoring.Settings;
using CostMonitoring.Wrappers;

namespace CostMonitoring.Helpers
{
    /// <summary>
    /// This represents the helper entity for the <see cref="AzureBillingApiClientWrapper"/> class.
    /// </summary>
    public class AzureBillingApiClientHelper : IAzureBillingApiClientHelper
    {
        private readonly ICostMonitoringSettings _settings;
        private readonly IAzureBillingApiClientWrapper _wrapper;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBillingApiClientHelper"/> class.
        /// </summary>
        /// <param name="settings"><see cref="ICostMonitoringSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="settings"/> is <see langword="null"/></exception>
        public AzureBillingApiClientHelper(ICostMonitoringSettings settings)
        {
            this._settings = settings.ThrowIfNullOrEmpty();
            this._wrapper = new AzureBillingApiClientWrapper(this._settings);
        }

        /// <summary>
        /// Gets the list of <see cref="ResourceCosts"/> instances.
        /// </summary>
        /// <param name="subscriptionId">Subscription Id where resources belong.</param>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <param name="authToken">Authentication token value.</param>
        /// <returns>Returns the list of <see cref="ResourceCosts"/> instances.</returns>
        public async Task<List<ResourceCosts>> GetResourceCostsAsync(string subscriptionId, DateTime dateStart, DateTime dateEnd, string authToken)
        {
            var showDetails = false;

            this._wrapper.SubscriptionId = subscriptionId;

            var offerId = new AzureOfferId(this._settings.Billing.OfferId);

            var result = await this._wrapper.GetResourceCostsAsync(offerId, this._settings.Billing.Currency, this._settings.Billing.Locale, this._settings.Billing.RegionInfo, dateStart, dateEnd, AggregationGranularity.Daily, showDetails, authToken)
                                            .ConfigureAwait(false);

            return result.Costs;
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

            this._wrapper.Dispose();

            this._disposed = true;
        }
    }
}

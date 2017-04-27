using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CostMonitoring.Models;

using Microsoft.Azure.Management.ResourceManager.Models;

using Subscription = CostMonitoring.Models.Subscription;

namespace CostMonitoring.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="CostAggregationService"/> class.
    /// </summary>
    public interface ICostAggregationService : IDisposable
    {
        /// <summary>
        /// Processes the data aggregation.
        /// </summary>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <returns>Returns the number of records processed.</returns>
        Task<int> ProcessAsync(DateTime dateStart, DateTime dateEnd);

        /// <summary>
        /// Gets the list of subscriptions.
        /// </summary>
        /// <param name="authToken">Authentication token.</param>
        /// <returns>Returns the list of <see cref="Models.Subscription"/> instances.</returns>
        /// <exception cref="InvalidOperationException">Endpoint not exist</exception>
        Task<IEnumerable<Subscription>> GetSubscriptionsAsync(string authToken);

        /// <summary>
        /// Gets the list of <see cref="ResourceGroupCost"/> instances.
        /// </summary>
        /// <param name="subscription"><see cref="Subscription"/> instance.</param>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <param name="authToken">Authentication token value.</param>
        /// <returns>Returns the list of <see cref="Models.ResourceGroupCost"/> instances.</returns>
        Task<IEnumerable<ResourceGroupCost>> GetResourceGroupCostsAsync(Subscription subscription, DateTime dateStart, DateTime dateEnd, string authToken);

        /// <summary>
        /// Gets the list of <see cref="ResourceGroup"/> instances.
        /// </summary>
        /// <param name="subscriptionId">Subscription Id where resources belong.</param>
        /// <param name="authToken">Authentication token value.</param>
        /// <returns>Returns the list of <see cref="ResourceGroup"/> instance.</returns>
        Task<IEnumerable<ResourceGroup>> GetResourceGroupsAsync(string subscriptionId, string authToken);

        /// <summary>
        /// Saves the resource group cost results to database.
        /// </summary>
        /// <param name="subscription">Subscription name where resources belong.</param>
        /// <param name="results">List of <see cref="ResourceGroupCostResult"/> instances.</param>
        /// <returns>Return <see cref="Task"/>.</returns>
        Task SaveResourceGroupCostsResultsAsync(Subscription subscription, IEnumerable<ResourceGroupCostResult> results);
    }
}
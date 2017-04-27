using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CodeHollow.AzureBillingApi;

namespace CostMonitoring.Helpers
{
    /// <summary>
    /// This provides interfaces to the <see cref="AzureBillingApiClientHelper"/> class.
    /// </summary>
    public interface IAzureBillingApiClientHelper
    {
        /// <summary>
        /// Gets the list of <see cref="ResourceCosts"/> instances.
        /// </summary>
        /// <param name="subscriptionId">Subscription Id where resources belong.</param>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <param name="authToken">Authentication token value.</param>
        /// <returns>Returns the list of <see cref="ResourceCosts"/> instances.</returns>
        Task<List<ResourceCosts>> GetResourceCostsAsync(string subscriptionId, DateTime dateStart, DateTime dateEnd, string authToken);
    }
}
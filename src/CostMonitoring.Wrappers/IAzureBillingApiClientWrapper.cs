using System;
using System.Threading.Tasks;

using CodeHollow.AzureBillingApi;
using CodeHollow.AzureBillingApi.Usage;

namespace CostMonitoring.Wrappers
{
    /// <summary>
    /// This provides interfaces to the <see cref="AzureBillingApiClientWrapper"/> class.
    /// </summary>
    public interface IAzureBillingApiClientWrapper : IDisposable
    {
        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        string SubscriptionId { get; set; }

        /// <summary>
        /// Gets the <see cref="ResourceCostData"/> instance.
        /// </summary>
        /// <param name="offerId">Azure offer Id.</param>
        /// <param name="currency">Currency value.</param>
        /// <param name="locale">Locale value.</param>
        /// <param name="regionInfo">Region information.</param>
        /// <param name="dateStart">Start date.</param>
        /// <param name="dateEnd">End date.</param>
        /// <param name="granularity"><see cref="AggregationGranularity"/> value.</param>
        /// <param name="showDetails">Value indicating whether to display details or not.</param>
        /// <param name="authToken">Authentication token.</param>
        /// <returns>Returns the <see cref="ResourceCostData"/> instance.</returns>
        Task<ResourceCostData> GetResourceCostsAsync(string offerId, string currency, string locale, string regionInfo, DateTime dateStart, DateTime dateEnd, AggregationGranularity granularity, bool showDetails, string authToken = null);
    }
}
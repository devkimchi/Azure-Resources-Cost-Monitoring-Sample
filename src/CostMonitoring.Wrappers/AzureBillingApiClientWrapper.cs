using System;
using System.Threading.Tasks;

using CodeHollow.AzureBillingApi;
using CodeHollow.AzureBillingApi.Usage;

using CostMonitoring.Extensions;
using CostMonitoring.Settings;

namespace CostMonitoring.Wrappers
{
    /// <summary>
    /// This represents the wrapper entity for the <see cref="Client"/> class.
    /// </summary>
    public class AzureBillingApiClientWrapper : IAzureBillingApiClientWrapper
    {
        private readonly AuthenticationSettings _auth;

        private Client _client;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBillingApiClientWrapper"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AuthenticationSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="settings"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException">Authentication settings not found</exception>
        public AzureBillingApiClientWrapper(ICostMonitoringSettings settings)
        {
            settings.ThrowIfNullOrEmpty();

            if (settings.Authentication.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Authentication settings not found");
            }

            this._auth = settings.Authentication;
        }

        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        public string SubscriptionId { get; set; }

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
        public async Task<ResourceCostData> GetResourceCostsAsync(string offerId, string currency, string locale, string regionInfo, DateTime dateStart, DateTime dateEnd, AggregationGranularity granularity, bool showDetails, string authToken = null)
        {
            this.EnsureClientIsReady();

            var result = await Task.Factory.StartNew(() => this._client.GetResourceCosts(offerId, currency, locale, regionInfo, dateStart, dateEnd, granularity, showDetails, authToken)).ConfigureAwait(false);

            return result;
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

            this._client = null;

            this._disposed = true;
        }

        private void EnsureClientIsReady()
        {
            if (!this._client.IsNullOrEmpty())
            {
                return;
            }

            if (this.SubscriptionId.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException("Subscription Id not exist");
            }

            this._client = new Client(this._auth.TenantId, this._auth.ApplicationId, this._auth.ApplicationSecret, this.SubscriptionId, this._auth.RedirectUrl);
        }
    }
}
using System;

using CostMonitoring.EntityModels;
using CostMonitoring.Helpers;
using CostMonitoring.Settings;
using CostMonitoring.Wrappers;

using Moq;

namespace CostMonitoring.Services.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="CostAggregationServiceTests"/> class.
    /// </summary>
    public class CostAggregationServiceFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostAggregationServiceFixture"/> class.
        /// </summary>
        public CostAggregationServiceFixture()
        {
            this.Settings = new Mock<ICostMonitoringSettings>();

            this.DbContext = new Mock<IMonitoringDbContext>();

            this.Authentication = new Mock<IAzureAuthenticationHelperWrapper>();

            this.Billing = new Mock<IAzureBillingApiClientHelper>();

            this.HttpClient = new Mock<IHttpClientHelper>();

            this.Service = new CostAggregationService(this.Settings.Object, this.DbContext.Object, this.Authentication.Object, this.Billing.Object, this.HttpClient.Object);
        }

        /// <summary>
        /// Gets the <see cref="Mock{ICostMonitoringSettings}"/> instance.
        /// </summary>
        public Mock<ICostMonitoringSettings> Settings { get; }

        /// <summary>
        /// Gets the <see cref="Mock{IMonitoringDbContext}"/> instance.
        /// </summary>
        public Mock<IMonitoringDbContext> DbContext { get; }

        /// <summary>
        /// Gets the <see cref="Mock{IAzureAuthenticationHelperWrapper}"/> instance.
        /// </summary>
        public Mock<IAzureAuthenticationHelperWrapper> Authentication { get; }

        /// <summary>
        /// Gets the <see cref="Mock{IAzureBillingApiClientHelper}"/> instance.
        /// </summary>
        public Mock<IAzureBillingApiClientHelper> Billing { get; }

        /// <summary>
        /// Gets the <see cref="Mock{IHttpClientHelper}"/> instance.
        /// </summary>
        public Mock<IHttpClientHelper> HttpClient { get; }

        /// <summary>
        /// Gets the <see cref="ICostAggregationService"/> instance.
        /// </summary>
        public ICostAggregationService Service { get; }

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

using System;

using CostMonitoring.Settings;

using Moq;

namespace CostMonitoring.Wrappers.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="AzureBillingApiClientWrapperTest"/> class.
    /// </summary>
    public class AzureBillingApiClientWrapperFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBillingApiClientWrapperFixture"/> class.
        /// </summary>
        public AzureBillingApiClientWrapperFixture()
        {
            this.SubscriptionId = Guid.NewGuid();

            var auth = new AuthenticationSettings()
                           {
                               TenantId = Guid.NewGuid().ToString(),
                               ApplicationId = Guid.NewGuid().ToString(),
                               ApplicationSecret = Guid.NewGuid().ToString(),
                               RedirectUrl = "https://localhost:443"
                           };

            this.Settings = new Mock<ICostMonitoringSettings>();
            this.Settings.SetupGet(p => p.Authentication).Returns(auth);

            this.Wrapper = new AzureBillingApiClientWrapper(this.Settings.Object);
        }

        /// <summary>
        /// Gets the <see cref="Mock{ICostMonitoringSettings}"/> instance.
        /// </summary>
        public Mock<ICostMonitoringSettings> Settings { get; }

        /// <summary>
        /// Gets the subscription Id.
        /// </summary>
        public Guid SubscriptionId { get; }

        /// <summary>
        /// Gets the <see cref="IAzureBillingApiClientWrapper"/> instance.
        /// </summary>
        public IAzureBillingApiClientWrapper Wrapper { get; }

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

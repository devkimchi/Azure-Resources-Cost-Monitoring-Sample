using System;

using CostMonitoring.Settings;
using CostMonitoring.Wrappers.Tests.Fixtures;

using FluentAssertions;

using Moq;

using Xunit;

namespace CostMonitoring.Wrappers.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="AzureBillingApiClientWrapper"/> class.
    /// </summary>
    public class AzureBillingApiClientWrapperTests : IClassFixture<AzureBillingApiClientWrapperFixture>
    {
        private readonly Mock<ICostMonitoringSettings> _settings;
        private readonly Guid _subscriptionId;
        private readonly IAzureBillingApiClientWrapper _wrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBillingApiClientWrapperTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="AzureBillingApiClientWrapperFixture"/> instance.</param>
        public AzureBillingApiClientWrapperTests(AzureBillingApiClientWrapperFixture fixture)
        {
            this._settings = fixture.Settings;
            this._subscriptionId = fixture.SubscriptionId;
            this._wrapper = fixture.Wrapper;
        }

        /// <summary>
        /// Tests whether the constructor throws an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            var settings = new Mock<ICostMonitoringSettings>();

            Action action = () => { var instance = new AzureBillingApiClientWrapper(null); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var instance = new AzureBillingApiClientWrapper(settings.Object); };
            action.ShouldThrow<InvalidOperationException>();
        }
    }
}

using System.Linq;

using CostMonitoring.Settings.Tests.Fixtures;

using FluentAssertions;

using Xunit;

namespace CostMonitoring.Settings.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="CostMonitoringSettings"/> class.
    /// </summary>
    public class CostMonitoringSettingsTests : IClassFixture<CostMonitoringSettingsFixture>
    {
        private readonly ICostMonitoringSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostMonitoringSettingsTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="CostMonitoringSettingsFixture"/> instance.</param>
        public CostMonitoringSettingsTests(CostMonitoringSettingsFixture fixture)
        {
            this._settings = fixture.Settings;
        }

        /// <summary>
        /// Tests whether the settings should have correct values or not.
        /// </summary>
        [Fact]
        public void Given_Settings_AllValues_ShouldReturn_Results()
        {
            var auth = this._settings.Authentication;
            auth.AadLoginUrl.Should().BeEquivalentTo("https://login.microsoftonline.com");
            auth.TenantId.Should().BeEquivalentTo("tenant-a");
            auth.ApplicationId.Should().BeEquivalentTo("application-a");
            auth.ApplicationSecret.Should().BeEquivalentTo("secret-a");
            auth.RedirectUrl.Should().BeEquivalentTo("https://redirecturl");

            var resources = this._settings.Resources;
            resources.BaseUrl.Should().BeEquivalentTo("https://management.azure.com/");
            resources.Endpoints.First().Name.Should().BeEquivalentTo("subscriptions");
            resources.Endpoints.First().Url.Should().BeEquivalentTo("subscriptions?api-version=2016-08-31-preview");

            var tags = this._settings.Tags;
            tags.OwnerEmailsKey.Should().BeEquivalentTo("OwnerEmails");
            tags.TotalSpendLimitKey.Should().BeEquivalentTo("TotalSpendLimit");
            tags.DailySpendLimitKey.Should().BeEquivalentTo("DailySpendLimit");
            tags.OverspendActionKey.Should().BeEquivalentTo("OverspendAction");
        }
    }
}

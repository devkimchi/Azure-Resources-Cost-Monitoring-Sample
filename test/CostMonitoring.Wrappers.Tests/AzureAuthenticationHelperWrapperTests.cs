using System;

using CostMonitoring.Settings;

using FluentAssertions;

using Moq;

using Xunit;

namespace CostMonitoring.Wrappers.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="AzureAuthenticationHelperWrapper"/> class.
    /// </summary>
    public class AzureAuthenticationHelperWrapperTests
    {
        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullSettings_GetOAuthTokenFromAad_ShouldThrow_Exception()
        {
            var settings = new Mock<ICostMonitoringSettings>();
            var wrapper = new AzureAuthenticationHelperWrapper();

            Action action = () => { var result = wrapper.GetOAuthTokenFromAad(null); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var result = wrapper.GetOAuthTokenFromAad(settings.Object); };
            action.ShouldThrow<InvalidOperationException>();

            settings.SetupGet(p => p.Authentication).Returns(new AuthenticationSettings());
            action = () => { var result = wrapper.GetOAuthTokenFromAad(settings.Object); };
            action.ShouldThrow<InvalidOperationException>();
        }
    }
}

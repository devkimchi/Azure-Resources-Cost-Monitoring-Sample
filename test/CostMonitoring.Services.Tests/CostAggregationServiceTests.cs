using System;

using CostMonitoring.EntityModels;
using CostMonitoring.Helpers;
using CostMonitoring.Services.Tests.Fixtures;
using CostMonitoring.Settings;
using CostMonitoring.Wrappers;

using FluentAssertions;

using Moq;

using Xunit;

namespace CostMonitoring.Services.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="CostAggregationService"/> class.
    /// </summary>
    public class CostAggregationServiceTests : IClassFixture<CostAggregationServiceFixture>
    {
        private readonly Mock<ICostMonitoringSettings> _settings;
        private readonly Mock<IMonitoringDbContext> _dbContext;
        private readonly Mock<IAzureAuthenticationHelperWrapper> _auth;
        private readonly Mock<IAzureBillingApiClientHelper> _billing;
        private readonly Mock<IHttpClientHelper> _httpClient;
        private readonly ICostAggregationService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostAggregationServiceTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="CostAggregationServiceFixture"/> instance.</param>
        public CostAggregationServiceTests(CostAggregationServiceFixture fixture)
        {
            this._settings = fixture.Settings;
            this._dbContext = fixture.DbContext;
            this._auth = fixture.Authentication;
            this._billing = fixture.Billing;
            this._httpClient = fixture.HttpClient;
            this._service = fixture.Service;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => { var instance = new CostAggregationService(null, this._dbContext.Object, this._auth.Object, this._billing.Object, this._httpClient.Object); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var instance = new CostAggregationService(this._settings.Object, null, this._auth.Object, this._billing.Object, this._httpClient.Object); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var instance = new CostAggregationService(this._settings.Object, this._dbContext.Object, null, this._billing.Object, this._httpClient.Object); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var instance = new CostAggregationService(this._settings.Object, this._dbContext.Object, this._auth.Object, null, this._httpClient.Object); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var instance = new CostAggregationService(this._settings.Object, this._dbContext.Object, this._auth.Object, this._billing.Object, null); };
            action.ShouldThrow<ArgumentNullException>();
        }
    }
}

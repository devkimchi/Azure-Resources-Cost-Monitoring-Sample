using System;
using System.Net;
using System.Threading.Tasks;

using CostMonitoring.Helpers.Tests.Fixtures;

using FluentAssertions;

using RichardSzalay.MockHttp;

using Xunit;

namespace CostMonitoring.Helpers.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="HttpClientHelper"/> class.
    /// </summary>
    public class HttpClientHelperTests : IClassFixture<HttpClientHelperFixture>
    {
        private readonly MockHttpMessageHandler _handler;
        private readonly IHttpClientHelper _helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientHelperTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="HttpClientHelperFixture"/> instance.</param>
        public HttpClientHelperTests(HttpClientHelperFixture fixture)
        {
            this._handler = fixture.Handler;
            this._helper = fixture.Helper;
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="authToken">Authentication token.</param>
        [Theory]
        [InlineData("https://localhost:443/hello", "authtoken")]
        public void Given_NullParameter_GetStringAsync_ShouldThrow_Exception(string requestUri, string authToken)
        {
            Func<Task> func = async () => { var result = await this._helper.GetStringAsync(null, authToken).ConfigureAwait(false); };
            func.ShouldThrow<ArgumentNullException>();

            func = async () => { var result = await this._helper.GetStringAsync(requestUri, null).ConfigureAwait(false); };
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="baseUrl">Base URL.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        /// <param name="authToken">Authentication token.</param>
        /// <param name="expected">Expected value.</param>
        [Theory]
        [InlineData("https://localhost:443", "hello", "authtoken", "Hello World")]
        public async void Given_Parameters_GetStringAsync_ShouldReturn_Result(string baseUrl, string endpoint, string authToken, string expected)
        {
            var json = $"{{ \"message\": \"{expected}\" }}";

            this._handler
                .When($"{baseUrl.TrimEnd('/')}/{endpoint}")
                .Respond(HttpStatusCode.OK, "application/json", json);

            this._helper.BaseAddress = new Uri(baseUrl);

            var result = await this._helper.GetStringAsync(endpoint, authToken).ConfigureAwait(false);

            result.Should().BeEquivalentTo(json);
        }
    }
}

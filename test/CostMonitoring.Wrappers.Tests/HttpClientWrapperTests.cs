using System;
using System.Net;
using System.Threading.Tasks;

using CostMonitoring.Wrappers.Tests.Fixtures;

using FluentAssertions;

using RichardSzalay.MockHttp;

using Xunit;

namespace CostMonitoring.Wrappers.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    public class HttpClientWrapperTests : IClassFixture<HttpClientWrapperFixture>
    {
        private readonly MockHttpMessageHandler _handler;
        private readonly IHttpClientWrapper _wrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapperTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="HttpClientWrapperFixture"/> instance.</param>
        public HttpClientWrapperTests(HttpClientWrapperFixture fixture)
        {
            this._handler = fixture.Handler;
            this._wrapper = fixture.Wrapper;
        }

        /// <summary>
        /// Test whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullEndpoint_GetStringAsync_ShouldThrow_Exception()
        {
            Func<Task> func = async () => { var result = await this._wrapper.GetStringAsync(null).ConfigureAwait(false); };

            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="baseUrl">Base URL.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        /// <param name="expected">Expected value.</param>
        [Theory]
        [InlineData("http://localhost", "greeting", "hello world")]
        public async void Given_Endpoint_GetStringAsync_ShouldReturn_Result(string baseUrl, string endpoint, string expected)
        {
            var json = $"{{ \"message\": \"{expected}\" }}";

            this._handler
                .When($"{baseUrl.TrimEnd('/')}/{endpoint}")
                .Respond(HttpStatusCode.OK, "application/json", json);

            this._wrapper.BaseAddress = new Uri(baseUrl);

            var result = await this._wrapper.GetStringAsync(endpoint).ConfigureAwait(false);

            result.Should().BeEquivalentTo(json);
        }
    }
}

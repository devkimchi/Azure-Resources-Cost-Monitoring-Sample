using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using CostMonitoring.Extensions;
using CostMonitoring.Wrappers;

namespace CostMonitoring.Helpers
{
    /// <summary>
    /// This represents the helper entity for the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly IHttpClientWrapper _client;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientHelper"/> class.
        /// </summary>
        /// <param name="handler"><see cref="HttpMessageHandler"/> instance.</param>
        public HttpClientHelper(HttpMessageHandler handler = null)
        {
            this._client = new HttpClientWrapper(handler);
        }


        /// <summary>
        /// Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.
        /// </summary>
        public Uri BaseAddress
        {
            get
            {
                return this._client.BaseAddress;
            }

            set
            {
                this._client.BaseAddress = value;
            }
        }

        /// <summary>
        /// Sends a GET request to the specified URI.
        /// </summary>
        /// <param name="requestUri">The URI the request is sent to.</param>
        /// <param name="authToken">Authentication token value.</param>
        /// <returns>Returns response body as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentNullException"><paramref name="authToken"/> is <see langword="null"/></exception>
        public async Task<string> GetStringAsync(string requestUri, string authToken)
        {
            if (requestUri.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (authToken.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(authToken));
            }

            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var result = await this._client.GetStringAsync(requestUri).ConfigureAwait(false);

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

            this._client.Dispose();

            this._disposed = true;
        }
    }
}

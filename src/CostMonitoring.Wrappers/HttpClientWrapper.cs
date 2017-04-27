using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using CostMonitoring.Extensions;

namespace CostMonitoring.Wrappers
{
    /// <summary>
    /// This represents the wrapper entity for the <see cref="HttpClient"/> class.
    /// </summary>
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
        /// </summary>
        /// <param name="handler"><see cref="HttpMessageHandler"/> instance.</param>
        public HttpClientWrapper(HttpMessageHandler handler = null)
        {
            this._client = handler == null ? new HttpClient() : new HttpClient(handler);
        }

        /// <summary>
        /// Gets the headers which should be sent with each request.
        /// </summary>
        public HttpRequestHeaders DefaultRequestHeaders => this._client.DefaultRequestHeaders;

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
        /// <returns>Returns response body as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null"/></exception>
        public async Task<string> GetStringAsync(string requestUri)
        {
            if (requestUri.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

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

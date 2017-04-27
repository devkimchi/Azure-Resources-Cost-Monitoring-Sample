using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CostMonitoring.Wrappers
{
    /// <summary>
    /// This provides interfaces to the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    public interface IHttpClientWrapper : IDisposable
    {
        /// <summary>
        /// Gets the headers which should be sent with each request.
        /// </summary>
        HttpRequestHeaders DefaultRequestHeaders { get; }

        /// <summary>
        /// Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.
        /// </summary>
        Uri BaseAddress { get; set; }

        /// <summary>
        /// Sends a GET request to the specified URI.
        /// </summary>
        /// <param name="requestUri">The URI the request is sent to.</param>
        /// <returns>Returns response body as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null"/></exception>
        Task<string> GetStringAsync(string requestUri);
    }
}
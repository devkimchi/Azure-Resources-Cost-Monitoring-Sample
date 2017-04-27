using System;
using System.Threading.Tasks;

namespace CostMonitoring.Helpers
{
    /// <summary>
    /// This provides interfaces to the <see cref="HttpClientHelper"/> class.
    /// </summary>
    public interface IHttpClientHelper : IDisposable
    {
        /// <summary>
        /// Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.
        /// </summary>
        Uri BaseAddress { get; set; }

        /// <summary>
        /// Sends a GET request to the specified URI.
        /// </summary>
        /// <param name="requestUri">The URI the request is sent to.</param>
        /// <param name="authToken">Authentication token value.</param>
        /// <returns>Returns response body as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentNullException"><paramref name="authToken"/> is <see langword="null"/></exception>
        Task<string> GetStringAsync(string requestUri, string authToken);
    }
}
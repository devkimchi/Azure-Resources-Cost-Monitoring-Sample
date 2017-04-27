using System;

using RichardSzalay.MockHttp;

namespace CostMonitoring.Helpers.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="HttpClientHelperTest"/> class.
    /// </summary>
    public class HttpClientHelperFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientHelperFixture"/> class.
        /// </summary>
        public HttpClientHelperFixture()
        {
            this.Handler = new MockHttpMessageHandler();

            this.Helper = new HttpClientHelper(this.Handler);
        }

        /// <summary>
        /// Gets the <see cref="MockHttpMessageHandler"/> instance.
        /// </summary>
        public MockHttpMessageHandler Handler { get; }

        /// <summary>
        /// Gets the <see cref="IHttpClientHelper"/> instance.
        /// </summary>
        public IHttpClientHelper Helper { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}

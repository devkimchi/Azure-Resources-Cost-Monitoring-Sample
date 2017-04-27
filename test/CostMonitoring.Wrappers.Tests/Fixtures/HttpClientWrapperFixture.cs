using System;

using RichardSzalay.MockHttp;

namespace CostMonitoring.Wrappers.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="HttpClientWrapperTests"/> class.
    /// </summary>
    public class HttpClientWrapperFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapperFixture"/> class.
        /// </summary>
        public HttpClientWrapperFixture()
        {
            this.Handler = new MockHttpMessageHandler();

            this.Wrapper = new HttpClientWrapper(this.Handler);
        }

        /// <summary>
        /// Gets the <see cref="MockHttpMessageHandler"/> instance.
        /// </summary>
        public MockHttpMessageHandler Handler { get; }

        /// <summary>
        /// Gets the <see cref="IHttpClientWrapper"/> instance.
        /// </summary>
        public IHttpClientWrapper Wrapper { get; }

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

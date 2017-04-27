using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostMonitoring.Settings.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="CostMonitoringSettingsTests"/> class.
    /// </summary>
    public class CostMonitoringSettingsFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostMonitoringSettingsFixture"/> class.
        /// </summary>
        public CostMonitoringSettingsFixture()
        {
            this.Settings = CostMonitoringSettings.CreateInstance();
        }

        /// <summary>
        /// Gets the <see cref="ICostMonitoringSettings"/> instance.
        /// </summary>
        public ICostMonitoringSettings Settings { get; }

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

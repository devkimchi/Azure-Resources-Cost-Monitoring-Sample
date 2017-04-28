using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace CostMonitoring.Settings
{
    /// <summary>
    /// This represents the settings entity for cost monitoring directly read from <c>costmonitoring.settings</c>.
    /// </summary>
    public class CostMonitoringSettings : ICostMonitoringSettings
    {
        private const string CostMonitoringSettingsFile = "costmonitoringsettings.json";

        private bool _disposed;

        /// <summary>
        /// Gets or sets the <see cref="AuthenticationSettings"/> instance.
        /// </summary>
        public AuthenticationSettings Authentication { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ResourceEndpointCollectionSettings"/> instance.
        /// </summary>
        public ResourceEndpointCollectionSettings Resources { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="BillingSettings"/> instance.
        /// </summary>
        public BillingSettings Billing { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TagSettings"/> instance.
        /// </summary>
        public TagSettings Tags { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ICostMonitoringSettings"/> class.
        /// </summary>
        /// <returns>Returns the new instance of the <see cref="ICostMonitoringSettings"/> class.</returns>
        public static ICostMonitoringSettings CreateInstance()
        {
            using (var reader = new StreamReader(CostMonitoringSettingsFile, Encoding.UTF8))
            {
                var serialised = reader.ReadToEnd();
                var settings = JsonConvert.DeserializeObject<CostMonitoringSettings>(serialised);

                return settings;
            }
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

            this._disposed = true;
        }
    }
}

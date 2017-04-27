using System.Collections.Generic;

namespace CostMonitoring.Settings
{
    /// <summary>
    /// This represents the settings entity for the collection of the resource endpoints.
    /// </summary>
    public class ResourceEndpointCollectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceEndpointCollectionSettings"/> class.
        /// </summary>
        public ResourceEndpointCollectionSettings()
        {
            this.Endpoints = new List<ResourceEndpointSettings>();
        }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of endpoints.
        /// </summary>
        public List<ResourceEndpointSettings> Endpoints { get; set; }
    }
}
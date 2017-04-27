namespace CostMonitoring.Settings
{
    /// <summary>
    /// This represents the settings entity for resource endpoint.
    /// </summary>
    public class ResourceEndpointSettings
    {
        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the endpoint of the resource.
        /// </summary>
        public string Url { get; set; }
    }
}
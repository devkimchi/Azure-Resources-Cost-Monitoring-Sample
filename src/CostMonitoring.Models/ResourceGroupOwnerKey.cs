namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the key entity for resource group owner.
    /// </summary>
    public class ResourceGroupOwnerKey
    {
        /// <summary>
        /// Gets or sets the subscription name.
        /// </summary>
        public string Subscription { get; set; }

        /// <summary>
        /// Gets or sets the resource group name.
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the owners of the resource group.
        /// </summary>
        public string Owners { get; set; }
    }
}
using System.Collections.Generic;

namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the entity for Azure subscriptions.
    /// </summary>
    public class Subscriptions
    {
        /// <summary>
        /// Gets or sets the list of subscriptions.
        /// </summary>
        public List<Subscription> Value { get; set; }
    }
}

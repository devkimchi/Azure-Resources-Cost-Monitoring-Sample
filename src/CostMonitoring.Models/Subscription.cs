namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the entity for Azure subscription.
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the display name of the subscription.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the current state of the subscription.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the subscription policies.
        /// </summary>
        public SubscriptionPolicies SubscriptionPolicies { get; set; }

        /// <summary>
        /// Gets or sets the authorization source.
        /// </summary>
        public string AuthorizationSource { get; set; }
    }
}
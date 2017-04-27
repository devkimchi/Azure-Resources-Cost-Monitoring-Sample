namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the entity for subscription policies.
    /// </summary>
    public class SubscriptionPolicies
    {
        /// <summary>
        /// Gets or sets the Id for location placement.
        /// </summary>
        public string LocationPlacementId { get; set; }

        /// <summary>
        /// Gets or sets the quota Id.
        /// </summary>
        public string QuotaId { get; set; }

        /// <summary>
        /// Gets or sets the spending limit.
        /// </summary>
        public string SpendingLimit { get; set; }
    }
}
using System;

namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the entity for resource group owner.
    /// </summary>
    public class ResourceGroupOwner : ResourceGroupOwnerKey
    {
        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the cost of the resource group.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the total spend limit of the resource group since creation.
        /// </summary>
        public decimal TotalSpendLimit { get; set; }

        /// <summary>
        /// Gets or sets the daily spend limit of the resource group.
        /// </summary>
        public decimal DailySpendLimit { get; set; }

        /// <summary>
        /// Gets or sets the action to be taken when total spend is over budget.
        /// </summary>
        public string OverspendAction { get; set; }
    }
}

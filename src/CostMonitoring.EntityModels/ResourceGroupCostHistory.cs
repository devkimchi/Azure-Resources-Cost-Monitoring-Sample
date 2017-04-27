using System;

namespace CostMonitoring.EntityModels
{
    /// <summary>
    /// This represents the entity for resource group cost.
    /// </summary>
    public class ResourceGroupCostHistory
    {
        /// <summary>
        /// Gets or sets the resource group cost history Id.
        /// </summary>
        public Guid ResourceGroupCostHistoryId { get; set; }

        /// <summary>
        /// Gets or sets the subscription name.
        /// </summary>
        public string Subscription { get; set; }

        /// <summary>
        /// Gets or sets the subscription Id.
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the resource group name.
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the owners of resource group.
        /// </summary>
        public string Owners { get; set; }

        /// <summary>
        /// Gets or sets the cost for resource group.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the total spending limit amount for resource group since creation.
        /// </summary>
        public decimal TotalSpendLimit { get; set; }

        /// <summary>
        /// Gets or sets the daily spending limit amount for resource group.
        /// </summary>
        public decimal DailySpendLimit { get; set; }

        /// <summary>
        /// Gets or sets the action when the resource group reaches the spend limit.
        /// </summary>
        public string OverspendAction { get; set; }

        /// <summary>
        /// Gets or sets the date when the cost calculation started.
        /// </summary>
        public DateTimeOffset DateStart { get; set; }

        /// <summary>
        /// Gets or sets the date when the cost calculation ended.
        /// </summary>
        public DateTimeOffset DateEnd { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }
    }
}
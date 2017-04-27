using System;

namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the entity for resource group cost result key.
    /// </summary>
    public class ResourceGroupCostResultKey
    {
        /// <summary>
        /// Gets or sets the resource group name.
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the resource group owner.
        /// </summary>
        public string OwnerEmails { get; set; }

        /// <summary>
        /// Gets or sets the total spend limit for the resource group since creation.
        /// </summary>
        public string TotalSpendLimit { get; set; }

        /// <summary>
        /// Gets or sets the daily spend limit for the resource group.
        /// </summary>
        public string DailySpendLimit { get; set; }

        /// <summary>
        /// Gets or sets the overspent action for the resource group.
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
        /// Gets the <see cref="ResourceGroupCostResultKey"/> instance.
        /// </summary>
        /// <param name="result"><see cref="ResourceGroupCostResult"/> instance.</param>
        /// <returns>Returns the <see cref="ResourceGroupCostResultKey"/> instance.</returns>
        public static ResourceGroupCostResultKey GetResourceGroupCostResultKey(ResourceGroupCostResult result)
        {
            var key = new ResourceGroupCostResultKey()
                          {
                              ResourceGroupName = result.ResourceGroupName,
                              OwnerEmails = result.OwnerEmails,
                              TotalSpendLimit = result.TotalSpendLimit,
                              DailySpendLimit = result.DailySpendLimit,
                              OverspendAction = result.OverspendAction,
                              DateStart = result.DateStart,
                              DateEnd = result.DateEnd
                          };

            return key;
        }
    }
}
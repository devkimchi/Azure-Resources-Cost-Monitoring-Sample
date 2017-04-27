using System;
using System.Collections.Generic;
using System.Linq;

using CodeHollow.AzureBillingApi;

namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the entity for resource group cost.
    /// </summary>
    public class ResourceGroupCost
    {
        /// <summary>
        /// Gets or sets the resource group name.
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTimeOffset DateStart { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTimeOffset DateEnd { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Gets the <see cref="ResourceGroupCost"/> instance.
        /// </summary>
        /// <param name="group"><see cref="ResourceGroupDateKey"/> instance.</param>
        /// <param name="records">List of <see cref="ResourceCosts"/> instances.</param>
        /// <returns>Returns the <see cref="ResourceGroupCost"/> instance.</returns>
        public static ResourceGroupCost GetResourceGroupCost(ResourceGroupDateKey group, IEnumerable<ResourceCosts> records)
        {
            var cost = new ResourceGroupCost()
                           {
                               ResourceGroupName = group.ResourceGroupName,
                               Cost = records.Sum(p => p.CalculatedCosts)
                           };

            return cost;
        }
    }
}

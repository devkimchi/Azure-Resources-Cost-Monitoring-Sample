using System;
using System.Collections.Generic;
using System.Linq;

using CostMonitoring.Extensions;
using CostMonitoring.Settings;

using Microsoft.Azure.Management.ResourceManager.Models;

namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the entity for resource group cost result.
    /// </summary>
    public class ResourceGroupCostResult : ResourceGroupCostResultKey
    {
        /// <summary>
        /// Gets or sets the resource group cost.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets the <see cref="ResourceGroupCostResult"/> instance.
        /// </summary>
        /// <param name="cost"><see cref="ResourceGroupCost"/> instance.</param>
        /// <param name="groups">List of <see cref="ResourceGroup"/> instances.</param>
        /// <param name="tagSettings"><see cref="TagSettings"/> instance.</param>
        /// <returns>Returns the <see cref="ResourceGroupCostResult"/> instance.</returns>
        public static ResourceGroupCostResult GetResourceGroupCostResult(ResourceGroupCost cost, IEnumerable<ResourceGroup> groups, TagSettings tagSettings)
        {
            var group = groups.SingleOrDefault(p => p.Name.IsEquivalentTo(cost.ResourceGroupName));
            var tags = group?.Tags;

            var result = new ResourceGroupCostResult()
                             {
                                 ResourceGroupName = cost.ResourceGroupName,
                                 Cost = Convert.ToDecimal(cost.Cost),
                                 OwnerEmails = tags?.SingleOrDefault(p => p.Key.IsEquivalentTo(tagSettings.OwnerEmailsKey)).Value,
                                 TotalSpendLimit = tags?.SingleOrDefault(p => p.Key.IsEquivalentTo(tagSettings.TotalSpendLimitKey)).Value,
                                 DailySpendLimit = tags?.SingleOrDefault(p => p.Key.IsEquivalentTo(tagSettings.DailySpendLimitKey)).Value,
                                 OverspendAction = tags?.SingleOrDefault(p => p.Key.IsEquivalentTo(tagSettings.OverspendActionKey)).Value,
                                 DateStart = cost.DateStart,
                                 DateEnd = cost.DateEnd
                             };
            return result;
        }

        /// <summary>
        /// Gets the <see cref="ResourceGroupCostResult"/> instance.
        /// </summary>
        /// <param name="group"><see cref="ResourceGroupCostResultKey"/> instance.</param>
        /// <param name="records">List of <see cref="ResourceGroupCostResult"/> instances.</param>
        /// <returns>Returns the <see cref="ResourceGroupCostResult"/> instance.</returns>
        public static ResourceGroupCostResult GetResourceGroupCostResult(ResourceGroupCostResultKey group, IEnumerable<ResourceGroupCostResult> records)
        {
            var result = new ResourceGroupCostResult()
                             {
                                 ResourceGroupName = group.ResourceGroupName,
                                 OwnerEmails = group.OwnerEmails,
                                 Cost = records.Sum(q => q.Cost),
                                 DateStart = group.DateStart,
                                 DateEnd = group.DateEnd,
                                 TotalSpendLimit = group.TotalSpendLimit,
                                 DailySpendLimit = group.DailySpendLimit,
                                 OverspendAction = group.OverspendAction
                             };

            return result;
        }
    }
}
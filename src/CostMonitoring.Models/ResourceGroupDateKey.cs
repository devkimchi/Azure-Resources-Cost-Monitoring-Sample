using System;

using CodeHollow.AzureBillingApi;

using CostMonitoring.Extensions;

namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the entity for resource group date key.
    /// </summary>
    public class ResourceGroupDateKey
    {
        /// <summary>
        /// Gets or sets the resource group name.
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public string DateStart { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public string DateEnd { get; set; }

        /// <summary>
        /// Gets the <see cref="ResourceGroupDateKey"/> instance.
        /// </summary>
        /// <param name="cost"><see cref="ResourceCosts"/> instance.</param>
        /// <returns>Returns the <see cref="ResourceGroupDateKey"/> instance.</returns>
        public static ResourceGroupDateKey GetResourceGroupDateKey(ResourceCosts cost)
        {
            var rgd = new ResourceGroupDateKey()
                          {
                              ResourceGroupName = string.Empty,
                              DateStart = cost.UsageValue.Properties.UsageStartTimeAsDate.ToUniversalTime().ToString("O"),
                              DateEnd = cost.UsageValue.Properties.UsageEndTimeAsDateTime.ToUniversalTime().ToString("O")
                          };

            var resourceGroupName = GetResourceGroupKey(cost);
            if (resourceGroupName.IsNullOrWhiteSpace())
            {
                return rgd;
            }

            rgd.ResourceGroupName = resourceGroupName.ToLowerInvariant();

            return rgd;
        }

        /// <summary>
        /// Gets the <see cref="ResourceGroupDateKey"/> instance.
        /// </summary>
        /// <param name="cost"><see cref="ResourceCosts"/> instance.</param>
        /// <returns>Returns the <see cref="ResourceGroupDateKey"/> instance.</returns>
        public static string GetResourceGroupKey(ResourceCosts cost)
        {
            var uri = cost?.UsageValue?.Properties?.InstanceData?.MicrosoftResources?.ResourceUri;
            if (uri.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            var startIndex = uri.IndexOf("/ResourceGroups/", StringComparison.CurrentCultureIgnoreCase);
            if (startIndex < 0)
            {
                return string.Empty;
            }

            startIndex += "/ResourceGroups/".Length;
            var endIndex = uri.IndexOf("/", startIndex, StringComparison.CurrentCultureIgnoreCase);

            var resourceGroupName = endIndex < 0
                                        ? uri.Substring(startIndex)
                                        : uri.Substring(startIndex, endIndex - startIndex);

            return resourceGroupName.ToLowerInvariant();
        }
    }
}

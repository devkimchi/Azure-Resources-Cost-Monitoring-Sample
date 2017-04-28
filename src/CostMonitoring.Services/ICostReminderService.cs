using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CostMonitoring.Models;

namespace CostMonitoring.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="CostReminderService"/> class.
    /// </summary>
    public interface ICostReminderService : IDisposable
    {
        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <param name="runEntirePeriod">Value indicating whether to run entire period or not.</param>
        /// <param name="threshold">Threshold value for total spend limit calculation.</param>
        /// <returns>Returns the number of records processed.</returns>
        Task<int> ProcessAsync(DateTime dateStart, DateTime dateEnd, bool runEntirePeriod, decimal threshold);

        /// <summary>
        /// Gets the list of <see cref="ResourceGroupOwner"/> instances.
        /// </summary>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <param name="runEntirePeriod">Value indicating whether to run entire period or not.</param>
        /// <param name="threshold">Threshold value for total spend limit calculation.</param>
        /// <returns>Returns the list of <see cref="ResourceGroupOwner"/> instances.</returns>
        Task<IEnumerable<ResourceGroupOwner>> GetResourceGroupOwnersAsync(DateTime dateStart, DateTime dateEnd, bool runEntirePeriod, decimal threshold);

        /// <summary>
        /// Takes an action to the resource group owner.
        /// </summary>
        /// <param name="owner"><see cref="ResourceGroupOwner"/> instance.</param>
        /// <param name="runEntirePeriod">Value indicating whether to run entire period or not.</param>
        /// <param name="threshold">Threshold value for total spend limit calculation.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task TakeActionAsync(ResourceGroupOwner owner, bool runEntirePeriod, decimal threshold);
    }
}
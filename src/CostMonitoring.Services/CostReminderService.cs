using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CostMonitoring.EntityModels;
using CostMonitoring.Extensions;
using CostMonitoring.Models;
using CostMonitoring.Settings;

namespace CostMonitoring.Services
{
    /// <summary>
    /// This represents the service entity for the Azure billing cost reminder.
    /// </summary>
    public class CostReminderService : ICostReminderService
    {
        private readonly ICostMonitoringSettings _settings;
        private readonly IMonitoringDbContext _dbContext;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="CostReminderService"/> class.
        /// </summary>
        /// <param name="settings"><see cref="ICostMonitoringSettings"/> instance.</param>
        /// <param name="dbContext"><see cref="IMonitoringDbContext"/> instance.</param>
        public CostReminderService(ICostMonitoringSettings settings, IMonitoringDbContext dbContext)
        {
            this._settings = settings.ThrowIfNullOrEmpty();
            this._dbContext = dbContext.ThrowIfNullOrEmpty();
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <param name="runEntirePeriod">Value indicating whether to run entire period or not.</param>
        /// <param name="threshold">Threshold value for total spend limit calculation.</param>
        /// <returns>Returns the number of records processed.</returns>
        public async Task<int> ProcessAsync(DateTime dateStart, DateTime dateEnd, bool runEntirePeriod, decimal threshold)
        {
            var owners = await this.GetResourceGroupOwnersAsync(dateStart, dateEnd, runEntirePeriod, threshold).ConfigureAwait(false);

            foreach (var owner in owners)
            {
                await this.TakeActionAsync(owner, runEntirePeriod, threshold).ConfigureAwait(false);
            }

            return owners.Count();
        }

        /// <summary>
        /// Gets the list of <see cref="ResourceGroupOwner"/> instances.
        /// </summary>
        /// <param name="dateStart">Date starts calculation.</param>
        /// <param name="dateEnd">Date ends calculation.</param>
        /// <param name="runEntirePeriod">Value indicating whether to run entire period or not.</param>
        /// <param name="threshold">Threshold value for total spend limit calculation.</param>
        /// <returns>Returns the list of <see cref="ResourceGroupOwner"/> instances.</returns>
        public async Task<IEnumerable<ResourceGroupOwner>> GetResourceGroupOwnersAsync(DateTime dateStart, DateTime dateEnd, bool runEntirePeriod, decimal threshold)
        {
            var costs = await this._dbContext.ResourceGroupCostHistories
                                  .ToListAsync().ConfigureAwait(false);

            if (dateStart > DateTime.MinValue.ToUniversalTime())
            {
                costs = costs.Where(p => p.DateStart >= new DateTimeOffset(dateStart)).ToList();
            }

            if (dateEnd < DateTime.MaxValue.ToUniversalTime())
            {
                costs = costs.Where(p => p.DateEnd < new DateTimeOffset(dateEnd)).ToList();
            }

            var owners = costs.GroupBy(
                                       p => new
                                                {
                                                    Subscription = p.Subscription,
                                                    ResourceGroupName = p.ResourceGroupName,
                                                    Owners = p.Owners
                                                })
                              .Select(
                                      p => new ResourceGroupOwner()
                                               {
                                                   Subscription = p.Key.Subscription,
                                                   SubscriptionId = p.OrderByDescending(q => q.DateStart).First().SubscriptionId,
                                                   ResourceGroupName = p.Key.ResourceGroupName,
                                                   Owners = p.Key.Owners,
                                                   Cost = p.Sum(q => q.Cost),
                                                   TotalSpendLimit = p.OrderByDescending(q => q.DateStart).First().TotalSpendLimit,
                                                   DailySpendLimit = p.OrderByDescending(q => q.DateStart).First().DailySpendLimit,
                                                   OverspendAction = p.OrderByDescending(q => q.DateStart).First().OverspendAction
                                               })
                              .Where(p => p.Owners != null);

            // Total spend limit
            if (runEntirePeriod)
            {
                owners = owners.Where(p => p.TotalSpendLimit > 0.00M);

                // Threshold
                owners = threshold < 1.00M
                             ? owners.Where(p => p.TotalSpendLimit * threshold <= p.Cost && p.Cost < p.TotalSpendLimit)
                             : owners.Where(p => p.Cost >= p.TotalSpendLimit * threshold);
            }
            else
            {
                // Daily spend limit
                owners = owners
                               .Where(p => p.DailySpendLimit > 0.00M)
                               .Where(p => p.Cost > p.DailySpendLimit);
            }

            owners = owners.OrderBy(p => p.Owners)
                           .ThenBy(p => p.ResourceGroupName)
                           .ToList();

            return owners;
        }

        /// <summary>
        /// Takes an action to the resource group owner.
        /// </summary>
        /// <param name="owner"><see cref="ResourceGroupOwner"/> instance.</param>
        /// <param name="runEntirePeriod">Value indicating whether to run entire period or not.</param>
        /// <param name="threshold">Threshold value for total spend limit calculation.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        public async Task TakeActionAsync(ResourceGroupOwner owner, bool runEntirePeriod, decimal threshold)
        {
            var owners = owner.Owners.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();
            sb.AppendLine($"An alert has been sent to {string.Join(" & ", owners)}");

            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}

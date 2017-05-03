using System;
using System.Configuration;
using System.Threading.Tasks;

using CostMonitoring.EntityModels;
using CostMonitoring.Helpers;
using CostMonitoring.Services;
using CostMonitoring.Settings;
using CostMonitoring.Wrappers;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace CostMonitoring.Functions
{
    /// <summary>
    /// This represents the runner entity for Azure Functions.
    /// </summary>
    public static class CostMonitoringRunner
    {
        /// <summary>
        /// Runs the timer trigger function.
        /// </summary>
        /// <param name="myTimer"><see cref="TimerInfo"/> instance.</param>
        /// <param name="log"><see cref="TraceWriter"/> instance.</param>
        public static async void Run(TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var connString = ConfigurationManager.ConnectionStrings["MonitoringDbContext"].ConnectionString;
            var settings = CostMonitoringSettings.CreateInstance();
            var options = settings.Arguments;
            var dbContext = new MonitoringDbContext(connString);
            var auth = new AzureAuthenticationHelperWrapper();
            var billing = new AzureBillingApiClientHelper(settings);
            var httpClient = new HttpClientHelper();

            var aggregator = new CostAggregationService(settings, dbContext, auth, billing, httpClient);

            if (options.RunEntirePeriod)
            {
                var monthsBack = 0;
                var result = 1;
                while (result > 0)
                {
                    result = await ProcessAggregatorAsync(aggregator, log, monthsBack: monthsBack).ConfigureAwait(false);
                    monthsBack++;
                }
            }
            else
            {
                var result = await ProcessAggregatorAsync(aggregator, log, options: options).ConfigureAwait(false);
            }

            var reminder = new CostReminderService(settings, dbContext);
            var owners = await ProcessReminderAsync(reminder, log, options).ConfigureAwait(false);
        }

        private static async Task<int> ProcessAggregatorAsync(ICostAggregationService service, TraceWriter log, ArgumentOptions options = null, int? monthsBack = null)
        {
            var dateStart = options != null ? BillingHelper.GetStartDate(options) : BillingHelper.GetStartDateOfMonth(monthsBack.Value);
            var dateEnd = options != null ? BillingHelper.GetEndDate(options) : BillingHelper.GetEndDateOfMonth(monthsBack.Value);

            log.Info($"Usage from {dateStart.ToLocalTime():yyyy-MM-dd HH:mm:ss} to {dateEnd.ToLocalTime():yyyy-MM-dd HH:mm:ss}:");

            var result = await service.ProcessAsync(dateStart, dateEnd).ConfigureAwait(false);

            return result;
        }

        private static async Task<int> ProcessReminderAsync(ICostReminderService service, TraceWriter log, ArgumentOptions options)
        {
            var dateStart = BillingHelper.GetStartDate(options);
            var dateEnd = BillingHelper.GetEndDate(options);

            log.Info(options.RunEntirePeriod
                         ? $"Usage from the entire billing history:"
                         : $"Usage from {dateStart.ToLocalTime():yyyy-MM-dd HH:mm:ss} to {dateEnd.AddDays(-1).ToLocalTime():yyyy-MM-dd HH:mm:ss}:");

            var result = await service.ProcessAsync(dateStart, dateEnd, options.RunEntirePeriod, (decimal)options.Threshold).ConfigureAwait(false);

            return result;
        }
    }
}
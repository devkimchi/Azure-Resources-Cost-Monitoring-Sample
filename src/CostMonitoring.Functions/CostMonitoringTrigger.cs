using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using CostMonitoring.EntityModels;
using CostMonitoring.Helpers;
using CostMonitoring.Services;
using CostMonitoring.Settings;
using CostMonitoring.Wrappers;

using Microsoft.Azure.WebJobs.Host;

namespace CostMonitoring.Functions
{
    /// <summary>
    /// This represents the trigger entity for Azure Functions.
    /// </summary>
    public static class CostMonitoringTrigger
    {
        /// <summary>
        /// Runs the HTTP trigger function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="TraceWriter"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var options = await req.Content.ReadAsAsync<ArgumentOptions>().ConfigureAwait(false);

            var connString = ConfigurationManager.ConnectionStrings["MonitoringDbContext"].ConnectionString;

            var settings = CostMonitoringSettings.CreateInstance();
            if (options == null)
            {
                options = settings.Arguments;
            }

            var dbContext = new MonitoringDbContext(connString);
            var auth = new AzureAuthenticationHelperWrapper();
            var billing = new AzureBillingApiClientHelper(settings);
            var httpClient = new HttpClientHelper();

            var aggregator = new CostAggregationService(settings, dbContext, auth, billing, httpClient);

            var result = 0;
            if (!options.RunEntirePeriod)
            {
                result = await ProcessAggregatorAsync(aggregator, log, options: options).ConfigureAwait(false);
            }
            else
            {
                var monthsBack = 0;
                result = 1;
                while (result > 0)
                {
                    result = await ProcessAggregatorAsync(aggregator, log, monthsBack: monthsBack).ConfigureAwait(false);
                    monthsBack++;
                }
            }

            var reminder = new CostReminderService(settings, dbContext);
            var owners = await ProcessReminderAsync(reminder, log, options).ConfigureAwait(false);

            return req.CreateResponse(HttpStatusCode.OK, new { Processed = result, Owners = owners });
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
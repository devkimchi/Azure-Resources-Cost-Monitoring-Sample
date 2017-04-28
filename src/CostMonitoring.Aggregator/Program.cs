using System;
using System.Diagnostics;
using System.Threading.Tasks;

using CommandLine;

using CostMonitoring.EntityModels;
using CostMonitoring.Helpers;
using CostMonitoring.Models;
using CostMonitoring.Services;
using CostMonitoring.Settings;
using CostMonitoring.Wrappers;

namespace CostMonitoring.Aggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new ArgumentOptions();
            Parser.Default.ParseArguments(args, options);

            var settings = CostMonitoringSettings.CreateInstance();
            var dbContext = new MonitoringDbContext();
            var auth = new AzureAuthenticationHelperWrapper();
            var billing = new AzureBillingApiClientHelper(settings);
            var httpClient = new HttpClientHelper();

            var service = new CostAggregationService(settings, dbContext, auth, billing, httpClient);

            Console.WriteLine("Azure Resources Usage Cost Aggregator");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            if (!options.RunEntirePeriod)
            {
                var result = ProcessAsync(service, options: options).Result;
            }
            else
            {
                var monthsBack = 0;
                var result = 1;
                while (result > 0)
                {
                    result = ProcessAsync(service, monthsBack: monthsBack).Result;
                    monthsBack++;
                }
            }
        }

        private static async Task<int> ProcessAsync(ICostAggregationService service, ArgumentOptions options = null, int? monthsBack = null)
        {
            var dateStart = options != null ? BillingHelper.GetStartDate(options) : BillingHelper.GetStartDateOfMonth(monthsBack.Value);
            var dateEnd = options != null ? BillingHelper.GetEndDate(options) : BillingHelper.GetEndDateOfMonth(monthsBack.Value);

            Console.WriteLine($"Usage from {dateStart.ToLocalTime():yyyy-MM-dd HH:mm:ss} to {dateEnd.ToLocalTime():yyyy-MM-dd HH:mm:ss}:");
            Console.WriteLine();

            var watch = Stopwatch.StartNew();

            var result = await service.ProcessAsync(dateStart, dateEnd).ConfigureAwait(false);

            watch.Stop();

            Console.WriteLine();
            Console.WriteLine($"Execution time: {watch.ElapsedMilliseconds} ms");

            return result;
        }
    }
}

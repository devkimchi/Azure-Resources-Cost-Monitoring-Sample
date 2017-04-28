using System;
using System.Diagnostics;
using System.Threading.Tasks;

using CommandLine;

using CostMonitoring.EntityModels;
using CostMonitoring.Helpers;
using CostMonitoring.Models;
using CostMonitoring.Services;
using CostMonitoring.Settings;

namespace CostMonitoring.Reminder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new ArgumentOptions();
            Parser.Default.ParseArguments(args, options);

            var settings = CostMonitoringSettings.CreateInstance();
            var dbContext = new MonitoringDbContext();

            var service = new CostReminderService(settings, dbContext);

            Console.WriteLine("Azure Resources Usage Reminder");
            Console.WriteLine("------------------------------");
            Console.WriteLine();

            var result = ProcessAsync(service, options).Result;
        }

        private static async Task<int> ProcessAsync(ICostReminderService service, ArgumentOptions options)
        {
            var dateStart = BillingHelper.GetStartDate(options);
            var dateEnd = BillingHelper.GetEndDate(options);

            Console.WriteLine(
                              options.RunEntirePeriod
                                  ? $"Usage from the entire billing history:"
                                  : $"Usage from {dateStart.ToLocalTime():yyyy-MM-dd HH:mm:ss} to {dateEnd.AddDays(-1).ToLocalTime():yyyy-MM-dd HH:mm:ss}:");
            Console.WriteLine();

            var watch = Stopwatch.StartNew();

            var result = await service.ProcessAsync(dateStart, dateEnd, options.RunEntirePeriod, (decimal)options.Threshold).ConfigureAwait(false);

            watch.Stop();

            Console.WriteLine();
            Console.WriteLine($"Execution time: {watch.ElapsedMilliseconds} ms");

            return result;
        }
    }
}

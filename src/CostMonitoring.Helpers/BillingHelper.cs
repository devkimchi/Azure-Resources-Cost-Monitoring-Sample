using System;

using CostMonitoring.Extensions;
using CostMonitoring.Models;
using CostMonitoring.Settings;

namespace CostMonitoring.Helpers
{
    /// <summary>
    /// This represents the helper entity for the Azure billing.
    /// </summary>
    public static class BillingHelper
    {
        /// <summary>
        /// Gets the start date of the billing.
        /// </summary>
        /// <param name="options"><see cref="ArgumentOptions"/> instance.</param>
        /// <returns>Returns the start date based on options.</returns>
        public static DateTime GetStartDate(ArgumentOptions options)
        {
            if (options.RunEntirePeriod)
            {
                return DateTime.MinValue.ToUniversalTime();
            }

            var dt = options.DateStart.IsNullOrWhiteSpace()
                         ? DateTime.Today.AddDays(options.Retro * (-1))
                         : DateTime.Parse(options.DateStart);
            DateTime.SpecifyKind(dt, DateTimeKind.Local);

            var dateStart = dt.ToUniversalTime();

            return options.RunEntirePeriod ? DateTime.MinValue : dateStart;
        }

        /// <summary>
        /// Gets the end date of the billing.
        /// </summary>
        /// <param name="options"><see cref="ArgumentOptions"/> instance.</param>
        /// <returns>Returns the end date based on options.</returns>
        public static DateTime GetEndDate(ArgumentOptions options)
        {
            if (options.RunEntirePeriod)
            {
                return DateTime.MaxValue.ToUniversalTime();
            }

            var dt = options.DateEnd.IsNullOrWhiteSpace()
                         ? (options.DateStart.IsNullOrWhiteSpace()
                                ? DateTime.Today.AddDays((options.Retro - options.Duration) * (-1))
                                : DateTime.Parse(options.DateStart).AddDays(options.Duration))
                         : DateTime.Parse(options.DateEnd);
            DateTime.SpecifyKind(dt, DateTimeKind.Local);

            var dateEnd = dt.ToUniversalTime();

            return dateEnd;
        }

        /// <summary>
        /// Gets the start date of the billing.
        /// </summary>
        /// <param name="monthsBack">Number of months back.</param>
        /// <returns>Returns the start date of the billing.</returns>
        public static DateTime GetStartDateOfMonth(int monthsBack)
        {
            var today = DateTime.Today;
            var dt = today.AddMonths(monthsBack * (-1));

            var dateStart = new DateTime(dt.Year, dt.Month, 1);
            DateTime.SpecifyKind(dateStart, DateTimeKind.Local);

            return dateStart.ToUniversalTime();
        }

        /// <summary>
        /// Gets the end date of the billing.
        /// </summary>
        /// <param name="monthsBack">Number of months back.</param>
        /// <returns>Returns the end date of the billing.</returns>
        public static DateTime GetEndDateOfMonth(int monthsBack)
        {
            var today = DateTime.Today;
            var dt = today.AddMonths(monthsBack * (-1) + 1);

            var dateEnd = new DateTime(dt.Year, dt.Month, 1);
            DateTime.SpecifyKind(dateEnd, DateTimeKind.Local);

            return dateEnd > today
                ? today.ToUniversalTime()
                : dateEnd.ToUniversalTime();
        }
    }
}

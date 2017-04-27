using System;

namespace CostMonitoring.Extensions
{
    /// <summary>
    /// This represents the extension class for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether the string value is either <c>null</c> or white space.
        /// </summary>
        /// <param name="value"><see cref="string"/> value to check.</param>
        /// <returns>Returns <c>True</c>, if the string value is either <c>null</c> or white space; otherwise returns <c>False</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Checks whether the string value is equal to the comparer, regardless of casing.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Comparing value.</param>
        /// <returns>Returns <c>True</c>, if the string value is equal to the comparer, regardless of casing; otherwise returns <c>False</c>.</returns>
        public static bool IsEquivalentTo(this string value, string comparer)
        {
            return value.Equals(comparer, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

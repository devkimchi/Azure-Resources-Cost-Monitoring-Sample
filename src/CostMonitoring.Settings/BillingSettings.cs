namespace CostMonitoring.Settings
{
    /// <summary>
    /// This represents the settings entity for Azure Billing metadata.
    /// </summary>
    public class BillingSettings
    {
        /// <summary>
        /// Gets or sets the offer Id.
        /// </summary>
        public int OfferId { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the region info.
        /// </summary>
        public string RegionInfo { get; set; }

        /// <summary>
        /// Gets or sets the default amount for total spend limit.
        /// </summary>
        public decimal TotalSpendLimit { get; set; }

        /// <summary>
        /// Gets or sets the default amount for daily spend limit.
        /// </summary>
        public decimal DailySpendLimit { get; set; }

        /// <summary>
        /// Gets or sets the default action for overspending.
        /// </summary>
        public string OverspendAction { get; set; }
    }
}
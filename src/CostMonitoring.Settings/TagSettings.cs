namespace CostMonitoring.Settings
{
    /// <summary>
    /// This represents the settings entity for Azure resource tag.
    /// </summary>
    public class TagSettings
    {
        /// <summary>
        /// Gets or sets the owner emails tag key.
        /// </summary>
        public string OwnerEmailsKey { get; set; }

        /// <summary>
        /// Gets or sets the total spend limit tag key.
        /// </summary>
        public string TotalSpendLimitKey { get; set; }

        /// <summary>
        /// Gets or sets the daily spend limit tag key.
        /// </summary>
        public string DailySpendLimitKey { get; set; }

        /// <summary>
        /// Gets or sets the overspending action tag key.
        /// </summary>
        public string OverspendActionKey { get; set; }
    }
}
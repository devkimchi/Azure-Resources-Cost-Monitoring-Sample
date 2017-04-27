using CommandLine;

namespace CostMonitoring.Models
{
    /// <summary>
    /// This represents the options entity for arguments.
    /// </summary>
    public class ArgumentOptions
    {
        /// <summary>
        /// Gets or sets the start date in <c>yyyy-MM-dd</c> format.
        /// </summary>
        [Option('f', "from", Required = false)]
        public string DateStart { get; set; }

        /// <summary>
        /// Gets or sets the end date in <c>yyyy-MM-dd</c> format.
        /// </summary>
        [Option('t', "to", Required = false)]
        public string DateEnd { get; set; }

        /// <summary>
        /// Gets or sets the date for retro.
        /// </summary>
        [Option('r', "retro", Required = false, DefaultValue = 2)]
        public int Retro { get; set; }

        /// <summary>
        /// Gets or sets the duration for retro.
        /// </summary>
        [Option('d', "duration", Required = false, DefaultValue = 2)]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to search the entire period or not.
        /// </summary>
        [Option('e', "entire", Required = false, DefaultValue = false)]
        public bool RunEntirePeriod { get; set; }

        /// <summary>
        /// Gets or sets the threshold value when an alert is sent.
        /// </summary>
        [Option('h', "threshold", Required = false, DefaultValue = 1.00)]
        public double Threshold { get; set; }
    }
}

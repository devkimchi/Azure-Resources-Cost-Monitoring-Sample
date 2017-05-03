using System;
using System.Collections.Generic;

namespace CostMonitoring.Settings
{
    /// <summary>
    /// This provides interfaces to the <see cref="CostMonitoringSettings"/> class.
    /// </summary>
    public interface ICostMonitoringSettings : IDisposable
    {
        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        ArgumentOptions Arguments { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="AuthenticationSettings"/> instance.
        /// </summary>
        AuthenticationSettings Authentication { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ResourceEndpointCollectionSettings"/> instance.
        /// </summary>
        ResourceEndpointCollectionSettings Resources { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="BillingSettings"/> instance.
        /// </summary>
        BillingSettings Billing { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TagSettings"/> instance.
        /// </summary>
        TagSettings Tags { get; set; }
    }
}
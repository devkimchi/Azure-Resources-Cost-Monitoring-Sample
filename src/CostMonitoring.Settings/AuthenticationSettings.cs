namespace CostMonitoring.Settings
{
    /// <summary>
    /// This represents the settings entity for authentication as a service principal.
    /// </summary>
    public class AuthenticationSettings
    {
        /// <summary>
        /// Gets or sets the Azure AD login endpoint URL.
        /// </summary>
        public string AadLoginUrl { get; set; }

        /// <summary>
        /// Gets or sets the tenant Id.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the application Id (client Id).
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the application secret key (client secret key).
        /// </summary>
        public string ApplicationSecret { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL (callback URL).
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}
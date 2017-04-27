using System;
using System.Diagnostics.CodeAnalysis;

using CodeHollow.AzureBillingApi;

using CostMonitoring.Settings;

namespace CostMonitoring.Wrappers
{
    /// <summary>
    /// This represents the wrapper entity for the <see cref="AzureAuthenticationHelper"/> class.
    /// </summary>
    public class AzureAuthenticationHelperWrapper : IAzureAuthenticationHelperWrapper
    {
        private bool _disposed;

        /// <summary>
        /// Authenticates to Azure AD and returns the OAuth token. If clientSecret is provided, authentication is done via app authentication. If clientSecret is not provided, authentication is done with user prompt.
        /// </summary>
        /// <param name="settings"><see cref="ICostMonitoringSettings"/> instance.</param>
        /// <returns>Returns the OAuth token.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="settings"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException">Authentication settings not found</exception>
        /// <exception cref="InvalidOperationException">Resources settings not found</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string GetOAuthTokenFromAad(ICostMonitoringSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var auth = settings.Authentication;
            if (auth == null)
            {
                throw new InvalidOperationException("Authentication settings not found");
            }

            var resources = settings.Resources;
            if (resources == null)
            {
                throw new InvalidOperationException("Resources settings not found");
            }

            return AzureAuthenticationHelper.GetOAuthTokenFromAAD(auth.AadLoginUrl, auth.TenantId, resources.BaseUrl, auth.RedirectUrl, auth.ApplicationId, auth.ApplicationSecret);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}

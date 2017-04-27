using System;
using System.Diagnostics.CodeAnalysis;

using CostMonitoring.Settings;

namespace CostMonitoring.Wrappers
{
    /// <summary>
    /// This provides interfaces to the <see cref="AzureAuthenticationHelperWrapper"/> class.
    /// </summary>
    public interface IAzureAuthenticationHelperWrapper : IDisposable
    {
        /// <summary>
        /// Authenticates to Azure AD and returns the OAuth token. If clientSecret is provided, authentication is done via app authentication. If clientSecret is not provided, authentication is done with user prompt.
        /// </summary>
        /// <param name="settings"><see cref="ICostMonitoringSettings"/> instance.</param>
        /// <returns>Returns the OAuth token.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="settings"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException">Authentication settings not found</exception>
        /// <exception cref="InvalidOperationException">Resources settings not found</exception>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        string GetOAuthTokenFromAad(ICostMonitoringSettings settings);
    }
}
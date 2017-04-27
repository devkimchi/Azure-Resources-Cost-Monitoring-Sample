using System.Diagnostics.CodeAnalysis;

using CostMonitoring.Extensions;

namespace CostMonitoring.Models
{
    /// <summary>
    /// This specifies Azure Offer Id.
    /// </summary>
    /// <remarks>This is based on https://azure.microsoft.com/en-us/support/legal/offer-details/. </remarks>
    public class AzureOfferId
    {
        /// <summary>Identifies the offer as Pay-As-You-Go.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId PayAsYouGo = new AzureOfferId(3);

        /// <summary>Identifies the offer as Enterprise Agreement Support.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId EnterpriseAgreementSupport = new AzureOfferId(17);

        /// <summary>Identifies the offer as Visual Studio Dev Essentials members.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId VisualStudioDevEssentialsMembers = new AzureOfferId(22);

        /// <summary>Identifies the offer as Pay-As-You-Go Dev/Test.</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId PayAsYouGoDevTest = new AzureOfferId(23);

        /// <summary>Identifies the offer as Action Pack.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId ActionPack = new AzureOfferId(25);

        /// <summary>Identifies the offer as Visual Studio Enterprise (MPN) subscribers.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId VisualStudioEnterpriseMpnSubscribers = new AzureOfferId(29);

        /// <summary>Identifies the offer as Microsoft Azure Sponsored Offer.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId MicrosoftAzureSponsoredOffer = new AzureOfferId(36);

        /// <summary>Identifies the offer as Support Plans 41.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId SupportPlans41 = new AzureOfferId(41);

        /// <summary>Identifies the offer as Support Plans 42.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId SupportPlans42 = new AzureOfferId(42);

        /// <summary>Identifies the offer as Support Plans 43.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId SupportPlans43 = new AzureOfferId(43);

        /// <summary>Identifies the offer as Free Trial.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId FreeTrial = new AzureOfferId(44);

        /// <summary>Identifies the offer as Azure Germany Free Trial.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzureGermanyFreeTrial = new AzureOfferId(44);

        /// <summary>Identifies the offer as Visual Studio Professional subscribers.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId VisualStudioProfessionalSubscribers = new AzureOfferId(59);

        /// <summary>Identifies the offer as Visual Studio Test Professional subscribers.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId VisualStudioTestProfessionalSubscribers = new AzureOfferId(60);

        /// <summary>Identifies the offer as MSDN Platforms subscribers.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId MsdnPlatformsSubscribers = new AzureOfferId(62);

        /// <summary>Identifies the offer as Visual Studio Enterprise subscribers.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId VisualStudioEnterpriseSubscribers = new AzureOfferId(63);

        /// <summary>Identifies the offer as Visual Studio Enterprise (BizSpark) subscribers.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId VisualStudioEnterpriseBizsparkSubscribers = new AzureOfferId(64);

        /// <summary>Identifies the offer as Azure in Open Licensing.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzureInOpenLicensing = new AzureOfferId(111);

        /// <summary>Identifies the offer as Azure Pass 120.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass120 = new AzureOfferId(120);

        /// <summary>Identifies the offer as Azure Pass 121.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass121 = new AzureOfferId(121);

        /// <summary>Identifies the offer as Azure Pass 122.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass122 = new AzureOfferId(122);

        /// <summary>Identifies the offer as Azure Pass 123.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass123 = new AzureOfferId(123);

        /// <summary>Identifies the offer as Azure Pass 124.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass124 = new AzureOfferId(124);

        /// <summary>Identifies the offer as Azure Pass 125.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass125 = new AzureOfferId(125);

        /// <summary>Identifies the offer as Azure Pass 126.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass126 = new AzureOfferId(126);

        /// <summary>Identifies the offer as Azure Pass 127.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass127 = new AzureOfferId(127);

        /// <summary>Identifies the offer as Azure Pass 128.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass128 = new AzureOfferId(128);

        /// <summary>Identifies the offer as Azure Pass 129.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass129 = new AzureOfferId(129);

        /// <summary>Identifies the offer as Azure Pass 130.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzurePass130 = new AzureOfferId(130);

        /// <summary>Identifies the offer as Microsoft Imagine.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId MicrosoftImagine = new AzureOfferId(144);

        /// <summary>Identifies the offer as Azure in CSP.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzureInCsp = new AzureOfferId(145);

        /// <summary>Identifies the offer as Enterprise Dev/Test.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId EnterpriseDevTest = new AzureOfferId(148);

        /// <summary>Identifies the offer as BizSpark Plus.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId BizsparkPlus = new AzureOfferId(149);

        /// <summary>Identifies the offer as Azure Germany Pay-As-You-Go.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzureGermanyPayAsYouGo = new AzureOfferId(3, "DE");

        /// <summary>Identifies the offer as Azure Germany Support Plans 41.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzureGermanySupportPlans41 = new AzureOfferId(41, "DE");

        /// <summary>Identifies the offer as Azure Germany Support Plans 42.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzureGermanySupportPlans42 = new AzureOfferId(42, "DE");

        /// <summary>Identifies the offer as Azure Germany Support Plans 43.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzureGermanySupportPlans43 = new AzureOfferId(43, "DE");

        /// <summary>Identifies the offer as Azure Germany in CSP for Microsoft Cloud Germany.</summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static AzureOfferId AzureGermanyInCspForMicrosoftCloudGermany = new AzureOfferId(145, "DE");

        private string _locale;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureOfferId"/> class.
        /// </summary>
        /// <param name="key">Offer key value.</param>
        /// <param name="locale">Two-letter local value. This is particularly for German offer.</param>
        public AzureOfferId(int key, string locale = null)
        {
            this.Key = key;

            this._locale = locale;
        }

        /// <summary>
        /// Gets the Key value.
        /// </summary>
        public int Key { get; }

        /// <summary>
        /// Gets the Azure Offer Id.
        /// </summary>
        public string Value => this.ToString();

        /// <summary>
        /// Converts <see cref="AzureOfferId"/> instance to <see cref="string"/> value.
        /// </summary>
        /// <param name="value"><see cref="AzureOfferId"/> instance.</param>
        public static implicit operator string(AzureOfferId value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Converts <see cref="AzureOfferId"/> instance to <see cref="string"/> value.
        /// </summary>
        /// <returns>Returns the string value converted.</returns>
        public override string ToString()
        {
            var id = "MS-AZR-";
            if (!this._locale.IsNullOrWhiteSpace())
            {
                id += $"{this._locale.ToUpperInvariant()}-";
            }

            id += $"{this.Key:0000}P";

            return id;
        }
    }
}

using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// The platform for a client application
    /// </summary>
    /// <seealso cref="Microsoft.SfB.PlatformService.SDK.ClientModel.IClientPlatform" />
    public class ClientPlatform : IClientPlatform
    {
        #region Private fields

        private readonly ClientPlatformSettings m_platformSettings;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the discover URI.
        /// </summary>
        /// <value>The discover URI.</value>
        public Uri DiscoverUri
        {
            get {
                if (m_platformSettings.DiscoverUri != null)
                {
                    return m_platformSettings.DiscoverUri;
                }
                else if (this.IsSandBoxEnv)
                {
                    return Constants.PlatformDiscoverUri_SandBox;
                }
                else
                {
                    return Constants.PlatformDiscoverUri_Prod;
                }
            }
        }

        /// <summary>
        /// Gets the aad client Id.
        /// </summary>
        /// <value>The aad client Id.</value>
        public Guid AADClientId
        {
            get { return m_platformSettings.AADClientId; }
        }

        /// <summary>
        /// Gets the aad client secret.
        /// </summary>
        /// <value>The aad client secret.</value>
        public string AADClientSecret
        {
            get { return m_platformSettings.AADClientSecret; }
        }

        internal bool IsSandBoxEnv
        {
            get { return m_platformSettings.IsSandBoxEnv; }
        }

        /// <summary>
        /// Callback url where events related to a conversation will be delivered by SfB
        /// </summary>
        internal string CustomizedCallbackUrl
        {
            get { return m_platformSettings.CustomizedCallbackUrl; }
        }

        internal bool IsInternalPartner
        {
            get { return m_platformSettings.IsInternalPartner; }
        }

        /// <summary>
        /// Gets the aad application certificate.
        /// </summary>
        /// <value>The aad application certificate.</value>
        public X509Certificate2 AADAppCertificate { get; }

        #endregion

        #region Internal properties

        internal IRestfulClientFactory RestfulClientFactory { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientPlatform"/> class.
        /// </summary>
        /// <param name="platformSettings">The platform settings.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">
        /// platformSettings
        /// or
        /// logger
        /// </exception>
        /// <exception cref="System.ArgumentException"></exception>
        public ClientPlatform(ClientPlatformSettings platformSettings, IPlatformServiceLogger logger)
        {
            if(platformSettings == null)
            {
                throw new ArgumentNullException(nameof(platformSettings));
            }

            if(logger == null)
            {
                 throw new ArgumentNullException(nameof(logger));
            }

            m_platformSettings = platformSettings;
            Logger.RegisterLogger(logger);
            if (!string.IsNullOrEmpty(platformSettings.AppTokenCertThumbprint))
            {
                AADAppCertificate =
                    CertificateHelper.LookupCertificate(X509FindType.FindByThumbprint, platformSettings.AppTokenCertThumbprint, StoreName.My, StoreLocation.LocalMachine)
                    ?? CertificateHelper.LookupCertificate(X509FindType.FindByThumbprint, platformSettings.AppTokenCertThumbprint, StoreName.My, StoreLocation.CurrentUser);

                if (AADAppCertificate == null)
                {
                    throw new ArgumentException($"Certificate with thumbprint {platformSettings.AppTokenCertThumbprint} not found in store");
                }
            }
            RestfulClientFactory = new RestfulClientFactory();
        }

        #endregion
    }
}

// We put all not official supported features (workarounds to help developers) in this namespace
namespace Microsoft.SfB.PlatformService.SDK.ClientModel.Internal
{
    /// <summary>
    /// Internal extensions for <see cref="ClientPlatform"/>
    /// </summary>
    public static class ClientPlatformExtensions
    {
        /// <summary>
        /// Gets the customized callback URL.
        /// </summary>
        /// <param name="This"> this.</param>
        /// <returns>System.String.</returns>
        public static string GetCustomizedCallbackUrl(this ClientPlatform This)
        {
            return This.CustomizedCallbackUrl;
        }

        /// <summary>
        /// Gets if this Client platform is a sandbox environment.
        /// </summary>
        /// <param name="This">This.</param>
        /// <returns><c>true</c> if this Client platform is a sandbox environment, <c>false</c> otherwise.</returns>
        public static bool GetIsSandboxEnv(this ClientPlatform This)
        {
            return This.IsSandBoxEnv;
        }

        /// <summary>
        /// Gets if this client platform is an internal partner
        /// </summary>
        /// <param name="This">The this.</param>
        /// <returns><c>true</c> if this client platform is an internal partner, <c>false</c> otherwise.</returns>
        public static bool GetIsInternalPartner(this ClientPlatform This)
        {
            return This.IsInternalPartner;
        }
    }
}
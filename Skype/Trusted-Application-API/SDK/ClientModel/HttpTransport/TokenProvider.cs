using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// The interface of Token Provider.
    /// </summary>
    internal interface ITokenProvider
    {
        /// <summary>
        /// Get Token from provider.
        /// </summary>
        /// <param name="oauthIdentity">The oauth identity.</param>
        /// <returns>A string format of token.</returns>
        Task<string> GetTokenAsync(OAuthTokenIdentifier oauthIdentity);
    }

    /// <summary>
    /// Token provider for AAD
    /// </summary>
    internal class AADServiceTokenProvider : ITokenProvider
    {
        private readonly string aadClientId;
        private readonly string aadAuthority;
        private readonly string aadClientSecret;
        private readonly X509Certificate2 aadAppCert;

        /// <summary>
        /// Initializes a new instance of the <see cref="AADServiceTokenProvider"/> class.
        /// </summary>
        /// <param name="aadClientId">The client id which is registered in AAD.</param>
        /// <param name="aadAuthority">The authority uri which is registered in AAD.</param>
        /// <param name="aadAppCert">The app certificate which is registered in AAD.</param>
        /// <param name="aadclientsecret">The app secret which can be retrieved from AAD.</param>
        public AADServiceTokenProvider(string aadClientId, string aadAuthority,  X509Certificate2 aadAppCert, string aadclientsecret=null)
        {
            if (string.IsNullOrWhiteSpace(aadClientId))
            {
                throw new ArgumentNullException(nameof(aadClientId), "The parameter named aadClientId can't be null, empty and white space.");
            }

            // TODO : This should be a Uri
            if (string.IsNullOrWhiteSpace(aadAuthority))
            {
                throw new ArgumentNullException(nameof(aadAuthority), "The parameter named aadAuthority can't be null, empty and white space.");
            }

            if (aadAppCert == null && string.IsNullOrWhiteSpace(aadclientsecret))
            {
                throw new ArgumentNullException(nameof(aadAppCert), "The parameters named aadAppCert amd client secret can't be null at the same time.");
            }

            this.aadClientId = aadClientId;
            this.aadAuthority = aadAuthority;
            this.aadAppCert = aadAppCert;
            this.aadClientSecret = aadclientsecret;
        }

        /// <summary>
        /// Get app token from AAD.
        /// </summary>
        /// <param name="oauthIdentity">The oauth identity.</param>
        /// <returns>The Azure AAD App token.</returns>
        public async Task<string> GetTokenAsync(OAuthTokenIdentifier oauthIdentity)
        {
            var authenticationContext = new AuthenticationContext((new Uri(new Uri(aadAuthority), oauthIdentity.TenantDomain)).ToString(), false);

            // The ADAL will cache the token and auto refresh when it is expired, we don't need to call AcquireTokenByRefreshToken method and in ADAL 3, the AcquireTokenByRefreshToken method will be deleted.
            // The token cache use ConcurrentDictionary, it is thread safe.
            AuthenticationResult authenticateResult;

            if (string.IsNullOrEmpty(this.aadClientSecret))//using cert if secret is empty or null
            {
                var clientCred = new ClientAssertionCertificate(this.aadClientId, this.aadAppCert);
                authenticateResult = await authenticationContext.AcquireTokenAsync(oauthIdentity.Audience, clientCred).ConfigureAwait(false);
            }
            else
            {
                var clientCredSecret = new ClientCredential(this.aadClientId, this.aadClientSecret);
                authenticateResult = await authenticationContext.AcquireTokenAsync(oauthIdentity.Audience, clientCredSecret).ConfigureAwait(false);
            }
            return authenticateResult.CreateAuthorizationHeader();
        }
    }

    #region OAuthTokenIdentifier

    /// <summary>
    /// OAuthTokenIdentifier
    /// </summary>
    internal class OAuthTokenIdentifier
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthTokenIdentifier"/> class.
        /// </summary>
        /// <param name="audience">The audience.</param>
        /// <param name="tenantDomain">The tenant identifier.</param>
        public OAuthTokenIdentifier(string audience, string tenantDomain)
        {
            if(string.IsNullOrEmpty(audience))
            {
                throw new ArgumentException("audience cannot be null or empty");
            }

            if (string.IsNullOrEmpty(tenantDomain))
            {
                throw new ArgumentException("tenantId cannot be null or empty");
            }

            Audience = audience;
            TenantDomain = tenantDomain;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the audience.
        /// </summary>
        /// <value>
        /// The audience.
        /// </value>
        public string Audience { get; }

        /// <summary>
        /// Gets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public string TenantDomain { get; }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            bool result = false;
            OAuthTokenIdentifier identifier = obj as OAuthTokenIdentifier;
            if (identifier != null)
            {
                if (identifier.Audience.Equals(Audience, StringComparison.OrdinalIgnoreCase)
                    && identifier.TenantDomain.Equals(TenantDomain, StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Audience.ToUpperInvariant().GetHashCode() ^ TenantDomain.ToUpperInvariant().GetHashCode();
        }

        #endregion
    }
    #endregion

}

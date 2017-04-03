using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    #pragma warning disable CS0618 // Type or member is obsolete
    internal class Applications : BasePlatformResource<ApplicationsResource, ApplicationsCapability>, IApplications
    #pragma warning restore CS0618 // Type or member is obsolete
    {
        #region Private fields

        /// <summary>
        /// Appplication
        /// </summary>
        private Application m_application;

        #endregion

        #region Public properties

        /// <summary>
        /// Get Communication
        /// </summary>
        public IApplication Application
        {
            get { return m_application; }
        }

        #endregion

        #region Constructor

        internal Applications(IRestfulClient restfulClient, ApplicationsResource resource, Uri baseUri, Uri resourceUri, object parent)
                : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Call Get on applications and initialize application resource
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <returns></returns>
        public async Task RefreshAndInitializeAsync(LoggingContext loggingContext = null)
        {
            await this.RefreshAsync(loggingContext).ConfigureAwait(false);
            if (this.PlatformResource.Application != null)
            {
                m_application = new Application(this.RestfulClient, null, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, this.PlatformResource.Application.Href), this);
            }
            else
            {
                throw new RemotePlatformServiceException("Not get application resource from applications");
            }
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(ApplicationsCapability capability)
        {
            return false;
        }
    }

    #endregion
}

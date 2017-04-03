using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class Discover : BasePlatformResource<DiscoverResource, DiscoverCapability>, IDiscover
    {
        #region Public properties

        /// <summary>
        /// Get Applications
        /// </summary>
        [Obsolete("Please use Application property instead")]
        public IApplications Applications { get; private set; }

        /// <summary>
        /// Get Application
        /// </summary>
        public IApplication Application
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            get { return Applications?.Application; }
            #pragma warning restore CS0618 // Type or member is obsolete
        }

        #endregion

        #region Constructor

        internal Discover(IRestfulClient restfulClient, Uri baseUri, Uri resourceUri, object parent)
                : base(restfulClient, null, baseUri, resourceUri, parent)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Call Get on application and initialize communication resource
        /// </summary>
        /// <param name="endpointId">The application endpoint's sip id</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        public async Task RefreshAndInitializeAsync(string endpointId, LoggingContext loggingContext = null)
        {
            await this.RefreshAsync(loggingContext).ConfigureAwait(false);
            if (this.PlatformResource.Applications != null)
            {
                Uri baseUri = UriHelper.GetBaseUriFromAbsoluteUri(this.PlatformResource.Applications.Href);
                Uri applicationsUri = new Uri(this.PlatformResource.Applications.Href);
                if (!string.IsNullOrEmpty(endpointId))
                {
                    applicationsUri = UriHelper.AppendQueryParameterOnUrl(applicationsUri.ToString(), Constants.EndpointId, endpointId, false);
                }

                #pragma warning disable CS0618 // Type or member is obsolete
                Applications = new Applications(RestfulClient, null, baseUri, applicationsUri, this);
                await Applications.RefreshAndInitializeAsync(loggingContext).ConfigureAwait(false);
                #pragma warning restore CS0618 // Type or member is obsolete
            }
            else
            {
                throw new RemotePlatformServiceException("Retrieved DiscoverResource doesn't have link to ApplicationsResource.");
            }
        }

        /// <summary>
        /// Call Get on application and initialize communication resource
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="endpointId">The application endpoint's sip id</param>
        [Obsolete("Please use the other variation")]
        public Task RefreshAndInitializeAsync(LoggingContext loggingContext, string endpointId)
        {
            return RefreshAndInitializeAsync(endpointId, loggingContext);
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(DiscoverCapability capability)
        {
            return false;
        }

        #endregion
    }
}

using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;
using ResourceModel = Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// The base class for invitations
    /// </summary>
    /// <typeparam name="TPlatformResource">The type which inherit from InvitationResource.</typeparam>
    /// <typeparam name="TCapabilities">
    /// An enum listing all the capabilties that this <see cref="IPlatformResource{TCapabilities}"/> supports. Capabilities
    /// might not be available at runtime, such cases can be handled by invoking
    /// <see cref="IPlatformResource{TCapabilities}.Supports(TCapabilities)"/> when a capabilty needs to be used.
    /// </typeparam>
    internal abstract class Invitation<TPlatformResource, TCapabilities> : BasePlatformResource<TPlatformResource, TCapabilities>, IInvitation, IInvitationWithConversation
        where TPlatformResource : InvitationResource
    {
        #region Private fields

        /// <summary>
        /// complete tcs
        /// </summary>
        private readonly TaskCompletionSource<IConversation> m_invitationCompleteTcs;

        #endregion

        #region Public properties

        /// <summary>
        /// Get or set Related conversation
        /// </summary>
        public IConversation RelatedConversation { get; private set; }

        /// <summary>
        /// Gets the application resource.
        /// </summary>
        /// <value>The application resource.</value>
        public ApplicationResource ApplicationResource
        {
            get { return PlatformResource?.Application; }
        }

        #endregion

        #region Constructor

        internal Invitation(IRestfulClient restfulClient, TPlatformResource resource, Uri baseUri, Uri resourceUri, Communication parent)
            :base (restfulClient, resource, baseUri, resourceUri, parent)
        {
            m_invitationCompleteTcs = new TaskCompletionSource<IConversation>();
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "The paramater named parent can't be null.");
            }
        }

        /// <summary>
        /// Sets the related conversation.
        /// </summary>
        /// <param name="conversation">The conversation.</param>
        public void SetRelatedConversation(Conversation conversation)
        {
            this.RelatedConversation = conversation;
        }

        #endregion

        #region Internal methods

        internal override void HandleResourceEvent(EventContext eventcontext)
        {
            TPlatformResource resource = this.ConvertToPlatformServiceResource<TPlatformResource>(eventcontext);
            if (resource != null)
            {
                if (eventcontext.EventEntity.Relationship == ResourceModel.EventOperation.Completed)
                {
                    if (resource.State == InvitationState.Failed)
                    {
                        ResourceModel.ErrorInformation error = eventcontext.EventEntity.Error;
                        ErrorInformation errorInfo = error == null ? null : new ErrorInformation(error);
                        string errorMessage = errorInfo?.ToString();
                        m_invitationCompleteTcs.TrySetException(new RemotePlatformServiceException("Invitation failed " + errorMessage, errorInfo));
                    }
                    else if (resource.State == InvitationState.Connected)
                    {
                        m_invitationCompleteTcs.TrySetResult(this.RelatedConversation);
                    }
                }
                else if (eventcontext.EventEntity.Relationship == ResourceModel.EventOperation.Started)
                {
                    var communication = this.Parent as Communication;
                    communication.HandleInviteStarted(resource.OperationContext, this);
                }

                base.HandleResourceEvent(eventcontext);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Wait for invite complete
        /// </summary>
        /// <returns></returns>
        public Task<IConversation> WaitForInviteCompleteAsync()
        {
            return m_invitationCompleteTcs.Task;
        }

        #endregion

    }
}

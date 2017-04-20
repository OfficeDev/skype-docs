using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using System;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents the transfer of a <see cref="AudioVideoCall"/>>
    /// </summary>
    /// <seealso cref="BasePlatformResource{TPlatformResource, TCapabilities}"/>
    /// <seealso cref="ITransfer" />
    internal class Transfer : BasePlatformResource<TransferResource, TransferCapability>, ITransfer
    {
        #region Private fields

        /// <summary>
        /// complete tcs
        /// </summary>
        private readonly TaskCompletionSource<string> m_transferCompleteTcs;

        #endregion

        #region Constructor

        internal Transfer(IRestfulClient restfulClient, TransferResource resource, Uri baseUri, Uri resourceUri, AudioVideoCall parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "AudioVideo is required");
            }
            m_transferCompleteTcs = new TaskCompletionSource<string>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Wait for invite complete
        /// </summary>
        /// <returns></returns>
        public Task WaitForTransferCompleteAsync()
        {
            return m_transferCompleteTcs.Task;
        }

        /// <summary>
        /// <see cref="AnonymousApplicationToken"/> doesn't support any capability so always returns <code>false</code>.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked</param>
        /// <returns><code>false</code> </returns>
        public override bool Supports(TransferCapability capability)
        {
            return false;
        }

        #endregion

        #region Internal methods

        internal override void HandleResourceEvent(EventContext eventcontext)
        {
            TransferResource resource = this.ConvertToPlatformServiceResource<TransferResource>(eventcontext);
            if (resource != null)
            {
                if (eventcontext.EventEntity.Relationship == EventOperation.Completed)
                {
                    if (eventcontext.EventEntity.Status == EventStatus.Failure)
                    {
                        string error = eventcontext.EventEntity.Error == null ? null : eventcontext.EventEntity.Error.GetErrorInformationString();
                        m_transferCompleteTcs.TrySetException(new RemotePlatformServiceException("Invitation failed " + error));
                    }
                    else
                    {
                        m_transferCompleteTcs.TrySetResult(string.Empty);
                    }
                }
                else if (eventcontext.EventEntity.Relationship == EventOperation.Started)
                {
                    var audioVideoCall = this.Parent as AudioVideoCall;
                    audioVideoCall.HandleTransferStarted(resource.OperationId, this);
                }

                base.HandleResourceEvent(eventcontext);
            }
        }

        #endregion
    }
}

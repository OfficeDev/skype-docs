using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using System.Collections.Concurrent;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents AudioVideo call inside a <see cref="Conversation"/>.
    /// </summary>
    internal class AudioVideoCall : Call<AudioVideoResource, IAudioVideoInvitation, AudioVideoCallCapability>, IAudioVideoCall
    {
        #region Private fields

        /// <summary>
        /// <see cref="Transfer"/> transfers which are currently in progress.
        /// </summary>
        private readonly ConcurrentDictionary<string, Transfer> m_transfers;

        /// <summary>
        /// <see cref="TaskCompletionSource{TResult}"/> used to signal completion of a <see cref="Transfer"/> operation.
        /// </summary>
        private readonly ConcurrentDictionary<string, TaskCompletionSource<Transfer>> m_transferAddedTcses;

        private EventHandler<AudioVideoFlowUpdatedEventArgs> m_audioVideoFlowConnected;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates instances of <see cref="AudioVideoCall"/>.
        /// </summary>
        /// <param name="restfulClient"><see cref="IRestfulClient"/> to use to make outbound REST HTTP requests.</param>
        /// <param name="resource"><see cref="AudioVideoResource"/> corresponding to this object.</param>
        /// <param name="baseUri"><see cref="Uri"/> of the service.</param>
        /// <param name="resourceUri"><see cref="Uri"/> of <paramref name="resource"/> relative to <paramref name="baseUri"/>.</param>
        /// <param name="parent"><see cref="Conversation"/> of which this modality is part of.</param>
        internal AudioVideoCall(IRestfulClient restfulClient, AudioVideoResource resource, Uri baseUri, Uri resourceUri, Conversation parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Conversation object is required");
            }

            // Track call transfers
            m_transfers = new ConcurrentDictionary<string, Transfer>();
            m_transferAddedTcses = new ConcurrentDictionary<string, TaskCompletionSource<Transfer>>();
        }

        #endregion

        #region Public events

        /// <summary>
        /// Event which will be raised when the corresponding <see cref="ClientModel.AudioVideoFlow"/> goes to <see cref="FlowState.Connected"/> state.
        /// </summary>
        public event EventHandler<AudioVideoFlowUpdatedEventArgs> AudioVideoFlowConnected
        {
            add
            {
                if (m_audioVideoFlowConnected == null || !m_audioVideoFlowConnected.GetInvocationList().Contains(value))
                {
                    m_audioVideoFlowConnected += value;
                }
            }
            remove { m_audioVideoFlowConnected -= value; }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the context for this call. It can be used to refer to this call when performing operations.
        /// </summary>
        public string CallContext
        {
            get { return PlatformResource?.CallContext; }
        }

        /// <summary>
        /// Gets the audio video flow.
        /// </summary>
        /// <value>The audio video flow.</value>
        public IAudioVideoFlow AudioVideoFlow { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Transfers the audio video call to a user
        /// </summary>
        /// <param name="transferTarget">SIP uri of the user where the call needs to be transferred to</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="ITransfer"/> which can be used to track the transfer operation</returns>
        public Task<ITransfer> TransferAsync(SipUri transferTarget, LoggingContext loggingContext = null)
        {
            if(transferTarget == null)
            {
                throw new ArgumentNullException(nameof(transferTarget));
            }

            #pragma warning disable CS0618 // Type or member is obsolete
            return TransferAsync(transferTarget, null, loggingContext);
            #pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Transfers the audio video call by replacing an existing audio video call
        /// </summary>
        /// <param name="replacesCallContext"><see cref="CallContext"/> of the <see cref="IAudioVideoCall"/> which you are trying to transfer to</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="ITransfer"/> which can be used to track the transfer operation</returns>
        public Task<ITransfer> TransferAsync(string replacesCallContext, LoggingContext loggingContext = null)
        {
            if (replacesCallContext == null)
            {
                throw new ArgumentNullException(nameof(replacesCallContext));
            }

            #pragma warning disable CS0618 // Type or member is obsolete
            return TransferAsync((SipUri)null, replacesCallContext, loggingContext);
            #pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Transfers the audio video call.
        /// </summary>
        /// <param name="transferTarget">SIP uri of the user where the call needs to be transferred to</param>
        /// <param name="replacesCallContext">
        /// <see cref="P:Microsoft.SfB.PlatformService.SDK.ClientModel.IAudioVideoCall.CallContext" /> of the
        /// <see cref="IAudioVideoCall" /> which you are trying to replace
        /// </param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used to log all related events</param>
        /// <returns><see cref="ITransfer" /> which can be used to track the transfer operation</returns>
        /// <remarks>only one of <paramref name="transferTarget" /> or <paramref name="replacesCallContext" /> is supported at a time</remarks>
        /// <exception cref="CapabilityNotAvailableException">Link to start transfer of AudioVideo is not available.</exception>
        /// <exception cref="RemotePlatformServiceException">Timeout to get incoming transfer started event from platformservice!</exception>
        [Obsolete("Please use any of the other variations")]
        public async Task<ITransfer> TransferAsync(SipUri transferTarget, string replacesCallContext, LoggingContext loggingContext = null)
        {
            // TODO : Make this method private and non obsolete when releasing 1.0.0
            string href = PlatformResource?.StartTransferLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to start transfer of AudioVideo is not available.");
            }

            Uri transferLink = UriHelper.CreateAbsoluteUri(BaseUri, href);

            var operationId = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<Transfer>();

            this.HandleNewTransferOperationKickedOff(operationId, tcs);
            var input = new TransferOperationInput() { To = transferTarget?.ToString(), ReplacesCallContext = replacesCallContext, OperationId = operationId };
            await PostRelatedPlatformResourceAsync(transferLink, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            Transfer result = null;
            try
            {
                result = await tcs.Task.TimeoutAfterAsync(WaitForEvents).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new RemotePlatformServiceException("Timeout to get incoming transfer started event from platformservice!");
            }

            return result;
        }

        [Obsolete("Please use any of the other variations")]
        public Task<ITransfer> TransferAsync(string transferTarget, string replacesCallContext, LoggingContext loggingContext = null)
        {
            return TransferAsync(new SipUri(transferTarget), replacesCallContext, loggingContext);
        }

        /// <summary>
        /// Terminates as an asynchronous operation.
        /// </summary>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>Task.</returns>
        /// <exception cref="CapabilityNotAvailableException">Link to terminate AudioVideo.</exception>
        public override Task TerminateAsync(LoggingContext loggingContext = null)

        {
            string href = PlatformResource?.StopAudioVideoLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to terminate AudioVideo.");
            }

            Uri terminate = UriHelper.CreateAbsoluteUri(BaseUri, href);

            string input = string.Empty;
            return PostRelatedPlatformResourceAsync(terminate, input, new ResourceJsonMediaTypeFormatter(), loggingContext);
        }

        /// <summary>
        /// Add AudioVideo to an already estalished conversation.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to use for logging</param>
        /// <returns><see cref="AudioVideoInvitation"/> which tracks the outgoing invite.</returns>
        public override async Task<IAudioVideoInvitation> EstablishAsync(LoggingContext loggingContext = null)
        {
            string href = PlatformResource?.AddAudioVideoLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to establish AudioVideo is not available.");
            }

            Logger.Instance.Information("[AudioVideo] Calling AddAudioVideo. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString());

            var conversation = base.Parent as Conversation;
            if (conversation == null)
            {
                Logger.Instance.Error("[AudioVideo] Conversation from AudioVideo base parent is null");
                throw new Exception("[AudioVideo] Failed to get Conversation from AudioVideo base parent");
            }
            var communication = conversation.Parent as Communication;
            if (communication == null)
            {
                Logger.Instance.Error("[AudioVideo] Communication from conversation base parent is null");
                throw new Exception("[AudioVideo] Failed to get communication from conversation base parent");
            }

            string operationId = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<IInvitation>();
            //Tracking the incoming invitation from communication resource
            communication.HandleNewInviteOperationKickedOff(operationId, tcs);

            IInvitation invite = null;
            var input = new AudioVideoInvitationInput
            {
                OperationContext = operationId,
                MediaHost = MediaHostType.Remote
            };

            var addAudioVideoUri = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            await this.PostRelatedPlatformResourceAsync(addAudioVideoUri, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            try
            {
                invite = await tcs.Task.TimeoutAfterAsync(WaitForEvents).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new RemotePlatformServiceException("Timeout to get incoming AudioVideo invitation started event from platformservice!");
            }

            //We are sure the invite sure be there now.
            var result = invite as AudioVideoInvitation;
            if (result == null)
            {
                throw new RemotePlatformServiceException("Platformservice do not deliver a AudioVideoInvitation resource with operationId " + operationId);
            }

            return result;
        }

        /// <summary>
        /// Waits for <see cref="AudioVideoFlow"/> to be connected.
        /// </summary>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <returns>Task&lt;IAudioVideoFlow&gt;.</returns>
        public Task<IAudioVideoFlow> WaitForAVFlowConnected(int timeoutInSeconds = 30)
        {
            IAudioVideoFlow flow = AudioVideoFlow;
            TaskCompletionSource<IAudioVideoFlow> s = new TaskCompletionSource<IAudioVideoFlow>();
            AudioVideoFlowConnected += (o, p) => s.TrySetResult(AudioVideoFlow);

            if (flow?.State == FlowState.Connected)
            {
                s.TrySetResult(AudioVideoFlow);
            }
            return s.Task.TimeoutAfterAsync(TimeSpan.FromSeconds(timeoutInSeconds));
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(AudioVideoCallCapability capability)
        {
            string href = null;
            switch (capability)
            {
                case AudioVideoCallCapability.Transfer:
                    {
                        href = PlatformResource?.StartTransferLink?.Href;
                        break;
                    }
                case AudioVideoCallCapability.Terminate:
                    {
                        href = PlatformResource?.StopAudioVideoLink?.Href;
                        break;
                    }
                case AudioVideoCallCapability.Establish:
                    {
                        href = PlatformResource?.AddAudioVideoLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        #endregion

        #region Internal methods

        internal override bool ProcessAndDispatchEventsToChild(EventContext eventContext)
        {
            bool processed = false;

            if (eventContext.EventEntity.Link.Token == TokenMapper.GetTokenName(typeof(AudioVideoFlowResource)))
            {
                if (eventContext.EventEntity.Relationship == EventOperation.Added)
                {
                    var audioVideoFlowResource = ConvertToPlatformServiceResource<AudioVideoFlowResource>(eventContext);
                    AudioVideoFlow = new AudioVideoFlow(RestfulClient, audioVideoFlowResource, BaseUri, UriHelper.CreateAbsoluteUri(BaseUri, audioVideoFlowResource.SelfUri), this);

                    // Raise event when flow state changes to connected
                    AudioVideoFlow.HandleResourceUpdated += RaiseAudioVideoFlowConnectedEventIfConnected;

                    // Raise event if the flow is already connected
                    RaiseAudioVideoFlowConnectedEventIfConnected(null, null);
                }

                ((AudioVideoFlow)AudioVideoFlow).HandleResourceEvent(eventContext);

                if (eventContext.EventEntity.Relationship == EventOperation.Deleted)
                {
                    AudioVideoFlow = null;
                }

                processed = true;
            }
            else if (eventContext.EventEntity.Link.Token == TokenMapper.GetTokenName(typeof(TransferResource)))
            {
                this.HandleTransferEvent(eventContext);
                processed = true;
            }

            var flow = AudioVideoFlow;
            if (!processed && flow != null)
            {
                processed = ((AudioVideoFlow)flow).ProcessAndDispatchEventsToChild(eventContext);
            }

            //add any new child resource under audioVideo processing here
            return processed;
        }

        /// <summary>
        /// Handle a transfer started event
        /// </summary>
        /// <param name="operationId">ID of the transfer operation</param>
        /// <param name="transfer">The <see cref="Transfer"/> object representing the transfer operation</param>
        internal void HandleTransferStarted(string operationId, Transfer transfer)
        {
            TaskCompletionSource<Transfer> tcs = null;
            m_transferAddedTcses.TryGetValue(operationId, out tcs);
            if (tcs != null)
            {
                tcs.TrySetResult(transfer);
                TaskCompletionSource<Transfer> removeTemp = null;
                m_transferAddedTcses.TryRemove(operationId, out removeTemp);
            }
        }

        /// <summary>
        /// Tracking the transfer resources
        /// </summary>
        /// <param name="operationid"></param>
        /// <param name="tcs"></param>
        internal void HandleNewTransferOperationKickedOff(string operationid, TaskCompletionSource<Transfer> tcs)
        {
            if (string.IsNullOrEmpty(operationid) || tcs == null)
            {
                throw new RemotePlatformServiceException("Faied to add null object into m_transferAddedTcses which is to track the new transfer invite.");
            }

            m_transferAddedTcses.TryAdd(operationid, tcs);
        }

        #endregion

        #region Private methods

        private void RaiseAudioVideoFlowConnectedEventIfConnected(object sender, PlatformResourceEventArgs e)
        {
            if(AudioVideoFlow.State == FlowState.Connected)
            {
                var eventArgs = new AudioVideoFlowUpdatedEventArgs()
                {
                    AudioVideoFlow = AudioVideoFlow
                };

                m_audioVideoFlowConnected?.Invoke(this, eventArgs);
            }
        }

        /// <summary>
        /// Handle transfer event
        /// </summary>
        /// <param name="eventcontext"></param>
        private void HandleTransferEvent(EventContext eventcontext)
        {
            string normalizedUri = UriHelper.NormalizeUriWithNoQueryParameters(eventcontext.EventEntity.Link.Href, eventcontext.BaseUri);
            TransferResource localResource = this.ConvertToPlatformServiceResource<TransferResource>(eventcontext);

            Transfer transfer = m_transfers.GetOrAdd(localResource.OperationId, (a) =>
            {
                Logger.Instance.Information(string.Format("[AudioVideo] Started and Add Transfer: OperationContext:{0}, Href: {1} , LoggingContext: {2}",
                    localResource.OperationId, normalizedUri, eventcontext.LoggingContext == null ? string.Empty : eventcontext.LoggingContext.ToString()));

                return new Transfer(this.RestfulClient, localResource, eventcontext.BaseUri, eventcontext.EventFullHref, this);
            });

            //Remove from cache if it is a complete operation
            if (eventcontext.EventEntity.Relationship == EventOperation.Completed)
            {
                Transfer completedTransfer = null;
                Logger.Instance.Information(string.Format("[AudioVideo] Completed and remove transfer: OperationContext:{0}, Href: {1} , LoggingContext: {2}",
                      localResource.OperationId, normalizedUri, eventcontext.LoggingContext == null ? string.Empty : eventcontext.LoggingContext.ToString()));
                m_transfers.TryRemove(localResource.OperationId, out completedTransfer);
            }

            var eventableEntity = transfer as EventableEntity;
            eventableEntity.HandleResourceEvent(eventcontext);
        }

        #endregion
    }
}

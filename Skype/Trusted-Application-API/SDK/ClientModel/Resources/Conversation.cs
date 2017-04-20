using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represent the conversation resource
    /// </summary>
    internal class Conversation : BasePlatformResource<ConversationResource, ConversationCapability>, IConversation
    {
        #region Private fields

        /// <summary>
        /// The messaging modality
        /// </summary>
        private MessagingCall m_messagingCall;

        /// <summary>
        /// The AudioVideo modality of the conversation.
        /// </summary>
        private AudioVideoCall m_audioVideoCall;

        /// <summary>
        /// The onlineMeeting  resource
        /// </summary>
        private ConversationConference m_conversationConference;

        /// <summary>
        /// The conversation bridge resource
        /// </summary>
        private ConversationBridge m_conversationBridge;

        /// <summary>
        /// The participants involved in the conversation
        /// </summary>
        private ParticipantsInternal m_participants;

        private EventHandler<ParticipantChangeEventArgs> m_handleParticipantChange;

        private EventHandler<ConversationStateChangedEventArgs> m_conversationStateChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Conversation"/> class.
        /// </summary>
        /// <param name="restfulClient">The restful client.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="resourceUri">The resource URI.</param>
        /// <param name="parent">The parent.</param>
        /// <exception cref="System.ArgumentNullException">parent - Communication is required</exception>
        internal Conversation(IRestfulClient restfulClient, ConversationResource resource, Uri baseUri, Uri resourceUri, Communication parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Communication is required");
            }
            m_participants = new ParticipantsInternal(restfulClient, baseUri, null, this);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the list of active modalities for this Conversation.
        /// </summary>
        public IReadOnlyCollection<ConversationModalityType> ActiveModalities
        {
            get { return this.PlatformResource?.ActiveModalities; }
        }

        /// <summary>
        /// State of Conversation
        /// </summary>
        public ConversationState State
        {
            get { return PlatformResource?.State ?? default(ConversationState); }
        }

        /// <summary>
        /// Get The messaging call
        /// </summary>
        public IMessagingCall MessagingCall
        {
            get { return m_messagingCall; }
        }

        /// <summary>
        /// Gets the audio video call.
        /// </summary>
        /// <value>The audio video call.</value>
        public IAudioVideoCall AudioVideoCall
        {
            get { return m_audioVideoCall; }
        }

        /// <summary>
        /// Gets the conversation conference.
        /// </summary>
        /// <value>The conversation conference.</value>
        public IConversationConference ConversationConference
        {
            get { return m_conversationConference; }
        }

        /// <summary>
        /// Gets the conversation bridge.
        /// </summary>
        /// <value>The conversation bridge.</value>
        public IConversationBridge ConversationBridge
        {
            get { return m_conversationBridge; }
        }

        /// <summary>
        /// Get the participants
        /// </summary>
        public List<IParticipant> Participants
        {
            get
            {
                if (m_participants?.ParticipantsCache != null)
                {
                    return m_participants.ParticipantsCache.Values.Cast<IParticipant>().ToList();
                }
                else
                {
                    return new List<IParticipant>();
                }
            }
        }

        #endregion

        #region Public events

        public event EventHandler<ParticipantChangeEventArgs> HandleParticipantChange
        {
            add
            {
                if (m_handleParticipantChange == null || !m_handleParticipantChange.GetInvocationList().Contains(value))
                {
                    m_handleParticipantChange += value;
                }
            }
            remove { m_handleParticipantChange -= value; }
        }

        /// <summary>
        /// Event raised when <see cref="ConversationState"/> is changed for this <see cref="Conversation"/>.
        /// </summary>
        /// <remarks>
        /// This event is raised <i>after</i> the corresponding
        /// <see cref="BasePlatformResource{TPlatformResource, TCapabilities}.HandleResourceUpdated"/>,
        /// <see cref="BasePlatformResource{TPlatformResource, TCapabilities}.HandleResourceCompleted"/> or
        /// <see cref="BasePlatformResource{TPlatformResource, TCapabilities}.HandleResourceRemoved"/>
        /// events have been raised but before raising events for children calls and resources.
        /// </remarks>
        public event EventHandler<ConversationStateChangedEventArgs> ConversationStateChanged
        {
            add
            {
                if (m_conversationStateChanged == null || !m_conversationStateChanged.GetInvocationList().Contains(value))
                {
                    m_conversationStateChanged += value;
                }
            }
            remove { m_conversationStateChanged -= value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get participant object based on the href
        /// </summary>
        /// <param name="href">the href of participant</param>
        /// <returns></returns>
        public IParticipant TryGetParticipant(string href)
        {
            string uri =  UriHelper.NormalizeUri(href, this.BaseUri);
            Participant result = null;

            if (m_participants?.ParticipantsCache != null)
            {
                 m_participants.ParticipantsCache.TryGetValue(uri, out result);
            }

            return result;
        }

        /// <summary>
        /// add participant as an asynchronous operation.
        /// </summary>
        /// <param name="targetSip">The target sip.</param>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>Task&lt;IParticipantInvitation&gt;.</returns>
        /// <exception cref="CapabilityNotAvailableException">Link to add participant is not available.</exception>
        /// <exception cref="RemotePlatformServiceException">
        /// Timeout to get Participant invitation started event from platformservice!
        /// or
        /// Platformservice do not deliver a ParticipantInvitation resource with operationId " + operationId
        /// </exception>
        public async Task<IParticipantInvitation> AddParticipantAsync(SipUri targetSip, LoggingContext loggingContext = null)

        {
            string href = PlatformResource?.AddParticipantResourceLink?.Href;
            if (string.IsNullOrEmpty(href))
            {
                throw new CapabilityNotAvailableException("Link to add participant is not available.");
            }

            var communication = this.Parent as Communication;

            IInvitation invite = null;
            string operationId = Guid.NewGuid().ToString();
            TaskCompletionSource<IInvitation> tcs = new TaskCompletionSource<IInvitation>();

            communication.HandleNewInviteOperationKickedOff(operationId, tcs);

            var input = new AddParticipantInvitationInput
            {
                OperationContext = operationId,
                To = targetSip.ToString()
            };

            Uri addparticipantUrl = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            await this.PostRelatedPlatformResourceAsync(addparticipantUrl, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            try
            {
                invite = await tcs.Task.TimeoutAfterAsync(WaitForEvents).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new RemotePlatformServiceException("Timeout to get Participant invitation started event from platformservice!");
            }

            var result = invite as ParticipantInvitation;
            if (result == null)
            {
                throw new RemotePlatformServiceException("Platformservice do not deliver a ParticipantInvitation resource with operationId " + operationId);
            }

            return result;
        }

        [Obsolete("Please use the other variation")]
        public Task<IParticipantInvitation> AddParticipantAsync(string targetSip, LoggingContext loggingContext = null)
        {
            return AddParticipantAsync(new SipUri(targetSip), loggingContext);
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>

        public override bool Supports(ConversationCapability capability)
        {
            string href = null;
            switch (capability)
            {
                case ConversationCapability.AddParticipant:
                    {
                        href = PlatformResource?.AddParticipantResourceLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Handle current conversation events
        /// </summary>
        /// <param name="eventContext">Events to be processed</param>
        internal override bool ProcessAndDispatchEventsToChild(EventContext eventContext)
        {
            bool processed = false;
            Logger.Instance.Information(string.Format("[Conversation] get incoming communication event, sender: {0}, senderHref: {1}, EventResourceName: {2} EventFullHref: {3}, EventType: {4} ,LoggingContext: {5}",
            eventContext.SenderResourceName, eventContext.SenderHref, eventContext.EventResourceName, eventContext.EventFullHref, eventContext.EventEntity.Relationship.ToString(), eventContext.LoggingContext == null ? string.Empty : eventContext.LoggingContext.ToString()));

            // Messaging
            processed = this.HandleConversationChildEvents<MessagingCall, MessagingResource>(eventContext, ref m_messagingCall, TokenMapper.GetTokenName(typeof(MessagingResource)),
                (messagingResource) => new MessagingCall(this.RestfulClient, messagingResource, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, messagingResource.SelfUri), this));

            // AudioVideo
            if (!processed)
            {
                processed = this.HandleConversationChildEvents<AudioVideoCall, AudioVideoResource>(eventContext, ref m_audioVideoCall, TokenMapper.GetTokenName(typeof(AudioVideoResource)),
                        (AudioVideoResource) => new AudioVideoCall(this.RestfulClient, AudioVideoResource, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, AudioVideoResource.SelfUri), this));
            }

            // ConversationConference
            if (!processed)
            {
                processed = this.HandleConversationChildEvents<ConversationConference, ConversationConferenceResource>(eventContext, ref m_conversationConference, TokenMapper.GetTokenName(typeof(ConversationConferenceResource)),
                        (ConversationConferenceResource) => new ConversationConference(this.RestfulClient, ConversationConferenceResource, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, ConversationConferenceResource.SelfUri), this));
            }

            // ConversationBridge
            if (!processed)
            {
                processed = this.HandleConversationChildEvents<ConversationBridge, ConversationBridgeResource>(eventContext, ref m_conversationBridge, TokenMapper.GetTokenName(typeof(ConversationBridgeResource)),
                        (ConversationBridgeResource) => new ConversationBridge(this.RestfulClient, ConversationBridgeResource, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, ConversationBridgeResource.SelfUri), this));
            }

            // Participants
            if (!processed)
            {
                //Here is a hack for participant. Basically we will never get the participantsResource in event, we need to leverage ParticipantsInternal to handle the participant related events
                processed = this.HandleConversationChildEvents<ParticipantsInternal, ParticipantsResource>(eventContext, ref m_participants, TokenMapper.GetTokenName(typeof(ParticipantsResource)),
                     (a) => new ParticipantsInternal(this.RestfulClient, this.BaseUri, null, this));
            }

            return processed;
        }

        internal override void HandleResourceEvent(EventContext eventcontext)
        {
            ConversationState oldState = State;
            base.HandleResourceEvent(eventcontext);

            // Raise state changed event
            ConversationState newState = State;
            EventHandler<ConversationStateChangedEventArgs> stateChangeHandler = m_conversationStateChanged;

            if(oldState != newState && stateChangeHandler != null)
            {
                stateChangeHandler(this, new ConversationStateChangedEventArgs(oldState, newState));
            }

            // Create children resources
            PopulateConversationChildResources(eventcontext.EventEntity.EmbeddedResource as ConversationResource);
        }

        /// <summary>
        /// On participant changed
        /// </summary>
        /// <param name="participantChangeEventArgs"></param>
        internal void OnParticipantChange(ParticipantChangeEventArgs participantChangeEventArgs)
        {
            m_handleParticipantChange?.Invoke(this, participantChangeEventArgs);
        }

        #endregion

        #region Private methods

        private void PopulateConversationChildResources(ConversationResource resource) //Events processing are always sequenced, so do not need to worry about thread safe
        {
            if (resource != null)
            {
                if (resource.MessagingResourceLink != null && m_messagingCall == null)
                {
                    m_messagingCall = new MessagingCall(this.RestfulClient, null, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, resource.MessagingResourceLink.Href), this);
                }

                if (resource.AudioVideoResourceLink != null && m_audioVideoCall == null)
                {
                    m_audioVideoCall = new AudioVideoCall(this.RestfulClient, null, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, resource.AudioVideoResourceLink.Href), this);
                }

                if (resource.ConversationConferenceResourceLink != null && m_conversationConference == null)
                {
                    m_conversationConference = new ConversationConference(this.RestfulClient, null, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, resource.ConversationConferenceResourceLink.Href), this);
                }

                if (resource.ConversationBridgeInfoResourceLink != null && m_conversationBridge == null)
                {
                    m_conversationBridge = new ConversationBridge(this.RestfulClient, null, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, resource.ConversationBridgeInfoResourceLink.Href), this);
                }
            }
        }

        /// <summary>
        /// Handle Conversation childs events
        /// </summary>
        /// <typeparam name="TPlatformResource"></typeparam>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="eventContext"></param>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <param name="initiator"></param>
        private bool HandleConversationChildEvents<TPlatformResource, TResource>(EventContext eventContext, ref TPlatformResource entity, string token, Func<TResource, TPlatformResource> initiator)
            where TResource : Resource
            where TPlatformResource : EventableEntity
        {
            if (string.Equals(eventContext.EventEntity.Link.Token, token))
            //If the resource belong to child of current resource (like messaging, audioVideo,onlinemeeting under Conversation, process it locally and stop pass to deeper childs)
            {
                if (entity == null)
                {
                    TResource localResource = this.ConvertToPlatformServiceResource<TResource>(eventContext);
                    if (localResource != null)
                    {
                        entity = initiator(localResource);
                    }
                }

                entity?.HandleResourceEvent(eventContext);

                return true;
            }
            else //If the resource does not match current resource, it is possible for its child resources
            {
                if (entity != null)
                {
                    return entity.ProcessAndDispatchEventsToChild(eventContext);
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Argument for the event when participant is changed
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ParticipantChangeEventArgs : EventArgs
    {
        /// <summary>
        /// The list of participants added to the list
        /// </summary>
        /// <value>The added participants.</value>
        public List<IParticipant> AddedParticipants { get; internal set; }

        /// <summary>
        /// The list of participants removed from the list
        /// </summary>
        /// <value>The removed participants.</value>
        public List<IParticipant> RemovedParticipants { get; internal set; }

        /// <summary>
        /// The list of participants updated in the list
        /// </summary>
        /// <value>The updated participants.</value>
        public List<IParticipant> UpdatedParticipants { get; internal set; }
    }

    /// <summary>
    /// The arguments for the event when the <see cref="AudioVideoFlow"/> is changed
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class AudioVideoFlowUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the audio video flow.
        /// </summary>
        /// <value>The audio video flow.</value>
        public IAudioVideoFlow AudioVideoFlow { get; internal set; }
    }

    /// <summary>
    /// The arguments for the event when the ConversationState is changed
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ConversationStateChangedEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationStateChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldState">The old state.</param>
        /// <param name="newState">The new state.</param>
        public ConversationStateChangedEventArgs(ConversationState oldState, ConversationState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the old state.
        /// </summary>
        /// <value>The old state.</value>
        public ConversationState OldState { get; }

        /// <summary>
        /// Gets the new state.
        /// </summary>
        /// <value>The new state.</value>
        public ConversationState NewState { get; }

        #endregion
    }
}







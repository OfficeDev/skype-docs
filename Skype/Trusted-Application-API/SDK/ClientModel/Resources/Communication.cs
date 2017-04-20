using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class Communication : BasePlatformResource<CommunicationResource, CommunicationCapability>, ICommunication
    {
        #region Private fields

        /// <summary>
        /// Conversations
        /// </summary>
        private readonly ConcurrentDictionary<string, Conversation> m_conversations;

        /// <summary>
        /// invitations
        /// </summary>
        private readonly ConcurrentDictionary<string, IInvitation> m_invites;

        /// <summary>
        /// invitations TCS: invites thread Id &lt;---&gt; Invites Tcs, this is to track the incoming invite comes
        /// </summary>
        private readonly ConcurrentDictionary<string, TaskCompletionSource<IInvitation>> m_inviteAddedTcses;

        #endregion

        #region Constructor

        internal Communication(IRestfulClient restfulClient, CommunicationResource resource, Uri baseUri, Uri resourceUri, Application parent)
                : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            m_conversations = new ConcurrentDictionary<string, Conversation>();
            m_invites = new ConcurrentDictionary<string, IInvitation>();
            m_inviteAddedTcses = new ConcurrentDictionary<string, TaskCompletionSource<IInvitation>>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts messaging with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackUrl">Uri where all related events need to be posted</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used for logging all related events.</param>
        /// <returns><see cref="IMessagingInvitation" /> which can be used to wait for user to accept/decline the invitation</returns>
        [Obsolete("Please use the other StartMessagingAsync")]
        public Task<IMessagingInvitation> StartMessagingAsync(string subject, string to, string callbackUrl, LoggingContext loggingContext = null)
        {
            Logger.Instance.Information(string.Format("[Communication] calling startMessaging. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.StartMessagingLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to start messaging is not available.");
            }

            return StartMessagingWithIdentityAsync(subject, to, null, callbackUrl, href, null, null, loggingContext);
        }

        /// <summary>
        /// Starts messaging with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackContext">An opaque blob which will be provided in all related events so that your application can relate them to this operation</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used for logging all related events.</param>
        /// <returns><see cref="IMessagingInvitation" /> which can be used to wait for user to accept/decline the invitation</returns>
        public Task<IMessagingInvitation> StartMessagingAsync(string subject, SipUri to, string callbackContext, LoggingContext loggingContext = null)
        {
            Logger.Instance.Information(string.Format("[Communication] calling startMessaging. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.StartMessagingLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to start messaging is not available.");
            }

            return StartMessagingWithIdentityAsync(subject, to.ToString(), callbackContext, null, href, null, null, loggingContext);
        }

        /// <summary>
        /// Starts messaging with a user; user will see the message as originating from the specified identity
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackUrl">Uri where all related events need to be posted</param>
        /// <param name="localUserDisplayName">Name to be used as the originator's identity</param>
        /// <param name="localUserUri">SIP uri to be used as the originator's identity</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used for logging all related events.</param>
        /// <returns><see cref="IMessagingInvitation" /> which can be used to wait for user to accept/decline the invitation</returns>
        [Obsolete("This feature is not supported by SfB server for public applications")]
        public Task<IMessagingInvitation> StartMessagingWithIdentityAsync(string subject, string to, string callbackUrl, string localUserDisplayName, string localUserUri, LoggingContext loggingContext = null)
        {
            Logger.Instance.Information(string.Format("[Communication] calling startMessagingWithIdentity. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.StartMessagingWithIdentityLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to start messaging with identity is not available.");
            }

            return StartMessagingWithIdentityAsync(subject, to, null, callbackUrl, href, localUserDisplayName, localUserUri, loggingContext);
        }

        /// <summary>
        /// Starts an audio video call with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackUrl">Uri where all related events need to be posted</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used to log all related events</param>
        /// <returns><see cref="IAudioVideoInvitation" /> which can be used to wait for user to accept/decline the invitation</returns>
        [Obsolete("Please use the other StartAudioVideoAsync")]
        public Task<IAudioVideoInvitation> StartAudioVideoAsync(string subject, string to, string callbackUrl, LoggingContext loggingContext = null)
        {
            Logger.Instance.Information(string.Format("[Communication] calling startAudioVideo. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.StartAudioVideoLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to start AudioVideoCall is not available.");
            }

            return StartAudioVideoAsync(href, subject, to, null, callbackUrl, loggingContext);
        }

        /// <summary>
        /// Starts an audio video call with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackContext">An opaque blob which will be provided in all related events so that your application can relate them to this operation</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used to log all related events</param>
        /// <returns><see cref="IAudioVideoInvitation" /> which can be used to wait for user to accept/decline the invitation</returns>
        public Task<IAudioVideoInvitation> StartAudioVideoAsync(string subject, SipUri to, string callbackContext, LoggingContext loggingContext = null)
        {
            Logger.Instance.Information(string.Format("[Communication] calling startAudioVideo. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.StartAudioVideoLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to start AudioVideoCall is not available.");
            }

            return StartAudioVideoAsync(href, subject, to.ToString(), callbackContext, null, loggingContext);
        }

        /// <summary>
        /// Starts an audio call with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackUrl">Uri where all related events need to be posted</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used to log all related events</param>
        /// <returns><see cref="IAudioVideoInvitation" /> which can be used to wait for user to accept/decline the invitation</returns>
        [Obsolete("Please use the other StartAudioAsync")]
        public Task<IAudioVideoInvitation> StartAudioAsync(string subject, string to, string callbackUrl, LoggingContext loggingContext = null)
        {
            Logger.Instance.Information(string.Format("[Communication] calling startAudio. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.StartAudioLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to start audio is not available.");
            }

            return StartAudioVideoAsync(href, subject, to, null, callbackUrl, loggingContext);
        }

        /// <summary>
        /// Starts an audio call with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackContext">An opaque blob which will be provided in all related events so that your application can relate them to this operation</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used to log all related events</param>
        /// <returns><see cref="IAudioVideoInvitation" /> which can be used to wait for user to accept/decline the invitation</returns>
        public Task<IAudioVideoInvitation> StartAudioAsync(string subject, SipUri to, string callbackContext, LoggingContext loggingContext = null)
        {
            Logger.Instance.Information(string.Format("[Communication] calling startAudio. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.StartAudioLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to start audio is not available.");
            }

            return StartAudioVideoAsync(href, subject, to.ToString(), callbackContext, null, loggingContext);
        }

        /// <summary>
        /// Checks whether the application can join a specific meeting or not
        /// </summary>
        /// <param name="adhocMeeting">Meeting to be checked</param>
        /// <returns><code>true</code> if and only if the application is capable of joining the meeting</returns>
        public bool CanJoinAdhocMeeting(IAdhocMeeting adhocMeeting)
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            return adhocMeeting.Supports(AdhocMeetingCapability.JoinAdhocMeeting);
            #pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Checks whether the application can create and join a meeting for the invitation or not
        /// </summary>
        /// <param name="invitation">Invitation for which the meeting needs to be started</param>
        /// <returns><code>true</code> if and only if the application is capable of joining the meeting</returns>
        public bool CanStartAdhocMeeting(IMessagingInvitation invitation)
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            return invitation.Supports(MessagingInvitationCapability.StartAdhocMeeting);
            #pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Checks whether the application can create and join a meeting for the invitation or not
        /// </summary>
        /// <param name="invitation">Invitation for which the meeting needs to be started</param>
        /// <returns><code>true</code> if and only if the application is capable of joining the meeting</returns>
        public bool CanStartAdhocMeeting(IAudioVideoInvitation invitation)
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            return invitation.Supports(AudioVideoInvitationCapability.StartAdhocMeeting);
            #pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Adds the application to the meeting.
        /// </summary>
        /// <param name="adhocMeeting">Meeting to be joined</param>
        /// <param name="callbackContext">A state/context object which will be provided by SfB in all related events</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used for logging all related events</param>
        /// <returns><see cref="IOnlineMeetingInvitation" /> which can be used to wait for the meeting join to complete</returns>
        public Task<IOnlineMeetingInvitation> JoinAdhocMeetingAsync(IAdhocMeeting adhocMeeting, string callbackContext, LoggingContext loggingContext = null)
        {
            return (adhocMeeting as AdhocMeeting).JoinAdhocMeetingAsync(callbackContext, loggingContext);
        }

        /// <summary>
        /// Schedules and joins an adhoc meeting for the invitation
        /// </summary>
        /// <param name="invitation">Invitation for which the meeting needs to be created</param>
        /// <param name="subject">Subject of the meeting</param>
        /// <param name="callbackContext">A state/context object which will be provided by SfB in all related events</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used for logging all related events</param>
        /// <returns><see cref="IOnlineMeetingInvitation" /> which can be used to wait for the meeting join to complete</returns>
        public Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(IMessagingInvitation invitation, string subject, string callbackContext, LoggingContext loggingContext = null)
        {
            return (invitation as MessagingInvitation).StartAdhocMeetingAsync(subject, callbackContext, null, loggingContext);
        }

        /// <summary>
        /// Schedules and joins an adhoc meeting for the invitation
        /// </summary>
        /// <param name="invitation">Invitation for which the meeting needs to be created</param>
        /// <param name="subject">Subject of the meeting</param>
        /// <param name="callbackContext">A state/context object which will be provided by SfB in all related events</param>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used for logging all related events</param>
        /// <returns><see cref="IOnlineMeetingInvitation" /> which can be used to wait for the meeting join to complete</returns>
        public Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(IAudioVideoInvitation invitation, string subject, string callbackContext, LoggingContext loggingContext = null)
        {
            return (invitation as AudioVideoInvitation).StartMeetingAsync(subject, callbackContext, loggingContext);
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>
        /// Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="CapabilityNotAvailableException" />
        /// </remarks>
        public override bool Supports(CommunicationCapability capability)
        {
            string href = null;
            switch (capability)
            {
                case CommunicationCapability.StartMessaging:
                    {
                        href = PlatformResource?.StartMessagingLink?.Href;
                        break;
                    }
                case CommunicationCapability.StartMessagingWithIdentity:
                    {
                        href = PlatformResource?.StartMessagingWithIdentityLink?.Href;
                        break;
                    }
                case CommunicationCapability.StartAudioVideo:
                    {
                        href = PlatformResource?.StartAudioVideoLink?.Href;
                        break;
                    }
                case CommunicationCapability.StartAudio:
                    {
                        href = PlatformResource?.StartAudioLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Dispatch events to conversations
        /// </summary>
        /// <param name="eventContexts"></param>
        internal void DispatchConversationEvents(List<EventContext> eventContexts)//Suppose sender of eventContext list should be same
        {
            if (eventContexts == null || eventContexts.Count == 0)
            {
                return;
            }
            EventContext eventDefault = eventContexts.FirstOrDefault();
            string conversationUri = UriHelper.NormalizeUriWithNoQueryParameters(eventDefault.SenderHref, eventDefault.BaseUri);
            Conversation conversation = null;
            m_conversations.TryGetValue(conversationUri, out conversation);
            if (conversation != null)
            {
                foreach (EventContext e in eventContexts)
                {
                    conversation.ProcessAndDispatchEventsToChild(e);
                }
            }
        }

        internal override bool ProcessAndDispatchEventsToChild(EventContext eventContext)
        {
            //There is no child for events with sender = communication
            Logger.Instance.Information(string.Format("[Communication] get incoming communication event, sender: {0}, senderHref: {1}, EventResourceName: {2} EventFullHref: {3}, EventType: {4} ,LoggingContext: {5}",
                        eventContext.SenderResourceName, eventContext.SenderHref, eventContext.EventResourceName, eventContext.EventFullHref, eventContext.EventEntity.Relationship.ToString(), eventContext.LoggingContext == null ? string.Empty : eventContext.LoggingContext.ToString()));

            if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(ConversationResource))))
            {
                string conversationNormalizedUri = UriHelper.NormalizeUriWithNoQueryParameters(eventContext.EventEntity.Link.Href, eventContext.BaseUri);
                Conversation currentConversation = m_conversations.GetOrAdd(conversationNormalizedUri,
                    (a) =>
                    {
                        Logger.Instance.Information(string.Format("[Communication] Add conversation {0} LoggingContext: {1}",
                        conversationNormalizedUri, eventContext.LoggingContext == null ? string.Empty : eventContext.LoggingContext.ToString()));

                        ConversationResource localResource = this.ConvertToPlatformServiceResource<ConversationResource>(eventContext);
                        //For every conversation resource, we want to make sure it is using latest rest ful client
                        return new Conversation(this.RestfulClient, localResource, eventContext.BaseUri, eventContext.EventFullHref, this);
                    }
                    );

                //Remove from cache if it is a delete operation
                if (eventContext.EventEntity.Relationship == EventOperation.Deleted)
                {
                    Conversation removedConversation = null;
                    Logger.Instance.Information(string.Format("[Communication] Remove conversation {0} LoggingContext: {1}",
                           conversationNormalizedUri, eventContext.LoggingContext == null ? string.Empty : eventContext.LoggingContext.ToString()));
                    m_conversations.TryRemove(conversationNormalizedUri, out removedConversation);
                }

                currentConversation.HandleResourceEvent(eventContext);

                return true;
            }
            else if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(MessagingInvitationResource))))
            {
                this.HandleInvitationEvent<MessagingInvitationResource>(
                    eventContext,
                    (localResource) => new MessagingInvitation(this.RestfulClient, localResource, eventContext.BaseUri, eventContext.EventFullHref, this)
                 );
                return true;
            }
            else if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(AudioVideoInvitationResource))))
            {
                this.HandleInvitationEvent<AudioVideoInvitationResource>(
                    eventContext,
                    (localResource) => new AudioVideoInvitation(this.RestfulClient, localResource, eventContext.BaseUri, eventContext.EventFullHref, this)
                );
                return true;
            }
            else if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(OnlineMeetingInvitationResource))))
            {
                this.HandleInvitationEvent<OnlineMeetingInvitationResource>(
                    eventContext,
                    (localResource) => new OnlineMeetingInvitation(this.RestfulClient, localResource, eventContext.BaseUri, eventContext.EventFullHref, this)
                );
                return true;
            }
            else if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(ParticipantInvitationResource))))
            {
                this.HandleInvitationEvent<ParticipantInvitationResource>(
                    eventContext,
                    (localResource) => new ParticipantInvitation(this.RestfulClient, localResource, eventContext.BaseUri, eventContext.EventFullHref, this)
                );
                return true;
            }
            //TODO: Process , audioVideoInvitation, ...
            else
            {
                return false;
            }
        }

        private void HandleInvitationEvent<T>(EventContext eventcontext, Func<T, IInvitation> inviteGenerateDelegate) where T : InvitationResource
        {
            string NormalizedUri = UriHelper.NormalizeUriWithNoQueryParameters(eventcontext.EventEntity.Link.Href, eventcontext.BaseUri);
            T localResource = this.ConvertToPlatformServiceResource<T>(eventcontext);
            IInvitation invite = m_invites.GetOrAdd(localResource.OperationContext, (a) =>
            {
                Logger.Instance.Information(string.Format("[Communication] Started and Add invitation: OperationContext:{0}, Href: {1} , LoggingContext: {2}",
                    localResource.OperationContext, NormalizedUri, eventcontext.LoggingContext == null ? string.Empty : eventcontext.LoggingContext.ToString()));

                return inviteGenerateDelegate(localResource);
            });

            if (invite.RelatedConversation == null)
            {
                //Populate conversation resource if needed
                string relatedConversationNormalizedUri = UriHelper.NormalizeUriWithNoQueryParameters(localResource.ConversationResourceLink.Href, eventcontext.BaseUri);
                Uri relatedConversationUri = UriHelper.CreateAbsoluteUri(eventcontext.BaseUri, localResource.ConversationResourceLink.Href);
                Conversation relatedConversation = m_conversations.GetOrAdd(relatedConversationNormalizedUri,
                    (a) =>
                    {
                        Logger.Instance.Information(string.Format("[Communication] Add conversation {0} LoggingContext: {1}",
                            relatedConversationNormalizedUri, eventcontext.LoggingContext == null ? string.Empty : eventcontext.LoggingContext.ToString()));

                        return new Conversation(this.RestfulClient, null, eventcontext.BaseUri, relatedConversationUri,this);
                    }
                );
                ((IInvitationWithConversation)invite).SetRelatedConversation(relatedConversation);
            }

            //Remove from cache if it is a complete operation
            if (eventcontext.EventEntity.Relationship == EventOperation.Completed)
            {
                IInvitation completedInvite = null;
                Logger.Instance.Information(string.Format("[Communication] Completed and remove invitation: OperationContext:{0}, Href: {1} , LoggingContext: {2}",
                      localResource.OperationContext, NormalizedUri, eventcontext.LoggingContext == null ? string.Empty : eventcontext.LoggingContext.ToString()));
                m_invites.TryRemove(localResource.OperationContext, out completedInvite);
            }

            var eventableEntity = invite as EventableEntity;
            eventableEntity.HandleResourceEvent(eventcontext);

            if (eventcontext.EventEntity.Relationship == EventOperation.Started)
            //here we ignore the case that a new incoming invite is failure and with completed operation
            {
                var temp = eventcontext.EventEntity.EmbeddedResource as InvitationResource;
                if (temp.Direction == Direction.Incoming)
                {
                    var application = this.Parent as Application;
                    var applications = application.Parent as Applications;
                    var discover = applications.Parent as Discover;
                    var endpoint = discover.Parent as ApplicationEndpoint;
                    endpoint.HandleNewIncomingInvite(invite);
                    //TODO:should we treat new incoming INVITE (with new conversation) differently than the incoming modality escalation invite?
                }
            }
        }

        /// <summary>
        /// Handle a invitation started event
        /// </summary>
        /// <param name="operationId">ID of the operation</param>
        /// <param name="invite">Invitation which was started</param>
        internal void HandleInviteStarted(string operationId, IInvitation invite)
        {
            TaskCompletionSource<IInvitation> tcs = null;
            m_inviteAddedTcses.TryGetValue(operationId, out tcs);
            if (tcs != null)
            {
                tcs.TrySetResult(invite);
                TaskCompletionSource<IInvitation> removeTemp = null;
                m_inviteAddedTcses.TryRemove(operationId, out removeTemp);
            }
        }

        /// <summary>
        /// Tracking the invitation resources
        /// </summary>
        /// <param name="operationId">ID of the operation</param>
        /// <param name="tcs"><see cref="TaskCompletionSource{TResult}"/> which will be completed when invite completes</param>
        internal void HandleNewInviteOperationKickedOff(string operationId, TaskCompletionSource<IInvitation> tcs)
        {
            if (string.IsNullOrEmpty(operationId) || tcs == null)
            {
                throw new RemotePlatformServiceException("Faied to add null object into m_inviteAddedTcses which is to track the incoming invite.");
            }

            m_inviteAddedTcses.TryAdd(operationId, tcs);
        }

        #endregion

        #region Private methods

        private async Task<IMessagingInvitation> StartMessagingWithIdentityAsync(string subject, string to, string callbackContext, string callbackUrl, string href, string localUserDisplayName, string localUserUri, LoggingContext loggingContext = null)
        {
            (Parent as Application).GetCallbackUrlAndCallbackContext(ref callbackUrl, ref callbackContext);

            string operationId = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<IInvitation>();
            HandleNewInviteOperationKickedOff(operationId, tcs);
            IInvitation invite = null;
            var input = new MessagingInvitationInput
            {
                OperationContext = operationId,
                To = to,
                Subject = subject,
                CallbackContext = callbackContext,
                CallbackUrl = callbackUrl,
                LocalUserDisplayName = localUserDisplayName,
                LocalUserUri = localUserUri
            };

            var startMessagingUri = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            await this.PostRelatedPlatformResourceAsync(startMessagingUri, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);
            try
            {
                invite = await tcs.Task.TimeoutAfterAsync(WaitForEvents).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new RemotePlatformServiceException("Timeout to get incoming messaging invitation started event from platformservice!");
            }

            //We are sure the invite sure be there now.
            var result = invite as MessagingInvitation;
            if (result == null)
            {
                throw new RemotePlatformServiceException("Platformservice do not deliver a messageInvitation resource with operationId " + operationId);
            }

            return result;
        }

        private async Task<IAudioVideoInvitation> StartAudioVideoAsync(string href, string subject, string to, string callbackContext, string callbackUrl, LoggingContext loggingContext = null)
        {
            (Parent as Application).GetCallbackUrlAndCallbackContext(ref callbackUrl, ref callbackContext);

            var operationId = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<IInvitation>();
            HandleNewInviteOperationKickedOff(operationId, tcs);
            IInvitation invite = null;
            var input = new AudioVideoInvitationInput
            {
                OperationContext = operationId,
                To = to,
                Subject = subject,
                CallbackContext = callbackContext,
                CallbackUrl = callbackUrl,
                MediaHost = MediaHostType.Remote
            };

            Uri startAudioVideoUri = UriHelper.CreateAbsoluteUri(this.BaseUri, href);

            await this.PostRelatedPlatformResourceAsync(startAudioVideoUri, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);
            try
            {
                invite = await tcs.Task.TimeoutAfterAsync(WaitForEvents).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new RemotePlatformServiceException("Timeout to get incoming audioVideo invitation started event from platformservice!");
            }

            // We are sure that the invite is there now.
            var result = invite as AudioVideoInvitation;
            if (result == null)
            {
                throw new RemotePlatformServiceException("Platformservice do not deliver a AudioVideoInvitation resource with operationId " + operationId);
            }

            return result;
        }

        #endregion
    }
}

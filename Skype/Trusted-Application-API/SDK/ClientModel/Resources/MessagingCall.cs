using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents a  MessagingCall inside an conversation.
    /// </summary>
    /// <seealso cref="Call{TPlatformResource, TInvitation, TCapabilities}"/>
    /// <seealso cref="IMessagingCall" />
    internal class MessagingCall : Call<MessagingResource, IMessagingInvitation, MessagingCallCapability>, IMessagingCall
    {
        #region Private fields

        /// <summary>
        /// OutgoingMessagings
        /// </summary>
        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> m_outGoingmessageTcses;

        #endregion

        #region Public properties

        /// <summary>
        /// Handler for incoming message
        /// </summary>
        public EventHandler<IncomingMessageEventArgs> IncomingMessageReceived { get; set; }

        #endregion

        #region Constructor

        internal MessagingCall(IRestfulClient restfulClient, MessagingResource resource, Uri baseUri, Uri resourceUri, Conversation parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            m_outGoingmessageTcses = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Conversation is required");
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Stop messaging, RemotePlatformServiceException may throw if remote failed
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        public override Task TerminateAsync(LoggingContext loggingContext = null)
        {
            string href = PlatformResource?.StopMessagingLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to terminate messaging is not available.");
            }

            Uri stopLink = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            return this.PostRelatedPlatformResourceAsync(stopLink, null, loggingContext);
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">Message to be sent</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="contentType">
        /// Type of the message; could be any of these:
        /// <ul>
        /// <li><seealso cref="Common.Constants.TextPlainContentType"/></li>
        /// <li><seealso cref="Common.Constants.TextHtmlContentType"/></li>
        /// </ul>
        /// </param>
        public async Task SendMessageAsync(string message, LoggingContext loggingContext = null, string contentType = Constants.TextPlainContentType)
        {
            string href = PlatformResource?.SendMessageLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to send message is not available.");
            }

            var sendmessageUri = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            using (var messageContent = new StringContent(message, Encoding.UTF8, contentType))
            {
                HttpResponseMessage response = await this.PostRelatedPlatformResourceAsync(sendmessageUri, messageContent, loggingContext).ConfigureAwait(false);
                if (response?.Headers?.Location != null)
                {
                    m_outGoingmessageTcses.TryAdd(UriHelper.CreateAbsoluteUri(this.BaseUri, response.Headers.Location.ToString()).ToString().ToLower(), tcs);
                }
            }

            await tcs.Task.ConfigureAwait(false);
        }

        /// <summary>
        /// Establishes a <see cref="IMessagingInvitation"/>> as an asynchronous operation.
        /// </summary>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>Task&lt;TInvitation&gt;.</returns>
        /// <exception cref="Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException">Link to establish messaging is not available.</exception>
        /// <exception cref="System.Exception">
        /// [Messaging] Failed to get Conversation from messaging base parent
        /// or
        /// [Messaging] Failed to get communication from conversation base parent
        /// </exception>
        /// <exception cref="Microsoft.SfB.PlatformService.SDK.Common.RemotePlatformServiceException">
        /// Timeout to get incoming messaging invitation started event from platformservice!
        /// or
        /// Platformservice do not deliver a messageInvitation resource with operationId " + operationId
        /// </exception>
        public override async Task<IMessagingInvitation> EstablishAsync(LoggingContext loggingContext = null)
        {
            string href = PlatformResource?.AddMessagingLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to establish messaging is not available.");
            }

            Logger.Instance.Information(string.Format("[Messaging] calling AddMessaging. LoggingContext: {0}",
                 loggingContext == null ? string.Empty : loggingContext.ToString()));

            var conversation = base.Parent as Conversation;
            if (conversation == null)
            {
                Logger.Instance.Error("[Messaging] Conversation from messaging base parent is numll");
                throw new Exception("[Messaging] Failed to get Conversation from messaging base parent");
            }
            var communication = conversation.Parent as Communication;
            if (communication == null)
            {
                Logger.Instance.Error("[Messaging] communication from  conversation base parent is numll");
                throw new Exception("[Messaging] Failed to get communication from conversation base parent");
            }

            string operationId = Guid.NewGuid().ToString();
            TaskCompletionSource<IInvitation> tcs = new TaskCompletionSource<IInvitation>();
            //Tracking the incoming invitation from communication resource
            communication.HandleNewInviteOperationKickedOff(operationId, tcs);

            IInvitation invite = null;
            //It seems that this is not the expected input, please clarify the right input resource here.
            var input = new MessagingInvitationInput
            {
                OperationContext = operationId,
            };

            Uri addMessagingUri = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            await this.PostRelatedPlatformResourceAsync(addMessagingUri, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            try
            {
                invite = await tcs.Task.TimeoutAfterAsync(WaitForEvents).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new RemotePlatformServiceException("Timeout to get incoming messaging invitation started event from platformservice!");
            }

            //We are sure the invite sure be there now.
            var result = invite as IMessagingInvitation;
            if (result == null)
            {
                throw new RemotePlatformServiceException("Platformservice do not deliver a messageInvitation resource with operationId " + operationId);
            }

            return result;
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(MessagingCallCapability capability)
        {
            string href = null;
            switch (capability)
            {
                case MessagingCallCapability.Establish:
                    {
                        href = PlatformResource?.AddMessagingLink?.Href;
                        break;
                    }
                case MessagingCallCapability.SendMessage:
                    {
                        href = PlatformResource?.SendMessageLink?.Href;
                        break;
                    }
                case MessagingCallCapability.Terminate:
                    {
                        href = PlatformResource?.StopMessagingLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        #endregion

        #region Internal methods

        internal override bool ProcessAndDispatchEventsToChild(EventContext eventContext)
        {
            //No child to dispatch any more, need to dispatch to child , process locally for message type
            if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(MessageResource))))
            {
                MessageResource message = this.ConvertToPlatformServiceResource<MessageResource>(eventContext);
                if (message != null)
                {
                    if (message.Direction == Direction.Incoming)
                    {
                        // File incoming message received event
                        IncomingMessageEventArgs args = new IncomingMessageEventArgs
                        {
                            HtmlMessage = message.HtmlMessage == null? null : message.HtmlMessage.Value as TextHtmlMessage,
                            PlainMessage = message.TextMessage == null ? null : message.TextMessage.Value as TextPlainMessage
                        };
                        Conversation conv = this.Parent as Conversation;

                        Participant fromParticipant = conv.TryGetParticipant(message.ParticipantResourceLink.Href) as Participant;
                        if (fromParticipant == null)
                        {
                            Logger.Instance.Warning("Get message from an unknown participant, probably client code bug or server bug");
                        }
                        else
                        {
                            args.FromParticipantName = fromParticipant.Name;
                        }

                        IncomingMessageReceived?.Invoke(this, args);
                    }
                    else //For out going, just detect the completed event
                    {
                        if (eventContext.EventEntity.Relationship == EventOperation.Completed)
                        {
                            TaskCompletionSource<string> tcs;
                            m_outGoingmessageTcses.TryGetValue(UriHelper.CreateAbsoluteUri(this.BaseUri, eventContext.EventEntity.Link.Href).ToString().ToLower(), out tcs);
                            if (tcs != null)
                            {
                                if (eventContext.EventEntity.Status == EventStatus.Success)
                                {
                                    tcs.TrySetResult(string.Empty);
                                }
                                else if (eventContext.EventEntity.Status == EventStatus.Failure)
                                {
                                    string error = eventContext.EventEntity.Error?.GetErrorInformationString();
                                    tcs.TrySetException(new RemotePlatformServiceException("Send Message failed with error" + error + eventContext.LoggingContext.ToString()));
                                }
                                else
                                {
                                    Logger.Instance.Error("Do not get a valid status code for message complete event!");
                                    tcs.TrySetException(new RemotePlatformServiceException("Send Message failed !"));
                                }
                                m_outGoingmessageTcses.TryRemove(eventContext.EventEntity.Link.Href.ToLower(), out tcs);
                            }
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// the IncomingMessageEventArgs
    /// </summary>
    public class IncomingMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Html message
        /// </summary>
        public TextHtmlMessage HtmlMessage { get; internal set; }

        /// <summary>
        /// Plain Message
        /// </summary>
        public TextPlainMessage PlainMessage { get; internal set; }

        /// <summary>
        /// From participant name
        /// </summary>
        public string FromParticipantName { get; internal set; }
    }
}

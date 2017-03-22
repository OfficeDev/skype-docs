using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class MessagingCallTests
    {
        private LoggingContext m_loggingContext;
        private Mock<IEventChannel> m_mockEventChannel;
        private MockRestfulClient m_restfulClient;
        private IMessagingCall m_messagingCall;
        private bool m_deliverMessageCompletedEvent = true;

        [TestInitialize]
        public async void TestSetup()
        {
            m_loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            m_mockEventChannel = data.EventChannel;
            m_restfulClient = data.RestfulClient;

            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            await applicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);

            ICommunication communication = applicationEndpoint.Application.Communication;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_mockEventChannel);
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.EstablishMessagingCall, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_mockEventChannel);

                // Message completed event has to be delivered after the response, so start a new thread to wait some time before delivering the event
                if (m_deliverMessageCompletedEvent)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(10)).ConfigureAwait(false);
                        TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.SendMessage, HttpMethod.Post, "Event_MessageCompleted.json", m_mockEventChannel);
                    });
                }
            };

            // Establish a messaging call
            IMessagingInvitation invitation = await communication.StartMessagingAsync(
                "Test subject",
                new SipUri("sip:user@example.com"),
                "https://example.com/callback",
                m_loggingContext).ConfigureAwait(false);

            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ConversationConnected.json"); // It has link to the messaging call
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantAdded.json");
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessagingConnected.json"); // It has the messaging call

            m_messagingCall = invitation.RelatedConversation.MessagingCall;
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task TerminateAsyncShouldThrowIfCapabilityNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessagingConnected_NoActionLink.json"); // No links available

            // When
            await m_messagingCall.TerminateAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TerminateAsyncShouldMakeHttpRequest()
        {
            // Given
            // Setup

            // When
            await m_messagingCall.TerminateAsync(m_loggingContext).ConfigureAwait(false);

            // then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.TerminateMessagingCall));
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task SendMessageShouldThrowIfCapabilityNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessagingConnected_NoActionLink.json"); // No links available

            // When
            await m_messagingCall.SendMessageAsync("Hello World!", m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task SendMessageShouldMakeHttpRequest()
        {
            // Given
            // Setup

            // When
            await m_messagingCall.SendMessageAsync("Hello World!", m_loggingContext).ConfigureAwait(false);

            // then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.SendMessage));
        }

        [TestMethod]
        public async Task SendMessageShouldReturnATaskToWaitForMessageCompletion()
        {
            // Given
            m_deliverMessageCompletedEvent = false;
            Task messageTask = m_messagingCall.SendMessageAsync("Hello World!", m_loggingContext);

            await Task.Delay(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);
            Assert.IsFalse(messageTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessageCompleted.json");

            // Then
            Assert.IsTrue(messageTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task EstablishAsyncShouldThrowIfCapabilityNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessagingConnected_NoActionLink.json"); // No links available

            // When
            await m_messagingCall.EstablishAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task EstablishAsyncShoulMakeHttpRequest()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessagingDisconnected.json");

            // When
            await m_messagingCall.EstablishAsync(m_loggingContext).ConfigureAwait(false);

            // then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.EstablishMessagingCall));
        }

        [TestMethod]
        public async Task EstalbishAsyncInviteShouldCompleteOnlyOnCompletedEvent()
        {
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessagingDisconnected.json");

            // Save OperationId of the MessagingInvitation when HTTP request is made to start the invitation.
            string operationId = null;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                if (args.Uri == new Uri(DataUrls.EstablishMessagingCall) && args.Method == HttpMethod.Post && args.Input is InvitationInput)
                {
                    // We need to replace operationContext in invitation completed event with what was provided in the input
                    operationId = ((InvitationInput)args.Input).OperationContext;
                }
            };

            // Start MessagingInvitation
            var inviteCompleted = false;
            IMessagingInvitation invite = await m_messagingCall.EstablishAsync(m_loggingContext).ConfigureAwait(false);
            invite.HandleResourceCompleted += (sender, args) => inviteCompleted = true;

            await Task.Delay(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);

            // Make sure invitation is not completed
            Assert.IsFalse(inviteCompleted);
            Assert.IsFalse(string.IsNullOrEmpty(operationId));

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_mockEventChannel, "Event_MessagingInvitationCompleted.json", operationId);

            // then
            Assert.IsTrue(inviteCompleted);
        }

        [TestMethod]
        public async Task ShouldSupportEstablishIfLinkAvailable()
        {
            // Given
            // Messaging is disconnected. MockRestfulClient is configured to return disconnected Messaging on HTTP GET.
            await m_messagingCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            Assert.AreEqual(CallState.Disconnected, m_messagingCall.State);

            // When
            bool supported = m_messagingCall.Supports(MessagingCallCapability.Establish);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public void ShouldNotSupportEstablishIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_messagingCall.Supports(MessagingCallCapability.Establish);

            // Then
            Assert.IsFalse(supported);
        }

        [TestMethod]
        public void ShouldSupportSendMessageIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_messagingCall.Supports(MessagingCallCapability.SendMessage);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public async Task ShouldNotSupportSendMessageIfLinkNotAvailable()
        {
            // Given
            // Messaging is disconnected. MockRestfulClient is configured to return disconnected Messaging on HTTP GET.
            await m_messagingCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            Assert.AreEqual(CallState.Disconnected, m_messagingCall.State);

            // When
            bool supported = m_messagingCall.Supports(MessagingCallCapability.SendMessage);

            // Then
            Assert.IsFalse(supported);
        }

        [TestMethod]
        public void ShouldSupportTerminateIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_messagingCall.Supports(MessagingCallCapability.Terminate);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public async Task ShouldNotSupportTerminateIfLinkNotAvailable()
        {
            // Given
            // Messaging is disconnected. MockRestfulClient is configured to return disconnected Messaging on HTTP GET.
            await m_messagingCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            Assert.AreEqual(CallState.Disconnected, m_messagingCall.State);

            // When
            bool supported = m_messagingCall.Supports(MessagingCallCapability.Terminate);

            // Then
            Assert.IsFalse(supported);
        }

        [TestMethod]
        public void IncomingMessageReceivedShouldDeliverTextMessage()
        {
            // Given
            bool messageReceived = false;
            m_messagingCall.IncomingMessageReceived += (sender, args) =>
            {
                if(System.Text.Encoding.Default.GetString(args.PlainMessage.Message) == "__message__")
                {
                    messageReceived = true;
                }
            };

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessageIncoming.json");

            // Then
            Assert.IsTrue(messageReceived);
        }

        [TestMethod]
        public void IncomingMessageReceivedShouldDeliverHtmlMessage()
        {
            // Given
            bool messageReceived = false;
            m_messagingCall.IncomingMessageReceived += (sender, args) =>
            {
                // We have some html markups in the message, so we can't use exact string match
                if (System.Text.Encoding.Default.GetString(args.HtmlMessage.Message).Contains("__htmlmessage__"))
                {
                    messageReceived = true;
                }
            };

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessageIncomingHtml.json");

            // Then
            Assert.IsTrue(messageReceived);
        }

        [TestMethod]
        public void IncomingMessageReceivedShouldHaveParticipant()
        {
            // Given
            bool participantReceived = false;
            m_messagingCall.IncomingMessageReceived += (sender, args) =>
            {
                if (args.FromParticipantName == "Original Name")
                {
                    participantReceived = true;
                }
            };

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_MessageIncoming.json");

            // Then
            Assert.IsTrue(participantReceived);
        }
    }
}

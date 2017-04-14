using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    /// <summary>
    /// Unit tests for <see cref="MessagingInvitation"/>
    /// </summary>
    [TestClass]
    public class MessagingInvitationTests
    {
        private LoggingContext m_loggingContext;
        private IMessagingInvitation m_messagingInvitation;
        private Mock<IEventChannel> m_mockEventChannel;
        private MockRestfulClient m_restfulClient;
        private ApplicationEndpoint m_applicationEndpoint;

        [TestInitialize]
        public async void TestSetup()
        {
            m_loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            m_mockEventChannel = data.EventChannel;
            m_restfulClient = data.RestfulClient;

            m_applicationEndpoint = data.ApplicationEndpoint;
            await m_applicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await m_applicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);

            ICommunication communication = m_applicationEndpoint.Application.Communication;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_mockEventChannel);
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.EstablishMessagingCall, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_mockEventChannel);
            };

            // Start a messaging invitation
            m_messagingInvitation = await communication.StartMessagingWithIdentityAsync(
                "Test subject",
                "sip:user@example.com",
                "https://example.com/callback",
                "Local user",
                "sip:user1@example.com",
                m_loggingContext).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartAdhocMeetingShouldThrowIfCapabilityNotAvailable()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall_NoActionLinks.json").ConfigureAwait(false);

            // When
            await m_messagingInvitation.StartAdhocMeetingAsync("Test subject", "https://example.com/callback", m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAdhocMeetingShouldMakeTheHttpRequest()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.StartAdhocMeeting, HttpMethod.Post, "Event_OnlineMeetingInvitationStarted.json", m_mockEventChannel);

            // When
            await m_messagingInvitation.StartAdhocMeetingAsync("Test subject", "https://example.com/callback", m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.StartAdhocMeeting));
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartAdhocMeetingShouldThrowIfAdhocMeetingStartedEventNotReceived()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);
            ((MessagingInvitation)m_messagingInvitation).WaitForEvents = TimeSpan.FromMilliseconds(300);

            // When
            await m_messagingInvitation.StartAdhocMeetingAsync("Test subject", "https://example.com/callback", m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAdhocMeetingShouldReturnATaskToWaitForInvitationStartedEvent()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);
            var invitationOperationid = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.StartAdhocMeeting, HttpMethod.Post, null, null);
                if(!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationid = operationId;
                }
            };

            Task invitationTask = m_messagingInvitation.StartAdhocMeetingAsync("Test subject", "https://example.com/callback", m_loggingContext);
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_mockEventChannel, "Event_OnlineMeetingInvitationStarted.json", invitationOperationid);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        public async Task StartAdhocMeetingShouldWorkWithNullLoggingContext()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.StartAdhocMeeting, HttpMethod.Post, "Event_OnlineMeetingInvitationStarted.json", m_mockEventChannel);

            // When
            await m_messagingInvitation.StartAdhocMeetingAsync("Test subject", "https://example.com/callback", null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AcceptAndBridgeAsyncShouldThrowIfMeetingUrlNull()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);

            // When
            await m_messagingInvitation.AcceptAndBridgeAsync(m_loggingContext, null, "Example User").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AcceptAndBridgeAsyncShouldThrowIfMeetingUrlEmpty()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);

            // When
            await m_messagingInvitation.AcceptAndBridgeAsync(m_loggingContext, string.Empty, "Example User").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AcceptAndBridgeAsyncShouldThrowIfMeetingUrlIsWhitespaces()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);

            // When
            await m_messagingInvitation.AcceptAndBridgeAsync(m_loggingContext, "   ", "Example User").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task AcceptAndBridgeAsyncShouldThrowIfCapabilityNotAvailable()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall_NoActionLinks.json").ConfigureAwait(false);

            // When
            await m_messagingInvitation.AcceptAndBridgeAsync(m_loggingContext, "https://example.com", "Example User").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task AcceptAndBridgeAsyncShouldMakeHttpRequest()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);

            // When
            await m_messagingInvitation.AcceptAndBridgeAsync(m_loggingContext, "https://example.com", "Example User").ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AcceptAndBridge));
        }

        [TestMethod]
        public async Task AcceptAndBridgeAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);

            // When
            await m_messagingInvitation.AcceptAndBridgeAsync(null, "https://example.com", "Example User").ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task ShouldSupportStartAdhocMeetingIfLinkAvailable()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);

            // When
            bool supported = m_messagingInvitation.Supports(MessagingInvitationCapability.StartAdhocMeeting);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public async Task ShouldNotSupportStartAdhocMeetingIfLinkNotAvailable()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall_NoActionLinks.json").ConfigureAwait(false);

            // When
            bool supported = m_messagingInvitation.Supports(MessagingInvitationCapability.StartAdhocMeeting);

            // Then
            Assert.IsFalse(supported);
        }

        [TestMethod]
        public async Task ShouldSupportAcceptAndBridgeIfLinkAvailable()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall.json").ConfigureAwait(false);

            // When
            bool supported = m_messagingInvitation.Supports(MessagingInvitationCapability.AcceptAndBridge);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public async Task ShouldNotSupportAcceptAndBridgeIfLinkNotAvailable()
        {
            // Given
            m_messagingInvitation = await StartIncomingMessagingInvitationAsync("Event_IncomingIMCall_NoActionLinks.json").ConfigureAwait(false);

            // When
            bool supported = m_messagingInvitation.Supports(MessagingInvitationCapability.AcceptAndBridge);

            // Then
            Assert.IsFalse(supported);
        }

        #region Private methods

        private Task<IMessagingInvitation> StartIncomingMessagingInvitationAsync(string filename)
        {
            var tcs = new TaskCompletionSource<IMessagingInvitation>();

            m_applicationEndpoint.HandleIncomingInstantMessagingCall += (sender, args) => tcs.SetResult(args.NewInvite);

            TestHelper.RaiseEventsFromFile(m_mockEventChannel, filename);

            return tcs.Task;
        }

        #endregion
    }
}

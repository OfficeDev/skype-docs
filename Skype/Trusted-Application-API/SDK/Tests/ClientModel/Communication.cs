using System;
using System.IO;
using System.Net;
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
    public class CommunicationTests
    {
        private LoggingContext m_loggingContext;
        private ICommunication m_communication;
        private MockRestfulClient m_restfulClient;
        private Mock<IEventChannel> m_eventChannel;

        [TestInitialize]
        public async void TestSetup()
        {
            m_loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            m_restfulClient = data.RestfulClient;
            m_eventChannel = data.EventChannel;

            await data.ApplicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await data.ApplicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);

            m_communication = data.ApplicationEndpoint.Application.Communication;
        }

        [TestMethod]
        public async Task ShouldSupportStartMessagingIfLinkAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessaging.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartMessaging);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public void ShouldNotSupportStartMessagingIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartMessaging);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartMessagingShouldThrowIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            await m_communication.StartMessagingAsync("Test message", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartMessagingShouldWork()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessaging.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.MessagingInvitations));
        }

        [TestMethod]
        public void ShouldSupportStartMessagingWithIdentityIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartMessagingWithIdentity);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportStartMessagingWithIdentityIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessaging.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartMessagingWithIdentity);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartMessagingWithIdentityShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessaging.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", "https://example.com/callback", "Test user", "sip:user1@example.com").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartMessagingWithIdentityShouldWork()
        {
            // Given
            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);

            // When
            IMessagingInvitation invitation = await m_communication
                .StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", "https://example.com/callback", "Test user 1", "sip:user1@example.com")
                .ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.MessagingInvitations));
        }

        [TestMethod]
        public void ShouldSupportStartAudioIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartAudio);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportStartAudioIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudio.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartAudio);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartAudioShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudio.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartAudioAsync("Test subject", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioShouldWork()
        {
            // Given
            m_restfulClient.HandleRequestProcessed += (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioAsync("Test subject", "sip:user@example.com", "https://example.com/callback")
                .ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AudioInvitations));
        }

        [TestMethod]
        public async Task StartAudioShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, null, null);
                if(!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            // Given
            Task invitationTask = m_communication.StartAudioAsync("Test subject", "sip:user@example.com", "https://example.com/callback");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_AudioVideoInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartAudioShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartAudioAsync("Test subject", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public void ShouldSupportStartAudioVideoIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartAudioVideo);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportStartAudioVideoIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudioVideo.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartAudioVideo);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartAudioVideoShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudioVideo.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartAudioVideoAsync("Test subject", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioVideoShouldWork()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", "sip:user@example.com", "https://example.com/callback")
                .ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AudioVideoInvitations));
        }

        [TestMethod]
        public async Task StartAudioVideoShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, null, null);
                if (!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            // Given
            Task invitationTask = m_communication.StartAudioVideoAsync("Test subject", "sip:user@example.com", "https://example.com/callback");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_AudioVideoInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartAudioVideoShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartAudioVideoAsync("Test subject", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }
    }
}

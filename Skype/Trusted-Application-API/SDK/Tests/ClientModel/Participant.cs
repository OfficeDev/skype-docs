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
    /// Unit tests for <see cref="Participant"/>.
    /// </summary>
    [TestClass]
    public class ParticipantTests
    {
        private LoggingContext m_loggingContext;
        private Mock<IEventChannel> m_mockEventChannel;
        private MockRestfulClient m_restfulClient;
        private IConversation m_conversation;

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

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_mockEventChannel);
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_ConversationConnected.json", m_mockEventChannel);
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_ParticipantAdded.json", m_mockEventChannel);
            };

            IMessagingInvitation invitation = await applicationEndpoint.Application.Communication
                .StartMessagingAsync("Test subject", new SipUri("sip:user@example.com"), "https://example.com/callback", m_loggingContext)
                .ConfigureAwait(false);

            m_conversation = invitation.RelatedConversation;
        }

        [TestMethod]
        public void UriPropertyShouldBePopultated()
        {
            Assert.AreEqual("sip:participant@example.com", m_conversation.Participants[0].Uri);
        }

        [TestMethod]
        public void UriPropertyShouldGetUpdatedOnEvent()
        {
            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantUpdated.json");

            // Then
            Assert.AreEqual("sip:updated@example.com", m_conversation.Participants[0].Uri);
        }

        [TestMethod]
        public void NamePropertyShouldBePopulated()
        {
            Assert.AreEqual("Original Name", m_conversation.Participants[0].Name);
        }

        [TestMethod]
        public void NamePropertyShouldGetUpdatedOnEvent()
        {
            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantUpdated.json");

            // Then
            Assert.AreEqual("Updated Name", m_conversation.Participants[0].Name);
        }

        [TestMethod]
        public void ShouldReturnParticipantMessagingIfAvailable()
        {
            // Given
            Assert.IsNull(m_conversation.Participants[0].ParticipantMessaging);

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantMessagingAdded.json");

            // Then
            Assert.IsNotNull(m_conversation.Participants[0].ParticipantMessaging);
        }

        [TestMethod]
        public void ShouldReturnNullParticipantMessagingIfModalityNotAvailable()
        {
            Assert.IsNull(m_conversation.Participants[0].ParticipantMessaging);
        }

        [TestMethod]
        public void ShouldRaiseParticipantModalityChangedEventWhenParticipantMessagingIsAdded()
        {
            // Given
            var eventReceived = false;
            m_conversation.Participants[0].HandleParticipantModalityChange += (sender, args) => { if (args.AddedModalities.Count == 1) eventReceived = true; };

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantMessagingAdded.json");

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void ShouldRaiseParticipantModalityChangedEventWhenParticipantMessagingIsRemoved()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantMessagingAdded.json");
            var eventReceived = false;
            m_conversation.Participants[0].HandleParticipantModalityChange += (sender, args) => { if (args.RemovedModalities.Count == 1) eventReceived = true; };

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantMessagingDeleted.json");

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void ShouldSupportEjectIfLinkAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantUpdated_WithEjectLink.json");

            // When
            var supports = m_conversation.Participants[0].Supports(ParticipantCapability.Eject);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public void ShouldNotSupportEjectIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            var supports = m_conversation.Participants[0].Supports(ParticipantCapability.Eject);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task EjectAsyncShouldThrowIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            await m_conversation.Participants[0].EjectAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task EjectAsyncShouldMakeHttpRequest()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantUpdated_WithEjectLink.json");

            // When
            await m_conversation.Participants[0].EjectAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.EjectParticipant));
        }

        [TestMethod]
        public async Task EjectAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ParticipantUpdated_WithEjectLink.json");

            // When
            await m_conversation.Participants[0].EjectAsync(null).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.EjectParticipant));
        }
    }
}

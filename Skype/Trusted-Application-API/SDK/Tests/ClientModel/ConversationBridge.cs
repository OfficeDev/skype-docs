using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class ConversationBridgeTests
    {
        private LoggingContext m_loggingContext;
        private MockRestfulClient m_restfulClient;
        private Mock<IEventChannel> m_eventChannel;
        private IConversationBridge m_conversationBridge;

        [TestInitialize]
        public async void TestSetup()
        {
            m_loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            m_restfulClient = data.RestfulClient;
            m_eventChannel = data.EventChannel;

            await data.ApplicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await data.ApplicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);

            var communication = data.ApplicationEndpoint.Application.Communication;

            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);

            // Start a conversation with messaging modality
            IMessagingInvitation invitation = await communication
                .StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), "https://example.com/callback")
                .ConfigureAwait(false);

            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationBridgeAdded.json");

            m_conversationBridge = invitation.RelatedConversation.ConversationBridge;
        }

        [TestMethod]
        public void ShouldExposeBridgedParticipantsList()
        {
            Assert.IsNotNull(m_conversationBridge.BridgedParticipants);
        }

        [TestMethod]
        public void BridgedParticipantsShouldReturnEmptyListIfNoParticipants()
        {
            Assert.AreEqual(0, m_conversationBridge.BridgedParticipants.Count);
        }

        [TestMethod]
        public void BridgedParticipantsListShouldGetUpdatedOnBridgedParticipantAddedEvent()
        {
            // Given
            Assert.AreEqual(0, m_conversationBridge.BridgedParticipants.Count);

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_BridgedParticipantAdded.json");

            // Then
            Assert.AreEqual(1, m_conversationBridge.BridgedParticipants.Count);
        }

        /// <summary>
        /// BridgedParticipants should create a new list and copy all participants to it before returning it.
        /// This guarantees that if the calling application decides to modify the returned list,
        /// SDK's internal state/list doesn't get changed.
        /// </summary>
        [TestMethod]
        public void BridgedParticipantsListShouldReturnANewList()
        {
            // Given
            var oldList = m_conversationBridge.BridgedParticipants;

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_BridgedParticipantAdded.json");
            var newList = m_conversationBridge.BridgedParticipants;

            // Then
            Assert.IsFalse(ReferenceEquals(oldList, newList));
            Assert.AreEqual(0, oldList.Count);
            Assert.AreEqual(1, newList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task AddBridgedParticipantAsyncShouldThrowIfLinkNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationBridgeUpdated_NoActionLink.json");

            // When
            await m_conversationBridge.AddBridgedParticipantAsync("Example User", new SipUri("sip:user@example.com"), true, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddBridgedParticipantAsyncShouldThrowOnNullSipUri()
        {
            // Given
            // Setup

            // When
            await m_conversationBridge.AddBridgedParticipantAsync("Example User", null, true, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task AddBridgedParticipantAsyncShouldMakeHttpRequest()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.BridgedParticipants, HttpMethod.Post, "Event_BridgedParticipantAdded.json", m_eventChannel);

            // When
            await m_conversationBridge.AddBridgedParticipantAsync("Example User", new SipUri("sip:bridgedparticipant@example.com"), true, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.BridgedParticipants));
        }

        [TestMethod]
        public async Task AddBridgedParticipantAsyncShouldReturnATaskToWaitForParticipantAddedEvent()
        {
            // Given
            Task participantTask = m_conversationBridge.AddBridgedParticipantAsync("Example User", new SipUri("sip:bridgedparticipant@example.com"), true, m_loggingContext);
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(participantTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_BridgedParticipantAdded.json");

            // Then
            Assert.IsTrue(participantTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task AddBridgedParticipantAsyncShouldThrowIfParticipantAddedEventNotReceived()
        {
            // Given
            // No bridged participant event to be delivered
            ((ConversationBridge)m_conversationBridge).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_conversationBridge.AddBridgedParticipantAsync("Example User", new SipUri("sip:bridgedparticipant@example.com"), true, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task AddBridgedParticipantAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.BridgedParticipants, HttpMethod.Post, "Event_BridgedParticipantAdded.json", m_eventChannel);

            // When
            await m_conversationBridge.AddBridgedParticipantAsync("Example User", new SipUri("sip:bridgedparticipant@example.com"), true, null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public void ShouldSupportAddBridgedParticipantIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_conversationBridge.Supports(ConversationBridgeCapability.AddBridgedParticipant);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public void ShouldNotSupportAddBridgedParticipantIfLinkIsNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationBridgeUpdated_NoActionLink.json");

            // When
            bool supported = m_conversationBridge.Supports(ConversationBridgeCapability.AddBridgedParticipant);

            // Then
            Assert.IsFalse(supported);
        }
    }
}

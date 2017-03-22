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
    public class ConversationConferenceTests
    {
        private LoggingContext m_loggingContext;
        private MockRestfulClient m_restfulClient;
        private Mock<IEventChannel> m_eventChannel;
        private IConversationConference m_conversationConference;

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

            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenceAdded.json");
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced.json");

            m_conversationConference = invitation.RelatedConversation.ConversationConference;
        }

        [TestMethod]
        public void ShouldExposeOnlineMeetingUri()
        {
            Assert.AreEqual("sip:BL20R04meet48@apacmeetings.lync.com;gruu;opaque=app:conf:focus:id:8C1RSJYA", m_conversationConference.OnlineMeetingUri);
        }

        [TestMethod]
        public void OnlineMeetingUriShouldGetUpdatedOnUpdateEvent()
        {
            // Given
            // Setup

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenceUpdated.json");

            // Then
            Assert.AreEqual("sip:BL20R04meet49@apacmeetings.lync.com;gruu;opaque=app:conf:focus:id:8C1RSJYA", m_conversationConference.OnlineMeetingUri);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task TerminateAsyncShouldThrowIfLinkNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenceUpdated.json");

            // When
            await m_conversationConference.TerminateAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TerminateAsyncShouldMakeHttpRequest()
        {
            // Given
            // Setup

            // When
            await m_conversationConference.TerminateAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.TerminateConversationConference));
        }

        [TestMethod]
        public async Task TerminateAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            // Setup

            // When
            await m_conversationConference.TerminateAsync(null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public void ShouldSupportTerminateIfLinkIsAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_conversationConference.Supports(ConversationConferenceCapability.Terminate);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public void ShouldNotSupportTerminateIfLinkIsNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenceUpdated.json");

            // When
            bool supported = m_conversationConference.Supports(ConversationConferenceCapability.Terminate);

            // Then
            Assert.IsFalse(supported);
        }
    }
}

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
    public class BridgedParticipantTests
    {
        private LoggingContext m_loggingContext;
        private MockRestfulClient m_restfulClient;
        private Mock<IEventChannel> m_eventChannel;
        private IBridgedParticipant m_bridgedParticipant;

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
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_BridgedParticipantAdded.json");

            m_bridgedParticipant = invitation.RelatedConversation.ConversationBridge.BridgedParticipants[0];
        }

        [TestMethod]
        public async Task UpdateAsyncShouldMakeHttpRequest()
        {
            // Given
            // Setup

            // When
            await m_bridgedParticipant.UpdateAsync("New display name", false, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("PUT " + DataUrls.BridgedParticipant));
        }

        [TestMethod]
        public async Task UpdateAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            // Setup

            // When
            await m_bridgedParticipant.UpdateAsync("New display name", false, null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class InvitationTests
    {
        private Invitation<InvitationResource, TestCapabilities> m_invitation;
        private Conversation m_conversation;
        private ApplicationResource m_applicationResource;
        private EventContext m_eventContext;

        [TestInitialize]
        public void TestSetup()
        {
            var baseUri = new Uri("https://example.com/");
            var resourceUri = new Uri("https://example.com/invitation/1");
            var restfulClient = new MockRestfulClient();
            var parent = new object();
            Logger.RegisterLogger(new ConsoleLogger());

            var applicationsResource = new ApplicationsResource(resourceUri.ToString());
            m_applicationResource = new ApplicationResource(resourceUri.ToString());
            var communicationResource = new CommunicationResource(resourceUri.ToString());
            var conversationResource = new ConversationResource(resourceUri.ToString());
            var invitationResource = new InvitationResource(resourceUri.ToString())
            {
                Application = m_applicationResource
            };

            var applications = new Applications(restfulClient, applicationsResource, baseUri, resourceUri, parent);
            var application = new Application(restfulClient, m_applicationResource, baseUri, resourceUri, applications);
            var communication = new Communication(restfulClient, communicationResource, baseUri, resourceUri, application);
            m_conversation = new Conversation(restfulClient, conversationResource, baseUri, resourceUri, communication);

            var mockInvitation = new Mock<Invitation<InvitationResource, TestCapabilities>>(restfulClient, invitationResource, baseUri, resourceUri, communication);
            mockInvitation.CallBase = true;

            m_invitation = mockInvitation.Object;

            // Resource deserealization understands only those resources which are registered. This means we cannot use mock
            // implementation of InvitationResource to raise any event. Raise an event for MessagingCall instead.
            EventsEntity eventsEntity = TestHelper.GetEventsEntityForEventsInFile("Event_MessagingInvitationCompleted.json");

            m_eventContext = new EventContext()
            {
                BaseUri = baseUri,
                EventFullHref = resourceUri,
                EventEntity = eventsEntity.Senders[0].Events[0]
            };
        }

        [TestMethod]
        public void ShouldExposeRelatedConversationProperty()
        {
            // When
            ((IInvitationWithConversation)m_invitation).SetRelatedConversation(m_conversation);

            // Then
            Assert.IsTrue(ReferenceEquals(m_conversation, m_invitation.RelatedConversation));
        }

        [TestMethod]
        public async Task WaitForInviteCompleteAsyncShouldReturnATaskWhichCompletesOnInvitationCompletedEvent()
        {
            // When
            ((IInvitationWithConversation)m_invitation).SetRelatedConversation(m_conversation);

            // Given
            Task<IConversation> invitationTask = m_invitation.WaitForInviteCompleteAsync();
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);

            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            m_invitation.HandleResourceEvent(m_eventContext);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
            Assert.IsTrue(invitationTask.Result != null);
        }
    }
}

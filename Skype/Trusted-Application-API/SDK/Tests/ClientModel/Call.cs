using System;
using System.Threading;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class CallTests
    {
        private Call<MessagingResource, Invitation<InvitationResource, TestCapabilities>, TestCapabilities> m_call;
        private EventContext m_eventContext;

        [TestInitialize]
        public void TestSetup()
        {
            // Create mock implementation of Call
            var callUri = new Uri("https://example.com/resources/call/1");
            var restfulClient = new MockRestfulClient();

            var resource = new MessagingResource(callUri.ToString());
            var baseUri = new Uri("https://example.com/");
            var parent = new object();
            Logger.RegisterLogger(new ConsoleLogger());

            var mockCall = new Mock<Call<MessagingResource, Invitation<InvitationResource, TestCapabilities>, TestCapabilities>>
                (restfulClient, resource, baseUri, callUri, parent);
            mockCall.CallBase = true;

            m_call = mockCall.Object;

            // Resource deserealization understands only those resources which are registered. This means we cannot use mock
            // implementation of MessagingResource to raise any event. Raise event for MessagingCall instead.
            EventsEntity eventsEntity = TestHelper.GetEventsEntityForEventsInFile("Event_MessagingConnected.json");

            m_eventContext = new EventContext()
            {
                BaseUri = baseUri,
                EventFullHref = callUri,
                EventEntity =  eventsEntity.Senders[0].Events[0]
            };
        }

        [TestMethod]
        public void CallStateShouldBeDisconnectedByDefault()
        {
            Assert.AreEqual(CallState.Disconnected, m_call.State);
        }

        [TestMethod]
        public void CallStateShouldGetUpdatedOnUpdateEvent()
        {
            // Given
            Assert.AreEqual(CallState.Disconnected, m_call.State);

            // When
            m_call.HandleResourceEvent(m_eventContext);

            // Then
            Assert.AreEqual(CallState.Connected, m_call.State);
        }

        [TestMethod]
        public void ShouldRaiseCallStateChangedEventWhenUpdated()
        {
            // Given
            var eventReceived = false;
            m_call.CallStateChanged += (sender, args) => eventReceived = true;

            // When
            m_call.HandleResourceEvent(m_eventContext);

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void ShouldRaiseCallStateChangedEventOnlyOnceIfSameHandlerRegisteredMultipleTimes()
        {
            // Given
            var eventsReceived = 0;
            EventHandler<CallStateChangedEventArgs> handler = (sender, args) => Interlocked.Increment(ref eventsReceived);
            m_call.CallStateChanged += handler;
            m_call.CallStateChanged += handler;

            // When
            m_call.HandleResourceEvent(m_eventContext);

            // Then
            Assert.AreEqual(1, eventsReceived);
        }

        [TestMethod]
        public void ShouldRaiseCallStateChangedEventForAllRegisteredEventHandlersInOrder()
        {
            // Given
            var eventsReceived = 0;
            var lastEvent = 0;
            m_call.CallStateChanged += (sender, args) => { ++eventsReceived; lastEvent = 1; };
            m_call.CallStateChanged += (sender, args) => { ++eventsReceived; lastEvent = 2; };

            // When
            m_call.HandleResourceEvent(m_eventContext);

            // Then
            Assert.AreEqual(2, eventsReceived);
            Assert.AreEqual(2, lastEvent);
        }
    }
}

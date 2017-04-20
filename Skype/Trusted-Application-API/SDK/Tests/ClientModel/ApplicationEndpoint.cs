using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests
{
    [TestClass]
    public class ApplicationEndpointTests
    {
        [TestMethod]
        public void CanCreateMultipleApplicationEndpoints()
        {
            // Given

            // When
            ApplicationEndpoint applicationEndpoint1 = TestHelper.CreateApplicationEndpoint().ApplicationEndpoint;
            ApplicationEndpoint applicationEndpoint2 = TestHelper.CreateApplicationEndpoint().ApplicationEndpoint;

            // Then
            Assert.IsFalse(ReferenceEquals(applicationEndpoint1, applicationEndpoint2));
        }

        [TestMethod]
        public async Task InitializingApplicationEndpointStartsEventChannel()
        {
            // Given
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            var loggingContext = new LoggingContext(Guid.NewGuid());
            mockEventChannel.Verify(foo => foo.TryStartAsync(), Times.Never);

            // When
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);

            // Then
            mockEventChannel.Verify(foo => foo.TryStartAsync(), Times.Once);
        }

        [TestMethod]
        public void ApplicationEndpointSubscribesToEventChannelBeforeStartingIt()
        {
            // Given
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var callbackHttpRequest = TestHelper.GetEventsChannelArgs("Event_Empty.json").CallbackHttpRequest;
            var data = TestHelper.CreateApplicationEndpoint();

            // We don't have a clean way of checking if the event is consumed by ApplicationEndpoint.
            // In this hacky way we raise an event with mock event args, if someone reads the CallbackHttpRequest
            // property, we can be sure that the event was consumed.
            var propertyAccessed = false;
            var mockEventArgs = new Mock<EventsChannelArgs>(callbackHttpRequest);
            mockEventArgs
                .Setup(mock => mock.CallbackHttpRequest)
                .Returns(callbackHttpRequest)
                .Callback(() => propertyAccessed = true);

            // When
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            mockEventChannel.Raise(mock => mock.HandleIncomingEvents += null, mockEventChannel.Object, mockEventArgs.Object);

            // Then
            mockEventChannel.Verify(foo => foo.TryStartAsync(), Times.Never);
            Assert.IsTrue(propertyAccessed, "Event was not consumed by ApplicationEndpoint. This indicates that event handler was not registered.");
        }

        [TestMethod]
        public async Task InitializeApplicationAsyncShouldMakeHttpRequest()
        {
            // Given
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);

            // When
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(applicationEndpoint.Application);
            Assert.IsTrue(data.RestfulClient.RequestsProcessed("GET " + DataUrls.Application));
        }

        [TestMethod]
        public async Task InitializeApplicationAsyncWorksWithNullLoggingContext()
        {
            // Given
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);

            // When
            await applicationEndpoint.InitializeApplicationAsync(null).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(applicationEndpoint.Application);
            Assert.IsTrue(data.RestfulClient.RequestsProcessed("GET " + DataUrls.Application));
        }

        [TestMethod]
        public async Task InitializeAsyncWorksWithNullLoggingContext()
        {
            // Given
            var data = TestHelper.CreateApplicationEndpoint();
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;

            // When
            await applicationEndpoint.InitializeAsync(null).ConfigureAwait(false);

            // Then
            // No exception
        }

        [TestMethod]
        public async Task InitializeApplicationAsyncShouldPopulateApplicationProperty()
        {
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);

            // Given
            Assert.IsNull(applicationEndpoint.Application);

            // When
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(applicationEndpoint.Application);
        }

        [TestMethod]
        public void ShouldExposeApplicationEndpointId()
        {
            // when
            var data = TestHelper.CreateApplicationEndpoint();

            // Then
            Assert.AreEqual(TestHelper.ApplicationEndpointUri, data.ApplicationEndpoint.ApplicationEndpointId);
        }

        [TestMethod]
        public void ShouldExposeClientPlatform()
        {
            // when
            var data = TestHelper.CreateApplicationEndpoint();

            // Then
            Assert.IsTrue(ReferenceEquals(data.ClientPlatform, data.ApplicationEndpoint.ClientPlatform));
        }

        [TestMethod]
        public async Task ShouldRaiseIncomingIMInvite()
        {
            // Given
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            var callReceived = false;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            applicationEndpoint.HandleIncomingInstantMessagingCall += (sender, args) => callReceived = true;

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingIMCall.json");

            // Then
            Assert.IsTrue(callReceived, "IM call not raised by SDK to the application.");
        }

        [TestMethod]
        public async Task ShouldRaiseIncomingIMInviteOnceIfSameEventHandlerRegisteredMultipleTimes()
        {
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Given
            var eventsReceived = 0;
            EventHandler<IncomingInviteEventArgs<IMessagingInvitation>> handler = (sender, args) => Interlocked.Increment(ref eventsReceived);
            applicationEndpoint.HandleIncomingInstantMessagingCall += handler;
            applicationEndpoint.HandleIncomingInstantMessagingCall += handler;

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingIMCall.json");

            // Then
            Assert.AreEqual(1, eventsReceived);
        }

        [TestMethod]
        public async Task ShouldRaiseIncomingIMInviteToAllRegisteredEventHandlersInOrder()
        {
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Given
            var eventsReceived = 0;
            var lastEvent = 0;

            applicationEndpoint.HandleIncomingInstantMessagingCall += (sender, args) => { ++eventsReceived; lastEvent = 1; };
            applicationEndpoint.HandleIncomingInstantMessagingCall += (sender, args) => { ++eventsReceived; lastEvent = 2; };

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingIMCall.json");

            // Then
            Assert.AreEqual(2, eventsReceived);
            Assert.AreEqual(2, lastEvent);
        }

        [TestMethod]
        public async Task ShouldNotThrowOnIncomingIMInviteIfNoEventHandlerRegistered()
        {
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Given
            // No event handler registered at applicationEndpoint.HandleIncomingInstantMessagingCall

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingIMCall.json");

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task ShouldRaiseIncomingAudioVideoInvite()
        {
            // Given
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            var callReceived = false;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            applicationEndpoint.HandleIncomingAudioVideoCall += (sender, args) => callReceived = true;

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingAudioCall.json");

            // Then
            Assert.IsTrue(callReceived, "AudioVideo call not raised by SDK to the application.");
        }

        [TestMethod]
        public async Task ShouldRaiseIncomingAudioVideoInviteOnceIfSameEventHandlerRegisteredMultipleTimes()
        {
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Given
            var eventsReceived = 0;
            EventHandler<IncomingInviteEventArgs<IAudioVideoInvitation>> handler = (sender, args) => Interlocked.Increment(ref eventsReceived);
            applicationEndpoint.HandleIncomingAudioVideoCall += handler;
            applicationEndpoint.HandleIncomingAudioVideoCall += handler;

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingAudioCall.json");

            // Then
            Assert.AreEqual(1, eventsReceived);
        }

        [TestMethod]
        public async Task ShouldRaiseIncomingAudioVideoInviteToAllRegisteredEventHandlersInOrder()
        {
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Given
            var eventsReceived = 0;
            var lastEvent = 0;

            applicationEndpoint.HandleIncomingAudioVideoCall += (sender, args) => { ++eventsReceived; lastEvent = 1; };
            applicationEndpoint.HandleIncomingAudioVideoCall += (sender, args) => { ++eventsReceived; lastEvent = 2; };

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingAudioCall.json");

            // Then
            Assert.AreEqual(2, eventsReceived);
            Assert.AreEqual(2, lastEvent);
        }

        [TestMethod]
        public async Task ShouldNotThrowOnIncomingAudioVideoInviteIfNoEventHandlerRegistered()
        {
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Given
            // No event handler registered for applicationEndpoint.HandleIncomingAudioVideoCall

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingAudioCall.json");

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task ShouldStopEventChannelOnUnitialization()
        {
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            await applicationEndpoint.InitializeAsync(new LoggingContext(Guid.NewGuid())).ConfigureAwait(false);

            // When
            applicationEndpoint.Uninitialize();

            // Then
            mockEventChannel.Verify(foo => foo.TryStopAsync(), Times.Once);
        }

        [TestMethod]
        public async Task ShoulNotRaiseIncomingMessagingInvitationToUnregisteredHandler()
        {
            // Given
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            var callReceived = false;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            EventHandler<IncomingInviteEventArgs<IMessagingInvitation>> handler = (sender, args) => callReceived = true;
            applicationEndpoint.HandleIncomingInstantMessagingCall += handler;

            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingIMCall.json");
            Assert.IsTrue(callReceived, "IM call not raised by SDK.");
            callReceived = false;

            // When
            applicationEndpoint.HandleIncomingInstantMessagingCall -= handler;
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingIMCall.json");

            // Then
            Assert.IsFalse(callReceived, "IM call raised by SDK to an unregistered event handler.");
        }

        [TestMethod]
        public async Task ShoulNotRaiseIncomingAudioVideoInvitationToUnregisteredHandler()
        {
            // Given
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            var callReceived = false;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            EventHandler<IncomingInviteEventArgs<IAudioVideoInvitation>> handler = (sender, args) => callReceived = true;
            applicationEndpoint.HandleIncomingAudioVideoCall += handler;

            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingAudioCall.json");
            Assert.IsTrue(callReceived, "AudioVideo call not raised by SDK.");
            callReceived = false;

            // When
            applicationEndpoint.HandleIncomingAudioVideoCall -= handler;

            // Then
            Assert.IsFalse(callReceived, "AudioVideo call raised by SDK to an unregistered event handler.");
        }

        [TestMethod]
        public async Task ShouldHandleIncomingEventWithMalformedBaseUri()
        {
            // Given
            var loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            Mock<IEventChannel> mockEventChannel = data.EventChannel;
            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;

            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingIMCall_MalformedBaseUri.json");

            // When
            TestHelper.RaiseEventsFromFile(mockEventChannel, "Event_IncomingIMCall.json");

            // Then
            // No exception is thrown
        }
    }
}

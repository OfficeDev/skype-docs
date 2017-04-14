using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class BasePlatformResourceTests
    {
        private BasePlatformResource<Resource, TestCapabilities> m_platformResource;
        private Uri m_baseUri;
        private Uri m_resourceUri;
        private object m_parent;
        private LoggingContext m_loggingContext;
        private MockRestfulClient m_restfulClient;
        private EventContext m_eventContext;

        [TestInitialize]
        public void TestSetup()
        {
            // We will have mock implementation of both Microsoft.Rtc.Internal.RestAPI.ResourceModel.Resource and 
            // Microsoft.SfB.PlatformService.SDK.ClientModel.BasePlatformResource
            m_resourceUri = new Uri("https://example.com/resources/1");
            m_restfulClient = new MockRestfulClient();
            m_restfulClient.OverrideResponse(m_resourceUri, HttpMethod.Get, HttpStatusCode.OK, null);
            m_restfulClient.OverrideResponse(m_resourceUri, HttpMethod.Delete, HttpStatusCode.NoContent, null);

            var resource = new Mock<Resource>(m_resourceUri.ToString());
            m_baseUri = new Uri("https://example.com/");
            m_parent = new object();
            m_loggingContext = new LoggingContext();
            Logger.RegisterLogger(new ConsoleLogger());

            var mockPlatformResource = new Mock<BasePlatformResource<Resource, TestCapabilities>>(m_restfulClient, resource.Object, m_baseUri, m_resourceUri, m_parent);
            mockPlatformResource.CallBase = true;

            m_platformResource = mockPlatformResource.Object;

            m_eventContext = new EventContext()
            {
                BaseUri = m_baseUri,
                EventFullHref = m_resourceUri,
                EventEntity = new EventEntity()
                {
                    Relationship = EventOperation.Added
                }
            };
        }

        [TestMethod]
        public void ShouldExposeBaseUri()
        {
            Assert.AreEqual(m_baseUri, m_platformResource.BaseUri);
        }

        [TestMethod]
        public void ShouldExposeResourceUri()
        {
            Assert.AreEqual(m_resourceUri, m_platformResource.ResourceUri);
        }

        [TestMethod]
        public void ShouldExposeParentObject()
        {
            Assert.IsTrue(object.ReferenceEquals(m_parent, m_platformResource.Parent));
        }

        [TestMethod]
        public async Task RefreshAsyncShouldMakeHttpRequest()
        {
            // When
            await m_platformResource.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("GET " + m_resourceUri.ToString()));
        }

        [TestMethod]
        public async Task RefreshAsyncShouldWorkWithNullLoggingContext()
        {
            // When
            await m_platformResource.RefreshAsync(null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task RefreshAsyncShouldRaiseResourceUpdatedEvent()
        {
            // Given
            var eventReceived = false;
            m_platformResource.HandleResourceUpdated += (sender, args) => eventReceived = true;

            // When
            await m_platformResource.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public async Task RefreshAsyncShouldRaiseResourceUpdatedEventForAllRegisteredEventHandlersInOrder()
        {
            // Given
            var eventsReceived = 0;
            var lastEvent = 0;
            m_platformResource.HandleResourceUpdated += (sender, args) => { ++eventsReceived; lastEvent = 1; };
            m_platformResource.HandleResourceUpdated += (sender, args) => { ++eventsReceived; lastEvent = 2; };

            // When
            await m_platformResource.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.AreEqual(2, eventsReceived);
            Assert.AreEqual(2, lastEvent);
        }

        [TestMethod]
        public async Task DeleteAsyncShouldMakeHttpRequest()
        {
            // When
            await m_platformResource.DeleteAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("DELETE " + m_resourceUri.ToString()));
        }

        [TestMethod]
        public async Task DeleteAsyncShouldWorkWithNullLoggingContext()
        {
            // When
            await m_platformResource.DeleteAsync(null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task DeleteAsyncShouldRaiseResourceRemovedEvent()
        {
            // Given
            var eventReceived = false;
            m_platformResource.HandleResourceRemoved += (sender, args) => eventReceived = true;

            // When
            await m_platformResource.DeleteAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void HandleResourceEventShouldRaiseUpdatedEvent()
        {
            // Given
            var eventReceived = false;
            m_platformResource.HandleResourceUpdated += (sender, args) => eventReceived = true;
            m_eventContext.EventEntity.Relationship = EventOperation.Updated;

            // When
            m_platformResource.HandleResourceEvent(m_eventContext);

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void HandleResourceEventShouldRaiseUpdatedEventForAllRegisteredEventHandlersInOrder()
        {
            // Given
            var eventsReceived = 0;
            var lastEvent = 0;
            m_platformResource.HandleResourceUpdated += (sender, args) => { ++eventsReceived; lastEvent = 1; };
            m_platformResource.HandleResourceUpdated += (sender, args) => { ++eventsReceived; lastEvent = 2; };
            m_eventContext.EventEntity.Relationship = EventOperation.Updated;

            // When
            m_platformResource.HandleResourceEvent(m_eventContext);

            // Then
            Assert.AreEqual(2, eventsReceived);
            Assert.AreEqual(2, lastEvent);
        }

        [TestMethod]
        public void HandleResourceEventShouldRaiseResourceCompletedEvent()
        {
            // Given
            var eventReceived = false;
            m_platformResource.HandleResourceCompleted += (sender, args) => eventReceived = true;
            m_eventContext.EventEntity.Relationship = EventOperation.Completed;

            // When
            m_platformResource.HandleResourceEvent(m_eventContext);

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void HandleResourceEventShouldRaiseResourceCompletedEventForAllRegisteredEventHandlersInOrder()
        {
            // Given
            var eventsReceived = 0;
            var lastEvent = 0;
            m_platformResource.HandleResourceCompleted += (sender, args) => { ++eventsReceived; lastEvent = 1; };
            m_platformResource.HandleResourceCompleted += (sender, args) => { ++eventsReceived; lastEvent = 2; };
            m_eventContext.EventEntity.Relationship = EventOperation.Completed;

            // When
            m_platformResource.HandleResourceEvent(m_eventContext);

            // Then
            Assert.AreEqual(2, eventsReceived);
            Assert.AreEqual(2, lastEvent);
        }

        [TestMethod]
        public void HandleResourceEventShouldRaiseResourceRemovedEvent()
        {
            // Given
            var eventReceived = false;
            m_platformResource.HandleResourceRemoved += (sender, args) => eventReceived = true;
            m_eventContext.EventEntity.Relationship = EventOperation.Deleted;

            // When
            m_platformResource.HandleResourceEvent(m_eventContext);

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void HandleResourceEventShouldRaiseResourceRemovedEventForAllRegisteredEventHandlersInOrder()
        {
            // Given
            var eventsReceived = 0;
            var lastEvent = 0;
            m_platformResource.HandleResourceRemoved += (sender, args) => { ++eventsReceived; lastEvent = 1; };
            m_platformResource.HandleResourceRemoved += (sender, args) => { ++eventsReceived; lastEvent = 2; };
            m_eventContext.EventEntity.Relationship = EventOperation.Deleted;

            // When
            m_platformResource.HandleResourceEvent(m_eventContext);

            // Then
            Assert.AreEqual(2, eventsReceived);
            Assert.AreEqual(2, lastEvent);
        }
    }

    public enum TestCapabilities
    {
    }
}

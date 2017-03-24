using System;
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
    public class AdhocMeetingTests
    {
        private IApplication m_application;
        private LoggingContext m_loggingContext;
        private MockRestfulClient m_restfulClient;
        private IAdhocMeeting m_adhocMeeting;
        private ClientPlatformSettings m_clientPlatformSettings;
        private Mock<IEventChannel> m_mockEventChannel;

        [TestInitialize]
        public async void TestSetup()
        {
            TestHelper.InitializeTokenMapper();

            m_loggingContext = new LoggingContext(Guid.NewGuid());

            var data = TestHelper.CreateApplicationEndpoint();
            m_restfulClient = data.RestfulClient;
            m_clientPlatformSettings = data.ClientPlatformSettings;
            m_mockEventChannel = data.EventChannel;

            await data.ApplicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await data.ApplicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);
            m_application = data.ApplicationEndpoint.Application;

            m_adhocMeeting = await m_application.CreateAdhocMeetingAsync(new AdhocMeetingCreationInput("subject"), m_loggingContext).ConfigureAwait(false);
        }

        [TestMethod]
        public void ShouldExposeJoinUrl()
        {
            // Given
            // Setup

            // Then
            Assert.IsNotNull(m_adhocMeeting.JoinUrl);
        }

        [TestMethod]
        public void ShouldExposeOnlineMeetingUri()
        {
            // Given
            // Setup

            // Then
            Assert.IsNotNull(m_adhocMeeting.OnlineMeetingUri);
        }

        [TestMethod]
        public void ShouldExposeSubject()
        {
            // Given
            // Setup

            // Then
            Assert.IsNotNull(m_adhocMeeting.Subject);
        }

        [TestMethod]
        public void ShouldSupportJoinAdhocMeetingWhenLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_adhocMeeting.Supports(AdhocMeetingCapability.JoinAdhocMeeting);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportJoinAdhocMeetingWhenLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AdhocMeeting), HttpMethod.Post, HttpStatusCode.Created, "AdhocMeeting_NoJoinAdhocMeetingLink.json");
            m_adhocMeeting = await m_application.CreateAdhocMeetingAsync(new AdhocMeetingCreationInput("subject"), m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_adhocMeeting.Supports(AdhocMeetingCapability.JoinAdhocMeeting);

            // Then
            Assert.IsFalse(supports);
        }
    }
}

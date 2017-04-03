using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class ApplicationTests
    {
        private LoggingContext m_loggingContext;
        private IApplication m_application;
        private MockRestfulClient m_restfulClient;

        [TestInitialize]
        public async void TestSetup()
        {
            m_restfulClient = new MockRestfulClient();
            Logger.RegisterLogger(new ConsoleLogger());
            m_loggingContext = new LoggingContext(Guid.NewGuid());
            TestHelper.InitializeTokenMapper();

            Uri discoverUri = TestHelper.DiscoverUri;
            Uri baseUri = UriHelper.GetBaseUriFromAbsoluteUri(discoverUri.ToString());
            SipUri ApplicationEndpointId = TestHelper.ApplicationEndpointUri;

            var discover = new Discover(m_restfulClient, baseUri, discoverUri, this);
            await discover.RefreshAndInitializeAsync(ApplicationEndpointId.ToString(), m_loggingContext).ConfigureAwait(false);

            m_application = discover.Application;
        }

        [TestMethod]
        public async Task InitializationShouldPopulateCommunication()
        {
            // Given
            Assert.IsNull(m_application.Communication);
            Assert.IsFalse(m_restfulClient.RequestsProcessed("GET "+DataUrls.Application));

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(m_application.Communication);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("GET "+ DataUrls.Application));
        }

        [TestMethod]
        public async Task ShouldSupportGetAnonApplicationTokenIfUrlAvailable()
        {
            // Given

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_application.Supports(ApplicationCapability.GetAnonApplicationToken));
        }

        [TestMethod]
        public async Task ShouldNotSupportGetAnonApplicationTokenIfUrlNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoGetAnonApplicationToken.json");

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsFalse(m_application.Supports(ApplicationCapability.GetAnonApplicationToken));
        }

        [TestMethod]
        public async Task ShouldSupportGetAnonApplicationTokenForMeetingIfUrlAvailable()
        {
            // Given

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_application.Supports(ApplicationCapability.GetAnonApplicationTokenForMeeting));
        }

        [TestMethod]
        public async Task ShouldNotSupportGetAnonApplicationTokenForMeetingIfUrlNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoGetAnonApplicationToken.json");

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsFalse(m_application.Supports(ApplicationCapability.GetAnonApplicationTokenForMeeting));
        }

        [TestMethod]
        public async Task ShouldSupportGetAnonApplicationTokenForP2PCallIfUrlAvailable()
        {
            // Given

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_application.Supports(ApplicationCapability.GetAnonApplicationTokenForP2PCall));
        }

        [TestMethod]
        public async Task ShouldNotSupportGetAnonApplicationTokenForP2PCallIfUrlNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoGetAnonApplicationToken.json");

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsFalse(m_application.Supports(ApplicationCapability.GetAnonApplicationTokenForP2PCall));
        }

        [TestMethod]
        public async Task ShouldSupportGetAdhocMeetingIfResourceAvailable()
        {
            // Given
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supported = m_application.Supports(ApplicationCapability.GetAdhocMeetingResource);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public async Task ShouldNotSupportGetAdhocMeetingIfUrlNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoAdhocMeetings.json");

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsFalse(m_application.Supports(ApplicationCapability.GetAdhocMeetingResource));
        }

        [TestMethod]
        public async Task ShouldSupportCreateAdhocMeetingIfResourceAvailable()
        {
            // Given
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supported = m_application.Supports(ApplicationCapability.CreateAdhocMeeting);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public async Task ShouldNotSupportCreateAdhocMeetingIfUrlNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoAdhocMeetings.json");

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsFalse(m_application.Supports(ApplicationCapability.CreateAdhocMeeting));
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task GetAnonApplicationTokenShouldThrowIfUrlNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoGetAnonApplicationToken.json");
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);
            var input = new AnonymousApplicationTokenInput();

            // When
            await m_application.GetAnonApplicationTokenAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAnonApplicationTokenShouldThrowIfInputNull()
        {
            // Given
            AnonymousApplicationTokenInput input = null;

            // When
            await m_application.GetAnonApplicationTokenAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenShouldReturnToken()
        {
            // Given
            var input = new AnonymousApplicationTokenInput();

            // When
            AnonymousApplicationTokenResource token = await m_application.GetAnonApplicationTokenAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenShouldMakeHttpRequest()
        {
            // Given
            Assert.IsFalse(m_restfulClient.RequestsProcessed("POST " + DataUrls.AnonToken));
            var input = new AnonymousApplicationTokenInput();

            // When
            await m_application.GetAnonApplicationTokenAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AnonToken));
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenShouldWorkWithNullLoggingContext()
        {
            // Given
            var input = new AnonymousApplicationTokenInput();

            // When
            AnonymousApplicationTokenResource token = await m_application.GetAnonApplicationTokenAsync(null, input).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task GetAnonApplicationTokenShouldThrowIfServerResponseMalformed()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AnonToken), HttpMethod.Post, HttpStatusCode.OK, "AnonApplicationToken_Malformed.json");
            var input = new AnonymousApplicationTokenInput();

            // When
            await m_application.GetAnonApplicationTokenAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task GetAnonApplicationTokenForMeetingShouldThrowIfUrlNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoGetAnonApplicationToken.json");
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_application.GetAnonApplicationTokenForMeetingAsync("https://example.com/meetingurl", null, null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAnonApplicationTokenForMeetingShouldThrowIfMeetingUrlNull()
        {
            // Given
            // Setup

            // When
            await m_application.GetAnonApplicationTokenForMeetingAsync(null, "https://example.com;https://examples.com", Guid.NewGuid().ToString(), m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenForMeetingShouldReturnToken()
        {
            // Given
            // Setup

            // When
            IAnonymousApplicationToken token = await m_application.GetAnonApplicationTokenForMeetingAsync("https://example.com/meetingurl", null, null, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenForMeetingShouldMakeHttpRequest()
        {
            // Given
            Assert.IsFalse(m_restfulClient.RequestsProcessed("POST " + DataUrls.AnonToken));
            // Setup

            // When
            await m_application.GetAnonApplicationTokenForMeetingAsync("https://example.com/meetingurl", null, null, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AnonToken));
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenForMeetingShouldWorkWithNullLoggingContext()
        {
            // Given
            // Setup

            // When
            IAnonymousApplicationToken token = await m_application.GetAnonApplicationTokenForMeetingAsync("https://example.com/meetingurl", null, null, null).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task GetAnonApplicationTokenForMeetingShouldThrowIfServerResponseMalformed()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AnonToken), HttpMethod.Post, HttpStatusCode.OK, "AnonApplicationToken_Malformed.json");

            // When
            await m_application.GetAnonApplicationTokenForMeetingAsync("https://example.com/meetingurl", null, null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task GetAnonApplicationTokenForP2PCallShouldThrowIfUrlNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoGetAnonApplicationToken.json");
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_application.GetAnonApplicationTokenForP2PCallAsync(null, null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenForP2PCallShouldReturnToken()
        {
            // Given

            // When
            IAnonymousApplicationToken token = await m_application.GetAnonApplicationTokenForP2PCallAsync(null, null, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenForP2PCallShouldMakeHttpRequest()
        {
            // Given
            Assert.IsFalse(m_restfulClient.RequestsProcessed("POST " + DataUrls.AnonToken));

            // When
            await m_application.GetAnonApplicationTokenForP2PCallAsync(null, null, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AnonToken));
        }

        [TestMethod]
        public async Task GetAnonApplicationTokenForP2PCallShouldWorkWithNullLoggingContext()
        {
            // Given
            // Setup

            // When
            IAnonymousApplicationToken token = await m_application.GetAnonApplicationTokenForP2PCallAsync("https://example.com;https://examples.com", Guid.NewGuid().ToString(), null).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task GetAnonApplicationTokenForP2PCallShouldThrowIfServerResponseMalformed()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AnonToken), HttpMethod.Post, HttpStatusCode.OK, "AnonApplicationToken_Malformed.json");

            // When
            await m_application.GetAnonApplicationTokenForP2PCallAsync(null, null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task GetAdhocMeetingShouldWork()
        {
            // Given
            var input = new AdhocMeetingInput();

            // When
            AdhocMeetingResource meeting = await m_application.GetAdhocMeetingResourceAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(meeting);
        }

        [TestMethod]
        public async Task CreateAdhocMeetingShouldWork()
        {
            // Given
            var input = new AdhocMeetingCreationInput("subject");

            // When
            IAdhocMeeting meeting = await m_application.CreateAdhocMeetingAsync(input, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(meeting);
        }

        [TestMethod]
        public async Task GetAdhocMeetingShouldMakeHttpRequest()
        {
            // Given
            var input = new AdhocMeetingInput();
            Assert.IsFalse(m_restfulClient.RequestsProcessed("POST " + DataUrls.AdhocMeeting));

            // When
            await m_application.GetAdhocMeetingResourceAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AdhocMeeting), "HTTP request to create adhoc meeting wasn't sent out.");
        }

        [TestMethod]
        public async Task CreateAdhocMeetingShouldMakeHttpRequest()
        {
            // Given
            var input = new AdhocMeetingCreationInput("subject");
            Assert.IsFalse(m_restfulClient.RequestsProcessed("POST " + DataUrls.AdhocMeeting));

            // When
            await m_application.CreateAdhocMeetingAsync(input, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AdhocMeeting), "HTTP request to create adhoc meeting wasn't sent out.");
        }

        [TestMethod]
        public async Task GetAdhocMeetingShouldWorkWithNullLoggingContext()
        {
            // Given
            var input = new AdhocMeetingInput();

            // When
            await m_application.GetAdhocMeetingResourceAsync(null, input).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task CreateAdhocMeetingShouldWorkWithNullLoggingContext()
        {
            // Given
            var input = new AdhocMeetingCreationInput("subject");

            // When
            await m_application.CreateAdhocMeetingAsync(input, null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAdhocMeetingShouldThrowOnNullInput()
        {
            // Given
            AdhocMeetingInput input = null;

            // When
            await m_application.GetAdhocMeetingResourceAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAdhocMeetingShouldThrowOnNullInput()
        {
            // Given
            AdhocMeetingCreationInput input = null;

            // When
            await m_application.CreateAdhocMeetingAsync(input, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task GetAdhocMeetingShouldThrowIfAdhocMeetingResourceNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoAdhocMeetings.json");
            await m_application.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            var input = new AdhocMeetingInput();

            // When
            await m_application.GetAdhocMeetingResourceAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task CreateAdhocMeetingShouldThrowIfAdhocMeetingResourceNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoAdhocMeetings.json");
            await m_application.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            var input = new AdhocMeetingCreationInput("subject");

            // When
            await m_application.CreateAdhocMeetingAsync(input, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task GetAdhocMeetingShouldThrowIfServerResponseMalformed()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AdhocMeeting), HttpMethod.Post, HttpStatusCode.OK, "AdhocMeeting_Malformed.json");
            var input = new AdhocMeetingInput();

            // When
            await m_application.GetAdhocMeetingResourceAsync(m_loggingContext, input).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task CreateAdhocMeetingShouldThrowIfServerResponseMalformed()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AdhocMeeting), HttpMethod.Post, HttpStatusCode.OK, "AdhocMeeting_Malformed.json");
            var input = new AdhocMeetingCreationInput("subject");

            // When
            await m_application.CreateAdhocMeetingAsync(input, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task RefreshAndInitializeAsyncShouldFailIfCommunicationResourceIsNotEmbedded()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Application), HttpMethod.Get, HttpStatusCode.OK, "Application_NoCommunicationResource.json");

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task RefreshAndInitializeAsyncShouldMakeHttpRequest()
        {
            // Given
            Assert.IsFalse(m_restfulClient.RequestsProcessed("GET " + DataUrls.Application));

            // When
            await m_application.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("GET " + DataUrls.Application));
        }

        [TestMethod]
        public async Task RefreshAndInitializeAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            Assert.IsFalse(m_restfulClient.RequestsProcessed("GET " + DataUrls.Application));

            // When
            await m_application.RefreshAndInitializeAsync(null).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("GET " + DataUrls.Application));
        }
    }
}

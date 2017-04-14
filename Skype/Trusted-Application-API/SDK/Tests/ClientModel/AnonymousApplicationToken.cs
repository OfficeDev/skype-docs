using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class AnonymousApplicationTokenTests
    {
        private IApplication m_application;
        private LoggingContext m_loggingContext;
        private MockRestfulClient m_restfulClient;
        private IAnonymousApplicationToken m_anonToken;

        [TestInitialize]
        public async void TestSetup()
        {
            TestHelper.InitializeTokenMapper();

            m_loggingContext = new LoggingContext(Guid.NewGuid());

            var data = TestHelper.CreateApplicationEndpoint();
            m_restfulClient = data.RestfulClient;

            await data.ApplicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await data.ApplicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);
            m_application = data.ApplicationEndpoint.Application;

            m_anonToken = await m_application.GetAnonApplicationTokenForMeetingAsync("https://example.com/meetingjoinurl", "https://example.com;https://example1.com", Guid.NewGuid().ToString(), m_loggingContext).ConfigureAwait(false);
        }

        [TestMethod]
        public void ShouldExposeAuthToken()
        {
            // Given
            // Setup

            // When
            string authToken = m_anonToken.AuthToken;

            // Then
            Assert.IsFalse(string.IsNullOrWhiteSpace(authToken));
        }

        [TestMethod]
        public void ShouldExposeAuthTokenExpiryTime()
        {
            // Given
            // Setup

            // When
            DateTime expiry = m_anonToken.AuthTokenExpiryTime;

            // Then
            var expected = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(1488872625837);
            Assert.AreEqual(expected, expiry);
        }

        [TestMethod]
        public void ShouldExposeAnonymousApplicationsDiscoverUri()
        {
            // Given
            // Setup

            // When
            Uri discoverUri = m_anonToken.AnonymousApplicationsDiscoverUri;

            // Then
            Assert.IsNotNull(discoverUri);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task AnonymousApplicationsDiscoverUriShouldThrowIfInvalidUrlInResponse()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AnonToken), HttpMethod.Post, HttpStatusCode.OK, "AnonApplicationToken_MalformedDiscoverUri.json");
            m_anonToken = await m_application.GetAnonApplicationTokenForMeetingAsync("https://example.com/meetingjoinurl", "https://example.com;https://example1.com", Guid.NewGuid().ToString(), m_loggingContext).ConfigureAwait(false);

            // When
            Uri discoverUri = m_anonToken.AnonymousApplicationsDiscoverUri;

            // Then
            // Exception is thrown
        }
    }
}

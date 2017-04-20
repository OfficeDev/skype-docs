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
    public class ApplicationsTests
    {
        private MockRestfulClient m_restfulClient;
        private LoggingContext m_loggingContext;
        private IApplications m_applications;

        [TestInitialize]
        public void TestSetup()
        {
            Logger.RegisterLogger(new ConsoleLogger());

            m_loggingContext = new LoggingContext();

            m_restfulClient = new MockRestfulClient();
            Uri baseUri = UriHelper.GetBaseUriFromAbsoluteUri(TestHelper.DiscoverUri.ToString());

            m_applications = new Applications(m_restfulClient, null, baseUri, new Uri(DataUrls.Applications), this);
        }

        [TestMethod]
        public async Task ShouldInitializeProperly()
        {
            // Given
            Assert.IsNull(m_applications.Application);
            Assert.IsFalse(m_restfulClient.RequestsProcessed("GET " + DataUrls.Applications));

            // When
            await m_applications.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(m_applications.Application);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("GET " + DataUrls.Applications));
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task ShouldFailInitializationIfApplicationResourceNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Applications), HttpMethod.Get, HttpStatusCode.OK, "Applications_NoApplication.json");

            // When
            await m_applications.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }
    }
}
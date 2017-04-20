using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests
{
    /// <summary>
    /// Contains all the mock objects used to create ApplicationEndpoint
    /// </summary>
    internal class MockApplicationEndpointData
    {
        internal Mock<IEventChannel> EventChannel { get; set; }

        internal Mock<IRestfulClientFactory> RestfulClientFactory { get; set; }

        internal MockRestfulClient RestfulClient { get; set; }

        internal ApplicationEndpoint ApplicationEndpoint { get; set; }

        internal ClientPlatform ClientPlatform { get; set; }

        internal ClientPlatformSettings ClientPlatformSettings { get; set; }
    }

    internal static class TestHelper
    {
        public static bool IsInternalApp = true;

        public static MockApplicationEndpointData CreateApplicationEndpoint()
        {
            Logger.RegisterLogger(new ConsoleLogger());

            var platformSettings = new ClientPlatformSettings(
                DiscoverUri,
                AADClientId,
                AppTokenCertThumbprint,
                null,
                IsInternalApp);

            var restfulClient = new MockRestfulClient();
            var restfulClientFactory = new Mock<IRestfulClientFactory>();
            restfulClientFactory
                .Setup(foo => foo.GetRestfulClient(It.IsAny<OAuthTokenIdentifier>(), It.IsAny<ITokenProvider>()))
                .Returns(restfulClient);

            var platform = new ClientPlatform(platformSettings, new ConsoleLogger());
            platform.RestfulClientFactory = restfulClientFactory.Object;

            var endpointSettings = new ApplicationEndpointSettings(ApplicationEndpointUri);

            var mockEventChannel = new Mock<IEventChannel>();

            return new MockApplicationEndpointData()
            {
                EventChannel = mockEventChannel,
                RestfulClientFactory = restfulClientFactory,
                RestfulClient = restfulClient,
                ClientPlatformSettings = platformSettings,
                ClientPlatform = platform,
                ApplicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, mockEventChannel.Object)
            };
        }

        public static Uri DiscoverUri
        {
            get { return new Uri("https://NOAMmeetings.resources.lync.com/platformservice/discover?deploymentpreference=Weekly"); }
        }

        public static Uri AudienceUri
        {
            get { return new Uri("https://platformservicemonitor.cloudapp.net"); }
        }

        public static Uri AADAuthorityUri
        {
            get { return new Uri("https://login.windows.net"); }
        }

        public static Guid AADClientId
        {
            get { return Guid.Parse("44ff763b-5d1f-40ab-95bf-f31a18757998"); }
        }

        public static string AppTokenCertThumbprint
        {
            get
            {
                //return "3512AF18F02C3B359321B3BE197C268DA71E0154";

                // return a random certificate's thumbprint
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                if(store.Certificates.Count == 0)
                {
                    store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly);
                }

                if (store.Certificates.Count == 0)
                {
                    throw new Exception("No certificate could be found to use as token certificate.");
                }

                return store.Certificates[0].Thumbprint;
            }
        }

        public static SipUri ApplicationEndpointUri
        {
            get { return new SipUri("sip:monitoringaudio@0mcorp2cloudperf.onmicrosoft.com"); }
        }

        public static void RaiseEventsFromFile(Mock<IEventChannel> eventChannel, string filename)
        {
            var eventsChannelArgs = GetEventsChannelArgs(filename);
            eventChannel.Raise(mock => mock.HandleIncomingEvents += null, eventChannel.Object, eventsChannelArgs);
        }

        public static void RaiseEventsFromFileWithOperationId(Mock<IEventChannel> eventChannel, string filename, string operationId)
        {
            string jsonContent = File.ReadAllText("Data\\" + filename);
            jsonContent = jsonContent.Replace("49541af3-6866-4098-b6d9-b3ecd7784346", operationId);

            var eventsChannelArgs = GetEventsChannelArgsFromContent(jsonContent);
            eventChannel.Raise(mock => mock.HandleIncomingEvents += null, eventChannel.Object, eventsChannelArgs);
        }

        public static EventsChannelArgs GetEventsChannelArgs(string filename)
        {
            string jsonContent = File.ReadAllText("Data\\" + filename);
            return GetEventsChannelArgsFromContent(jsonContent);
        }

        public static EventsChannelArgs GetEventsChannelArgsFromContent(string jsonContent)
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri("https://platformservicemonitoring3.cloudapp.net/callback?callbackContext={\"JobId\":\"5474606859144360bc7cee5a87d9e9cc\"%2c\"InstanceId\":\"133976f272274227bc45e349c2aac558\"}");

            if (!TestHelper.IsInternalApp)
            {
                jsonContent = jsonContent.Replace(Constants.DefaultResourceNamespace, Constants.PublicServiceResourceNamespace);
            }

            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var message = new SerializableHttpRequestMessage();
            message.InitializeAsync(request, "no-request-id").Wait();

            return new EventsChannelArgs(message);
        }

        /// <summary>
        /// Raises events from <paramref name="eventsJsonFilename"/> on <paramref name="eventChannel"/> 
        /// when a HTTP request is made to <paramref name="uri"/> with <paramref name="method"/>.
        /// </summary>
        /// <param name="args"><see cref="RequestProcessedEventArgs"/> containing information about request</param>
        /// <param name="uri">Events will be raised for this uri</param>
        /// <param name="method">Events will be raised for this HTTP operation</param>
        /// <param name="eventsJsonFilename">Name of the file containing all events to be raised</param>
        /// <param name="eventChannel">Event channel on which events are to be raised.</param>
        /// <returns>
        /// If the HTTP request body contained an operationId/operationContext, 
        /// it will be returned otherwise <code>null</code> is returned
        /// </returns>
        public static string RaiseEventsOnHttpRequest(RequestProcessedEventArgs args, string uri, HttpMethod method, string eventsJsonFilename, Mock<IEventChannel> eventChannel)
        {
            // Some operations have operationId which is provided by the application.
            // When raising events related to those operations, we platformservice gives the same operationId in the event.
            // We also need to fake this behavior.
            string operationId = null;

            if (args.Uri == new Uri(uri) && args.Method == method)
            {
                if (args.Input is InvitationInput)
                {
                    operationId = ((InvitationInput)args.Input).OperationContext;
                }
                else if (args.Input is TransferOperationInput)
                {
                    operationId = ((TransferOperationInput)args.Input).OperationId;
                }
                else if (args.Input is AddParticipantInvitationInput)
                {
                    operationId = ((AddParticipantInvitationInput)args.Input).OperationContext;
                }

                if (eventChannel != null)
                {
                    if (operationId != null)
                    {
                        RaiseEventsFromFileWithOperationId(eventChannel, eventsJsonFilename, operationId);
                    }
                    else
                    {
                        RaiseEventsFromFile(eventChannel, eventsJsonFilename);
                    }
                }
            }

            return operationId;
        }

        public static EventsEntity GetEventsEntityForEventsInFile(string filename)
        {
            InitializeTokenMapper();

            EventsEntity eventsEntity = null;

            string jsonContent = File.ReadAllText("Data\\" + filename);

            if (!TestHelper.IsInternalApp)
            {
                jsonContent = jsonContent.Replace(Constants.DefaultResourceNamespace, Constants.PublicServiceResourceNamespace);
            }

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));
            MediaTypeHeaderValue typeHeader = null;
            MediaTypeHeaderValue.TryParse("application/json", out typeHeader);
            eventsEntity = MediaTypeFormattersHelper.ReadContentWithType(typeof(EventsEntity), typeHeader, stream) as EventsEntity;

            return eventsEntity;
        }

        public static void InitializeTokenMapper()
        {
            if (!TestHelper.IsInternalApp)
            {
                TokenMapper.RegisterNameSpaceHandling(Constants.DefaultResourceNamespace, Constants.PublicServiceResourceNamespace);
            }

            TokenMapper.RegisterTypesInAssembly(Assembly.GetAssembly(typeof(ApplicationsResource)));
        }
    }
}

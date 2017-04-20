using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using QuickSamplesCommon;
using System;
using System.Threading.Tasks;

/// <summary>
/// Simple sampel on remote advisor scenario:
/// 1. Schedule an adhoc meeting with SkypeforBusiness
/// 2. And get anon token of that adhoc meeting for webSDK or AppSDK anon user to join the meeting
/// </summary>
namespace RemoteAdvisorSample
{
    public static class Program
    {
        public static void Main()
        {
            RemoteAdvisorSample sample = new RemoteAdvisorSample();
            try
            {
                sample.RunAsync().Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Exception hit:" + ex.GetBaseException().ToString());
            }
        }
    }

    internal class RemoteAdvisorSample
    {
        public async Task RunAsync()
        {
            var logger = new SampleAppLogger();
            logger.HttpRequestResponseNeedsToBeLogged = true;//Set to true if you want to log all http request and responses

            //Prepare platform
            ClientPlatformSettings platformSettings = new ClientPlatformSettings(QuickSamplesConfig.AAD_ClientSecret, Guid.Parse(QuickSamplesConfig.AAD_ClientId));

            var platform = new ClientPlatform(platformSettings, logger);

            //Prepare endpoint
            var eventChannel = new FakeEventChannel();
            var endpointSettings = new ApplicationEndpointSettings(new SipUri(QuickSamplesConfig.ApplicationEndpointId));
            ApplicationEndpoint applicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, eventChannel);

            var loggingContext = new LoggingContext(Guid.NewGuid());
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            //Schedule meeting
            var input = new AdhocMeetingCreationInput(Guid.NewGuid().ToString("N") + "testMeeting");
            var adhocMeeting =  await applicationEndpoint.Application.CreateAdhocMeetingAsync(input, loggingContext).ConfigureAwait(false);

            logger.Information("ad hoc meeting uri : " + adhocMeeting.OnlineMeetingUri);
            logger.Information("ad hoc meeting join url : " + adhocMeeting.JoinUrl);

            //Get anon join token
            IAnonymousApplicationToken anonToken = await applicationEndpoint.Application.GetAnonApplicationTokenForMeetingAsync(
                    adhocMeeting.JoinUrl,
                    "https://contoso.com;https://litware.com;http://www.microsoftstore.com/store/msusa/en_US/home", //Fill your own web site, For allow cross domain using
                    Guid.NewGuid().ToString(), //Should be unique everytime
                    loggingContext).ConfigureAwait(false);

            logger.Information("Get anon token : " + anonToken.AuthToken);
            logger.Information("Get discover url for web SDK : " + anonToken.AnonymousApplicationsDiscoverUri.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            logger.Information("RemoteAdvisor sample completed successfully!");
            Console.ResetColor();
        }
    }
}

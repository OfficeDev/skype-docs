using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel.Internal; // Required for setting customized callback url
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Skype.Calling.ServiceAgents.SkypeToken;
using QuickSamplesCommon;
using TrouterCommon;
namespace MessagingAfterJoinMeeting
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample = new MessagingAfterJoinMeeting();
            try
            {
                sample.RunAsync().Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Exception: " + ex.GetBaseException().ToString());
            }

            if(sample.EventChannel != null)
            {
                sample.EventChannel.TryStopAsync().Wait();
            }
        }
    }

    /// <summary>
    /// Scenario:
    ///  1. Schedule a conference
    ///  2. Trusted join the conference
    ///  3. Add messaging to the conference
    ///  4. Send "Hello World!" to the conference
    /// </summary>
    internal class MessagingAfterJoinMeeting
    {
        public TrouterBasedEventChannel EventChannel { get; private set; }

        private IPlatformServiceLogger m_logger;

        public async Task RunAsync()
        {
            var skypeId = ConfigurationManager.AppSettings["Trouter_SkypeId"];
            var password = ConfigurationManager.AppSettings["Trouter_Password"];
            var applicationName = ConfigurationManager.AppSettings["Trouter_ApplicationName"];
            var userAgent = ConfigurationManager.AppSettings["Trouter_UserAgent"];
            var token = SkypeTokenClient.ConstructSkypeToken(
                skypeId: skypeId,
                password: password,
                useTestEnvironment: false,
                scope: string.Empty,
                applicationName: applicationName).Result;

            m_logger = new SampleAppLogger();

            // Uncomment for debugging
            // m_logger.HttpRequestResponseNeedsToBeLogged = true;

            EventChannel = new TrouterBasedEventChannel(m_logger, token, userAgent);

            // Prepare platform
            var platformSettings = new ClientPlatformSettings(QuickSamplesConfig.AAD_ClientSecret, new Guid(QuickSamplesConfig.AAD_ClientId));
            var platform = new ClientPlatform(platformSettings, m_logger);

            // Prepare endpoint
            var endpointSettings = new ApplicationEndpointSettings(new SipUri(QuickSamplesConfig.ApplicationEndpointId));
            var applicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, EventChannel);

            var loggingContext = new LoggingContext(Guid.NewGuid());
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Meeting configuration
            var meetingConfiguration = new AdhocMeetingCreationInput(Guid.NewGuid().ToString("N") + " test meeting");

            // Schedule meeting
            var adhocMeeting = await applicationEndpoint.Application.CreateAdhocMeetingAsync(loggingContext, meetingConfiguration).ConfigureAwait(false);

            WriteToConsoleInColor("ad hoc meeting uri : " + adhocMeeting.OnlineMeetingUri);
            WriteToConsoleInColor("ad hoc meeting join url : " + adhocMeeting.JoinUrl);

            // Get all the events related to join meeting through Trouter's uri
            platformSettings.SetCustomizedCallbackurl(new Uri(EventChannel.CallbackUri));

            // Start joining the meeting
            var invitation = await adhocMeeting.JoinAdhocMeeting(loggingContext, null).ConfigureAwait(false);

            // Wait for the join to complete
            await invitation.WaitForInviteCompleteAsync().ConfigureAwait(false);

            WriteToConsoleInColor("Please use this url to join the meeting : " + adhocMeeting.JoinUrl);

            WriteToConsoleInColor("Giving 30 seconds for the user to join the meeting...");
            await Task.Delay(TimeSpan.FromSeconds(30)).ConfigureAwait(false);

            var conversation = invitation.RelatedConversation;

            var imCall = invitation.RelatedConversation.MessagingCall;

            if (imCall == null)
            {
                WriteToConsoleInColor("No messaging call link found in conversation of the conference.");
                return;
            }

            var messagingInvitation = await imCall.EstablishAsync(loggingContext).ConfigureAwait(false);

            messagingInvitation.HandleResourceCompleted += OnMessagingResourceCompletedReceived;

            await messagingInvitation.WaitForInviteCompleteAsync().ConfigureAwait(false);

            if (imCall.State != CallState.Connected)
            {
                WriteToConsoleInColor("Messaging call is not connected.");
                return;
            }

            var modalities = invitation.RelatedConversation.ActiveModalities;
            WriteToConsoleInColor("Active modalities : ");
            bool hasMessagingModality = false;
            foreach (var modality in modalities)
            {
                WriteToConsoleInColor(" " + modality.ToString());
                if (modality == ConversationModalityType.Messaging)
                {
                    hasMessagingModality = true;
                }
            }

            await imCall.SendMessageAsync("Hello World.", loggingContext).ConfigureAwait(false);

            if (!hasMessagingModality)
            {
                WriteToConsoleInColor("Failed to connect messaging call.", ConsoleColor.Red);
                return;
            }
            WriteToConsoleInColor("Adding messaging to meeting completed successfully.");
        }

        private void OnMessagingResourceCompletedReceived(object sender, PlatformResourceEventArgs args)
        {
            if (args.PlatformResource is MessagingInvitationResource)
            {
                WriteToConsoleInColor("Messaging resource completed event found.");
            }
        }

        private void WriteToConsoleInColor(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

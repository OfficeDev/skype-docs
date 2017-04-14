using System;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel.Internal; // Required for setting customized callback url
using Microsoft.SfB.PlatformService.SDK.Common;
using QuickSamplesCommon;

namespace MessagingAfterJoinMeeting
{
    public static class Program
    {
        public static void Main()
        {
            var sample = new MessagingAfterJoinMeeting();
            try
            {
                Uri callbackUri;
                // Start HTTP server and get callback uri
                using (WebEventChannel.WebEventChannel.StartHttpServer(out callbackUri))
                {
                    sample.RunAsync(callbackUri).Wait();
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Exception: " + ex.GetBaseException().ToString());
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
        private IPlatformServiceLogger m_logger;

        public async Task RunAsync(Uri callbackUri)
        {
            m_logger = new SampleAppLogger();

            // Uncomment for debugging
            // m_logger.HttpRequestResponseNeedsToBeLogged = true;

            // You can hook up your own implementation of IEventChannel here
            IEventChannel eventChannel = WebEventChannel.WebEventChannel.Instance;

            // Prepare platform
            var platformSettings = new ClientPlatformSettings(QuickSamplesConfig.AAD_ClientSecret, new Guid(QuickSamplesConfig.AAD_ClientId));
            var platform = new ClientPlatform(platformSettings, m_logger);

            // Prepare endpoint
            var endpointSettings = new ApplicationEndpointSettings(new SipUri(QuickSamplesConfig.ApplicationEndpointId));
            var applicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, eventChannel);

            var loggingContext = new LoggingContext(Guid.NewGuid());
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Meeting configuration
            var meetingConfiguration = new AdhocMeetingCreationInput(Guid.NewGuid().ToString("N") + " test meeting");

            // Schedule meeting
            var adhocMeeting = await applicationEndpoint.Application.CreateAdhocMeetingAsync(meetingConfiguration, loggingContext).ConfigureAwait(false);

            WriteToConsoleInColor("ad hoc meeting uri : " + adhocMeeting.OnlineMeetingUri);
            WriteToConsoleInColor("ad hoc meeting join url : " + adhocMeeting.JoinUrl);

            // Get all the events related to join meeting through our custom callback uri
            platformSettings.SetCustomizedCallbackurl(callbackUri);

            // Start joining the meeting
            ICommunication communication = applicationEndpoint.Application.Communication;
            if(!communication.CanJoinAdhocMeeting(adhocMeeting))
            {
                throw new Exception("Cannot join adhoc meeting");
            }

            var invitation = await communication.JoinAdhocMeetingAsync(adhocMeeting, null, loggingContext).ConfigureAwait(false);

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

            messagingInvitation.HandleResourceCompleted += OnMessagingInvitationCompleted;

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

            if (!hasMessagingModality)
            {
                WriteToConsoleInColor("Failed to connect messaging call.", ConsoleColor.Red);
                return;
            }
            WriteToConsoleInColor("Adding messaging to meeting completed successfully.");

            WriteToConsoleInColor("Sending Hellow World!");
            await imCall.SendMessageAsync("Hello World!", loggingContext).ConfigureAwait(false);
            WriteToConsoleInColor("Sent Hello World!");

            await WebEventChannel.WebEventChannel.Instance.TryStopAsync().ConfigureAwait(false);
        }

        private void OnMessagingInvitationCompleted(object sender, PlatformResourceEventArgs args)
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

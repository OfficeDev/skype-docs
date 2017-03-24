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

namespace TrustedAudioVideoMeeting
{
    public static class Program
    {
        public static void Main()
        {
            var sample = new TrustedAudioVideoMeeting();
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
    ///  3. Start AudioVideo call
    ///  4. Wait for new participants
    ///  5. Play a prompt
    /// </summary>
    internal class TrustedAudioVideoMeeting
    {
        private static readonly string mediaUrl =
            "https://raw.githubusercontent.com/OfficeDev/skype-docs/master/Skype/Trusted-Application-API/samples/QuickStartSamples/TrustedAudioVideoMeeting/media/prompt.wav";

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
            m_logger.HttpRequestResponseNeedsToBeLogged = true;

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
            var conversation = invitation.RelatedConversation;

            // Wait for the join to complete
            await invitation.WaitForInviteCompleteAsync().ConfigureAwait(false);
            conversation.HandleParticipantChange += Conversation_HandleParticipantChange;

            IAudioVideoCall audioVideoCall = conversation.AudioVideoCall;
            await audioVideoCall.EstablishAsync(loggingContext).ConfigureAwait(false);

            // Wait for the audiovideo call to complete
            await audioVideoCall.WaitForAVFlowConnected().ConfigureAwait(false);

            WriteToConsoleInColor("Showing roaster udpates for 1 minute for meeting : " + adhocMeeting.JoinUrl);

            // Wait for 1 minutes before playing prompt. During this time, we can connect from other skype client and listen prompt.
            // Since we have registered Conversation_HandleParticipantChange, we will also continue to show participant changes in the
            // meeting for this duration.
            await Task.Delay(TimeSpan.FromMinutes(1)).ConfigureAwait(false);

            Uri promptUri = new Uri(mediaUrl);
            // Wait for prompt play to complete
            await audioVideoCall.AudioVideoFlow.PlayPromptAsync(promptUri, loggingContext).ConfigureAwait(false);
            
            // exit after play prompt. wait for 5 seconds to see all responses.
            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
        }

        private void Conversation_HandleParticipantChange(object sender, ParticipantChangeEventArgs eventArgs)
        {
            if (eventArgs.AddedParticipants?.Count > 0)
            {
                foreach (var participant in eventArgs.AddedParticipants)
                {
                    WriteToConsoleInColor(participant.Name + " has joined the meeting.");
                }
            }

            if (eventArgs.RemovedParticipants?.Count > 0)
            {
                foreach (var participant in eventArgs.RemovedParticipants)
                {
                    WriteToConsoleInColor(participant.Name + " has left the meeting.");
                }
            }

            if (eventArgs.UpdatedParticipants?.Count > 0)
            {
                foreach (var participant in eventArgs.UpdatedParticipants)
                {
                    WriteToConsoleInColor(participant.Name + " got updated");
                }
            }
        }

        private void WriteToConsoleInColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel.Internal; // Required for setting customized callback url
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Skype.Calling.ServiceAgents.SkypeToken;
using QuickSamplesCommon;
using TrouterCommon;

namespace InviteAndRemoveParticipant
{
    public static class Program
    {
        public static void Main()
        {
            var sample = new InviteAndRemoveParticipant();
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
    ///  3. Invite the participant to join the conference
    ///  4. Listen for participant changes for 5 minutes.If the partcipant has accepted the invitation,then remove the participant from the conference
    /// </summary>
    internal class InviteAndRemoveParticipant
    {
        public TrouterBasedEventChannel EventChannel { get; private set; }

        private IPlatformServiceLogger m_logger;

        public async Task RunAsync()
        {
            var skypeId = ConfigurationManager.AppSettings["Trouter_SkypeId"];
            var password = ConfigurationManager.AppSettings["Trouter_Password"];
            var applicationName = ConfigurationManager.AppSettings["Trouter_ApplicationName"];
            var userAgent = ConfigurationManager.AppSettings["Trouter_UserAgent"];
            var participantUri = ConfigurationManager.AppSettings["ParticipantUri"];
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

            invitation.RelatedConversation.HandleParticipantChange += Conversation_HandleParticipantChange;

            // invite the participant to join the meeting
            WriteToConsoleInColor("Invite " + participantUri + " to join the meeting");
            var participantInvitation = await invitation.RelatedConversation.AddParticipantAsync(participantUri, loggingContext).ConfigureAwait(false);

            // Wait for the join to complete
            await participantInvitation.WaitForInviteCompleteAsync().ConfigureAwait(false);

            //remove the participant from the meeting
            foreach (var participant in invitation.RelatedConversation.Participants)
            {
                if (participantUri.Equals(participant.Uri))
                {
                    await participant.EjectAsync(loggingContext).ConfigureAwait(false);
                }
            }

            WriteToConsoleInColor("Showing roaster udpates for 5 minutes for meeting : " + adhocMeeting.JoinUrl);

            // Wait 5 minutes before exiting.
            // Since we have registered Conversation_HandleParticipantChange, we will continue to show participant changes in the
            // meeting for this duration.
            await Task.Delay(TimeSpan.FromMinutes(5)).ConfigureAwait(false);
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

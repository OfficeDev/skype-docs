using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel.Internal; // Required for setting customized callback url
using Microsoft.SfB.PlatformService.SDK.Common;
using QuickSamplesCommon;

namespace InviteAndRemoveParticipant
{
    public static class Program
    {
        public static void Main()
        {
            var sample = new InviteAndRemoveParticipant();
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
    ///  3. Invite the participant to join the conference
    ///  4. Listen for participant changes for 5 minutes.If the partcipant has accepted the invitation,then remove the participant from the conference
    /// </summary>
    internal class InviteAndRemoveParticipant
    {
        private IPlatformServiceLogger m_logger;
        private TaskCompletionSource<IParticipant> m_participantAddedTcs;

        public async Task RunAsync(Uri callbackUri)
        {
            var participantUri = ConfigurationManager.AppSettings["ParticipantUri"];

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
                throw new Exception("Cannot join the adhoc meeting!");
            }

            var invitation = await communication.JoinAdhocMeetingAsync(adhocMeeting, null, loggingContext).ConfigureAwait(false);

            // Wait for the join to complete
            await invitation.WaitForInviteCompleteAsync().ConfigureAwait(false);

            m_participantAddedTcs = new TaskCompletionSource<IParticipant>();

            invitation.RelatedConversation.HandleParticipantChange += Conversation_HandleParticipantChange;

            // invite the participant to join the meeting
            WriteToConsoleInColor("Invite " + participantUri + " to join the meeting");
            var participantInvitation = await invitation.RelatedConversation.AddParticipantAsync(new SipUri(participantUri), loggingContext).ConfigureAwait(false);

            // Wait for the join to complete
            await participantInvitation.WaitForInviteCompleteAsync().ConfigureAwait(false);

            // Get the corresponding IParticipant; other way of getting this is by traversing invitation.RelatedConversation.Participants
            IParticipant participant = await m_participantAddedTcs.Task.ConfigureAwait(false);

            WriteToConsoleInColor("Waiting 10 seconds before ejecting " + participant.Name);
            await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false);

            WriteToConsoleInColor("Ejecting " + participant.Name + " from the meeting");

            await participant.EjectAsync(loggingContext).ConfigureAwait(false);

            WriteToConsoleInColor("Showing roaster udpates for 5 minutes for meeting : " + adhocMeeting.JoinUrl);

            // Wait 5 minutes before exiting.
            // Since we have registered Conversation_HandleParticipantChange, we will continue to show participant changes in the
            // meeting for this duration.
            await Task.Delay(TimeSpan.FromMinutes(5)).ConfigureAwait(false);

            await WebEventChannel.WebEventChannel.Instance.TryStopAsync().ConfigureAwait(false);
        }

        private void Conversation_HandleParticipantChange(object sender, ParticipantChangeEventArgs eventArgs)
        {
            if (eventArgs.AddedParticipants?.Count > 0)
            {
                foreach (var participant in eventArgs.AddedParticipants)
                {
                    WriteToConsoleInColor(participant.Name + " has joined the meeting.");
                    m_participantAddedTcs.SetResult(participant);
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

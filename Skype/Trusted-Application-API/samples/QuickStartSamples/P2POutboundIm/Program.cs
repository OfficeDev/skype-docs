using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Skype.Calling.ServiceAgents.SkypeToken;
using QuickSamplesCommon;
using TrouterCommon;

namespace P2POutboundIm
{
    public static class Program
    {
        public static void Main()
        {
            var sample = new P2PImOutboundCall();
            try
            {
                sample.RunAsync().Wait();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Exception: " + ex.GetBaseException());
            }

            sample.EventChannel?.TryStopAsync().Wait();
        }
    }

    internal class P2PImOutboundCall
    {
        public TrouterBasedEventChannel EventChannel { get; private set; }

        private IPlatformServiceLogger m_logger;

        public async Task RunAsync()
        {
            var skypeId = ConfigurationManager.AppSettings["Trouter_SkypeId"];
            var password = ConfigurationManager.AppSettings["Trouter_Password"];
            var applicationName = ConfigurationManager.AppSettings["Trouter_ApplicationName"];
            var userAgent = ConfigurationManager.AppSettings["Trouter_UserAgent"];
            var targetUserId = ConfigurationManager.AppSettings["Skype_TargetUserId"];
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
            var platformSettings = new ClientPlatformSettings(QuickSamplesConfig.AAD_ClientSecret,
                new Guid(QuickSamplesConfig.AAD_ClientId));
            var platform = new ClientPlatform(platformSettings, m_logger);

            // Prepare endpoint
            var endpointSettings = new ApplicationEndpointSettings(new SipUri(QuickSamplesConfig.ApplicationEndpointId));
            var applicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, EventChannel);

            var loggingContext = new LoggingContext(Guid.NewGuid());
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            WriteToConsoleInColor("Start to send messaging invitation");
            var invitation = await applicationEndpoint.Application.Communication.StartMessagingAsync(
                subject: "Subject",
                to: targetUserId,
                callbackUrl: EventChannel.CallbackUri,
                loggingContext: loggingContext);
            var conversation = await invitation.WaitForInviteCompleteAsync().ConfigureAwait(false);

            conversation.HandleParticipantChange += Conversation_HandleParticipantChange;
            conversation.MessagingCall.IncomingMessageReceived += Handle_IncomingMessage;

            WriteToConsoleInColor("Showing roaster udpates for 5 minutes for the messages");
            
            // Wait 5 minutes before exiting
            // Since we registered callbacks, we will continue to show message logs
            await Task.Delay(TimeSpan.FromMinutes(5)).ConfigureAwait(false);
        }

        private void Handle_IncomingMessage(object sender, IncomingMessageEventArgs incomingMessageEventArgs)
        {
            var msg = Encoding.UTF8.GetString(incomingMessageEventArgs.PlainMessage.Message);
            WriteToConsoleInColor($"Message Received from '{incomingMessageEventArgs.FromParticipantName}': {msg}");
        }

        private void Conversation_HandleParticipantChange(object sender, ParticipantChangeEventArgs eventArgs)
        {
            if (eventArgs.AddedParticipants?.Count > 0)
            {
                foreach (var participant in eventArgs.AddedParticipants)
                {
                    WriteToConsoleInColor(participant.Name + " has joined the conversation.");
                }
            }

            if (eventArgs.RemovedParticipants?.Count > 0)
            {
                foreach (var participant in eventArgs.RemovedParticipants)
                {
                    WriteToConsoleInColor(participant.Name + " has left the conversation.");
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
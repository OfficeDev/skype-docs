using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel.Internal; // Required for setting customized callback url
using Microsoft.SfB.PlatformService.SDK.Common;
using QuickSamplesCommon;

namespace P2POutboundIm
{
    public static class Program
    {
        public static void Main()
        {
            var sample = new P2PImOutboundCall();
            try
            {
                Uri callbackUri;
                // Start HTTP server and get callback uri
                using (WebEventChannel.WebEventChannel.StartHttpServer(out callbackUri))
                {
                    sample.RunAsync(callbackUri).Wait();
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Exception: " + ex.GetBaseException());
            }
        }
    }

    internal class P2PImOutboundCall
    {
        private IPlatformServiceLogger m_logger;

        public async Task RunAsync(Uri callbackUri)
        {
            var targetUserId = ConfigurationManager.AppSettings["Skype_TargetUserId"];

            m_logger = new SampleAppLogger();

            // Uncomment for debugging
            // m_logger.HttpRequestResponseNeedsToBeLogged = true;

            // You can hook up your own implementation of IEventChannel here
            IEventChannel eventChannel = WebEventChannel.WebEventChannel.Instance;

            // Prepare platform
            var platformSettings = new ClientPlatformSettings(QuickSamplesConfig.AAD_ClientSecret,
                new Guid(QuickSamplesConfig.AAD_ClientId));
            var platform = new ClientPlatform(platformSettings, m_logger);

            // Prepare endpoint
            var endpointSettings = new ApplicationEndpointSettings(new SipUri(QuickSamplesConfig.ApplicationEndpointId));
            var applicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, eventChannel);

            var loggingContext = new LoggingContext(Guid.NewGuid());
            await applicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            // Get all the events related to join meeting through our custom callback uri
            platformSettings.SetCustomizedCallbackurl(callbackUri);

            WriteToConsoleInColor("Start to send messaging invitation");
            var invitation = await applicationEndpoint.Application.Communication.StartMessagingAsync(
                "Subject",
                new SipUri(targetUserId),
                null,
                loggingContext).ConfigureAwait(false);

            // Wait for user to accept the invitation
            var conversation = await invitation.WaitForInviteCompleteAsync().ConfigureAwait(false);

            conversation.HandleParticipantChange += Conversation_HandleParticipantChange;
            conversation.MessagingCall.IncomingMessageReceived += Handle_IncomingMessage;

            // Send the initial message
            await conversation.MessagingCall.SendMessageAsync("Hello World!", loggingContext).ConfigureAwait(false);

            WriteToConsoleInColor("Staying in the conversation for 5 minutes");

            // Wait 5 minutes before exiting
            // Since we registered callbacks, we will continue to show message logs
            await Task.Delay(TimeSpan.FromMinutes(5)).ConfigureAwait(false);

            await WebEventChannel.WebEventChannel.Instance.TryStopAsync().ConfigureAwait(false);
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
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Newtonsoft.Json;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// The definition for SimpleNotifyJob
    /// </summary>
    public class SimpleNotifyJob : PlatformServiceJobBase
    {
        private TaskCompletionSource<string> m_responseMessageNotified;

        public SimpleNotifyJob(string jobId, string instanceId, AzureBasedApplicationBase azureApplication, SimpleNotifyJobInput input)
            : base(jobId, instanceId, azureApplication, input, JobType.SimpleNotification)
        {
            m_responseMessageNotified = new TaskCompletionSource<string>();
        }

        /// <summary>
        /// Implement the interface ExecuteCoreAsync
        /// </summary>
        /// <returns></returns>
        public override async Task ExecuteCoreAsync()
        {
            ICommunication communication = AzureApplication.ApplicationEndpoint.Application.Communication;

            //Construct callbackUrl with callbackContext for callback message routing
            CallbackContext callbackcontext = new CallbackContext { InstanceId = this.InstanceId, JobId = this.JobId };
            string callbackContextJsonString = JsonConvert.SerializeObject(callbackcontext);
            string CallbackUrl = string.Format(CultureInfo.InvariantCulture, AzureApplication.CallbackUriFormat, HttpUtility.UrlEncode(callbackContextJsonString));

            Logger.Instance.Information("[SimpleNotifyJob] start to send messaging invitation");

            SimpleNotifyJobInput jobinput = this.JobInput as SimpleNotifyJobInput;
            if (jobinput == null)
            {
                throw new InvalidOperationException("Failed to get SimpleNotifyJobInput instance");
            }

            IMessagingInvitation invite = null;

            try
            {
                //Invite resource will be there when this taskc completes
                invite = await communication.StartMessagingWithIdentityAsync("Notification", jobinput.TargetUri, CallbackUrl, "Sample Application", "sip:sampleapplication@0MCorp2CloudPerf.onmicrosoft.com", LoggingContext).ConfigureAwait(false);

                //Waiting for invite complete
                await invite.WaitForInviteCompleteAsync().ConfigureAwait(false);

                IMessagingCall messagingCall = invite.RelatedConversation.MessagingCall;
                messagingCall.IncomingMessageReceived += OnIncomingMessageReceived;

                Logger.Instance.Information("[SimpleNotifyJob] sending notification message");
                await messagingCall.SendMessageAsync(jobinput.NotificationMessage, LoggingContext).ConfigureAwait(false);

                Task reseponseReceived = m_responseMessageNotified.Task;

                try
                {
                    await reseponseReceived.TimeoutAfterAsync(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
                }
                catch (TimeoutException)
                {
                    Logger.Instance.Warning("[SimpleNotifyJob] Do not get a response with in 30 seconds");
                }

                if (reseponseReceived.IsCompleted)
                {
                    Logger.Instance.Information("[SimpleNotifyJob] sending thank you message");
                    await messagingCall.SendMessageAsync("Thank you!", LoggingContext).ConfigureAwait(false);
                }
            }
            catch(CapabilityNotAvailableException ex)
            {
                Logger.Instance.Error("[SimpleNotifyJob] Failed!", ex);
            }
            catch(RemotePlatformServiceException ex)
            {
                Logger.Instance.Error("[SimpleNotifyJob] Failed!", ex);
            }
            finally
            {
                if (invite?.RelatedConversation != null)
                {
                    await invite.RelatedConversation.DeleteAsync(LoggingContext).ConfigureAwait(false);
                }
            }
        }

        private void OnIncomingMessageReceived(object sender, IncomingMessageEventArgs args)
        {
            Logger.Instance.Information("[SimpleNotifyJob] Get incoming message");
            m_responseMessageNotified.TrySetResult(string.Empty);
        }
    }
}
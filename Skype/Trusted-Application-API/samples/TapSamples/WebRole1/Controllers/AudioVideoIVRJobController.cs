using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    /// <summary>
    /// A controller to handle <see cref="AudioVideoIVRJob"/>s.
    /// </summary>
    [MyCorsPolicy]
    public class AudioVideoIVRJobController : JobControllerBase
    {
        /// <summary>
        /// Starts an <see cref="AudioVideoIVRJob"/>.
        /// </summary>
        /// <param name="input"><see cref="AudioVideoIVRJobInput"/> specifying the configuration for the job.</param>
        /// <returns>ID of the started job</returns>
        public HttpResponseMessage Post(AudioVideoIVRJobInput input)
        {
            ValidateInput(input);

            var jobConfig = new PlatformServiceSampleJobConfiguration
            {
                JobType = JobType.AudioVideoIVR,
                AudioVideoIVRJobInput = input
            };

            string jobId = Guid.NewGuid().ToString("N");

            try
            {
                var job = PlatformServiceClientJobHelper.GetJob(jobId, WebApiApplication.InstanceId, WebApiApplication.AzureApplication, jobConfig) as AudioVideoIVRJob;

                if (job == null)
                {
                    return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid job input or job type\"}");
                }

                job.ExecuteAsync().Observe<Exception>();

                return Request.CreateResponse(HttpStatusCode.Created, job);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e, "Exception while starting the job.");
                return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Unable to start the job\"}");
            }
        }

        #region Input validation

        /// <summary>
        /// Checks if an <see cref="AudioVideoIVRJobInput"/> is valid or not.
        /// </summary>
        /// <param name="input"><see cref="AudioVideoIVRJobInput"/> to be validated.</param>
        /// <exception cref="HttpRequestValidationException">If <paramref name="input"/> is invalid</exception>
        /// <remarks>It also sets <see cref="AudioVideoIVRJobInput.ParentInput"/> references.</remarks>
        private void ValidateInput(AudioVideoIVRJobInput input)
        {
            if (input == null)
            {
                throw new HttpRequestValidationException("Invalid AudioVideoIVRJobInput");
            }

            if (input.Action == AudioVideoIVRActions.TerminateCall
                || input.Action == AudioVideoIVRActions.GoToPreviousPrompt
                || input.Action == AudioVideoIVRActions.RepeatPrompt)
            {
                // No more validation required.
                return;
            }

            string validationError;

            if (input.Action == AudioVideoIVRActions.TransferToUser)
            {
                if (!UriHelper.IsSipUri(input.User))
                {
                    throw new HttpRequestValidationException("Invalid User " + input.User);
                }

                // We require a transfer prompt.
                if (!IsPromptValid(input.Prompt, out validationError))
                {
                    throw new HttpRequestValidationException("Prompt specified for transfer to user " + input.User + " is invalid : " + validationError);
                }
            }

            if (input.Action == AudioVideoIVRActions.PlayPrompt)
            {
                if(!IsPromptValid(input.Prompt, out validationError))
                {
                    throw new HttpRequestValidationException(validationError);
                }

                if (input.KeyMap == null || input.KeyMap.Count == 0)
                {
                    throw new HttpRequestValidationException("No KeyMap specified for PlayPrompt.");
                }

                foreach (var keyInfo in input.KeyMap)
                {
                    if (!IsValidToneKey(keyInfo.Key))
                    {
                        throw new HttpRequestValidationException(keyInfo.Key + " is not a valid tone key");
                    }

                    ValidateInput(keyInfo.Value);
                    keyInfo.Value.ParentInput = input;
                }
            }
        }

        /// <summary>
        /// Validates whether we have an audio file available under <i>resources</i> directory for the specified prompt.
        /// </summary>
        /// <param name="prompt">Name of the audio file, it must contain the full name including file extension</param>
        /// <param name="validationError"><i>out</i> param containing the error message.</param>
        /// <returns><code>true</code> iff <paramref name="prompt"/> is acceptable</returns>
        private bool IsPromptValid(string prompt, out string validationError)
        {
            if (prompt == null)
            {
                validationError = "No Prompt specified.";
                return false;
            }

            // Verify that prompt file exists.
            var promptFile = Path.Combine(HttpRuntime.AppDomainAppPath, "Resources", prompt);
            if (!File.Exists(promptFile))
            {
                validationError = "Specified prompt file(" + prompt + ") doesn't exist, please use one of the existing files or add a new one.";
                return false;
            }

            validationError = null;
            return true;
        }

        /// <summary>
        /// Validates whether <paramref name="address"/> is a valid sip address or not.
        /// </summary>
        /// <param name="address">sip address to be validated</param>
        /// <returns><code>true</code> iff <paramref name="address"/> is a valid sip address.</returns>
        public bool IsValidSipAddress(string address)
        {
            return address.StartsWith("sip:", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Validates whether <paramref name="key"/> is a valid tone key or not.
        /// </summary>
        /// <param name="key">tone key to be validated</param>
        /// <returns><code>true</code> iff <paramref name="key"/> is a valid tone key</returns>
        public bool IsValidToneKey(string key)
        {
            var validToneKeys = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "*", "#" };
            return validToneKeys.Contains(key);
        }

        #endregion
    }
}
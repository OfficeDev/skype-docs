using System;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Exposes all the error data provided by SfB in a HTTP response
    /// </summary>
    public class ErrorInformation
    {
        private Rtc.Internal.RestAPI.ResourceModel.ErrorInformation m_errorInformation { get; }

        /// <summary>
        /// HTTP Status Code returned
        /// </summary>
        public ErrorCode Code
        {
            get { return m_errorInformation.Code; }
        }

        /// <summary>
        /// Error subcode hinting what went wrong
        /// </summary>
        public ErrorSubcode? Subcode
        {
            get { return m_errorInformation.Subcode; }
        }

        /// <summary>
        /// Message explaining the error
        /// </summary>
        public string Message
        {
            get { return m_errorInformation.Message; }
        }

        /// <summary>
        /// Get all debug information from the error as a string
        /// </summary>
        /// <returns>All debug information from the error as a string</returns>
        public string GetDebugPropertiesAsString()
        {
            return m_errorInformation.GetDebugPropertiesAsString();
        }

        internal ErrorInformation(Rtc.Internal.RestAPI.ResourceModel.ErrorInformation errorInformation)
        {
            if (errorInformation == null)
            {
                throw new ArgumentNullException(nameof(errorInformation));
            }

            m_errorInformation = errorInformation;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return m_errorInformation.GetErrorInformationString();
        }
    }

    /// <summary>
    /// Provides more detailed error information
    /// </summary>
    public static class ErrorInformationExtensions
    {
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents more detailed error information
        /// </summary>
        /// <param name="This">The error information.</param>
        /// <returns>more detailed error information.</returns>
        public static string GetErrorInformationString(this Rtc.Internal.RestAPI.ResourceModel.ErrorInformation This)
        {
            return string.Format("ErrorCode {0}, Error Subcode {1}, Messaging {2}, Debug Info {3}",
                This.Code.ToString(),
                This.Subcode.HasValue ? This.Subcode.Value.ToString() : string.Empty,
                This.Message,
                This.GetDebugPropertiesAsString());
        }
    }
}

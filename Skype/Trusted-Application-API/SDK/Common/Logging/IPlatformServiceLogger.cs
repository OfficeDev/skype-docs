using System;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// Interface used by Trusted Application API to write logs
    /// </summary>
    public interface IPlatformServiceLogger
    {
        /// <summary>
        /// Decides whether to write full HTTP requests and responses to logs
        /// </summary>
        bool HttpRequestResponseNeedsToBeLogged { get; set; }

        /// <summary>
        /// Writes logs at INFO level
        /// </summary>
        /// <param name="message">Message to be logged</param>
        void Information(string message);

        /// <summary>
        /// Writes logs at INFO level
        /// </summary>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        void Information(string fmt, params object[] vars);

        /// <summary>
        /// Writes logs at INFO level
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be logged</param>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        void Information(Exception exception, string fmt, params object[] vars);

        /// <summary>
        /// Writes logs at WARN level
        /// </summary>
        /// <param name="message">Message to be logged</param>
        void Warning(string message);

        /// <summary>
        /// Writes logs at WARN level
        /// </summary>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        void Warning(string fmt, params object[] vars);

        /// <summary>
        /// Writes logs at WARN level
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be logged</param>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        void Warning(Exception exception, string fmt, params object[] vars);

        /// <summary>
        /// Writes logs at ERROR level
        /// </summary>
        /// <param name="message">Message to be logged</param>
        void Error(string message);

        /// <summary>
        /// Writes logs at ERROR level
        /// </summary>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        void Error(string fmt, params object[] vars);

        /// <summary>
        /// Writes logs at ERROR level
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be logged</param>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        void Error(Exception exception, string fmt, params object[] vars);
    }
}

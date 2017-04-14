using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Diagnostics;

namespace QuickSamplesCommon
{
    /// <summary>
    /// Definition for the class AzureDiagnosticLogger
    /// </summary>
    public class DiagnosticLogger : IPlatformServiceLogger
    {
        public bool HttpRequestResponseNeedsToBeLogged { get; set; }

        //
        // Warning - trace information within the application

        public void Information(string message)
        {
            Trace.TraceInformation(message);
        }

        public void Information(string fmt, params object[] vars)
        {
            Trace.TraceInformation(fmt, vars);
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            var msg = String.Format(fmt, vars);
            Trace.TraceInformation(string.Format(fmt, vars) + ";Exception Details={0}", ExceptionUtils.FormatException(exception, includeContext: true));
        }

        //
        // Warning - trace warnings within the application

        public void Warning(string message)
        {
            Trace.TraceWarning(message);
        }

        public void Warning(string fmt, params object[] vars)
        {
            Trace.TraceWarning(fmt, vars);
        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            var msg = String.Format(fmt, vars);
            Trace.TraceWarning(string.Format(fmt, vars) + ";Exception Details={0}", ExceptionUtils.FormatException(exception, includeContext: true));
        }

        //
        // Error - trace fatal errors within the application

        public void Error(string message)
        {
            Trace.TraceError(message);
        }

        public void Error(string fmt, params object[] vars)
        {
            Trace.TraceError(fmt, vars);
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            var msg = String.Format(fmt, vars);
            Trace.TraceError(string.Format(fmt, vars) + ";Exception Details={0}", ExceptionUtils.FormatException(exception, includeContext: true));
        }
    }
}

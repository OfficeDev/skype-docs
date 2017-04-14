using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// Provides utilities for the exception formats
    /// </summary>
    public static class ExceptionUtils
    {
        /// <summary>
        /// Formats the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>System.String.</returns>
        public static string FormatExceptionSimple(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            try
            {
                sb.AppendFormat("\r\n-----------\r\n{0}", ex.ToString());
            }
            catch (Exception ex0)
            {
                sb.AppendFormat("Warning; Could not format exception {0}", ex0.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// By default return Exception.ToString()
        /// </summary>
        private static readonly Func<Exception, string> defaultFormatter =
            (ex) => (ex == null) ? String.Empty : ex.ToString();

        #region "Specialized Formatters"
        /// <summary>
        /// Keep a map of specialized formatters for types with extra embedded
        /// information
        /// </summary>
        private static readonly IDictionary<Type, Func<Exception, string>>
            FormatExceptionMap = new Dictionary<Type, Func<Exception, string>>();

        #endregion

        /// <summary>
        /// Formats the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="includeContext">if set to <c>true</c> [include context].</param>
        /// <returns>System.String.</returns>
        public static string FormatException(Exception ex, bool includeContext = false)
        {
            if (ex == null)
                return String.Empty;
            var sb = new StringBuilder();
            try
            {
                // Whether or not to include the application and machine context
                if (includeContext)
                    AppendContext(sb);

                AppendExceptionInfo(sb, ex);
            }
            catch (Exception ex0)
            {
                sb.AppendFormat("Warning; Could not format exception {0}", ex0.ToString());
            }

            return sb.ToString();
        }

        private static void AppendExceptionInfo(StringBuilder sb, Exception exception)
        {
            Func<Exception, string> formatter = defaultFormatter;
            if (FormatExceptionMap.ContainsKey(exception.GetType()))
                formatter = FormatExceptionMap[exception.GetType()];

            sb.AppendFormat("\r\n------------------------------\r\n{0}",
                formatter(exception));
        }

        private static void AppendContext(StringBuilder sb)
        {
            var currentAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var lastWritten = File.GetLastWriteTime(currentAssembly.Location);

            sb.AppendFormat("[Context] assembly={0},version={1},buildTime={2},appDomain={3},basePath={4}",
                currentAssembly.FullName,
                currentAssembly.GetName().Version.ToString(),
                lastWritten,
                AppDomain.CurrentDomain.FriendlyName,
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        }
    }
}

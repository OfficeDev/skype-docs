using System;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// The logger class
    /// </summary>
    public class Logger
    {
        private IPlatformServiceLogger m_innerLogger;
        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());

        /// <summary>
        /// Avoid construct
        /// </summary>
        private Logger()
        {
        }

        private void RegisterInnerLogger(IPlatformServiceLogger logger)
        {
            m_innerLogger = logger;
        }

        /// <summary>
        /// Initializes the <see cref="Logger"/> with <paramref name="logger"/>
        /// </summary>
        /// <param name="logger"></param>
        public static void RegisterLogger(IPlatformServiceLogger logger)
        {
            Logger.Instance.RegisterInnerLogger(logger);
        }

        /// <summary>
        /// Gets the Logger Instance.
        /// </summary>
        public static Logger Instance
        {
            get { return instance.Value; }
        }

        /// <summary>
        /// Writes logs at INFO level
        /// </summary>
        /// <param name="message">Message to be logged</param>
        public void Information(string message)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Information(message);
            }
        }

        /// <summary>
        /// Writes logs at INFO level
        /// </summary>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        public void Information(string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Information(fmt, vars);
            }
        }

        /// <summary>
        /// Writes logs at INFO level
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be logged</param>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        public void Information(Exception exception, string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Information(exception, fmt, vars);
            }
        }

        /// <summary>
        /// Writes logs at WARN level
        /// </summary>
        /// <param name="message">Message to be logged</param>
        public void Warning(string message)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Warning(message);
            }
        }

        /// <summary>
        /// Writes logs at WARN level
        /// </summary>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        public void Warning(string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Warning(fmt, vars);
            }
        }

        /// <summary>
        /// Writes logs at WARN level
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be logged</param>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Warning(exception, fmt, vars);
            }
        }

        /// <summary>
        /// Writes logs at ERROR level
        /// </summary>
        /// <param name="message">Message to be logged</param>
        public void Error(string message)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Error(message);
            }
        }

        /// <summary>
        /// Writes logs at ERROR level
        /// </summary>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        public void Error(string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Error(fmt, vars);
            }
        }

        /// <summary>
        /// Writes logs at ERROR level
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to be logged</param>
        /// <param name="fmt">formatted string</param>
        /// <param name="vars">parameters for formatted string</param>
        public void Error(Exception exception, string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Error(exception, fmt, vars);
            }
        }

        /// <summary>
        /// Decides whether to write full HTTP requests and responses to logs
        /// </summary>
        public bool HttpRequestResponseNeedsToBeLogged
        {
            get
            {
                if(this.m_innerLogger != null)
                {
                    return m_innerLogger.HttpRequestResponseNeedsToBeLogged;
                }

                return false;
            }
        }
    }
}

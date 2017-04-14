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

        public void Information(string message)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Information(message);
            }
        }

        public void Information(string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Information(fmt, vars);
            }
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Information(exception, fmt, vars);
            }
        }

        public void Warning(string message)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Warning(message);
            }
        }

        public void Warning(string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Warning(fmt, vars);
            }
        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Warning(exception, fmt, vars);
            }
        }

        public void Error(string message)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Error(message);
            }
        }

        public void Error(string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Error(fmt, vars);
            }
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            if (this.m_innerLogger != null)
            {
                m_innerLogger.Error(exception, fmt, vars);
            }
        }

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

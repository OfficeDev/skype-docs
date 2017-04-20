using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.IO;

namespace QuickSamplesCommon
{
    public class SampleAppLogger : IPlatformServiceLogger
    {
        public bool HttpRequestResponseNeedsToBeLogged
        {
            get;
            set;
        }
        private bool m_enableFileLogging;
        private const string c_logPath = @".\log\applicationLog.log";
        public SampleAppLogger(bool enableFileLogging = false)
        {
            m_enableFileLogging = enableFileLogging;
            if (enableFileLogging &&  Directory.Exists(Path.GetDirectoryName(c_logPath)) && File.Exists(c_logPath) )
            {
                File.Delete(c_logPath);
            }
            else if(!Directory.Exists(Path.GetDirectoryName(c_logPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(c_logPath));
            }
            Console.WriteLine("Log file path is : " + c_logPath);

        }
        public void LogWrite(string logMessage)
        {
            using (StreamWriter w = (File.Exists(c_logPath) ? File.AppendText(c_logPath) : File.CreateText(c_logPath)))
            {
                Log(logMessage, w);
            }
            
        }
        public void Log(string logMessage, TextWriter txtWriter)
        {
            txtWriter.WriteLine("  :{0}", logMessage);
        }
        public void Information(string message)
        {
            Console.WriteLine("[INFO]" + message);
            if (m_enableFileLogging)
            {
                LogWrite("[INFO]" + message);
            }
        }

        public void Information(string fmt, params object[] vars)
        {
            Console.WriteLine("[INFO]" + string.Format(fmt, vars));
            if (m_enableFileLogging)
            {
                LogWrite("[INFO]" + string.Format(fmt, vars));
            }

        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            string msg = String.Format(fmt, vars);
            Console.WriteLine("[INFO]" + msg + "; \r\nException Details= ", ExceptionUtils.FormatException(exception, includeContext: true));
            if (m_enableFileLogging)
            {
                LogWrite("[INFO]" + msg + "; \r\nException Details= " + ExceptionUtils.FormatException(exception, includeContext: true));
            }

        }

        //
        // Warning - trace warnings within the application

        public void Warning(string message)
        {
            Console.WriteLine("[WARN]" + message);
            if (m_enableFileLogging)
            {
                LogWrite("[WARN]" + message);
            }

        }

        public void Warning(string fmt, params object[] vars)
        {
            Console.WriteLine("[WARN]" + string.Format(fmt, vars));
            if (m_enableFileLogging)
            {
                LogWrite("[WARN]" + string.Format(fmt, vars));
            }

        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            string msg = String.Format(fmt, vars);
            Console.WriteLine(msg + "; \r\nException Details= ", ExceptionUtils.FormatException(exception, includeContext: true));
            if (m_enableFileLogging)
            {
                LogWrite(msg + "; \r\nException Details= " + ExceptionUtils.FormatException(exception, includeContext: true));
            }

        }

        //
        // Error - trace fatal errors within the application

        public void Error(string message)
        {
            Console.WriteLine(message);
            if (m_enableFileLogging)
            {
                LogWrite(message);
            }

        }

        public void Error(string fmt, params object[] vars)
        {
            Console.WriteLine("[ERROR]" + String.Format(fmt, vars));
            if (m_enableFileLogging)
            {
                LogWrite("[ERROR]" + String.Format(fmt, vars));
            }

        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            string msg = String.Format(fmt, vars);
            Console.WriteLine("[ERROR]" + msg + "; \r\nException Details= ", ExceptionUtils.FormatException(exception, includeContext: true));
            if (m_enableFileLogging)
            {
                LogWrite("[ERROR]" + msg + "; \r\nException Details= " + ExceptionUtils.FormatException(exception, includeContext: true));
            }

        }
    }
}

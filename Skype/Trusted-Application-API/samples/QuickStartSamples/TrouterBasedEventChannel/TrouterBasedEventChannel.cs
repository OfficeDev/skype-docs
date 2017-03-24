using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Skype.Calling.ServiceAgents.Trouter;

namespace TrouterCommon
{
    /// <summary>
    /// Trusted Application API uses callbacks to deliver notifications (events) to the application. If the scenario supported by the application
    /// requires callbacks, developer needs to deploy the application before they can even test it. One way to make development of such applications
    /// easier is by using Trouter.
    /// <para>
    /// Trouter is an online service which has two interfaces : 
    /// <ol>
    /// <li>HTTP server where Skype for Business's service will POST all the notifications</li>
    /// <li>a persistent connection where your application can connect and receive all those notifications</li>
    /// </ol>
    /// Trouter provides dlls to manage connection with the service.
    /// </para>
    /// Please keep in mind that Trouter is not a production service and this method should only be used while developing/debugging your service.
    /// </summary>
    public class TrouterBasedEventChannel : IEventChannel
    {
        private readonly TrouterServiceHost m_trouterServiceHost;

        /// <summary>
        /// Trouter's home url
        /// </summary>
        private const string c_trouterUrl = "wss://go.trouter.io/v3/c";

        /// <summary>
        /// Url to Trouter's policies
        /// </summary>
        private const string c_tpcUrl = "https://prod.tpc.skype.com/v1/policies";

        public event EventHandler<EventsChannelArgs> HandleIncomingEvents;

        public string CallbackUri { get; private set; }

        private readonly IPlatformServiceLogger m_logger;

        /// <summary>
        /// Create an <see cref="IEventChannel"/> which uses Trouter to receive callbacks
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="token">Token to be used for connection with Trouter</param>
        /// <param name="userAgent">User agent's name</param>
        public TrouterBasedEventChannel(IPlatformServiceLogger logger, string token, string userAgent)
        {
            m_logger = logger;

            LoadNativeTrouterAssemblies(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            m_trouterServiceHost = new TrouterServiceHost(
                skypetoken: token,
                trouterUrl: c_trouterUrl,
                tpcUrl: c_tpcUrl,
                userAgent: userAgent,
                clientVersion: Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        /// <summary>
        /// Starts the event channel
        /// </summary>
        public async Task TryStartAsync()
        {
            string callbackPrefix = Guid.NewGuid().ToString();
            ITrouterRequestReceiver trouterRequestReceiver = await m_trouterServiceHost.RegisterPrefix(callbackPrefix).ConfigureAwait(false);

            trouterRequestReceiver.ProcessRequest += ProcessIncomingTrouterRequestAsync;
            trouterRequestReceiver.OnTrouterUriChanged += (sender, args) => CallbackUri = args.NewTrouterUri.ToString();

            CallbackUri = trouterRequestReceiver.TrouterUri.ToString();
        }

        /// <summary>
        /// Handles the http request received through Trouter
        /// </summary>
        /// <param name="requestReceived">Http request to be processed</param>
        /// <returns><see cref="HttpResponseMessage"/> to be sent in response of the request</returns>
        private async Task<HttpResponseMessage> ProcessIncomingTrouterRequestAsync(RequestReceived requestReceived)
        {
            try
            {
                m_logger.Information("Incoming callback\r\n " + requestReceived.Body);

                var message = new SerializableHttpRequestMessage();
                message.Method = requestReceived.HttpMethod;
                message.Uri = requestReceived.RelativeUri;
                message.LoggingContext = new LoggingContext(Guid.NewGuid());
                message.Content = requestReceived.Body;
                message.ContentType = "application/json";
                message.IsIncoming = true;
                message.Timestamp = DateTime.UtcNow;

                if (requestReceived.Headers != null)
                {
                    message.RequestHeaders = new List<Tuple<string, string>>();
                    foreach (KeyValuePair<string, string> header in requestReceived.Headers)
                    {
                        if (!string.IsNullOrEmpty(header.Key) && !string.IsNullOrEmpty(header.Value))
                        {
                            message.RequestHeaders.Add(new Tuple<string, string>(header.Key, header.Value));
                        }
                    }
                }

                // Suppress the warning
                await Task.CompletedTask.ConfigureAwait(false);

                // Pass down the response to event channel
                HandleIncomingEvents?.Invoke(this, new EventsChannelArgs(message));
            }
            catch (Exception ex)
            {
                Debug.Assert(false, "unexpected exception: " + ex.ToString());
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Stops the event channel
        /// </summary>
        /// <returns></returns>
        public async Task TryStopAsync()
        {
            m_trouterServiceHost.Dispose();

            await Task.CompletedTask.ConfigureAwait(false);
        }

        #region Helpers to load trouter dlls

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string libname);

        /// <summary>
        /// Loads all native assemblies required for Trouter client
        /// </summary>
        /// <param name="rootApplicationPath">
        /// Root path of the current application. Use Server.MapPath(".") for ASP.NET applications
        /// and AppDomain.CurrentDomain.BaseDirectory for desktop applications.
        /// </param>
        public static void LoadNativeTrouterAssemblies(string rootApplicationPath)
        {
            LoadNativeAssembly(rootApplicationPath, "CLITrouterClient.dll");
            LoadNativeAssembly(rootApplicationPath, "DLLTrouterClient.dll");
        }

        /// <summary>
        /// Loads a native dll
        /// </summary>
        /// <param name="nativeBinaryPath">Directory containing the dll</param>
        /// <param name="assemblyName">Name of the dll (must have .dll extension)</param>
        /// <exception cref="Exception">An exception is thrown when dlls can't be loaded</exception>
        private static void LoadNativeAssembly(string nativeBinaryPath, string assemblyName)
        {
            var path = Path.Combine(nativeBinaryPath, assemblyName);
            var ptr = LoadLibrary(path);
            if (ptr == IntPtr.Zero)
            {
                throw new Exception(string.Format("Error loading {0} (ErrorCode: {1})", assemblyName, Marshal.GetLastWin32Error()));
            }
        }

        #endregion
    }
}

using System;
using System.Web.Http;
using Owin;
using QuickSamplesCommon;

namespace WebEventChannel
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        // Since this class is part of WebEventChannel, you can't add more controllers here directly.
        // If you need to add more controllers, do these:
        //  1. Move this class to your project
        //  2. Create an equivalent class here (in WebEventChannel) which takes HttpConfiguration as
        //     input and registers the callback route
        /// <summary>
        /// Configures IAppBuilder to route all requests to /callback to <see cref="CallbackController"/>
        /// </summary>
        /// <param name="appBuilder"><see cref="IAppBuilder"/> where routes need to be registered</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            // Get the path where we want to listen for callbacks
            // e.g. if myCallbackUri is https://mydevbox.contoso.com/mycallback,
            // We want to add CallbackController on "mycallback" path
            var myCallbackUri = new Uri(QuickSamplesConfig.MyCallbackUri);

            var callbackPath = myCallbackUri.AbsolutePath.Substring(1); // Substring(1) to remove the first forward slash

            // Configure Web API for self-host.
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "CallbackRoute",     // This is just a name and doesn't affect routing
                routeTemplate: callbackPath, // only match /callback urls
                defaults: new { controller = "Callback" } // send matching requests to CallbackController
            );

            appBuilder.UseWebApi(config);
        }
    }
}

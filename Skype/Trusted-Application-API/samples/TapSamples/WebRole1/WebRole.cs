using System;
using System.Linq;
using Microsoft.Web.Administration;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    /// <summary>
    /// Class to customize the IIS web role.
    /// </summary>
    public class WebRole : RoleEntryPoint
    {
        /// <summary>
        /// Configure the web role when it is starting up.
        /// </summary>
        /// <returns><code>true</code> on success</returns>
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            var serverManager = new ServerManager();

            Site site = serverManager.Sites.First();
            Application app = site.Applications.First();
            ApplicationPool appPool = serverManager.ApplicationPools[app.ApplicationPoolName];

            // Auto start app pool; do not wait for the first request to arrive.
            appPool.AutoStart = true;
            appPool["startMode"] = "AlwaysRunning";

            // Pre load the application
            app["preLoadEnabled"] = true;

            // Suspend app pool in place of terminating it on idle timeout.
            // This means we will not be using resources and still not lose our state when idle.
            // (0 = Terminate / 1 = Suspend)
            appPool.ProcessModel["idleTimeoutAction"] = 1;

            // Disable overlapping recycling
            appPool.Recycling.DisallowOverlappingRotation = true;

            // Disable automatic pool recycling
            appPool.Recycling.PeriodicRestart.Time = TimeSpan.FromSeconds(0);

            // Commit all changes
            serverManager.CommitChanges();

            return base.OnStart();
        }
    }
}

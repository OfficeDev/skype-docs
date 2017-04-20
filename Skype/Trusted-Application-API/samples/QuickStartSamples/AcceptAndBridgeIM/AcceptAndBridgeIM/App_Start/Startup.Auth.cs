using System.Collections.Generic;
using Owin;
using System.Configuration;
using Microsoft.Owin.Security.ActiveDirectory;
using System.IdentityModel.Tokens;
namespace AcceptAndBridgeIM
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            string audienceUri = ConfigurationManager.AppSettings["MyAudienceUri"];
            string endpointId = ConfigurationManager.AppSettings["ApplicationEndpointId"];
            string tenant = endpointId.Split('@')[1];
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(new WindowsAzureActiveDirectoryBearerAuthenticationOptions
            {
                //If tenant is assigned  and ValidateIssuer = true, only this tenant is allowed to pass through
                //If tenant is assigned "common",   ValidateIssuer = true, multitenant allowed
                Tenant = tenant,         
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidAudiences = new List<string> { audienceUri }                     
                }
            });
        }
    }
}

using Microsoft.Azure;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    public class AADAppAuthorizeAttribute : AuthorizeAttribute
    {
        private string aadApplicationIdKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="AADAppAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="aadApplicationIdKey">The aad Application Id.</param>
        public AADAppAuthorizeAttribute(string aadApplicationIdKey)
        {
            if (string.IsNullOrWhiteSpace(aadApplicationIdKey))
            {
                throw new ArgumentNullException("aadApplicationId", "The parameter named aadApplicationId should not be null, empty or white space.");
            }

            this.aadApplicationIdKey = aadApplicationIdKey;
        }

        /// <summary>Indicates whether the specified control is authorized.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        /// <returns>true if the control is authorized; otherwise, false.</returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var isAuthorized = base.IsAuthorized(actionContext);
            if (isAuthorized)
            {
                isAuthorized = false;
                ClaimsIdentity claimsIdentity = null;
                var requestContext = actionContext.RequestContext;
                if (requestContext != null && requestContext.Principal != null)
                {
                    claimsIdentity = requestContext.Principal.Identity as ClaimsIdentity;
                }

                if (claimsIdentity != null && claimsIdentity.Claims != null && claimsIdentity.Claims.Any())
                {
                    var applicationIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == Constants.ApplicationIdClaim);
                    if (applicationIdClaim != null)
                    {
                        var aadApplicationId = CloudConfigurationManager.GetSetting(this.aadApplicationIdKey);
                        isAuthorized = string.Equals(aadApplicationId, applicationIdClaim.Value, StringComparison.OrdinalIgnoreCase);
                    }
                }
            }

            return isAuthorized;
        }

        /// <summary>
        /// Processes requests that fail authorization.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var tracingId = actionContext.Request.GetCorrelationId();
            base.HandleUnauthorizedRequest(actionContext);
            if (actionContext.Response != null)
            {
                actionContext.Response.Headers.Add(Constants.TrackingIdHeaderName, tracingId.ToString());
            }
        }
    }
}
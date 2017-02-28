using Newtonsoft.Json;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// The Logging Context Action Filter Attribute.
    /// </summary>
    public class LoggingContextActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the Query Parameter Name.
        /// </summary>
        public string QueryParameterName { get; set; }

        /// <summary>
        /// Occurs before the action method is invoking.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The task.</returns>
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var actionDescriptor = actionContext.ActionDescriptor;
            var httpRequestMessage = actionContext.Request;

            var loggingContext = new LoggingContext(httpRequestMessage.GetCorrelationId());

            httpRequestMessage.Properties.Add(Constants.LoggingContext, loggingContext);
            if (!string.IsNullOrWhiteSpace(this.QueryParameterName))
            {
                var queryStringCollection = httpRequestMessage.RequestUri.ParseQueryString();
                CallbackContext callbackContext = null;
                var callbackContextString = queryStringCollection[this.QueryParameterName];
                if (!string.IsNullOrWhiteSpace(callbackContextString))
                {
                    try
                    {
                        callbackContext = JsonConvert.DeserializeObject<CallbackContext>(callbackContextString);
                    }
                    catch(Exception ex)
                    {
                        Logger.Instance.Error(ex, "[LoggingContextActionFilterAttribute] error in deserializing callback context!");
                    }

                    if (callbackContext != null)
                    {
                        httpRequestMessage.Properties.Add(Constants.CallbackContext, callbackContext);
                        loggingContext.FillFromCallbackContext(callbackContext);
                    }
                }

                loggingContext.FillTracingInfoFromHeaders(httpRequestMessage.Headers, true);
            }
            return Task.FromResult<string>(string.Empty);
        }

        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var tracingId = actionExecutedContext.Request.GetCorrelationId();
            if (actionExecutedContext.Exception != null && actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.Add(Constants.TrackingIdHeaderName, tracingId.ToString());
            }
        }
    }
}
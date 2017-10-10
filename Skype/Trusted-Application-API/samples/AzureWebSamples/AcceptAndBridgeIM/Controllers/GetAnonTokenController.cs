using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace AcceptAndBridgeIM
{    
    public class GetAnonTokenController: ApiController
    {
        public async Task<HttpResponseMessage> PostAsync(GetAnonTokenInput input)
        {
            if (string.IsNullOrEmpty(input.ApplicationSessionId))
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"No or invalid callback context specified!\"}");
            }

            if (string.IsNullOrEmpty(input.AllowedOrigins))
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid AllowedOrigins\"}");
            }

            string jobId = Guid.NewGuid().ToString("N");

            try
            {
                AnonymousApplicationTokenInput anoninput = new AnonymousApplicationTokenInput()
                {
                    AllowedOrigins = input.AllowedOrigins,
                    ApplicationSessionId = input.ApplicationSessionId                    
                };

                var LoggingContext = new LoggingContext(jobId, "");

                IAnonymousApplicationToken token = await WebApiApplication.ApplicationEndpoint.Application.GetAnonApplicationTokenForP2PCallAsync(anoninput.AllowedOrigins,anoninput.ApplicationSessionId, LoggingContext).ConfigureAwait(false);

                if (token == null)
                {
                    return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Returned token is null\"}");
                }

                AnonymousToken result = new AnonymousToken
                {
                    DiscoverUri = token.AnonymousApplicationsDiscoverUri.AbsoluteUri,
                    ExpireTime = token.AuthTokenExpiryTime,
                    Token = token.AuthToken,
                    TenantEndpointId = WebApiApplication.ApplicationEndpoint.ApplicationEndpointId.ToString()
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, "Exception while scheduling job.");                
                return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Hit exception when running the job\"}");
            }
        }

        protected HttpResponseMessage CreateHttpResponse(HttpStatusCode statusCode, string message)
        {
            var response = new HttpResponseMessage(statusCode);
            if (message != null)
            {
                response.Content = new StringContent(message);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            return response;
        }


    }
}

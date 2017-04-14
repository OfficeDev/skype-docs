using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace WebEventChannel
{
    public class CallbackController : ApiController
    {
        public async Task Post(HttpRequestMessage request)
        {
            // CallbackController can't implement IEventChannel as ASP.Net creates new instance of the
            // controller everytime a new request is received.

            var message = new SerializableHttpRequestMessage();
            await message.InitializeAsync(request, null).ConfigureAwait(false);

            // Pass down the response to event channel
            WebEventChannel.Instance.HandleIncomingHttpRequest(message);
        }
    }
}

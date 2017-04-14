using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// Helper Class for  Http Messages.
    /// </summary>
    public static class HttpMessageHelper
    {
        /// <summary>
        /// Get value from headers in http request and response.
        /// </summary>
        /// <param name="httpHeaders">The http headers.</param>
        /// <param name="headerName">The header name which you look for.</param>
        /// <returns>The header value.</returns>
        public static string GetHttpHeaderValue(HttpHeaders httpHeaders, string headerName)
        {
            var result = string.Empty;
            if (httpHeaders == null)
            {
                return result;
            }

            if (httpHeaders != null && !string.IsNullOrWhiteSpace(headerName))
            {
                IEnumerable<string> headerValues;
                httpHeaders.TryGetValues(headerName, out headerValues);
                if (headerValues?.Any() == true)
                {
                    result = headerValues.First();
                }
            }

            return result;
        }

        /// <summary>
        /// Formats the HTTP content as an asynchronous operation.
        /// </summary>
        /// <param name="sb">a <see cref="System.Text.StringBuilder"/>.</param>
        /// <param name="content">The content of the http message.</param>
        /// <returns>Task.</returns>
        public static async Task FormatHttpContentAsync(StringBuilder sb, HttpContent content)
        {
            if (content.Headers != null)
            {
                foreach (var header in content.Headers)
                {
                    sb.AppendLine(string.Format("{0} : {1}", header.Key, header.Value.First()));
                }
            }

            await content.LoadIntoBufferAsync().ConfigureAwait(false);
            bool isMultipart = content.IsMimeMultipartContent();
            if (isMultipart)
            {
                //TODO: for some reason it hangs when trying to read the multi-part.  might have something to do with UI thread :(

                //MultipartMemoryStreamProvider provider = await content.ReadAsMultipartAsync();

                //TODO: for now format just the first part.  We will need to figure out what to do with the remain
                //parts so that it doesnt break viewer
                //content = provider.Contents.First() as StreamContent;
            }
            var sw = new StringWriter(sb);
            var writer = new JsonTextWriter(sw)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };

            string contentBody = await content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                var jsonSerializer = new JsonSerializer();
                var reader = new JsonTextReader(new StringReader(contentBody));

                object obj = jsonSerializer.Deserialize(reader);
                if (obj != null)
                {
                    jsonSerializer.Serialize(writer, obj);
                }
                else
                {
                    sb.AppendLine(contentBody);
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine(contentBody);
                Logger.Instance.Warning("JsonWriter failed with ex: " + ex.ToString());
            }
            finally
            {
                writer.Close();
                sw.Close();
            }
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// The Uri Helper.
    /// </summary>
    public static class UriHelper
    {
        private static readonly Regex emailRegex = new Regex(Constants.EmailRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Generate an absolute uri.
        /// </summary>
        /// <param name="baseUri">The base uri.</param>
        /// <param name="relativeOrAbsoluteUri">The uri which may be relative or absolute.</param>
        /// <returns>The absolute uri.</returns>
        public static Uri CreateAbsoluteUri(Uri baseUri, string relativeOrAbsoluteUri)
        {
            Uri result;
            if (baseUri == null)
            {
                throw new ArgumentNullException(nameof(baseUri), "The parameter named baseUri can't  be null.");
            }

            if (string.IsNullOrWhiteSpace(relativeOrAbsoluteUri))
            {
                throw new ArgumentNullException("relativeOrAbsoluteUrl", "The parameter named relativeOrAbsoluteUrl can't  be null, empty and whitespace.");
            }

            if (Uri.IsWellFormedUriString(relativeOrAbsoluteUri, UriKind.Absolute))
            {
                result = new Uri(relativeOrAbsoluteUri);
            }
            else
            {
                if (!Uri.TryCreate(baseUri, relativeOrAbsoluteUri, out result))
                {
                    throw new InvalidCastException("Can't use baseUri and relativeOrAbsoluteUri to create an absolute uri.");
                }
            }

            return result;
        }

        /// <summary>
        /// Generate an absolute uri.
        /// </summary>
        /// <param name="sip">The sip uri.</param>
        /// <returns>The value indicating the input is sip uri or not.</returns>
        public static bool IsSipUri(string sip)
        {
            if (string.IsNullOrWhiteSpace(sip))
            {
                return false;
            }

            Uri sipUri;
            if (!Uri.TryCreate(sip, UriKind.Absolute, out sipUri))
            {
                return false;
            }

            if (!string.Equals(sipUri.Scheme, Constants.SipScheme, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return emailRegex.IsMatch(sipUri.PathAndQuery);
        }

        public static string StripQueryParametersFromHref(string href)
        {
            string tempHref = href;

            int queryParamIndex = href.LastIndexOf('?');
            if (queryParamIndex >= 0)
            {
                //drop the query parameters
                tempHref = href.Substring(0, queryParamIndex);
            }

            return tempHref;
        }

        public static string NormalizeUriWithNoQueryParameters(string href, Uri baseUri)
        {
            string hrefWithNoquery = StripQueryParametersFromHref(href);
            return CreateAbsoluteUri(baseUri, href).ToString().ToLower();
        }

        public static string NormalizeUri(string href, Uri baseUri)
        {
            return CreateAbsoluteUri(baseUri, href).ToString().ToLower();
        }

        public static Uri GetBaseUriFromAbsoluteUri(string absoluteuri)
        {
            if (!Uri.IsWellFormedUriString(absoluteuri, UriKind.Absolute))
            {
                throw new ArgumentException("Input " + absoluteuri + " is not wel formed absolute Uri!");
            }

            Uri baseUri = null;

            if (!Uri.TryCreate(new Uri(absoluteuri).GetLeftPart(UriPartial.Authority), UriKind.Absolute, out baseUri))
            {
                throw new ArgumentException("Cannot get base uri from Input " + absoluteuri);
            }

            return baseUri;
        }

        public static Uri AppendQueryParameterOnUrl(string url, string key, string value, bool httpUrlEncodeValue = true)
        {
            UriBuilder baseUri = new UriBuilder(url);
            string queryToAppend = string.Format("{0}={1}", key, httpUrlEncodeValue? System.Web.HttpUtility.UrlEncode(value) : value);

            if (baseUri.Query?.Length > 1)
                baseUri.Query = baseUri.Query.Substring(1) + "&" + queryToAppend;
            else
                baseUri.Query = queryToAppend;
            return baseUri.Uri;
        }
    }
}

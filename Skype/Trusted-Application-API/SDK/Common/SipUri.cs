using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    [Serializable]
    public class SipUri : Uri
    {
        public string Domain
        {
            get { return ToString().Split('@')[1]; }
        }

        private static readonly Regex EmailRegex = new Regex(Constants.EmailRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public SipUri(string uriString) : base(uriString)
        {
            Initialize();
        }

        #pragma warning disable CS0618 // Type or member is obsolete
        public SipUri(string uriString, bool dontEscape) : base(uriString, dontEscape)
        #pragma warning restore CS0618 // Type or member is obsolete
        {
            Initialize();
        }

        #pragma warning disable CS0618 // Type or member is obsolete
        public SipUri(Uri baseUri, string relativeUri, bool dontEscape) : base(baseUri, relativeUri, dontEscape)
        #pragma warning restore CS0618 // Type or member is obsolete
        {
            Initialize();
        }

        public SipUri(string uriString, UriKind uriKind) : base(uriString, uriKind)
        {
            Initialize();
        }

        public SipUri(Uri baseUri, string relativeUri) : base(baseUri, relativeUri)
        {
            Initialize();
        }

        public SipUri(Uri baseUri, Uri relativeUri) : base(baseUri, relativeUri)
        {
            Initialize();
        }

        protected SipUri(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!string.Equals(Scheme, Constants.SipScheme, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Only sip: scheme is allowed, provided : " + Scheme);
            }

            if(!EmailRegex.IsMatch(PathAndQuery))
            {
                throw new ArgumentException(PathAndQuery + " is not a valid email address.");
            }
        }
    }
}

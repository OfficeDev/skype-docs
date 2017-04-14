// ***********************************************************************
// Assembly         : Microsoft.SfB.PlatformService.SDK.Common
// Author           : yuou
// Created          : 03-22-2017
//
// Last Modified By : yuou
// Last Modified On : 03-22-2017
// ***********************************************************************
// <copyright file="Constants.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// Class Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The email regex
        /// </summary>
        public const string EmailRegex = "^((([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+(\\.([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?$";
        /// <summary>
        /// The sip scheme
        /// </summary>
        public const string SipScheme = "sip";
        /// <summary>
        /// The local endpoint
        /// </summary>
        public const string LocalEndpoint = "LocalEndpoint";
        /// <summary>
        /// The original token
        /// </summary>
        public const string OriginalToken = "OriginalToken";
        /// <summary>
        /// The partner service retry strategy
        /// </summary>
        public const string PartnerServiceRetryStrategy = "PartnerServiceRetryStrategy";
        /// <summary>
        /// The remote platform service URI
        /// </summary>
        public const string RemotePlatformServiceUri = "RemotePlatformServiceUri";
        /// <summary>
        /// The platform event received time
        /// </summary>
        public const string PlatformEventReceivedTime = "PlatformEventReceivedTime";
        /// <summary>
        /// The platform events correlation identifier
        /// </summary>
        public const string PlatformEventsCorrelationId = "PlatformEventsCorrelationId";
        /// <summary>
        /// The platform events client request identifier
        /// </summary>
        public const string PlatformEventsClientRequestId = "PlatformEventsClientRequestId";
        /// <summary>
        /// The platform events server FQDN
        /// </summary>
        public const string PlatformEventsServerFqdn = "PlatformEventsServerFqdn";
        /// <summary>
        /// The platform response correlation identifier
        /// </summary>
        public const string PlatformResponseCorrelationId = "PlatformResponseCorrelationId";
        /// <summary>
        /// The platform response server FQDN
        /// </summary>
        public const string PlatformResponseServerFqdn = "PlatformResponseServerFqdn";
        /// <summary>
        /// The platform events
        /// </summary>
        public const string PlatformEvents = "PlatformEvents";
        /// <summary>
        /// The unknown
        /// </summary>
        public const string Unknown = "Unknown";
        /// <summary>
        /// The platform applications URI format
        /// </summary>
        public const string PlatformApplicationsUriFormat = "{0}?endpointId={1}";
        /// <summary>
        /// The tenant identifier claim
        /// </summary>
        public const string TenantIdClaim = "http://schemas.microsoft.com/identity/claims/tenantid";
        /// <summary>
        /// The application identifier claim
        /// </summary>
        public const string ApplicationIdClaim = "appid";
        /// <summary>
        /// The skype for business application identifier
        /// </summary>
        public const string SkypeForBusinessApplicationId = "SkypeForBusinessApplicationId";
        /// <summary>
        /// The conversation extension queuing operation context format
        /// </summary>
        public const string ConversationExtensionQueuingOperationContextFormat = "{0}&{1}";
        /// <summary>
        /// The sip format
        /// </summary>
        public const string SipFormat = "sip:{0}";
        /// <summary>
        /// The participants resource expand
        /// </summary>
        public const string ParticipantsResourceExpand = "expand";
        /// <summary>
        /// The endpoint identifier
        /// </summary>
        public const string EndpointId = "endpointId";
        /// <summary>
        /// The storage connection
        /// </summary>
        public const string STORAGE_CONNECTION = "Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString";
        /// <summary>
        /// The servicebus connection
        /// </summary>
        public const string SERVICEBUS_CONNECTION = "Microsoft.ServiceBus.ConnectionString";
        /// <summary>
        /// The job metadata table name
        /// </summary>
        public const string JOB_METADATA_TABLE_NAME = "platformservicenotificationjobmetadata";

        #region PlatformService Constants
        /// <summary>
        /// The ucap ms correlation identifier
        /// </summary>
        public const string UcapMsCorrelationId = "X-MS-Correlation-Id";
        /// <summary>
        /// The ucap ms server FQDN
        /// </summary>
        public const string UcapMsServerFqdn = "X-MS-Server-Fqdn";
        /// <summary>
        /// The ucap client request identifier
        /// </summary>
        public const string UcapClientRequestId = "client-request-id";
        /// <summary>
        /// The text plain content type
        /// </summary>
        public const string TextPlainContentType = "text/plain";
        /// <summary>
        /// The text HTML content type
        /// </summary>
        public const string TextHtmlContentType = "text/html";
        /// <summary>
        /// The text json content type
        /// </summary>
        public const string TextJsonContentType = "text/json";
        /// <summary>
        /// The conversation extension service name
        /// </summary>
        public const string ConversationExtensionServiceName = "InviteToConference";
        /// <summary>
        /// The is rejoin
        /// </summary>
        public const string IsRejoin = "IsRejoin";
        /// <summary>
        /// The etag
        /// </summary>
        public const string Etag = "etag";

        // Misc
        /// <summary>
        /// The base URI
        /// </summary>
        public const string BaseUri = "baseuri";
        /// <summary>
        /// The sender
        /// </summary>
        public const string Sender = "sender";
        /// <summary>
        /// The events
        /// </summary>
        public const string Events = "events";
        /// <summary>
        /// The prefix
        /// </summary>
        public const string Prefix = "ms:rtc:saas:";
        /// <summary>
        /// The link
        /// </summary>
        public const string Link = "link";
        /// <summary>
        /// The links
        /// </summary>
        public const string Links = "_links";
        /// <summary>
        /// The embedded
        /// </summary>
        public const string Embedded = "_embedded";
        /// <summary>
        /// The relative
        /// </summary>
        public const string Rel = "rel";
        /// <summary>
        /// The href
        /// </summary>
        public const string Href = "href";
        /// <summary>
        /// The self
        /// </summary>
        public const string Self = "self";
        /// <summary>
        /// The direction
        /// </summary>
        public const string Direction = "direction";
        /// <summary>
        /// The importance
        /// </summary>
        public const string Importance = "importance";
        /// <summary>
        /// The operation identifier
        /// </summary>
        public const string OperationId = "operationId";
        /// <summary>
        /// The state
        /// </summary>
        public const string State = "state";
        /// <summary>
        /// The subject
        /// </summary>
        public const string Subject = "subject";
        /// <summary>
        /// The thread identifier
        /// </summary>
        public const string ThreadId = "threadId";
        /// <summary>
        /// The HTML message
        /// </summary>
        public const string HtmlMessage = "htmlMessage";
        /// <summary>
        /// The plain message
        /// </summary>
        public const string PlainMessage = "plainMessage";
        /// <summary>
        /// The time stamp
        /// </summary>
        public const string TimeStamp = "timeStamp";
        /// <summary>
        /// The reason
        /// </summary>
        public const string Reason = "reason";
        /// <summary>
        /// The failure
        /// </summary>
        public const string Failure = "Failure";
        /// <summary>
        /// The status
        /// </summary>
        public const string Status = "status";
        /// <summary>
        /// The on behalf of
        /// </summary>
        public const string OnBehalfOf = "onBehalfOf";
        /// <summary>
        /// The callback context
        /// </summary>
        public const string CallbackContext = "callbackContext";
        /// <summary>
        /// The logging context
        /// </summary>
        public const string LoggingContext = "LoggingContext";
        /// <summary>
        /// The input
        /// </summary>
        public const string Input = "input";

        // Conversation
        /// <summary>
        /// The active modalities
        /// </summary>
        public const string ActiveModalities = "activeModalities";

        // Messaging Modality
        /// <summary>
        /// The negotiated message formats
        /// </summary>
        public const string NegotiatedMessageFormats = "negotiatedMessageFormats";
        /// <summary>
        /// The content
        /// </summary>
        public const string Content = "content";
        /// <summary>
        /// The content type
        /// </summary>
        public const string ContentType = "contentType";

        // Participant
        /// <summary>
        /// The anonymous
        /// </summary>
        public const string Anonymous = "anonymous";
        /// <summary>
        /// The local
        /// </summary>
        public const string Local = "local";
        /// <summary>
        /// The in lobby
        /// </summary>
        public const string InLobby = "inLobby";
        /// <summary>
        /// The name
        /// </summary>
        public const string Name = "name";
        /// <summary>
        /// The organizer
        /// </summary>
        public const string Organizer = "organizer";
        /// <summary>
        /// The other phone number
        /// </summary>
        public const string OtherPhoneNumber = "otherPhoneNumber";
        /// <summary>
        /// The phone number
        /// </summary>
        public const string PhoneNumber = "phoneNumber";
        /// <summary>
        /// The role
        /// </summary>
        public const string Role = "role";
        /// <summary>
        /// The source network
        /// </summary>
        public const string SourceNetwork = "sourceNetwork";
        /// <summary>
        /// The URI
        /// </summary>
        public const string Uri = "uri";
        /// <summary>
        /// The title
        /// </summary>
        public const string Title = "title";

        // AudioVideo
        /// <summary>
        /// The small media offer
        /// </summary>
        public const string SmallMediaOffer = "mediaOffer";

        // Conversation Externsion Multipart
        /// <summary>
        /// The conversation externsion multipart content identifier
        /// </summary>
        public const string ConversationExternsionMultipartContentId = "Content-ID";
        /// <summary>
        /// The conversation externsion multipart content
        /// </summary>
        public const string ConversationExternsionMultipartContent = "content";
        /// <summary>
        /// The conversation externsion multipart content position format
        /// </summary>
        public const string ConversationExternsionMultipartContentPositionFormat = "CID:{0}";
        /// <summary>
        /// The conversation externsion service name
        /// </summary>
        public const string ConversationExternsionServiceName = "serviceName";
        /// <summary>
        /// The conversation externsion multipart service URL
        /// </summary>
        public const string ConversationExternsionMultipartServiceUrl = "serviceUrl";
        /// <summary>
        /// The conversation externsion multipart json content type parameter
        /// </summary>
        public const string ConversationExternsionMultipartJsonContentTypeParameter = "multipart/related; type=\"application/json\"";

        //Logging purpose
        /// <summary>
        /// Incoming request - start tag
        /// </summary>
        public const string NotifierInboundHttpRequestStart = ">>>>>NotifierInboundHttpRequestStart";
        /// <summary>
        /// Incoming request - end tag
        /// </summary>
        public const string NotifierInboundHttpRequestEnd = "<<<<<NotifierInboundHttpRequestEnd";
        /// <summary>
        /// Response to the incoming request - start tag
        /// </summary>
        public const string NotifierInboundHttpRequestResponseStart = ">>>>>NotifierInboundHttpRequestResponseStart";
        /// <summary>
        /// Response to the incoming request - end tag
        /// </summary>
        public const string NotifierInboundHttpRequestResponseEnd = "<<<<<NotifierInboundHttpRequestResponseEnd";

        /// <summary>
        /// The transport outbound HTTP request start
        /// </summary>
        public const string TransportOutboundHttpRequestStart = ">>>>>TransportOutboundHttpRequestStart";
        /// <summary>
        /// The transport outbound HTTP request end
        /// </summary>
        public const string TransportOutboundHttpRequestEnd = "<<<<<TransportOutboundHttpRequestEnd";
        /// <summary>
        /// The transport outbound HTTP request response start
        /// </summary>
        public const string TransportOutboundHttpRequestResponseStart = ">>>>>TransportOutboundHttpRequestResponseStart";
        /// <summary>
        /// The transport outbound HTTP request response end
        /// </summary>
        public const string TransportOutboundHttpRequestResponseEnd = "<<<<<TransportOutboundHttpRequestResponseEnd";
        #endregion

        #region Http Header Names

        /// <summary>
        /// The error code header name
        /// </summary>
        public const string ErrorCodeHeaderName = "x-ms-error-code";
        /// <summary>
        /// The tracking identifier header name
        /// </summary>
        public const string TrackingIdHeaderName = "x-ms-tracking-id";
        /// <summary>
        /// The visit identifier header name
        /// </summary>
        public const string VisitIdHeaderName = "x-ms-visit-id";
        /// <summary>
        /// The engagement identifier header name
        /// </summary>
        public const string EngagementIdHeaderName = "x-ms-engagement-id";
        /// <summary>
        /// The partner identifier header name
        /// </summary>
        public const string PartnerIdHeaderName = "x-ms-partner-id";

        #endregion

        #region parameters
        /// <summary>
        /// The aad metadata URI
        /// </summary>
        public const string AAD_MetadataUri = "https://login.windows.net/common/FederationMetadata/2007-06/FederationMetadata.xml";
        /// <summary>
        /// The aad authority URI
        /// </summary>
        public const string AAD_AuthorityUri = "https://login.windows.net";
        /// <summary>
        /// The skype for business application client identifier
        /// </summary>
        public const string SkypeForBusinessApplicationClientId = "00000004-0000-0ff1-ce00-000000000000";
        /// <summary>
        /// The platform discover URI product
        /// </summary>
        public static readonly Uri PlatformDiscoverUri_Prod = new Uri("https://api.skypeforbusiness.com/platformservice/discover");
        /// <summary>
        /// The platform discover URI sand box
        /// </summary>
        public static readonly Uri PlatformDiscoverUri_SandBox = new Uri("https://NOAMmeetings.resources.lync.com/platformservice/discover"); //Todo: update to api.skypeforbusinessonline after prod ready
        /// <summary>
        /// The platform applications URI sand box
        /// </summary>
        public static readonly Uri PlatformApplicationsUri_SandBox = new Uri("https://ring0NOAMfreemiummeetings.resources.lync.com/platformservice/v1/applications"); //TODO update to https://NOAMfreemiummeetings.resources.lync.com/platformservice/v1/applications
        /// <summary>
        /// The platform audience URI
        /// </summary>
        public const string PlatformAudienceUri = "https://NOAMmeetings.resources.lync.com"; //Todo: update to api.skypeforbusinessonline after prod ready

        #endregion

        #region resource namespaces
        /// <summary>
        /// The default resource namespace
        /// </summary>
        public const string DefaultResourceNamespace = "ms:rtc:saas:";
        /// <summary>
        /// The public service resource namespace
        /// </summary>
        public const string PublicServiceResourceNamespace = "service:";
        #endregion
    }
}

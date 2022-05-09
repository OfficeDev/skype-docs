---
title: funky called medina.  booya
---

more changes

something

# reportMediaDiagnostics

 _**Applies to:** Skype for Business 2015_

More changes to upload

Represents RreportMediaDiagnostics operation.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.

| **Scope** | **Permission** | **Description** |
| ----- | ----- | ----- |
| Conversations.Initiate | Initiate conversations and join meetings | Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user |
| Conversations.Receive | Receive conversation invites | Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user |

## Operations



<a name="sectionSection2"></a>

### POST




Report media diagnostics for a call.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|errorCode|Gets or sets the call termination error code.Nullable (ErrorCode)BadRequest, Forbidden, NotFound, MethodNotAllowed, NotAcceptable, Conflict, Gone, PreconditionFailed, EntityTooLarge, UnsupportedMediaType, PreconditionRequired, TooManyRequests, ServiceFailure, ServiceUnavailable, GatewayTimeout, ExchangeServiceFailure, Timeout, LocalFailure, RemoteFailure, or Informational|No|
|errorSubcode|Gets or sets the call termination error subcode.Nullable (ErrorSubcode)None, Timeout, UnsupportedMediaType, DeserializationFailure, AnonymousNotAllowed, InviteesOnly, AlreadyExists, AnotherOperationPending, APIVersionNotSupported, NormalizationFailed, ProvisioningDataUnavailable, ApplicationNotFound, TooManyApplications, InactiveApplicationExpired, UserAgentNotAllowed, LimitExceeded, OperationNotSupported, NoDelegatesConfigured, NoTeamMembersConfigured, MakeMeAvailableRequired, LisServiceUnavailable, NoLocationFound, TooManyContacts, MigrationInProgress, TooManyGroups, TooManyOnlineMeetings, ThreadIdAlreadyExists, DoNotDisturb, ConnectedElsewhere, Missed, MediaFallback, FederationRequired, Canceled, Declined, Forwarded, Transferred, Replaced, EscalationFailed, InsufficientBandwidth, RepliedWithOtherModality, DestinationNotFound, DialoutNotAllowed, Unreachable, MediaEncryptionNotSupported, MediaEncryptionRequired, MediaEncryptionMismatch, Unavailable, TooManyParticipants, TooManyLobbyParticipants, Busy, AttendeeNotAllowed, Demoted, MediaFailure, InvalidMediaDescription, Removed, TemporarilyUnavailable, ModalityNotSupported, NotAllowed, Ejected, Denied, Ended, ParameterValidationFailure, SessionSwitched, DerivedConversation, Redirected, Expired, NotAcceptable, NewContentSharer, PhoneNumberConflict, IPv6NotSupported, PGetReplaced, PstnCallFailed, TransferDeclined, TransferTargetDeclined, CallbackUriUnreachable, ConversationNotFound, AutoAccepted, TooManyConversations, MediaNegotiationFailure, MediaNegotiationTimeOut, CallTerminated, MaxEventCountReached, CallbackChannelError, InvalidExchangeServerVersion, UnprocessableEntity, ExchangeTimeout, or CannotRedirect|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/reportMediaDiagnostics HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 56
{
  &quot;errorCode&quot; : &quot;BadRequest&quot;,
  &quot;errorSubcode&quot; : &quot;InviteesOnly&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/reportMediaDiagnostics HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Content-Length: 224
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;http://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;errorCode&quot;&gt;TooManyRequests&lt;/property&gt;
  &lt;property name=&quot;errorSubcode&quot;&gt;MediaNegotiationFailure&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



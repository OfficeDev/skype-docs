# decline

 _**Applies to:** Skype for Business 2015_


Declines an incoming invitation.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource is used to decline an incoming [messagingInvitation](messagingInvitation_ref.md), [phoneAudioInvitation](phoneAudioInvitation_ref.md) or [onlineMeetingInvitation](onlineMeetingInvitation_ref.md) as part of a new or existing [conversation](conversation_ref.md).decline causes the corresponding invitation to fail with an indication that it was declined.For an incoming [messagingInvitation](messagingInvitation_ref.md), decline will make the instant messaging modality disconnected in the corresponding conversation.For an incoming [phoneAudioInvitation](phoneAudioInvitation_ref.md), decline will make the phone audio modality disconnected in the corresponding conversation.For an incoming [onlineMeetingInvitation](onlineMeetingInvitation_ref.md), decline will terminate the conversation. Other participants may remain active in the [onlineMeeting](onlineMeeting_ref.md).

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Declines an incoming invitation.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|reason|Whether the invitation is declined on behalf of the local application or on behalf the user across all of her applications.(CallDeclineReason)Unknown, Local, Global, MediaAllocationFailure, EncryptionMismatch, IPv6NotSupported, MediaConnectivityFailure, CodecMismatch, UnsupportedMedia, InvalidMediaOffer, InsufficientBandwidth, or InsufficientBandwidthRerouteOverPstn|No|

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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/decline HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 18
{
  &quot;reason&quot; : &quot;Local&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/decline HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Content-Length: 147
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;reason&quot;&gt;Local&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



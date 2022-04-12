---
title: SendProvisionalAnswer resource for Skype for Business 2015
description: "SendProvisionalAnswer indicates a link in an audioVideoInvitation resource that lets the application send an answer for early media negotiation."
---
# sendProvisionalAnswer

 _**Applies to:** Skype for Business 2015_


Represents a link in an audioVideoInvitation resource that allows the application to send an answer for early media negotiation.
            

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

|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Allows application to send provisional answer an incoming invitation.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|processedOfferId|Optional input to specify the SDP multi-part content id when answering a multi-part SDP.When answering an incoming offer that has a multi-part SDP, clients can choose to specify the content id of the offer for which the SDP answer corresponds to.The maximum length is 100 characters.|No|
|sessionContext|The context of the session.The maximum length is 50 characters.|Yes|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|sdp|The body that represents the SDP data.Array of Byte|No|

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



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/507/sendProvisionalAnswer HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/sdp
Content-Length: 272
v=0o=AudiocodesGW129380630129380304INIP4172.29.107.252s=Phone-Callc=INIP4172.29.107.252t=00m=audio-6020RTP/AVP013101a=rtpmap : 0PCMU/8000a=rtpmap : 101telephone-event/8000a=fmtp : 1010-15a=ptime : 20a=sendrecva=rtcp : 6021INIP4172.29.107.252
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/507/sendProvisionalAnswer HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/sdp
Content-Length: 272

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



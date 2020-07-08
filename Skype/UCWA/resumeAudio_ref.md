# resumeAudio

 _**Applies to:** Skype for Business 2015_


Represents an operation to resume audio modality. 
            

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
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Renegotiates media for resuming audio.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|operationId|Gets or sets operation id.The maximum length is 50 characters.|Yes|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|sdp|The body that represents the SDP data.Array of Byte|No|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[Renegotiation entity](audioVideoRenegotiation_ref.md)|Represents a single re-negotiation entity of a session entity. The entity will only showup in the event channel. When a client wants to initiate a re-negotiation, it uses application/sdpinstead of CommunicationRequest to do so.|

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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/resumeAudio HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/sdp
Accept: application/json
Content-Length: 272
v=0o=AudiocodesGW129380630129380304INIP4172.29.107.252s=Phone-Callc=INIP4172.29.107.252t=00m=audio-6020RTP/AVP013101a=rtpmap : 0PCMU/8000a=rtpmap : 101telephone-event/8000a=fmtp : 1010-15a=ptime : 20a=sendrecva=rtcp : 6021INIP4172.29.107.252
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/json
Content-Length: 716
{
  "direction" : "Incoming",
  "operationId" : "samplevalue",
  "_links" : {
    "mediaAnswer" : {
      "href" : "data:application/sdp;base64,base64-encoded-sdp"
    },
    "mediaOffer" : {
      "href" : "data:application/sdp;base64,base64-encoded-sdp"
    },
    "answer" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation/answer"
    },
    "audioVideoSession" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession"
    },
    "conversation" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137"
    },
    "dismiss" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation/dismiss"
    }
  }
}
```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/resumeAudio HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/sdp
Accept: application/xml
Content-Length: 272

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/xml
Content-Length: 911
<?xml version="1.0" encoding="utf-8"?>
<resource xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="mediaAnswer" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="mediaOffer" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="answer" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation/answer" />
  <link rel="audioVideoSession" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="dismiss" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation/dismiss" />
  <property name="direction">Incoming</property>
  <property name="operationId">samplevalue</property>
</resource>
```



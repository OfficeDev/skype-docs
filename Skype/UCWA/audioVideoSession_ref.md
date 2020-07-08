# audioVideoSession

 _**Applies to:** Skype for Business 2015_


Represents a session in an audioVideo call. 
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

### Properties



|**Name**|**Description**|
|:-----|:-----|
|remoteEndpoint|The remote endpoint of a session.|
|sessionContext|The context of the session.|
|state|The state of the session.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|applicationSharing|Represents the application sharing modality in the corresponding [conversation](conversation_ref.md).|
|audioVideo|Represents the audio/video modality in the corresponding [conversation](conversation_ref.md).|
|conversation|Represents the local participants perspective on a multi-modal, multi-party communication.|
|dataCollaboration|Represents the data collaboration modality in the corresponding [conversation](conversation_ref.md).|
|publishCallQualityFeedback|Represents publishCallQualityFeedback operation.|
|renegotiations|Represents the collection of renegotiations.|
|resumeAudio|Represents an operation to resume audio modality.|
|resumeAudioVideo|Represents an operation to resume both audio and video modalities.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Operation description coming soon...

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 855
{
  "rel" : "session",
  "remoteEndpoint" : "samplevalue",
  "sessionContext" : "samplevalue",
  "state" : "Established",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/session"
    },
    "applicationSharing" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/applicationSharing"
    },
    "audioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo"
    },
    "conversation" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137"
    },
    "dataCollaboration" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/dataCollaboration"
    },
    "publishCallQualityFeedback" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/publishCallQualityFeedback"
    },
    "renegotiations" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 1100
<?xml version="1.0" encoding="utf-8"?>
<resource rel="session" href="/ucwa/v1/applications/192/communication/session" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="applicationSharing" href="/ucwa/v1/applications/192/communication/conversations/137/applicationSharing" />
  <link rel="audioVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="dataCollaboration" href="/ucwa/v1/applications/192/communication/conversations/137/dataCollaboration" />
  <link rel="publishCallQualityFeedback" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/publishCallQualityFeedback" />
  <link rel="renegotiations" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations" />
  <property name="rel">session</property>
  <property name="remoteEndpoint">samplevalue</property>
  <property name="sessionContext">samplevalue</property>
  <property name="state">Establishing</property>
</resource>
```



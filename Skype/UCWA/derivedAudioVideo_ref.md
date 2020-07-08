# derivedAudioVideo

 _**Applies to:** Skype for Business 2015_


The derived audioVideo resource.
            

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



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/derivedAudioVideo HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 1816
{
  "rel" : "audioVideo",
  "state" : "Connecting",
  "supportsReplaces" : "None",
  "videoSourcesAllowed" : "PresentersOnly",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo"
    },
    "addAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/addAudio"
    },
    "addAudioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/addAudioVideo"
    },
    "addVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/addVideo"
    },
    "audioVideoPolicies" : {
      "href" : "/ucwa/v1/applications/192/communication/audioVideoPolicies"
    },
    "audioVideoSession" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession"
    },
    "conversation" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137"
    },
    "escalateAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/escalateAudio"
    },
    "escalateAudioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/escalateAudioVideo"
    },
    "replaceWithPhoneAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/replaceWithPhoneAudio"
    },
    "reportMediaDiagnostics" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/reportMediaDiagnostics"
    },
    "stopAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/stopAudio"
    },
    "stopAudioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/stopAudioVideo"
    },
    "stopVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/stopVideo"
    },
    "videoFreeze" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/videoFreeze"
    },
    "videoLockedOnParticipant" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/videoLockedOnParticipant"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/derivedAudioVideo HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 2137
<?xml version="1.0" encoding="utf-8"?>
<resource rel="audioVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="addAudio" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/addAudio" />
  <link rel="addAudioVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/addAudioVideo" />
  <link rel="addVideo" href="/ucwa/v1/applications/192/communication/addVideo" />
  <link rel="audioVideoPolicies" href="/ucwa/v1/applications/192/communication/audioVideoPolicies" />
  <link rel="audioVideoSession" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="escalateAudio" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/escalateAudio" />
  <link rel="escalateAudioVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/escalateAudioVideo" />
  <link rel="replaceWithPhoneAudio" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/replaceWithPhoneAudio" />
  <link rel="reportMediaDiagnostics" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/reportMediaDiagnostics" />
  <link rel="stopAudio" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/stopAudio" />
  <link rel="stopAudioVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/stopAudioVideo" />
  <link rel="stopVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/stopVideo" />
  <link rel="videoFreeze" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/videoFreeze" />
  <link rel="videoLockedOnParticipant" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/videoLockedOnParticipant" />
  <property name="rel">audioVideo</property>
  <property name="state">Disconnected</property>
  <property name="supportsReplaces">None</property>
  <property name="videoSourcesAllowed">PresentersOnly</property>
</resource>
```



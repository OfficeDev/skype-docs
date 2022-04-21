---
title: audioVideo
description: Represents the audio/video modality in the corresponding conversation.
---

# audioVideo

 _**Applies to:** Skype for Business 2015_


Represents the audio/video modality in the corresponding [conversation](conversation_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

In this version of the API, establishing audio/video calls is not supported.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|state|The state of the modality, such as Connecting, Connected, or Disconnected.|
|supportsReplaces|The replacementId for the audioVideo resource that can be used while doing consultative transfer|
|videoSourcesAllowed|The video sources allowed to contribute to the gallery ([VideoSourcesAllowed](VideoSourcesAllowed_ref.md)).|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|addAudio|Represents an operation to add audio to an existing conversation.|
|addAudioVideo|Represents an operation to add audio/video to an existing conversation.|
|addVideo|Represents an operation to add video to an existing conversation.|
|audioVideoPolicies|The AudioVideoPolicies resource captures all audio-video policies that may applyto UCWA clients.|
|audioVideoSession|Represents a session in an audioVideo call.|
|conversation|Represents the local participants perspective on a multi-modal, multi-party communication.|
|escalateAudio|Represents an operation to escalate audio modality from P2P to conferencing|
|escalateAudioVideo|Represents an operation to escalate both audio and video modality from P2P to conferencing.The client must provide sdp for both modalities.|
|replaceWithPhoneAudio|Adds phone audio to an existing conversation.|
|reportMediaDiagnostics|Represents RreportMediaDiagnostics operation.|
|stopAudio|Represents an operation to stop established AudioVideo. This token indicatesThe user has ability to renegotiate only audio.|
|stopAudioVideo|Represents an operation to stop established AudioVideo. This token indicatesthe user has ability to renegotiate both audio and video.|
|stopVideo|Represents an operation to stop established AudioVideo. This token indicatesThe user has ability to renegotiate only video.|
|videoFreeze|Represents a link resource to indicate local participant's video should be freezed|
|videoLockedOnParticipant|Represents the [participant](participant_ref.md) on whom the video spotlight is locked in an [onlineMeeting](onlineMeeting_ref.md).|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.

|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Updated

|**Resource**|**Priority**|**Sender**|**Reason**|
|:----- |:----- |:----- |:----- |
|audioVideo|High|conversation|Indicates that the audio/video modality was updated.</p><p></p>|

Sample of returned event data.

This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
{
  "_links" : {
    "self" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=1"
    },
    "next" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=2"
    }
  },
  "sender" : [
    {
      "rel" : "conversation",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137",
      "events" : [
        {
          "link" : {
            "rel" : "audioVideo",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}
```

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the audio/video modality in the corresponding [conversation](conversation_ref.md).

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 1815
{
  "rel" : "audioVideo",
  "state" : "Connected",
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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 2134
<?xml version="1.0" encoding="utf-8"?>
<resource rel="audioVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo" xmlns="http://schemas.microsoft.com/rtc/2012/03/ucwa">
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
  <property name="state">Connected</property>
  <property name="supportsReplaces">None</property>
  <property name="videoSourcesAllowed">PresentersOnly</property>
</resource>
```



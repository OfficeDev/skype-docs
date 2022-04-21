---
title: audioVideoRenegotiation
description: audioVideoRenegotiation represents a single re-negotiation entity of a session entity.
---

# audioVideoRenegotiation

 _**Applies to:** Skype for Business 2015_


Represents a single re-negotiation entity of a session entity. The entity will only show
up in the event channel. When a client wants to initiate a re-negotiation, it uses application/sdp
instead of CommunicationRequest to do so.
            

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
|direction|Gets the direction for this negotiation.|
|mediaAnswer|Gets the final answer.|
|mediaOffer|Gets the offer.|
|operationId|Gets the operation identifier.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|answer|Represents a link resource to answer the negotiation.|
|audioVideoSession|Represents a session in an audioVideo call.|
|conversation|Represents the local participants perspective on a multi-modal, multi-party communication.|
|dismiss|Represents a link resource to answer the negotiation.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.

|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Started



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|audioVideoRenegotiation|High|communication|Delivered when an audiovideo renegotiation is started.|

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
      "rel" : "communication",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication",
      "events" : [
        {
          "link" : {
            "rel" : "audioVideoRenegotiation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation"
          },
          "type" : "started"
        }
      ]
    }
  ]
}
```

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 892
{
  "rel" : "audioVideoRenegotiation",
  "direction" : "Incoming",
  "operationId" : "samplevalue",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation"
    },
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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 1129
<?xml version="1.0" encoding="utf-8"?>
<resource rel="audioVideoRenegotiation" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation" xmlns="http://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="mediaAnswer" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="mediaOffer" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="answer" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation/answer" />
  <link rel="audioVideoSession" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="dismiss" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession/renegotiations/audioVideoRenegotiation/dismiss" />
  <property name="rel">audioVideoRenegotiation</property>
  <property name="direction">Incoming</property>
  <property name="operationId">samplevalue</property>
</resource>
```



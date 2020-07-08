# audioVideoNegotiation

 _**Applies to:** Skype for Business 2015_


A resource that provides an outgoing audioVideo call its media answers. This resource appears only
in the event channel or in a response to a request. It will not appear in a request.
            

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
|mediaAnswer|The final media answer.|
|mediaProvisionalAnswer|The provisional media answer.|
|msAcceptedContentId|The content ID of the offer that was accepted.|
|remoteEndpoint|The remote endpoint of a session.|
|sessionContext|The context of the session.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|audioVideoInvitation|Represents an audio-video invitation.|
|audioVideoSession|Represents a session in an audioVideo call.|

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
|:-----|:-----|:-----|:-----|
|audioVideoNegotiation|High|audioVideoInvitation|Delivered when the AudioVideo negotiation is updated.</p><p></p>|
Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
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
      "rel" : "audioVideoInvitation",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/507",
      "events" : [
        {
          "link" : {
            "rel" : "audioVideoNegotiation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/audioVideoNegotiation"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}


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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/audioVideoNegotiation HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 599
{
  "rel" : "audioVideoNegotiation",
  "msAcceptedContentId" : "samplevalue",
  "remoteEndpoint" : "samplevalue",
  "sessionContext" : "samplevalue",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/audioVideoNegotiation"
    },
    "mediaAnswer" : {
      "href" : "data:application/sdp;base64,base64-encoded-sdp"
    },
    "mediaProvisionalAnswer" : {
      "href" : "data:application/sdp;base64,base64-encoded-sdp"
    },
    "audioVideoInvitation" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/507"
    },
    "audioVideoSession" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/audioVideoNegotiation HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 841
<?xml version="1.0" encoding="utf-8"?>
<resource rel="audioVideoNegotiation" href="/ucwa/v1/applications/192/communication/invitations/665/audioVideoNegotiation" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="mediaAnswer" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="mediaProvisionalAnswer" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="audioVideoInvitation" href="/ucwa/v1/applications/192/communication/invitations/507" />
  <link rel="audioVideoSession" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/audioVideoSession" />
  <property name="rel">audioVideoNegotiation</property>
  <property name="msAcceptedContentId">samplevalue</property>
  <property name="remoteEndpoint">samplevalue</property>
  <property name="sessionContext">samplevalue</property>
</resource>
```



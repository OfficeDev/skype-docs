# participantAudio

 _**Applies to:** Skype for Business 2015_


Represents whether a participant is using the audio modality in a conversation.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|
|title|The identifier of the audio source for this participant.|

## Resource description
<a name = "sectionSection1"> </a>

This resource helps the application track when a participant joins or leaves this modality.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|audioDirection|The direction of the participant's audio ([MediaDirectionType](MediaDirectionType_ref.md)): SendReceive,SendOnly,ReceiveOnly, or Inactive.|
|audioMuted|Whether the participant's audio is muted.|
|audioSourceId|The source identifier of the participant's audio.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|muteAudio|Mutes a [participant](participant_ref.md)'s audio.|
|participant|Represents a remote participant in a [conversation](conversation_ref.md).|
|unmuteAudio|Unmutes a [participant](participant_ref.md)'s audio.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Added



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|participantAudio|High|conversation|Indicates that a participant is now using the audio modality. The application can choose to fetch the updated information.</p><p></p>|
|participantAudio|High|conversation|Indicates that the user is now using the audio modality.</p><p></p>|
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
      "rel" : "conversation",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137",
      "events" : [
        {
          "link" : {
            "rel" : "participantAudio",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio"
          },
          "in" : {
            "rel" : "localParticipant",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295"
          },
          "type" : "added"
        }
      ]
    }
  ]
}


### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|participantAudio|High|conversation|Indicates that a participant's audio modality has changed. The application can choose to fetch the updated information.</p><p></p>|
|participantAudio|High|conversation|Indicates that the user's audio modality has changed.</p><p></p>|
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
      "rel" : "conversation",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137",
      "events" : [
        {
          "link" : {
            "rel" : "participantAudio",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio"
          },
          "in" : {
            "rel" : "localParticipant",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}


### Deleted



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|participantAudio|High|conversation|Indicates that a participant is no longer using the audio modality. The application can choose to fetch the updated information.</p><p></p>|
|participantAudio|High|conversation|Indicates that the user is no longer using the audio modality.</p><p></p>|
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
      "rel" : "conversation",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137",
      "events" : [
        {
          "link" : {
            "rel" : "participantAudio",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio"
          },
          "in" : {
            "rel" : "localParticipant",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295"
          },
          "type" : "deleted"
        }
      ]
    }
  ]
}


## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the audio modality for a participant.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 573
{
  "rel" : "participantAudio",
  "audioDirection" : "Unknown",
  "audioMuted" : false,
  "audioSourceId" : "1234567",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio"
    },
    "muteAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio/muteAudio"
    },
    "participant" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196"
    },
    "unmuteAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio/unmuteAudio"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 804
<?xml version="1.0" encoding="utf-8"?>
<resource rel="participantAudio" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="muteAudio" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio/muteAudio" />
  <link rel="participant" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196" />
  <link rel="unmuteAudio" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio/unmuteAudio" />
  <property name="rel">participantAudio</property>
  <property name="audioDirection">Unknown</property>
  <property name="audioMuted">False</property>
  <property name="audioSourceId">1234567</property>
</resource>
```



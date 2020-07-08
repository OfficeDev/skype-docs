# conversation

 _**Applies to:** Skype for Business 2015_


Represents the local participants perspective on a multi-modal, multi-party communication.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

A dashboard of the current capabilities that are dynamically aggregated based on the corresponding[application](application_ref.md)'s permissions, the user's role, and the capabilities of the remote participants andservice components that are involved in the communication. While a conversation can be multi-modaland multi-party, it can also represent a basic call with one remote participant. A conversation iscreated by the server following an invitation. Note that terminating a conversation simply meansthat the user is leaving the communication; other participants might still be able to communicate.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|activeModalities|The active media in the conversation.|
|audienceMessaging|Whether the audience messaging modality is enabled/disabled in the current conversation.|
|audienceMute|The audio mute status of the local participant.|
|created|Creation time stamp in UTC.|
|expirationTime|The expiry time of the conversation.|
|importance|The importance of the conversation.|
|participantCount|The number of participants in the conversation.|
|readLocally|Whether the conversation was read locally.|
|recording|Whether the conversation is being recorded.|
|state|The state of the conversation.|
|subject|The subject of the conversation.|
|threadId|The thread ID of the conversation.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|addParticipant|Invites a [contact](contact_ref.md) to participate in a multiparty [conversation](conversation_ref.md).|
|applicationSharing|Represents the application sharing modality in the corresponding [conversation](conversation_ref.md).|
|attendees|Represents a view of the [participant](participant_ref.md)s having the attendee role in an [onlineMeeting](onlineMeeting_ref.md).|
|audioVideo|Represents the audio/video modality in the corresponding [conversation](conversation_ref.md).|
|dataCollaboration|Represents the data collaboration modality in the corresponding [conversation](conversation_ref.md).|
|disableAudienceMessaging|Disables the [messaging](messaging_ref.md) modality for all members of a [conversation](conversation_ref.md).|
|disableAudienceMuteLock|Disables the forced mute of attendees in a [conversation](conversation_ref.md).|
|enableAudienceMessaging|Enables the [messaging](messaging_ref.md) modality for all members of a [conversation](conversation_ref.md).|
|enableAudienceMuteLock|Enables the forced mute of attendees in a [conversation](conversation_ref.md).|
|leaders|Represents a view of the [participant](participant_ref.md)s in the leader role in an [onlineMeeting](onlineMeeting_ref.md).|
|lobby|Represents a view of the [participant](participant_ref.md)s who have not yet been admitted to an [onlineMeeting](onlineMeeting_ref.md).|
|localParticipant|Represents the user as a local [participant](participant_ref.md) in a specific [conversation](conversation_ref.md).|
|messaging|Represents the instant messaging modality in a [conversation](conversation_ref.md).|
|onlineMeeting|Represents a read-only version of the [onlineMeeting](onlineMeeting_ref.md) associated with this [conversation](conversation_ref.md).|
|participants|A collection of [participant](participant_ref.md) resources.|
|phoneAudio|Represents the phone audio modality in a [conversation](conversation_ref.md).|
|userAcknowledged|Represents the user acknowledged|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Added



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|conversation|High|communication|Delivered when a new conversation resource is added.</p><p></p>|
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
      "rel" : "communication",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication",
      "events" : [
        {
          "link" : {
            "rel" : "conversation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137"
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
|conversation|High|communication|Delivered when the conversation resource is updated.</p><p></p>|
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
      "rel" : "communication",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication",
      "events" : [
        {
          "link" : {
            "rel" : "conversation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137"
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
|conversation|High|communication|Delivered when the conversation resource is deleted.</p><p></p>|
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
      "rel" : "communication",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication",
      "events" : [
        {
          "link" : {
            "rel" : "conversation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137"
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




Returns a representation of the local participant's perspective on a multi-modal, multi-party communication.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 2235
{
  "rel" : "conversation",
  "activeModalities" : [
    "Messaging",
    "Audio",
    "Video",
    "ApplicationSharing"
  ],
  "audienceMessaging" : "Enabled",
  "audienceMute" : "Unknown",
  "created" : "\/Date(1474932024822)\/",
  "expirationTime" : "\/Date(1326337402743)\/",
  "importance" : "Normal",
  "participantCount" : 11,
  "readLocally" : false,
  "recording" : false,
  "state" : "Disconnected",
  "subject" : "Skype for Business",
  "threadId" : "534e445ee854436a8abe02c24985f78a",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137"
    },
    "addParticipant" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/addParticipant"
    },
    "applicationSharing" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/applicationSharing"
    },
    "attendees" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/attendees"
    },
    "audioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo"
    },
    "dataCollaboration" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/dataCollaboration"
    },
    "disableAudienceMessaging" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/messaging/disableAudienceMessaging"
    },
    "disableAudienceMuteLock" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/disableAudienceMuteLock"
    },
    "enableAudienceMessaging" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/messaging/enableAudienceMessaging"
    },
    "enableAudienceMuteLock" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/enableAudienceMuteLock"
    },
    "leaders" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/leaders"
    },
    "lobby" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/lobby"
    },
    "localParticipant" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295"
    },
    "messaging" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/messaging"
    },
    "onlineMeeting" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting"
    },
    "participants" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants"
    },
    "phoneAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/phoneAudio"
    },
    "userAcknowledged" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/userAcknowledged"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 2853
<?xml version="1.0" encoding="utf-8"?>
<resource rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="addParticipant" href="/ucwa/v1/applications/192/communication/conversations/137/addParticipant" />
  <link rel="applicationSharing" href="/ucwa/v1/applications/192/communication/conversations/137/applicationSharing" />
  <link rel="attendees" href="/ucwa/v1/applications/192/communication/conversations/137/attendees" />
  <link rel="audioVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo" />
  <link rel="dataCollaboration" href="/ucwa/v1/applications/192/communication/conversations/137/dataCollaboration" />
  <link rel="disableAudienceMessaging" href="/ucwa/v1/applications/192/communication/conversations/137/messaging/disableAudienceMessaging" />
  <link rel="disableAudienceMuteLock" href="/ucwa/v1/applications/192/communication/conversations/137/disableAudienceMuteLock" />
  <link rel="enableAudienceMessaging" href="/ucwa/v1/applications/192/communication/conversations/137/messaging/enableAudienceMessaging" />
  <link rel="enableAudienceMuteLock" href="/ucwa/v1/applications/192/communication/conversations/137/enableAudienceMuteLock" />
  <link rel="leaders" href="/ucwa/v1/applications/192/communication/conversations/137/leaders" />
  <link rel="lobby" href="/ucwa/v1/applications/192/communication/conversations/137/lobby" />
  <link rel="localParticipant" href="/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295" />
  <link rel="messaging" href="/ucwa/v1/applications/192/communication/conversations/137/messaging" />
  <link rel="onlineMeeting" href="/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting" />
  <link rel="participants" href="/ucwa/v1/applications/192/communication/conversations/137/participants" />
  <link rel="phoneAudio" href="/ucwa/v1/applications/192/communication/phoneAudio" />
  <link rel="userAcknowledged" href="/ucwa/v1/applications/192/communication/conversations/137/userAcknowledged" />
  <property name="rel">conversation</property>
  <propertyList name="activeModalities">
    <item>Messaging</item>
    <item>Audio</item>
    <item>Video</item>
    <item>ApplicationSharing</item>
  </propertyList>
  <property name="audienceMessaging">Enabled</property>
  <property name="audienceMute">Unknown</property>
  <property name="created">2016-09-26T16:20:24.8356669-07:00</property>
  <property name="expirationTime">2012-01-11T19:03:22.7433336-08:00</property>
  <property name="importance">Normal</property>
  <property name="participantCount">4</property>
  <property name="readLocally">False</property>
  <property name="recording">False</property>
  <property name="state">Disconnected</property>
  <property name="subject">Skype for Business</property>
  <property name="threadId">534e445ee854436a8abe02c24985f78a</property>
</resource>
```



### DELETE




Removes the user from the communication, which ends the conversation. This operation tears down all active modalities.

#### Request body



None


#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Delete https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```


#### XML Request




```
Delete https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



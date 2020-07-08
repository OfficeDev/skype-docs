# derivedConversation

 _**Applies to:** Skype for Business 2015_


Represents a related conversation with a different [participant](participant_ref.md) than the one of the original conversation.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

A derivedConversation is created when a new modality is added to an existing [conversation](conversation_ref.md) but is answered by another participant (perhaps due to [simultaneousRingSettings](simultaneousRingSettings_ref.md)).

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




Returns a representation of a related conversation with a different [participant](participant_ref.md) than the original conversation.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/derivedConversation HTTP/1.1
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
  "created" : "\/Date(1474932025211)\/",
  "expirationTime" : "\/Date(1326337402743)\/",
  "importance" : "Normal",
  "participantCount" : 90,
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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/665/derivedConversation HTTP/1.1
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
  <property name="created">2016-09-26T16:20:25.2157180-07:00</property>
  <property name="expirationTime">2012-01-11T19:03:22.7433336-08:00</property>
  <property name="importance">Normal</property>
  <property name="participantCount">2</property>
  <property name="readLocally">False</property>
  <property name="recording">False</property>
  <property name="state">Disconnected</property>
  <property name="subject">Skype for Business</property>
  <property name="threadId">534e445ee854436a8abe02c24985f78a</property>
</resource>
```



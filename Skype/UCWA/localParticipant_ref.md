# localParticipant

 _**Applies to:** Skype for Business 2015_


Represents the user as a local [participant](participant_ref.md) in a specific [conversation](conversation_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

participant is the transient representation of the user that captures her attributes such as role or capabilities (such as promoting to leader or admitting from lobby).A localParticipant's lifetime is controlled by the server and starts when the user joins a [conversation](conversation_ref.md).It is removed when the participant leaves the [conversation](conversation_ref.md).

### Properties



|**Name**|**Description**|
|:-----|:-----|
|anonymous|Whether the participant is anonymous.|
|inLobby|Whether the participant is in the lobby.|
|local|Whether the participant is local.The local participant is the same as the user.|
|name|The participant's display name.|
|organizer|Whether the participant is an organizer.An organizer can never be locked out of her own meeting.|
|otherPhoneNumber|The participant's Other Phone.|
|role|The participant's role ([Role](Role_ref.md)), such as Attendee or Leader.|
|sourceNetwork|The participant's source network ([SourceNetwork](SourceNetwork_ref.md)), such as SameEnterprise or PublicCloud (for example, a Skype contact).|
|uri|The participant's URI.|
|workPhoneNumber|The participant's Phone URI.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|contact|Represents a person or service that the user can communicate and collaborate with.|
|contactPhoto|The photo of a contact.|
|contactPresence|Represents a [contact](contact_ref.md)'s availability and activity.|
|conversation|Represents the local participants perspective on a multi-modal, multi-party communication.|
|eject|Ejects the corresponding [participant](participant_ref.md) from the [onlineMeeting](onlineMeeting_ref.md).|
|me|Represents the user.|
|participantApplicationSharing|Represents whether a participant is using the application sharing modality in a conversation.|
|participantAudio|Represents whether a participant is using the audio modality in a conversation.|
|participantDataCollaboration|Represents whether a participant is using the data collaboration modality in a conversation.|
|participantMessaging|Used to determine whether a participant is using the instant messaging modality in a conversation.|
|participantPanoramicVideo|Represents whether a participant is using the panoramic video modality in a conversation.|
|participantVideo|Represents whether a participant is using the main video modality in a conversation.|

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
|localParticipant|High|conversation|Indicates that the user has joined a [conversation](conversation_ref.md).</p><p>The application should use this as a hint to update its conversation cache.This event is raised when the user is first added.Other added events for the same participant indicate that the user has been admitted to the conference, promoted, demoted, or is typing a message.</p>|
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
|localParticipant|High|conversation|Indicates that the user has been updated.</p><p>This event is raised when the user's capabilities as a participant have changed. Note that this is the only updated event for localParticipant.For example, if the user is was demoted from leader to attendee, the [enableAudienceMuteLock](enableAudienceMuteLock_ref.md) capability is no longer present.</p>|
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
|localParticipant|High|conversation|Indicates that the user has left a conversation.</p><p></p>|
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




Returns a representation of the user as a local [participant](participant_ref.md) in a specific [conversation](conversation_ref.md).

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 1620
{
  "rel" : "localParticipant",
  "anonymous" : true,
  "inLobby" : true,
  "local" : true,
  "name" : "Joe Smith",
  "organizer" : true,
  "otherPhoneNumber" : "tel:+14251111111",
  "role" : "Attendee",
  "sourceNetwork" : "SameEnterprise",
  "uri" : "sip:john@contoso.com",
  "workPhoneNumber" : "tel:+14251111111",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295"
    },
    "contact" : {
      "href" : "/ucwa/v1/applications/192/people/282"
    },
    "contactPhoto" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactPhoto"
    },
    "contactPresence" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactPresence"
    },
    "conversation" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137"
    },
    "eject" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/eject"
    },
    "me" : {
      "href" : "/ucwa/v1/applications/192/me"
    },
    "participantApplicationSharing" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantApplicationSharing"
    },
    "participantAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio"
    },
    "participantDataCollaboration" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration"
    },
    "participantMessaging" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantMessaging"
    },
    "participantPanoramicVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantPanoramicVideo"
    },
    "participantVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantVideo"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 2090
<?xml version="1.0" encoding="utf-8"?>
<resource rel="localParticipant" href="/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/295" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
  <link rel="contactPhoto" href="/ucwa/v1/applications/192/people/282/contactPhoto" />
  <link rel="contactPresence" href="/ucwa/v1/applications/192/people/282/contactPresence" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="eject" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/eject" />
  <link rel="me" href="/ucwa/v1/applications/192/me" />
  <link rel="participantApplicationSharing" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantApplicationSharing" />
  <link rel="participantAudio" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio" />
  <link rel="participantDataCollaboration" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration" />
  <link rel="participantMessaging" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantMessaging" />
  <link rel="participantPanoramicVideo" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantPanoramicVideo" />
  <link rel="participantVideo" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantVideo" />
  <property name="rel">localParticipant</property>
  <property name="anonymous">True</property>
  <property name="inLobby">True</property>
  <property name="local">True</property>
  <property name="name">Joe Smith</property>
  <property name="organizer">True</property>
  <property name="otherPhoneNumber">tel:+14251111111</property>
  <property name="role">Attendee</property>
  <property name="sourceNetwork">SameEnterprise</property>
  <property name="uri">sip:john@contoso.com</property>
  <property name="workPhoneNumber">tel:+14251111111</property>
</resource>
```



# onlineMeetingInvitation

 _**Applies to:** Skype for Business 2015_


Represents an invitation to a new or existing [onlineMeeting](onlineMeeting_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource can be incoming or outgoing.If outgoing, the onlineMeetingInvitation can be created using [joinOnlineMeeting](joinOnlineMeeting_ref.md).This resource assists in keeping track of the invitation status; for example, the invitation could be accepted, declined, or ignored.An outgoing onlineMeetingInvitation will be in the 'Connecting' state while the invitation is being processed. Note that the onlineMeetingInvitation will not complete until the user has been admitted ([admit](admit_ref.md)). Even after the user is in the [lobby](lobby_ref.md), the onlineMeetingInvitation will still be in the 'Connecting' state.The onlineMeetingInvitation will complete with success if the user is admitted from the lobby or with failure if he or she is rejected.Ultimately, the onlineMeetingInvitation will complete with success or failure (in which case a [reason](reason_ref.md) is supplied).The onlineMeetingInvitation will complete with success only after the [participant](participant_ref.md) appears in the roster.If incoming, the onlineMeetingInvitation was created after the user accepted a [participantInvitation](participantInvitation_ref.md). Note that this is the only way an incoming onlineMeetingInvitation can occur.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|anonymousDisplayName|The display name for anonymous users. This is required for anonymous users and should not be set for authenticated users.The maximum length is 250 characters.|
|availableModalities|The available modality types in the conference.|
|direction|The direction of the invitation.|
|importance|The importance.|
|onlineMeetingUri|The URI of the meeting to join.|
|message|The first message represented in this invitation.|
|operationId|The operation ID as supplied by the client.The maximum length is 50 characters.|
|state|The invitation state.|
|subject|The subject.The maximum length is 250 characters.|
|threadId|The thread ID of the conversation.|
|to|The target of this invitation.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|accept|Accepts an incoming invitation.|
|acceptedByContact|Represents the [contact](contact_ref.md) who ultimately accepted an incoming invitation.|
|cancel|Cancels the corresponding invitation.|
|conversation|Represents the local participants perspective on a multi-modal, multi-party communication.|
|decline|Declines an incoming invitation.|
|from|Represents the [participant](participant_ref.md) that sent an invitation.|
|onBehalfOf|Represents the [contact](contact_ref.md) on whose behalf the invitation was received.|
|to|Represents the originally intended target of the invitation as a [contact](contact_ref.md).|
|from|Represents the [participant](participant_ref.md) that sent an invitation.|

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
|onlineMeetingInvitation|High|communication|Delivered when an online meeting invitation is started. This occurs when the application joins the [onlineMeeting](onlineMeeting_ref.md) modality.|
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
            "rel" : "onlineMeetingInvitation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/711"
          },
          "type" : "started"
        }
      ]
    }
  ]
}


### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|onlineMeetingInvitation|High|communication|Delivered when the online meeting invitation is updated.|
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
            "rel" : "onlineMeetingInvitation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/711"
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
|BadRequest|400|None|Something is wrong with the entire request (malformed XML/JSON, for example).|
|BadRequest|400|ParameterValidationFailure|Incorrect parameters were provided for the request (for example, the requested conference subject exceeds the maximum length).|
|Gone|410|None|The content-type is not supported.|
|NotFound|404|None|The resource does not exist.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/711 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 3393
{
  "rel" : "onlineMeetingInvitation",
  "anonymousDisplayName" : "Cathy Arnold",
  "availableModalities" : [
    "Messaging",
    "Audio",
    "ApplicationSharing"
  ],
  "direction" : "Incoming",
  "importance" : "Normal",
  "onlineMeetingUri" : "sip:john@contoso.com",
  "operationId" : "74cb7404e0a247d5a2d4eb0376a47dbf",
  "state" : "Connected",
  "subject" : "Strategy for next quarter",
  "threadId" : "292e0aaef36c426a97757f43dda19d06",
  "to" : "sip:john@contoso.com",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/711"
    },
    "message" : {
      "href" : "data:text/plain;base64,somebase64encodedmessage"
    },
    "from" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/831"
    },
    "accept" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/accept"
    },
    "acceptedByContact" : {
      "href" : "/ucwa/v1/applications/192/people/169"
    },
    "cancel" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/cancel"
    },
    "conversation" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137"
    },
    "decline" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/decline"
    },
    "onBehalfOf" : {
      "href" : "/ucwa/v1/applications/192/people/444"
    },
    "to" : {
      "href" : "/ucwa/v1/applications/192/people/107"
    }
  },
  "_embedded" : {
    "from" : {
      "rel" : "participant",
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
          "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196"
        },
        "admit" : {
          "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/admit"
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
        "demote" : {
          "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/demote"
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
        },
        "promote" : {
          "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/promote"
        },
        "reject" : {
          "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/reject"
        }
      }
    },
    "startEmergencyCallInput" : [
      {
        "rel" : "startEmergencyCallParameters",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/192/communication/invitations/665/startEmergencyCallParameters"
          }
        }
      }
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/711 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 4276
<?xml version="1.0" encoding="utf-8"?>
<resource rel="onlineMeetingInvitation" href="/ucwa/v1/applications/192/communication/invitations/711" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="message" href="data:text/plain;base64,somebase64encodedmessage" />
  <link rel="from" href="/ucwa/v1/applications/192/communication/conversations/137/participants/831" />
  <link rel="accept" href="/ucwa/v1/applications/192/communication/invitations/665/accept" />
  <link rel="acceptedByContact" href="/ucwa/v1/applications/192/people/169" />
  <link rel="cancel" href="/ucwa/v1/applications/192/communication/invitations/665/cancel" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="decline" href="/ucwa/v1/applications/192/communication/invitations/665/decline" />
  <link rel="onBehalfOf" href="/ucwa/v1/applications/192/people/444" />
  <link rel="to" href="/ucwa/v1/applications/192/people/107" />
  <property name="rel">onlineMeetingInvitation</property>
  <property name="anonymousDisplayName">Cathy Arnold</property>
  <propertyList name="availableModalities">
    <item>Messaging</item>
    <item>Audio</item>
    <item>ApplicationSharing</item>
  </propertyList>
  <property name="direction">Incoming</property>
  <property name="importance">Normal</property>
  <property name="onlineMeetingUri">sip:john@contoso.com</property>
  <property name="operationId">74cb7404e0a247d5a2d4eb0376a47dbf</property>
  <property name="state">Connecting</property>
  <property name="subject">Strategy for next quarter</property>
  <property name="threadId">292e0aaef36c426a97757f43dda19d06</property>
  <property name="to">sip:john@contoso.com</property>
  <resource rel="from" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196">
    <link rel="admit" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/admit" />
    <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
    <link rel="contactPhoto" href="/ucwa/v1/applications/192/people/282/contactPhoto" />
    <link rel="contactPresence" href="/ucwa/v1/applications/192/people/282/contactPresence" />
    <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
    <link rel="demote" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/demote" />
    <link rel="eject" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/eject" />
    <link rel="me" href="/ucwa/v1/applications/192/me" />
    <link rel="participantApplicationSharing" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantApplicationSharing" />
    <link rel="participantAudio" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantAudio" />
    <link rel="participantDataCollaboration" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration" />
    <link rel="participantMessaging" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantMessaging" />
    <link rel="participantPanoramicVideo" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantPanoramicVideo" />
    <link rel="participantVideo" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantVideo" />
    <link rel="promote" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/promote" />
    <link rel="reject" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/reject" />
    <property name="rel">participant</property>
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
  <resource rel="startEmergencyCallInput" href="/ucwa/v1/applications/192/communication/invitations/665/startEmergencyCallParameters">
    <property name="rel">startEmergencyCallParameters</property>
  </resource>
</resource>
```



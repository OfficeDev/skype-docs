# messagingInvitation

 _**Applies to:** Skype for Business 2015_


            Represents an invitation to a [conversation](conversation_ref.md) for the [messaging](messaging_ref.md) modality.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource can be incoming or outgoing.If outgoing, the messagingInvitation can be created in one of two ways.First, [startMessaging](startMessaging_ref.md) will create a messagingInvitation that also creates a [conversation](conversation_ref.md).Second, [addMessaging](addMessaging_ref.md) will attempt to add the [messaging](messaging_ref.md) modality to an existing [conversation](conversation_ref.md).This resource assists in keeping track of the invitation status; for example, the invitation could be accepted, declined, or ignored.An outgoing messagingInvitation will be in the 'Connecting' state while waiting for the recipient to accept or decline it; during this time, the messagingInvitation can be terminated using [cancel](cancel_ref.md).Note that if the recipient does not respond in approximately thirty seconds, the messagingInvitation will complete with failure.Ultimately, the messagingInvitation will complete with success or failure (in which case a [reason](reason_ref.md) is supplied).If the messagingInvitation succeeds, the participant that accepts it will be provided ([acceptedByParticipant](acceptedByParticipant_ref.md)).If incoming, the messagingInvitation can create a new [conversation](conversation_ref.md) or attempt to add the [messaging](messaging_ref.md) modality to an existing [conversation](conversation_ref.md).

### Properties



|**Name**|**Description**|
|:-----|:-----|
|customContent|Custom Content.|
|direction|The direction of the invitation.|
|importance|The importance.|
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
|derivedMessaging|Represents the [messaging](messaging_ref.md) modality in a [derivedConversation](derivedConversation_ref.md).|
|from|Represents the [participant](participant_ref.md) that sent an invitation.|
|messaging|Represents the instant messaging modality in a [conversation](conversation_ref.md).|
|onBehalfOf|Represents the [contact](contact_ref.md) on whose behalf the invitation was received.|
|to|Represents the originally intended target of the invitation as a [contact](contact_ref.md).|
|acceptedByParticipant|Represents the remote participant who accepted the invitation of the user.|
|from|Represents the [participant](participant_ref.md) that sent an invitation.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed. 

|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Started



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|messagingInvitation|High|communication|Delivered when a messaging invitation is started. This occurs when the application starts the [messaging](messaging_ref.md) modality.| 

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
            "rel" : "messagingInvitation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/602"
          },
          "type" : "started"
        }
      ]
    }
  ]
}
```


### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|messagingInvitation|High|communication|Delivered when the messaging invitation is updated.|
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
            "rel" : "messagingInvitation",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/602"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}
					
```


### completed





|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|messagingInvitation|High|communication|Delivered when the messaging invitation completes.|
Sample of returned event data.

This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.




## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of an invitation to a [conversation](conversation_ref.md) for the [messaging](messaging_ref.md) modality.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/602 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 5561
{
  "rel" : "messagingInvitation",
  "direction" : "Incoming",
  "importance" : "Normal",
  "operationId" : "74cb7404e0a247d5a2d4eb0376a47dbf",
  "state" : "Connected",
  "subject" : "Strategy for next quarter",
  "threadId" : "292e0aaef36c426a97757f43dda19d06",
  "to" : "sip:john@contoso.com",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/602"
    },
    "customContent" : {
      "href" : "data:application/sdp;base64,base64-encoded-sdp"
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
    "derivedMessaging" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/derivedMessaging"
    },
    "messaging" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/messaging"
    },
    "onBehalfOf" : {
      "href" : "/ucwa/v1/applications/192/people/444"
    },
    "to" : {
      "href" : "/ucwa/v1/applications/192/people/107"
    }
  },
  "_embedded" : {
    "acceptedByParticipant" : [
      {
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
      }
    ],
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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/invitations/602 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 6744
<?xml version="1.0" encoding="utf-8"?>
<resource rel="messagingInvitation" href="/ucwa/v1/applications/192/communication/invitations/602" xmlns="http://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="customContent" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="message" href="data:text/plain;base64,somebase64encodedmessage" />
  <link rel="from" href="/ucwa/v1/applications/192/communication/conversations/137/participants/831" />
  <link rel="accept" href="/ucwa/v1/applications/192/communication/invitations/665/accept" />
  <link rel="acceptedByContact" href="/ucwa/v1/applications/192/people/169" />
  <link rel="cancel" href="/ucwa/v1/applications/192/communication/invitations/665/cancel" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="decline" href="/ucwa/v1/applications/192/communication/invitations/665/decline" />
  <link rel="derivedMessaging" href="/ucwa/v1/applications/192/communication/invitations/665/derivedMessaging" />
  <link rel="messaging" href="/ucwa/v1/applications/192/communication/conversations/137/messaging" />
  <link rel="onBehalfOf" href="/ucwa/v1/applications/192/people/444" />
  <link rel="to" href="/ucwa/v1/applications/192/people/107" />
  <property name="rel">messagingInvitation</property>
  <property name="direction">Incoming</property>
  <property name="importance">Normal</property>
  <property name="operationId">74cb7404e0a247d5a2d4eb0376a47dbf</property>
  <property name="state">Connecting</property>
  <property name="subject">Strategy for next quarter</property>
  <property name="threadId">292e0aaef36c426a97757f43dda19d06</property>
  <property name="to">sip:john@contoso.com</property>
  <resource rel="acceptedByParticipant" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196">
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



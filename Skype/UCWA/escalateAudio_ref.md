# escalateAudio

 _**Applies to:** Skype for Business 2015_


Represents an operation to escalate audio modality from P2P to conferencing
            

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



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Starts audio negotiation by supplying offer. Creates [audioVideoInvitation](audioVideoInvitation_ref.md) in event channel.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|operationId|The operation ID.The maximum length is 50 characters.|Yes|
|sessionContext|The context of the session.The maximum length is 50 characters.|Yes|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|sdp|The body that represents the SDP data.Array of Byte|No|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[audioVideoInvitation](AudioVideoInvitation_ref.md)|Represents an audio-video invitation.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/escalateAudio HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/sdp
Accept: application/json
Content-Length: 272
v=0o=AudiocodesGW129380630129380304INIP4172.29.107.252s=Phone-Callc=INIP4172.29.107.252t=00m=audio-6020RTP/AVP013101a=rtpmap : 0PCMU/8000a=rtpmap : 101telephone-event/8000a=fmtp : 1010-15a=ptime : 20a=sendrecva=rtcp : 6021INIP4172.29.107.252
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/json
Content-Length: 6674
{
  "rel" : "audioVideoInvitation",
  "address" : "samplevalue",
  "bandwidthControlId" : "samplevalue",
  "building" : "samplevalue",
  "city" : "samplevalue",
  "country" : "samplevalue",
  "delegator" : "sip:john@contoso.com",
  "direction" : "Incoming",
  "importance" : "Normal",
  "joinAudioMuted" : false,
  "joinVideoMuted" : false,
  "locationState" : "samplevalue",
  "operationId" : "74cb7404e0a247d5a2d4eb0376a47dbf",
  "privateLine" : false,
  "sessionContext" : "8efd502350ff419cb615018ae561f97e",
  "state" : "Connected",
  "subject" : "Strategy for next quarter",
  "threadId" : "292e0aaef36c426a97757f43dda19d06",
  "to" : "sip:john@contoso.com",
  "zip" : "samplevalue",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/507"
    },
    "customContent" : {
      "href" : "data:application/sdp;base64,base64-encoded-sdp"
    },
    "mediaOffer" : {
      "href" : "data:application/sdp;base64,base64-encoded-sdp"
    },
    "from" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/831"
    },
    "acceptedByContact" : {
      "href" : "/ucwa/v1/applications/192/people/169"
    },
    "acceptWithAnswer" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/acceptWithAnswer"
    },
    "acceptWithPhoneAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/507/acceptWithPhoneAudio"
    },
    "audioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo"
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
    "delegator" : {
      "href" : "/ucwa/v1/applications/192/people/312"
    },
    "derivedAudioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/derivedAudioVideo"
    },
    "derivedConversation" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/derivedConversation"
    },
    "forwardedBy" : {
      "href" : "/ucwa/v1/applications/192/people/776"
    },
    "onBehalfOf" : {
      "href" : "/ucwa/v1/applications/192/people/444"
    },
    "replacesAudioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/665/replacesAudioVideo"
    },
    "reportMediaDiagnostics" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/reportMediaDiagnostics"
    },
    "sendProvisionalAnswer" : {
      "href" : "/ucwa/v1/applications/192/communication/invitations/507/sendProvisionalAnswer"
    },
    "to" : {
      "href" : "/ucwa/v1/applications/192/people/107"
    },
    "transferredBy" : {
      "href" : "/ucwa/v1/applications/192/people/766"
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/audioVideo/escalateAudio HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/sdp
Accept: application/xml
Content-Length: 272

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/xml
Content-Length: 8203
<?xml version="1.0" encoding="utf-8"?>
<resource rel="audioVideoInvitation" href="/ucwa/v1/applications/192/communication/invitations/507" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="customContent" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="mediaOffer" href="data:application/sdp;base64,base64-encoded-sdp" />
  <link rel="from" href="/ucwa/v1/applications/192/communication/conversations/137/participants/831" />
  <link rel="acceptedByContact" href="/ucwa/v1/applications/192/people/169" />
  <link rel="acceptWithAnswer" href="/ucwa/v1/applications/192/communication/invitations/665/acceptWithAnswer" />
  <link rel="acceptWithPhoneAudio" href="/ucwa/v1/applications/192/communication/invitations/507/acceptWithPhoneAudio" />
  <link rel="audioVideo" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo" />
  <link rel="cancel" href="/ucwa/v1/applications/192/communication/invitations/665/cancel" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="decline" href="/ucwa/v1/applications/192/communication/invitations/665/decline" />
  <link rel="delegator" href="/ucwa/v1/applications/192/people/312" />
  <link rel="derivedAudioVideo" href="/ucwa/v1/applications/192/communication/invitations/665/derivedAudioVideo" />
  <link rel="derivedConversation" href="/ucwa/v1/applications/192/communication/invitations/665/derivedConversation" />
  <link rel="forwardedBy" href="/ucwa/v1/applications/192/people/776" />
  <link rel="onBehalfOf" href="/ucwa/v1/applications/192/people/444" />
  <link rel="replacesAudioVideo" href="/ucwa/v1/applications/192/communication/invitations/665/replacesAudioVideo" />
  <link rel="reportMediaDiagnostics" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/reportMediaDiagnostics" />
  <link rel="sendProvisionalAnswer" href="/ucwa/v1/applications/192/communication/invitations/507/sendProvisionalAnswer" />
  <link rel="to" href="/ucwa/v1/applications/192/people/107" />
  <link rel="transferredBy" href="/ucwa/v1/applications/192/people/766" />
  <property name="rel">audioVideoInvitation</property>
  <property name="address">samplevalue</property>
  <property name="bandwidthControlId">samplevalue</property>
  <property name="building">samplevalue</property>
  <property name="city">samplevalue</property>
  <property name="country">samplevalue</property>
  <property name="delegator">sip:john@contoso.com</property>
  <property name="direction">Incoming</property>
  <property name="importance">Normal</property>
  <property name="joinAudioMuted">False</property>
  <property name="joinVideoMuted">False</property>
  <property name="locationState">samplevalue</property>
  <property name="operationId">74cb7404e0a247d5a2d4eb0376a47dbf</property>
  <property name="privateLine">False</property>
  <property name="sessionContext">8efd502350ff419cb615018ae561f97e</property>
  <property name="state">Connected</property>
  <property name="subject">Strategy for next quarter</property>
  <property name="threadId">292e0aaef36c426a97757f43dda19d06</property>
  <property name="to">sip:john@contoso.com</property>
  <property name="zip">samplevalue</property>
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



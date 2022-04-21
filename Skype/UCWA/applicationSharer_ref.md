---
title: applicationSharer
description: applicationSharer represents the participant in a conversation who is currently sharing a program or her screen.
---

# applicationSharer

 _**Applies to:** Skype for Business 2015_


Represents the participant in a [conversation](conversation_ref.md) who is currently sharing a program or her screen.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

Today, the API does not support viewing this modality.However, this information can be useful for UI updates or for letting the user contact the sharer to let her know that he cannot see the content.The absence of this resource indicates that no one is sharing a program or their screen.

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

## Events
<a name="sectionSection2"></a>

### Added



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|applicationSharer|High|conversation|Indicates that a [participant](participant_ref.md) has started sharing a program or her screen for the first time in this [conversation](conversation_ref.md). The application can decide to fetch the updated information.</p><p></p>|

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
            "rel" : "applicationSharer",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing/applicationSharer"
          },
          "in" : {
            "rel" : "applicationSharing",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing"
          },
          "type" : "added"
        }
      ]
    }
  ]
}
```

### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|applicationSharer|High|conversation|Indicates that the [participant](participant_ref.md) who is sharing a program or her screen has changed. The application can decide to fetch the updated information.</p><p></p>|

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
            "rel" : "applicationSharer",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing/applicationSharer"
          },
          "in" : {
            "rel" : "applicationSharing",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}
```

### Deleted



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|applicationSharer|High|conversation|Indicates that no one is sharing a program or screen anymore. The application can decide to fetch the updated information.</p><p></p>|

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
            "rel" : "applicationSharer",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing/applicationSharer"
          },
          "in" : {
            "rel" : "applicationSharing",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing"
          },
          "type" : "deleted"
        }
      ]
    }
  ]
}
```

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the participant who is currently sharing a program or her screen.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing/applicationSharer HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 2022
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
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing/applicationSharer HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 2519
<?xml version="1.0" encoding="utf-8"?>
<resource rel="participant" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196" xmlns="http://schemas.microsoft.com/rtc/2012/03/ucwa">
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
```



# participantDataCollaboration

 _**Applies to:** Skype for Business 2015_


Represents whether a participant is using the data collaboration modality in a conversation.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource helps the application track when a participant joins or leaves this modality.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|participant|Represents a remote participant in a [conversation](conversation_ref.md).|

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
|participantDataCollaboration|High|conversation|Indicates that a [participant](participant_ref.md) is now using the [dataCollaboration](dataCollaboration_ref.md) modality.</p><p></p>|
|participantDataCollaboration|High|conversation|Indicates that the user is now using the [dataCollaboration](dataCollaboration_ref.md) modality.</p><p></p>|
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
            "rel" : "participantDataCollaboration",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration"
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
|participantDataCollaboration|High|conversation|Indicates that a [participant](participant_ref.md)'s [dataCollaboration](dataCollaboration_ref.md) modality has changed.</p><p></p>|
|participantDataCollaboration|High|conversation|Indicates that the user's [dataCollaboration](dataCollaboration_ref.md) modality has changed.</p><p></p>|
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
            "rel" : "participantDataCollaboration",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration"
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
|participantDataCollaboration|High|conversation|Indicates that a [participant](participant_ref.md) is no longer using the [dataCollaboration](dataCollaboration_ref.md) modality.</p><p></p>|
|participantDataCollaboration|High|conversation|Indicates that the user is no longer using the [dataCollaboration](dataCollaboration_ref.md) modality.</p><p></p>|
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
            "rel" : "participantDataCollaboration",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration"
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




Returns a representation of the data collaboration modality for a participant.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 271
{
  "rel" : "participantDataCollaboration",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration"
    },
    "participant" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 427
<?xml version="1.0" encoding="utf-8"?>
<resource rel="participantDataCollaboration" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196/participantDataCollaboration" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="participant" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196" />
  <property name="rel">participantDataCollaboration</property>
</resource>
```



# attendees

 _**Applies to:** Skype for Business 2015_


Represents a view of the [participant](participant_ref.md)s having the attendee role in an [onlineMeeting](onlineMeeting_ref.md).
            

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



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|participant|Represents a remote participant in a [conversation](conversation_ref.md).|

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
|participant|High|conversation|Indicates that a [participant](participant_ref.md) was added to the list of attendee participants.</p><p></p>|
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
            "rel" : "participant",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196"
          },
          "in" : {
            "rel" : "attendees",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/attendees"
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
|participant|High|conversation|Delivered when the participant resource in attendees is updated.</p><p></p>|
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
            "rel" : "participant",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196"
          },
          "in" : {
            "rel" : "attendees",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/attendees"
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
|participant|High|conversation|Indicates that a [participant](participant_ref.md) was removed from the list of attendee participants.</p><p>For example, the [participant](participant_ref.md) was promoted to presenter or left the [onlineMeeting](onlineMeeting_ref.md). Note that if the [participant](participant_ref.md) leaves the [onlineMeeting](onlineMeeting_ref.md), a deleted event will be fired by this [participant](participant_ref.md) to indicate the application can release any cached information regarding this [participant](participant_ref.md).</p>|
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
            "rel" : "participant",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/participants/196"
          },
          "in" : {
            "rel" : "attendees",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/attendees"
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




Returns a representation of a view of the [participant](participant_ref.md)s whose role is attendee in an [onlineMeeting](onlineMeeting_ref.md).

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/attendees HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 218
{
  "rel" : "attendees",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/attendees"
    },
    "participant" : [
      {
        "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/425"
      }
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/attendees HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 353
<?xml version="1.0" encoding="utf-8"?>
<resource rel="attendees" href="/ucwa/v1/applications/192/communication/conversations/137/attendees" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="participant" href="/ucwa/v1/applications/192/communication/conversations/137/participants/839" />
  <property name="rel">attendees</property>
</resource>
```



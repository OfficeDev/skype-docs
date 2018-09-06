# message

 _**Applies to:** Skype for Business 2015_


Represents an instant message sent or received by the local participant.
             

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

Message is started in the event channel for both incoming and outgoing scenarios.For outgoing instant messages, the message delivery status can be tracked via the event channel.When the message is part of a peer-to-peer conversation, the delivery status merely indicates whether the message was delivered to the remote participant.In the multi-party case, the delivery status indicates which of the remote participants failed to receive the message.For incoming instant messages, message captures information such as the remote participant who sent the message, the time stamp, and the body of the message. If an incoming instant message is received by UCWA but is not fetched by the application via the event channel in a timely fashion (within 30 seconds), the message will time out and the app will not be able to receive it.Additionally, the sender of the message will be notified that the message was not received.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|direction|The [Direction](Direction_ref.md) of this message, either incoming or outgoing.|
|htmlMessage|If populated, indicates an HTML message body.|
|operationId|A application-supplied identifier to correlate an outgoing message started in the event channel using [sendMessage](sendMessage_ref.md).|
|status|The delivery [Status](Status_ref.md) of this message.|
|plainMessage|If populated, indicates a plain text message body.|
|timeStamp|The message's time stamp.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|contact|Represents a person or service that the user can communicate and collaborate with.|
|failedDeliveryParticipant|Represents a [participant](participant_ref.md) that failed to receive an instant message sent by the user.|
|messaging|Represents the instant messaging modality in a [conversation](conversation_ref.md).|
|participant|Represents a remote participant in a [conversation](conversation_ref.md).|

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
|message|High|conversation|Delivered when a message is started for an incoming instant message.|
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
            "rel" : "message",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/messaging/messages/165"
          },
          "type" : "started"
        }
      ]
    }
  ]
}
				
```


### completed

|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|message|High|conversation|Delivered when the message is completed for an incoming instant message.|
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
 "href" : "https://fe1.contoso.com:443//v1/applications/316/communication/conversations/271",
 "events" : [
 {
 "link" : {
 "rel" : "message",
 "href" : "https://fe1.contoso.com:443//v1/applications/316/communication/conversations/271/messaging/messages/142"
 },
 "type" : "completed"
 }
 ]
 }
 ]
}
					
```

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
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/messaging/messages/165 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.

```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 773
{
  "rel" : "message",
  "direction" : "Incoming",
  "operationId" : "74cb7404e0a247d5a2d4eb0376a47dbf",
  "status" : "Failed",
  "timeStamp" : "\/Date(1474932027613)\/",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/messaging/messages/165"
    },
    "htmlMessage" : {
      "href" : "data:text/html;base64,base64-encoded-htmlmessage"
    },
    "plainMessage" : {
      "href" : "data:text/plain;charset=utf8,URLEncodedMessageString"
    },
    "contact" : {
      "href" : "/ucwa/v1/applications/192/people/282"
    },
    "failedDeliveryParticipant" : [
      {
        "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/678"
      }
    ],
    "messaging" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/messaging"
    },
    "participant" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/participants/196"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/messaging/messages/165 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 1049
<?xml version="1.0" encoding="utf-8"?>
<resource rel="message" href="/ucwa/v1/applications/192/communication/conversations/137/messaging/messages/165" xmlns="http://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="htmlMessage" href="data:text/html;base64,base64-encoded-htmlmessage" />
  <link rel="plainMessage" href="data:text/plain;charset=utf8,URLEncodedMessageString" />
  <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
  <link rel="failedDeliveryParticipant" href="/ucwa/v1/applications/192/communication/conversations/137/participants/192" />
  <link rel="messaging" href="/ucwa/v1/applications/192/communication/conversations/137/messaging" />
  <link rel="participant" href="/ucwa/v1/applications/192/communication/conversations/137/participants/196" />
  <property name="rel">message</property>
  <property name="direction">Incoming</property>
  <property name="operationId">74cb7404e0a247d5a2d4eb0376a47dbf</property>
  <property name="status">Pending</property>
  <property name="timeStamp">2016-09-26T16:20:27.6158253-07:00</property>
</resource>
```



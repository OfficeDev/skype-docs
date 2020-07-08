# conversationLogTranscript

 _**Applies to:** Skype for Business 2015_


Represents a transcript within a [conversationLog](conversationLog_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

### Properties



|**Name**|**Description**|
|:-----|:-----|
|timeStamp|The transcript's time stamp.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|contact|Represents a person or service that the user can communicate and collaborate with.|
|me|Represents the user.|
|audioTranscript|Represents an audio transcript within a [conversationLog](conversationLog_ref.md).|
|errorTranscript|Represents an error transcript within a [conversationLog](conversationLog_ref.md).|
|messageTranscript|Represents a message transcript within a [conversationLog](conversationLog_ref.md).|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of a transcript in [conversationLog](conversationLog_ref.md).

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 1226
{
  "rel" : "conversationLogTranscript",
  "timeStamp" : "\/Date(1474932024653)\/",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript"
    },
    "contact" : {
      "href" : "/ucwa/v1/applications/192/people/282"
    },
    "me" : {
      "href" : "/ucwa/v1/applications/192/me"
    }
  },
  "_embedded" : {
    "audioTranscript" : {
      "rel" : "audioTranscript",
      "duration" : "samplevalue",
      "status" : "Connected",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript/audioTranscript"
        }
      }
    },
    "errorTranscript" : {
      "rel" : "errorTranscript",
      "reason" : "TranscriptionFailed",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript/errorTranscript"
        }
      }
    },
    "messageTranscript" : {
      "rel" : "messageTranscript",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript/messageTranscript"
        },
        "htmlMessage" : {
          "href" : "data:text/html;base64,base64-encoded-htmlmessage"
        },
        "plainMessage" : {
          "href" : "data:text/plain;charset=utf8,URLEncodedMessageString"
        }
      }
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 1563
<?xml version="1.0" encoding="utf-8"?>
<resource rel="conversationLogTranscript" href="/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
  <link rel="me" href="/ucwa/v1/applications/192/me" />
  <property name="rel">conversationLogTranscript</property>
  <property name="timeStamp">2016-09-26T16:20:24.6776587-07:00</property>
  <resource rel="audioTranscript" href="/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript/audioTranscript">
    <property name="rel">audioTranscript</property>
    <property name="duration">samplevalue</property>
    <property name="status">Connected</property>
  </resource>
  <resource rel="errorTranscript" href="/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript/errorTranscript">
    <property name="rel">errorTranscript</property>
    <property name="reason">TranscriptionFailed</property>
  </resource>
  <resource rel="messageTranscript" href="/ucwa/v1/applications/192/communication/conversationLogs/conversationLog/conversationLogTranscripts/conversationLogTranscript/messageTranscript">
    <link rel="htmlMessage" href="data:text/html;base64,base64-encoded-htmlmessage" />
    <link rel="plainMessage" href="data:text/plain;charset=utf8,URLEncodedMessageString" />
    <property name="rel">messageTranscript</property>
  </resource>
</resource>
```



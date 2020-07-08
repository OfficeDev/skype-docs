# applicationSharing

 _**Applies to:** Skype for Business 2015_


Represents the application sharing modality in the corresponding [conversation](conversation_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

In this version of the API, viewing or sharing of content is not supported.However, this information can be useful for UI updates or for letting the user contact the sharer to let the user see the content that is being shared.The absence of this resource indicates that no one is sharing a program or their screen.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|state|The state of the modality, such as Connecting, Connected, or Disconnected.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|applicationSharer|Represents the participant in a [conversation](conversation_ref.md) who is currently sharing a program or her screen.|
|conversation|Represents the local participants perspective on a multi-modal, multi-party communication.|
|reportMediaDiagnostics|Represents RreportMediaDiagnostics operation.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|applicationSharing|High|conversation|Indicates that the application sharing modality was updated.</p><p></p>|
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
            "rel" : "applicationSharing",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing"
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




Returns the application sharing modality in the corresponding [conversation](conversation_ref.md).

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 495
{
  "rel" : "applicationSharing",
  "state" : "Transferring",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/applicationSharing"
    },
    "applicationSharer" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/applicationSharing/applicationSharer"
    },
    "conversation" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137"
    },
    "reportMediaDiagnostics" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137/audioVideo/reportMediaDiagnostics"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/applicationSharing HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 678
<?xml version="1.0" encoding="utf-8"?>
<resource rel="applicationSharing" href="/ucwa/v1/applications/192/communication/conversations/137/applicationSharing" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="applicationSharer" href="/ucwa/v1/applications/192/communication/conversations/137/applicationSharing/applicationSharer" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="reportMediaDiagnostics" href="/ucwa/v1/applications/192/communication/conversations/137/audioVideo/reportMediaDiagnostics" />
  <property name="rel">applicationSharing</property>
  <property name="state">Connecting</property>
</resource>
```



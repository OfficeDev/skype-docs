---
title: PhoneAudio reference for Skype for Business 2015
description: "PhoneAudio represents the phone audio modality in a conversation. It refers to communication that is delivered via a public switched telephone network or PSTN." 
---
# phoneAudio

 _**Applies to:** Skype for Business 2015_


Represents the phone audio modality in a [conversation](conversation_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

Phone audio refers to communication that is delivered via a public switched telephone network (PSTN).When present, the resource indicates the status of the phone audio channel and can provide capabilities to start or stop the phone call via a user-supplied phone number, as well as to place the call on hold.phoneAudio will be updated whenever the state and capabilities of the modality change.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|state|The state of the modality, such as Connecting, Connected, or Disconnected.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|addPhoneAudio|Adds phone audio to an existing conversation.|
|conversation|Represents the local participants perspective on a multi-modal, multi-party communication.|
|holdPhoneAudio|Places a phone call on hold.|
|resumePhoneAudio|Resumes a phone call that was placed on hold.|
|stopPhoneAudio|Stops the corresponding phone audio modality that is currently connecting or connected.|

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
|phoneAudio|High|conversation|The phoneAudio resource has changed.</p><p></p>|

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
            "rel" : "phoneAudio",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/phoneAudio"
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




Returns a representation of the phone audio modality in a [conversation](conversation_ref.md).

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/phoneAudio HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 585
{
  "rel" : "phoneAudio",
  "state" : "Transferring",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/phoneAudio"
    },
    "addPhoneAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/phoneAudio/addPhoneAudio"
    },
    "conversation" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations/137"
    },
    "holdPhoneAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/phoneAudio/holdPhoneAudio"
    },
    "resumePhoneAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/phoneAudio/resumePhoneAudio"
    },
    "stopPhoneAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/phoneAudio/stopPhoneAudio"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/phoneAudio HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 778
<?xml version="1.0" encoding="utf-8"?>
<resource rel="phoneAudio" href="/ucwa/v1/applications/192/communication/phoneAudio" xmlns="http://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="addPhoneAudio" href="/ucwa/v1/applications/192/communication/phoneAudio/addPhoneAudio" />
  <link rel="conversation" href="/ucwa/v1/applications/192/communication/conversations/137" />
  <link rel="holdPhoneAudio" href="/ucwa/v1/applications/192/communication/phoneAudio/holdPhoneAudio" />
  <link rel="resumePhoneAudio" href="/ucwa/v1/applications/192/communication/phoneAudio/resumePhoneAudio" />
  <link rel="stopPhoneAudio" href="/ucwa/v1/applications/192/communication/phoneAudio/stopPhoneAudio" />
  <property name="rel">phoneAudio</property>
  <property name="state">Transferring</property>
</resource>
```



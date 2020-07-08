# missedItems

 _**Applies to:** Skype for Business 2015_


A collection of unread voicemails and conversations.
            

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
|conversationLogsCount|The number of conversation logs.|
|conversationLogsNotifications|Gets or sets the conversation logs notifications.|
|missedConversationsCount|The number of missed conversations.|
|unreadMissedConversationsCount|The number of unread missed conversations.|
|unreadVoicemailsCount|The number of unread voicemails.|
|voiceMailsCount|The number of voicemails.|

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

### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|missedItems|High|communication|Delivered when there is an update for the missed items resource.</p><p></p>|
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
            "rel" : "missedItems",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/missedItems"
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
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/missedItems HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 285
{
  "rel" : "missedItems",
  "conversationLogsCount" : 58,
  "conversationLogsNotifications" : "Disabled",
  "missedConversationsCount" : 5,
  "unreadMissedConversationsCount" : 51,
  "unreadVoicemailsCount" : 51,
  "voiceMailsCount" : 38,
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/missedItems"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/missedItems HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 564
<?xml version="1.0" encoding="utf-8"?>
<resource rel="missedItems" href="/ucwa/v1/applications/192/communication/missedItems" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">missedItems</property>
  <property name="conversationLogsCount">97</property>
  <property name="conversationLogsNotifications">Disabled</property>
  <property name="missedConversationsCount">82</property>
  <property name="unreadMissedConversationsCount">70</property>
  <property name="unreadVoicemailsCount">1</property>
  <property name="voiceMailsCount">40</property>
</resource>
```



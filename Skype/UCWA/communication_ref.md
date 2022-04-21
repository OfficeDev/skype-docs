---
title: communication
description: Represents the dashboard for communication capabilities.
---

# communication

 _**Applies to:** Skype for Business 2015_


Represents the dashboard for communication capabilities.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource exposes the modalities and settings available to the user, including the ability to join an [onlineMeeting](onlineMeeting_ref.md) or create an ad-hoc [onlineMeeting](onlineMeeting_ref.md).Please note that this resource will be the sender for all events pertaining to [conversation](conversation_ref.md)s and modality invitations ([messagingInvitation](messagingInvitation_ref.md) or [phoneAudioInvitation](phoneAudioInvitation_ref.md)).

### Properties



|**Name**|**Description**|
|:-----|:-----|
|audioPreference|The preferred mode for incoming audio, if any, supplied by the user.VoIP is the default mode.|
|conversationHistory|Gets or sets the conversation history policy.|
|lisLocation|Represents the endpoint location returned from the Location Information Service|
|lisQueryResult|Whether the LIS query succeeded|
|phoneNumber|The phone number of the device the user wishes to be reached on.The maximum length is 100 characters.|
|publishEndpointLocation|Gets whether to set the user's endpoint location|
|supportedMessageFormats|The message formats ([MessageFormat](MessageFormat_ref.md)) that are supported by this application.|
|supportedModalities|The list of incoming modalities ([ModalityType](ModalityType_ref.md)) of interest to the user.|
|videoBasedScreenSharing|Gets whether UCWA supports Video Based Screen Sharing or not.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|conversationLogs|Represents the user's past conversation logs (both peer-to-peer and conferences).|
|conversations|Represents the user's ongoing [conversation](conversation_ref.md)s.|
|joinOnlineMeeting|Joins an online meeting.|
|mediaPolicies|Represents a resource that allows clients to fetch all media-related settings thatcannot be modeled as capability links or properties of individual resources. Mostof them are directly consumed by media stack manager on the client side.|
|mediaRelayAccessToken|Represents a media relay token.|
|missedItems|A collection of unread voicemails and conversations.|
|replayMessage|Replay message. Client would use this resource to trigger a notificationfrom an application endpoint to current ucwa application instance|
|startAudioOnBehalfOfDelegator|Represents an operation to start Audio call on behalf of the delegator. This token indicatesthe user has ability to start audio call on behalf of the delegator.|
|startAudio|Represents an operation to start AudioVideo. This token indicatesthe user has ability to start only audio.|
|startAudioVideoOnBehalfOfDelegator|Represents an operation to start AudioVideo call on behalf of the delegator. This token indicatesthe user has ability to start audiovideo call on behalf of the delegator.|
|startAudioVideo|Represents an operation to start AudioVideo. This token indicatesthe user has ability to start audio, video, or audio and video.|
|startEmergencyCall|Represents an operation to start E911 (emergency) call, provided enhancedemergency services are enabled for a registered user|
|startMessaging|Starts a [messagingInvitation](messagingInvitation_ref.md) that adds the [messaging](messaging_ref.md) modality to a new [conversation](conversation_ref.md).|
|startOnlineMeeting|Creates and joins an ad-hoc multiparty conversation.|
|startPhoneAudioOnBehalfOfDelegator|Initiates a call-via-work on behalf of the delegator.|
|startPhoneAudio|Initiates a call-via-work.|
|startVideo|Represents an operation to start AudioVideo. This token indicatesthe user has ability to start only video.|

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
|:----- |:----- |:----- |:----- |
|communication|Medium|communication|Indicates that the communication resource has been updated.</p><p>For example, this can occur after the application invokes [makeMeAvailable](makeMeAvailable_ref.md).</p>|

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
            "rel" : "communication",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/communication"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}
```
## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the dashboard for communication capabilities.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: 2f2c6e07-f7fd-478f-928b-b142a00207a1

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: c5287429-be70-4b54-a4d6-139dd04d7cad
Content-Type: application/json
Content-Length: 1957
{
  "simultaneousRingNumberMatch" : "Disabled",
  "videoBasedScreenSharing" : "Disabled",
  "rel" : "communication",
  "audioPreference" : "PhoneAudio",
  "conversationHistory" : "Disabled",
  "lisLocation" : "samplevalue",
  "lisQueryResult" : "Succeeded",
  "phoneNumber" : "tel:+14255552222",
  "publishEndpointLocation" : false,
  "supportedMessageFormats" : [
    "Plain",
    "Html"
  ],
  "supportedModalities" : [
    "PhoneAudio",
    "Messaging"
  ],
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication"
    },
    "conversationLogs" : {
      "href" : "/ucwa/v1/applications/192/communication/conversationLogs"
    },
    "conversations" : {
      "href" : "/ucwa/v1/applications/192/communication/conversations"
    },
    "joinOnlineMeeting" : {
      "href" : "/ucwa/v1/applications/192/communication/joinOnlineMeeting"
    },
    "mediaPolicies" : {
      "href" : "/ucwa/v1/applications/192/mediaPolicies"
    },
    "mediaRelayAccessToken" : {
      "href" : "/ucwa/v1/applications/192/mediaRelayAccessToken"
    },
    "missedItems" : {
      "href" : "/ucwa/v1/applications/192/communication/missedItems"
    },
    "replayMessage" : {
      "href" : "/ucwa/v1/applications/192/communication/replayMessage"
    },
    "startAudioOnBehalfOfDelegator" : {
      "href" : "/ucwa/v1/applications/192/communication/startAudioOnBehalfOfDelegator"
    },
    "startAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/startAudio"
    },
    "startAudioVideoOnBehalfOfDelegator" : {
      "href" : "/ucwa/v1/applications/192/communication/startAudioVideoOnBehalfOfDelegator"
    },
    "startAudioVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/startAudioVideo"
    },
    "startEmergencyCall" : {
      "href" : "/ucwa/v1/applications/192/communication/startEmergencyCall"
    },
    "startMessaging" : {
      "href" : "/ucwa/v1/applications/192/communication/startMessaging"
    },
    "startOnlineMeeting" : {
      "href" : "/ucwa/v1/applications/192/communication/startOnlineMeeting"
    },
    "startPhoneAudioOnBehalfOfDelegator" : {
      "href" : "/ucwa/v1/applications/192/communication/startPhoneAudioOnBehalfOfDelegator"
    },
    "startPhoneAudio" : {
      "href" : "/ucwa/v1/applications/192/communication/startPhoneAudio"
    },
    "startVideo" : {
      "href" : "/ucwa/v1/applications/192/communication/startVideo"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: 937fdce3-1213-4108-83b7-ef45c5cc7dfc

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 27b368e0-c047-40d8-b543-48080527b31e
Content-Type: application/xml
Content-Length: 2516
<?xml version="1.0" encoding="utf-8"?>
<resource rel="communication" href="/ucwa/v1/applications/192/communication" xmlns="http://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="conversationLogs" href="/ucwa/v1/applications/192/communication/conversationLogs" />
  <link rel="conversations" href="/ucwa/v1/applications/192/communication/conversations" />
  <link rel="joinOnlineMeeting" href="/ucwa/v1/applications/192/communication/joinOnlineMeeting" />
  <link rel="mediaPolicies" href="/ucwa/v1/applications/192/mediaPolicies" />
  <link rel="mediaRelayAccessToken" href="/ucwa/v1/applications/192/mediaRelayAccessToken" />
  <link rel="missedItems" href="/ucwa/v1/applications/192/communication/missedItems" />
  <link rel="replayMessage" href="/ucwa/v1/applications/192/communication/replayMessage" />
  <link rel="startAudioOnBehalfOfDelegator" href="/ucwa/v1/applications/192/communication/startAudioOnBehalfOfDelegator" />
  <link rel="startAudio" href="/ucwa/v1/applications/192/communication/startAudio" />
  <link rel="startAudioVideoOnBehalfOfDelegator" href="/ucwa/v1/applications/192/communication/startAudioVideoOnBehalfOfDelegator" />
  <link rel="startAudioVideo" href="/ucwa/v1/applications/192/communication/startAudioVideo" />
  <link rel="startEmergencyCall" href="/ucwa/v1/applications/192/communication/startEmergencyCall" />
  <link rel="startMessaging" href="/ucwa/v1/applications/192/communication/startMessaging" />
  <link rel="startOnlineMeeting" href="/ucwa/v1/applications/192/communication/startOnlineMeeting" />
  <link rel="startPhoneAudioOnBehalfOfDelegator" href="/ucwa/v1/applications/192/communication/startPhoneAudioOnBehalfOfDelegator" />
  <link rel="startPhoneAudio" href="/ucwa/v1/applications/192/communication/startPhoneAudio" />
  <link rel="startVideo" href="/ucwa/v1/applications/192/communication/startVideo" />
  <property name="simultaneousRingNumberMatch">Disabled</property>
  <property name="videoBasedScreenSharing">Disabled</property>
  <property name="rel">communication</property>
  <property name="audioPreference">PhoneAudio</property>
  <property name="conversationHistory">Disabled</property>
  <property name="lisLocation">samplevalue</property>
  <property name="lisQueryResult">Succeeded</property>
  <property name="phoneNumber">tel:+14255552222</property>
  <property name="publishEndpointLocation">False</property>
  <propertyList name="supportedMessageFormats">
    <item>Plain</item>
    <item>Html</item>
  </propertyList>
  <propertyList name="supportedModalities">
    <item>PhoneAudio</item>
    <item>Messaging</item>
  </propertyList>
</resource>
```



### PUT




Modifies the dashboard for communication capabilities.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|audioPreference|The preferred mode for incoming audio, if any, supplied by the user.VoIP is the default mode. (AudioPreference)PhoneAudio, or VoipAudio|No|
|conversationHistory|Gets or sets the conversation history policy.(GenericPolicy)None, Disabled, or Enabled|No|
|lisLocation|Represents the endpoint location returned from the Location Information ServiceString|No|
|lisQueryResult|Whether the LIS query succeededNullable (LisQueryResultEnum)Succeeded, or Failed|No|
|phoneNumber|The phone number of the device the user wishes to be reached on.The maximum length is 100 characters. String|No|
|publishEndpointLocation|Gets whether to set the user's endpoint location|No|
|supportedMessageFormats|The message formats ([MessageFormat](MessageFormat_ref.md)) that are supported by this application.Array of (MessageFormat)Plain, or Html|No|
|supportedModalities|The list of incoming modalities ([ModalityType](ModalityType_ref.md)) of interest to the user.Array of (ModalityType)Audio, Video, PhoneAudio, ApplicationSharing, Messaging, DataCollaboration, or PanoramicVideo|No|
|videoBasedScreenSharing|Gets whether UCWA supports Video Based Screen Sharing or not.(GenericPolicy)None, Disabled, or Enabled|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/communication HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
if-match: 3cfa5796-5973-48da-9133-1745e9d4cd66
Content-Length: 379
{
  &quot;simultaneousRingNumberMatch&quot; : &quot;Disabled&quot;,
  &quot;videoBasedScreenSharing&quot; : &quot;Disabled&quot;,
  &quot;rel&quot; : &quot;communication&quot;,
  &quot;audioPreference&quot; : &quot;PhoneAudio&quot;,
  &quot;conversationHistory&quot; : &quot;Disabled&quot;,
  &quot;lisLocation&quot; : &quot;samplevalue&quot;,
  &quot;lisQueryResult&quot; : &quot;Succeeded&quot;,
  &quot;phoneNumber&quot; : &quot;tel : +14255552222&quot;,
  &quot;publishEndpointLocation&quot; : false,
  &quot;supportedMessageFormats&quot; : [
    &quot;Plain&quot;,
    &quot;Html&quot;
  ],
  &quot;supportedModalities&quot; : [
    &quot;PhoneAudio&quot;,
    &quot;Messaging&quot;
  ]
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```


#### XML Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/communication HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
if-match: be7d5ece-e3b9-41e0-b157-d488c977c7d5
Content-Length: 804
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;resource xmlns=&quot;http://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;simultaneousRingNumberMatch&quot;&gt;Disabled&lt;/property&gt;
  &lt;property name=&quot;videoBasedScreenSharing&quot;&gt;Disabled&lt;/property&gt;
  &lt;property name=&quot;rel&quot;&gt;communication&lt;/property&gt;
  &lt;property name=&quot;audioPreference&quot;&gt;PhoneAudio&lt;/property&gt;
  &lt;property name=&quot;conversationHistory&quot;&gt;Disabled&lt;/property&gt;
  &lt;property name=&quot;lisLocation&quot;&gt;samplevalue&lt;/property&gt;
  &lt;property name=&quot;lisQueryResult&quot;&gt;Succeeded&lt;/property&gt;
  &lt;property name=&quot;phoneNumber&quot;&gt;tel:+14255552222&lt;/property&gt;
  &lt;property name=&quot;publishEndpointLocation&quot;&gt;False&lt;/property&gt;
  &lt;propertyList name=&quot;supportedMessageFormats&quot;&gt;
    &lt;item&gt;Plain&lt;/item&gt;
    &lt;item&gt;Html&lt;/item&gt;
  &lt;/propertyList&gt;
  &lt;propertyList name=&quot;supportedModalities&quot;&gt;
    &lt;item&gt;PhoneAudio&lt;/item&gt;
    &lt;item&gt;Messaging&lt;/item&gt;
  &lt;/propertyList&gt;
&lt;/resource&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```



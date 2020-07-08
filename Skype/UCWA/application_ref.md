# application

 _**Applies to:** Skype for Business 2015_


Represents your real-time communication application.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource represents an application on one of the user's devices.This resource is used as an entry point to start to communicate and collaborate. The application gives all supported capabilities and embeds the resources associated with the following relationships: [me](me_ref.md), [people](people_ref.md), [communication](communication_ref.md), [onlineMeetings](onlineMeetings_ref.md).The application resource will expire if the application remains idle (i.e. no HTTP requests are received for a period of time from the application) for a certain amount of time. The expiration time varies depending upon whether the application makes use of the event channel (by issuing pending GETs on events) or not.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|type|The application type.This property is used to indicate the type of device that the application is running on. In this release, only the browser type is supported.|
|culture|The culture and locale information.This property is used to control various language-specific items, such as the language of the online meeting announcement service.|
|endpointId|Gets or sets the endpoint identifier.|
|id|Id for individual registrations. This is unique per user per device.This Id is used for telemetry purposes.|
|instanceId|Gets or sets the instance identifier.|
|userAgent|The application user agent.This property specifies the identity of the application and possibly can specify information about the operating system.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|batch|Initiates an operation that groups multiple, independent HTTP operations into a single HTTP request payload.|
|events|Represents the event channel resource.|
|policies|Represents the admin policies that can apply to a user's application.|
|reportMyNetwork|Represents the reportMyNetwork resource.|
|communication|Represents the dashboard for communication capabilities.|
|me|Represents the user.|
|onlineMeetings|Represents the dashboard for viewing and scheduling online meetings.|
|people|A hub for the people with whom the logged-on user can communicate, using Skype for Business.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|
|Meetings.ReadWrite|Create Skype Meetings|Allows the app to create Skype meetings on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of an application.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: 441a2fc4-9d02-4c9d-b674-c0b8403308e8

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: a41f8c7e-39b0-4b18-b763-9469b2aa9291
Content-Type: application/json
Content-Length: 5105
{
  "rel" : "application",
  "type" : "Browser",
  "culture" : "en-us",
  "endpointId" : "samplevalue",
  "id" : "samplevalue",
  "instanceId" : "samplevalue",
  "userAgent" : "ContosoApp/1.0",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192"
    },
    "batch" : {
      "href" : "/ucwa/v1/applications/192/batch"
    },
    "events" : {
      "href" : "http://sample/ucwa/v1/applications/appId/events"
    },
    "policies" : {
      "href" : "/ucwa/v1/applications/192/policies"
    },
    "reportMyNetwork" : {
      "href" : "/ucwa/v1/applications/192/reportMyNetwork"
    }
  },
  "_embedded" : {
    "communication" : {
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
    },
    "me" : {
      "rel" : "me",
      "company" : "Microsoft",
      "department" : "Sales",
      "emailAddresses" : [
        "johndoe@contoso.com"
      ],
      "endpointUri" : "sip:johndoe@contoso.com;opaque=user:epid:0mHG5gqQylGWpPELsEK8xAAA;gruu",
      "homePhoneNumber" : "tel:+14257035449",
      "mobilePhoneNumber" : "tel:+14257035449",
      "name" : "John Doe",
      "officeLocation" : "5/1380",
      "otherPhoneNumber" : "tel:+14257035449",
      "title" : "Senior Manager",
      "uri" : "sip:johndoe@contoso.com",
      "workPhoneNumber" : "tel:+14257035449",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/me"
        },
        "callForwardingSettings" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings"
        },
        "location" : {
          "href" : "/ucwa/v1/applications/192/me/location"
        },
        "makeMeAvailable" : {
          "href" : "/ucwa/v1/applications/192/communication/makeMeAvailable"
        },
        "note" : {
          "href" : "/ucwa/v1/applications/192/me/note"
        },
        "phones" : {
          "href" : "/ucwa/v1/applications/192/me/phones"
        },
        "photo" : {
          "href" : "/ucwa/v1/applications/192/photo"
        },
        "presence" : {
          "href" : "/ucwa/v1/applications/192/me/presence"
        },
        "reportMyActivity" : {
          "href" : "/ucwa/v1/applications/192/reportMyActivity"
        }
      }
    },
    "onlineMeetings" : {
      "rel" : "onlineMeetings",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/onlineMeetings"
        },
        "myAssignedOnlineMeeting" : {
          "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/600"
        },
        "myOnlineMeetings" : {
          "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings"
        },
        "onlineMeetingDefaultValues" : {
          "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingDefaultValues"
        },
        "onlineMeetingEligibleValues" : {
          "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingEligibleValues"
        },
        "onlineMeetingInvitationCustomization" : {
          "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingInvitationCustomization"
        },
        "onlineMeetingPolicies" : {
          "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingPolicies"
        },
        "phoneDialInInformation" : {
          "href" : "/ucwa/v1/applications/192/onlineMeetings/phoneDialInInformation"
        }
      }
    },
    "people" : {
      "rel" : "people",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/people"
        },
        "myContactsAndGroupsSubscription" : {
          "href" : "/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription"
        },
        "myContacts" : {
          "href" : "/ucwa/v1/applications/192/contacts"
        },
        "myGroupMemberships" : {
          "href" : "/ucwa/v1/applications/192/myGroupMemberships"
        },
        "myGroups" : {
          "href" : "/ucwa/v1/applications/192/groups"
        },
        "myPrivacyRelationships" : {
          "href" : "/ucwa/v1/applications/192/myPrivacyRelationships"
        },
        "presenceSubscriptionMemberships" : {
          "href" : "/ucwa/v1/applications/192/presenceSubscriptionMemberships"
        },
        "presenceSubscriptions" : {
          "href" : "/ucwa/v1/applications/192/presenceSubscriptions"
        },
        "search" : {
          "href" : "/ucwa/v1/applications/192/search"
        },
        "subscribedContacts" : {
          "href" : "/ucwa/v1/applications/192/subscribedContacts"
        }
      }
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: 102258d0-5c53-415f-a283-6c056129df46

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 8519c89e-594c-4a37-97da-101cff8799a4
Content-Type: application/xml
Content-Length: 6401
<?xml version="1.0" encoding="utf-8"?>
<resource rel="application" href="/ucwa/v1/applications/192" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="batch" href="/ucwa/v1/applications/192/batch" />
  <link rel="events" href="http://sample/ucwa/v1/applications/appId/events" />
  <link rel="policies" href="/ucwa/v1/applications/192/policies" />
  <link rel="reportMyNetwork" href="/ucwa/v1/applications/192/reportMyNetwork" />
  <property name="rel">application</property>
  <property name="type">Browser</property>
  <property name="culture">en-us</property>
  <property name="endpointId">samplevalue</property>
  <property name="id">samplevalue</property>
  <property name="instanceId">samplevalue</property>
  <property name="userAgent">ContosoApp/1.0</property>
  <resource rel="communication" href="/ucwa/v1/applications/192/communication">
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
  <resource rel="me" href="/ucwa/v1/applications/192/me">
    <link rel="callForwardingSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings" />
    <link rel="location" href="/ucwa/v1/applications/192/me/location" />
    <link rel="makeMeAvailable" href="/ucwa/v1/applications/192/communication/makeMeAvailable" />
    <link rel="note" href="/ucwa/v1/applications/192/me/note" />
    <link rel="phones" href="/ucwa/v1/applications/192/me/phones" />
    <link rel="photo" href="/ucwa/v1/applications/192/photo" />
    <link rel="presence" href="/ucwa/v1/applications/192/me/presence" />
    <link rel="reportMyActivity" href="/ucwa/v1/applications/192/reportMyActivity" />
    <property name="rel">me</property>
    <property name="company">Microsoft</property>
    <property name="department">Sales</property>
    <propertyList name="emailAddresses">
      <item>johndoe@contoso.com</item>
    </propertyList>
    <property name="endpointUri">sip:johndoe@contoso.com;opaque=user:epid:0mHG5gqQylGWpPELsEK8xAAA;gruu</property>
    <property name="homePhoneNumber">tel:+14257035449</property>
    <property name="mobilePhoneNumber">tel:+14257035449</property>
    <property name="name">John Doe</property>
    <property name="officeLocation">5/1380</property>
    <property name="otherPhoneNumber">tel:+14257035449</property>
    <property name="title">Senior Manager</property>
    <property name="uri">sip:johndoe@contoso.com</property>
    <property name="workPhoneNumber">tel:+14257035449</property>
  </resource>
  <resource rel="onlineMeetings" href="/ucwa/v1/applications/192/onlineMeetings">
    <link rel="myAssignedOnlineMeeting" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/600" />
    <link rel="myOnlineMeetings" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings" />
    <link rel="onlineMeetingDefaultValues" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingDefaultValues" />
    <link rel="onlineMeetingEligibleValues" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingEligibleValues" />
    <link rel="onlineMeetingInvitationCustomization" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingInvitationCustomization" />
    <link rel="onlineMeetingPolicies" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingPolicies" />
    <link rel="phoneDialInInformation" href="/ucwa/v1/applications/192/onlineMeetings/phoneDialInInformation" />
    <property name="rel">onlineMeetings</property>
  </resource>
  <resource rel="people" href="/ucwa/v1/applications/192/people">
    <link rel="myContactsAndGroupsSubscription" href="/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription" />
    <link rel="myContacts" href="/ucwa/v1/applications/192/contacts" />
    <link rel="myGroupMemberships" href="/ucwa/v1/applications/192/myGroupMemberships" />
    <link rel="myGroups" href="/ucwa/v1/applications/192/groups" />
    <link rel="myPrivacyRelationships" href="/ucwa/v1/applications/192/myPrivacyRelationships" />
    <link rel="presenceSubscriptionMemberships" href="/ucwa/v1/applications/192/presenceSubscriptionMemberships" />
    <link rel="presenceSubscriptions" href="/ucwa/v1/applications/192/presenceSubscriptions" />
    <link rel="search" href="/ucwa/v1/applications/192/search" />
    <link rel="subscribedContacts" href="/ucwa/v1/applications/192/subscribedContacts" />
    <property name="rel">people</property>
  </resource>
</resource>
```



### DELETE




Terminates the running application. This operation will tear down all active communications and subscriptions, ultimately signing the application out.

#### Request body



None


#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Delete https://fe1.contoso.com:443/ucwa/v1/applications/192 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```


#### XML Request




```
Delete https://fe1.contoso.com:443/ucwa/v1/applications/192 HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



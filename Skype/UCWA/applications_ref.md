# applications

 _**Applies to:** Skype for Business 2015_


Represents the entry point for registering this application with the server.
            

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



None

## Operations



<a name="sectionSection2"></a>

### POST




This very first operation used to create an instance of the application resource. The application resource is the starting point for all applications to discover and navigate all available resources and capabilities of the service.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|culture|Gets or sets the Culture of the client. Cannot be null or empty.The maximum length is 100 characters. String|Yes|
|endpointId|Gets or sets a unique Id for this application. This value is required and cannot be null or empty.This value does not have to necessarily match the device id on which the application is running. But, it should be unique among applications for the same user currently running across all the devices.The maximum length is 100 characters. String|Yes|
|instanceId|Gets or sets the instance identifier.The maximum length is 100 characters. String|No|
|userAgent|Gets or sets the user agent string to be used for identifying messages sent on behalf of this application. This value is required and cannot be null or empty.The maximum length is 100 characters. String|Yes|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[application](Application_ref.md)|Represents your real-time communication application.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Forbidden when a federated Office Communications Server 2007 R2 user joins a meeting.|
|Forbidden|403|None|Indicates that user is not allowed to create mobile application.|
|Forbidden|403|None|Indicates that user is not allowed to create desktop application.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Accept: application/json
Content-Length: 120
{
  &quot;culture&quot; : &quot;en-us&quot;,
  &quot;endpointId&quot; : &quot;123456&quot;,
  &quot;instanceId&quot; : &quot;samplevalue&quot;,
  &quot;userAgent&quot; : &quot;ContosoApp/1.0(WindowsPhoneOS7.5)&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Etag: d78f484b-9a16-45fe-b115-04bed62687ee
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
Post https://fe1.contoso.com:443/ucwa/v1/applications HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Accept: application/xml
Content-Length: 318
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;culture&quot;&gt;en-us&lt;/property&gt;
  &lt;property name=&quot;endpointId&quot;&gt;123456&lt;/property&gt;
  &lt;property name=&quot;instanceId&quot;&gt;samplevalue&lt;/property&gt;
  &lt;property name=&quot;userAgent&quot;&gt;ContosoApp/1.0 (Windows Phone OS 7.5)&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Etag: 4402f751-9331-4a0d-8ef2-6ed8f11c051d
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



### OPTIONS




Provides metadata about HTTP methods supported by this resource

#### Request body



None


#### Response body



|**Item**|**Description**|
|:-----|:-----|
||Metadata returned in response to HTTP OPTIONS request.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).


#### Examples




#### JSON Request




```
Options https://fe1.contoso.com:443/ucwa/v1/applications HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 115
{
  "metadata" : [
    {
      "httpMethod" : "samplevalue",
      "parameters" : [
        {
          "name" : "samplevalue",
          "supportedValues" : [
            "samplevalue"
          ]
        }
      ]
    }
  ]
}
```


#### XML Request




```
Options https://fe1.contoso.com:443/ucwa/v1/applications HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 504
<?xml version="1.0"?>
<optionsMetadata xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <metadata>
    <httpMethodMetadata>
      <httpMethod>samplevalue</httpMethod>
      <parameters>
        <parameter>
          <name>samplevalue</name>
          <supportedValues>
            <value>samplevalue</value>
          </supportedValues>
        </parameter>
      </parameters>
    </httpMethodMetadata>
  </metadata>
</optionsMetadata>
```



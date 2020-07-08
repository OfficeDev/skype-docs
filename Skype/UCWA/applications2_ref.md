
# applications2



Represents the entry point for registering this application with the server. 


## Web Link
<a name="sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).



|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name="sectionSection1"> </a>




### Properties

None


### Links

This resource can have the following relationships.



|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|application|Represents your real-time communication application.|

## Operations
<a name="sectionSection2"> </a>




### GET

Returns a representation of apllications resource.


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

#### Examples




#### JSON Request


```

 Get https://fe1.contoso.com:443//v1/applications HTTP/1.1
 Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
 Host: fe1.contoso.com
 Accept: application/json
 
									
```


#### JSON Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.


```

 HTTP/1.1 200 OK
 Content-Type: application/json
 Content-Length: 3587
 {
 "rel" : "applications",
 "_links" : {
 "self" : {
 "href" : "//v1/applications"
 }
 },
 "_embedded" : {
 "application" : [
 {
 "rel" : "application",
 "culture" : "en-us",
 "endpointId" : "samplevalue",
 "instanceId" : "samplevalue",
 "userAgent" : "ContosoApp/1.0",
 "_links" : {
 "self" : {
 "href" : "//v1/applications/833"
 },
 "batch" : {
 "href" : "//v1/applications/833/batch"
 },
 "events" : {
 "href" : "http://sample/ucwa/v1/applications/appId/events"
 },
 "policies" : {
 "href" : "//v1/applications/833/policies"
 }
 },
 "_embedded" : {
 "communication" : {
 "simultaneousRingNumberMatch" : "Disabled",
 "rel" : "communication",
 "conversationHistory" : "Disabled",
 "phoneNumber" : "tel:+14255552222",
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
 "href" : "//v1/applications/833/communication"
 },
 "conversationLogs" : {
 "href" : "//v1/applications/833/communication/conversationLogs"
 },
 "conversations" : {
 "href" : "//v1/applications/833/communication/conversations"
 },
 "joinOnlineMeeting" : {
 "href" : "//v1/applications/833/communication/joinOnlineMeeting"
 },
 "missedItems" : {
 "href" : "//v1/applications/833/communication/missedItems"
 },
 "startMessaging" : {
 "href" : "//v1/applications/833/communication/startMessaging"
 },
 "startOnlineMeeting" : {
 "href" : "//v1/applications/833/communication/startOnlineMeeting"
 },
 "startPhoneAudio" : {
 "href" : "//v1/applications/833/communication/startPhoneAudio"
 }
 }
 },
 "me" : {
 "rel" : "me",
 "department" : "Sales",
 "emailAddresses" : [
 "johndoe@contoso.com"
 ],
 "name" : "John Doe",
 "title" : "Senior Manager",
 "uri" : "sip:johndoe@contoso.com",
 "_links" : {
 "self" : {
 "href" : "//v1/applications/833/me"
 },
 "callForwardingSettings" : {
 "href" : "//v1/applications/833/me/callForwardingSettings"
 },
 "location" : {
 "href" : "//v1/applications/833/me/location"
 },
 "makeMeAvailable" : {
 "href" : "//v1/applications/833/communication/makeMeAvailable"
 },
 "note" : {
 "href" : "//v1/applications/833/me/note"
 },
 "phones" : {
 "href" : "//v1/applications/833/me/phones"
 },
 "photo" : {
 "href" : "//v1/applications/833/photo"
 },
 "presence" : {
 "href" : "//v1/applications/833/me/presence"
 },
 "reportMyActivity" : {
 "href" : "//v1/applications/833/reportMyActivity"
 }
 }
 },
 "onlineMeetings" : {
 "rel" : "onlineMeetings",
 "_links" : {
 "self" : {
 "href" : "//v1/applications/833/onlineMeetings"
 },
 "myAssignedOnlineMeeting" : {
 "href" : "//v1/applications/833/onlineMeetings/myOnlineMeetings/318"
 },
 "myOnlineMeetings" : {
 "href" : "//v1/applications/833/onlineMeetings/myOnlineMeetings"
 },
 "onlineMeetingDefaultValues" : {
 "href" : "//v1/applications/833/onlineMeetings/onlineMeetingDefaultValues"
 },
 "onlineMeetingEligibleValues" : {
 "href" : "//v1/applications/833/onlineMeetings/onlineMeetingEligibleValues"
 },
 "onlineMeetingInvitationCustomization" : {
 "href" : "//v1/applications/833/onlineMeetings/onlineMeetingInvitationCustomization"
 },
 "onlineMeetingPolicies" : {
 "href" : "//v1/applications/833/onlineMeetings/onlineMeetingPolicies"
 },
 "phoneDialInInformation" : {
 "href" : "//v1/applications/833/onlineMeetings/phoneDialInInformation"
 }
 }
 },
 "people" : {
 "rel" : "people",
 "_links" : {
 "self" : {
 "href" : "//v1/applications/833/people"
 },
 "myContactsAndGroupsSubscription" : {
 "href" : "//v1/applications/833/people/myContactsAndGroupsSubscription"
 },
 "myContacts" : {
 "href" : "//v1/applications/833/contacts"
 },
 "myGroupMemberships" : {
 "href" : "//v1/applications/833/myGroupMemberships"
 },
 "myGroups" : {
 "href" : "//v1/applications/833/groups"
 },
 "myPrivacyRelationships" : {
 "href" : "//v1/applications/833/myPrivacyRelationships"
 },
 "presenceSubscriptionMemberships" : {
 "href" : "//v1/applications/833/presenceSubscriptionMemberships"
 },
 "presenceSubscriptions" : {
 "href" : "//v1/applications/833/presenceSubscriptions"
 },
 "search" : {
 "href" : "//v1/applications/833/search"
 },
 "subscribedContacts" : {
 "href" : "//v1/applications/833/subscribedContacts"
 }
 }
 }
 }
 }
 ]
 }
}
									
```


#### XML Request


```

 Get https://fe1.contoso.com:443//v1/applications HTTP/1.1
 Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
 Host: fe1.contoso.com
 Accept: application/xml
 
									
```


#### XML Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.


```

 HTTP/1.1 200 OK
 Content-Type: application/xml
 Content-Length: 4480
 <?xml version="1.0" encoding="utf-8"?>
<resource rel="applications" href="//v1/applications" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
 <property name="rel">applications</property>
 <resource rel="application" href="//v1/applications/833">
 <link rel="batch" href="//v1/applications/833/batch" />
 <link rel="events" href="http://sample/ucwa/v1/applications/appId/events" />
 <link rel="policies" href="//v1/applications/833/policies" />
 <property name="rel">application</property>
 <property name="culture">en-us</property>
 <property name="endpointId">samplevalue</property>
 <property name="instanceId">samplevalue</property>
 <property name="userAgent">ContosoApp/1.0</property>
 <resource rel="communication" href="//v1/applications/833/communication">
 <link rel="conversationLogs" href="//v1/applications/833/communication/conversationLogs" />
 <link rel="conversations" href="//v1/applications/833/communication/conversations" />
 <link rel="joinOnlineMeeting" href="//v1/applications/833/communication/joinOnlineMeeting" />
 <link rel="missedItems" href="//v1/applications/833/communication/missedItems" />
 <link rel="startMessaging" href="//v1/applications/833/communication/startMessaging" />
 <link rel="startOnlineMeeting" href="//v1/applications/833/communication/startOnlineMeeting" />
 <link rel="startPhoneAudio" href="//v1/applications/833/communication/startPhoneAudio" />
 <property name="simultaneousRingNumberMatch">Disabled</property>
 <property name="rel">communication</property>
 <property name="conversationHistory">Disabled</property>
 <property name="phoneNumber">tel:+14255552222</property>
 <propertyList name="supportedMessageFormats">
 <item>Plain</item>
 <item>Html</item>
 </propertyList>
 <propertyList name="supportedModalities">
 <item>PhoneAudio</item>
 <item>Messaging</item>
 </propertyList>
 </resource>
 <resource rel="me" href="//v1/applications/833/me">
 <link rel="callForwardingSettings" href="//v1/applications/833/me/callForwardingSettings" />
 <link rel="location" href="//v1/applications/833/me/location" />
 <link rel="makeMeAvailable" href="//v1/applications/833/communication/makeMeAvailable" />
 <link rel="note" href="//v1/applications/833/me/note" />
 <link rel="phones" href="//v1/applications/833/me/phones" />
 <link rel="photo" href="//v1/applications/833/photo" />
 <link rel="presence" href="//v1/applications/833/me/presence" />
 <link rel="reportMyActivity" href="//v1/applications/833/reportMyActivity" />
 <property name="rel">me</property>
 <property name="department">Sales</property>
 <propertyList name="emailAddresses">
 <item>johndoe@contoso.com</item>
 </propertyList>
 <property name="name">John Doe</property>
 <property name="title">Senior Manager</property>
 <property name="uri">sip:johndoe@contoso.com</property>
 </resource>
 <resource rel="onlineMeetings" href="//v1/applications/833/onlineMeetings">
 <link rel="myAssignedOnlineMeeting" href="//v1/applications/833/onlineMeetings/myOnlineMeetings/318" />
 <link rel="myOnlineMeetings" href="//v1/applications/833/onlineMeetings/myOnlineMeetings" />
 <link rel="onlineMeetingDefaultValues" href="//v1/applications/833/onlineMeetings/onlineMeetingDefaultValues" />
 <link rel="onlineMeetingEligibleValues" href="//v1/applications/833/onlineMeetings/onlineMeetingEligibleValues" />
 <link rel="onlineMeetingInvitationCustomization" href="//v1/applications/833/onlineMeetings/onlineMeetingInvitationCustomization" />
 <link rel="onlineMeetingPolicies" href="//v1/applications/833/onlineMeetings/onlineMeetingPolicies" />
 <link rel="phoneDialInInformation" href="//v1/applications/833/onlineMeetings/phoneDialInInformation" />
 <property name="rel">onlineMeetings</property>
 </resource>
 <resource rel="people" href="//v1/applications/833/people">
 <link rel="myContactsAndGroupsSubscription" href="//v1/applications/833/people/myContactsAndGroupsSubscription" />
 <link rel="myContacts" href="//v1/applications/833/contacts" />
 <link rel="myGroupMemberships" href="//v1/applications/833/myGroupMemberships" />
 <link rel="myGroups" href="//v1/applications/833/groups" />
 <link rel="myPrivacyRelationships" href="//v1/applications/833/myPrivacyRelationships" />
 <link rel="presenceSubscriptionMemberships" href="//v1/applications/833/presenceSubscriptionMemberships" />
 <link rel="presenceSubscriptions" href="//v1/applications/833/presenceSubscriptions" />
 <link rel="search" href="//v1/applications/833/search" />
 <link rel="subscribedContacts" href="//v1/applications/833/subscribedContacts" />
 <property name="rel">people</property>
 </resource>
 </resource>
</resource>
									
```


### POST

This very first operation used to create an instance of the application resource. The application resource is the starting point for all applications to discover and navigate all available resources and capabilities of the service.


#### Request body

None


#### Response body



|**Item**|**Description**|
|:-----|:-----|
| [application](application_ref.md)|Represents your real-time communication application.|

#### Synchronous errors

The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).



|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Forbidden when a federated Office Communications Server 2007 R2 user joins a meeting.|
|Forbidden|403|None|Indicates that user is not allowed to create mobile application.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|

#### Examples




#### JSON Request


```

 Post https://fe1.contoso.com:443//v1/applications HTTP/1.1
 Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
 Host: fe1.contoso.com
 Content-Type: application/json
 Accept: application/json
 Content-Length: 120
 {
 "culture" : "en-us",
 "endpointId" : "123456",
 "instanceId" : "samplevalue",
 "userAgent" : "ContosoApp/1.0 (Windows Phone OS 7.5)"
}
									
```


#### JSON Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.


```

 HTTP/1.1 201 Created
 Etag: f33dcfdc-3c03-4c52-b566-4b94fc6353bb
 Content-Type: application/json
 Content-Length: 3487
 {
 "rel" : "application",
 "culture" : "en-us",
 "endpointId" : "samplevalue",
 "instanceId" : "samplevalue",
 "userAgent" : "ContosoApp/1.0",
 "_links" : {
 "self" : {
 "href" : "//v1/applications/833"
 },
 "batch" : {
 "href" : "//v1/applications/833/batch"
 },
 "events" : {
 "href" : "http://sample/ucwa/v1/applications/appId/events"
 },
 "policies" : {
 "href" : "//v1/applications/833/policies"
 }
 },
 "_embedded" : {
 "communication" : {
 "simultaneousRingNumberMatch" : "Disabled",
 "rel" : "communication",
 "conversationHistory" : "Disabled",
 "phoneNumber" : "tel:+14255552222",
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
 "href" : "//v1/applications/833/communication"
 },
 "conversationLogs" : {
 "href" : "//v1/applications/833/communication/conversationLogs"
 },
 "conversations" : {
 "href" : "//v1/applications/833/communication/conversations"
 },
 "joinOnlineMeeting" : {
 "href" : "//v1/applications/833/communication/joinOnlineMeeting"
 },
 "missedItems" : {
 "href" : "//v1/applications/833/communication/missedItems"
 },
 "startMessaging" : {
 "href" : "//v1/applications/833/communication/startMessaging"
 },
 "startOnlineMeeting" : {
 "href" : "//v1/applications/833/communication/startOnlineMeeting"
 },
 "startPhoneAudio" : {
 "href" : "//v1/applications/833/communication/startPhoneAudio"
 }
 }
 },
 "me" : {
 "rel" : "me",
 "department" : "Sales",
 "emailAddresses" : [
 "johndoe@contoso.com"
 ],
 "name" : "John Doe",
 "title" : "Senior Manager",
 "uri" : "sip:johndoe@contoso.com",
 "_links" : {
 "self" : {
 "href" : "//v1/applications/833/me"
 },
 "callForwardingSettings" : {
 "href" : "//v1/applications/833/me/callForwardingSettings"
 },
 "location" : {
 "href" : "//v1/applications/833/me/location"
 },
 "makeMeAvailable" : {
 "href" : "//v1/applications/833/communication/makeMeAvailable"
 },
 "note" : {
 "href" : "//v1/applications/833/me/note"
 },
 "phones" : {
 "href" : "//v1/applications/833/me/phones"
 },
 "photo" : {
 "href" : "//v1/applications/833/photo"
 },
 "presence" : {
 "href" : "//v1/applications/833/me/presence"
 },
 "reportMyActivity" : {
 "href" : "//v1/applications/833/reportMyActivity"
 }
 }
 },
 "onlineMeetings" : {
 "rel" : "onlineMeetings",
 "_links" : {
 "self" : {
 "href" : "//v1/applications/833/onlineMeetings"
 },
 "myAssignedOnlineMeeting" : {
 "href" : "//v1/applications/833/onlineMeetings/myOnlineMeetings/318"
 },
 "myOnlineMeetings" : {
 "href" : "//v1/applications/833/onlineMeetings/myOnlineMeetings"
 },
 "onlineMeetingDefaultValues" : {
 "href" : "//v1/applications/833/onlineMeetings/onlineMeetingDefaultValues"
 },
 "onlineMeetingEligibleValues" : {
 "href" : "//v1/applications/833/onlineMeetings/onlineMeetingEligibleValues"
 },
 "onlineMeetingInvitationCustomization" : {
 "href" : "//v1/applications/833/onlineMeetings/onlineMeetingInvitationCustomization"
 },
 "onlineMeetingPolicies" : {
 "href" : "//v1/applications/833/onlineMeetings/onlineMeetingPolicies"
 },
 "phoneDialInInformation" : {
 "href" : "//v1/applications/833/onlineMeetings/phoneDialInInformation"
 }
 }
 },
 "people" : {
 "rel" : "people",
 "_links" : {
 "self" : {
 "href" : "//v1/applications/833/people"
 },
 "myContactsAndGroupsSubscription" : {
 "href" : "//v1/applications/833/people/myContactsAndGroupsSubscription"
 },
 "myContacts" : {
 "href" : "//v1/applications/833/contacts"
 },
 "myGroupMemberships" : {
 "href" : "//v1/applications/833/myGroupMemberships"
 },
 "myGroups" : {
 "href" : "//v1/applications/833/groups"
 },
 "myPrivacyRelationships" : {
 "href" : "//v1/applications/833/myPrivacyRelationships"
 },
 "presenceSubscriptionMemberships" : {
 "href" : "//v1/applications/833/presenceSubscriptionMemberships"
 },
 "presenceSubscriptions" : {
 "href" : "//v1/applications/833/presenceSubscriptions"
 },
 "search" : {
 "href" : "//v1/applications/833/search"
 },
 "subscribedContacts" : {
 "href" : "//v1/applications/833/subscribedContacts"
 }
 }
 }
 }
}
									
```


#### XML Request


```

 Post https://fe1.contoso.com:443//v1/applications HTTP/1.1
 Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
 Host: fe1.contoso.com
 Content-Type: application/xml
 Accept: application/xml
 Content-Length: 318
 <?xml version="1.0" encoding="utf-8"?>
<input xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
 <property name="culture">en-us</property>
 <property name="endpointId">123456</property>
 <property name="instanceId">samplevalue</property>
 <property name="userAgent">ContosoApp/1.0 (Windows Phone OS 7.5)</property>
</input>
									
```


#### XML Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.


```

 HTTP/1.1 201 Created
 Etag: bc27b0a9-d43a-4024-835c-fb1ab9bb4930
 Content-Type: application/xml
 Content-Length: 4371
 <?xml version="1.0" encoding="utf-8"?>
<resource rel="application" href="//v1/applications/833" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
 <link rel="batch" href="//v1/applications/833/batch" />
 <link rel="events" href="http://sample/ucwa/v1/applications/appId/events" />
 <link rel="policies" href="//v1/applications/833/policies" />
 <property name="rel">application</property>
 <property name="culture">en-us</property>
 <property name="endpointId">samplevalue</property>
 <property name="instanceId">samplevalue</property>
 <property name="userAgent">ContosoApp/1.0</property>
 <resource rel="communication" href="//v1/applications/833/communication">
 <link rel="conversationLogs" href="//v1/applications/833/communication/conversationLogs" />
 <link rel="conversations" href="//v1/applications/833/communication/conversations" />
 <link rel="joinOnlineMeeting" href="//v1/applications/833/communication/joinOnlineMeeting" />
 <link rel="missedItems" href="//v1/applications/833/communication/missedItems" />
 <link rel="startMessaging" href="//v1/applications/833/communication/startMessaging" />
 <link rel="startOnlineMeeting" href="//v1/applications/833/communication/startOnlineMeeting" />
 <link rel="startPhoneAudio" href="//v1/applications/833/communication/startPhoneAudio" />
 <property name="simultaneousRingNumberMatch">Disabled</property>
 <property name="rel">communication</property>
 <property name="conversationHistory">Disabled</property>
 <property name="phoneNumber">tel:+14255552222</property>
 <propertyList name="supportedMessageFormats">
 <item>Plain</item>
 <item>Html</item>
 </propertyList>
 <propertyList name="supportedModalities">
 <item>PhoneAudio</item>
 <item>Messaging</item>
 </propertyList>
 </resource>
 <resource rel="me" href="//v1/applications/833/me">
 <link rel="callForwardingSettings" href="//v1/applications/833/me/callForwardingSettings" />
 <link rel="location" href="//v1/applications/833/me/location" />
 <link rel="makeMeAvailable" href="//v1/applications/833/communication/makeMeAvailable" />
 <link rel="note" href="//v1/applications/833/me/note" />
 <link rel="phones" href="//v1/applications/833/me/phones" />
 <link rel="photo" href="//v1/applications/833/photo" />
 <link rel="presence" href="//v1/applications/833/me/presence" />
 <link rel="reportMyActivity" href="//v1/applications/833/reportMyActivity" />
 <property name="rel">me</property>
 <property name="department">Sales</property>
 <propertyList name="emailAddresses">
 <item>johndoe@contoso.com</item>
 </propertyList>
 <property name="name">John Doe</property>
 <property name="title">Senior Manager</property>
 <property name="uri">sip:johndoe@contoso.com</property>
 </resource>
 <resource rel="onlineMeetings" href="//v1/applications/833/onlineMeetings">
 <link rel="myAssignedOnlineMeeting" href="//v1/applications/833/onlineMeetings/myOnlineMeetings/318" />
 <link rel="myOnlineMeetings" href="//v1/applications/833/onlineMeetings/myOnlineMeetings" />
 <link rel="onlineMeetingDefaultValues" href="//v1/applications/833/onlineMeetings/onlineMeetingDefaultValues" />
 <link rel="onlineMeetingEligibleValues" href="//v1/applications/833/onlineMeetings/onlineMeetingEligibleValues" />
 <link rel="onlineMeetingInvitationCustomization" href="//v1/applications/833/onlineMeetings/onlineMeetingInvitationCustomization" />
 <link rel="onlineMeetingPolicies" href="//v1/applications/833/onlineMeetings/onlineMeetingPolicies" />
 <link rel="phoneDialInInformation" href="//v1/applications/833/onlineMeetings/phoneDialInInformation" />
 <property name="rel">onlineMeetings</property>
 </resource>
 <resource rel="people" href="//v1/applications/833/people">
 <link rel="myContactsAndGroupsSubscription" href="//v1/applications/833/people/myContactsAndGroupsSubscription" />
 <link rel="myContacts" href="//v1/applications/833/contacts" />
 <link rel="myGroupMemberships" href="//v1/applications/833/myGroupMemberships" />
 <link rel="myGroups" href="//v1/applications/833/groups" />
 <link rel="myPrivacyRelationships" href="//v1/applications/833/myPrivacyRelationships" />
 <link rel="presenceSubscriptionMemberships" href="//v1/applications/833/presenceSubscriptionMemberships" />
 <link rel="presenceSubscriptions" href="//v1/applications/833/presenceSubscriptions" />
 <link rel="search" href="//v1/applications/833/search" />
 <link rel="subscribedContacts" href="//v1/applications/833/subscribedContacts" />
 <property name="rel">people</property>
 </resource>
</resource>
									
```


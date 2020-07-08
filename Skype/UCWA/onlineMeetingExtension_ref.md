# onlineMeetingExtension

 _**Applies to:** Skype for Business 2015_


Represents custom data for the associated [onlineMeeting](onlineMeeting_ref.md) that can be used by an application. 
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

An onlineMeetingExtension resource can have zero or more optional programmer-defined properties that represent key-value pairs.An application can set and retrieve these properties at any time during the life of the [onlineMeeting](onlineMeeting_ref.md).Applications that understand a particular extension can use the data to enhance the user's meeting experience.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|id|The user-defined unique identifier for this extension.|
|type|The extension's data distribution policy, such as RoamedOrganizerData or RoamedParticipantData.The data distribution policy indicates whether the extension data will be shared onlywith the meeting organizer or with all meeting participants.|

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

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the data that is needed by a custom meeting extension, stored as a collection of key-value pair properties.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: dee2eaf8-7ad8-474c-a557-5dc070f8fccc

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 3f05e1cc-57f4-461c-91a3-87913fb16538
Content-Type: application/json
Content-Length: 215
{
  "rel" : "onlineMeetingExtension",
  "id" : "917823",
  "type" : "RoamedParticipantData",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: 8d0db68a-b75e-4e2e-9460-742345c48a62

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: f7d476b2-53a3-4e5b-8019-3c715f4cabdd
Content-Type: application/xml
Content-Length: 403
<?xml version="1.0" encoding="utf-8"?>
<resource rel="onlineMeetingExtension" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">onlineMeetingExtension</property>
  <property name="id">917823</property>
  <property name="type">RoamedParticipantData</property>
</resource>
```



### DELETE




Removes an extension.

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
Delete https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension HTTP/1.1
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
Delete https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



### PUT




Modifies an existing extension.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|id|The user-defined unique identifier for this extension.String|No|
|type|A flag that indicates the intended purpose of this extension.(OnlineMeetingExtensionType)Undefined, RoamedOrganizerData, or RoamedParticipantData|No|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[onlineMeetingExtension](OnlineMeetingExtension_ref.md)|Represents custom data for the associated [onlineMeeting](onlineMeeting_ref.md) that can be used by an application.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Accept: application/json
Content-Length: 46
{
  &quot;id&quot; : &quot;917823&quot;,
  &quot;type&quot; : &quot;RoamedParticipantData&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: c7205ee2-a3c4-40a2-b3be-c4cca4cd90a7
Content-Type: application/json
Content-Length: 215
{
  "rel" : "onlineMeetingExtension",
  "id" : "917823",
  "type" : "RoamedParticipantData",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension"
    }
  }
}
```


#### XML Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Accept: application/xml
Content-Length: 198
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;id&quot;&gt;917823&lt;/property&gt;
  &lt;property name=&quot;type&quot;&gt;RoamedParticipantData&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: cd33d95e-7a71-4003-b6c3-3344d687af8c
Content-Type: application/xml
Content-Length: 403
<?xml version="1.0" encoding="utf-8"?>
<resource rel="onlineMeetingExtension" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">onlineMeetingExtension</property>
  <property name="id">917823</property>
  <property name="type">RoamedParticipantData</property>
</resource>
```



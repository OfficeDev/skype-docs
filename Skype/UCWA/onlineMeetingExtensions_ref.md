# onlineMeetingExtensions

 _**Applies to:** Skype for Business 2015_


Represents the set of [onlineMeetingExtension](onlineMeetingExtension_ref.md)s for the associated [onlineMeeting](onlineMeeting_ref.md).
            

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



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|onlineMeetingExtension|Represents custom data for the associated [onlineMeeting](onlineMeeting_ref.md) that can be used by an application.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the  set of [onlineMeetingExtension](onlineMeetingExtension_ref.md)s for the associated [onlineMeeting](onlineMeeting_ref.md).

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 405
{
  "rel" : "onlineMeetingExtensions",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions"
    }
  },
  "_embedded" : {
    "onlineMeetingExtension" : [
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
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 602
<?xml version="1.0" encoding="utf-8"?>
<resource rel="onlineMeetingExtensions" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">onlineMeetingExtensions</property>
  <resource rel="onlineMeetingExtension" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension">
    <property name="rel">onlineMeetingExtension</property>
    <property name="id">917823</property>
    <property name="type">RoamedParticipantData</property>
  </resource>
</resource>
```



### POST




Creates a new [onlineMeetingExtension](onlineMeetingExtension_ref.md) for the associated [onlineMeeting](onlineMeeting_ref.md).

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
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions HTTP/1.1
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
HTTP/1.1 201 Created
Etag: 3bfb81d0-dc13-48ba-afcf-1543d1ea09f3
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions HTTP/1.1
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
HTTP/1.1 201 Created
Etag: cb2837dc-0ad3-402f-9987-d96e4e1682fa
Content-Type: application/xml
Content-Length: 403
<?xml version="1.0" encoding="utf-8"?>
<resource rel="onlineMeetingExtension" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">onlineMeetingExtension</property>
  <property name="id">917823</property>
  <property name="type">RoamedParticipantData</property>
</resource>
```



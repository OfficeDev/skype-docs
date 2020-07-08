# simultaneousRingSettings

 _**Applies to:** Skype for Business 2015_


Represents a user's settings to simultaneously send incoming calls to a specified target.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The user's incoming calls can be simultaneously sent to a contact, delegates, or team, as well as to the user.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|ringDelay|The wait time before a call simultaneously rings to the designated target numbers.|
|target|The currently active simultaneous ring target.This can be a custom number, delegates, or other members of the logged-on user's team.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|contact|Represents a person or service that the user can communicate and collaborate with.|
|simultaneousRingToContact|Simultaneously send all incoming calls to a user-provided number or contact in addition to the user.|
|simultaneousRingToDelegates|Simultaneously send all incoming calls to a user's delegates in addition to the user.|
|simultaneousRingToTeam|Simultaneously send all incoming calls to a user's team, in addition to the user.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of a user's settings to simultaneously send incoming calls to a specified target.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Indicates that the user is not enabled for enterprise voice.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: b3bc1546-31fd-4203-9a35-d8d7231d8af5

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 3d129906-3a18-42e0-a2ef-6421b35a7e15
Content-Type: application/json
Content-Length: 652
{
  "rel" : "simultaneousRingSettings",
  "ringDelay" : 5,
  "target" : "None",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings"
    },
    "contact" : {
      "href" : "/ucwa/v1/applications/192/people/282"
    },
    "simultaneousRingToContact" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToContact"
    },
    "simultaneousRingToDelegates" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToDelegates"
    },
    "simultaneousRingToTeam" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToTeam"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: d428ebf5-dba8-4702-bfe8-fd05e3789f31

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 1093f894-ccce-4158-aa45-e4ca3028746b
Content-Type: application/xml
Content-Length: 876
<?xml version="1.0" encoding="utf-8"?>
<resource rel="simultaneousRingSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
  <link rel="simultaneousRingToContact" href="/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToContact" />
  <link rel="simultaneousRingToDelegates" href="/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToDelegates" />
  <link rel="simultaneousRingToTeam" href="/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToTeam" />
  <property name="rel">simultaneousRingSettings</property>
  <property name="ringDelay">5</property>
  <property name="target">None</property>
</resource>
```



### PUT




Modifies a user's settings to simultaneously send incoming calls to a specified target.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|ringDelay|The wait time before a call simultaneously rings to the designated target numbers.|No|
|target|The currently active simultaneous ring target.This can be a custom number, delegates, or other members of the logged-on user's team.(SimultaneousRingTarget)None, Team, Delegates, or Contact|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Indicates that the user is not enabled for enterprise voice.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
if-match: 86500580-0450-4f92-b165-c9b340f3a887
Content-Length: 64
{
  &quot;rel&quot; : &quot;simultaneousRingSettings&quot;,
  &quot;ringDelay&quot; : 5,
  &quot;target&quot; : &quot;None&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```


#### XML Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
if-match: 03e8de5e-ce46-473f-8192-e2908d1f6eb4
Content-Length: 247
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;resource xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;rel&quot;&gt;simultaneousRingSettings&lt;/property&gt;
  &lt;property name=&quot;ringDelay&quot;&gt;5&lt;/property&gt;
  &lt;property name=&quot;target&quot;&gt;None&lt;/property&gt;
&lt;/resource&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```



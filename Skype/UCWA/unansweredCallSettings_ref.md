# unansweredCallSettings

 _**Applies to:** Skype for Business 2015_


Represents a user's settings to send unanswered calls to a specified target.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The user's incoming calls can be sent to a contact, number, delegates, or team members, if the user does not respond.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|ringDelay|The total time to wait for the the user or any target (delegates/team/custom) to answerbefore an incoming call is finally transferred to the target number.This value can range from 5 seconds to 60 seconds.|
|target|The currently active target for unanswered calls.This can be a custom number or "Voicemail". The default value is "Voicemail".|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|contact|Represents a person or service that the user can communicate and collaborate with.|
|resetUnansweredCallSettings|Resets unanswered call settings to the default values.|
|unansweredCallToContact|Forward all incoming calls to a user-provided number or contact if the user does not respond.|
|unansweredCallToVoicemail|Forward all incoming calls to the user's voicemail if she does not respond.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of a user's settings to send unanswered calls to a specified target.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Forbidden exception that occurs when the logged-on user is not enabled for enterprise voice or tries to set unanswered calls for whichthe current setting is ImmediateForward (see [CallForwardingState](CallForwardingState_ref.md)).|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: 39f9313f-ceb9-45d9-872d-cb2cd6f5bede

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 80c96214-58e7-409a-bd71-ac84efb11482
Content-Type: application/json
Content-Length: 644
{
  "rel" : "unansweredCallSettings",
  "ringDelay" : 5,
  "target" : "None",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings"
    },
    "contact" : {
      "href" : "/ucwa/v1/applications/192/people/282"
    },
    "resetUnansweredCallSettings" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/resetUnansweredCallSettings"
    },
    "unansweredCallToContact" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToContact"
    },
    "unansweredCallToVoicemail" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToVoicemail"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: 9b825a90-d221-4545-b796-0ea81323fef2

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: b28a6f77-d201-4edb-9774-af56de287c75
Content-Type: application/xml
Content-Length: 866
<?xml version="1.0" encoding="utf-8"?>
<resource rel="unansweredCallSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
  <link rel="resetUnansweredCallSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/resetUnansweredCallSettings" />
  <link rel="unansweredCallToContact" href="/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToContact" />
  <link rel="unansweredCallToVoicemail" href="/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToVoicemail" />
  <property name="rel">unansweredCallSettings</property>
  <property name="ringDelay">5</property>
  <property name="target">None</property>
</resource>
```



### PUT




Modifies a user's settings to send unanswered calls to a specified target.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|ringDelay|The total time to wait for the the user or any target (delegates/team/custom) to answerbefore an incoming call is finally transferred to the target number.This value can range from 5 seconds to 60 seconds.|No|
|target|The currently active target for unanswered calls.This can be a custom number or "Voicemail". The default value is "Voicemail".(UnansweredCallHandlingTarget)None, Voicemail, or Contact|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Forbidden exception that occurs when the logged-on user is not enabled for enterprise voice or tries to set unanswered calls for whichthe current setting is ImmediateForward (see [CallForwardingState](CallForwardingState_ref.md)).|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
if-match: 7a4de55d-50d4-4a90-bac7-400ee906ae8a
Content-Length: 62
{
  &quot;rel&quot; : &quot;unansweredCallSettings&quot;,
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
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
if-match: 300d84af-b3e1-4caa-ba1a-f1f735318477
Content-Length: 245
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;resource xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;rel&quot;&gt;unansweredCallSettings&lt;/property&gt;
  &lt;property name=&quot;ringDelay&quot;&gt;5&lt;/property&gt;
  &lt;property name=&quot;target&quot;&gt;None&lt;/property&gt;
&lt;/resource&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```



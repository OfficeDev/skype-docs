# immediateForwardSettings

 _**Applies to:** Skype for Business 2015_


Represents the settings for a user to immediately forward incoming calls to a specified target.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The user's incoming calls can be forwarded to a contact, number, delegates, or voicemail, without ringing the user's work number.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|target|The number that calls will be forwarded to.Calls can be forwarded to a Contact, Delegates, or Voicemail.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|contact|Represents a person or service that the user can communicate and collaborate with.|
|immediateForwardToContact|Immediately forward all incoming calls to a user-provided number or contact.|
|immediateForwardToDelegates|Immediately forward all incoming calls to the user's delegates.|
|immediateForwardToVoicemail|Immediately forward all incoming calls to the user's voicemail.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Represents the settings for a user to immediately forward incoming calls to a specified target.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Indicates that the user is not enabled for enterprise voice or is anonymous.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: dfa4e8bb-30db-42b3-8fdd-c350ec18bb26

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: ba61ddcb-3ea0-4b1d-b360-eeac6fe0f580
Content-Type: application/json
Content-Length: 648
{
  "rel" : "immediateForwardSettings",
  "target" : "None",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings"
    },
    "contact" : {
      "href" : "/ucwa/v1/applications/192/people/282"
    },
    "immediateForwardToContact" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToContact"
    },
    "immediateForwardToDelegates" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToDelegates"
    },
    "immediateForwardToVoicemail" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToVoicemail"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: a47def0f-29fe-475c-a3d2-ec2d09b79fc1

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 8635af96-0a1e-458d-a1e5-ee1d5ecde5cb
Content-Type: application/xml
Content-Length: 847
<?xml version="1.0" encoding="utf-8"?>
<resource rel="immediateForwardSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
  <link rel="immediateForwardToContact" href="/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToContact" />
  <link rel="immediateForwardToDelegates" href="/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToDelegates" />
  <link rel="immediateForwardToVoicemail" href="/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToVoicemail" />
  <property name="rel">immediateForwardSettings</property>
  <property name="target">None</property>
</resource>
```



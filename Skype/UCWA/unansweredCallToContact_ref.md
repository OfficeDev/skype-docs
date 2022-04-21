---
title: unansweredCallToContact
description: Forward all incoming calls to a user-provided number or contact if the user does not respond.
---

# unansweredCallToContact

 _**Applies to:** Skype for Business 2015_


Forward all incoming calls to a user-provided number or contact if the user does not respond.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The presence of this resource indicates that the user can forward her unanswered incoming calls to the number or contact of her choosing.The calls will ring at the target after the specified number of seconds.

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.

|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Forward all incoming calls to a user-provided number or contact if the user does not respond.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|ringDelay|The length of the delay, in seconds, before calls ring for the specified number or contact.The maximum value is 60 and the minimum value is 5|No|
|target|The number or contact that the user wants to forward her unanswered calls to.If the target is a number, the application should provide a tel URI, which is provided by the [contact](contact_ref.md).If the target is a contact, the application should provide a sip URI, which is provided by the [contact](contact_ref.md).The maximum length is 80 characters.|Yes|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|ringDelay|The length of the delay, in seconds, before calls ring for the specified number or contact.The maximum value is 60 and the minimum value is 5 Nullable Int32|No|
|target|The number or contact that the user wants to forward her unanswered calls to.If the target is a number, the application should provide a tel URI, which is provided by the [contact](contact_ref.md).If the target is a contact, the application should provide a sip URI, which is provided by the [contact](contact_ref.md).The maximum length is 80 characters. String|Yes|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Indicates that the user is not enabled for enterprise voice, has tried to set unanswered calls when immediate forward is set, or the administrator has disabled call forwarding.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToContact HTTP/1.1
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToContact HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



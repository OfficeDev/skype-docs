---
title: ChangeNumber resource for Skype for Business 2015
description: "ChangeNumber Changes or clears the number stored in the corresponding [phone](phone_ref.md) resource." 
---
# changeNumber

 _**Applies to:** Skype for Business 2015_


Changes or clears the number stored in the corresponding [phone](phone_ref.md) resource.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The presence of this resource indicates that the user can change the number stored in this phone resource.The server will normalize the number if necessary and if no number is provided, the existing number is deleted.Please note that the user is responsible for supplying a valid number.Additionally, this resource can be used to change the visibility of the supplied number without needing a second request.

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




Changes or clears the number stored in the corresponding [phone](phone_ref.md) resource.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|includeInContactCard|Whether the phone resource is visible to other contacts.|No|
|number|The new phone number for the corresponding [phone](phone_ref.md) resource.The maximum length is 80 characters.The maximum length is 80 characters.|No|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|includeInContactCard|Whether the phone resource is visible to other contacts.Nullable Boolean|No|
|number|The new phone number for the corresponding [phone](phone_ref.md) resource.The maximum length is 80 characters.The maximum length is 80 characters. String|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Forbidden exception which occurs when the logged in user is not enabled for enterprise voice.|
|Conflict|409|None|Conflict when trying to share a phone resouce that is read only because it is set in Active Directory|
|BadRequest|400|NormalizationFailed|Indicates that phone number normalization failed.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/phones/phone/changeNumber HTTP/1.1
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/phones/phone/changeNumber HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



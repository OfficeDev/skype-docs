# simultaneousRingToTeam

 _**Applies to:** Skype for Business 2015_


Simultaneously send all incoming calls to a user's team, in addition to the user.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The presence of this resource indicates that the user can have her calls simultaneously ring her team as well as herself.The calls ring for the user as well as her team.The user can specify a delay between the time the call rings for herself and for her team.A team is a set of contacts that receive redirected calls as a unit. (See https://technet.microsoft.com/en-us/library/dd425271(v=office.13).aspx.)This version of the API does not support team management.

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




Simultaneously send all incoming calls to a user's team in addition to the user.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|ringDelay|The length of the delay in seconds before calls ring for the user's team members.The maximum value is 55 and the minimum value is 0|No|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|ringDelay|The length of the delay in seconds before calls ring for the user's team members.The maximum value is 55 and the minimum value is 0 Nullable Int32|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Indicates that the user is not enabled for enterprise voice.|
|Forbidden|403|NoTeamMembersConfigured|Indicates that the user has no team members configured.|
|Conflict|409|NoTeamMembersConfigured|There are no team members configured.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToTeam HTTP/1.1
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToTeam HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



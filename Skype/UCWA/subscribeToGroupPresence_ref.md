# subscribeToGroupPresence

 _**Applies to:** Skype for Business 2015_


Subscribes to the presence information of all the contacts in a particular group.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The presence of the web link indicates that the application can subscribe to the presence of thecurrent group members for a specified duration.

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|

## Operations



<a name="sectionSection2"></a>

### POST




Creates a time-bound [presenceSubscription](presenceSubscription_ref.md) resource.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|duration|The presence subscription expiration time (in minutes).The maximum value is 30 and the minimum value is 10|Yes|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|duration|The presence subscription expiration time (in minutes).The maximum value is 30 and the minimum value is 10 |Yes|
|groupId|The group ID query parameter. Required parameter.The maximum length is 256 characters. String|Yes|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[presenceSubscription](PresenceSubscription_ref.md)|Represents the subscription to a user-defined set of contacts.|

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



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence?groupId=Family HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/json
Content-Length: 311
{
  "rel" : "presenceSubscription",
  "id" : "3",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/presenceSubscription"
    },
    "addToPresenceSubscription" : {
      "href" : "/ucwa/v1/applications/192/presenceSubscription/addToPresenceSubscription"
    },
    "memberships" : {
      "href" : "/ucwa/v1/applications/192/presenceSubscription/memberships"
    }
  }
}
```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence?groupId=Family HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/xml
Content-Length: 490
<?xml version="1.0" encoding="utf-8"?>
<resource rel="presenceSubscription" href="/ucwa/v1/applications/192/presenceSubscription" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="addToPresenceSubscription" href="/ucwa/v1/applications/192/presenceSubscription/addToPresenceSubscription" />
  <link rel="memberships" href="/ucwa/v1/applications/192/presenceSubscription/memberships" />
  <property name="rel">presenceSubscription</property>
  <property name="id">3</property>
</resource>
```



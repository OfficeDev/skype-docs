# myGroupMembership

 _**Applies to:** Skype for Business 2015_


Represents the [group](group_ref.md) membership of a single [contact](contact_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource captures a unique pair of [contact](contact_ref.md) and [group](group_ref.md).If a [contact](contact_ref.md) appears in multiple [group](group_ref.md)s, there will be a separate resource for each membership of this contact.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|contact|Represents a person or service that the user can communicate and collaborate with.|
|defaultGroup|Represents a persistent, system-created group where a user's contacts are placed by default.|
|group|Represents a user's persistent, personal group.|
|pinnedGroup|Represents a system-created group of contacts that the user pins or that the user frequentlycommunicates and collaborates with.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the [group](group_ref.md) membership of a single [contact](contact_ref.md).

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|The user does not have sufficient privileges to access the contact list.|
|Forbidden|403|None|The user does not have sufficient privileges to access pending contacts|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/myGroupMemberships/myGroupMembership HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 377
{
  "rel" : "myGroupMembership",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/myGroupMemberships/myGroupMembership"
    },
    "contact" : {
      "href" : "/ucwa/v1/applications/192/people/282"
    },
    "defaultGroup" : {
      "href" : "/ucwa/v1/applications/192/groups/defaultGroup"
    },
    "group" : {
      "href" : "/ucwa/v1/applications/192/groups/group"
    },
    "pinnedGroup" : {
      "href" : "/ucwa/v1/applications/192/groups/pinnedGroup"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/myGroupMemberships/myGroupMembership HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 546
<?xml version="1.0" encoding="utf-8"?>
<resource rel="myGroupMembership" href="/ucwa/v1/applications/192/myGroupMemberships/myGroupMembership" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
  <link rel="defaultGroup" href="/ucwa/v1/applications/192/groups/defaultGroup" />
  <link rel="group" href="/ucwa/v1/applications/192/groups/group" />
  <link rel="pinnedGroup" href="/ucwa/v1/applications/192/groups/pinnedGroup" />
  <property name="rel">myGroupMembership</property>
</resource>
```



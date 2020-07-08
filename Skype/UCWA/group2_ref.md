# group2

 _**Applies to:** Skype for Business 2015_


Represents a user's persistent, personal group version two.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

An application can subscribe to updates from members of this group. Updates include [presence](presence_ref.md),[location](location_ref.md), or [note](note_ref.md) changes for a specific contact.An application must call [startOrRefreshSubscriptionToContactsAndGroups](startOrRefreshSubscriptionToContactsAndGroups_ref.md) before it can receive eventswhen a group is created, modified, or removed.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|id|The group's ID.|
|name|The group's name.The maximum length is 256 characters.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|groupContacts|A collection of contact resources that belong to a particular group resource.|
|subscribeToGroupPresence|Subscribes to the presence information of all the contacts in a particular group.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|

## Events
<a name="sectionSection2"></a>

### Added



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|contact|High|people|Indicates that a specific contact was added to this group. The application can decide to fetchthe updated information.</p><p></p>|
Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
{
  "_links" : {
    "self" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=1"
    },
    "next" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=2"
    }
  },
  "sender" : [
    {
      "rel" : "people",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people",
      "events" : [
        {
          "link" : {
            "rel" : "contact",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282"
          },
          "in" : {
            "rel" : "group",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group"
          },
          "type" : "added"
        }
      ]
    }
  ]
}


### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|group|High|people|Indicates that the group has been updated. The application can decide to fetch the updatedinformation.</p><p></p>|
Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
{
  "_links" : {
    "self" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=1"
    },
    "next" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=2"
    }
  },
  "sender" : [
    {
      "rel" : "people",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people",
      "events" : [
        {
          "link" : {
            "rel" : "group",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}


### Deleted



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|contact|High|people|Indicates that a specific contact was deleted from this group. The application can decide tofetch the updated information.</p><p></p>|
Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
{
  "_links" : {
    "self" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=1"
    },
    "next" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=2"
    }
  },
  "sender" : [
    {
      "rel" : "people",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people",
      "events" : [
        {
          "link" : {
            "rel" : "contact",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282"
          },
          "in" : {
            "rel" : "group",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group"
          },
          "type" : "deleted"
        }
      ]
    }
  ]
}


## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of a user's persistent, personal group.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: b00985ad-0859-4cb5-9228-e4b901883557

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: f1702bc2-c110-4888-95f7-073dd28e30e6
Content-Type: application/json
Content-Length: 281
{
  "rel" : "group",
  "id" : "7",
  "name" : "MyPersonalGroup",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/groups/group"
    },
    "groupContacts" : {
      "href" : "/ucwa/v1/applications/192/contacts"
    },
    "subscribeToGroupPresence" : {
      "href" : "/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: 9629cc70-c068-4f2b-a520-b864246a1b61

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 6c289c1e-65ed-4af6-9cfa-63672210473f
Content-Type: application/xml
Content-Length: 468
<?xml version="1.0" encoding="utf-8"?>
<resource rel="group" href="/ucwa/v1/applications/192/groups/group" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="groupContacts" href="/ucwa/v1/applications/192/contacts" />
  <link rel="subscribeToGroupPresence" href="/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence" />
  <property name="rel">group</property>
  <property name="id">7</property>
  <property name="name">MyPersonalGroup</property>
</resource>
```



### DELETE




Deletes group

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
Delete https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group HTTP/1.1
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
Delete https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



### PUT




Updates a group

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|id|The group's ID.String|No|
|name|The group's name.The maximum length is 256 characters. String|Yes|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
if-match: 60b04caa-82d2-479d-b839-e7270037d1c3
Content-Length: 49
{
  &quot;rel&quot; : &quot;group&quot;,
  &quot;id&quot; : &quot;7&quot;,
  &quot;name&quot; : &quot;MyPersonalGroup&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```


#### XML Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/group HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
if-match: 78b69606-3200-44e0-9cad-cea7bcb3d17b
Content-Length: 230
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;resource xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;rel&quot;&gt;group&lt;/property&gt;
  &lt;property name=&quot;id&quot;&gt;7&lt;/property&gt;
  &lt;property name=&quot;name&quot;&gt;MyPersonalGroup&lt;/property&gt;
&lt;/resource&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```



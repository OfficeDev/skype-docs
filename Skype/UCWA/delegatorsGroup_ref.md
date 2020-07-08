# delegatorsGroup

 _**Applies to:** Skype for Business 2015_


Represents a group of users designated as delegates.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

A delegates group is a logical container for users authorized as delegates.An application must call [startOrRefreshSubscriptionToContactsAndGroups](startOrRefreshSubscriptionToContactsAndGroups_ref.md) before it can receive eventswhen a delegateGroup is created, modified, or removed.

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
|delegatorsGroup|High|people|Indicates that the delegators group was added. The application can decide to fetchthe updated information.</p><p></p>|
|contact|Low|people|Indicates that a specific delegator was added to this group. The application can decide to fetchthe updated information.</p><p></p>|
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
            "rel" : "delegatorsGroup",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/delegatorsGroup"
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
|delegatorsGroup|High|people|Indicates that the delegators group has been updated. The application can decide to fetch theupdated information.</p><p></p>|
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
            "rel" : "delegatorsGroup",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/delegatorsGroup"
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
|delegatorsGroup|High|people|Indicates that the delegators group was deleted. The application can decide tofetch the updated information.</p><p></p>|
|contact|Low|people|Indicates that a specific delegator was deleted from this group. The application can decide tofetch the updated information.</p><p></p>|
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
            "rel" : "delegatorsGroup",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/delegatorsGroup"
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




Returns a representation of a group of users for which the user is a delegate.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/delegatorsGroup HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 301
{
  "rel" : "delegatorsGroup",
  "id" : "7",
  "name" : "MyPersonalGroup",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/groups/delegatorsGroup"
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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/delegatorsGroup HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 498
<?xml version="1.0" encoding="utf-8"?>
<resource rel="delegatorsGroup" href="/ucwa/v1/applications/192/groups/delegatorsGroup" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="groupContacts" href="/ucwa/v1/applications/192/contacts" />
  <link rel="subscribeToGroupPresence" href="/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence" />
  <property name="rel">delegatorsGroup</property>
  <property name="id">7</property>
  <property name="name">MyPersonalGroup</property>
</resource>
```



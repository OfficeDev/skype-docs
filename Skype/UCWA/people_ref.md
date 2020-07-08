# people

 _**Applies to:** Skype for Business 2015_


A hub for the people with whom the logged-on user can communicate, using Skype for Business.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource provides access to resources that enable the logged-on user to find, organize, communicate with,and subscribe to the presence of other people.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|myContactsAndGroupsSubscription|Represents the subscription to a user's contacts and groups.|
|myContacts|A collection of contact resources that belong to the logged-on user.|
|myGroupMemberships|A collection of groupMembership resources, each of which uniquely links a contact to a group.|
|myGroups|A collection of groups in the contact list of the logged-on user.|
|myPrivacyRelationships|Represents the various privacy relationships that the user maintains with his or her contacts.|
|presenceSubscriptionMemberships|A collection of [presenceSubscriptionMembership](presenceSubscriptionMembership_ref.md) resources.|
|presenceSubscriptions|Represents the user's set of [presenceSubscription](presenceSubscription_ref.md) resources.|
|search|Provides a way to search for contacts.|
|subscribedContacts|A collection of contacts for which the logged-on user has created active presence subscriptions.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the hub for the people with whom the logged-on user can communicate, using Skype for Business.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/people HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 780
{
  "rel" : "people",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/people"
    },
    "myContactsAndGroupsSubscription" : {
      "href" : "/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription"
    },
    "myContacts" : {
      "href" : "/ucwa/v1/applications/192/contacts"
    },
    "myGroupMemberships" : {
      "href" : "/ucwa/v1/applications/192/myGroupMemberships"
    },
    "myGroups" : {
      "href" : "/ucwa/v1/applications/192/groups"
    },
    "myPrivacyRelationships" : {
      "href" : "/ucwa/v1/applications/192/myPrivacyRelationships"
    },
    "presenceSubscriptionMemberships" : {
      "href" : "/ucwa/v1/applications/192/presenceSubscriptionMemberships"
    },
    "presenceSubscriptions" : {
      "href" : "/ucwa/v1/applications/192/presenceSubscriptions"
    },
    "search" : {
      "href" : "/ucwa/v1/applications/192/search"
    },
    "subscribedContacts" : {
      "href" : "/ucwa/v1/applications/192/subscribedContacts"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/people HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 978
<?xml version="1.0" encoding="utf-8"?>
<resource rel="people" href="/ucwa/v1/applications/192/people" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="myContactsAndGroupsSubscription" href="/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription" />
  <link rel="myContacts" href="/ucwa/v1/applications/192/contacts" />
  <link rel="myGroupMemberships" href="/ucwa/v1/applications/192/myGroupMemberships" />
  <link rel="myGroups" href="/ucwa/v1/applications/192/groups" />
  <link rel="myPrivacyRelationships" href="/ucwa/v1/applications/192/myPrivacyRelationships" />
  <link rel="presenceSubscriptionMemberships" href="/ucwa/v1/applications/192/presenceSubscriptionMemberships" />
  <link rel="presenceSubscriptions" href="/ucwa/v1/applications/192/presenceSubscriptions" />
  <link rel="search" href="/ucwa/v1/applications/192/search" />
  <link rel="subscribedContacts" href="/ucwa/v1/applications/192/subscribedContacts" />
  <property name="rel">people</property>
</resource>
```



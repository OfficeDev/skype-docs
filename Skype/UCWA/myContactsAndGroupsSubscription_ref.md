# myContactsAndGroupsSubscription

 _**Applies to:** Skype for Business 2015_


Represents the subscription to a user's contacts and groups.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The application can use this resource to keep track of a user's contacts and groups via the event channel.Updates include the addition, removal, or modification of [group](group_ref.md)s or [contact](contact_ref.md)s.Additionally, an update on the event channel will inform the application that the subscription is about to expire.The application can then choose to refresh the subscription. Note that, unlike [presenceSubscription](presenceSubscription_ref.md), this resource does not subscribe to [presence](presence_ref.md), [note](note_ref.md), or [location](location_ref.md).

### Properties



|**Name**|**Description**|
|:-----|:-----|
|state|The subscription state ([SubscriptionState](SubscriptionState_ref.md)) such as Connecting, Connected, or Disconnected.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|startOrRefreshSubscriptionToContactsAndGroups|Starts or refreshes the subscription to a user's contacts and groups.|
|stopSubscriptionToContactsAndGroups|Stops the subscription to a user's contacts and groups.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|

## Events
<a name="sectionSection2"></a>

### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|myContactsAndGroupsSubscription|Medium|people|Indicates that the subscription will soon expire, giving the application a chance to refresh it.</p><p></p>|
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
            "rel" : "myContactsAndGroupsSubscription",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}


## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the subscription to a user's contacts and groups.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 476
{
  "rel" : "myContactsAndGroupsSubscription",
  "state" : "Connecting",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription"
    },
    "startOrRefreshSubscriptionToContactsAndGroups" : {
      "href" : "/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription/startOrRefreshSubscriptionToContactsAndGroups"
    },
    "stopSubscriptionToContactsAndGroups" : {
      "href" : "/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription/stopSubscriptionToContactsAndGroups"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 666
<?xml version="1.0" encoding="utf-8"?>
<resource rel="myContactsAndGroupsSubscription" href="/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="startOrRefreshSubscriptionToContactsAndGroups" href="/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription/startOrRefreshSubscriptionToContactsAndGroups" />
  <link rel="stopSubscriptionToContactsAndGroups" href="/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription/stopSubscriptionToContactsAndGroups" />
  <property name="rel">myContactsAndGroupsSubscription</property>
  <property name="state">Connecting</property>
</resource>
```



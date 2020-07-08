# startOrRefreshSubscriptionToContactsAndGroups

 _**Applies to:** Skype for Business 2015_


Starts or refreshes the subscription to a user's contacts and groups.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The presence of this resource indicates that a user can begin or extend a [myContactsAndGroupsSubscription](myContactsAndGroupsSubscription_ref.md).

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




Starts or refreshes the subscription to a user's contacts and groups.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|duration|The desired length, in minutes, of the subscription.For a new subscription, the length will be used.For an existing subscription, the length will be added to the remaining duration. If this sum is greater than 30 minutes, 30 minutes will be used.The maximum value is 60 and the minimum value is 10|Yes|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|duration|The desired length, in minutes, of the subscription.For a new subscription, the length will be used.For an existing subscription, the length will be added to the remaining duration. If this sum is greater than 30 minutes, 30 minutes will be used.The maximum value is 60 and the minimum value is 10 |Yes|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[myContactsAndGroupsSubscription](mycontactsandgroupssubscription_ref.md)|Represents the subscription to a user's contacts and groups.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|The user does not have sufficient privileges to modify the contact list.|
|Conflict|409|None|Conflict occurs when the resource is not in the proper state to accept the request.|
|Forbidden|403|None|The user does not have sufficient privileges to access or set delegates|
|Forbidden|403|None|The user does not have sufficient privileges to access or expand Distribution Groups|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription/startOrRefreshSubscriptionToContactsAndGroups HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/people/myContactsAndGroupsSubscription/startOrRefreshSubscriptionToContactsAndGroups HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
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



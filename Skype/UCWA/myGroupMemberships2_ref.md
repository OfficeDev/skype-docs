# myGroupMemberships2

 _**Applies to:** Skype for Business 2015_


Represents the version two of MyGroupMembershipsResource (a collection of groupMembership resources)
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The version two supports adding single contact to a particular group and removing a contact from the buddy list (all groups associated)

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|myGroupMembership|Represents the [group](group_ref.md) membership of a single [contact](contact_ref.md).|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|
|Meetings.ReadWrite|Create Skype Meetings|Allows the app to create Skype meetings on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Get group memberships

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|groupId|The group id of the group for which memberships are requested.The maximum length is 256 characters.|No|


#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|OperationNotSupported|Indicates group membership for delegators group cannot be retrieved|
|Forbidden|403|OperationNotSupported|Indicates group membership for my organization group cannot be retrieved|
|Forbidden|403|None|The user does not have sufficient privileges to access the contact list.|
|Forbidden|403|None|The user does not have sufficient privileges to access pending contacts|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/myGroupMemberships HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 592
{
  "rel" : "myGroupMemberships",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/myGroupMemberships"
    }
  },
  "_embedded" : {
    "myGroupMembership" : [
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
          "delegatesGroup" : {
            "href" : "/ucwa/v1/applications/192/groups/delegatesGroup"
          },
          "group" : {
            "href" : "/ucwa/v1/applications/192/groups/group"
          },
          "pinnedGroup" : {
            "href" : "/ucwa/v1/applications/192/groups/pinnedGroup"
          }
        }
      }
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/myGroupMemberships HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 778
<?xml version="1.0" encoding="utf-8"?>
<resource rel="myGroupMemberships" href="/ucwa/v1/applications/192/myGroupMemberships" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">myGroupMemberships</property>
  <resource rel="myGroupMembership" href="/ucwa/v1/applications/192/myGroupMemberships/myGroupMembership">
    <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
    <link rel="defaultGroup" href="/ucwa/v1/applications/192/groups/defaultGroup" />
    <link rel="delegatesGroup" href="/ucwa/v1/applications/192/groups/delegatesGroup" />
    <link rel="group" href="/ucwa/v1/applications/192/groups/group" />
    <link rel="pinnedGroup" href="/ucwa/v1/applications/192/groups/pinnedGroup" />
    <property name="rel">myGroupMembership</property>
  </resource>
</resource>
```



### POST




Adds a contactUri to a particular group.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|contactUri|Gets or sets the group membership pair's contactUri.The maximum length is 250 characters.|Yes|
|groupId|Gets or sets the group membership pair's groupId.The maximum length is 256 characters.|No|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|contactUri|Gets or sets the group membership pair's contactUri.The maximum length is 250 characters. String|Yes|
|groupId|Gets or sets the group membership pair's groupId.The maximum length is 256 characters. String|No|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[myGroupMembership](myGroupMembership2_ref.md)|Represents the version two of MyGroupMembershipResource (a [group](group_ref.md) membership of a single [contact](contact_ref.md))|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|MigrationInProgress|Indicates that adding or removing contact fails during migration|
|Conflict|409|AlreadyExists|Indicates that adding the contact fails because the contact is already present in the group|
|Forbidden|403|None|Indicates that adding a contact to a distribution group is forbidden|
|Forbidden|403|OperationNotSupported|Indicates that adding a contact to the delegators group is forbidden|
|Forbidden|403|OperationNotSupported|Indicates that adding a contact to the my organization group is forbidden|
|Forbidden|403|LimitExceeded|Indicates that adding the delegate fails because the maximum number of delegates limit (25) is exceeded|
|Conflict|409|LimitExceeded|Indicates that adding the contact fails because the maximum number of contacts limit (default is 250) is exceeded|
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/myGroupMemberships HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/json
Content-Length: 453
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
    "delegatesGroup" : {
      "href" : "/ucwa/v1/applications/192/groups/delegatesGroup"
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/myGroupMemberships HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/xml
Content-Length: 630
<?xml version="1.0" encoding="utf-8"?>
<resource rel="myGroupMembership" href="/ucwa/v1/applications/192/myGroupMemberships/myGroupMembership" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
  <link rel="defaultGroup" href="/ucwa/v1/applications/192/groups/defaultGroup" />
  <link rel="delegatesGroup" href="/ucwa/v1/applications/192/groups/delegatesGroup" />
  <link rel="group" href="/ucwa/v1/applications/192/groups/group" />
  <link rel="pinnedGroup" href="/ucwa/v1/applications/192/groups/pinnedGroup" />
  <property name="rel">myGroupMembership</property>
</resource>
```



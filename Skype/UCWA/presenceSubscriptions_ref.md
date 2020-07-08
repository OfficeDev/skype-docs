# presenceSubscriptions

 _**Applies to:** Skype for Business 2015_


Represents the user's set of [presenceSubscription](presenceSubscription_ref.md) resources.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

/// This resource can be used to create new [presenceSubscription](presenceSubscription_ref.md)s as well as to modify and delete existing ones.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|presenceSubscription|Represents the subscription to a user-defined set of contacts.|

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




Returns a representation of a collection of [presenceSubscription](presenceSubscription_ref.md) resources.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|The user does not have sufficient privileges to access or modify presence subscriptions.|
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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/presenceSubscriptions HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 459
{
  "rel" : "presenceSubscriptions",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/presenceSubscriptions"
    }
  },
  "_embedded" : {
    "presenceSubscription" : [
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
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/presenceSubscriptions HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 647
<?xml version="1.0" encoding="utf-8"?>
<resource rel="presenceSubscriptions" href="/ucwa/v1/applications/192/presenceSubscriptions" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">presenceSubscriptions</property>
  <resource rel="presenceSubscription" href="/ucwa/v1/applications/192/presenceSubscription">
    <link rel="addToPresenceSubscription" href="/ucwa/v1/applications/192/presenceSubscription/addToPresenceSubscription" />
    <link rel="memberships" href="/ucwa/v1/applications/192/presenceSubscription/memberships" />
    <property name="rel">presenceSubscription</property>
    <property name="id">3</property>
  </resource>
</resource>
```



### POST




Creates a new subscription to a user-defined set of contacts.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|duration|The duration, in minutes, for the presence subscription.The maximum value is 30 and the minimum value is 10 |Yes|
|uris|The URIs of the users whose presence is to be subscribed to.Array of String|Yes|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[presenceSubscription](PresenceSubscription_ref.md)|Represents the subscription to a user-defined set of contacts.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|The user does not have sufficient privileges to access or modify presence subscriptions.|
|Forbidden|403|LimitExceeded|The user has reached the maximum number of subscriptions that can be created.|
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




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/presenceSubscriptions HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Accept: application/json
Content-Length: 85
{
  &quot;duration&quot; : 15,
  &quot;uris&quot; : [
    &quot;\&quot;sip : johndoe@contoso.com\&quot;&quot;,
    &quot;\&quot;sip : janedoe@contoso.com\&quot;&quot;
  ]
}
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/presenceSubscriptions HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Accept: application/xml
Content-Length: 264
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;duration&quot;&gt;15&lt;/property&gt;
  &lt;propertyList name=&quot;uris&quot;&gt;
    &lt;item&gt;&quot;sip:johndoe@contoso.com&quot;&lt;/item&gt;
    &lt;item&gt;&quot; sip:janedoe@contoso.com&quot;&lt;/item&gt;
  &lt;/propertyList&gt;
&lt;/input&gt;
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



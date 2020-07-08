# presence

 _**Applies to:** Skype for Business 2015_


Represents the user's availability and activity.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

[presence](presence_ref.md) is updated when the user's availability or activity changes.The user can express her willingness to communicate by manually changing her presence.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|activity|The user's current activity encoded in UTF-16 and in the locale specified during application creation.|
|availability|The user's availability ([Availability](Availability_ref.md)), such as Away or Busy.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.

|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Added



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|presence|Medium|me|Indicates that the application is no longer in lurker mode. The application will now receive the user's presence updates.|

Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```json
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
      "rel" : "me",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me",
      "events" : [
        {
          "link" : {
            "rel" : "presence",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me/presence"
          },
          "type" : "added"
        }
      ]
    }
  ]
}
```

### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|presence|Medium|me|Indicates the user's presence has changed.|

Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```json
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
      "rel" : "me",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me",
      "events" : [
        {
          "link" : {
            "rel" : "presence",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me/presence"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}
```

### Deleted



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|presence|Medium|me|Indicates that the application will no longer receive the user's presence updates.|

Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```json
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
      "rel" : "me",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me",
      "events" : [
        {
          "link" : {
            "rel" : "presence",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me/presence"
          },
          "type" : "deleted"
        }
      ]
    }
  ]
}
```

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the user's availability and activity.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Conflict|409|MakeMeAvailableRequired|Returned when the user has not made herself available to communicate ([makeMeAvailable](makeMeAvailable_ref.md)) or is anonymous.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/presence HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 131
{
  "rel" : "presence",
  "activity" : "Off Work",
  "availability" : "Online",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me/presence"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/presence HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 311
<?xml version="1.0" encoding="utf-8"?>
<resource rel="presence" href="/ucwa/v1/applications/192/me/presence" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">presence</property>
  <property name="activity">Off Work</property>
  <property name="availability">DoNotDisturb</property>
</resource>
```



### POST




Sets the user's presence. An empty body will reset the user's presence.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|availability|The user's availability ([PreferredAvailability](PreferredAvailability_ref.md)).Nullable (PreferredAvailability)Online, Busy, DoNotDisturb, BeRightBack, Away, or Offwork|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Conflict|409|MakeMeAvailableRequired|Returned when the user has not made herself available to communicate ([makeMeAvailable](makeMeAvailable_ref.md)) or is anonymous.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/presence HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 30
{
  &quot;availability&quot; : &quot;BeRightBack&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/presence HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Content-Length: 154
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;availability&quot;&gt;Online&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



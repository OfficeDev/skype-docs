# note

 _**Applies to:** Skype for Business 2015_


Represents the user's personal or out-of-office note.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

 note aggregates the user's personal and out-of-office notes into one displayable string. The application can only set the personal note. The out-of-office note gets displayed when set in Exchange.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|message|A string representing the note text.|
|type|The note type ([NoteType](NoteType_ref.md)) gives a hint to the user as to what type of note is being displayed, either Personal or OutOfOffice.|

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
|note|Medium|me|Indicates that the application is no longer in lurker mode. The application will now receive the user's note updates.</p><p></p>|
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
      "rel" : "me",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me",
      "events" : [
        {
          "link" : {
            "rel" : "note",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me/note"
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
|note|Medium|me|Indicates the user's note has changed.</p><p></p>|
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
      "rel" : "me",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me",
      "events" : [
        {
          "link" : {
            "rel" : "note",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me/note"
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
|note|Medium|me|Indicates that the application will no longer receive the user's note updates.</p><p></p>|
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
      "rel" : "me",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me",
      "events" : [
        {
          "link" : {
            "rel" : "note",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me/note"
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




Returns a representation of the user's personal or out-of-office note.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Conflict|409|MakeMeAvailableRequired|Returned when an application tries to get or set the user's note without first making the user available ([makeMeAvailable](makeMeAvailable_ref.md)).|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/note HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 124
{
  "rel" : "note",
  "message" : "Heads down today",
  "type" : "Personal",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me/note"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/note HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 294
<?xml version="1.0" encoding="utf-8"?>
<resource rel="note" href="/ucwa/v1/applications/192/me/note" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">note</property>
  <property name="message">Heads down today</property>
  <property name="type">Personal</property>
</resource>
```



### POST




Publish a new self note or reset the note.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|message|Sets the note message. If blank, the note will be reset.|No|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|message|Sets the note message. If blank, the note will be reset.String|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Conflict|409|MakeMeAvailableRequired|Returned when an application tries to get or set the user's note without first making the user available ([makeMeAvailable](makeMeAvailable_ref.md)).|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/note HTTP/1.1
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
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/me/note HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```



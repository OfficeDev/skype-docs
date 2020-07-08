# me

 _**Applies to:** Skype for Business 2015_


Represents the user.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The me resource will be updated whenever the application becomes ready for incoming calls and leaves lurker mode ([makeMeAvailable](makeMeAvailable_ref.md)). Note that me will not be updated if any of its properties, such as emailAddresses or title, change while the application is active.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|company|The user's company.|
|department|The user's department.|
|emailAddresses|The user's e-mail addresses.|
|endpointUri|The URI specific to the current app endpoint user is logged on to.|
|homePhoneNumber|The user's home phone number.|
|mobilePhoneNumber|The user's mobile phone number.|
|name|The user's display name.|
|officeLocation|The user's office location.|
|otherPhoneNumber|The user's other phone number.|
|title|The user's title.|
|uri|The user's URI.|
|workPhoneNumber|The user's work phone number.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|callForwardingSettings|Represents settings that govern call forwarding.|
|location|Represents the user's location.|
|makeMeAvailable|Makes the user available for incoming communications.|
|note|Represents the user's personal or out-of-office note.|
|phones|A collection of phone resources that represent the phone numbers of the logged-on user.|
|photo|Represents the user's photo.|
|presence|Represents the user's availability and activity.|
|reportMyActivity|Indicates that the user is actively using this application.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|me|Medium|me|The application is no longer in lurker mode and is therefore visible to contacts. The application can now set things such as the user's presence and note.</p><p></p>|
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
            "rel" : "me",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/me"
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




Returns a representation of the user.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Conflict|409|None|Returned when an application is in the process of initializing or terminating.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 1018
{
  "rel" : "me",
  "company" : "Microsoft",
  "department" : "Sales",
  "emailAddresses" : [
    "johndoe@contoso.com"
  ],
  "endpointUri" : "sip:johndoe@contoso.com;opaque=user:epid:0mHG5gqQylGWpPELsEK8xAAA;gruu",
  "homePhoneNumber" : "tel:+14257035449",
  "mobilePhoneNumber" : "tel:+14257035449",
  "name" : "John Doe",
  "officeLocation" : "5/1380",
  "otherPhoneNumber" : "tel:+14257035449",
  "title" : "Senior Manager",
  "uri" : "sip:johndoe@contoso.com",
  "workPhoneNumber" : "tel:+14257035449",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me"
    },
    "callForwardingSettings" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings"
    },
    "location" : {
      "href" : "/ucwa/v1/applications/192/me/location"
    },
    "makeMeAvailable" : {
      "href" : "/ucwa/v1/applications/192/communication/makeMeAvailable"
    },
    "note" : {
      "href" : "/ucwa/v1/applications/192/me/note"
    },
    "phones" : {
      "href" : "/ucwa/v1/applications/192/me/phones"
    },
    "photo" : {
      "href" : "/ucwa/v1/applications/192/photo"
    },
    "presence" : {
      "href" : "/ucwa/v1/applications/192/me/presence"
    },
    "reportMyActivity" : {
      "href" : "/ucwa/v1/applications/192/reportMyActivity"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 1499
<?xml version="1.0" encoding="utf-8"?>
<resource rel="me" href="/ucwa/v1/applications/192/me" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="callForwardingSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings" />
  <link rel="location" href="/ucwa/v1/applications/192/me/location" />
  <link rel="makeMeAvailable" href="/ucwa/v1/applications/192/communication/makeMeAvailable" />
  <link rel="note" href="/ucwa/v1/applications/192/me/note" />
  <link rel="phones" href="/ucwa/v1/applications/192/me/phones" />
  <link rel="photo" href="/ucwa/v1/applications/192/photo" />
  <link rel="presence" href="/ucwa/v1/applications/192/me/presence" />
  <link rel="reportMyActivity" href="/ucwa/v1/applications/192/reportMyActivity" />
  <property name="rel">me</property>
  <property name="company">Microsoft</property>
  <property name="department">Sales</property>
  <propertyList name="emailAddresses">
    <item>johndoe@contoso.com</item>
  </propertyList>
  <property name="endpointUri">sip:johndoe@contoso.com;opaque=user:epid:0mHG5gqQylGWpPELsEK8xAAA;gruu</property>
  <property name="homePhoneNumber">tel:+14257035449</property>
  <property name="mobilePhoneNumber">tel:+14257035449</property>
  <property name="name">John Doe</property>
  <property name="officeLocation">5/1380</property>
  <property name="otherPhoneNumber">tel:+14257035449</property>
  <property name="title">Senior Manager</property>
  <property name="uri">sip:johndoe@contoso.com</property>
  <property name="workPhoneNumber">tel:+14257035449</property>
</resource>
```



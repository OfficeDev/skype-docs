# contactPresence

 _**Applies to:** Skype for Business 2015_


Represents a [contact](contact_ref.md)'s availability and activity.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

contactPresence is updated when a [contact](contact_ref.md)'s availability or activity changes.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|activity|The contact's current activity encoded in UTF-16 and in the locale specified during application creation.The value of this string is localized in the current application's culture provided the localized string exists in the contact's publication.Otherwise, the value of this string is one of the well known activity strings in which case the application is responsible for the localization.The well known ones are "in-a-meeting", "urgent-interruptions-only", "on-the-phone", "in-a-conference", "off-work", and "presenting".|
|availability|The contact's availability ([Availability](Availability_ref.md)), such as Away or Busy.|
|deviceType|The type of device ([ContactDeviceType](ContactDeviceType_ref.md)) that the contact is currently using.|
|lastActive|The last date and time the contact used her device. In JSON, this is represented as POSIX time. In XML, as ISO 8601.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|

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
|contactPresence|Medium|people|Indicates the contact's presence has changed. The application may decide to fetch the updated information.</p><p></p>|
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
            "rel" : "contactPresence",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282/contactPresence"
          },
          "in" : {
            "rel" : "contact",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282"
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




Returns a representation of a contact's presence.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282/contactPresence HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 223
{
  "rel" : "contactPresence",
  "activity" : "in-a-conference",
  "availability" : "DoNotDisturb",
  "deviceType" : "PC",
  "lastActive" : "\/Date(1474932023993)\/",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactPresence"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282/contactPresence HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 462
<?xml version="1.0" encoding="utf-8"?>
<resource rel="contactPresence" href="/ucwa/v1/applications/192/people/282/contactPresence" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">contactPresence</property>
  <property name="activity">in-a-conference</property>
  <property name="availability">IdleOnline</property>
  <property name="deviceType">Mobile</property>
  <property name="lastActive">2016-09-26T16:20:23.9946221-07:00</property>
</resource>
```



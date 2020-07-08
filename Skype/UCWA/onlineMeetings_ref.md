# onlineMeetings

 _**Applies to:** Skype for Business 2015_


Represents the dashboard for viewing and scheduling online meetings.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The onlineMeetings resource exposes the meetings and settings available to the user, including the ability to create a new [myOnlineMeeting](myOnlineMeeting_ref.md).

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|myAssignedOnlineMeeting|Represents a user's [onlineMeeting](onlineMeeting_ref.md) that is commonly used for scheduled meetings with other [contact](contact_ref.md)s.|
|myOnlineMeetings|Represents the set of [myOnlineMeeting](myOnlineMeeting_ref.md)s currently on the user's calendar.|
|onlineMeetingDefaultValues|Represents the values of [myOnlineMeeting](myOnlineMeeting_ref.md) properties if not specified at scheduling time.|
|onlineMeetingEligibleValues|Represents the eligible values that the application can choose from when scheduling a [myOnlineMeeting](myOnlineMeeting_ref.md).|
|onlineMeetingInvitationCustomization|Represents the recommended custom values to use when an [onlineMeetingInvitation](onlineMeetingInvitation_ref.md) is sent.|
|onlineMeetingPolicies|Represents the admin policies for the user's online meetings ([myOnlineMeetings](myOnlineMeetings_ref.md)).|
|phoneDialInInformation|Represents phone access information for an [onlineMeeting](onlineMeeting_ref.md).|

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




Returns a representation of the dashboard for viewing and scheduling online meetings.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 825
{
  "rel" : "onlineMeetings",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings"
    },
    "myAssignedOnlineMeeting" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/600"
    },
    "myOnlineMeetings" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings"
    },
    "onlineMeetingDefaultValues" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingDefaultValues"
    },
    "onlineMeetingEligibleValues" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingEligibleValues"
    },
    "onlineMeetingInvitationCustomization" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingInvitationCustomization"
    },
    "onlineMeetingPolicies" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingPolicies"
    },
    "phoneDialInInformation" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/phoneDialInInformation"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 1015
<?xml version="1.0" encoding="utf-8"?>
<resource rel="onlineMeetings" href="/ucwa/v1/applications/192/onlineMeetings" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="myAssignedOnlineMeeting" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/600" />
  <link rel="myOnlineMeetings" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings" />
  <link rel="onlineMeetingDefaultValues" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingDefaultValues" />
  <link rel="onlineMeetingEligibleValues" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingEligibleValues" />
  <link rel="onlineMeetingInvitationCustomization" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingInvitationCustomization" />
  <link rel="onlineMeetingPolicies" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingPolicies" />
  <link rel="phoneDialInInformation" href="/ucwa/v1/applications/192/onlineMeetings/phoneDialInInformation" />
  <property name="rel">onlineMeetings</property>
</resource>
```



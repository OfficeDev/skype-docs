# onlineMeetingPolicies

 _**Applies to:** Skype for Business 2015_


Represents the admin policies for the user's online meetings ([myOnlineMeetings](myOnlineMeetings_ref.md)).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

### Properties



|**Name**|**Description**|
|:-----|:-----|
|entryExitAnnouncement|The ability to enable or disable attendance announcements.|
|externalUserMeetingRecording|The ability of external users to record online meetings.|
|meetingRecording|The ability of internal users to record online meetings.|
|meetingSize|The maximum number of meeting participants that can be invited before a warning should be shown to the user.|
|phoneUserAdmission|The ability to access online meetings by phone.|
|voipAudio|The ability to schedule an online meeting using VoIP audio.|

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
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|
|Meetings.ReadWrite|Create Skype Meetings|Allows the app to create Skype meetings on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the admin policies for the user's online meetings.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/onlineMeetingPolicies HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 281
{
  "rel" : "onlineMeetingPolicies",
  "entryExitAnnouncement" : "None",
  "externalUserMeetingRecording" : "None",
  "meetingRecording" : "None",
  "meetingSize" : 5,
  "phoneUserAdmission" : "None",
  "voipAudio" : "None",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/onlineMeetingPolicies"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/onlineMeetingPolicies HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 562
<?xml version="1.0" encoding="utf-8"?>
<resource rel="onlineMeetingPolicies" href="/ucwa/v1/applications/192/onlineMeetings/onlineMeetingPolicies" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">onlineMeetingPolicies</property>
  <property name="entryExitAnnouncement">None</property>
  <property name="externalUserMeetingRecording">None</property>
  <property name="meetingRecording">None</property>
  <property name="meetingSize">5</property>
  <property name="phoneUserAdmission">None</property>
  <property name="voipAudio">None</property>
</resource>
```



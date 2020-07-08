# organizer

 _**Applies to:** Skype for Business 2015_


Represents the organizer of the online meeting.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This represents the [contact](contact_ref.md) who organized the corresponding online meeting.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the organizer of the online meeting.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/organizer HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: 36617189-8485-43af-b357-5cb43ef03556

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 10c8f983-18bf-44ab-80e6-c9e08f523298
Content-Type: application/json
Content-Length: 1106
{
  "rel" : "contact",
  "company" : "Contoso Corp.",
  "department" : "Engineering",
  "emailAddresses" : [
    "Alex.Doe@contoso.com"
  ],
  "homePhoneNumber" : "tel:+19185550107",
  "sourceNetworkIconUrl" : "https://images.contoso.com/logo_16x16.png",
  "mobilePhoneNumber" : "tel:4255551212;phone-context=defaultprofile",
  "name" : "Alex Doe",
  "office" : "tel:+1425554321;ext=54321",
  "otherPhoneNumber" : "tel:+19195558194",
  "sourceNetwork" : "SameEnterprise",
  "title" : "Engineer 2",
  "type" : "User",
  "uri" : "sip:alex@contoso.com",
  "workPhoneNumber" : "tel:+1425554321;ext=54321",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/people/282"
    },
    "contactLocation" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactLocation"
    },
    "contactNote" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactNote"
    },
    "contactPhoto" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactPhoto"
    },
    "contactPresence" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactPresence"
    },
    "contactPrivacyRelationship" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactPrivacyRelationship"
    },
    "contactSupportedModalities" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactSupportedModalities"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/onlineMeeting/organizer HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: e2009672-9436-4cbf-8f38-9ec21295fb98

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 32491406-26d5-4ca0-a0a9-52b9bdb7eb1c
Content-Type: application/xml
Content-Length: 1622
<?xml version="1.0" encoding="utf-8"?>
<resource rel="contact" href="/ucwa/v1/applications/192/people/282" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="contactLocation" href="/ucwa/v1/applications/192/people/282/contactLocation" />
  <link rel="contactNote" href="/ucwa/v1/applications/192/people/282/contactNote" />
  <link rel="contactPhoto" href="/ucwa/v1/applications/192/people/282/contactPhoto" />
  <link rel="contactPresence" href="/ucwa/v1/applications/192/people/282/contactPresence" />
  <link rel="contactPrivacyRelationship" href="/ucwa/v1/applications/192/people/282/contactPrivacyRelationship" />
  <link rel="contactSupportedModalities" href="/ucwa/v1/applications/192/people/282/contactSupportedModalities" />
  <property name="rel">contact</property>
  <property name="company">Contoso Corp.</property>
  <property name="department">Engineering</property>
  <propertyList name="emailAddresses">
    <item>Alex.Doe@contoso.com</item>
  </propertyList>
  <property name="homePhoneNumber">tel:+19185550107</property>
  <property name="sourceNetworkIconUrl">https://images.contoso.com/logo_16x16.png</property>
  <property name="mobilePhoneNumber">tel:4255551212;phone-context=defaultprofile</property>
  <property name="name">Alex Doe</property>
  <property name="office">tel:+1425554321;ext=54321</property>
  <property name="otherPhoneNumber">tel:+19195558194</property>
  <property name="sourceNetwork">SameEnterprise</property>
  <property name="title">Engineer 2</property>
  <property name="type">User</property>
  <property name="uri">sip:alex@contoso.com</property>
  <property name="workPhoneNumber">tel:+1425554321;ext=54321</property>
</resource>
```



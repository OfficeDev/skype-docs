# myPrivacyRelationships

 _**Applies to:** Skype for Business 2015_


Represents the various privacy relationships that the user maintains with his or her contacts.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|myPrivacyRelationship|Represents a set of contacts that have a given privacy relationship with the user.|

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




Returns a representation of the various privacy relationships that the user maintains with his or her contacts.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/myPrivacyRelationships HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 372
{
  "rel" : "myPrivacyRelationships",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/myPrivacyRelationships"
    }
  },
  "_embedded" : {
    "myPrivacyRelationship" : [
      {
        "rel" : "myPrivacyRelationship",
        "relationshipLevel" : "Unknown",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/192/myPrivacyRelationships/myPrivacyRelationship"
          },
          "contact" : [
            {
              "href" : "/ucwa/v1/applications/192/people/816"
            }
          ]
        }
      }
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/myPrivacyRelationships HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 552
<?xml version="1.0" encoding="utf-8"?>
<resource rel="myPrivacyRelationships" href="/ucwa/v1/applications/192/myPrivacyRelationships" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">myPrivacyRelationships</property>
  <resource rel="myPrivacyRelationship" href="/ucwa/v1/applications/192/myPrivacyRelationships/myPrivacyRelationship">
    <link rel="contact" href="/ucwa/v1/applications/192/people/464" />
    <property name="rel">myPrivacyRelationship</property>
    <property name="relationshipLevel">External</property>
  </resource>
</resource>
```



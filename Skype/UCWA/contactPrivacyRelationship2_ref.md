# contactPrivacyRelationship2

 _**Applies to:** Skype for Business 2015_


Represents the privacy relationship between the user and a [contact](contact_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource captures the closeness of the relationship, with more information available in closer relationships. FriendsAndFamily contactssee appointment and meeting titles, while Colleagues see only Free or Busy.If an application has subscribed to a contact, events will be raised when a contact's privacy relationship changes.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|relationshipLevel|The relationship level ([PrivacyRelationshipLevel](PrivacyRelationshipLevel_ref.md)) between the user and a contact, such as Colleagues or FriendsAndFamily.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|resetContactPrivacyRelationship|Resets a contact's privacy relationship|

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
|contactPrivacyRelationship|Medium|people|Indicates that the contact's privacy relationship has changed. The application may decide to fetch the updated information.</p><p></p>|

**Sample of returned event data**

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
      "rel" : "people",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people",
      "events" : [
        {
          "link" : {
            "rel" : "contactPrivacyRelationship",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282/contactPrivacyRelationship"
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
```

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of a privacy relationship between a contact and the logged-in user.

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282/contactPrivacyRelationship HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: 33934c8d-3574-4244-989d-a9109729b153

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.


```json
HTTP/1.1 200 OK
Etag: c2155e1c-512b-4024-a662-db3b02b81400
Content-Type: application/json
Content-Length: 303
{
  "rel" : "contactPrivacyRelationship",
  "relationshipLevel" : "Colleagues",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactPrivacyRelationship"
    },
    "resetContactPrivacyRelationship" : {
      "href" : "/ucwa/v1/applications/192/people/282/contactPrivacyRelationship/resetContactPrivacyRelationship"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282/contactPrivacyRelationship HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: f6eaac49-f760-4c51-8fa9-fb5ac9a7c4ff

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.

```xml

<?xml version="1.0" encoding="utf-8"?>
<resource rel="contactPrivacyRelationship" href="/ucwa/v1/applications/192/people/282/contactPrivacyRelationship" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="resetContactPrivacyRelationship" href="/ucwa/v1/applications/192/people/282/contactPrivacyRelationship/resetContactPrivacyRelationship" />
  <property name="rel">contactPrivacyRelationship</property>
  <property name="relationshipLevel">FriendsAndFamily</property>
</resource>
```



### PUT




Change the privacy relationship of a contact.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|relationshipLevel|The relationship level ([PrivacyRelationshipLevel](PrivacyRelationshipLevel_ref.md)) between the user and a contact, such as Colleagues or FriendsAndFamily.(PrivacyRelationshipLevel)Unknown, External, Colleagues, Workgroup, FriendsAndFamily, or Blocked|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Indicates that the user does not have privileges to update privacy relationship data for this contact.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282/contactPrivacyRelationship HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
if-match: 4c29488f-025d-439c-952d-79db8adec920
Content-Length: 68
{
  &quot;rel&quot; : &quot;contactPrivacyRelationship&quot;,
  &quot;relationshipLevel&quot; : &quot;Workgroup&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```


#### XML Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/people/282/contactPrivacyRelationship HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
if-match: 6934f43c-4b5f-439e-8b39-a9ac5bfe7d58
Content-Length: 224
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;resource xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;rel&quot;&gt;contactPrivacyRelationship&lt;/property&gt;
  &lt;property name=&quot;relationshipLevel&quot;&gt;Unknown&lt;/property&gt;
&lt;/resource&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```



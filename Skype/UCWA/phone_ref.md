# phone

 _**Applies to:** Skype for Business 2015_


Represents one of the user's phone numbers.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

These phone numbers can be used as targets for a user's incoming calls or made visible as part of the user's contact card.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|includeInContactCard|Indicates whether this phone number is shared in contact card.|
|number|The phone number.|
|type|The type of phone number: Work/Home/Other/Mobile.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|changeNumber|Changes or clears the number stored in the corresponding [phone](phone_ref.md) resource.|
|changeVisibility|Changes the visibility of a phone number to other contacts.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of one of the user's phone numbers.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Indicates that the user is not enabled for enterprise voice.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/phones/phone HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: a2b5cd48-3194-4c88-bccb-1e818facd0f8

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: b947d843-ba20-4005-8edb-899e4082213e
Content-Type: application/json
Content-Length: 339
{
  "rel" : "phone",
  "includeInContactCard" : false,
  "number" : " tel:+1425554321;ext=54321",
  "type" : " Other",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me/phones/phone"
    },
    "changeNumber" : {
      "href" : "/ucwa/v1/applications/192/me/phones/phone/changeNumber"
    },
    "changeVisibility" : {
      "href" : "/ucwa/v1/applications/192/me/phones/phone/changeVisibility"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/phones/phone HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: 7af1a48c-ebe6-4294-8e70-0e557174b625

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 84f1f208-a3c3-47c0-8682-c8d0ff0e9c20
Content-Type: application/xml
Content-Length: 550
<?xml version="1.0" encoding="utf-8"?>
<resource rel="phone" href="/ucwa/v1/applications/192/me/phones/phone" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="changeNumber" href="/ucwa/v1/applications/192/me/phones/phone/changeNumber" />
  <link rel="changeVisibility" href="/ucwa/v1/applications/192/me/phones/phone/changeVisibility" />
  <property name="rel">phone</property>
  <property name="includeInContactCard">False</property>
  <property name="number"> tel:+1425554321;ext=54321</property>
  <property name="type"> Home</property>
</resource>
```



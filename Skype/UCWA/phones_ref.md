# phones

 _**Applies to:** Skype for Business 2015_


A collection of phone resources that represent the phone numbers of the logged-on user.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This collection is read/write; it can be used to both create new phone resources, as well as to view and remove existing phone resources.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|phone|Represents one of the user's phone numbers.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the collection of phone resources that represent the phone numbers of the logged-on user.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|AnonymousNotAllowed|The user cannot access delegates as he or she has signed in as anonymous.|
|Forbidden|403|None|Forbidden exception which occurs when the logged in user is not enabled for enterprise voice.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/phones HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 434
{
  "rel" : "phones",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me/phones"
    }
  },
  "_embedded" : {
    "phone" : [
      {
        "rel" : "phone",
        "includeInContactCard" : false,
        "number" : "tel:+14255551234",
        "type" : " Home",
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
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/phones HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 656
<?xml version="1.0" encoding="utf-8"?>
<resource rel="phones" href="/ucwa/v1/applications/192/me/phones" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">phones</property>
  <resource rel="phone" href="/ucwa/v1/applications/192/me/phones/phone">
    <link rel="changeNumber" href="/ucwa/v1/applications/192/me/phones/phone/changeNumber" />
    <link rel="changeVisibility" href="/ucwa/v1/applications/192/me/phones/phone/changeVisibility" />
    <property name="rel">phone</property>
    <property name="includeInContactCard">False</property>
    <property name="number">tel:+14255551234</property>
    <property name="type"> Other</property>
  </resource>
</resource>
```



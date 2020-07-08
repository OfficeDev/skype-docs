# root

 _**Applies to:** Skype for Business 2015_


Represents the entry point to the API.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The root resource lets the application choose the kind of credentials it is going to present. Currently, only [user](user_ref.md) credentials are supported.The root resource also informs the application about the location of the cross-domain frame ([xframe](xframe_ref.md)) needed for web programming.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|redirect|Represents a pointer to a different server that the application should use for future requests.|
|user|Represents the entry point to the API using user credentials.|
|xframe|Represents the cross-domain frame used for web-based applications.|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the entry point to the API.

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
Get https://fe1.contoso.com:443/autodiscover/autodiscoverservice.svc/root HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 301
{
  "rel" : "root",
  "_links" : {
    "self" : {
      "href" : "/autodiscover/autodiscoverservice.svc/root"
    },
    "redirect" : {
      "href" : "/autodiscover/autodiscoverservice.svc/root/user/redirect"
    },
    "user" : {
      "href" : "/autodiscover/autodiscoverservice.svc/root/user"
    },
    "xframe" : {
      "href" : "/autodiscover/autodiscoverservice.svc/root/xframe"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/autodiscover/autodiscoverservice.svc/root HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 449
<?xml version="1.0" encoding="utf-8"?>
<resource rel="root" href="/autodiscover/autodiscoverservice.svc/root" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="redirect" href="/autodiscover/autodiscoverservice.svc/root/user/redirect" />
  <link rel="user" href="/autodiscover/autodiscoverservice.svc/root/user" />
  <link rel="xframe" href="/autodiscover/autodiscoverservice.svc/root/xframe" />
  <property name="rel">root</property>
</resource>
```



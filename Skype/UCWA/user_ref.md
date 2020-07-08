# user

 _**Applies to:** Skype for Business 2015_


Represents the entry point to the API using user credentials.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

The user resource indicates that the application plans to use the API on behalf of a user.If an application attempts to use this resource without credentials, the server will respond with a 401 Not Authorized and authentication hints in the WWW-Authenticate Header.After credentials are acquired, this resource will point the application to the [applications](applications_ref.md) resource.In some cases, after credentials are acquired, the server might serve a [redirect](redirect_ref.md) resource indicating that the user is homed on another server.The application should follow this resource to the new server. Credentials might need to be resubmitted.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|applications|Represents the entry point for registering this application with the server.|
|redirect|Represents a pointer to a different server that the application should use for future requests.|
|xframe|Represents the cross-domain frame used for web-based applications.|

## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the entry point to the API using user credentials.

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
Get https://fe1.contoso.com:443/autodiscover/autodiscoverservice.svc/root/user HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 288
{
  "rel" : "user",
  "_links" : {
    "self" : {
      "href" : "/autodiscover/autodiscoverservice.svc/root/user"
    },
    "applications" : {
      "href" : "/ucwa/v1/applications"
    },
    "redirect" : {
      "href" : "/autodiscover/autodiscoverservice.svc/root/user/redirect"
    },
    "xframe" : {
      "href" : "/autodiscover/autodiscoverservice.svc/root/xframe"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/autodiscover/autodiscoverservice.svc/root/user HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 436
<?xml version="1.0" encoding="utf-8"?>
<resource rel="user" href="/autodiscover/autodiscoverservice.svc/root/user" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="applications" href="/ucwa/v1/applications" />
  <link rel="redirect" href="/autodiscover/autodiscoverservice.svc/root/user/redirect" />
  <link rel="xframe" href="/autodiscover/autodiscoverservice.svc/root/xframe" />
  <property name="rel">user</property>
</resource>
```



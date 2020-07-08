
# Errors and informational messages

**Applies to**: Skype for Business 2015

Learn about the types of errors and informational messages that are used in UCWA 2.0.

To report failures, UCWA 2.0 uses the failure response classes (4xx &amp; 5xx) that are defined in Section 10 (Status Code Definitions) of the [HTTP RFC](http://tools.ietf.org/html/rfc2616). An HTTP request that is targeted to UCWA 2.0 can have multiple intermediate proxies on the path, meaning an intermediate component can return a failure response of 4xx or 5xx as well. 

If UCWA 2.0 responds with a failure, it will provide supplementary information in the error response that can be used to aid in problem diagnosis or drive the user interface. Clients can determine whether the response comes from UCWA 2.0 by analyzing the Content-Type header. In this release, the supported content types are:

- application/json
 
- application/xml
 

## Error types

UCWA 2.0 categorizes errors as either synchronous or asynchronous based on their presence in the command or event channels, respectively.


### Synchronous errors

Synchronous errors are returned only in the command channel in response to GET, POST, PUT, or DELETE requests. These errors map one-to-one to the familiar HTTP response codes.

The errors listed in the following table apply to most resources.

|Error|Description|
|:-----|:-----|
|ApplicationNotFound|The application resource does not exist.|
|BadRequest|Something is wrong with the entire request (malformed XML/JSON, for example).|
|DeserializationFailure|The request body could not be deserialized. Please check if the body confirms to allowed formats and does not have any invalid characters.|
|EntityTooLarge|The request is too large.|
|InactiveApplicationExpired|An inactive application expired.|
|MethodNotAllowed|The requested HTTP method is not supported for this resource.|
|MobileApplicationNoLongerAllowed|User is no longer authorized for mobile applications.|
|ParameterValidationFailure|A parameter value is not valid.|
|PreconditionFailed|An If-Match precondition was not met.|
|PreconditionRequired|The operation requires an If-Match precondition.|
|ResourceNotFound|The resource does not exist.|
|ServiceFailure|Internal Server Error.|
|ServiceTimeout|Internal Server Error, remote timeout.|
|TooManyApplications|There are too many applications for this user.|
|UnsupportedMediaType|The content-type is not supported.|
|VersionNotSupported|Requested Version is not supported.|

### Asynchronous errors

Asynchronous errors are returned only in the event channel, and are used to indicate failure reasons. These errors are associated exclusively with operation resources such as the [onlineMeetingInvitation](onlineMeetingInvitation_ref.md) resource.


### Asynchronous informational messages

Asynchronous informational messages are also returned only in the event channel, but are used to provide additional information on why a dynamic resource was updated (that is, the reason for a state transition).


## Error response body

The following table lists the properties in an error response body.

|Property name|Type|Required in error response?|Description|
|:-----|:-----|:-----|:-----|
|code|String|Yes|For synchronous failures, this maps one-to-one with HTTP responses. For asynchronous failures or messages, it is contextual.|
|subcode|String|No|The subcode further classifies a failure. For example, if the response code is 409 Conflict, the subcode could be 'AlreadyExists'.|
|message|String|No|The message provides a reason, localized to the user's locale that was specified when application was created.|
|link|href|No|The link potentially can provide further information about the error and how to resolve the problem.|
|parameters|Property bag|No|Parameters apply only for response codes of 400 Bad Request. In this case a property bag will list all of the invalid properties in the request that the response refers to.|
|debugInfo|Property bag|No|A property bag that provides supplementary information that can be used for troubleshooting.|

## Client/server programming contract

- The only property that is required in the error body is "code". All other properties are optional.
 
- New subcodes can be introduced at any time. Clients should be designed that that they ignore missing or unknown subcodes.
 
- Both Code and Subcode should be consumed contextually. For example, if the [messaging](messaging_ref.md) modality sends an **updated** event, with a subcode of "Removed", this means that the **messaging** modality was removed. There are no modality-specific subcodes.
 
Anything that resides under `<debugInfo>` is subject to change, so a client must not make assumptions about the contents of this property. However, clients are recommended to log this information along with the failed request. These items can provide substantial help when troubleshooting the problem.


## Error sample - JSON

The following shows the form of a 400 BadRequest error body in JSON format.

```json
{
 "code": "Conflict",
 "subcode": "AlreadyExists",
 "message": "The requested resource already exists. Please wait and try again."
}

```


## Error sample - XML

The following shows the general form of a 409 Conflict error body in XML format.


```xml
<?xml version="1.0" encoding="utf-8"?>
<reason xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
 <code>Conflict</code>
 <subcode>AlreadyExists</subcode>
 <message>The requested resource already exists. Please wait and try again.</message>
 <debugInfo />
 <parameters />
</reason>

```


## HTTP error codes returned by UCWA

|HTTP status code|Error|Can appear in event channel?|
|:-----|:-----|:-----|
|400|BadRequest|Yes|
|403|Forbidden|Yes|
|404|NotFound|Yes|
|405|MethodNotAllowed|No|
|408|ClientTimeout|No|
|409|Conflict|Yes|
|410|Gone|No|
|412|PreConditionFailed|No|
|413|EntityTooLarge|Yes|
|415|UnsupportedMediaType|No|
|428|PreConditionRequired|No|
|429|TooManyRequests|No|
|500|ServiceFailure|Yes|
|503|ServiceUnavailable|No|
|504|Timeout|Yes|

## See also

- [Key programming concepts](keyprogrammingconcepts.md)
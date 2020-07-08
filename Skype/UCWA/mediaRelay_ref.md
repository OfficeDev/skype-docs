# mediaRelay

 _**Applies to:** Skype for Business 2015_


MediaRelay class contains the information about media relay and ports.
            

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
|host|MediaRelay host name.|
|ipv4|Gets ipv6 for the media relay(only applicapleto ietf media relay)|
|ipv6|Gets ipv6 for the media relay(only applicaple to ietf media relay)|
|location|Gets the location.|
|tcpPort|Gets TcpPort for relay, returns -1, if tcp port not set|
|udpPort|Gets UdpPort for relay, returns -1, if udp port not set|

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




Operation description coming soon...

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
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/mediaRelay HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 211
{
  "rel" : "mediaRelay",
  "host" : "samplevalue",
  "ipv4" : "samplevalue",
  "ipv6" : "samplevalue",
  "location" : "Intranet",
  "tcpPort" : 29,
  "udpPort" : 10,
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/communication/mediaRelay"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/mediaRelay HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 483
<?xml version="1.0" encoding="utf-8"?>
<resource rel="mediaRelay" href="/ucwa/v1/applications/192/communication/mediaRelay" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">mediaRelay</property>
  <property name="host">samplevalue</property>
  <property name="ipv4">samplevalue</property>
  <property name="ipv6">samplevalue</property>
  <property name="location">Intranet</property>
  <property name="tcpPort">72</property>
  <property name="udpPort">17</property>
</resource>
```



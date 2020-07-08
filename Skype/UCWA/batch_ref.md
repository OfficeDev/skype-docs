# batch

 _**Applies to:** Skype for Business 2015_


Initiates an operation that groups multiple, independent HTTP operations into a single HTTP request payload.
             

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

Batch requests are submitted as a single HTTP POST request to the batch resource.The batch request must contain a Content-Type header specifying a content type of "multipart/batching" and a boundary specification.The body of a batch request is made up of an ordered series of HTTP operations.In the batch request body, each HTTP operation is represented as a distinct MIME part and is separated by the boundary marker defined in the Content-Type header of the request. Each MIME part representing an HTTP operation within the Batch includes both Content-Type and Content-Transfer-Encoding MIME headers.The batch request boundary is the name specified in the Content-Type Header for the batch.

### Properties



None

### Links



None

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

### POST




Submit a group of multiple, independent HTTP operations as a single HTTP request payload.

#### Request body



None


#### Response body



|**Item**|**Description**|
|:-----|:-----|
|batch|Initiates an operation that groups multiple, independent HTTP operations into a single HTTP request payload.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|TooManyRequests|429|None|Indicates that there are too many outstanding HTTP operations.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/batch HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: multipart/batching;boundary="EA370946"
Accept: multipart/batching
Content-Length: 557
--EA370946<BR>Content-Type : application/http;msgtype=request<BR>Content-Length : 200<BR><BR>GET/ucwa/v1/applications/11540713861/policiesHTTP/1.1<BR>Accept : application/vnd.microsoft.com.ucwa+json<BR>Content-Type : application/vnd.microsoft.com.ucwa+json<BR>Host : fe1.contoso.com<BR><BR><BR>--EA370946<BR>Content-Type : application/http;msgtype=request<BR>Content-Length : 201<BR><BR>GET/ucwa/v1/applications/11540713861/me/phonesHTTP/1.1<BR>Accept : application/vnd.microsoft.com.ucwa+json<BR>Content-Type : application/vnd.microsoft.com.ucwa+json<BR>Host : fe1.contoso.com<BR><BR><BR>--EA370946--
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: multipart/batching; boundary="0cac9abd"
Content-Length: 2027
--0cac9abd<BR>Content-Type : application/http;msgtype=response<BR><BR>HTTP/1.1200OK<BR>Cache-Control : no-cache<BR>Content-Type : application/vnd.microsoft.com.ucwa+json;charset=utf-8<BR><BR>{
  "telephonyMode" : "Uc",
  "exchangeUnifiedMessaging" : "Enabled",
  "logging" : "Enabled",
  "loggingLevel" : "Full",
  "photos" : "Enabled",
  "voicemailUri" : "sip:jdoe@contoso.com;opaque=app:voicemail",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/11540713861/policies"
    }
  },
  "rel" : "policies"
}<BR>--0cac9abd<BR>Content-Type : application/http;msgtype=response<BR><BR>HTTP/1.1200OK<BR>Cache-Control : no-cache<BR>Content-Type : application/vnd.microsoft.com.ucwa+json;charset=utf-8<BR><BR>{
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/11540713861/me/phones"
    }
  },
  "_embedded" : {
    "phone" : [
      {
        "number" : "tel:+14255554321;ext=54321",
        "type" : "work",
        "includeInContactCard" : true,
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/work"
          }
        },
        "rel" : "phone",
        "etag" : "2758999089"
      },
      {
        "number" : "4255551212",
        "type" : "mobile",
        "includeInContactCard" : true,
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/mobile"
          },
          "changeNumber" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/mobile/changeNumber"
          },
          "changeVisibility" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/mobile/changeVisibility"
          }
        },
        "rel" : "phone",
        "etag" : "2878440199"
      },
      {
        "number" : "",
        "includeInContactCard" : false,
        "type" : "home",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/home"
          },
          "changeNumber" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/home/changeNumber"
          },
          "changeVisibility" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/home/changeVisibility"
          }
        },
        "rel" : "phone",
        "etag" : "2976547271"
      },
      {
        "number" : "",
        "includeInContactCard" : false,
        "type" : "other",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/other"
          },
          "changeNumber" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/other/changeNumber"
          },
          "changeVisibility" : {
            "href" : "/ucwa/v1/applications/11540713861/me/phones/other/changeVisibility"
          }
        },
        "rel" : "phone",
        "etag" : "1067208367"
      }
    ]
  },
  "rel" : "phones"
}<BR>--0cac9abd--
```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/batch HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: multipart/batching;boundary="EA370946"
Accept: multipart/batching
Content-Length: 553
--EA370946
Content-Type: application/http; msgtype=request
Content-Length: 200

GET /ucwa/v1/applications/11540713861/policies HTTP/1.1
Accept: application/vnd.microsoft.com.ucwa+xml
Content-Type: application/vnd.microsoft.com.ucwa+xml
Host: fe1.contoso.com


--EA370946
Content-Type: application/http; msgtype=request
Content-Length: 201

GET /ucwa/v1/applications/11540713861/me/phones HTTP/1.1
Accept: application/vnd.microsoft.com.ucwa+xml
Content-Type: application/vnd.microsoft.com.ucwa+xml
Host: fe1.contoso.com


--EA370946--
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: multipart/batching; boundary="0cac9abd"
Content-Length: 2690


--0cac9abd
Content-Type: application/http; msgtype=response

HTTP/1.1 200 OK
Cache-Control: no-cache
Content-Type: application/vnd.microsoft.com.ucwa+xml; charset=utf-8

<?xml version="1.0" encoding="utf-8"?><resource rel="policies" href="/ucwa/v1/applications/21219618648" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa"><property name="telephonyMode">Uc</property><property name="exchangeUnifiedMessaging">Enabled</property><property name="logging">Enabled</property><property name="loggingLevel">Full</property><property name="photos">Enabled</property><property name="voicemailUri">sip:jdoe@contoso.com;opaque=app:voicemail</property></resource>
--0cac9abd
Content-Type: application/http; msgtype=response

HTTP/1.1 200 OK
Cache-Control: no-cache
Content-Type: application/vnd.microsoft.com.ucwa+xml; charset=utf-8

<?xml version="1.0" encoding="utf-8"?><resource rel="phones" href="/ucwa/v1/applications/11540713861/me/phones" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa"><resource rel="phone" href="/ucwa/v1/applications/11540713861/me/phones/work"><property name="number">tel:+14255554321;ext=54321</property><property name="type">work</property><property name="includeInContactCard">true</property><property name="etag">2758999089</property></resource><resource rel="phone" href="/ucwa/v1/applications/11540713861/me/phones/mobile"><link rel="changeNumber" href="/ucwa/v1/applications/11540713861/me/phones/mobile/changeNumber"/><link rel="changeVisibility" href="/ucwa/v1/applications/11540713861/me/phones/mobile/changeVisibility"/><property name="number">4255551212</property><property name="type">mobile</property><property name="includeInContactCard">true</property><property name="etag">2878440199</property></resource><resource rel="phone" href="/ucwa/v1/applications/11540713861/me/phones/home"><link rel="changeNumber" href="/ucwa/v1/applications/11540713861/me/phones/home/changeNumber"/><link rel="changeVisibility" href="/ucwa/v1/applications/11540713861/me/phones/home/changeVisibility"/><property name="number"/><property name="includeInContactCard">false</property><property name="type">home</property><property name="etag">2976547271</property></resource><resource rel="phone" href="/ucwa/v1/applications/11540713861/me/phones/other"><link rel="changeNumber" href="/ucwa/v1/applications/11540713861/me/phones/other/changeNumber"/><link rel="changeVisibility" href="/ucwa/v1/applications/11540713861/me/phones/other/changeVisibility"/><property name="number"/><property name="includeInContactCard">false</property><property name="type">other</property><property name="etag">1067208367</property></resource></resource>
--0cac9abd--

```



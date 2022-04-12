---
title: Batching requests for skype for business 2015
description: "batch resource can be used to package multiple HTTP requests into a single request."
---
# Batching requests
Learn how the **batch** resource can be used to package multiple HTTP requests into a single request.


 _**Applies to:** Skype for Business 2015_

UCWA 2.0 provides a facility to submit multiple requests in a single HTTP operation, called a batch request. A client application can use it to reduce the number of separate HTTP requests it sends (which will help reduce battery drain on mobile devices), and to reduce the number of separate connections to the server (which is generally limited by web browsers).

A batch request is a multipart MIME message, where each part is a separate HTTP request. The batch request is submitted with a POST operation to the [batch](batch_ref.md) resource, and the response to the POST request will be a multipart MIME message where each part is the response to the corresponding request.
The requests that are present in a batch request can be processed in any order, so applications should not presume a particular order of execution of the requests in a batch.
The content type for the entire request is "multipart/batching", and each individual request is an "application/http; msgtype=request" message containing the request. The response content type is "multipart/batching", and each part contains a response formatted as "application/http; msgtype=response". 

## Batch examples

The following is an example batch request. This request contains two GET requests; one on the [myContacts](myContacts_ref.md) href for a particular user, and the other on the [me](me_ref.md) href for that user.

Note that for brevity, application IDs and the OAuth token have been abbreviated.




```
POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/103/batch HTTP/1.1
Accept: multipart/batching
Content-Type: multipart/batching;boundary=77f2569d-c005-442b-b856-782305305e5f
Authorization: Bearer cwt=AAEB...buHc
X-Ms-Origin: http://localhost
X-Requested-With: XMLHttpRequest
Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFra/documentation/Resources-me
Accept-Language: en-us
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)
Host: lyncweb.contoso.com
Content-Length: 463
Connection: Keep-Alive
Cache-Control: no-cache

--77f2569d-c005-442b-b856-782305305e5f
Content-Type: application/http; msgtype=request

GET /ucwa/oauth/v1/applications/103/people/contacts HTTP/1.1
Host: lyncweb.contoso.com
Accept: application/json

--77f2569d-c005-442b-b856-782305305e5f
Content-Type: application/http; msgtype=request

GET /ucwa/oauth/v1/applications/103/me HTTP/1.1
Host: lyncweb.contoso.com
Accept: application/json

--77f2569d-c005-442b-b856-782305305e5f--

```

The following is an example batch response. 

The first response is the response from the GET on the **myContacts** resource. The response shows contact information for two contacts - Chris Barry and Alex Darrow. The second response is the response from the GET request on the **me** resource, and contains contact information for the "me" user, Dana Birkby.

As in the preceding example, some information has be omitted for brevity.




```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 3064
Expires: -1
Date: Wed, 30 Jan 2013 22:43:22 GMT
Content-Type: multipart/batching; boundary="3968fb75-0b0e-4e11-8b0c-4e37d822ed0c"
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
Pragma: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

--3968fb75-0b0e-4e11-8b0c-4e37d822ed0c
Content-Type: application/http; msgtype=response

HTTP/1.1 200 OK
Cache-Control: no-cache
Content-Type: application/json; charset=utf-8

?
{
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/103/people/contacts"}
 },
 "_embedded":{
 "contact": [{
 "uri":"sip:ChrisB@contoso.com",
 "sourceNetwork":"SameEnterprise",
 "emailAddresses": ["ChrisB@contoso.com"],
 "type":"User",
 "name":"Chris Barry",
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/103/people/chrisb@contoso.com"},
 "contactPhoto":{"href":"/ucwa/oauth/v1/applications/103/photos/ChrisB@contoso.com"},
 "contactPresence":{"href":"/ucwa/oauth/v1/applications/103/people/chrisb@contoso.com/presence"},
 "contactLocation":{"href":"/ucwa/oauth/v1/applications/103/people/chrisb@contoso.com/location"},
 "contactNote":{"href":"/ucwa/oauth/v1/applications/103/people/chrisb@contoso.com/note"},
 "contactSupportedModalities":{"href":"/ucwa/oauth/v1/applications/103/people/chrisb@contoso.com/supportedMedia"},
 "contactPrivacyRelationship":{"href":"/ucwa/oauth/v1/applications/103/people/chrisb@contoso.com/privacyRelationship"}
 },
 "rel":"contact",
 "etag":"4062011640"
 },{
 "uri":"sip:AlexD@contoso.com",
 "sourceNetwork":"SameEnterprise",
 "emailAddresses": ["AlexD@contoso.com"],
 "type":"User","name":"Alex Darrow",
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/103/people/alexd@contoso.com"},
 "contactPhoto":{ ... },
 "contactPresence":{ ... }, 
 "contactLocation":{ ... },
 "contactNote":{ ... }, 
 "contactSupportedModalities":{ ... },
 "contactPrivacyRelationship":{ ... }
 },
 "rel":"contact",
 "etag":"2719065758"
 }]
 },
 "rel":"myContacts"
}
--3968fb75-0b0e-4e11-8b0c-4e37d822ed0c
Content-Type: application/http; msgtype=response

HTTP/1.1 200 OK
Cache-Control: no-cache
Content-Type: application/json; charset=utf-8

?
{
 "name":"Dana Birkby",
 "uri":"sip:DanaB@contoso.com",
 "emailAddresses": ["DanaB@contoso.com"],
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/103/me"},
 "note":{"href":"/ucwa/oauth/v1/applications/103/me/note"},
 "presence":{"href":"/ucwa/oauth/v1/applications/103/me/presence"},
 "location":{"href":"/ucwa/oauth/v1/applications/103/me/location"},
 "reportMyActivity":{"href":"/ucwa/oauth/v1/applications/103/me/reportMyActivity"},
 "callForwardingSettings":{"href":"/ucwa/oauth/v1/applications/103/me/callForwardingSettings"},
 "phones":{"href":"/ucwa/oauth/v1/applications/103/me/phones"},
 "photo":{"href":"/ucwa/oauth/v1/applications/103/photos/DanaB@contoso.com"}
 },
 "rel":"me"
}
--3968fb75-0b0e-4e11-8b0c-4e37d822ed0c--

```


## Limitations

Batch requests can contain any UCWA 2.0 request, other than another batch request, [photo](photo_ref.md) request or [events](events_ref.md) request.

UCWA 2.0 limits the number of outstanding requests for each user, and if a batch request causes this limit to be exceeded, the request will be rejected with error code 429 / Too Many Requests. In this case none of the requests in the batch is executed, so it is safe for a client application to retry the request later, preferably splitting it into multiple batch requests of smaller sizes.

The server-side limit in this version is set at 100 requests for a single user across all batches; a single batch with more than 100 requests can also exceed this limit. It is recommended that clients do not take a dependency on this specific number, as it can be changed in later releases. Instead, clients should split large batch requests into smaller ones if they encounter this error message, and introduce a timeout between retries.


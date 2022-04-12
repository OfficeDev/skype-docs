---
title: Search for a user's contact for Skype for Business 2015
description: Search the me user's contact list for a specific contact.
---

# Search for a user's contact
Search the **me** user's contact list for a specific contact.


 _**Applies to:** Skype for Business 2015_

Searching for a user's contact requires accessing the search resource. It involves resource navigation from [application](application_ref.md) to [people](people_ref.md) to [search](search_ref.md), specifying in the query parameter the email address of the specified contact and the maximum number of values to return. The programming flow is illustrated in the following steps.

The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Send a GET request on the **search** resource following the links from **people** resource embedded in the **application** resource.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/people/search?query=sarad@contoso.com&amp;limit=3 HTTP/1.1
Authorization: Bearer cwt=AAEB...buHcmvDs1Z7CzwgNEPoG3XyftjBYhE5zTT0buHc
Accept: application/json
X-Ms-Origin: http://localhost
X-Requested-With: XMLHttpRequest
Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
Accept-Language: en-us
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)
Host: lyncweb.contoso.com
Connection: Keep-Alive

```

2. Process the response from the previous GET request to obtain the contact info.
 
 The response you receive should be 200 OK. The body of the response contains an array property named "contact" that is an array of individual contacts. Each item in the array includes information about a contact's URI, source network, and screen name, as well as links to other information about the contact.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 3691
Date: Thu, 17 Jan 2013 00:04:19 GMT
Content-Type: application/json; charset=utf-8
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"moreResultsAvailable":false,
"_links":{"self":{"href":"/ucwa/oauth/v1/applications/101/people/search?query=sarad@contoso.com\u0026limit=3"}},
"_embedded":{
"contact": [
{
"uri":"sip:SaraD@contoso.com","sourceNetwork":"SameEnterprise",
"company":"Metio","department":"Sales \u0026 Marketing","office":"20/2107",
"title":"Product Manager","emailAddresses": ["SaraD@contoso.com"],
"workPhoneNumber":"tel:+19185550107","type":"User",
"name":"Sara Davis",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com"},
"contactPhoto":{"href":"/ucwa/oauth/v1/applications/101/photos/SaraD@contoso.com"},
"contactPresence":{"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com/presence"},
"contactLocation":{"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com/location"},
"contactNote":{"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com/note"},
"contactSupportedModalities":{"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com/supportedMedia"},
"contactPrivacyRelationship":{"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com/privacyRelationship"}
},
"rel":"contact",
"etag":"58253032"
}
]
},
"rel":"search"
}
```


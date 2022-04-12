---
title: Get a contact's data for Skype for Business 2015
description: Get the information for a specific contact of the me user.
---

# Get a contact's data
Get the information for a specific contact of the **me** user.


 _**Applies to:** Skype for Business 2015_

Getting a specific contact's information amounts to accessing the [contact](contact_ref.md) resource. It involves resource navigation from [application](application_ref.md) to [people](people_ref.md) to [myContacts](myContacts_ref.md) and to the specified contact. Alternatively, navigation can proceed from [application](application_ref.md) to [people](people_ref.md) to [search](search_ref.md), with the contact's name, email address, and other information, and to the specified contact. The programming flow using the **search** resource is discussed in the [Search for a user's contact](SearchForUsersContact.md) topic.

The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Send a GET request on the **myContacts** HREF.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/people/contacts HTTP/1.1
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

2. Process the response from the previous GET request.
 
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
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/contacts"}
},
"_embedded":{
"contact": [{
"uri":"sip:ManishC@contoso.com",
"sourceNetwork":"SameEnterprise",
"emailAddresses": ["ManishC@contoso.com"],
"type":"User",
"name":"Manish Chopra",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/manishc@contoso.com"},
"contactPhoto":{"href":"/ucwa/oauth/v1/applications/101/photos/ManishC@contoso.com"},
"contactPresence":{"href":"/ucwa/oauth/v1/applications/101/people/manishc@contoso.com/presence"},
"contactLocation":{"href":"/ucwa/oauth/v1/applications/101/people/manishc@contoso.com/location"},
"contactNote":{"href":"/ucwa/oauth/v1/applications/101/people/manishc@contoso.com/note"},
"contactSupportedModalities":{"href":"/ucwa/oauth/v1/applications/101/people/manishc@contoso.com/supportedMedia"},
"contactPrivacyRelationship":{"href":"/ucwa/oauth/v1/applications/101/people/manishc@contoso.com/privacyRelationship"}
},
"rel":"contact",
"etag":"1486203547"
},
{
"uri":"sip:lenea@contoso.com",
"sourceNetwork":"SameEnterprise",
"type":"User",
"name":"Lene Aaling",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/lenea@contoso.com"},
"contactPhoto":{"href":"/ucwa/oauth/v1/applications/101/photos/lenea@contoso.com"},
"contactPresence":{"href":"/ucwa/oauth/v1/applications/101/people/lenea@contoso.com/presence"},
"contactLocation":{"href":"/ucwa/oauth/v1/applications/101/people/lenea@contoso.com/location"},
"contactNote":{"href":"/ucwa/oauth/v1/applications/101/people/lenea@contoso.com/note"},
"contactSupportedModalities":{"href":"/ucwa/oauth/v1/applications/101/people/lenea@contoso.com/supportedMedia"},
"contactPrivacyRelationship":{"href":"/ucwa/oauth/v1/applications/101/people/lenea@contoso.com/privacyRelationship"}
},
"rel":"contact",
"etag":"2316395199"},]
},
"rel":"myContacts"
}

```

3. Send a GET request on a specified **contact** resource parsed from the previous response.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/people/lenea@contoso.com@contoso.com HTTP/1.1
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

4. Process the response from the previous GET request.
 
  The successful response is 200 OK. The body of the response contains references to supported contact data.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 852
Date: Thu, 17 Jan 2013 00:04:19 GMT
Content-Type: application/json; charset=utf-8
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"uri":"sip:lenea@contoso.com@contoso.com",
"sourceNetwork":"SameEnterprise",
"type":"User",
"name":"Lene Aaling",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101551302053/people/lenea@contoso.com"},
"contactPhoto":{"href":"/ucwa/oauth/v1/applications/101551302053/photos/lenea@contoso.com"},
"contactPresence":{"href":"/ucwa/oauth/v1/applications/101551302053/people/lenea@contoso.com/presence"},
"contactLocation":{"href":"/ucwa/oauth/v1/applications/101551302053/people/lenea@contoso.com@/location"},
"contactNote":{"href":"/ucwa/oauth/v1/applications/101551302053/people/lenea@contoso.com@/note"},
"contactSupportedModalities":{"href":"/ucwa/oauth/v1/applications/101551302053/people/lenea@contoso.com@contoso.com/supportedMedia"},
"contactPrivacyRelationship":{"href":"/ucwa/oauth/v1/applications/101551302053/people/lenea@contoso.com@contoso.com/privacyRelationship"}
},
"rel":"contact",
"etag":"4061867540"
}

```


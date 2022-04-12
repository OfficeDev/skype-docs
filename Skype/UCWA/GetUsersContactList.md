---
title: Get the user's contact list for Skype for Business 2015
description: Get the list of all contacts for the me user.
---

# Get the user's contact list
Get the list of all contacts for the **me** user.


 _**Applies to:** Skype for Business 2015_

Getting a user's contacts involves resource navigation from (visible) [application](application_ref.md) to [people](people_ref.md) to [myContacts](myContacts_ref.md). 

The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Send a GET request on the **myContacts** HREF. One of the embedded resources that are served in the response for the [application](application_ref.md) resource is the [people](people_ref.md) resource. Search for the **people** embedded resource, and then locate the HREF of the [myContacts](myContacts_ref.md) link.
 
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
 
 The response you receive should be 200 OK. The body of the request contains an entry named "contact" that is an array of individual contacts. Each item in the array includes information about a contact's URI, source network, and screen name, as well as links to other information about the contact.
 
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


 If a specific contact is returned in an embedded resource, the result can be parsed to return the contact. Otherwise, a link to the specified contact resource can be followed to get the contact.
 

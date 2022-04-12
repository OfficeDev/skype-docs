---
title: Get presence data of a contact for Skype for Business 2015
description: Get the presence of a specific contact of the me user.
---

# Get presence data of a contact
Get the presence of a specific contact of the **me** user.


 _**Applies to:** Skype for Business 2015_

Getting contact presence involves resource navigation from [contact](contact_ref.md) to [contactPresence](contactPresence_ref.md). Note that this is very similar to fetching the user's presence. 

A contact can be found via one of several resources, including: **myContacts**, **myGroups**, and **search**. The example shown here demonstrates how to get the presence of a contact in one of the user's groups, by querying the group.
The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Parse the response from a GET request on the myGroups resource to obtain the specified group resource. 
 
 The myGroups resource is embedded in the people resource.
 
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
"self":{"href":"/ucwa/oauth/v1/applications/101/people/groups"}},
"_embedded":{
"pinnedGroup":{
"id":"group_id",
"name":"Pinned Contacts",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/groups/group_id"},
"groupContacts":{"href":"/ucwa/oauth/v1/applications/101/people/contacts?groupId=group_id"},
"subscribeToGroupPresence":{
"href":"/ucwa/oauth/v1/applications/101/people/presenceSubscriptions?groupId=group_id"}
},
"rel":"pinnedGroup"
},
"defaultGroup":{
"id":"default_group_id",
"name":"Other Contacts",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/groups/default_group_id"},
"groupContacts":{"href":"/ucwa/oauth/v1/applications/101/people/contacts?groupId=default_group_id"},
"subscribeToGroupPresence":{
"href":"/ucwa/oauth/v1/applications/101/people/presenceSubscriptions?groupId=default_group_id"}
},
"rel":"defaultGroup"
},
"group": [],
"distributionGroup": []
},
"rel":"myGroups"
}

```

2. Send a GET request on the **groupContacts** resource from the **group** resource parsed above.
 
 This request should return all of the contacts in the specified group. 
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/people/contacts?groupId=group_id HTTP/1.1
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

3. Process the response from the previous GET request.
 
 The successful response is 200 OK and contains a collection of the **contact** resources of the specified group.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 852
Date: Thu, 18 Jan 2013 00:04:19 GMT
Content-Type: application/json; charset=utf-8
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"_links":{"self":{"href":"/ucwa/oauth/v1/applications/101/people/contacts?groupId=Etah6cLhDq72o1wPuJTauhyAiwZqeTNvmmtYt3LGBsY="}
},
"_embedded":{
"contact": [
{
"uri":"sip:SaraD@contoso.com","sourceNetwork":"SameEnterprise","company":"Metio",
"department":"Sales \u0026 Marketing","office":"20/2107","title":"Product Manager",
"emailAddresses": ["SaraD@contoso.com"],"workPhoneNumber":"tel:+19185550107","type":"User",
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
"rel":"contact","etag":"58253032"
},
{
"uri":"sip:toshm@contoso.com","sourceNetwork":"SameEnterprise","type":"User","name":"Tosh Meston",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/toshm@contoso.com"},
"contactPhoto":{"href":"/ucwa/oauth/v1/applications/101/photos/toshm@contoso.com"},
"contactPresence":{"href":"/ucwa/oauth/v1/applications/101/people/toshm@contoso.com/presence"},
"contactLocation":{"href":"/ucwa/oauth/v1/applications/101/people/toshm@contoso.com/location"},
"contactNote":{"href":"/ucwa/oauth/v1/applications/101/people/toshm@contoso.com/note"},
"contactSupportedModalities":{"href":"/ucwa/oauth/v1/applications/101/people/toshm@contoso.com/supportedMedia"},
"contactPrivacyRelationship":{"href":"/ucwa/oauth/v1/applications/101/people/toshm@contoso.com/privacyRelationship"}
},
"rel":"contact","etag":"4061867540"
}
]
},
"rel":"groupContacts"
}
```

4. Send a GET request on the **contactPresence** resource parsed from the previous response.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/people/sarad@contoso.com/presence HTTP/1.1
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

5. Process the response from the previous GET request. 
 
 The successful response is 200 OK and contains the presence data of the specified contact. 
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 852
Date: Thu, 18 Jan 2013 00:04:19 GMT
Content-Type: application/json; charset=utf-8
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"availability":"Offline",
"deviceType":"Unknown",
"lastActive":"\/Date(1358827096000)\/",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com/presence"}
},
"rel":"contactPresence"
}
```


 In the **lastActive** property value above, **Date(1358827096000)** specifies a time period, in milliseconds, from January 1, 1970 UTC. The escaped string value (including the enclosing backslashes, "\/") characters can be used by **JavaScriptSerializer** in ASP.NET AJAX. For more information, see [ASP .NET AJAX: Inside JSON date and time string](https://msdn.microsoft.com/library/bb299886.aspx).
 

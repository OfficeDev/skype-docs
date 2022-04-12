---
title: Subscribe to the presence of selected contacts for Business 2015
description: Subscribe to the presence of selected members of a contact group.
---


# Subscribe to the presence of selected contacts
Subscribe to the presence of selected members of a contact group.


 _**Applies to:** Skype for Business 2015_

Subscribing to the presence of selected contacts is sometimes referred to as subscribing to an ad hoc group. It involves resource navigation from [application](application_ref.md) to [people](people_ref.md) to [presenceSubscriptions](presenceSubscriptions_ref.md). The last resource is used to create the presence subscription.

An application can proceed to get the new presence data whenever a contact's presence changed event is received from the event channel. This involves getting the [contactPresence](contactPresence_ref.md) resource that is linked to from the returned events data.
The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md). The steps also assume you have already found a contact whose presence you wish to subscribe to via the event channel. For more information, see [Get the user's contact list](GetUsersContactList.md) or [Search for a user's contact](SearchForUsersContact.md).

1. Send a POST request on the **presenceSubscriptions** resource, as pointed to in the **people** resource embedded in the **application** resource. Specify the required **duration** parameter value and supply a list of SIP URIs (obtained from one or more contact resources) in the request body.
 
```
POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/people/presenceSubscriptions HTTP/1.1
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
Content-Length: 45

{"duration":11,"Uris": ["sip:SaraD@contoso.com"]}

```
In this example, the subscription expires in 11 minutes, if it is not extended. The supplied list of **Uris** contains a single user's SIP URI.
 
2. Process the response from the previous POST request.
 
 The successful response should be 201 Created. The response body contains the ID and the relative URL of the newly created subscription. The subscription ID or URL can be used to extend or delete this subscription. Available functionalities or operations are also specified by the returned links. 
 
```
HTTP/1.1 201 Created
Connection: Keep-Alive
Content-Length: 541
Date: Fri, 25 Jan 2013 19:37:38 GMT
Content-Type: application/json; charset=utf-8
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"id":"97b6bd66",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/people/presenceSubscriptions/97b6bd66"},
"memberships":{"href":"/ucwa/oauth/v1/applications/101/people/presenceSubscriptionMemberships?presenceSubscriptionId=97b6bd66"},
"addToPresenceSubscription":{"href":"/ucwa/oauth/v1/applications/101/people/presenceSubscriptionMemberships?presenceSubscriptionId=97b6bd66"}
},
"rel":"presenceSubscription"
}

```
When the event channel is activated, added events for **presenceSubscription** and **contact** and an updated event for **contactPresence** will be received in the event channel. The returned subscription URL in the **presenceSubscription** added event can be used to extend or delete this subscription.
 
```
"events": [{
"link":{
"rel":"contact",
"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com"
},
"in":{
"rel":"subscribedContacts",
"href":"/ucwa/oauth/v1/applications/101/people/subscribedContacts"
},
"type":"added"
},
{
"link":{
"rel":"presenceSubscription",
"href":"/ucwa/oauth/v1/applications/101/people/presenceSubscriptions/97b6bd66"
},
"type":"added"
},
{
"link":{
"rel":"contactPresence",
"href":"/ucwa/oauth/v1/applications/101/people/sarad@Contoso.com/presence"
},
"in":{
"rel":"contact",
"href":"/ucwa/oauth/v1/applications/101/people/sarad@contoso.com"
},
"type":"updated"
}]
```
The **href** value is used to receive the actual presence data of the contact, which is discussed in [Get presence data of a contact](GetPresenceDataOfAContact.md).
 

## Receive presence data from the event channel

To get the actual presence data that appears in the event channel, start the event channel by sending a pending-GET request on the [events](events_ref.md) resource. Later, the response to this request will contain an **updated** event for the **contactPresence** resource.


1. Listen for an **updated** event for the **contactPresence** resource.
 
 The following example shows that the presence for SaraD@contoso.com has been updated. 
 
```
HTTP/1.1 200 OK
{
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/101/events?ack=5"},
"next":{"href":"/ucwa/oauth/v1/applications/101/events?ack=6"}
},
"sender": [
{
"rel":"people","href":"/ucwa/oauth/v1/applications/101/people",
"events": [
{ 
"link":{"rel":"contactPresence","href":"/ucwa/oauth/v1/applications/101/people/saraD@contoso.com/presence"},
"in":{"rel":"contact","href":"/ucwa/oauth/v1/applications/101/people/saraD@contoso.com"},
"type":"updated"
}
]
}
]
}

```
An application should listen for an **updated** event for the **presenceSubscription** resource. This can indicate that the subscription is about to expire.
 
```
{
"_links":{
"self":{"href":"/ucwa/v1/applications/101/events?ack=10"},
"next":{"href":"/ucwa/v1/applications/101/events?ack=11"}
},
"sender": [
{
"rel":"people","href":"/ucwa/v1/applications/101/people",
"events": [
{
"link":{"rel":"presenceSubscription","href":"/ucwa/v1/applications/101/people/presenceSubscriptions/97b6bd66"},
"_embedded":{
"presenceSubscription":{
"id":"97b6bd66",
"_links":{
"self":{"href":"/ucwa/v1/applications/101/people/presenceSubscriptions/97b6bd66"},
"memberships":{"href":"/ucwa/v1/applications/101/people/presenceSubscriptionMemberships?presenceSubscriptionId=97b6bd66"},
"addToPresenceSubscription":{"href":"/ucwa/v1/applications/101/people/presenceSubscriptionMemberships?presenceSubscriptionId=97b6bd66"}
},
"rel":"presenceSubscription"
}
},
"type":"updated"
}
]
}]
}
```

2. Send a GET request on the **presence** resource for the contact.
 
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

3. Process the response from the previous GET request to receive presence data.
 
 As can be seen in the following example, the presence (availability) of saraD@contoso.com is Offline.
 
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
"self":{"href":"/ucwa/oauth/v1/applications/101/people/saraD@contoso.com/presence"}},
"rel":"contactPresence"
}

```


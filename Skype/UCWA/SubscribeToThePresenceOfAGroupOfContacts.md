---
title: Subscribe to the presence of a group of contacts for Skype for Business 2015
description: Subscribe to the presence of members of a contact group.
---


# Subscribe to the presence of a group of contacts
Subscribe to the presence of members of a contact group.


 _**Applies to:** Skype for Business 2015_

Subscribing to group presence, also known as subscribing to a group, amounts to creating a presence subscription to all contacts in a group. This type of subscription involves resource navigation from [application](application_ref.md) to [people](people_ref.md) to [myGroups](myGroups_ref.md), and to a specific [group](group_ref.md), and finally, to a [subscribeToGroupPresence](subscribeToGroupPresence_ref.md) resource. The last request amounts to creating a presence subscription to the group.

An application can proceed to get the new presence data whenever a contact's presence changed event is received from the event channel. This involves getting the [contactPresence](contactPresence_ref.md) resource that is linked to from the returned events data.
The programming flow is illustrated in the following steps. 
The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Send a GET request on myGroups, following the links from the **people** resource embedded in the **application** resource.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/people/groups HTTP/1.1
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
 
 The response you receive should be 200 OK. The body of the response contains the supported groups for the user, including **pinnedGroup**, and **defaultGroup**. Each of these groups has an **id** property and a **name** property. The [groupContacts](groupContacts_ref.md) link points to the location where the contacts of a group can be fetched. The **subscribeToGroupPresence** link points to the location where a subscription to the group's contact presence can be created. The href attribute value of the **subscribeToGroupPresence** resource will be used in the next step to create a subscription to the group.
 
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

3. Send a POST request on a **subscribeToGroupPresence** link that was received in the previous response.
 
```
POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/people/presenceSubscriptions?groupId=default_group_id&amp;duration=11 HTTP/1.1
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

4. Process the response from the previous POST request.
 
 The successful response is 201 Created. The body of the response contains a link to the created presence subscription.
 
```
HTTP/1.1 201 Created
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
"id":"default_group_id",
"_links":{
"self":{
"href":"/ucwa/oauth/v1/applications/101/people/presenceSubscriptions/default_group_id"},
"memberships":{
"href":"/ucwa/oauth/v1/applications/101/people/presenceSubscriptionMemberships?presenceSubscriptionId=pres_sub_id"}
},
"rel":"presenceSubscription"
}
```

## Receive Presence Data from the Event Channel

To get the actual presence data that appears in the event channel, start the event channel by sending a pending-GET request on the [events](events_ref.md) resource. Later, the response to this request will contain an **updated** event for the **contactPresence** resource.


1. Listen for an updated event for the **contactPresence** resource.
 
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


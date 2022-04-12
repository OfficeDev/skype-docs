---
title: Multi-Tab Support
description: UCWA has special support for multiple client instances in different web browser tabs.
---
# Multi-Tab Support


UCWA has special support for multiple client instances in different web browser tabs. This reduces server load and allows the UCWA plugin to pool connections to a server. Application resource instances with the same culture, endpointId, type, and userAgent will internally be the same application resource, but each instance will have its own event channel.

## Creating an Application Resource

When creating an application resource from different tabs, the underlying resource is the same, with a new event channel for each unique instanceId. If an application resource with the current instanceId does not exist the response will have a status code 201, and a new event stream will be created for that application resource. If an application resource does exist for the given instanceId then the response status code will be 200, with the existing event channel.

 **Sample Request**




```
POST https://ext.vdomain.com:4443/ucwa/v1/applications HTTP/1.1
X-MS-RequiresMinResourceVersion: 2
 
{
 "culture": "en-us",
 "endpointId": "123",
 "type": "Browser",
 "userAgent": "foo",
 "instanceId": "abc"
}

```

 **Sample Response**




```
HTTP/1.1 201 (or 200) OK
 
{
 "culture": "en-us",
 "userAgent": "foo",
 "type": "Browser",
 "instanceId": "abc",
 "_links": {
 "self": { "href": "/ucwa/v1/applications/114149606281?instanceId=123" },
 "policies": { "href": "/ucwa/v1/applications/114149606281/policies" },
 "batch": { "href": "/ucwa/v1/applications/114149606281/batch" },
 "events": { "href": "/ucwa/v1/applications/114149606281/events?ack=1&amp;instanceId=abc" }
 },
 "_embedded": {
 "me": { ... },
 "people": { ... },
 "onlineMeetings": { ... },
 "communication": { ... }
 },
 "rel": "application",
 "etag": "4059573954"
}

```


## Deleting an application resource
<a name="bk_addresources"> </a>

When deleting an application resource the application may have other event channels assigned to it. If there are other event channels for other instanceIds, only the event channel for the given instanceId is deleted.


---
title: Events in UCWA for skype for business 2015
description: "UCWA 2.0 implements a server-to- client communication channel within HTTP with a long poll model called pending GET or P-GET."
---
# Events in UCWA
Learn how the server notifies clients by sending events in the event channel.


 _**Applies to:** Skype for Business 2015_

As a real-time communications API, UCWA 2.0 needs a way for the server to notify the client about events such as incoming messages or presence updates. Because HTTP provides no built-in capability for server-to-client communication, UCWA 2.0 implements this within HTTP with a "long poll" model called pending GET or P-GET. This "server-to-client" communication channel is called the event channel to distinguish it from the command channel where ordinary client-to-server HTTP operations take place. 
To begin using the event channel, a client sends a GET request to the event channel URL. If the server has no events to send, it holds the connection open until some events arrive or a timer expires. When the server releases a response, the client sends another GET. This way the event channel GET is maintained as a return vehicle for server-initiated communication. 

## Pending GET (P-GET) response
<a name="sectionSection0"> </a>

The response to a P-GET describes important changes to the state of resources. Each event has an associated "type" parameter. The types for UCWA 2.0 events are started/updated/completed for operations, and added/updated/deleted for resources. Each event additionally has the target link (the resource that the event is about) and a sender link (pointing to the resource that generated the event - the owner or controller of the target resource). Optionally some events include a third link - "in" - for events where a resource can belong to more than one collection at a time. 

In addition to the actual events, a P-GET response contains a "next" link - the URL the client should use to send the next P-GET. The URL changes with each request and is used by the server to keep track of which events the client has seen. By sending a request using a URL that came in a P-GET response, the client proves to the server that it has received that response. This gives the client a guarantee that the event channel will never send duplicate events or skip events - each event will be sent exactly once, and the next P-GET response will resume exactly where the previous one left off. 

An event can contain a link to the changed resource, or for some resources it can include the resource content directly into the event. This is usually done for real-time events - such as incoming IMs or created conversations - where the client might need the content immediately. Links and resources always share a common format, making it possible to write a client in a generic way so that it uses the embedded resource content when it is available, or retrieves it from the URL if it is not. 


## Event response structure
<a name="sectionSection1"> </a>


- self link - common to all UCWA 2.0 resources.
 
- next link - the URL to which the client should send the next P-GET.
 
- sender array - each sender in the array contains an array of events that were sent by that sender.
 
The following is an example event response and an explanation. 

The _links property contains a self link and a next link.

The sender array has two elements - a [communication](communication_ref.md) resource (mostly omitted), and a [conversation](conversation_ref.md) resource.

The events array under conversation contains two elements. The first of these events indicates that a [participant](participant_ref.md) has been added to the conversation. The second event here indicates that the [messaging](messaging_ref.md) resource has been updated, so that the messaging modality is now in the Connecting state.




```
{
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/102/events?ack=2"},
 "next":{"href":"/ucwa/oauth/v1/applications/102/events?ack=3"}
 },
 "sender": [
 {
 "rel":"communication",
 ...
 },
 {
 "rel":"conversation",
 "href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1",
 "events": [
 {
 "link":{
 "rel":"localParticipant",
 "href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/johndoe@contoso.com",
 "title":"John Doe"
 },
 "_embedded":{
 "localParticipant":{
 "sourceNetwork":"SameEnterprise",
 "anonymous":false,
 "name":"John Doe",
 "uri":"sip:johndoe@contoso.com",
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/johndoe@contoso.com"},
 "conversation":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
 "me":{"href":"/ucwa/oauth/v1/applications/102/me"}
 },
 "rel":"participant"
 }
 },
 "type":"added"
 },
 {
 "link":{
 "rel":"messaging",
 "href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"
 },
 "_embedded":{
 "messaging":{
 "state":"Connecting",
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"},
 ...
 }
 "rel":"messaging"
 }
 },
 "type":"updated"
 },
 {
 ...
 }
 ]
 }
 ]
}

```


## Aggregation
<a name="sectionSection2"> </a>

UCWA 2.0 tries to minimize the consumed bandwidth and number of HTTP roundtrips a client needs to make - an optimization for clients with limited bandwidth and battery life. One way these goals are accomplished is by a mechanism called event aggregation, where some lower-priority events do not cause the P-GET to be released immediately - UCWA 2.0 holds it for some time and bundles multiple such events together in one P-GET response when possible. However, the aggregation rules are set up in such a way to ensure that real-time events are still delivered to the client as quickly as possible.

When the events are grouped together, the aggregation mechanism attempts to combine events to reflect the latest state. For example, multiple presence change events for a single contact will be merged into one event showing just the latest state. This helps reduce the response size, and helps to ensure that the client can act upon the events it receives immediately - without looking ahead to see if they were superseded by later events. 

For aggregation purposes, all events are grouped into four categories - real-time, high, medium, and low priority. The reference documentation provides details about the priority level of each event. Using a query parameter, a client can control how long events will be held before being released - so that it can choose a balance between real-time updates and conserving bandwidth and battery life. 

The presence of the aggregation mechanism means that status updates can skip some intermediate steps if they happen quickly enough. For example, an operation that completes very quickly might have only a "completed" event. A conference participant who joins and then leaves immediately might not cause an event to be sent.

Note that even though events have different priority levels, their relative ordering is still maintained. If a real-time event--such as an incoming invitation--causes a P-GET to be released, all of the events currently in the queue will be sent along with and ahead of that event. 


## Timeouts
<a name="sectionSection3"> </a>

When UCWA 2.0 sends no events for a long period of time, it is possible that the network connection has been interrupted without the client noticing it. To prevent this, a P-GET has a timeout interval - after which the server will send back a response with no events, and containing just the "next" link. A client can control the timeout interval by the use of a query parameter. 

If the client sends another P-GET request before the current one is released, the server will hold on to the latest request and respond to the earlier one with HTTP code 409, UCWA error subcode PGetReplaced.

If a client sends multiple replacement P-GETs in quick succession (such as, to change the aggregation settings), there is a chance that they cross paths and arrive at the server in the wrong order. To let the server know which of these requests is intended to be the latest, a client can supply an optional query parameter: "priority". A request with a higher priority value will replace a request with the same or lower priority


## Resume/resynchronize
<a name="sectionSection4"> </a>

If a client has not sent a P-GET for a long time, the server can clean up its state to conserve resources. One effect of this inactivity is that it shuts down all active presence subscriptions, and closes all conversations.

This normally results in a set of notifications sent in the event channel, but in this case the client is not around to receive them. In this case, when the client becomes active again, the server will return a "resume" link as opposed to a "next" link in the first event response after a long period of inactivity. This link has the same function as "next", but in addition notifies the client that the transient state such as active conversations and subscriptions has been reset. 

If the client uses an event channel link that is not the correct one (for instance, one from an earlier event channel response), the event response will contain nothing but a "resync" link, pointing to the last event package that the client has not yet acknowledged. The "resync" link usually indicates that a client lost track of the "next" link, and should never happen in normal operation.



# Event channel details
Learn how to request that the event channel be started, and to process events when they arrive on the event channel.


 _**Applies to:** Skype for Business 2015_

## Requesting events
<a name="sectionSection0"> </a>

The client application discovers a link to the event channel resource, the [events](events_ref.md) resource, from the [application](application_ref.md) resource. The link already includes some query parameters, so it is important that the client application uses a proper URL parsing/building algorithm to append its own parameters when needed, instead of doing simple string concatenation.

An event channel URL provided by the server usually looks like the following.




```
/applications/1234/events?ack=5
```

The "ack=N" parameter tells the server that by following this link, the client acknowledges that it has received the event whose response number is 5, so the response to the pending GET (P-GET) will include events starting from the correct point. The client application should never change the ack=N parameter in an event channel URL, but should instead always use one provided by the server in the response to the previous P-GET request or in the response from the GET request on the application response if it is sending the first P-GET.

Optionally, the client application can add query parameters that control the aggregation intervals, as in this example. For more information about event aggregation, see [Events in UCWA](EventsInUCWA.md). 




```
/applications/1234/events?ack=5&amp;medium=300&amp;low=600&amp;timeout=900
```

Here, the "medium" parameter sets the aggregation interval for medium-priority events to 300 seconds, and the aggregation interval for low-priority events to 600 seconds. The "timeout" parameter specifies that if the server does not have any events in 900 seconds, it should respond with an empty P-GET response.

An application normally does not need to repeat these parameters in every request, as they are remembered by the server and will take effect for the next P-GET until the client changes them again. Note that the parameter values can be lost if the event channel is destroyed and then re-created. If the server sends a "resume" link, the application should resend the query parameters.


## Processing events
<a name="sectionSection1"> </a>

A P-GET response consists of the following parts:


- The **self** and **next** links; the client should use the URL in the **next** link in the next P-GET it sends.
 
- The "senders" section with the list of blocks for each event sender. A sender can occur multiple times, as event ordering is preserved. Each sender block includes a link that identifies the resource that sent the event.
 
A "sender" section has a block for every event that was sent by the sender. The block includes the following.


- Event type: added, updated, deleted (for resources) or started, updated, completed (for operations). In a JSON response, the event type is a property. In an XML response, the event type is an element name.
 
- The link to the event target (the "link" property in JSON, or the "rel"/"href" attributes in XML).
 
- An optional "in" link if the resource was added or removed from a collection.
 
- Optionally, the content of the updated resource in an embedded form.
 
- For failed operations or server-initiated changes, a "reason" block that describes the details of the change. The block follows the common UCWA error response structure.
 
Clients should issue the next P-GET request before they process an event channel response, but must also carry out the processing in sequence.


## Sample JSON event response
<a name="sectionSection2"> </a>

The following is a sample event response in JSON format.


```
{ 
 "_links": { 
 "self": { 
 "href": "/ucwa/oauth/v1/applications/102/events?ack=1" 
 }, 
 "next": { 
 "href": "/ucwa/oauth/v1/applications/102/events?ack=2" 
 } 
 }, 
 "sender": [{ 
 "rel": "communication", 
 "href": "/ucwa/oauth/v1/applications/102/communication", 
 "events": [{ 
 "link": { 
 "rel": "communication", 
 "href": "/ucwa/oauth/v1/applications/102/communication" 
 }, 
 "_embedded": { 
 "communication": { 
 "56de7bbf-1081-43e6-bbf2-1cabf3224c83": "please pass this in a PUT request", 
 "supportedModalities": ["Messaging"], 
 "supportedMessageFormats": ["Plain"], 
 "_links": { 
 "self": {"href": "/ucwa/oauth/v1/applications/102/communication"}, 
 "conversations": {"href": "/ucwa/oauth/v1/applications/102/communication/conversations?filter=active"}, 
 "startMessaging": {"href": "/ucwa/oauth/v1/applications/102/communication/messagingInvitations"}, 
 "startOnlineMeeting": {"href": "/ucwa/oauth/v1/applications/102/communication/onlineMeetingInvitations?onlineMeetingUri=adhoc"}, 
 "joinOnlineMeeting": {"href": "/ucwa/oauth/v1/applications/102/communication/onlineMeetingInvitations"} 
 }, 
 "rel": "communication", 
 "etag": "2943169141" 
 } 
 }, 
 "type": "updated" 
 }] 
 }, { 
 "rel": "me", 
 "href": "/ucwa/oauth/v1/applications/102/me", 
 "events": [{ 
 "link": { 
 "rel": "me", 
 "href": "/ucwa/oauth/v1/applications/102/me" 
 }, 
 "type": "updated" 
 }, { 
 "link": { 
 "rel": "presence", 
 "href": "/ucwa/oauth/v1/applications/102/me/presence" 
 }, 
 "type": "added" 
 }, { 
 "link": { 
 "rel": "note", 
 "href": "/ucwa/oauth/v1/applications/102/me/note" 
 }, 
 "type": "added" 
 }, { 
 "link": { 
 "rel": "location", 
 "href": "/ucwa/oauth/v1/applications/102/me/location" 
 }, 
 "type": "added" 
 }] 
 }] 
}

```


## Sample XML event response
<a name="sectionSection3"> </a>


> [!NOTE] 
> To receive event data in XML format, you must include the following header in your request:


```
Accept: application/vnd.microsoft.com.ucwa+xml
```

The following is a sample event response in XML format.




```XML
<?xml version="1.0" encoding="utf-8"?> 
<events href="/ucwa/oauth/v1/applications/102/events?ack=1" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa"> 
 <link rel="next" href="/ucwa/oauth/v1/applications/102/events?ack=2" /> 
 <sender rel="communication" href="/ucwa/oauth/v1/applications/102/communication"> 
 <updated rel="communication" href="/ucwa/oauth/v1/applications/102/communication"> 
 <resource rel="communication" href="/ucwa/oauth/v1/applications/102/communication"> 
 <link rel="conversations" href="/ucwa/oauth/v1/applications/102/communication/conversations?filter=active" /> 
 <link rel="startMessaging" href="/ucwa/oauth/v1/applications/102/communication/messagingInvitations" /> 
 <link rel="startOnlineMeeting" href="/ucwa/oauth/v1/applications/102/communication/onlineMeetingInvitations?onlineMeetingUri=adhoc" /> 
 <link rel="joinOnlineMeeting" href="/ucwa/oauth/v1/applications/102/communication/onlineMeetingInvitations" /> 
 <property name="56de7bbf-1081-43e6-bbf2-1cabf3224c83">please pass this in a PUT request</property> 
 <propertyList name="supportedModalities"> 
 <item>Messaging</item> 
 </propertyList> 
 <propertyList name="supportedMessageFormats"> 
 <item>Plain</item> 
 </propertyList> 
 <property name="etag">2943169141</property> 
 </resource> 
 </updated> 
 </sender> 
 <sender rel="me" href="/ucwa/oauth/v1/applications/102/me"> 
 <updated rel="me" href="/ucwa/oauth/v1/applications/102/me" /> 
 <added rel="presence" href="/ucwa/oauth/v1/applications/102/me/presence" /> 
 <added rel="note" href="/ucwa/oauth/v1/applications/102/me/note" /> 
 <added rel="location" href="/ucwa/oauth/v1/applications/102/me/location" /> 
 </sender> 
</events>

```


## Special responses
<a name="sectionSection4"> </a>

In the case of an error or a special condition - for example, when an application hasn't sent a request in a while and the ack number is too low - the event response can be different from the common case.


- If the server application is active and the client sends an "ack" value that is out of order (lower than the earliest one that the server has, or higher than the latest one), the response will consist of a single link with rel="resync". Following the link will give the client the first unacknowledged event set. Such a response usually indicates a client bug and should never occur in normal operation.
 
 The client should be prepared to see such a link in any response. If the client application receives such a link, the correct action for it is to clear the caches for all transient data (including active conversations and subscriptions). This indicates that the client application was not receiving events for some time, as there was no connectivity.
 
- If the server application does not exist at all, UCWA will send a failure response (404) with subcode=ApplicationNotFound. If the client intends to continue using the application, it should recreate the application by sending a POST request on the [applications](applications_ref.md) resource.
 
- If the client sends another P-GET with the same ack ID, the previous P-GET will be released with a failure response (409 / Conflict) with UCWA error subcode PGetReplaced. If the client did in fact send another P-GET, it can ignore this code - otherwise it is an indication that the UCWA application ID is used by another active instance of the client application.
 

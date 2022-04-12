---
title: Receive an IM task for Skype for Business 2015
description: "The task describes the steps that are necessary for one user (ToshM@contoso.com) to receive an instant message from another user, LeneA@contoso.com."
---
# Receive an IM



 _**Applies to:** Skype for Business 2015_

The task presented here describes the steps that are necessary for one user (ToshM@contoso.com) to receive an instant message from another user (LeneA@contoso.com).

The steps shown here assume that you have already created an application and have received a response that contains the HREF for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Send a POST request on the **makeMeAvailable** resource.
 
 A POST request on the [makeMeAvailable](makeMeAvailable_ref.md) resource makes it possible for the user of the application to receive incoming messages. In the following sample, the request body contains information about the supported modality, messaging.
 
 A successful response (not shown) is '204 No Content'.
 
  ```
  POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/104/me/makeMeAvailable HTTP/1.1
  Accept: application/json
  Content-Type: application/json
  Authorization: Bearer cwt=AAEB...buHc
  X-Ms-Origin: http://localhost
  X-Requested-With: XMLHttpRequest
  Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
  Accept-Language: en-US
  Accept-Encoding: gzip, deflate
  User-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)
  Host: lyncweb.contoso.com
  Content-Length: 37
  DNT: 1
  Connection: Keep-Alive
  Cache-Control: no-cache

  {"SupportedModalities": ["Messaging"]}
  ```

2. Send a GET request on the **application** resource.
 
  ```
  GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/104 HTTP/1.1
  Authorization: Bearer cwt=AAEB...buHc
  Accept: application/json
  X-Ms-Origin: http://localhost
  X-Requested-With: XMLHttpRequest
  Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
  Accept-Language: en-US
  Accept-Encoding: gzip, deflate
  User-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)
  Host: lyncweb.contoso.com
  DNT: 1
  Connection: Keep-Alive
  ```

3. Process the response from the previous GET request.
 
  The response from a GET request on the [application](application_ref.md) resource contains useful links, as well as important embedded resources: [me](me_ref.md), [people](people_ref.md), [onlineMeetings](onlineMeetings_ref.md), and [communication](communication_ref.md).
 
  ```
  HTTP/1.1 200 OK
  Connection: Keep-Alive
  Content-Length: 3857
  Date: Thu, 07 Nov 2013 00:40:38 GMT
  Content-Type: application/json; charset=utf-8
  Server: Microsoft-IIS/7.5
  Cache-Control: no-cache
  P3P: CP="IDC CUR ADMa OUR BUS"
  X-AspNet-Version: 4.0.30319
  Set-Cookie: cwt_ucwa=AAEB...dG9z; path=/ucwa/oauth/v1/applications/104/photos; secure; HttpOnly
  X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
  X-Powered-By: ASP.NET

  {
   "culture":"en-US",
   "userAgent":"UCWA Samples",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104"},
   "policies":{"href":"/ucwa/oauth/v1/applications/104/policies"},
   "batch":{"href":"/ucwa/oauth/v1/applications/104/batch"},
   "events":{"href":"/ucwa/oauth/v1/applications/104/events?ack=1"}
   },
   "_embedded":{
   "me":{
   "name":"Tosh Meston",
   "uri":"sip:toshm@contoso.com",
   "emailAddresses": ["toshm@contoso.com"],
   "_links":{
  "self":{"href":"/ucwa/oauth/v1/applications/104/me"},
   "note":{"href":"/ucwa/oauth/v1/applications/104/me/note"},
   "presence":{"href":"/ucwa/oauth/v1/applications/104/me/presence"},
   "location":{"href":"/ucwa/oauth/v1/applications/104/me/location"},
   "reportMyActivity":{"href":"/ucwa/oauth/v1/applications/104/me/reportMyActivity"},
   "callForwardingSettings":{"href":"/ucwa/oauth/v1/applications/104/me/callForwardingSettings"},
   "phones":{"href":"/ucwa/oauth/v1/applications/104/me/phones"},
   "photo":{"href":"/ucwa/oauth/v1/applications/104/photos/toshm@contoso.com"}
   },
   "rel":"me"
   },
   "people":{
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/people"},
   "presenceSubscriptions":{"href":"/ucwa/oauth/v1/applications/104/people/presenceSubscriptions"},
   "subscribedContacts":{"href":"/ucwa/oauth/v1/applications/104/people/subscribedContacts"},
   "presenceSubscriptionMemberships":{"href":"/ucwa/oauth/v1/applications/104/people/presenceSubscriptionMemberships"},
   "myGroups":{"href":"/ucwa/oauth/v1/applications/104/people/groups"},
   "myGroupMemberships":{"href":"/ucwa/oauth/v1/applications/104/people/groupMemberships"},
   "myContacts":{"href":"/ucwa/oauth/v1/applications/104/people/contacts"},
   "myPrivacyRelationships":{"href":"/ucwa/oauth/v1/applications/104/people/privacyRelationships"},
   "myContactsAndGroupsSubscription":{"href":"/ucwa/oauth/v1/applications/104/people/contactsAndGroupsSubscription"},
   "search":{"href":"/ucwa/oauth/v1/applications/104/people/search"}
   },
   "rel":"people"
   },
   "onlineMeetings":{
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/onlineMeetings"},
   "myOnlineMeetings":{"href":"/ucwa/oauth/v1/applications/104/onlineMeetings/myOnlineMeetings"},
   "onlineMeetingDefaultValues": "href":"/ucwa/oauth/v1/applications/104/onlineMeetings/defaultValues"},
   "onlineMeetingEligibleValues": "href":"/ucwa/oauth/v1/applications/104/onlineMeetings/eligibleValues"}, "onlineMeetingInvitationCustomization":{"href":"/ucwa/oauth/v1/applications/104/onlineMeetings/customInvitation"},
   "onlineMeetingPolicies":{"href":"/ucwa/oauth/v1/applications/104/onlineMeetings/policies"},
   "phoneDialInInformation":{"href":"/ucwa/oauth/v1/applications/104/onlineMeetings/phoneDialInInformation"},
   "myAssignedOnlineMeeting":{"href":"/ucwa/oauth/v1/applications/104/onlineMeetings/myOnlineMeetings/W38NP8NT"}
   },
   "rel":"onlineMeetings"
   },
   "communication":{
   "c558dd5a-d323-484a-8b82-9c71687f04ef":"please pass this in a PUT request",
   "supportedModalities": ["Messaging"],
   "supportedMessageFormats": ["Plain"],
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication"},
   "startPhoneAudio":{"href":"/ucwa/oauth/v1/applications/104/communication/phoneAudioInvitations"},
   "conversations":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations?filter=active"},
   "startMessaging":{"href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations"},
   "startOnlineMeeting":{"href":"/ucwa/oauth/v1/applications/104/communication/onlineMeetingInvitations?onlineMeetingUri=adhoc"},
   "joinOnlineMeeting":{"href":"/ucwa/oauth/v1/applications/104/communication/onlineMeetingInvitations"}
   },
   "rel":"communication",
   "etag":"1925102412"
   }
   },
   "rel":"application"  
  }
  ```

4. Send a GET request on the **contacts** resource.
 
 The sample request shown here is abbreviated with some headers not shown.
 
  ```
  GET /ucwa/oauth/v1/applications/104/people/contacts HTTP/1.1 
  Host: lyncweb.contoso.com
  Accept: application/json
  ```

5. Process the response from the previous GET request.
 
 The sample response shown here is abbreviated, and shows only the response body. 
 
 The sample shows contact information for two of the local participant's contacts.
 
  ```
   {
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/people/contacts"}
   },
   "_embedded":{
   "contact": [{
   "uri":"sip:adrianag@contoso.com",
   "sourceNetwork":"SameEnterprise",
   "emailAddresses": ["adrianag@contoso.com"],
   "type":"User",
   "name":"Adriana Giorgi",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/people/adrianag@contoso.com"},
   "contactPhoto":{"href":"/ucwa/oauth/v1/applications/104/photos/adrianag@contoso.com"},
   "contactPresence":{"href":"/ucwa/oauth/v1/applications/104/people/adrianag@contoso.com/presence"},
   "contactLocation":{"href":"/ucwa/oauth/v1/applications/104/people/adrianag@contoso.com/location"},
   "contactNote":{"href":"/ucwa/oauth/v1/applications/104/people/adrianag@contoso.com/note"},
   "contactSupportedModalities":{"href":"/ucwa/oauth/v1/applications/104/people/adrianag@contoso.com/supportedMedia"},
   "contactPrivacyRelationship":{"href":"/ucwa/oauth/v1/applications/104/people/adrianag@contoso.com/privacyRelationship"}
   },
   "rel":"contact",
   "etag":"2765800399"
   }, 
   {
   "uri":"sip:lenea@contoso.com",
   "sourceNetwork":"SameEnterprise",
   "emailAddresses": ["lenea@contoso.com"],
   "type":"User",
   "name":"Lene Aaling",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com"},
   "contactPhoto":{"href":"/ucwa/oauth/v1/applications/104/photos/lenea@contoso.com"},
   "contactPresence":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/presence"},
   "contactLocation":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/location"},
   "contactNote":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/note"},
   "contactSupportedModalities":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/supportedMedia"},
   "contactPrivacyRelationship":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/privacyRelationship"}
   },
   "rel":"contact",
   "etag":"2880454307"
   }
   ]},
   "rel":"myContacts" 
  }
  ```

6. Send a GET request on the **events** resource.
 
 In the interest of brevity, only this GET request on the [events](events_ref.md) resource is shown. Subsequent GET requests on the **events** resource are similar and differ only in the query parameter (ack = _n_). An application normally will send a series of GET requests on this resource.
  
  ```
   GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/104/events?ack=1 HTTP/1.1
  Authorization: Bearer cwt=AAEB...buHc
  Accept: application/json
  X-Ms-Origin: http://localhost
  X-Requested-With: XMLHttpRequest
  Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
  Accept-Language: en-US
  Accept-Encoding: gzip, deflate
  User-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)
  Host: lyncweb.contoso.com
  DNT: 1
  Connection: Keep-Alive </pre>
  ```

7. Process the response from the previous GET request.
 
 The events shown here come from two senders: the [me](me_ref.md) resource and the [communication](communication_ref.md) resource. For a given sender there are links to a number of events, along with the type of event, such as "updated" or "added".
 
  ```
  HTTP/1.1 200 OK
  Connection: Keep-Alive
  Content-Length: 1727
  Date: Thu, 07 Nov 2013 00:40:58 GMT
  Content-Type: application/json; charset=utf-8
  Server: Microsoft-IIS/7.5
  Cache-Control: no-cache
  X-AspNet-Version: 4.0.30319
  X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
  X-Powered-By: ASP.NET

  {
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/events?ack=1"},
   "next":{"href":"/ucwa/oauth/v1/applications/104/events?ack=2"}
   },
   "sender": [{
   "rel":"me",
   "href":"/ucwa/oauth/v1/applications/104/me",
   "events": [{
   "link":{
   "rel":"me",
   "href":"/ucwa/oauth/v1/applications/104/me"
   },
   "type":"updated"
   },{
   "link":{
   "rel":"presence",
   "href":"/ucwa/oauth/v1/applications/104/me/presence"
   },
   "type":"added"
   },{
   "link":{
   "rel":"note",
   "href":"/ucwa/oauth/v1/applications/104/me/note"
   },
   "type":"added"
   },{
   "link":{
   "rel":"location",
   "href":"/ucwa/oauth/v1/applications/104/me/location"
   },
   "type":"added"
   }]
   },{
   "rel":"communication",
   "href":"/ucwa/oauth/v1/applications/104/communication",
   "events": [{
   "link":{
   "rel":"communication",
   "href":"/ucwa/oauth/v1/applications/104/communication"
   },
   "_embedded":{
   "communication":{
   "c558dd5a-d323-484a-8b82-9c71687f04ef":"please pass this in a PUT request",
   "supportedModalities": ["Messaging"],
   "supportedMessageFormats": ["Plain"],
   "_links":{
   "self": {"href":"/ucwa/oauth/v1/applications/104/communication"},
   "startPhoneAudio": {"href":"/ucwa/oauth/v1/applications/104/communication/phoneAudioInvitations"},
   "conversations": {"href":"/ucwa/oauth/v1/applications/104/communication/conversations?filter=active"},
   "startMessaging": {"href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations"},
   "startOnlineMeeting": {"href":"/ucwa/oauth/v1/applications/104/communication/onlineMeetingInvitations ? onlineMeetingUri=adhoc"},
   "joinOnlineMeeting": {"href":"/ucwa/oauth/v1/applications/104/communication/onlineMeetingInvitations"}
   },
   "rel":"communication",
   "etag":"1925102412"
   }
   },
   "type":"updated"
   }]
   }] 
  }
  ```

8. Process the response from the next GET request on the **events** resource.
 
 In this sample we see that the sender is the [communication](communication_ref.md) resource. The event channel contains several embedded resources, including a [messagingInvitation](messagingInvitation_ref.md) resource that has been started. This and other embedded resources indicate that an incoming message from Lene Aaling will arrive soon. The invitation contains links to the [accept](accept_ref.md) and [decline](decline_ref.md) resources, one of which can be used to accept or decline the incoming message. Notice that the [message](message_ref.md) link contains the actual text of the message.
 
  ```
  HTTP/1.1 200 OK
  Connection: Keep-Alive
  Content-Length: 4187
  Date: Thu, 07 Nov 2013 00:41:07 GMT
  Content-Type: application/json; charset=utf-8
  Server: Microsoft-IIS/7.5
  Cache-Control: no-cache
  X-AspNet-Version: 4.0.30319
  X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
  X-Powered-By: ASP.NET

  {
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/events?ack=2"},
   "next":{"href":"/ucwa/oauth/v1/applications/104/events?ack=3"}
   },
   "sender": [{
   "rel":"communication",
   "href":"/ucwa/oauth/v1/applications/104/communication",
   "events": [{
   "link":{
   "rel":"messagingInvitation",
   "href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations/6408"
   },
   "_embedded":{
   "messagingInvitation":{
   "direction":"Incoming",
   "importance":"Normal",
   "threadId":"Ac7bUftwUcVMzgTzRl+OQchCsUn35Q==",
   "state":"Connecting",
   "subject":"",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations/6408"},
   "to":{"href":"/ucwa/oauth/v1/applications/104/people/toshm@contoso.com"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "accept":{"href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations/6408/accept"},
   "decline":{"href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations/6408/decline"},
   "messaging":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"},
   "message":{"href":"data:text/plain;charset=utf-8,Hi+Tosh%0d%0a"}
   },
   "_embedded":{
   "from":{
   "sourceNetwork":"SameEnterprise",
   "anonymous":false,
   "name":"Lene Aaling",
   "uri":"sip:lenea@contoso.com",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@contoso.com"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "contact":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com"},
   "contactPresence":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/presence"}
   },
   "rel":"participant"
   }
   },
   "rel":"messagingInvitation"
   }
   },
   "type":"started"
   },
   {
   "link":{
   "rel":"conversation",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"
   },
   "_embedded":{
   "conversation":{
   "state":"Incoming",
   "threadId":"Ac7bUftwUcVMzgTzRl+OQchCsUn35Q==",
   "subject":"",
   "activeModalities": ["Messaging"],
   "importance":"Normal",
   "recording":false,
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "applicationSharing":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/applicationSharing"},
   "audioVideo":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/audioVideo"},
   "dataCollaboration":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/dataCollaboration"},
   "messaging":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"},
   "phoneAudio":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/phoneAudio"}
   },
   "rel":"conversation"
   }
   }, 
   type":"added"
   }]
   },
   {
   "rel":"conversation",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0",
   "events": [{
   "link":{
   "rel":"localParticipant",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/toshm@contoso.com",
   "title":"Tosh Meston"
   },
   "_embedded":{
   "localParticipant":{
   "sourceNetwork":"SameEnterprise",
   "anonymous":false,
   "name":"Tosh Meston",
   "uri":"sip:toshm@contoso.com",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/toshm@contoso.com"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "me":{"href":"/ucwa/oauth/v1/applications/104/me"}
   },
   "rel":"participant"
   }
   },
   "type":"added"
   }]
   }] 
  }
  ```

9. Process the response from the next GET request on the **events** resource.
 
  in this sample, the [conversation](conversation_ref.md) resource is the event sender, indicating that a remote participant (Lene Aaling) has been added to the conversation.
 
  ```
  HTTP/1.1 200 OK 
  Connection: Keep-Alive
  Content-Length: 527
  Date: Thu, 07 Nov 2013 00:41:08 GMT
  Content-Type: application/json; charset=utf-8
  Server: Microsoft-IIS/7.5
  Cache-Control: no-cache
  X-AspNet-Version: 4.0.30319
  X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
  X-Powered-By: ASP.NET

  {
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/events?ack=3"},
   "next":{"href":"/ucwa/oauth/v1/applications/104/events?ack=4"}
   },
   "sender": [{
   "rel":"conversation",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0",
   "events": [{
   "link":{
   "rel":"participant",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@metio.ms",
   "title":"Lene Aaling"
   },
   "type":"added"
   }]
   }]
  }
  ```

10. Process the response from the next GET request on the **events** resource.
 
 The event channel contains an embedded **messagingInvitation** resource, similar to step 8, but the event type has changed from "started" to "updated" with links to the **accept** and **decline** resources no longer being present.
 
 Notice that the href in the [message](message_ref.md) link contains the text of the message as a Data URL.
 
  ```
  Content-Length: 1895
  Date: Thu, 07 Nov 2013 00:41:09 GMT
  Content-Type: application/json; charset=utf-8
  Server: Microsoft-IIS/7.5
  Cache-Control: no-cache
  X-AspNet-Version: 4.0.30319
  X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
  X-Powered-By: ASP.NET

  {
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/events?ack=4"},
   "next":{"href":"/ucwa/oauth/v1/applications/104/events?ack=5"}
   },
   "sender": [{
   "rel":"communication",
   "href":"/ucwa/oauth/v1/applications/104/communication",
   "events": [{
   "link":{
   "rel":"messagingInvitation",
    "href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations/6408"
   },
   "_embedded":{
   "messagingInvitation":{
   "direction":"Incoming",
   "importance":"Normal",
   "threadId":"Ac7bUftwUcVMzgTzRl+OQchCsUn35Q==",
   "state":"Connecting",
   "subject":"",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations/6408"},
   "to":{"href":"/ucwa/oauth/v1/applications/104/people/toshm@contoso.com"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "messaging":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"},
   "message":{"href":"data:text/plain;charset=utf-8,Hi+Tosh%0d%0a"}
   },
   "_embedded":{
   "from":{
   "sourceNetwork":"SameEnterprise",
   "anonymous":false,
   "name":"Lene Aaling",
   "uri":"sip:lenea@contoso.com",
   "_links":{
   "self":{
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@contoso.com"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "contact":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com"},
   "contactPresence":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/presence"},
   "contactPhoto":{"href":"/ucwa/oauth/v1/applications/104/photos/lenea@contoso.com"}
   },
   "rel":"participant"
   }
   },
   "rel":"messagingInvitation"
   }
   },
   "type":"updated"
   }] 
   }]  
  }
  ```

11. Send a GET request on the [participant](participant_ref.md) resource, the resource for Lene Aaling.
 
 The purpose of this step is to obtain information about the sender of the message.
 
  ```
  GET https://lyncweb.metio.ms/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@metio.ms HTTP/1.1
  Authorization: Bearer cwt=AAEB...buHc
  Accept: application/json
  X-Ms-Origin: http://localhost
  X-Requested-With: XMLHttpRequest
  Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
  Accept-Language: en-US
  Accept-Encoding: gzip, deflate
  User-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)
  Host: lyncweb.contoso.com
  DNT: 1
  Connection: Keep-Alive
  ```

12. Process the response from the previous GET request.
 
 A successful response is '200 OK'. The body of the response contains information about the remote participant, Lene Aaling.
 
  ```
  HTTP/1.1 200 OK
  Connection: Keep-Alive
  HTTP/1.1 200 OK
  Connection: Keep-Alive
  Content-Length: 689
  Date: Thu, 07 Nov 2013 00:41:08 GMT
  Content-Type: application/json; charset=utf-8
  Server: Microsoft-IIS/7.5
  Cache-Control: no-cache
  X-AspNet-Version: 4.0.30319
  X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
  X-Powered-By: ASP.NET

  {
   "sourceNetwork":"SameEnterprise",
   "anonymous":false,
   "name":"Lene Aaling",
   "uri":"sip:lenea@contoso.com",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@contoso.com"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "contact":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com"},
   "contactPresence":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/presence"},
   "contactPhoto":{"href":"/ucwa/oauth/v1/applications/104/photos/lenea@contoso.com"}
   },
   "rel":"participant"  
  }
  ```

13. Send a POST request on the **accept** resource.
 
 The [accept](accept_ref.md) resource, which can be found in the cache, was shown in step 8.
 
  ```
  POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/104256996066/communication/messagingInvitations/64088d10047844518b75b392c5b04e39/accept?sessionContext=2687ba8f-048e-4ffa-83b3-0849c6b9004c HTTP/1.1
  Authorization: Bearer cwt=AAEB...buHc
  Accept: application/json
  X-Ms-Origin: http://localhost
  X-Requested-With: XMLHttpRequest
  Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
  Accept-Language: en-US
  Accept-Encoding: gzip, deflate
  User-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)
  Host: lyncweb.contoso.com
  Content-Length: 0
  DNT: 1
  Connection: Keep-Alive
  Cache-Control: no-cache
  ```

14. Process the response from the previous POST request.
 
 The response should be '204 No Content'.
 
  ```
  HTTP/1.1 204 No Content
  Connection: Keep-Alive
  Content-Length: 0
  Date: Thu, 07 Nov 2013 00:41:09 GMT
  Server: Microsoft-IIS/7.5
  Cache-Control: no-cache
  X-AspNet-Version: 4.0.30319
  X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
  X-Powered-By: ASP.NET
  ```

15. Process the response from the next GET request on the **events** resource.
 
 Note that for brevity, some events have been omitted.
 
  ```
  HTTP/1.1 200 OK
  Connection: Keep-Alive
  Content-Length: 8816
  Date: Thu, 07 Nov 2013 00:41:09 GMT
  Content-Type: application/json; charset=utf-8
  Server: Microsoft-IIS/7.5
  Cache-Control: no-cache
  X-AspNet-Version: 4.0.30319
  X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
  X-Powered-By: ASP.NET

  {
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/events?ack=5"},
   "next":{"href":"/ucwa/oauth/v1/applications/104/events?ack=6"}
   },
   "sender": [{
   "rel":"conversation",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0",
   "events": [{
   "link":{
   "rel":"messaging",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"
   },
   "_embedded":{
   "messaging":{
   "state":"Connected",
   "negotiatedMessageFormats": ["Plain"],
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "stopMessaging":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging/terminate"},
   "sendMessage":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging/messages"},
   "setIsTyping":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging/typing"},
   "typingParticipants":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging/typingParticipants"}
   },
   "rel":"messaging"
   }
   },
   "type":"updated"
   }]
   },{
   "rel":"communication",
   "href":"/ucwa/oauth/v1/applications/104/communication",
   "events": [{ 
   "link":{
   "rel":"conversation",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "_embedded":{
   "conversation":{
   "state":"Connected",
   "threadId":"Ac7bUftwUcVMzgTzRl+OQchCsUn35Q==",
   "subject":"",
   "activeModalities": ["Messaging"],
   "importance":"Normal",
   "recording":false,
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "applicationSharing":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/applicationSharing"},
   "audioVideo":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/audioVideo"},
   "dataCollaboration":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/dataCollaboration"},
   "messaging":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"},
   "phoneAudio":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/phoneAudio"},
   "localParticipant":{
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/toshm@contoso.com",
   "title":"Tosh Meston"
   },
   "addParticipant":{"href":"/ucwa/oauth/v1/applications/104/communication/participantInvitations?conversation=eee0"}
   },
   "rel":"conversation"
   }
   },
   "type":"updated"
   }]
   },{
   "rel":"conversation",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0",
   "events": [{
   "link":{
   "rel":"participantMessaging",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/toshm@contoso.com/messaging"},
   "in":{
   "rel":"localParticipant",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/toshm@contoso.com",
   "title":"Tosh Meston"
   },
   "_embedded":{
   "participantMessaging":{
   "_links":{
   "self"{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/toshm@contoso.com/messaging"},
   "participant":{
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/toshm@contoso.com",
   "title":"Tosh Meston"
   }
   },
   "rel":"participantMessaging"
   }
   },
   "type":"added"
   },{
   "link":{
   "rel":"participantMessaging",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@contoso.com/messaging"
   },
   "in":{
   "rel":"participant",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@contoso.com",
   "title":"Lene Aaling"
   },
   "type":"added"
   },{
   "link":{
   "rel":"participant",
   "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@contoso.com",
   "title":"Lene Aaling"
   },
   "type":"updated"
   }]
   },{
   "rel":"communication",
   "href":"/ucwa/oauth/v1/applications/104/communication",
   "events": [{
   "link":{
   "rel":"messagingInvitation",
   "href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations/6408"},
   "status":"Success",
   "_embedded":{
   "messagingInvitation":{
   "direction":"Incoming",
   "importance":"Normal",
   "threadId":"Ac7bUftwUcVMzgTzRl+OQchCsUn35Q==",
   "state":"Connected",
   "subject":"",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/messagingInvitations/64088"},
   "to":{"href":"/ucwa/oauth/v1/applications/104/people/toshm@contoso.com"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "messaging":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"},
   "message":{"href":"data:text/plain;charset=utf-8,Hi+Tosh%0d%0a"}
   },
   "_embedded":{
   "from":{
   "sourceNetwork":"SameEnterprise",
   "anonymous":false,
   "name":"Lene Aaling",
   "uri":"sip:lenea@contoso.com",
   "_links":{
   "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@contoso.com"},
   "conversation":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
   "contact":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com"},
   "contactPresence":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com/presence"},
   "contactPhoto":{"href":"/ucwa/oauth/v1/applications/104/photos/lenea@contoso.com"}
   },
   "rel":"participant"
   }
   },
   "rel":"messagingInvitation"
   }
   },
   "type":"completed"
   }]
   }]
  }
  ```

16. Process the response from the next GET request on the **events** resource.
 
 The status of the **message** is "Success" and the event type is "completed".
 


 ```
 HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 2927
Date: Thu, 07 Nov 2013 00:41:11 GMT
Content-Type: application/json; charset=utf-8
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/104/events?ack=6"},
 "next":{"href":"/ucwa/oauth/v1/applications/104/events?ack=7"}
 },
 "sender": [{
 "rel":"conversation",
 "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0",
 "events": [{
 "link":{
 "rel":"message",
 "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging/messages/2"
 },
 "status":"Success",
 "_embedded":{
 "message":{
 "direction":"Incoming",
 "timeStamp":"\/Date(1383784870271)\/",
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging/messages/2"},
 "contact":{"href":"/ucwa/oauth/v1/applications/104/people/lenea@contoso.com"},
 "participant":{
 "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/lenea@contoso.com",
 "title":"Lene Aaling"
 },
 "messaging":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"},
 "plainMessage":{"href":"data:text/plain;charset=utf-8,Hi+Tosh%0d%0a"}
 },
 "rel":"message"}
 },
 "type":"completed"
 }]
 },{
 "rel":"communication",
 "href":"/ucwa/oauth/v1/applications/104/communication",
 "events": [{
 "link":{
 "rel":"conversation",
 "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"
 },
 "_embedded":{
 "conversation":{
 "state":"Connected",
 "threadId":"Ac7bUftwUcVMzgTzRl+OQchCsUn35Q==",
 "subject":"",
 "activeModalities": ["Messaging"],
 "importance":"Normal",
 "recording":false,
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0"},
 "applicationSharing":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/applicationSharing"},
 "audioVideo":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/audioVideo"},
 "dataCollaboration":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/dataCollaboration"},
 "messaging":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/messaging"},
 "phoneAudio":{"href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/phoneAudio"},
 "localParticipant":{
 "href":"/ucwa/oauth/v1/applications/104/communication/conversations/eee0/participants/toshm@contoso.com",
 "title":"Tosh Meston"
 },
 "addParticipant":{"href":"/ucwa/oauth/v1/applications/104/communication/participantInvitations?conversation=eee0"}
 },
 "rel":"conversation"
 }
 },
 "type":"updated"
 }]
 }]
}
 ```

At this point, the application has received the incoming message and is ready for further actions, such as responding to the incoming message or terminating the conversation.

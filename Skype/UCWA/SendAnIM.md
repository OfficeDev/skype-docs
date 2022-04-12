---
title: Send an IM for Skype for Business 2015
description: Send an IM to one of your contacts.
---

# Send an IM
Send an IM to one of your contacts.


 _**Applies to:** Skype for Business 2015_

The task presented here describes the steps that are necessary for one user (ToshM@contoso.com) to send an instant message to another user (LeneA@contoso.com).

The steps shown here assume that you have already created an application and have received a response that contains the HREF for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).


1. Send a POST request on the [startMessaging](startMessaging_ref.md) resource, which can be found in the [communication](communication_ref.md) resource embedded in **application**. A POST request on this resource causes a [messagingInvitation](messagingInvitation_ref.md) resource to be created and started in the event channel.
 
 The sample shown here includes a request body with values for **sessionContext**, **to**, **operationID**, and other values. The "to" person is one of the contacts obtained in a previous step.
 
```
POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/102/communication/startmessaging HTTP/1.1
Accept: application/json
Content-Type: application/json
Authorization: Bearer cwt=AAEB...buHc
X-Ms-Origin: http://localhost
X-Requested-With: XMLHttpRequest
Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
Accept-Language: en-us
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)
Host: lyncweb.contoso.com
Content-Length: 201
Connection: Keep-Alive
Cache-Control: no-cache

{
"importance":"Normal",
"sessionContext":"33dc0ef6-0570-4467-bb7e-49fcbea8e944",
"subject":"Task Sample",
"telemetryId":null,
"to":"sip:lenea@contoso.com",
"operationId":"5028e824-2268-4b14-9e59-1abad65ff393"
}
```

2. Process the response from the previous request. If the request is successful, a response code of 201 Created is returned.
 
```
HTTP/1.1 201 Created
Connection: Keep-Alive
Content-Length: 0
Date: Tue, 22 Jan 2013 22:28:12 GMT
Location: /ucwa/oauth/v1/applications/102/communication/messagingInvitations/60c6490148e84972a18ac13d601f29ec
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET
```

3. Send a GET request on the [events](events_ref.md) resource.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/102/events?ack=2 HTTP/1.1
Authorization: Bearer cwt=AAEB...buHc
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

4. Process the response from the previous request. 
 
 The **messagingInvitation** is returned as an embedded resource. Another embedded resource that is the [conversation](conversation_ref.md) resource, which is used in the next step. Other information contained here is that the **conversation** is in the Connecting state.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 6599
Date: Tue, 22 Jan 2013 22:28:13 GMT
Content-Type: application/json
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"_links":{
"self":{
"href":"/ucwa/oauth/v1/applications/102/events?ack=2"
},
"next":{
"href":"/ucwa/oauth/v1/applications/102/events?ack=3"
}
},
"sender": [
{
"rel":"communication",
"href":"/ucwa/oauth/v1/applications/102/communication",
"events": [
{
"link":{
"rel":"messagingInvitation",
"href":"/ucwa/oauth/v1/applications/102/communication/messagingInvitations/60c"
},
"_embedded":{
"messagingInvitation":{
"direction":"Outgoing",
"importance":"Normal",
"threadId":"0c7e9e90916041099ffd46075944f433",
"state":"Connecting",
"operationId":"5028e824-2268-4b14-9e59-1abad65ff393",
"subject":"Task Sample",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/messagingInvitations/60c"},
"from":{
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com",
"title":"Tosh Meston"
},
"to":{"href":"/ucwa/oauth/v1/applications/102/people/lenea@contoso.com"},
"cancel":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/terminate"},
"conversation":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
"messaging":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"}
},
"rel":"messagingInvitation"
}
},
"type":"started"
},
{
"link":{
"rel":"conversation",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"
},
"_embedded":{
"conversation":{
"state":"Connecting",
"threadId":"0c7e9e90916041099ffd46075944f433",
"subject":"Task Sample",
"activeModalities": [],
"importance":"Normal",
"recording":false,
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
.
.
.
"messaging":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"},
"phoneAudio":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/phoneAudio"}
},
"rel":"conversation"
}
},
"type":"added"
}
]
},
{
"rel":"conversation",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1",
"events": [
{
"link":{
"rel":"localParticipant",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com",
"title":"Tosh Meston"
},
"_embedded":{
"localParticipant":{
"sourceNetwork":"SameEnterprise",
"anonymous":false,
"name":"Tosh Meston",
"uri":"sip:toshm@contoso.com",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com"},
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
"conversation":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
"stopMessaging":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/terminate"}
},
"rel":"messaging"
}
},
"type":"updated"
},
{
.
.
.
    
}
]
}
]
}

```

5. Send a GET request on the **events** resource.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/102/events?ack=3 HTTP/1.1
Authorization: Bearer cwt=AAEB...buHc
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

6. Process the response from the previous request.
 
 Here we find that the conversation state has progressed to the **Connected** state, that the **messagingInvitation** status is **Success**, and that LeneA has been added to the **conversation**.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 6674
Date: Tue, 22 Jan 2013 22:28:15 GMT
Content-Type: application/json
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/events?ack=3"},
"next":{"href":"/ucwa/oauth/v1/applications/102/events?ack=4"}
},
"sender": [
{
"rel":"communication",
"href":"/ucwa/oauth/v1/applications/102/communication",
"events": [
{
"link":{
"rel":"conversation","href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"
},
"_embedded":{
"conversation":{
"state":"Connected",
"threadId":"0c7e9e90916041099ffd46075944f433",
"subject":"Task Sample",
"activeModalities": ["Messaging"],
"importance":"Normal",
"recording":false,
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
.
.
.
"messaging":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"},
"phoneAudio":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/phoneAudio"},
"localParticipant":{
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com",
"title":"Tosh Meston"
},
"addParticipant":{"href":"/ucwa/oauth/v1/applications/102/communication/participantInvitations?conversation=21a1"}
},
"rel":"conversation"
}
},
"type":"updated"
}
]
},
{
"rel":"conversation",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"'
"events": [
{
"link":{
"rel":"participant",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/lenea@contoso.com", "title":""
},
"type":"added"
},
{
"link":{
"rel":"participantMessaging",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/lenea@contoso.com/messaging"
},
"in":{
"rel":"participant",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/lenea@contoso.com",
"title":""
},
"type":"added"
},
{
"link":{
"rel":"participantMessaging",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com/messaging"
},
"in":{
"rel":"localParticipant",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com",
"title":"Tosh Meston"
},
"_embedded":{
"participantMessaging":{
"_links":{
"self":{
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com/messaging"
},
"participant":{
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com",
"title":"Tosh Meston"
}
},
"rel":"participantMessaging"
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
"state":"Connected",
"negotiatedMessageFormats": ["Plain"],
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"},
"conversation":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
"stopMessaging":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/terminate"},
"sendMessage":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/messages"},
"setIsTyping":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/typing"},
"typingParticipants":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/typingParticipants"}
},
"rel":"messaging"
}
},
"type":"updated"
}
]
},
{
"rel":"communication",
"href":"/ucwa/oauth/v1/applications/102/communication",
"events": [
{
"link":{
"rel":"messagingInvitation",
"href":"/ucwa/oauth/v1/applications/102/communication/messagingInvitations/60c"
},
"status":"Success",
"_embedded":{
"messagingInvitation":{
"direction":"Outgoing",
"importance":"Normal",
"threadId":"0c7e9e90916041099ffd46075944f433",
"state":"Connected",
"operationId":"5028e824-2268-4b14-9e59-1abad65ff393",
"subject":"Task Sample",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/messagingInvitations/60c"},
"from":{
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com",
"title":"Tosh Meston"
},
"to":{"href":"/ucwa/oauth/v1/applications/102/people/lenea@contoso.com"},
"conversation":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
"messaging":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"}
},
"_embedded":{
"acceptedByParticipant": [
{
"sourceNetwork":"SameEnterprise",
"anonymous":false,
"name":"",
"uri":"sip:lenea@contoso.com",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/lenea@contoso.com"},
"conversation":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
"contact":{"href":"/ucwa/oauth/v1/applications/102/people/lenea@contoso.com"},
"contactPresence":{"href":"/ucwa/oauth/v1/applications/102/people/lenea@contoso.com/presence"},
"contactPhoto":{"href":"/ucwa/oauth/v1/applications/102/photos/lenea@contoso.com"}
},
"rel":"participant"
}
]
},
"rel":"messagingInvitation"
}
},
"type":"completed"
}
]
}
]
}

```

7. Send a GET request on the **conversation** resource.
 
 The response from this request provides information about the conversation, as well as links to other capabilities.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/102/communication/conversations/21a1 HTTP/1.1
Authorization: Bearer cwt=AAEB...buHc
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

8. Process the response from the previous request.
 
 Now that the **conversation** is in the **Connected** state, the response from the previous GET request provides links that can enable the application begin the process of adding modalities or adding new participants.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 1398
Date: Tue, 22 Jan 2013 22:28:15 GMT
Content-Type: application/json; charset=utf-8
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"state":"Connected",
"threadId":"0c7e9e90916041099ffd46075944f433",
"subject":"Task Sample",
"activeModalities": ["Messaging"],
"importance":"Normal",
"recording":false,
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1"},
.
.
.
"messaging":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"},
"phoneAudio":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/phoneAudio"},
"localParticipant":{
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/participants/toshm@contoso.com",
"title":"Tosh Meston"
},
"addParticipant":{"href":"/ucwa/oauth/v1/applications/102/communication/participantInvitations?conversation=21a1"}
},
"rel":"conversation"
}
```

9. Send a POST request on the [sendMessage](sendMessage_ref.md) resource embedded in the **conversation**.
 
 In this step the text of the instant message ("What's up?") is sent as the body of the POST request.
 
```
POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/messages?OperationContext=6b6ce55c-d4b9-4303-a61f-5813ecb2d7b1 HTTP/1.1
Accept: application/json
Content-Type: text/plain
Authorization: Bearer cwt=AAEB...buHc
X-Ms-Origin: http://localhost
X-Requested-With: XMLHttpRequest
Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
Accept-Language: en-us
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)
Host: lyncweb.contoso.com
Content-Length: 10
Connection: Keep-Alive
Cache-Control: no-cache

What's up?
```

10. Process the response from the previous request.
 
```
HTTP/1.1 201 Created
Connection: Keep-Alive
Content-Length: 0
Date: Tue, 22 Jan 2013 22:28:23 GMT
Location: /ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/messages/2
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET
```

11. Send a GET request on the **events** resource.
 
```
GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/102/events?ack=6 HTTP/1.1
Authorization: Bearer cwt=AAEB ...buHc
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

12. Process the response from the previous request. This can contain information, such as any messages sent by your application, messages sent by other participants, the addition of modalities, and the addition of participants.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 2996
Date: Tue, 22 Jan 2013 22:28:29 GMT
Content-Type: application/json
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/events?ack=6"},
"next":{"href":"/ucwa/oauth/v1/applications/102/events?ack=7"}
},
"sender": [
{
"rel":"conversation",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1",
"events": [
{
"link":{
"rel":"message",
"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/messages/2"
},
"status":"Success",
"_embedded":{
"message":{
"direction":"Outgoing",
"timeStamp":"\/Date(1364327898968)\/",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/messages/2"},
"messaging":{"href":"/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging"}
},
"rel":"message"
}
},
"type":"completed"
}]
}]
}
```

13. Send a POST request on the [stopMessaging](stopMessaging_ref.md) href.
 
 Sending a POST request on the **stopMessaging** href terminates the conversation.
 
```
POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/102/communication/conversations/21a1/messaging/terminate HTTP/1.1
Authorization: Bearer cwt=AAEB...buHc
Accept: application/json
X-Ms-Origin: http://localhost
X-Requested-With: XMLHttpRequest
Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
Accept-Language: en-us
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)
Host: lyncweb.contoso.com
Content-Length: 0
Connection: Keep-Alive
Cache-Control: no-cache

```

14. Process the response from the previous request.
 
```
HTTP/1.1 204 No Content
Connection: Keep-Alive
Date: Tue, 22 Jan 2013 22:28:28 GMT
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

```


---
title: SignalingSession
TOCTitle: SignalingSession
ms:assetid: 9ecca9a4-af5b-4df8-9e4d-41d20df8f8ec
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466048(v=office.16)
ms:contentKeyID: 65239988
ms.date: 07/27/2015
mtps_version: v=office.16
---

# SignalingSession


_**Applies to:** Skype for Business 2015_

**In this article**  
Signaling session properties  
Signaling session participants  
Signaling session events  
Initiating and accepting a signaling session  
Sending and receiving messages inside a signaling session  
Negotiating media capabilities in a signaling session  
Declining and leaving a signaling session  
Redirecting a signaling session to another target  
Referring another participant to the remote participant  

A signaling session provides the control channel in which one endpoint can invite another endpoint to participate in some activity or exchange media descriptions required to establish media communication. Two endpoints can use an established signaling session to exchange short control messages as well as text messages between each other. The supported message types are enumerated in the [MessageType](https://msdn.microsoft.com/en-us/library/hh349721\(v=office.16\)) enumerated type. The endpoints can also renegotiate the media description anytime in a session. Normally, media is described using Session Description Protocol (SDP) as defined in RFC 2327. However, other payload types can be used in a signaling session as well.

## Signaling session properties

In UCMA 5.0, a signaling session is modeled by the [SignalingSession](https://msdn.microsoft.com/en-us/library/hh365691\(v=office.16\)) class. A signaling session consists of a local participant ([LocalParticipant](https://msdn.microsoft.com/en-us/library/hh161716\(v=office.16\))) and a remote participant ([RemoteParticipant](https://msdn.microsoft.com/en-us/library/hh366401\(v=office.16\))).

A session is uniquely identified by its **Call-ID** and **From** and **To** headers, which correspond to the [CallId](https://msdn.microsoft.com/en-us/library/hh350259\(v=office.16\)), [FromTag](https://msdn.microsoft.com/en-us/library/hh350335\(v=office.16\)), and [ToTag](https://msdn.microsoft.com/en-us/library/hh384492\(v=office.16\)) properties on the **SignalingSession** class. **CallId** is generated when the session is created. **FromTag** is assigned when the INVITE request is sent out. **ToTag** is added by the remote participant when responding to the INVITE request.

A signaling session can be in any of the session states as enumerated by the [SignalingState](https://msdn.microsoft.com/en-us/library/hh350238\(v=office.16\)) enumerated type. An application can examine the state of a session reading the [State](https://msdn.microsoft.com/en-us/library/hh383605\(v=office.16\)) property of the session.

## Signaling session participants

Associated with a signaling session are two signaling participants, a local participant and a remote participant. A signaling participant is an instance of the [SignalingParticipant](https://msdn.microsoft.com/en-us/library/hh382394\(v=office.16\)) class. This object contains information about a participant, including the URI of the participant ([Uri](https://msdn.microsoft.com/en-us/library/hh382312\(v=office.16\))), the endpoint ID, which the participant uses to participate in a signaling session ([EndpointId](https://msdn.microsoft.com/en-us/library/hh383428\(v=office.16\))), and the display name of the participant ([Name](https://msdn.microsoft.com/en-us/library/hh350217\(v=office.16\))). In addition, a signaling participant object raises events when the display name or the endpoint ID is modified.

## Signaling session events

A signaling session supports the following events.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Event</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh365978(v=office.16)">InvitationAccepted</a></p></td>
<td><p>Raised when the remote participant has accepted the invitation. This typically happens when a successful response is received</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382639(v=office.16)">InvitationCompleted</a></p></td>
<td><p>Raised when the invitation process completes. This typically happens when the inviter acknowledges a successful response.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349494(v=office.16)">MessageReceived</a></p></td>
<td><p>Raised when a message is received.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh365709(v=office.16)">ProvisionalResponseReceived</a></p></td>
<td><p>Raised when the session receives any provisional response (response codes 101 through 199).</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh383727(v=office.16)">Redirecting</a></p></td>
<td><p>Raised when a redirect request is received.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh365689(v=office.16)">ReferReceived</a></p></td>
<td><p>Raised when a new REFER request is received.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382748(v=office.16)">ReInviteReceived</a></p></td>
<td><p>Raised when the remote participant sends a Re-INVITE message.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh365710(v=office.16)">RenegotiationToRefreshNeeded</a></p></td>
<td><p>Raised when the session is enabled for a session timer, the local endpoint is the refresher, and the remote does not support the UPDATE method. The application is responsible for calling <a href="https://msdn.microsoft.com/en-us/library/hh382847(v=office.16)">BeginRenegotiateDescription()</a> to keep the session alive.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh161893(v=office.16)">RequestOrResponseReceived</a></p></td>
<td><p>Raised when a request or response is received.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382697(v=office.16)">RouteSetStatusChanged</a></p></td>
<td><p>Raised when the state of the route set of the session changes.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh161900(v=office.16)">StateChanged</a></p></td>
<td><p>Raised when the session state changes.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349400(v=office.16)">TerminatedByRemote</a></p></td>
<td><p>Raised when the remote side terminates the session by sending a Bye or Cancel message. This event is useful only when the application intends to access the headers in the Bye or Cancel message for custom scenarios.</p></td>
</tr>
</tbody>
</table>


An application must implement appropriate event handlers and register them with a newly created **SignalingSession** object if it is interested in receiving the notification and handling any of the events.

## Initiating and accepting a signaling session

For each endpoint, a signaling session starts when an invitation is accepted and ends when a termination request is sent. In UCMA 5.0, an outgoing signaling session is started for an endpoint when a **SignalingSession** object is created. The invitation to join the session is sent to the remote participant (chosen when the signaling session is created) by a call to one of the overloaded [BeginEstablish()](https://msdn.microsoft.com/en-us/library/hh349457\(v=office.16\)) methods. For an incoming session, an endpoint accepts the session invitation by calling **BeginEstablish** when the session is received.

The invitation is inherently asynchronous and results in one or more responses from the remote participant. These responses trigger changes to the signaling session state.

In the Multiple Points of Presence (MPOP) situation, it is possible for an invitation to arrive at all of the endpoints of the remote participant. The signaling session will process provisional responses from all of these endpoints. However, only one successful response is used for establishing the session. After the session is established with one remote endpoint, none of the other remote endpoints can join the session. The sender can also send the invitation to a specific endpoint of the remote participant by specifying a specific endpoint ID, which can be obtained from another signaling session or a subscription session.

## Sending and receiving messages inside a signaling session

An endpoint can use a signaling session to send messages to the remote participant or to receive messages from the remote participant. An application does so by calling one of the overloaded [BeginSendMessage()](https://msdn.microsoft.com/en-us/library/hh382054\(v=office.16\)) methods. The messages can be of type **Message** or of the other enumerated message types defined in the [MessageType](https://msdn.microsoft.com/en-us/library/hh349721\(v=office.16\)) enumeration.

To receive messages, an application must subscribe to receive and handle the [MessageReceived](https://msdn.microsoft.com/en-us/library/hh349494\(v=office.16\)) event.

Although Page-Mode Messaging can be used for exchanging messages between endpoints, session-based messaging has the advantage of less overhead because the connection or connection information is cached. Messaging in a session is therefore preferred when message exchange takes place in a form of conversation.

### Signaling header

Many methods in UCMA 5.0, including [BeginSendMessage(MessageType, ContentType, \[\], IEnumerable\<SignalingHeader\>, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh383989\(v=office.16\)), enable you to specify custom headers to be sent along as part of a SIP message. A custom header is specified using a **SignalingHeader** object and consists of a name-value pair. Custom headers are interpreted only by applications. The UCMA platform uses several custom headers to provide platform-specific functionality.

## Negotiating media capabilities in a signaling session

Media negotiation refers to the exchange of media description that is transmitted as the payload of SIP messages. Participants use media description to describe their preferred media settings or acceptable media capabilities. Media negotiation involves one party making an offer and another presenting the answer. For example, when one participant invites another to a video chat, the inviting participant makes an offer specifying both incoming and outgoing audio/video feeds at a certain rate. The invited party might not be able to stream video, because there is no video camera installed on the device used by the invited participant. The invited participant might not be able to receive high-fidelity sound because the audio device is of a lower grade. In the answer, the invited party can present a counter offer specifying the acceptable media capabilities. Media is often described using Session Description Protocol (SDP). However, an application can use other protocols, including custom protocols.

Media negotiation takes place during the process of establishing a signaling session. When one participant invites another into a session, the inviting participant sends an INVITE message to the invited participant. To accept the invitation, the invited participant sends a successful response (200 OK) back to the inviter. Upon receiving the response, the inviter sends an ACK message to the invited participant to acknowledge that the session has been established.

The inviter can include the media description (the offer) as the body of the INVITE message. The recipient of the invitation then examines the offer and decides to accept, modify, or reject the offer. If the offer is not rejected, the invited participant prepares the answer to be accepted or modified, and expects this answer to be sent back with the 200 OK response from the inviter. Upon receiving the response, the session inviter might change media settings to match the received answer.

Another possibility is that the inviter can send an INVITE request with an empty offer (known as a null offer). When this happens, the invited participant must make an offer and send it back in the 200 OK response. The inviter must then examine the offer, prepare an answer, and send the answer to the invited participant as part of the ACK message. Upon receiving the ACK message, the invited participant must then change media settings according to the received answer.

Media description can also be renegotiated at any time during a signaling session, and such media renegotiation takes place when a Re-INVITE message is processed. The media description is offered and answered in the same way as with an INVITE message. However, either participant can initiate the renegotiation, and the initial offer cannot be null.

In UCMA 5.0, media description is represented by the [ContentDescription](https://msdn.microsoft.com/en-us/library/hh382823\(v=office.16\)) class, which specifies the content type and a message body. Applications can extend this class to represent other media types. Media negotiation involves concerted efforts between an application and the platform: the platform is responsible for delivering the offers and answers, whereas the application is responsible for parsing the offer and providing the answer. To coordinate this effort, UCMA 5.0 exposes the [IOfferAnswer](https://msdn.microsoft.com/en-us/library/hh348293\(v=office.16\)) interface as the contract between the platform and applications for handling media negotiation. A UCMA 5.0 application that intends to perform media negotiation must honor the contract by implementing the interface to prepare an offer or answer to be sent by the platform. Such applications must set the **IOfferAnswer** implementation as the value of the [OfferAnswerNegotiation](https://msdn.microsoft.com/en-us/library/hh349556\(v=office.16\)) property. Applications not interested in media negotiation can set this property to null.

## Declining and leaving a signaling session

To decline an incoming session, an application calls the [TerminateWithRejection(Int32, String, IEnumerable\<SignalingHeader\>)](https://msdn.microsoft.com/en-us/library/hh381712\(v=office.16\)) method on a **SignalingSession** object. When the session is declined, it is no longer available for any use, because the resources have been freed, although some properties may still be accessible until the object is garbage-collected.

To leave an existing session, the application can call one of the overloaded [BeginTerminate()](https://msdn.microsoft.com/en-us/library/hh385159\(v=office.16\)) methods at any time. This results in UCMA 5.0 sending a BYE or CANCEL message, depending on the session state, and then disconnecting the session. Some properties of the session might still be accessible after **BeginTerminate** is called.

## Redirecting a signaling session to another target

When an invitation to join an incoming session arrives at an endpoint, an application can redirect the request to one or more other targets by calling one of the overloaded **TerminateWithRedirection** methods on the **SignalingSession** object. If the target is a single endpoint, the real-time address is used as the method parameter. Multiple-endpoint targets require a collection of endpoints to be supplied in the overloaded method that takes a collection of endpoints as a parameter.

## Referring another participant to the remote participant

A local endpoint in a signaling session can refer another participant to the remote endpoint. Currently, you can refer to a third endpoint inside an existing dialog.


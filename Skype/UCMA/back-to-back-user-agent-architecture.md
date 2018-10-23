---
title: Back-to-back user agent architecture
TOCTitle: Back-to-back user agent architecture
ms:assetid: 433baf9b-d4c3-441d-a5fb-9f4010cc0f21
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465993(v=office.16)
ms:contentKeyID: 65239936
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Back-to-back user agent architecture

**Applies to**: Skype for Business 2015

The architectural framework behind the back-to-back user agent feature is based on interactions between the  [Call](https://msdn.microsoft.com/en-us/library/hh384235\(v=office.16\)) and [MediaProvider](https://msdn.microsoft.com/en-us/library/hh383767\(v=office.16\)) abstract classes. Both call legs are associated with the same **BackToBackMediaProvider** instance.

> [!NOTE]
> The **BackToBackMediaProvider** class is internal.

Signaling layer messages are exchanged at the **Call** level, while Session Description Protocol (SDP) exchange is carried out at the **MediaProvider** level. The **BackToBackCall** class provides the necessary infrastructure to combine Session Initiation Protocol (SIP) and SDP exchange.

Signaling-related messages are sent back-to-back between the Call objects. SDP offer/answer messages are sent back-to-back using media provider methods.

When messages are sent back-to-back across Calls, signaling headers that are configured using [SetPassThroughHeaderNames](https://msdn.microsoft.com/en-us/library/hh384137\(v=office.16\)) are copied from one call leg to the others.

## Header passing across call legs

A back-to-back user agent is not a proxy. [RFC-3725](https://www.rfc-editor.org/rfc/rfc3725.txt) (Best Current Practices for Third Party Call Control (3pcc) in the Session Initiation Protocol (SIP)) does not define how signaling headers should be passed across the two call legs associated with a back-to-back user agent.

> [!NOTE]
> RFC-3725 applies primarily to call-establish time.

The header fields shown in the following list are not passed through from one call leg to the other (independent of the previously listed points):

- Call-ID
- Contact
- Content-Type
- CSeq
- From
- History-Info
- Max-Forwards
- Ms-Conversation-ID
- P-Asserted-Identity
- P-Preferred-Identity
- Record-Route
- Refer-To
- Referred-By
- Replaces
- RSeq
- Target-Class
- To
- Via

Allow and Supported header fields are parsed for header values that are added by the platform’s signaling layer. Any such header values are reused on the outbound transaction.

## Message exchange on a BackToBackCall

A **BackToBackCall** instance performs back-to-back message transmission as follows.

> [!NOTE]
> The UCMA 5.0 implementation does not permit an application to control individual message exchange behavior on an Incoming-Idle call (that is, when the [State](https://msdn.microsoft.com/en-us/library/hh381151(v=office.16)) property on the first call leg is **Incoming**, and the same property on the second call leg is **Idle**). For more information, see [CallState](https://msdn.microsoft.com/en-us/library/hh366023(v=office.16)).

- Pass-through headers (except the restricted headers listed in the previous section) received on an incoming call are sent to the outgoing call.

- SDP MIME parts received in an offer on an incoming INVITE message appear in the Offer header on the outgoing call. Custom MIME parts must be set by the application in Accept options.

- Non-reliable provisional responses received on an outgoing call are sent to the caller intact. No content body on non-reliable provisional responses is expected.

- All reliable provisional responses (response code 183) that create early dialogs are returned to the incoming call with modified To tags, indicating a separate fork. SDP Answer responses received on 183 responses are passed unchanged. No other MIME parts are expected on 183 responses.

- All messages received and responses to messages on one call leg are pushed to the other call leg unchanged with all pass-through headers (excluding restricted headers) and body.


## Call leg state and BackToBackCall.State

The value of the [State](https://msdn.microsoft.com/en-us/library/hh383563\(v=office.16\)) property on a **\[BackToBackCall\]** instance is calculated from the [State](https://msdn.microsoft.com/en-us/library/hh381151\(v=office.16\)) on each call leg. The following table summarizes the relationships of these properties.

<table>
<colgroup>
<col style="width: 33%" />
<col style="width: 33%" />
<col style="width: 33%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Call leg 1 state</p></th>
<th><p>Call leg 2 state</p></th>
<th><p>BackToBackCall state</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Idle</p></td>
<td><p>Idle</p></td>
<td><p>Idle</p></td>
</tr>
<tr class="even">
<td><p>Incoming</p></td>
<td><p>Idle</p></td>
<td><p>Idle</p></td>
</tr>
<tr class="odd">
<td><p>Incoming</p></td>
<td><p>Establishing</p></td>
<td><p>Establishing</p></td>
</tr>
<tr class="even">
<td><p>Establishing</p></td>
<td><p>Establishing</p></td>
<td><p>Establishing</p></td>
</tr>
<tr class="odd">
<td><p>Established</p></td>
<td><p>Establishing</p></td>
<td><p>Establishing</p></td>
</tr>
<tr class="even">
<td><p>Establishing</p></td>
<td><p>Established</p></td>
<td><p>Establishing</p></td>
</tr>
<tr class="odd">
<td><p>Established</p></td>
<td><p>Established</p></td>
<td><p>Established</p></td>
</tr>
<tr class="even">
<td><p>Established</p></td>
<td><p>Terminating</p></td>
<td><p>Terminating</p></td>
</tr>
<tr class="odd">
<td><p>Terminating</p></td>
<td><p>Terminating</p></td>
<td><p>Terminating</p></td>
</tr>
<tr class="even">
<td><p>Terminated</p></td>
<td><p>Terminating</p></td>
<td><p>Terminating</p></td>
</tr>
<tr class="odd">
<td><p>Terminating</p></td>
<td><p>Terminated</p></td>
<td><p>Terminating</p></td>
</tr>
<tr class="even">
<td><p>Terminated</p></td>
<td><p>Terminated</p></td>
<td><p>Terminated</p></td>
</tr>
</tbody>
</table>


## See also

- [Video demo - Lync 2013: How to do back-to-back calls using UCMA 4](https://channel9.msdn.com/posts/lync-2013-how-to-do-back-to-back-calls-using-ucma-4)


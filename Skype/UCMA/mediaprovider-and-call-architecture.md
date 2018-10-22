---
title: MediaProvider and Call architecture
TOCTitle: MediaProvider and Call architecture
ms:assetid: e9fa4c2e-afd4-45f1-95a8-995a3871f05d
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466101(v=office.16)
ms:contentKeyID: 65240020
ms.date: 07/27/2015
mtps_version: v=office.16
---

# MediaProvider and Call architecture


**Applies to**: Skype for Business 2015



The [MediaProvider](https://msdn.microsoft.com/en-us/library/hh383767\(v=office.16\)) abstract class is intended to provide an abstraction of the link between the Signaling layer and Session Description Protocol (SDP) negotiation. A **MediaProvider** subclass facilitates offer/answer negotiation in a signaling session when an application initiates an outgoing call or accepts an incoming call. The [Call](https://msdn.microsoft.com/en-us/library/hh384235\(v=office.16\)) abstract class is intended to provide an abstraction of a call that uses a particular media type. A **Call** subclass instance invokes methods on the associated **MediaProvider** subclass instance to get an offer or answer, or to set an answer on the associated **MediaProvider** subclass instance.

If your application is intended to work with media types other than the standard media types built into UCMA 5.0, message, audio, and image, you will need to create non-abstract classes that inherit from the **MediaProvider** and **Call** classes. This topic describes the how the **MediaProvider** and **Call** subclass instances must interact so that your application operates successfully.

For the remainder of this topic, any mentions of **MediaProvider** or **Call** should be understood to mean developer-implemented subclasses of the **MediaProvider** and **Call** classes.

## MediaProvider and Call interaction

A developer who implements a non-abstract class such as **AudioVideoMediaProvider** should be aware of the following important virtual **MediaProvider** properties and methods. Only the most important properties and methods on **MediaProvider** are discussed.

### Properties that define MediaProvider-supported behavior

The following table lists the **MediaProvider** properties that define the behavior supported by a particular **MediaProvider** implementation. The only property that must be implemented in a class derived from the **MediaProvider** class is **SupportedMediaTypes**. This property should return the media names supported by the class derived from **MediaProvider**. All other properties can be overridden as needed to support a particular capability.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Property</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><strong>SupportedMediaTypes</strong></p></td>
<td><p>Gets a list of the supported media names that are acceptable in an m= line in an SDP offer/answer.</p></td>
</tr>
<tr class="even">
<td><p><strong>IsMcuSupported</strong></p></td>
<td><p>Gets or sets whether the media provider supports behaving as an MCU.</p></td>
</tr>
<tr class="odd">
<td><p><strong>IsEscalationSupported</strong></p></td>
<td><p>Gets or sets whether the media provider supports the escalation from a two-party call to conferencing.</p></td>
</tr>
<tr class="even">
<td><p><strong>IsEarlyMediaPreferred</strong></p></td>
<td><p>Gets or sets whether incoming call must perform early media renegotiation before sending a 200-OK response.</p></td>
</tr>
</tbody>
</table>


### Methods that handle SDP offer/answer negotiation

A key role of a class derived from the **MediaProvider** class is to participate in SDP offer/answer negotiation for a **Call**. A typical **MediaProvider** subclass, such as the **AudioVideoProvider** class, also manages the [MediaFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaflow?view=ucma-api) instance (media session) associated with the **Call**. This section describes the most important **MediaProvider** methods related to offer/answer negotiation.


> [!NOTE]
> <P>A developer who is implementing a <STRONG>MediaProvider</STRONG> subclass should be familiar with the standard .NET Framework asynchronous pattern that uses <STRONG>Begin</STRONG><EM>Xxx</EM> and <STRONG>End</STRONG><EM>Xxx</EM> for asynchronous operations. The <STRONG>MediaProvider</STRONG> subclass requires implementations of the abstract <STRONG>BeginGetOffer</STRONG> and <STRONG>BeginGetAnswer</STRONG> methods.</P>



<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Method</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh366051(v=office.16)">BeginGetOffer(OfferAnswerContext, Collection&lt;SdpContentDescription&gt;, AsyncCallback, Object)</a></p></td>
<td><p>Initiates an operation to get the initial offer from the <strong>MediaProvider</strong> implementation.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382852(v=office.16)">EndGetOffer(IAsyncResult)</a></p></td>
<td><p>Completes the operation started by <strong>BeginGetOffer</strong>.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384479(v=office.16)">BeginGetAnswer(OfferAnswerContext, SdpOffer, AsyncCallback, Object)</a></p></td>
<td><p>Initiates an operation to get an answer from the <strong>MediaProvider</strong> implementation for the incoming offer.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh383856(v=office.16)">EndGetAnswer(IAsyncResult)</a></p></td>
<td><p>Completes the operation started by <strong>BeginGetAnswer</strong>.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382509(v=office.16)">SetAnswer(OfferAnswerContext, SdpOffer, SdpAnswer)</a></p></td>
<td><p>Sets the incoming answer on the <strong>MediaProvider</strong> implementation.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh366290(v=office.16)">SetIncomingCallOffer(OfferAnswerContext, SdpOffer)</a></p></td>
<td><p>Sets the initial offer on the <strong>MediaProvider</strong> without requesting an answer.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384458(v=office.16)">BeginSdpRenegotiation(CallDialogContext, AsyncCallback, Object)</a></p></td>
<td><p>Begins SDP renegotiation for a given <strong>CallDialogContext</strong> value.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh350151(v=office.16)">EndSdpRenegotiation(IAsyncResult)</a></p></td>
<td><p>Ends the asynchronous operation started by the <strong>BeginSdpRenegotiation</strong> method.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh350168(v=office.16)">RaiseFlowConfigurationRequestedHandler(OfferAnswerContext, FlowConfigurationRequestedEventArgs)</a></p></td>
<td><p>Raises the <strong>FlowConfigurationRequested</strong> event.</p></td>
</tr>
</tbody>
</table>


### Methods that control the signaling session and the media session

The abstract methods shown in the following table must be implemented in a class derived from the **MediaProvider** class. These methods are used to terminate the signaling session, which is associated with signaling information and instant message text, and the media session, which is associated with audio.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Method</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384611(v=office.16)">BeginTerminateCall(CallDialogContext, IEnumerable&lt;SignalingHeader&gt;, AsyncCallback, Object)</a></p></td>
<td><p>Begins an asynchronous operation to terminate the associated call.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh383675(v=office.16)">EndTerminateCall(IAsyncResult)</a></p></td>
<td><p>Completes the asynchronous operation started by <strong>BeginTerminateCall</strong>.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh350188(v=office.16)">BeginTerminateMedia(CallDialogContext, Boolean, AsyncCallback, Object)</a></p></td>
<td><p>Begins an asynchronous operation to terminate media session.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349351(v=office.16)">EndTerminateMedia(IAsyncResult)</a></p></td>
<td><p>Completes the asynchronous operation started by <strong>BeginTerminateMedia</strong>.</p></td>
</tr>
</tbody>
</table>


### Successful outgoing SDP negotiation or renegotiation

The scenario described here is one in which SDP negotiation is successful. This scenario begins when the **Call** instance starts a new outgoing SDP negotiation or renegotiation.

1.  The **MediaProvider** instance starts the asynchronous process of generating a new SDP offer by calling its **BeginGetOffer** method.

2.  When the SDP offer is ready, the **MediaProvider** instance completes the asynchronous operation by invoking the callback method represented by the **BeginGetOffer** method’s **AsyncCallback** parameter.

3.  The **Call** instance invokes **EndGetOffer** and expects the **MediaProvider** instance to return the SDP offer.

4.  When the **Call** instance receives the SDP offer from the remote end, it calls the **MediaProvider**’s **SetOffer** method.

5.  For a new **Call** instance, the application calls **BeginEstablish** on the **Call** instance to start an outgoing SDP renegotiation. This is the typical scenario.
    
    For a **Call** instance that is already established, the **MediaProvider** instance calls its **BeginSdpRenegotiation** method.

For information about error cases and declining an incoming negotiation request, see [Negotiating declined offers](negotiating-declined-offers.md).

### Successful incoming SDP renegotiation

This scenario describes the steps involved in handling an incoming SDP renegotiation.

1.  The **MediaProvider** instance starts an asynchronous operation to generate the SDP answer by calling its **BeginGetAnswer** method.

2.  When the SDP answer is ready, the **MediaProvider** instance invokes the callback method represented by the **BeginGetAnswer** method’s **AsyncCallback** parameter.

3.  The **Call** instance invokes **EndGetAnswer** and expects the **MediaProvider** instance to return the SDP answer. The **SdpAnswer** object returned by **EndGetOffer** is used in response to an incoming negotiation request.


Whenever a **MediaProvider** subclass object, such as an **AudioVideoMediaProvider** instance, intends to renegotiate SDP, it calls the [BeginSdpRenegotiation(CallDialogContext, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh384458\(v=office.16\)) method to notify the associated non-abstract **Call** subclass instance to start SDP renegotiation. Any collision of renegotiation is handled by the **Call** instance.

Whenever the **Call** object receives a message in an existing session and the message is not to the **Call** object itself, the message is delivered to the **MediaProvider** instance by a call to **HandleMessageReceived(MessageReceivedEventArgs)**. If the **MediaProvider** instance takes no action on the message, it is forwarded to the application. If a delegate for the application also takes no action on the message, the **Call** instance rejects the message. The **AudioVideoProvider** and **InstantMessagingProvider** implementations are internal, but are tied into the platform architecture as described in the following diagram.


> [!NOTE]
> <P>The numbers at the ends of the horizontal arrows indicate the number of objects of one type that can be paired with the other type. For example, a <A href="https://msdn.microsoft.com/en-us/library/hh349224(v=office.16)">Conversation</A> instance can be associated with multiple <STRONG>Call</STRONG> instances.</P>



![AudioVideoProvider and InstantMessagingProvider](images/Dn466101.Call_MediaProvider(Office.16).png "AudioVideoProvider and InstantMessagingProvider")

In addition to simple SDP negotiation for simple two-party calls, the **MediaProvider** instance also provides a mechanism for escalating a two-party call to multiparty conferencing. For information about escalating a two-party call to a conference, see [Escalating to a Conference](https://msdn.microsoft.com/en-us/library/dd279769\(v=office.16\)).

## SdpContentDescription and offer/answer

The constructors for the [SdpOffer](https://msdn.microsoft.com/en-us/library/hh350150\(v=office.16\)) and [SdpAnswer](https://msdn.microsoft.com/en-us/library/hh349319\(v=office.16\)) classes all have parameters of type [SdpContentDescription](https://msdn.microsoft.com/en-us/library/hh385010\(v=office.16\)), a class that inherits from the [ContentDescription](https://msdn.microsoft.com/en-us/library/hh382823\(v=office.16\)) class. The **ContentDescription** class contains the [ContentType](https://msdn.microsoft.com/en-us/library/hh161892\(v=office.16\)) property and a [GetBody()](https://msdn.microsoft.com/en-us/library/hh349134\(v=office.16\)) method, which returns the content body. An important distinction between **SdpOffer** and **SdpAnswer** is that an **SdpOffer** instance can contain multiple SDP offers, while an **SdpAnswer** instance can contain only one SDP answer.

An **SdpOffer** instance contains a list of offers (the [Offers](https://msdn.microsoft.com/en-us/library/hh381830\(v=office.16\)) property) that have been received or are being sent out.

Incoming offers can be accepted as multiple offers in a multipart/mime message body or as a single offer in message with SDP content type. If SDP offers are present within the multipart/mime section, the **Call** instance extracts them from the multipart/mime content and presents the list to the **MediaProvider** instance. If the offer’s **ContentType** is "sdp" there should be only one offer in the **Offers** list. The [MessageHeaders](https://msdn.microsoft.com/en-us/library/hh385218\(v=office.16\)) property on the **SdpOffer** instance should contain a list of incoming headers received on the offer.

For an outgoing offer, if there is only one offer in the list, the offer should be sent out with **ContentType** set to "sdp"; otherwise a multipart/mime offer should be created from the list of offers. The **MessageHeaders** property on the **SdpOffer** instance should contain the list of additional SIP headers that will be included as message headers with the offer message.

For an outgoing answer, the [Answer](https://msdn.microsoft.com/en-us/library/hh382015\(v=office.16\)) property on the **SdpAnswer** instance represents the answer that has been received. The [MessageHeaders](https://msdn.microsoft.com/en-us/library/hh385315\(v=office.16\)) property on the **SdpAnswer** instance contains the list of additional SIP headers that the **MediaProvider** instance needs to send out with the answer.

The following figure illustrates the relationship between **SdpAnswer**, **SdpOffer**, and **SdpContentDescription** objects.

![Relationship between Sdp objects](images/Dn466101.SDPOfferAnswer(Office.16).png "Relationship between Sdp objects")

## In this section

  - [Negotiating declined offers](negotiating-declined-offers.md)

  - [Triggering SDP renegotiations](triggering-sdp-renegotiations.md)

  - [Call forking with early media](call-forking-with-early-media.md)

  - [Error handling](error-handling.md)


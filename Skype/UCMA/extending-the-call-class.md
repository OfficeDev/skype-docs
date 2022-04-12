---
title: Extending the Call class
TOCTitle: Extending the Call class
ms:assetid: bdbca8eb-0809-42a2-8cb1-c4d58fe796af
description: By default, UCMA 5.0 provides call controls for the audio and message media types. These call controls are, respectively, the AudioVideoCall and InstantMessagingCall classes.
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466098(v=office.16)
ms:contentKeyID: 65240018
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Extending the Call class


**Applies to**: Skype for Business 2015



By default, UCMA 5.0 provides call controls for the audio and message media types. These call controls are, respectively, the [AudioVideoCall](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api&preserve-view=true) and [InstantMessagingCall](/library/hh161841\(v=office.16\)) classes. A developer can extend the UCMA 5.0 platform by creating a custom call control for an additional media type. Adding a custom call control entails creating new classes that inherit from the [Call](/dotnet/api/microsoft.rtc.collaboration.call?view=ucma-api&preserve-view=true), [MediaProvider](/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaprovider?view=ucma-api&preserve-view=true), and [MediaFlow](/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaflow?view=ucma-api&preserve-view=true) abstract classes, as well as creating implementations of the factory classes that are used to generate instances of the new **Call** and **MediaProvider** subclasses. Much of the functionality of the abstract base classes is inherited by the new derived classes and can be reused and exposed publicly without further modifications. This topic describes the abstract properties and methods that must be defined in a **Call** subclass.

## Call base class

The **Call** base class handles Session Description Protocol (SDP)-based offers and answers. When a call (created locally, and represented by a **Call** subclass instance) is established, an SDP offer is sent that describes the nature of the call. The SDP offer contains a global section and one or more media sections. Each media section indicates the media type described in that section. For example, an SDP offer might include two media types, audio and video, and would therefore have two media sections, as describe in RFC 3264, An Offer/Answer Model with the Session Description Protocol (SDP). When the remote endpoint accepts the offer, it sends an SDP answer that contains the same media sections, but adds a description of the remote endpoint’s capabilities or preferences. For example, a media section might describe the codec formats that are supported. The set of media types supported by the call also should be supported by the media provider that is bound to the call. The **Conversation** associated with the call assists the call in selecting the appropriate media provider instance, which is obtained from the platform.

When an incoming INVITE message is processed, the SDP in the INVITE is also processed to determine the set of media types. These media types are used to create the call. When a call is accepted or established but the call does not have a provider, a media provider that is able to handle the media types in the call is created using the appropriate media provider factory. This provider is cached in the call. The conversation helps to bind the call to the media provider using the media provider factory.

## Abstract properties on the Call class

A class derived from the **Call** class must provide definitions for the two abstract properties on the **Call** class. The corresponding properties on the derived class override their abstract counterparts on the **Call** class.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Property</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/library/hh381972(v=office.16)">DefaultMediaType</a></p></td>
<td><p>Gets or sets the default media type that will be used to establish an outbound call.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/library/hh350179(v=office.16)">SupportedMediaTypes</a></p></td>
<td><p>Gets a list of the types of media intended to be supported in new class derived from the <strong>Call</strong> class.</p>
<p>The media types appear in the media description line (m=&lt;media&gt; &lt;port&gt;/&lt;number of ports&gt; &lt;proto&gt; &lt;fmt&gt; ...) in the SDP session description.</p></td>
</tr>
</tbody>
</table>


## Abstract methods on the Call class

A class derived from the **Call** class must also provide definitions for three abstract methods on the **Call** class.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Method</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/library/hh350189(v=office.16)">HandleFlowConfigurationRequested(MediaFlow)</a></p></td>
<td><p>Synchronously raises the flow to the application.</p>
<p>protected abstract bool HandleFlowConfigurationRequested(MediaFlow mediaFlow)</p></td>
</tr>
<tr class="even">
<td><p><a href="/dotnet/api/microsoft.rtc.collaboration.call.handletransfernotificationreceived?view=ucma-api&preserve-view=true">HandleTransferNotificationReceived(TransferStateChangedEventArgs)</a></p></td>
<td><p>Handles a transfer notification event in a class derived from the <strong>Call</strong> class.</p>
<p>protected abstract void HandleTransferNotificationReceived(TransferStateChangedEventArgs e)</p></td>
</tr>
<tr class="odd">
<td><p><a href="/dotnet/api/microsoft.rtc.collaboration.call.handletransferreceived?view=ucma-api&preserve-view=true">HandleTransferReceived(CallTransferReceivedEventArgs)</a></p></td>
<td><p>Handles a transfer event in a class derived from the <strong>Call</strong> class.</p>
<p>protected abstract void HandleTransferReceived(CallTransferReceivedEventArgs e)</p></td>
</tr>
</tbody>
</table>


If the custom call control is intended to provide call forwarding or call transfer functionality, the derived class can raise a strongly typed event to the application. Alternatively, if the custom call control does not support these capabilities, the event can be ignored (forwarding) or declined (transfer).

If the custom **Call** subclass implementation is intended to support call transfer, it should implement the **HandleTransferReceived** and **HandleTransferNotificationReceived** protected methods. The expected behavior in these methods is that they expose a first-class event handler so that an application can register for the events that notify it of incoming REFER requests and REFER status.


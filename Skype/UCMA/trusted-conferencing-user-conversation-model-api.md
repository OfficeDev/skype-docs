---
title: Trusted conferencing user conversation model API
TOCTitle: Trusted conferencing user conversation model API
ms:assetid: eca1ffb3-4c96-4425-ad62-5f28c764600a
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466013(v=office.16)
ms:contentKeyID: 65239949
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Trusted conferencing user conversation model API


_**Applies to:** Skype for Business 2015_

The Trusted Conferencing User (TCU) model was implemented in by the addition of several new types together with the addition of several new properties and overloaded methods on existing types. This topic provides the details of these changes.

### AudioVideoCallEstablishOptions

For UCMA 3.0, the [AudioVideoCallEstablishOptions](https://msdn.microsoft.com/en-us/library/hh382857\(v=office.16\)) class was added. An instance of this class can be used to contain the parameters for establishing an audio-video call. The public members of this class are shown in the following table.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>AudioVideoCallEstablishOptions member</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384410(v=office.16)">AudioVideoCallEstablishOptions()</a></p></td>
<td><p>Constructor. Creates a new instance of the <strong>AudioVideoCallEstablishOptions</strong> class.</p>
<p>AudioVideoCallEstablishOptions()</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh348625(v=office.16)">AudioVideoMcuDialInOptions</a></p></td>
<td><p>Property. Gets the optional parameters to be used when dialing into an audio-video MCU.</p>
<p>AudioVideoMcuDialInOptions AudioVideoMcuDialInOptions {get; }</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382405(v=office.16)">UseGeneratedIdentityForTrustedConference</a></p></td>
<td><p>Property. Gets or sets whether the call should use a generated identity during call establishment to a trusted conference.</p>
<p>bool UseGeneratedIdentityForTrustedConference {get; set;}</p></td>
</tr>
</tbody>
</table>


### AudioVideoMcuRouting

The [AudioVideoMcuRouting](https://msdn.microsoft.com/en-us/library/hh384660\(v=office.16\)) class represents audio routing operations that can be performed on a call. A call can specify the incoming audio route (the remote source of incoming media controlled by the call) and the outgoing audio routes (the remote sinks, or recipients, of outgoing media controlled by the call).

The following illustration shows incoming and outgoing audio routing. In this illustration, User C is the source of incoming audio to the Trusted Conferencing User (TCU) call; users A and B are the destinations of outgoing audio from the TCU call.

![Incoming and outgoing audio routing](images/Dn466013.UCMA3-TCURouting(Office.16).jpg "Incoming and outgoing audio routing")

The following table lists the public methods on the **AudioVideoMcuRouting** class.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>AudioVideoMcuRouting member</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh383615(v=office.16)">BeginUpdateAudioRoutes(IEnumerable&lt;OutgoingAudioRoute&gt;, IEnumerable&lt;IncomingAudioRoute&gt;, AsyncCallback, Object)</a></p></td>
<td><p>Method. Updates routing of outgoing audio to and incoming audio from other endpoints that are connected to the audio-video MCU.</p>
<p>BeginUpdateAudioRoutes(IEnumerable&lt;OutgoingAudioRoute&gt;, IEnumerable&lt;IncomingAudioRoute&gt;, AsyncCallback, object)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382045(v=office.16)">EndUpdateAudioRoutes(IAsyncResult)</a></p></td>
<td><p>Method. Waits for the pending asynchronous join operation to complete.</p>
<p>EndUpdateAudioRoutes(IAsyncResult)</p></td>
</tr>
</tbody>
</table>


### IncomingAudioRoute, OutgoingAudioRoute, and RouteUpdateOperation

The [IncomingAudioRoute](https://msdn.microsoft.com/en-us/library/hh383410\(v=office.16\)) and [OutgoingAudioRoute](https://msdn.microsoft.com/en-us/library/hh383214\(v=office.16\)) classes define, respectively, the remote endpoint that is the source of incoming audio, and the remote endpoint that is the destination of outgoing audio.

The following table lists the members of the **IncomingAudioRoute** class.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>IncomingAudioRoute member</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh381304(v=office.16)">IncomingAudioRoute(ParticipantEndpoint)</a></p></td>
<td><p>Constructor. Creates a new instance of the <strong>IncomingAudioRoute</strong> class.</p>
<p>IncomingAudioRoute(ParticipantEndpoint)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384101(v=office.16)">RemoteSource</a></p></td>
<td><p>Property. Gets the remote source of the incoming audio.</p>
<p>ParticipantEndpoint RemoteSource {get;}</p></td>
</tr>
</tbody>
</table>


The following table lists the members of the **OutgoingAudioRoute** class.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>OutgoingAudioRoute member</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349947(v=office.16)">OutgoingAudioRoute(ParticipantEndpoint)</a></p></td>
<td><p>Constructor. Creates a new instance of the <strong>OutgoingAudioRoute</strong> class.</p>
<p>OutgoingAudioRoute(ParticipantEndpoint)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382120(v=office.16)">RemoteSink</a></p></td>
<td><p>Property. Gets the remote sink (recipient) of the outgoing audio.</p>
<p>ParticipantEndpoint RemoteSink {get;}</p></td>
</tr>
</tbody>
</table>


The [RouteUpdateOperation](https://msdn.microsoft.com/en-us/library/hh366418\(v=office.16\)) enumerated type indicates the type of operation to be performed on a route. The valid values are **None**, **Add**, and **Remove**.

### AudioVideoMcuRouting

For UCMA 3.0, a new property was added to the [AudioVideoCall](https://msdn.microsoft.com/en-us/library/hh383901\(v=office.16\)) class-the [AudioVideoMcuRouting](https://msdn.microsoft.com/en-us/library/hh381149\(v=office.16\)) property. This property returns a reference to the **AudioVideoMcuRouting** instance associated with a given **AudioVideoCall** instance. This property is never null, but if the **AudioVideoMcuRouting** methods are called with invalid arguments (such as empty audio routes), these methods throw exceptions.

### BeginEstablish

For UCMA 3.0, two new overloaded methods were added to the **AudioVideoCall** class. These methods are **BeginEstablish(AudioVideoCallEstablishOptions, AsyncCallback, Object)** and **BeginEstablish(CallOrbit, AudioVideoCallEstablishOptions, AsyncCallback, Object)**. Each of these methods has a parameter of type [AudioVideoCallEstablishOptions](https://msdn.microsoft.com/en-us/library/hh382857\(v=office.16\)).

### ConferenceCommandOptions and derived classes (new types)

The [ConferenceCommandOptions](https://msdn.microsoft.com/en-us/library/hh382976\(v=office.16\)) abstract base class was added in UCMA 3.0. This class enables a TCU conversation to process commands in the context of another user, by the use of a set of options defined in derived classes. The members of the **ConferenceCommandOptions** class are shown in the following table.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Member</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh385189(v=office.16)">ConferenceCommandOptions()</a></p></td>
<td><p>Constructor. Creates a new instance of the <strong>ConferenceCommandOptions</strong> class.</p>
<p>ConferenceCommandOptions()</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384894(v=office.16)">Headers</a></p></td>
<td><p>Property. Gets the list of custom headers to be included in the outgoing INFO message carrying the CCCP command.</p>
<p>Collection&lt;SignalingHeaders&gt; Headers {get;}</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384388(v=office.16)">Issuer</a></p></td>
<td><p>Property. Gets or sets the issuer of the command. Trusted applications can issue commands in the context of other conference participants.</p>
<p>ConversationParticipant Issuer {get; set;}</p></td>
</tr>
</tbody>
</table>


The classes derived from the **ConferenceCommandOptions** class can be used for a variety of conference operations. The following table lists the classes that are derived from the **ConferenceCommandOptions** class.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Derived class</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382008(v=office.16)">AddToDefaultRoutingOptions</a></p></td>
<td><p>Options that an application can use to customize adding an endpoint to the MCU default routing.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349365(v=office.16)">MuteOptions</a></p></td>
<td><p>Options that an application can use to customize muting itself or other endpoints.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384787(v=office.16)">UnmuteOptions</a></p></td>
<td><p>Options that an application can use to customize unmuting itself or other endpoints.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh381336(v=office.16)">EjectOptions</a></p></td>
<td><p>Options that an application can use to customize modifying how a participant is ejected.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384043(v=office.16)">LobbyAdmitOptions</a></p></td>
<td><p>Options that an application can use to customize admitting lobby participants into a conference.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh366339(v=office.16)">LobbyDenyOptions</a></p></td>
<td><p>Options that an application can use to customize denying lobby participants admission into the conference.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh383174(v=office.16)">LockConferenceOptions</a></p></td>
<td><p>Options that an application can use to customize locking a conference.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384549(v=office.16)">McuDialOutOptions</a></p></td>
<td><p>Options that an application can use to customize how the MCU dials out to participants.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384212(v=office.16)">McuTransferOptions</a></p></td>
<td><p>Options that an application can use to customize how the MCU transfers a call from one participant to another.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384454(v=office.16)">ModifyRoleOptions</a></p></td>
<td><p>Options that an application can use to customize modifying the role of another conference participant.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382498(v=office.16)">RemoveFromDefaultRoutingOptions</a></p></td>
<td><p>Options that an application can use to customize removing an endpoint from the MCU default routing.</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382977(v=office.16)">UnlockConferenceOptions</a></p></td>
<td><p>Options that an application can use to customize unlocking a conference.</p></td>
</tr>
</tbody>
</table>


### Overloaded methods on ConferenceSession, McuSession, and AudioVideoMcuSession

The methods shown in the following table use the classes listed in the previous section. Some of the methods in the next table are overloaded methods added in on the existing [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)), [McuSession](https://msdn.microsoft.com/en-us/library/hh384975\(v=office.16\)), and [AudioVideoMcuSession](https://msdn.microsoft.com/en-us/library/hh385298\(v=office.16\)) classes. The remaining methods are on the [LobbyManager](https://msdn.microsoft.com/en-us/library/hh383596\(v=office.16\)) class, which is new in UCMA 3.0 Core SDK. For a given overloaded method, the containing class and associated **ConferenceCommandOptions** subclass are shown.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Method (class)</p></th>
<th><p>Options class</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><strong>BeginAddToDefaultRouting</strong> (<strong>AudioVideoMcuSession</strong>)</p></td>
<td><p><strong>AddToDefaultRoutingOptions</strong></p></td>
</tr>
<tr class="even">
<td><p><strong>BeginAdmitLobbyParticipants</strong> (<strong>LobbyManager</strong>)</p></td>
<td><p><strong>LobbyAdmitOptions</strong></p></td>
</tr>
<tr class="odd">
<td><p><strong>BeginDenyLobbyParticipants</strong> (<strong>LobbyManager</strong>)</p></td>
<td><p><strong>LobbyDenyOptions</strong></p></td>
</tr>
<tr class="even">
<td><p><strong>BeginDialOut</strong> (<strong>McuSession</strong> and <strong>AudioVideoMcuSession</strong>)</p></td>
<td><p><strong>McuDialOutOptions</strong></p></td>
</tr>
<tr class="odd">
<td><p><strong>BeginEject</strong> (<strong>McuSession</strong> and <strong>ConferenceSession</strong>)</p></td>
<td><p><strong>EjectOptions</strong></p></td>
</tr>
<tr class="even">
<td><p><strong>BeginLockConference</strong> (<strong>ConferenceSession</strong>)</p></td>
<td><p><strong>LockConferenceOptions</strong></p></td>
</tr>
<tr class="odd">
<td><p><strong>BeginModifyAccessLevel</strong> (<strong>LobbyManager</strong>)</p></td>
<td><p><strong>ModifyAccessLevelOptions</strong></p></td>
</tr>
<tr class="even">
<td><p><strong>BeginModifyAutomaticLeaderAssignment</strong> (<strong>ConferenceSession</strong>)</p></td>
<td><p><strong>ModifyAutomaticLeaderAssignmentOptions</strong></p></td>
</tr>
<tr class="odd">
<td><p><strong>BeginModifyLobbyBypass</strong> (<strong>LobbyManager</strong>)</p></td>
<td><p><strong>ModifyLobbyBypassOptions</strong></p></td>
</tr>
<tr class="even">
<td><p><strong>BeginModifyRole</strong> (<strong>ConferenceSession</strong>)</p></td>
<td><p><strong>ModifyRoleOptions</strong></p></td>
</tr>
<tr class="odd">
<td><p><strong>BeginMute</strong> (<strong>AudioVideoMcuSession</strong>)</p></td>
<td><p><strong>MuteOptions</strong></p></td>
</tr>
<tr class="even">
<td><p><strong>BeginRemoveFromDefaultRouting</strong> (<strong>ConferenceSession</strong>)</p></td>
<td><p><strong>RemoveFromDefaultRoutingOptions</strong></p></td>
</tr>
<tr class="odd">
<td><p><strong>BeginTransfer</strong> (<strong>McuSession</strong> and <strong>AudioVideoMcuSession</strong>)</p></td>
<td><p><strong>McuTransferOptions</strong></p></td>
</tr>
<tr class="even">
<td><p><strong>BeginUnlockConference</strong> (<strong>ConferenceSession</strong>)</p></td>
<td><p><strong>UnlockConferenceOptions</strong></p></td>
</tr>
<tr class="odd">
<td><p><strong>BeginUnmute (AudioVideoMcuSession)</strong></p></td>
<td><p><strong>UnmuteOptions</strong></p></td>
</tr>
</tbody>
</table>


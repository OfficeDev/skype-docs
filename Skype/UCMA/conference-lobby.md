---
title: Conference lobby
TOCTitle: Conference lobby
ms:assetid: a299808b-4228-4934-8fe7-7ec000382ee8
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465989(v=office.16)
ms:contentKeyID: 65239940
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Conference lobby


_**Applies to:** Skype for Business 2015_

**In this article**  
Lobby management capability  
Lobby join  
Conference lobby API  
LobbyManager class  

The conference lobby is a virtual room in which participants in a multi-party conversation (a conference session) wait until the presenters of in the meeting are ready to begin.

The lobby feature allows leaders to admit, deny, and eject participants from the lobby. Applications that are directed to the lobby can set a timeout value that indicates how long they are willing to wait before they exit automatically. When lobby participants are waiting in the lobby, they have no visibility into the meeting itself. This means that they cannot see who is attending the meeting and cannot monitor the media in the lobby. This gives leaders the ability choose who is allowed in to see the current presentation. Lobby participants waiting in the lobby cannot see one another or communicate in any way with each other.

Leaders and trusted applications have privileges and are admitted to the conference without first passing through the lobby. Participants calling in from a voice gateway might be placed into the lobby, depending on the current value of the [LobbyBypass](https://msdn.microsoft.com/en-us/library/hh381638\(v=office.16\)) property in the active meeting.

## Lobby management capability

The [CanManageLobby](https://msdn.microsoft.com/en-us/library/hh381614\(v=office.16\)) property on the [ConferenceJoinOptions](https://msdn.microsoft.com/en-us/library/hh385064\(v=office.16\)) class determines whether an application manages the conference lobby. For example, if an application joins the conference in the default mode but is unable to present enough user interface components to its users to manage the conference lobby (such as a phone), the application should set this property to false. The same guidance applies to applications that join in trusted mode (see [JoinMode](https://msdn.microsoft.com/en-us/library/hh384536\(v=office.16\))).

The [BeginJoin(ConferenceJoinOptions, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh348502\(v=office.16\)) method joins the application to an *ad hoc* conference organized under the application’s identity. However, if the application receives and accepted a conference invitation or received a conference MCU dial out, then calling this method will join the application to that specific conference.


> [!NOTE]
> <P>It is possible for an application to land in the conference lobby if it received a conference invitation.</P>



The [BeginJoin(String, ConferenceJoinOptions, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh348983\(v=office.16\)) joins the application to the supplied conference URI.

## Lobby join

If an application is placed in the conference lobby, the callback passed in the **BeginJoin** method is not be invoked until the application is admitted into the conference, removed from of the lobby, or the lobby wait timer expires.

The following table shows the behaviors that are expressed when an application is placed in the lobby.

<table>
<colgroup>
<col style="width: 25%" />
<col style="width: 25%" />
<col style="width: 25%" />
<col style="width: 25%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Scenario</p></th>
<th><p>EndJoin action</p></th>
<th><p>Conversation state</p></th>
<th><p>ConferenceSession state</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Application admitted to conference.</p></td>
<td><p>Does not throw an exception.</p></td>
<td><p><strong>InLobby</strong> changes to <strong>Conferenced</strong></p></td>
<td><p><strong>InLobby</strong> changes to <strong>Connected</strong></p></td>
</tr>
<tr class="even">
<td><p>Application removed from conference.</p></td>
<td><p>Throws <strong>ConferenceFailureException</strong> with appropriate <strong>Reason</strong> strings.</p></td>
<td><p><strong>InLobby</strong> changes to <strong>Terminated</strong></p></td>
<td><p><strong>InLobby</strong> changes to <strong>Idle</strong></p></td>
</tr>
<tr class="odd">
<td><p>Application leaves conference by calling <a href="https://msdn.microsoft.com/en-us/library/hh349607(v=office.16)">BeginTerminate()</a>.</p></td>
<td><p>Throws <strong>OperationFailureException</strong> exception.</p></td>
<td><p><strong>InLobby</strong> changes to <strong>Terminated</strong></p></td>
<td><p><strong>InLobby</strong> changes to <strong>Idle</strong></p></td>
</tr>
<tr class="even">
<td><p>Lobby wait timer expires.</p></td>
<td><p>Throws <strong>OperationTimeoutException</strong> exception.</p></td>
<td><p><strong>InLobby</strong> changes to <strong>Terminated</strong></p></td>
<td><p><strong>InLobby</strong> changes to <strong>Idle</strong></p></td>
</tr>
</tbody>
</table>


## Conference lobby API

The main APIs that can be used to work with the lobby are described here.

The [LobbyManager](https://msdn.microsoft.com/en-us/library/hh383596\(v=office.16\)) class can be used by conference leaders to admit or deny entrance to participants who are in the lobby.

Conference leaders can use the [BeginEject()](https://msdn.microsoft.com/en-us/library/hh384485\(v=office.16\)) overloaded methods to disconnect a lobby participant from the server.

The [GetLobbyParticipants()](https://msdn.microsoft.com/en-us/library/hh384852\(v=office.16\)) method can be used by anyone in the conference to get a filtered list of participants who are in the lobby. The [IsInLobby](https://msdn.microsoft.com/en-us/library/hh383635\(v=office.16\)) property can be used to determine whether a participant who joined or left did so coming in from the lobby or leaving the lobby. This can be used in the [LobbyParticipantAttendanceChanged](https://msdn.microsoft.com/en-us/library/hh349572\(v=office.16\)) event handler to identify lobby participants as they enter the lobby.

Client applications can check the state of the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) object to see if the value of the [State](https://msdn.microsoft.com/en-us/library/hh161754\(v=office.16\)) property on the **ConferenceSession** object is equal to [ConversationState](https://msdn.microsoft.com/en-us/library/hh381026\(v=office.16\)).

Conferences can be scheduled and updated while they are active to allow calls coming from the voice gateway to bypass the lobby, or to force them to enter the lobby first. This can be done by using the [LobbyBypass](https://msdn.microsoft.com/en-us/library/hh382782\(v=office.16\)) property on [ConferenceScheduleInformation](https://msdn.microsoft.com/en-us/library/hh381608\(v=office.16\)) or one of the [BeginModifyConferenceConfiguration()](https://msdn.microsoft.com/en-us/library/hh161773\(v=office.16\)) overloaded methods on the **ConferenceSession** instance.

## LobbyManager class

The methods and property on the [LobbyManager](https://msdn.microsoft.com/en-us/library/hh383596\(v=office.16\)) class can be used to manage incoming participants in a conference.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>LobbyManager member</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh350073(v=office.16)">BeginAdmitLobbyParticipants(IEnumerable&lt;ConversationParticipant&gt;, AsyncCallback, Object)</a></p></td>
<td><p>Method. Admits lobby participants into the conference.</p>
<p>BeginAdmitLobbyParticipants(IEnumerable&lt;ConversationParticipant&gt;, AsyncCallback, object)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349506(v=office.16)">BeginAdmitLobbyParticipants(IEnumerable&lt;ConversationParticipant&gt;, LobbyAdmitOptions, AsyncCallback, Object)</a></p></td>
<td><p>Method. Admits lobby participants into the conference.</p>
<p>BeginAdmitLobbyParticipants(IEnumerable&lt;ConversationParticipant&gt;, LobbyAdmitOptions, AsyncCallback, object)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382108(v=office.16)">BeginDenyLobbyParticipants(IEnumerable&lt;ConversationParticipant&gt;, AsyncCallback, Object)</a></p></td>
<td><p>Method. Denies lobby participants admission into the conference and removes them from the lobby.</p>
<p>BeginDenyLobbyParticipants(IEnumerable&lt;ConversationParticipant&gt;, AsyncCallback, object)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349251(v=office.16)">BeginDenyLobbyParticipants(IEnumerable&lt;ConversationParticipant&gt;, LobbyDenyOptions, AsyncCallback, Object)</a></p></td>
<td><p>Method. Denies lobby participants admission into the conference and removes them from the lobby.</p>
<p>BeginDenyLobbyParticipants(IEnumerable&lt;ConversationParticipant&gt;, LobbyDenyOptions, AsyncCallback, object)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh381873(v=office.16)">EndAdmitLobbyParticipants(IAsyncResult)</a></p></td>
<td><p>Method. Waits for the pending asynchronous operation to complete.</p>
<p>EndAdmitLobbyParticipants(IAsyncResult)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349240(v=office.16)">EndDenyLobbyParticipants(IAsyncResult)</a></p></td>
<td><p>Method. Waits for the pending asynchronous operation to complete.</p>
<p>EndDenyLobbyParticipants(IAsyncResult)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh366087(v=office.16)">ConferenceSession</a></p></td>
<td><p>Property. Gets the conference session object that is associated with this <strong>LobbyManager</strong> instance.</p>
<p>ConferenceSession ConferenceSession {get;}</p></td>
</tr>
</tbody>
</table>


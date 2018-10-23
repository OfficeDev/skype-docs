---
title: ConferenceSession state transitions
TOCTitle: ConferenceSession state transitions
ms:assetid: 3b9b7ac4-5876-4381-aaee-42f32d3db265
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466014(v=office.16)
ms:contentKeyID: 65239938
ms.date: 07/27/2015
mtps_version: v=office.16
---

# ConferenceSession state transitions

**Applies to**: Skype for Business 2015

A conference represents an online meeting place for participants to discuss topics of interest, using one or more media types, such as instant messages or audio-video. A two-party conversation can be escalated to a conference. In Microsoft Unified Communications Managed API (UCMA), the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) class implements the signaling functionality of a Centralized Conference Control Protocol (C3P) conference signaling session.

## ConferenceSession state transitions

The following illustration shows the possible state transitions on a **ConferenceSession** instance.

<br/>

![State transitions on a ConferenceSession instance](images/Dn466014.StateMach_ConfSession(Office.16).jpg "State transitions on a ConferenceSession instance")

1. The transition from **Idle** to **Connecting** occurs when **BeginJoin** is called.

2. The transition from **Connecting** to **Connected** occurs when the join process is complete. In the **Connected** state, [ParticipantEndpointAttendanceChanged](https://msdn.microsoft.com/en-us/library/hh383640\(v=office.16\)) events are raised for all existing participant endpoints in the Focus and MCUs before the callback method in the argument list of **BeginJoin** is called.

3. The transition from **Connected** to **Disconnecting** occurs when any of the following occur:
    
   - The application called **BeginTerminate** on the **Conversation** instance.
    
   - The application called **BeginTerminateConference** on the **ConferenceSession** instance.
    
   - A local participant was ejected from the conference.
    
   - The conference was terminated.
    
   - The parent **Conversation** failed an escalation attempt.

4. The transition from **Disconnecting** to **Disconnected** occurs when the call to **BeginTerminateConference** on the **ConferenceSession** instance succeeds. The termination process is complete. Signaling and subscription sessions with the Focus are terminated before this transition occurs. This transition also happens when passive cleanup is complete; that is, when a participant is ejected, and the object has been garbage-collected.

5. The transition from to **Disconnecting** to **Idle** occurs only when the parent [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) has failed an escalation attempt. Before the transition to **Idle** occurs, **ConferenceSession** state is cleaned up and [ParticipantEndpointAttendanceChanged](https://msdn.microsoft.com/en-us/library/hh383640\(v=office.16\)) events are raised for all participant endpoints associated with this **ConferenceSession** and its child [McuSession](https://msdn.microsoft.com/en-us/library/hh384975\(v=office.16\)) objects.

6. The transition from **Connected** to **Reconnecting** occurs if the front end that hosts the Focus has failed over. During this transition, all pending **ConferenceSession** operations fail. Any operation requested from **ConferenceSession** after its state changes to **Reconnecting** will fail with a [RealTimeInvalidOperationException](https://msdn.microsoft.com/en-us/library/hh349003\(v=office.16\)) with a failure reason of **RetryableOperation**. This transition also occurs when the communication channel (SIP dialog) is in recovery mode.

7. The transition from **Reconnecting** to **Connected** occurs when the reconnection process is successful. The Focus delivers a fresh roster that is compared to the existing roster information in **ConferenceSession** and its **McuSession** objects. Events are raised if any differences are detected.

8. The transition from **Reconnecting** to **Disconnecting** occurs if the reconnection attempt fails. Note that only one reconnection attempt is made.

9. The transition from **Connecting** to **Idle** occurs when the connection attempt fails, whereupon **EndJoin** throws an exception.

10. The transition from **Connecting** to **InLobby** occurs when a participant attempts to join the conference, and the lobby is enabled on a Skype for Business Server 2015 deployment.

11. The transition from **InLobby** to **Reconnecting** occurs if the front end that hosts the Focus has failed over. During this transition, all pending **ConferenceSession** operations fail. Any operation requested from **ConferenceSession** after its state changes to **Reconnecting** will fail with a [RealTimeInvalidOperationException](https://msdn.microsoft.com/en-us/library/hh349003\(v=office.16\)) with a failure reason of **RetryableOperation**. This transition also occurs when the communication channel (SIP dialog) is in recovery mode.

12. The transition from **Reconnecting** to **InLobby** occurs when the lobby is enabled on a Skype for Business Server 2015 deployment, and the reconnection process is successful. The Focus delivers a fresh roster that is compared to the existing roster information in **ConferenceSession** and its **McuSession** objects. Events are raised if any differences are detected.

13. The transition from **InLobby** to **Connected** occurs when the participant is admitted into the conference by the leader.

14. The transition from **InLobby** to **Disconnecting** occurs when a participant is ejected from the lobby, the conference is terminated, or **BeginTerminate** is called.


> [!NOTE]
> **Conversation** as currently implemented will begin terminating if an attempt to join a conference fails but the **ConversationState** on the **Conversation** instance is not **Conferencing**.




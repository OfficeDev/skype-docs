---
title: Conversation state transitions - outbound
TOCTitle: Conversation state transitions - outbound
ms:assetid: 10020cc8-7fba-4de3-8f23-624908ad7720
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465998(v=office.16)
ms:contentKeyID: 65239914
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Conversation state transitions - outbound


_**Applies to:** Skype for Business 2015_

The [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) state transitions for outbound calls are shown in the following illustration.

![Conversation state transitions for outbound calls](images/Dn465998.StateMach_Conversation-Out(Office.16).jpg "Conversation state transitions for outbound calls")

1.  The transition from **Idle** to **Establishing** occurs when Call.**BeginEstablish** executes for the first call bound to the **Conversation** instance.

2.  The transition from **Idle** to **Conferencing** occurs when Conversation.ConferenceSession.**BeginJoin** is called.

3.  The transition from **Establishing** to **Established** occurs when the first call is successfully established by Call.**EndEstablish**.

4.  The transition from **Establishing** to **Terminating** occurs when auto-termination is triggered and all conditions are satisfied. For details, see the explanation immediately following this list.

5.  The transition from **Conferencing** to **Idle** occurs when a conferencing failure occurs.

6.  The transition from **Established** to **Terminating** occurs when Conversation.**BeginTerminate** is called or when auto-termination is triggered and all conditions are satisfied. For details, see the explanation immediately following this list.

7.  The transition from **Established** to **Conferencing** occurs when Conversation.ConferenceSession.**BeginJoin** is called to escalate the conversation.

8.  The transition from **Conferencing** to **Terminating** occurs when Conversation.**BeginTerminate** is called.

9.  The transition from **Conferencing** to **Established** occurs when there is an escalation failure for the established calls.

10. The transition from **Idle** to **Terminating** occurs when Conversation.**BeginTerminate** is called or when auto-termination is triggered and all conditions are satisfied. For details, see the explanation immediately following this list.

11. The transition from **Establishing** to **Idle** occurs when a call is terminated while there are other calls that are about to be established.

12. The transition from **Conferencing** to **Establishing** occurs when a conference failure occurs with no calls in the **Established** state, but at least one call in the **Establishing** state.

13. The transition from **Conferencing** to **InLobby** occurs when a participant is placed in the lobby of a conference during a conference join.

14. The transition from **InLobby** to **Idle** occurs when a conferencing failure occurs while a participant is in the **InLobby** state.

15. The transition from **Conferencing** to **Conferenced** occurs when the calls are successfully escalated or when the conversation is successfully joined to the conference session.

16. The transition from **InLobby** to **Conferenced** occurs when a presenter admits a participant from the lobby into the conference.

17. The transition from **Conferenced** to **Terminating** occurs when Conversation.**BeginTerminate** is called, connection with the conference is disconnected, or a conference participant is ejected from the conference.

18. The transition from **InLobby** to **Terminating** occurs when Conversation.**BeginTerminate** is called while the participant is in the lobby.

19. The transition from **Terminating** to **Terminated** occurs when Conversation.**EndTerminate** completes.

*Auto-termination* is triggered when the last non-idle call in the conversation is terminated or when the **ConferenceSession** join operation fails.

Auto-termination occurs when all three of the following conditions are satisfied.

1.  All calls bound to the **Conversation** instance are in the **Idle** state.

2.  The state of the **ConferenceSession** instance that is bound to the **Conversation** instance is neither **Connecting** nor **Connected**.

3.  The conversation does not involve an MCU.


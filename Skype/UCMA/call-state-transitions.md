---
title: Call state transitions
TOCTitle: Call state transitions
ms:assetid: 2f9176a0-b25f-43e0-a000-1fe5b5dfc56d
ms:mtpsurl: https://msdn.microsoft.com/library/Dn465981(v=office.16)
ms:contentKeyID: 65239927
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Call state transitions


**Applies to**: Skype for Business 2015

## Call transitions—outbound calls

The [Call](/dotnet/api/microsoft.rtc.collaboration.call) state transitions for an outbound call are shown in the following illustration.

![Call state transitions for an outbound call](images/Dn465981.StateMach_Call_Out(Office.16).jpg "Call state transitions for an outbound call")

1.  The transition from **Idle** to **Establishing** occurs when the application calls [BeginEstablish](https://msdn.microsoft.com/library/hh383658\(v=office.16\)) on the call.

2.  The transition from **Establishing** to **Establishing** occurs when the application receives the first ringing notification from the remote side while the call is in the **Establishing** state.

3.  The transition from **Establishing** to **Terminating** occurs when **BeginEstablish** fails, or when the application calls [BeginTerminate](https://msdn.microsoft.com/library/hh383376\(v=office.16\)) on the call, or when the application calls **BeginTerminate** on the conversation that contains this call.

4.  The transition from **Establishing** to **Established** occurs when when [EndEstablish](https://msdn.microsoft.com/library/hh349248\(v=office.16\)) completes successfully.

5.  The transition from **Established** to **Transferring** occurs when the application calls [BeginTransfer](https://msdn.microsoft.com/library/hh381053\(v=office.16\)) on the call.

6.  The transition from **Transferring** to **Established** occurs when when **BeginTransfer** is unsuccessful.

7.  The transition from **Established** to **Terminating** occurs when the application calls **BeginTerminate** on the call, or when the remote side terminates the call, or when the application calls **BeginTerminate** on the conversation that contains this call.

8.  The transition from **Idle** to **Terminating** occurs when the application calls **BeginTerminate** on the call, or when the application calls **BeginTerminate** on the conversation that contains this call.

9.  The transition from **Transferring** to **Terminating** occurs when the application calls **BeginTerminate** on the call, or when the transfer operation is successful, or when the application calls **BeginTerminate** on the conversation that contains this call.

10. The transition from **Terminating** to **Terminated** occurs when **EndTerminate** completes.

### Call transitions—inbound calls

The **Call** state transitions for an inbound call are shown in the following illustration.

![Call state transitions for an inbound call](images/Dn465981.StateMach_Call_In(Office.16).jpg "Call state transitions for an inbound call")

1.  The transition from **Incoming** to **Establishing** occurs when the application calls [BeginEstablish](https://msdn.microsoft.com/library/hh383658\(v=office.16\)) on the call.

2.  The transition from **Establishing** to **Terminating** occurs when the call is not accepted, or when the application calls [BeginTerminate](https://msdn.microsoft.com/library/hh383376\(v=office.16\)) on the call, or when the application calls **BeginTerminate** on the conversation that contains this call.

3.  The transition from **Establishing** to **Established** occurs when the call is accepted (when [BeginAccept](https://msdn.microsoft.com/library/hh383161\(v=office.16\)) returns successfully).

4.  The transition from **Established** to **Transferring** occurs when the application calls [BeginTransfer](https://msdn.microsoft.com/library/hh381053\(v=office.16\)) on the call.

5.  The transition from **Transferring** to **Established** occurs when **BeginTransfer** is unsuccessful.

6.  The transition from **Established** to **Terminating** occurs when the application calls **BeginTerminate** on the call, or when the application calls **BeginTerminate** on the conversation that contains this call.

7.  The transition from **Incoming** to **Terminating** occurs when the application calls **Decline**, **Forward**, or **BeginTerminate** on the call, or when the application calls **BeginTerminate** on the conversation that contains this call, or when the remote side cancels its call request.

8.  The transition from **Transferring** to **Terminating** occurs when the application calls **BeginTerminate** on the call, or when the transfer operation is successful, or when the application calls **BeginTerminate** on the conversation that contains this call.

9.  The transition from **Terminating** to **Terminated** occurs when **EndTerminate** completes.


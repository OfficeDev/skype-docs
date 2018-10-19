---
title: Conference recovery
TOCTitle: Conference recovery
ms:assetid: a857410d-7f32-4850-9815-e0be658781b2
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466073(v=office.16)
ms:contentKeyID: 65240008
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Conference recovery


_**Applies to:** Skype for Business 2015_

UCMA 5.0 provides three levels of support for conference recovery to applications.

  - Focus Front-End failover
    
    When UCMA 5.0 detects that the Focus has failed over to another Front End server, the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) state changes to **Reconnecting**. The state of any active **McuSession** subclass instance changes to the **Retrying** state. Note that any calls connected to the MCUs are not disconnected.
    
    After failover is detected, pending conference operations fail immediately with an [OperationFailureException](https://msdn.microsoft.com/en-us/library/hh161725\(v=office.16\)) with [FailureReason](https://msdn.microsoft.com/en-us/library/hh381130\(v=office.16\)) set to **SessionRetrying**. Any **Begin***Xxx* method invoked on the **ConferenceSession** or an **McuSession** subclass fails with a [RealTimeInvalidOperationException](https://msdn.microsoft.com/en-us/library/hh349003\(v=office.16\)) with [FailureReason](https://msdn.microsoft.com/en-us/library/hh350167\(v=office.16\)) set to **RetryableOperation**.
    
    The **ConferenceSession** attempts to reconnect to the conference only once. If that attempt fails, the **ConferenceSession** is terminated, otherwise the state of the **ConferenceSession** is again changed to the **Connected** state. A fresh roster notification is sent and events are raised for any changes that occur during the reconnection process. The state of the **McuSession** subclass changes again to active when the MCUs are reported to be activated on the server.

  - MCU failover
    
    During conference activation, an MCU can failover to another Front End server. The corresponding **McuSession** state is changed to **Retrying** and the call with the MCU is terminated. The application can reestablish the call after the MCU becomes active again.

  - Intermediate Hop failover
    
    If an intermediate hop fails when a conference command is sent, UCMA 5.0 attempts to repair the route automatically. If the initial recovery attempt fails, the **ConferenceSession** instance enters the **Reconnecting** state. In this state, conferencing or MCU control commands and instant messages might fail. Other media such as audio, which do not travel over the signaling path, might still continue to work. The session will actively try to heal the route by periodically sending UPDATE messages. If healing succeeds, the **ConferenceSession** state will transition to the **Connected** or **InLobby** state. Because the **ConferenceSession** does not leave the **Reconnecting** state unless route healing succeeds, the application should implement timers or use other criteria such as media termination to determine when to terminate the **ConferenceSession** if recovery takes too long.


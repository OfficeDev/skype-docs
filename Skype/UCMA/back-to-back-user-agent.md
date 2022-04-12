---
title: Back-to-back user agent
TOCTitle: Back-to-back user agent
ms:assetid: f9ece1e1-ff34-4c31-9bb7-40e113cdbc36
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466011(v=office.16)
ms:contentKeyID: 65239934
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Back-to-back user agent

**Applies to**: Skype for Business 2015

Microsoft Unified Communications Managed API 5.0 provides several classes to support the back-to-back user agent concept. The principal class of interest to application developers is the [BackToBackCall](/dotnet/api/microsoft.rtc.collaboration.backtobackcall) class. 

Using back-to-back user agent functionality, an application can perform the following operations:

- Create a **BackToBackCall** instance, which establishes back-to-back connectivity between two calls. For example, a customer and an agent are connected through a back-end call center.

- Perform midway call control (transfer calls, park calls, play music to customers on hold) on an established back-to-back call.

This section includes the following topics:

- [Call leg scenarios](call-leg-scenarios.md)
- [Back-to-back user agent architecture](back-to-back-user-agent-architecture.md)

## See also

- [Video demo - Lync 2013: How to do back-to-back calls using UCMA 4](https://channel9.msdn.com/posts/lync-2013-how-to-do-back-to-back-calls-using-ucma-4)


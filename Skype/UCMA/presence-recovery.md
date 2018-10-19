---
title: Presence recovery
TOCTitle: Presence recovery
ms:assetid: a645388a-5da0-4798-a031-68721b2be91a
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466071(v=office.16)
ms:contentKeyID: 65240007
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Presence recovery


_**Applies to:** Skype for Business 2015_

The presence-related services of an endpoint—the [LocalOwnerPresence](https://msdn.microsoft.com/en-us/library/hh348476\(v=office.16\)), [ContactGroupServices](https://msdn.microsoft.com/en-us/library/hh383122\(v=office.16\)), and [PresenceServices](https://msdn.microsoft.com/en-us/library/hh384331\(v=office.16\)) properties—are references to, respectively, the [LocalOwnerPresence](https://msdn.microsoft.com/en-us/library/hh382370\(v=office.16\)), [ContactGroupServices](https://msdn.microsoft.com/en-us/library/hh381099\(v=office.16\)), and [LocalEndpointPresenceServices](https://msdn.microsoft.com/en-us/library/hh350157\(v=office.16\)) classes. These services use several Skype for Business Server 2015 servers to publish and subscribe to presence data. For example, the User Services role of Skype for Business Server 2015 is the repository of presence data, while the Front End service is responsible for routing publication and subscription requests. When any of the relevant Skype for Business Server 2015 services goes offline or loses connectivity, the UCMA 5.0 presence services are disrupted as well.

The UCMA 5.0 platform is able to detect several types of disruption, such as when a **LocalOwnerPresence** instance is unable to subscribe to its self presence, or when a **RemotePresenceView** instance is unable to add a new target. For registered endpoints receives a notification from the Skype for Business Server 2015 server that **User Services** is unavailable. When learns of a disruption, the state of the affected service changes to **WaitingForRetry**. This causes UCMA 5.0 to retry the failed operation to recover from a temporary server failure.


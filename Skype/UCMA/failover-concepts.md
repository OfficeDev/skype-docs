---
title: Failover concepts
TOCTitle: Failover concepts
ms:assetid: 7c25908d-e278-4204-9590-3fa44ecf4469
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466069(v=office.16)
ms:contentKeyID: 65240003
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Failover concepts


_**Applies to:** Skype for Business 2015_

If a Skype for Business Server 2015 registrar is taken offline for service, or a service-affecting interruption has occurred between the UCMA application and the registrar, a Skype for Business Server 2015 front-end pool can redirect the application to a currently active server, if one is available. Similar to the high-availability feature in non-failure scenarios, an application is unaware of this redirection.

When a registrar has failed, the next outgoing message (or registration refresh) from the UCMA platform causes UCMA to attempt to register first against other servers in the same pool, and then against other server pools. The application should handle this in the same way that it would handle an ordinary reregistration.

The connection of registration is also used to notify the application of partial failures in its associate pool. If the User Services pool for a given endpoint fails, the Presence and Conferencing Services for that endpoint’s pool will be unavailable. The application can be informed of the state of its user services by subscribing to the [StateChanged](https://msdn.microsoft.com/en-us/library/hh383071\(v=office.16\)) event, and watching for the **UserServicesState()** property. If the **UserServicesState** value is **Unavailable**, the application should take this to mean that presence subscriptions and outbound conference actions might fail or be stale. For more information, see [Call recovery usage](call-recovery-usage.md).


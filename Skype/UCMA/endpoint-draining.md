---
title: Endpoint draining
TOCTitle: Endpoint draining
ms:assetid: 983c3c61-2ac0-4d07-9e38-4ffed06d2798
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465934(v=office.16)
ms:contentKeyID: 65239823
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Endpoint draining


_**Applies to:** Skype for Business 2015_

In Microsoft Unified Communications Managed API 5.0 and , methods on the [ApplicationEndpoint](https://msdn.microsoft.com/en-us/library/hh384825\(v=office.16\)) class can be used to *drain* existing calls (gracefully decline incoming calls by routing them to another application instance): [BeginDrain(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh348822\(v=office.16\)) and [EndDrain(IAsyncResult)](https://msdn.microsoft.com/en-us/library/hh383086\(v=office.16\)). These methods cause all incoming calls to be automatically routed to another application instance, provided that this other application instance is available and is not draining. If the targeted application instance is unavailable or is draining, the incoming calls will be declined.

Existing conversations will continue normally (including modality addition and escalation to conference), so an application may use this feature during its shutdown phase while still completing existing calls. The application can then shut down, perform maintenance as needed, and restart. After the application restarts, new calls are again routed to it.


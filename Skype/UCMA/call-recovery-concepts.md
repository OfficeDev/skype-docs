---
title: Call recovery concepts
TOCTitle: Call recovery concepts
ms:assetid: 119480e2-b2df-4654-8345-37d2b2696aef
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466070(v=office.16)
ms:contentKeyID: 65240005
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Call recovery concepts


_**Applies to:** Skype for Business 2015_

If the SIP route between endpoints has failed, the Skype for Business Server 2015 computer causes all new incoming messages to fail (surfaced to the application as a **FailureResponseException** with a SIP 430 error code) until the route is healed. When this happens, the application can either terminate the call or wait for recovery. If the application chooses to wait for recovery, it should implement timers to ensure that the call does not wait indefinitely. The application should also subscribe to the **RouteSetStatusChanged** event on the call so that it is notified of route events. When the route is healed (see [Call recovery usage](call-recovery-usage.md)), the application can resend the messages.

In the UCMA 5.0 layer, an endpoint is set by default to **MaximumRetry**, one of the values of the [RouteSetRecoveryMode](https://msdn.microsoft.com/en-us/library/hh382127\(v=office.16\)) enumeration. In the signaling layer, however, an application has a number of High Availability modes to choose from. An endpoint can be set with one of these modes as shown in the following example.

``` csharp
RealTimeEndpoint rte = applicationEndpoint.InnerEndpoint;
rte.ApplyRouteSetRecoverySettings(new RouteSetRecoverySettings(RouteSetRecoverMode.LimitedRetry));
```


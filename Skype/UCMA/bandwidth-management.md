---
title: Bandwidth management
TOCTitle: Bandwidth management
ms:assetid: 38ebe3a5-afab-4877-9fdc-ce36e915d19f
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465930(v=office.16)
ms:contentKeyID: 65239811
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Bandwidth management


_**Applies to:** Skype for Business 2015_

Microsoft Unified Communications Managed API 5.0 and Skype for Business Server 2015 offer a bandwidth-management solution that allows calls to be selectively declined and re-routed based on the consumption of the network links. UCMA 5.0 will automatically adopt server settings for bandwidth control, and based on those settings, UCMA 5.0 will make a request for bandwidth automatically on call placement or receipt. If bandwidth is oversubscribed, and sufficient resources cannot be allocated for this call, the call establishment or acceptance might fail, or be re-routed through the PSTN link, if available.

If the application allows the call to be redirected to PSTN, be aware that any rich information associated with the call will be lost. In particular, if an application must be able to pass contextual data, or perform modalities other than voice, the application should not enable redirection to PSTN.

For more information, see [Call admission control](call-admission-control.md).


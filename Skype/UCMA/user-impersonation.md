---
title: User impersonation
TOCTitle: User impersonation
ms:assetid: 4d243ea7-4131-462f-a476-f41bba5dac51
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465968(v=office.16)
ms:contentKeyID: 65239859
ms.date: 07/27/2015
mtps_version: v=office.16
---

# User impersonation

**Applies to**: Skype for Business 2015

In Microsoft Unified Communications Managed API 5.0, an application (specifically, an [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) instance) can impersonate another user.

The following illustration depicts a two-party conversation using audio. In this illustration an application (Application A) impersonates a user (User A). In this situation, the endpoint owner and the local participant of the associated conversation instance are different. 

Another difference between this scenario and the two previous ones is that the endpoint for Application A must be an **ApplicationEndpoint**, rather than a [UserEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.userendpoint?view=ucma-api).

![Two-party conversation with impersonation](images/Dn465968.Two-party-Impersonation-AV(Office.16).png "Two-party conversation with impersonation")


---
title: 'UCMA 3.0 Core applications: Coexistence and deployment scenarios'
TOCTitle: 'UCMA 3.0 Core applications: Coexistence and deployment scenarios'
ms:assetid: 90b3c204-a67e-4604-aea2-b2c0c8be51d2
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466145(v=office.16)
ms:contentKeyID: 65240056
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA 3.0 Core applications: Coexistence and deployment scenarios

**Applies to**: Skype for Business 2015

If a mixed –Lync Server 2010 or mixed –Lync Server 2010 topology has already been prepared, no additional steps are necessary for a UCMA 3.0 application that has been deployed against Lync Server 2010 following the activation steps described in [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md). The trusted service entries and contact object entries created using the trusted application cmdlets are created in a backward-compatible fashion so that users homed on earlier Communications Server versions will be able to communicate with the application.

The following sections describe each of the UCMA 3.0 application coexistence scenarios.

## Coexistence of a UCMA 3.0 Core application in an Office Communications Server 2007 R2—Microsoft Lync Server 2010 topology

If a UCMA 3.0 application is deployed in a coexistence topology using trusted application cmdlets, no additional steps are necessary. In this scenario, users are able to communicate with the application.

For more information about activating a UCMA 3.0 application, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

## Coexistence of a UCMA 3.0 Core application in an Office Communications Server 2007—Microsoft Lync Server 2010 topology

Before testing this scenario, make sure that all of the latest patches for UCMA 2.0 have been applied to the pool of computers. The following knowledge base articles describe the patches and provided links to the update packages: [KB949935](https://support.microsoft.com/kb/949935), [KB945053](https://support.microsoft.com/kb/945053), [KB942872](https://support.microsoft.com/kb/942872), and [KB956389](https://support.microsoft.com/kb/956389).

Deploy the UCMA 3.0 application in the coexistence topology, using the trusted application cmdlets.

If a user is unable make an IM or audio call to a UCMA 3.0 trusted application’s contact object, the following command might be helpful. This command forces a user replication, which replicates the application contact object information to OCS. osql.exe -S (local)\\rtc -d rtc -E -Q "exec UrRegenerate"

> [!NOTE]
> The preceding command is case-sensitive.

For more information about activating a UCMA 3.0 application, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

## Direct deployment of a UCMA 3.0 Core application in a pure Office Communications Server 2007 R2 topology

There is only limited support for UCMA 3.0 applications deployed in a pure topology. Specifically, only endpoints of type [UserEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.userendpoint?view=ucma-api) are supported; applications that use endpoints of type [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) are not supported. Only person-to-person IM and IM conferencing are supported; audio is not supported. IM Conference Scheduling is supported, escalating a two-party IM call to an IM conference is supported, and IM conference joining are supported.

In general, a user object or contact object for a UCMA 3.0 application that is not homed on Lync Server 2010 or later, is not supported.

UCMA 3.0 features that depend on Lync Server 2010 are not supported if the application is deployed against .

Because the UCMA 3.0 application is deployed against , it should be deployed as if it were a UCMA 2.0 application. For more information, see [Deploying a UCMA 2.0 Core Application](https://msdn.microsoft.com/en-us/library/dd280155\(v=office.13\).aspx).


---
title: Troubleshooting media connectivity issues
TOCTitle: Troubleshooting media connectivity issues
ms:assetid: 4476d182-c84a-4ecc-be1a-a8291b21c995
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466064(v=office.16)
ms:contentKeyID: 65240001
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Troubleshooting media connectivity issues


_**Applies to:** Skype for Business 2015_

Several classes that are part of Microsoft Unified Communications Managed API 5.0 that can be used to provide information on call failures due to media connectivity errors. These classes can help to alert administrators of potential deployment problems.

## MediaTroubleshootingDataReportedEventArgs class

The [MediaTroubleshootingDataReportedEventArgs](https://msdn.microsoft.com/en-us/library/hh349570\(v=office.16\)) class provides troubleshooting data to the application, regarding a media connectivity error that involves a remote, external Microsoft Skype for Business 2015 call (a corporate user who is not logged into the corporate network).

An application can register a handler for the [MediaTroubleshootingDataReported](https://msdn.microsoft.com/en-us/library/hh383527\(v=office.16\)) event on the [AudioVideoCall](https://msdn.microsoft.com/en-us/library/hh383901\(v=office.16\)) class. The second parameter of the handler is of type [MediaTroubleshootingDataReportedEventArgs](https://msdn.microsoft.com/en-us/library/hh349570\(v=office.16\)).

The [MediaChannelEstablishmentDataCollection](https://msdn.microsoft.com/en-us/library/hh382882\(v=office.16\)) property on a **MediaTroubleshootingDataReportedEventArgs** instance returns a list of [MediaChannelEstablishmentData](https://msdn.microsoft.com/en-us/library/hh383850\(v=office.16\)) instances.

## Media connectivity troubleshooting

After the call terminates, determine whether there was a media connectivity problem by checking the [EstablishmentStatus](https://msdn.microsoft.com/en-us/library/hh383434\(v=office.16\)) property on a [MediaChannelEstablishmentData](https://msdn.microsoft.com/en-us/library/hh383850\(v=office.16\)) object. An **EstablishmentStatus** value of **Failed** indicates there was a problem, while a value of **Succeeded** indicates that media establishment occurred successfully.

If the value of the **EstablishmentStatus** property is **Failed**, the application can call the [GetDiagnosticsReason](https://msdn.microsoft.com/en-us/library/hh384673\(v=office.16\)) method to obtain diagnostics information. The **GetDiagnostics** method returns a value of the [MediaChannelEstablishmentDiagnosticsReason](https://msdn.microsoft.com/en-us/library/hh383063\(v=office.16\)) enumeration.

  - If the value returned by **GetDiagnosticsReason** is either **MediaEdgeAuthenticationServiceDiscoveryFailed** or **MediaEdgeAuthenticationServiceCredentialsAquisitionFail**, raise "Alert 1".

  - If the value returned by **GetDiagnosticsReason** is **MediaEdgeResourceAllocationFailed**, raise "Alert 2".

The following are suggested alert messages for these two alerts:

Alert 1: "Skype for Business Server 2015 calls involving remote users (located outside the Enterprise) might be failing using the current Skype for Business Server 2015 Audio/Video Edge resources. Please contact the appropriate administrator and verify that the Skype for Business Server 2015 Audio/Video Authentication Service is properly configured and operational."

Alert 2: "Skype for Business Server 2015 calls involving remote users (located outside the Enterprise) might be failing using the current Skype for Business Server 2015 Audio/Video Edge resources. Please contact the appropriate administrator and verify that the Skype for Business Server 2015 Audio/Video Edge is properly configured and operational.


> [!NOTE]
> <P>An application should implement a throttling mechanism based on specific alert keys so that warnings do not flood the system.</P>



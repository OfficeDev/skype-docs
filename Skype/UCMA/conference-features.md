---
title: Conference features
TOCTitle: Conference features
ms:assetid: 73465dfc-ab21-4659-bb4f-f806079e3309
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465958(v=office.16)
ms:contentKeyID: 65239792
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Conference features

**Applies to**: Skype for Business 2015

The topics in this section summarize the most important new or changed conferencing features in Microsoft Unified Communications Managed API 5.0. These features can be divided into three broad areas:

- Conferencing infrastructure

- [Conference scheduling](pre-meeting-conference-scheduling.md)

- [Joining the conference and the in-conference experience](in-meeting-during-the-conference.md)

## Conferencing infrastructure

To learn how UCMA 5.0 Conferencing API works, it is essential to first understand the underlying infrastructure. All conferences are scheduled by the use of a Skype for Business Server 2015 Front End Focus Factory and stored in the User Services database. The Focus stands for the central point of conferencing command and control. It runs on every instance of a Skype for Business Server 2015 Front End. 

The Multipoint Conferencing Units (MCUs) for chat, application, and desktop sharing, or data collaboration usually run on the Front End, although the Audio Video MCU runs on a separate server role of the Skype for Business Server 2015 architecture. The MCU Factory is responsible for allocating MCUs to an activated Conference and monitoring their health. 

The following illustration shows a typical conferencing topology.

![Typical conferencing topology](images/Dn465958.UCMA_ConfTopology(Office.16).png "Conference Topology")

<br/>

The following illustration shows a logical view of Skype for Business Server 2015, together with the protocols that are used.

![Conference logical topology and protocols](images/Dn465958.UCMA_ConfLogicalTopology(Office.16).png "Conference logical topology")

<br/>




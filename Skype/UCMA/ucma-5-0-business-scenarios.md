---
title: UCMA 5.0 business scenarios
TOCTitle: UCMA 5.0 business scenarios
ms:assetid: 31d51c95-b05e-4b90-ade4-7036af4d8241
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465936(v=office.16)
ms:contentKeyID: 65239778
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA 5.0 business scenarios


_**Applies to:** Skype for Business 2015_

**In this article**  
Contact center  
Standalone IVR application  
Voice companion  
Alert notification system  
Other scenarios  
In this section  

Microsoft Unified Communications Managed API 5.0 can be used to create a number of different types of application. In this topic, four of the most important application types are presented.

## Contact center

A contact center application is a central point in an enterprise from which all customer contacts are managed. This type of application is generally a part of an enterprise’s overall customer relationship management, and typically includes one or more online call centers. A contact center is typically provided with special software that allows contact information to be routed to appropriate people, contacts to be tracked, and data to be gathered.

Besides the traditional contact center features such as supervisor silent monitoring, whispering, coaching, or supervisor barge-in, call recording, data collection through an interactive voice response (IVR) system or Web chat, the UCMA 5.0 API enables Customer Care scenarios such as contextual communications for e-commerce or helpdesk, allowing the end user’s context to be enriched with CRM data, cross-channel communications that allow the customer to choose how to communicate with a business, and "anywhere access" that allows the customer to communicate from any device.

The following illustration describes the entities involved in a contact center application.

![Contact Center](images/Dn465936.UCMA-ContactCenter1(Office.16).png "Contact Center")

UCMA APIs provide all of the functionality needed to implement a typical contact center solution, including the following capabilities:

  - Route incoming communications to available agents with matching skills.

  - Use Enhanced Presence in Informal Agent switch-boarding (skills-based routing).

  - Create multimodal helpdesk scenarios, such as back-to-back user agent.

For more information, see [Contact center](contact-center.md).

## Standalone IVR application

The following illustration describes the entities involved in a standalone IVR application.

![Standalone IVR](images/Dn465936.UCMA-StandaloneIVR1(Office.16).png "Standalone IVR")

For more information, see [Standalone IVR application](standalone-ivr-application.md).

## Voice companion

A Voice Companion application makes it easier to expose an enterprise application through a unified voice portal, including unified messaging, voice mail playback, *ad hoc* conferencing, and contact lists. The following illustration describes the entities involved in a voice companion application.

![Personal virtual assistant](images/Dn465936.UCMA-PVA1(Office.16).png "Personal virtual assistant")

This type of worker’s is frequently "on the go." As a result, there are a variety of mobile devices that are used, including cellular telephones, Research In Motion (RIM) devices, personal digital assistants (PDAs), tablet computers, and laptop computers.

UCMA APIs can be used to create a Voice Companion application by enabling developers to develop a backend service that:

  - Integrates with .

  - Simplifies mobile-user communication scenarios using ordinary phones.

  - Takes advantage of presence-based communications.

For more information, see [Voice companion](voice-companion.md).

## Alert notification system

UCMA allows a user to be notified of changing conditions to be able to react in real time. IM automata and IVRs are primarily used to notify one or more users that an event has occurred, by the fastest means available. The canonical example is the stock ticker: when a stock has crossed a certain threshold, a flood of notifications can be sent to all users signed up to receive that notification.

The following illustration describes the entities involved in an alert notification system application.

![Alerts and Notifications](images/Dn465936.UCMA-Alerts(Office.16).png "Alerts and Notifications")

For more information, see [Alert notification system](alert-notification-system.md).

## Other scenarios

Besides the scenarios already listed, developers can use UCMA 5.0 to create applications for several other scenarios. For more information, see [Additional scenarios](additional-scenarios.md).

## In this section

  - [Contact center](contact-center.md)

  - [Voice companion](voice-companion.md)

  - [Standalone IVR application](standalone-ivr-application.md)

  - [Alert notification system](alert-notification-system.md)

  - [Additional scenarios](additional-scenarios.md)


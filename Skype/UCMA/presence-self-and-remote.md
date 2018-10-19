---
title: Presence - self and remote
TOCTitle: Presence - self and remote
ms:assetid: 13b41e4f-dff6-46fc-83d9-0ae9ad651a17
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465964(v=office.16)
ms:contentKeyID: 65239882
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Presence - self and remote


_**Applies to:** Skype for Business 2015_

Entities in a Skype for Business 2015 subsystem can have up to eight active endpoints at a given time (Multiple Points of Presence) that can each publish presence information and subscribe to the presence of others.

As can be seen in the following illustration, Alicia can be logged into multiple devices at the same time. These devices might include an Skype for Business 2015 endpoint, and a device, as well as presence from a mobile device. Each endpoint updates her presence information in Skype for Business Server 2015. Upon receiving an update, the server determines whether and which of the endpoint owner’s subscribers should be informed of this change and notifies them accordingly. In addition, all of Alicia’s other endpoints are informed as well if they choose to receive the information.

In Microsoft Unified Communications Managed API 5.0, applications can be developed to support a fully functional endpoint with presence functionalities. The primary presence functions and the related classes are described in this topic.

![Presence functions and related classes](images/Dn465964.LocalOwner_RemotePresence(Office.16).png "Presence functions and related classes")

## Publishing presence

The primary entry point for managing the endpoint owner’s presence is the **LocalOwnerPresence** property on the endpoint. This property provides access to a [LocalOwnerPresence](https://msdn.microsoft.com/en-us/library/hh382370\(v=office.16\)) instance, whose members can be used to carry out such tasks as publishing presence, managing relationship levels with subscribers, and acknowledging subscription requests from remote presentities.

An application can also enable presence at the time an endpoint is established. The **PresenceServices** property on an endpoint provides access to a simplified API that can be used at that time.

For more information, see [LocalOwnerPresence](https://msdn.microsoft.com/en-us/library/dd279776\(v=office.16\)).

## Subscribing to remote presence

An endpoint can discover the presence information of another Skype for Business 2015 presentity by using an instance of the [RemotePresenceView](https://msdn.microsoft.com/en-us/library/hh381152\(v=office.16\)) class. The mode of subscription (persistent versus polled) can be set, as well as the frequency of updates for a polled subscription. Developers can specify the subscription mode for an endpoint, choosing between subscribing for changes in presence in real time or receiving updates by polling at regular intervals, and can also choose, in some cases, the nature of the data to be received.

An endpoint can also make one-time presence queries.

For more information, see [RemotePresenceView](remotepresenceview.md).


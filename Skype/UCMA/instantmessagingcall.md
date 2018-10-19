---
title: InstantMessagingCall
TOCTitle: InstantMessagingCall
ms:assetid: e3b82993-177c-4c66-b801-b69f17e1e022
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466023(v=office.16)
ms:contentKeyID: 65239959
ms.date: 07/27/2015
mtps_version: v=office.16
---

# InstantMessagingCall


_**Applies to:** Skype for Business 2015_

An [InstantMessagingCall](https://msdn.microsoft.com/en-us/library/hh161841\(v=office.16\)) object represents an SDP-based instant messaging (IM) session between a local participant and a remote participant. An **InstantMessagingCall** object cannot exist on its own; it must be associated with a [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) object.

Because an IM call can be forked by Skype for Business Server 2015, the receiving end should have logic to determine which endpoint should accept the IM call. Normally, this process is done by the most active endpoint as determined by the device state published by all of the endpoints. If there is only one endpoint, the IM can be autoaccepted immediately. Otherwise, the user should be given the opportunity to click the toast message so that the IM can be accepted on that endpoint. If the user does not click any of the toast messages within 10 seconds, the most active endpoint should accept the IM. This allows the message to flow so that the user can see the missed message when returning to the device.

When an IM call is established, it is customary to include a "toast" message, which is generally a text/plain message.


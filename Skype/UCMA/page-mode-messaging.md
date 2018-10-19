---
title: Page-mode messaging
TOCTitle: Page-mode messaging
ms:assetid: c05a6e23-3fb2-4d4b-a48e-eaeb576fdeff
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466059(v=office.16)
ms:contentKeyID: 65239991
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Page-mode messaging


_**Applies to:** Skype for Business 2015_

In UCMA 5.0, an endpoint can send messages to another endpoint without establishing a signaling session. An operation of this type is referred to as *page-mode messaging*. In page mode, a message is delivered from the source to the target without a handshake being required between the two endpoints. UCMA 5.0 exposes the [SendMessage()](https://msdn.microsoft.com/en-us/library/hh350225\(v=office.16\)) overloaded methods for sending a page-mode message synchronously, and the [BeginSendMessage()](https://msdn.microsoft.com/en-us/library/hh349151\(v=office.16\)) overloaded methods and the [EndSendMessage(IAsyncResult)](https://msdn.microsoft.com/en-us/library/hh382471\(v=office.16\)) method for sending a page-mode message asynchronously.

The recipient of a page-mode message must subscribe to the [MessageReceived](https://msdn.microsoft.com/en-us/library/hh350010\(v=office.16\)) event in order to receive this type of message.

For more information, see [Sending and receiving page-mode messages](sending-and-receiving-page-mode-messages.md).


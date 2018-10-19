---
title: Queue usage model
TOCTitle: Queue usage model
ms:assetid: 7179d835-5fe3-4703-8b40-0245fd6ccea0
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466081(v=office.16)
ms:contentKeyID: 65240012
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Queue usage model


_**Applies to:** Skype for Business 2015_

Microsoft Unified Communications Managed API 5.0 serializes work items for raising events and invoking method callbacks into queues. The following information describes how UCMA 5.0 uses these queues and provides guidelines for how applications should use them:

  - A worker thread is used to invoke the work items in the queue. Since the work items are processed one by one, subsequent items are not processed until the first item is done. For this reason the application should not block the thread that raised an event or invoked a callback by making synchronous calls to operations that require excessive time.

  - Due to performance reasons, each conversation in UCMA 5.0 uses a separate queue. Each endpoint, together with the endpoint’s **ConferenceServices** instance, has its own queue, as does the **CollaborationPlatform**.

  - Incoming calls and conference invitations for a conversation will be raised to the application using the conversation’s queue. This is done so that the platform does not serialize these calls and invitations across conversations, thereby impairing performance.

  - All components used in a conversation will share the conversation’s queue for their events and callbacks. This includes calls, conference invitations, conference session, MCU sessions, and media flows. However, there is no requirement for other media provider components, such as player or recorder, to use the conversation queue. This is true especially because such components might be shared across conversations.

  - After the conversation terminated event is raised, there should be no other event that is raised from the conversation queue. However, work items for callbacks will be placed in the queue if these correspond to operations invoked by the application after the conversation was terminated.

  - Events and callbacks are ordered in a way that makes sense for the application. All events raised as a side effect of an operation are placed in the queue before the work item to invoke the callback method for the operation. For example, the state changed event for "Established" is raised before the Establish operation completes. The terminated events for all objects associated with a conversation instance are placed in the queue before the terminated event for the conversation itself. There is no guaranteed order implied between two different components, such as for two calls in the same conversation.

  - Since each conversation and endpoint uses a single queue, when an application terminates the endpoint without terminating the conversations of that endpoint, there is no order guaranteed between the terminated events of the conversation and the endpoint. If the application is not prepared to handle this, we recommend that the application terminate all conversations before terminating the endpoint.


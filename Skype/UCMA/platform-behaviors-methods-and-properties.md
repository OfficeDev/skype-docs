---
title: Platform behaviors (methods and properties)
TOCTitle: Platform behaviors (methods and properties)
ms:assetid: 2c50e201-69ad-4cf0-a506-91f3eca18e62
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466082(v=office.16)
ms:contentKeyID: 65240013
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Platform behaviors (methods and properties)


_**Applies to:** Skype for Business 2015_

The following describes general UCMA 5.0 platform behavior:

  - **BeginXxx** methods can throw **InvalidOperationException** if the state of the object does not permit the operation to be carried out. An application is expected to have a catch block for all **BeginXxx** method calls to ensure that a worker thread does not throw such an exception, thereby causing the application to crash.

  - Every method should specify in its XML comments all known exceptions that could be thrown by that method. Because a method invocation involves a tree of method calls, it is not possible to enumerate all possible exceptions. However, every attempt has been made to ensure that UCMA 5.0 methods throw only **RealtimeException** (or a class derived from this one), **InvalidOperationException**, **RealtimeInvalidOperationException**, or argument exceptions such as **ArgumentException**, **ArgumentNullException**.

  - All operations that are created as a result of a method invocation must complete within the time appropriate for the method, irrespective of how an application is written. Some operations depend on the behavior of a remote endpoint. For example, an INVITE operation can take a long time if the remote endpoint sends a "180 Ringing" response periodically without ever accepting the invitation. It is the application’s responsibility to create its own timers for such operations.

  - A method that depends on an extensible media provider can hang due to a poor implementation, such as by not completing an asynchronous operation exposed by the provider. It is the responsibility of the provider to ensure that their implementation meets the requirements discussed here and to follow any other .NET guidelines.

  - The **EndXxx** method will not throw **InvalidOperationException**. It can throw **ArgumentException** or **RealtimeException** or any exception that is derived from **RealtimeException**.

  - A component will not discard events if the event handler is null but the application did not have an opportunity to register for such events. The component is responsible for queuing a work item that will raise the event at the time the work item is processed. If the event handler is still null at the time the work item is processed, the event will not be raised.

  - If a method is expected to cause certain state changes in a component, all of these events will be placed in the queue ahead of the completion callback. For example, if a contact/group subscription session is established, all contacts and group events associated with the subscription will be raised before the method callback is invoked. The component will reflect the most recent state while the state transition events are placed in the queue. Thus, the application cannot expect the state of a component to match that of the event reported in the event handler.

  - The platform will not raise an event or invoke a method callback while holding a platform lock.

  - None of the public objects of the platform will be used by the platform for locking unless explicitly called out.

  - If the platform exposes a collection to the application, the collection is a snapshot of the actual internal collection; hence it should be safe for the application to enumerate that collection. Any changes to the original collection after the snapshot has been obtained will not be reflected in the collection returned to the application. Thus, it is recommended that the application use events to keep track of collections rather than to access the property that returns a snapshot.

  - When an asynchronous operation is completed, the platform will not keep any reference to the asynchronous result that corresponds to the operation.


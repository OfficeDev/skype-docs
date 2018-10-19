---
title: Application behavior
TOCTitle: Application behavior
ms:assetid: eecd7cfa-21a2-4b86-ae1d-66a6324d3f26
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466077(v=office.16)
ms:contentKeyID: 65240015
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Application behavior


_**Applies to:** Skype for Business 2015_

The following discusses general application behavior:

  - Application developers should make every effort to ensure that event handlers and method callbacks do not throw exceptions back to the platform. The platform does not have code to catch exceptions that are uncaught by an application, so any such exceptions that are thrown have the potential to cause the application to crash.

  - If the application blocks a worker thread that is raising an event or invoking a callback, subsequent events will not be delivered until the callback returns. However, this behavior will not interfere with the completion of any pending operations.

  - An application should avoid blocking inside an event handler or callback.

  - The application should catch **InvalidOperationException** when calling any **BeginXxx** method of the platform.

  - The application should not call **EndXxx** more than once.

  - Some objects will not be garbage collected until the application terminates them. For example, an endpoint will maintain strong references to conversations. Unless a conversation is terminated, it will not be released from the endpoint. The application is responsible for terminating any such components.

  - Some components might terminate themselves under certain circumstances, causing a state change event to inform the application that the component has been terminated. In such cases, the application need not terminate the component again. However, if an application chooses to terminate the component again, it is free to do so, and the termination operation is expected to work. For example, a client conversation will terminate itself when the last established call is terminated.

  - If the platform exposes a collection to the application, what is returned is a snapshot of the actual internal collection; hence it should be safe for the application to enumerate that collection. Any changes that are made to the original collection after getting the snapshot will not be reflected in the collection returned to the application. Thus, it is recommended that the application use events to keep track of collections rather than access the property that returns a snapshot.

  - The platform exposes a static class called **UnhandledExceptionManager** with a single property that represents a delegate. The application is expected to set this delegate so that the platform can report unhandled exceptions that occur in worker threads used by the application. If the delegate returns true, such exceptions are ignored. Otherwise, the exception is rethrown, potentially resulting in a process crash.


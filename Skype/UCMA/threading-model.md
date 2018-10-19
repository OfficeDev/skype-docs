---
title: Threading model
TOCTitle: Threading model
ms:assetid: abdf2e3d-c235-4ff9-b8b0-9b709e7b22f8
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466080(v=office.16)
ms:contentKeyID: 65240011
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Threading model


_**Applies to:** Skype for Business 2015_

Microsoft Unified Communications Managed API 5.0 is designed be multi-thread safe. Some objects that are used to configure settings or options or that are used to contain data passed into an API are exceptions.

  - An application can invoke methods from different threads. In addition, the platform can be performing work as a result from interacting with remote applications. The application should be prepared to handle exceptions as described in [Platform behaviors (methods and properties)](platform-behaviors-methods-and-properties.md).

  - UCMA 5.0 will raise events and invoke method callbacks using worker threads. If an application throws an exception in one of the event handlers or callback methods, the worker thread can crash unless the application registers for handling unhandled exceptions. For more information, see [UCMA 5.0 exception model](ucma-5-0-exception-model.md).

  - When an operation is invoked on an object that is already terminated, the callback associated with that operation can be invoked in the application thread itself. When this happens, the **CompletedSynchronously** property is set to true. For more information about **CompletedSynchronously**, see [IAsyncResult](http://msdn2.microsoft.com/en-us/library/ft8a6455).

  - The application thread will not be used by UCMA 5.0 to perform internal operations that take a long time.


---
title: Asynchronous pattern
TOCTitle: Asynchronous pattern
ms:assetid: 74da9223-e635-43cf-9e98-d9c7f8a6be38
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466078(v=office.16)
ms:contentKeyID: 65240010
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Asynchronous pattern


_**Applies to:** Skype for Business 2015_

Microsoft Unified Communications Managed API 5.0 is designed for middle-tier applications, for which performance is one of the most important goals. To provide this performance and to be consistent with the previous releases,UCMA 5.0 supports the **BeginXxx**/**EndXxx** pattern to implement asynchronous operations. The application programmer is expected to be familiar with this usage pattern. For more information, see [Asynchronous Programming Overview](http://msdn.microsoft.com/en-us/library/ms228963.aspx).

  - UCMA 5.0 supports the extensibility of the **Call** and **MediaProvider** classes. Those who provide these extensions are expected to provide an API that is consistent with UCMA 5.0. This includes implementing asynchronous operations using **BeginXxx**/**EndXxx** methods, and using queue mechanisms as defined in [Queue usage model](queue-usage-model.md). For more information about extending the **Call** and **MediaProvider** classes, see the [Extending the UCMA platform](extending-the-ucma-platform.md) group of topics.

  - UCMA 5.0 optimizes the implementation of asynchronous operations by not creating a wait handle when the application passes a callback. It is strongly recommended that applications pass a callback method rather than wait on a wait handle. Typically, the **EndXxx** method is invoked in the callback when it is called after the associated **BeginXxx** operation has finished. However, if the application chooses to wait on the handle in the **IAsyncResult** returned from the **BeginXxx** method, it can do so and the wait handle is created using lazy initialization.

  - If the application does not pass a callback or access the wait handle, the implicitly created wait handle created is disposed after the **EndXxx** method is invoked.


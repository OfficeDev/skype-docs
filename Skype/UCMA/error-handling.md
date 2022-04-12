---
title: Error handling (Unified Communications Managed API 5.0)
description: A developer who implements a subclass of the MediaProvider class must be aware of the exceptions that Microsoft Unified Communications Managed API expects.
TOCTitle: Error handling
ms:assetid: 039427ca-d9f6-4b31-986c-23db1850446c
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466107(v=office.16)
ms:contentKeyID: 65240024
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Error handling


**Applies to**: Skype for Business 2015

A developer who implements a subclass of the [MediaProvider](/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaprovider?view=ucma-api) class must be aware of the exceptions that Microsoft Unified Communications Managed API (UCMA) expects.

## OfferAnswerException

A [MediaProvider](/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaprovider?view=ucma-api) subclass implementation can throw [OfferAnswerException](https://msdn.microsoft.com/library/hh382722\(v=office.16\)) in [EndGetOffer](https://msdn.microsoft.com/library/hh382852\(v=office.16\)) and [EndGetAnswer(IAsyncResult)](https://msdn.microsoft.com/library/hh383856\(v=office.16\)). The [FailureReason](https://msdn.microsoft.com/library/hh384728\(v=office.16\)) property on this exception contains information about why the exception was thrown.

## Other exceptions

In addition to **OfferAnswerException**, the developer should also be aware of the following exceptions:

1.  A **Begin***Xxx* method should throw only [ArgumentException](https://msdn.microsoft.com/library/3w1b3114) or [InvalidOperationException](https://msdn.microsoft.com/library/2asft85a).
<br><br>**Begin***Xxx* can throw **InvalidOperationException** if an operation is not valid in the current state of the **MediaProvider** subclass implementation. The associated **Call** subclass should catch the exception and take appropriate action, depending on the operation state. Appropriate actions include terminating the call or retrying the operation.
<br><br>A custom implementation of a **Call** subclass should not receive [ArgumentException](https://msdn.microsoft.com/library/3w1b3114) in general, because **Call** never passes a null value of **OfferAnswerContext** or **CallContext**. The **MediaProvider** can very well assert the value on any API where **OfferAnswerContext** is passed. The **Call** class has special logic for handling an **OfferAnswerException** exception, which is shown in the previous section of this topic.

2.  An **End***Xxx* method can throw only the exceptions that are derived from the [RealTimeException](https://msdn.microsoft.com/library/hh385103\(v=office.16\)) class:
    
      - [CallOperationFailureException](https://msdn.microsoft.com/library/hh382522\(v=office.16\))
    
      - [ConferenceFailureException](https://msdn.microsoft.com/library/hh382829\(v=office.16\))
    
      - [OfferAnswerException](https://msdn.microsoft.com/library/hh382722\(v=office.16\))
    
      - [ProvisioningFailureException](https://msdn.microsoft.com/library/hh385160\(v=office.16\))
    
      - [ConnectionFailureException](https://msdn.microsoft.com/library/hh161695\(v=office.16\))
    
      - [FailureRequestException](https://msdn.microsoft.com/library/hh382870\(v=office.16\))
    
      - [FailureResponseException](https://msdn.microsoft.com/library/hh383231\(v=office.16\))
    
      - [AuthenticationException](https://msdn.microsoft.com/library/hh382813\(v=office.16\))
    
      - [PublishSubscribeException](https://msdn.microsoft.com/library/hh384897\(v=office.16\))
    
      - [RegisterException](https://msdn.microsoft.com/library/hh349227\(v=office.16\))
    
      - [ServerPolicyException](https://msdn.microsoft.com/library/hh349401\(v=office.16\))
    
      - [MessageParsingException](https://msdn.microsoft.com/library/hh365619\(v=office.16\))
    
      - [OperationFailureException](https://msdn.microsoft.com/library/hh161725\(v=office.16\))
    
      - [OperationTimeoutException](https://msdn.microsoft.com/library/hh380900\(v=office.16\))
    
      - [TlsFailureException](https://msdn.microsoft.com/library/hh366193\(v=office.16\))

3.  The [BeginTerminateMedia(CallDialogContext, Boolean, AsyncCallback, Object)](https://msdn.microsoft.com/library/hh350188\(v=office.16\)) can be called multiple times in UCMA and should not throw an exception if mediaSession is already terminated. If the media session is already terminated, **EndTerminateMedia** should be completed as a NOOP.


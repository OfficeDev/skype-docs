---
title: Call forking with early media
TOCTitle: Call forking with early media
ms:assetid: 1a0386ac-1bd1-4003-9222-d9e14251957b
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466104(v=office.16)
ms:contentKeyID: 65240023
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Call forking with early media


_**Applies to:** Skype for Business 2015_

An outgoing call can be forked to multiple endpoints. When an outgoing call is forked, the caller receives multiple provisional responses with response code 183 (Session Progress). A 183 response can contain an SDP body to establish an early dialog. In this case the [SetAnswer(OfferAnswerContext, SdpOffer, SdpAnswer)](https://msdn.microsoft.com/en-us/library/hh382509\(v=office.16\)) method is invoked on the [MediaProvider](https://msdn.microsoft.com/en-us/library/hh383767\(v=office.16\)) for each response. The [CallDialogContext](https://msdn.microsoft.com/en-us/library/hh383382\(v=office.16\)) property represents the dialog context for the response.

When **SetAnswer** is invoked for a Provisional answer, the [Status](https://msdn.microsoft.com/en-us/library/hh382499\(v=office.16\)) property will be **SdpAnswerStatus.Provisional**. After the call receives the final 200 OK response, the **SetAnswer** method is invoked with the final answer. In this case the [CallDialogContext](https://msdn.microsoft.com/en-us/library/hh383382\(v=office.16\)) property on the [OfferAnswerContext](https://msdn.microsoft.com/en-us/library/hh382841\(v=office.16\)) instance represents the final confirmed dialog and **Status** property will be **SdpAnswerStatus.Final**.


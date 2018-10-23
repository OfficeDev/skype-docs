---
title: Negotiating declined offers
TOCTitle: Negotiating declined offers
ms:assetid: bbca6041-4d98-4d0f-b81d-8978939fbaef
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466100(v=office.16)
ms:contentKeyID: 65240021
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Negotiating declined offers

**Applies to**: Skype for Business 2015

This topic describes a scenario that can occur in offer/answer negotiation. The first scenario describes how an endpoint should handle an initial outgoing multipart/mime SDP offer that is declined by a remote endpoint. The second scenario describes how an endpoint should decline an incoming SDP offer that it cannot accept and how it should handle an outgoing SDP offer that is declined by the remote endpoint.

## Handling an initial multipart/mime offer that is declined

When an initial outgoing INVITE message contains multipart/mime content with the SDP offer, and the remote endpoint declines the offer with response code 415 (the **UnsupportedMedia** value in the [OfferAnswerFailureReason](https://msdn.microsoft.com/en-us/library/hh348371\(v=office.16\)) enumeration), the **Call** instance can automatically retry SDP negotiation using either of the following means:

- If the original offer consists of only one offer or the remote endpoint supports multipart/alternate, the **Call** instance can exclude the custom optional mime parts (in the [CustomMimeParts](https://msdn.microsoft.com/en-us/library/hh348612\(v=office.16\)) property specified in a [CallEstablishOptions](https://msdn.microsoft.com/en-us/library/hh381079\(v=office.16\)) instance) and can retry the INVITE with the same offer.

- If the original SDP offer given by the [MediaProvider](https://msdn.microsoft.com/en-us/library/hh383767\(v=office.16\)) subclass instance contains more than one SDP and the remote endpoint does not support multipart/alternate, the **Call** class should call the [SetAnswer(OfferAnswerContext, SdpOffer, SdpAnswer)](https://msdn.microsoft.com/en-us/library/hh382509\(v=office.16\)) method on the **MediaProvider** subclass instance with an answer status (the [Status](https://msdn.microsoft.com/en-us/library/hh382499\(v=office.16\)) property on the [SdpAnswer](https://msdn.microsoft.com/en-us/library/hh349319\(v=office.16\)) instance) of **UnsupportedMediaCanRetry**, a value in the [SdpAnswerStatus](https://msdn.microsoft.com/en-us/library/hh383245\(v=office.16\)) enumeration.
    
  The **Call** class should then call **BeginGetOffer** to request another offer from the **MediaProvider** instance.

## Declining an incoming SDP offer and handling a declined offer

When the **MediaProvider** subclass instance is unable to accept the offer given by the remote endpoint, it should throw an [OfferAnswerException](https://msdn.microsoft.com/en-us/library/hh382722\(v=office.16\)) exception in its implementation of the [EndGetAnswer(IAsyncResult)](https://msdn.microsoft.com/en-us/library/hh383856\(v=office.16\)) method, with the specific reason set in the [FailureReason](https://msdn.microsoft.com/en-us/library/hh384728\(v=office.16\)) property. 

Similarly, if the remote endpoint declines the offer, the **SetAnswer** method will be called with a null SDP answer. The **Status** property on the **SdpAnswer** instance should contain the failure reason based on the response code.


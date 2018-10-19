---
title: Triggering SDP renegotiations
TOCTitle: Triggering SDP renegotiations
ms:assetid: 97940a24-9156-4741-9b7a-7245efc536b0
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466102(v=office.16)
ms:contentKeyID: 65240022
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Triggering SDP renegotiations


_**Applies to:** Skype for Business 2015_

The [BeginSdpRenegotiation(CallDialogContext, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh384458\(v=office.16\)) method on the **MediaProvider** instance can be used for several purposes, such as when an application places a call on hold or retrieves it, or when media types are added or removed. If an incoming SDP renegotiation is in progress when this method is called, the requested SDP renegotiation will begin after the pending renegotiation is finished. The **MediaProvider** instance should expect the [BeginGetOffer(OfferAnswerContext, Collection\<SdpContentDescription\>, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh366051\(v=office.16\)) method on the **MediaProvider** instance to be called after **BeginSdpRenegotiation** has started.

After the call to **BeginSdpRenegotiation**, the call flow is similar to the call flow of an outgoing call with normal offer/answer. For more information, see [Negotiating declined offers](negotiating-declined-offers.md).


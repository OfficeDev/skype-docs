---
title: Extending the MediaProvider class
TOCTitle: Extending the MediaProvider class
ms:assetid: c3f53f4e-561d-4b00-bebd-8db4f3f1f0cc
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466099(v=office.16)
ms:contentKeyID: 65240019
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Extending the MediaProvider class


_**Applies to:** Skype for Business 2015_

A call (represented in the UCMA 5.0 as a [Call](https://msdn.microsoft.com/en-us/library/hh384235\(v=office.16\)) subclass) serves as a SIP signaling control, while a [MediaProvider](https://msdn.microsoft.com/en-us/library/hh383767\(v=office.16\)) subclass is the media counterpart that is responsible for SDP negotiation, as described in RFC 3264, An Offer/Answer Model with the Session Description Protocol (SDP). A **MediaProvider** subclass also is responsible for creating and managing a media flow, which can be implemented in a [MediaFlow](https://msdn.microsoft.com/en-us/library/hh366262\(v=office.16\)) subclass. A **MediaFlow** subclass implementation must provide the protocol implementation of the actual media exchange and customized behavior for exchanging media of a specific type. For more information, see [Extending the MediaFlow class](extending-the-mediaflow-class.md).

A **MediaProvider** subclass is responsible for creating the media flow (an instance of a **MediaFlow** subclass) and making it available to the application. Generally, during offer/answer negotiation, the **MediaProvider** subclass should create a new flow if one has not been created for a given context. The flow can then be made available to the application by the [RaiseFlowConfigurationRequestedHandler](https://msdn.microsoft.com/en-us/library/hh350168\(v=office.16\)) method on the **MediaProvider** subclass instance.

## MediaProvider class abstract property

A developer who creates a **MediaProvider** subclass must provide a definition for only one **MediaProvider** property, **SupportedMediaTypes**. This property returns a list of the media types supported by the custom media provider, similar to the property of the same name on the **Call** class.

    public abstract IEnumerable<string> SupportedMediaTypes { get; }


> [!WARNING]
> <P>The <STRONG>MediaProvider</STRONG> and <STRONG>Call</STRONG> classes both have a <STRONG>SupportedMediaTypes</STRONG> property. Although these properties have the same name and purpose, their return types are different.</P>



## Abstract methods on the MediaProvider class

In addition to the previously mentioned property, the developer must provide definitions for the following seven **MediaProvider** methods.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Method</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh384479(v=office.16)">BeginGetAnswer(OfferAnswerContext, SdpOffer, AsyncCallback, Object)</a></p></td>
<td><p>Gets an answer from the <strong>MediaProvider</strong> implementation for the incoming offer.</p>
<p>protected abstract IAsyncResult BeginGetAnswer(OfferAnswerContext offerAnswerContext, SdpOffer offer, AsyncCallback userCallback, object state)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh383856(v=office.16)">EndGetAnswer(IAsyncResult)</a></p></td>
<td><p>Completes the operation started by <strong>BeginGetAnswer</strong>.</p>
<p>protected abstract SdpAnswer EndGetAnswer(IAsyncResult result)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh366051(v=office.16)">BeginGetOffer(OfferAnswerContext, Collection&lt;SdpContentDescription&gt;, AsyncCallback, Object)</a></p></td>
<td><p>Gets the initial offer from the <strong>MediaProvider</strong> implementation.</p>
<p>protected abstract IAsyncResult BeginGetOffer(OfferAnswerContext offerAnswerContext, Collection&lt;SdpContentDescription&gt; lastLocalOutgoingSdps, AsyncCallback userCallback, object state)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382852(v=office.16)">EndGetOffer(IAsyncResult)</a></p></td>
<td><p>Completes the operation started by <strong>BeginGetOffer</strong>.</p>
<p>protected abstract SdpOffer EndGetOffer(IAsyncResult result)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh350188(v=office.16)">BeginTerminateMedia(CallDialogContext, Boolean, AsyncCallback, Object)</a></p></td>
<td><p>Starts an asynchronous media termination operation.</p>
<p>protected abstract IAsyncResult BeginTerminateMedia(CallDialogContext callDialogContext, bool isTerminatingSignalingSession, AsyncCallback userCallback, object state)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349351(v=office.16)">EndTerminateMedia(IAsyncResult)</a></p></td>
<td><p>Completes the operation started by <strong>BeginTerminateMedia</strong>.</p>
<p>protected abstract void EndTerminateMedia(IAsyncResult result)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382509(v=office.16)">SetAnswer(OfferAnswerContext, SdpOffer, SdpAnswer)</a></p></td>
<td><p>Sets the incoming answer on the <strong>MediaProvider</strong> implementation.</p>
<p>protected abstract void SetAnswer(OfferAnswerContext context, SdpOffer originalOffer, SdpAnswer answer)</p></td>
</tr>
</tbody>
</table>


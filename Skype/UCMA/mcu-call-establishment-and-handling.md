---
title: MCU call establishment and handling
TOCTitle: MCU call establishment and handling
ms:assetid: 7a149cda-3032-4cf7-9082-87454d7b6981
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466015(v=office.16)
ms:contentKeyID: 65239951
ms.date: 07/27/2015
mtps_version: v=office.16
---

# MCU call establishment and handling


_**Applies to:** Skype for Business 2015_

The audio-video MCU does not support multiple points of conference, but does permit a given endpoint to join the audio-video MCU under more than one identity. To overcome the lack of support for multiple points of conference, the platform creates each new call under a fictitious identity. This topic discusses how the platform establishes these calls.

## Fictitious identity

The [UseGeneratedIdentityForTrustedConference](https://msdn.microsoft.com/en-us/library/hh382405\(v=office.16\)) property on an [AudioVideoCallEstablishOptions](https://msdn.microsoft.com/en-us/library/hh382857\(v=office.16\)) instance can be used to specify whether a fictitious identity is used when the call is established.

## Temporary Focus dialog

To dial in to an MCU, an application sends a C3P **addUser** request (for more information, see [\[MS-CONFBAS\]: Centralized Conference Control Protocol: Basic Architecture and Signaling Specification](http://msdn.microsoft.com/en-us/library/cc431498.aspx)) to the MCU (by way of the Focus) declaring that a specific identity will be dialing into the MCU. After this action, the application sends a media-INVITE request. The Focus intercepts any media-INVITE request that is sent to confirm that the caller already has an active Focus dialog under the same identity.

For the media-INVITE request to succeed, the platform creates a temporary signaling dialog with the Focus under the fictitious identity. The temporary dialog is established for a trusted user, which means that the Focus and audio-video MCU endpoints for this fictitious identity do not appear in the user roster, as would normally be the case. The temporary dialog includes establishing a signaling session, but does not include a subscription.


---
title: Key features of UCMA 5.0
TOCTitle: Key features of UCMA 5.0
ms:assetid: 7d496be2-794a-4989-82a6-51cb840b964d
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465947(v=office.16)
ms:contentKeyID: 65239788
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Key features of UCMA 5.0

**Applies to**: Skype for Business 2015

Developers can use the key features listed in this topic to create multimodal and multiparty communication and collaboration applications with Enhanced Presence capabilities.

## Modality-extensible communication framework

- Integrated support for instant messaging (IM).

- Integrated support for audio, with Secure Real-time Transport Protocol (SRTP), early media, and multiple codec selection.

- Common telephony features enabled by means of a reusable signaling framework (transfers, forwards, caller on hold, gateway interoperability and other operations).

- Integrated audio devices: recorder, player, tone controller for Dual-Tone Multi-Frequency (DTMF) and Fax tones, and connectors for speech recognition and speech synthesis.

- Loose coupling between signaling and media, allowing back-to-back and scenarios such as media-enabled Web clients.

- User impersonation.

- Conferencing features (control and monitoring): anonymous user join, trusted user join.

- Multimodal escalation-to-conference helpers for instant messaging calls.
    
  Developers who implement a custom audio provider can provide support for escalation-to-conferencing for the custom media type.

- Platform extensibility by means of the factory-based [Call](https://msdn.microsoft.com/en-us/library/hh384235\(v=office.16\)) and [MediaProvider](https://msdn.microsoft.com/en-us/library/hh383767\(v=office.16\)) classes.
    
  Developers can extend the platform to handle a new media type by creating custom **Call**, **MediaProvider**, and [MediaFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaflow?view=ucma-api) subclasses that work with the new media type.

## Offline conference scheduling and management

- Conference retrieval from PSTN conference ID.

## Presence publishing and presence subscription

- Publishing framework based on presence manifest. The manifest is predefined in UCMA 5.0 and follows the same rules for presence publication as Skype for Business 2015.

- Automatic user endpoint bootstrapping based on container manifest.

## Contacts and groups

The following features apply only to [UserEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.userendpoint?view=ucma-api) type, not the [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) type:

- Contact object registration

- Contact list creation and management

- Contact organizations in provided or custom groups


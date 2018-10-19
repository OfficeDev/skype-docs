---
title: Adding a modality to an existing conversation
TOCTitle: Adding a modality to an existing conversation
ms:assetid: 3e3ba497-c663-461e-9a57-a9ad5cc0521c
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465986(v=office.16)
ms:contentKeyID: 65239923
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Adding a modality to an existing conversation


_**Applies to:** Skype for Business 2015_

Adding an audio-video call is similar to adding an instant messaging (IM) call. When a call is added to a conversation, a new modality is considered to be added to the conversation. Adding a modality can be done in a conversation when the conversation is in two-party or conference mode. When a call is established, the corresponding provider is asked for the offer that contains the media types needed for the call. These media types are dependent on how the flow is configured by the application. After a successful offer/answer is negotiated on the call, the corresponding media types are locked in the conversation. It is not possible to add additional calls that want to negotiate the locked media types.

## Conversation remote participants

If the conversation is in two-party mode, the remote participant information represents the identity of the remote side. In two-party mode, there is only one remote participant. If the conversation is in conference mode, the remote participant information represents the identities of all of the conference participants. This does not include participants who are in the conference lobby.


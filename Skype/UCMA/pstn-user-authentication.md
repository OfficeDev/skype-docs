---
title: PSTN user authentication
TOCTitle: PSTN user authentication
ms:assetid: 8e0d8150-57bc-49ab-a190-e7e25d42df05
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465941(v=office.16)
ms:contentKeyID: 65239829
ms.date: 07/27/2015
mtps_version: v=office.16
---

# PSTN user authentication


_**Applies to:** Skype for Business 2015_

Microsoft Unified Communications Managed API 5.0 applications add the ability to authenticate a user to the Skype for Business Server 2015/ servers by means of a Skype for Business Server 2015 user personal identification number (PIN), or by authenticating a user separately, and then using server trust to take actions on behalf of that user.

UCMA 5.0 includes the **ApplicationPinServices** class, which has a method that can be used to verify a PIN.

With the ability to authenticate users by PINs, a mobile user can call aUCMA 5.0 application, present a single PIN, and then be authenticated to his or her mailbox to perform such tasks as reading mail using text-to-speech (TTS) or accessing his or her contact list. The Voice Companion sample that ships with Microsoft Unified Communications Managed API 5.0 SDK demonstrates how these actions can be carried out. For more information, see [Voice Companion (sample)](voice-companion-sample.md) and [Voice companion](voice-companion.md).


---
title: VoiceXML browser
TOCTitle: VoiceXML browser
ms:assetid: ae93b7b5-3bc4-40a6-bf01-ed80182003b5
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466125(v=office.16)
ms:contentKeyID: 65240066
ms.date: 07/27/2015
mtps_version: v=office.16
---

# VoiceXML browser

**Applies to**: Skype for Business 2015

The [Browser](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.voicexml.browser?view=ucma-voice) class in Microsoft Unified Communications Managed API 5.0 is derived from the **Browser** class in the **Microsoft.Speech.VoiceXml** namespace. Browser has access to all the public members of the **Browser** class in the **Microsoft.Speech.VoiceXml** namespace, as well as all the enumerated types, EventArgs classes, and interpretation result classes of the **Microsoft.Speech.VoiceXml.Common** namespace.

A **Browser** connects to the call processing and audio resources in UCMA 5.0 through the **SetAudioVideoCall()** method. See [Connecting to audio resources in UCMA 5.0](connecting-to-audio-resources-in-ucma-5-0.md) for more information.

## Browser functionality in UCMA 5.0

The **Browser** is the means by which callers communicate with a VoiceXML application in UCMA 5.0. A hosting .NET application binds an incoming phone call with an instance of the **Browser**, which loads a VoiceXML start page. The **Browser** processes the elements on a VoiceXML page and provides the connection to the call processing and audio resources in UCMA 5.0 that give information to, and collect information from, a caller.

In a VoiceXML session, a **Browser** instance may perform any of the following operations, as instructed by the VoiceXML application:

- Transfer a call to a recipient.

- Collect information from a caller through speech or Dual-Tone Multi-Frequency (DTMF) recognition.

- Give information to a caller using synthesized speech or recorded audio.

- Record audio spoken by a caller.

- Retrieve data from a web server using HTTP GET commands.

- Send information received during the call to a web server using HTTP POST commands.

- Log information about the session.

- Generate events with information about the session.

- Exit the VoiceXML page and end the session.


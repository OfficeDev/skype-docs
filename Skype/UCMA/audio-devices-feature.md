---
title: Audio devices (feature)
TOCTitle: Audio devices
ms:assetid: ce151696-2fec-49f4-842a-fa5e69f625d7
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465951(v=office.16)
ms:contentKeyID: 65239789
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Audio devices (feature)


_**Applies to:** Skype for Business 2015_

Microsoft Unified Communications Managed API 5.0 provides the following integrated audio devices:

  - Recorder
    
    For more information, see [Recorder](recorder.md).

  - Player
    
    For more information, see [Player](player.md).

  - Tone controller for Dual-Tone Multi-Frequency (DTMF) tones and Fax tones
    
    For more information, see [ToneController](tonecontroller.md).

  - Speech recognition connector and speech synthesis connector
    
    For more information, see [SpeechRecognitionConnector](speechrecognitionconnector.md) and [SpeechSynthesisConnector](speechsynthesisconnector.md).

Applications can use audio devices attached to media flows to implement scenarios such as the following:

  - Use a **Player** object to play prerecorded prompts in an interactive voice response (IVR) application.

  - Use a **Recorder** object to record messages or conversations in a call.

  - Use a **ToneController** object to receive or send DTMF commands and perform actions on these DTMF commands, or to send or receive Fax tones.

  - Use a **SpeechRecognitionConnector** object, in conjunction with the **Microsoft.Speech** namespace, to perform speech recognition.

  - Use a **SpeechSynthesisConnector** object, in conjunction with the **Microsoft.Speech** namespace, to render text as speech.


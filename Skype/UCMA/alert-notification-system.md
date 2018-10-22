---
title: Alert notification system
TOCTitle: Alert notification system
ms:assetid: e75d50af-2a7e-4558-bb18-7bcf2c66560f
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465952(v=office.16)
ms:contentKeyID: 65239777
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Alert notification system


**Applies to**: Skype for Business 2015



Microsoft Unified Communications Managed API 5.0 allows a user to be notified of changing conditions to be able to react in real time.

The following illustration shows the components involved in an alert notification system.

![Alerts details](images/Dn465952.UCMA-Alerts2(Office.16).png "Alerts details")

Other names for the Alert Notification scenario include Notification Service and Alerting.

## Key components of the alert notification system

Following are descriptions of the key components of the alert notification system.

### Senders

  - Anywhere access: Notification senders can choose the entry-point to use.
    
      - Skype for Business 2015.
    
      - Federated users and Skype
    
      - PSTN/cell phone (traditional) or SIP phones through an IP-PBX or gateway. (Add link to supported gateways.)

### Recipients

  - Hunt until found/simul-ring.
    
      - Applications can ring/IM a target periodically until it replies positively.
    
      - Applications can ring a user at many endpoints, based on presence information or the Unified Contact Store.

  - Scalable: Applications can send tens of thousands of notifications per minute using page-mode messaging.

  - Multimodal: Applications can reach out to a target user by means of page-mode IM, regular IM, or voice.

### UCMA system

  - Fully customizable: .NET Framework platform exposes simple concepts; integrates easily with other Microsoft platforms such as SQL, CRM, Exchange, and SharePoint.

  - On premise or off premise.

  - Highly scalable and available solution, capable of tens of thousands of alerts per minute.

## Typical call flow

The general call flow typical of an alert notification system application is as follows:

1.  An alert (new outbound notification) is sent to the UCMA application, coming as either voice or instant message from a user or from an automated data source.

2.  The UCMA application sends the notification to all targets, including those on phones, outside the corporation, and on the Skype network.

3.  The UCMA application can log an entry to indicate that a message is successfully delivered, with the option to failover to other contacts or contact means (for example, ringing a cellphone after ringing the desk phone). The contact information can be from the Contact Store, or given as part of the initiating alert.

## Related features

  - UCMA-based IVR applications include the full power of Microsoft.Speech-based speech recognition, text-to-speech, and DTMF handling. A user can call a UCMA application, be connected to a custom IVR (which can be VoiceXML-based), provide his or her information to the IVR, and then be connected to streaming music-on-hold. Meanwhile, the information that the user provides is passed to the application, allowing the application to intelligently route the call, fetch information from external sources, and play customized messages to the user.
    
      - The [Player](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.player?view=ucma-api), [Recorder](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.recorder?view=ucma-api), and [ToneController](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.tonecontroller?view=ucma-api) classes can be used to implement an IVR workflow that involves playing prerecorded prompts, handling DTMF tones, and recording audio.
    
      - The [Browser](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.voicexml.browser?view=ucma-voice) class can interpret VoiceXML code to implement workflow logic that involves TTS or speech recognition.
    
      - The Connector objects (the [SpeechRecognitionConnector](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.speechrecognitionconnector?view=ucma-api) and [SpeechSynthesisConnector](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.speechsynthesisconnector?view=ucma-api) classes) can be used to implement an application that involves TTS or speech recognition.
    
      - Recording and Music on Hold
        
        UCMA applications can play audio content to the caller on demand, record the audio portion of a call, and play music-on-hold to users while waiting for the IVR to perform background tasks.

  - IM and page-mode instant messaging
    
    UCMA applications can notify a user of an alert by page-mode messages, which are best-effort, or by full IM conversation if confirmation of user receipt is required.

  - PIC
    
    Using Microsoft SIP Processing Language (MSPL) and UCMA, an application can reach out to public internet cloud (PIC) contacts, and be contacted by PIC users for receiving or sending notifications.

  - Recording and playback
    
    UCMA applications can record the audio portion of a call for later review, play audio content to the target on demand, and play prerecorded prompts as part of an IVR/voice UI.

  - Simul-ring
    
    A UCMA application can contact all members of a group simultaneously, and then communicate only with the first one who replies.


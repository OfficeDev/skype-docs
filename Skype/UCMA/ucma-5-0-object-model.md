---
description: Learn about the object model and components of UCMA 5.0.
title: UCMA 5.0 object model
TOCTitle: UCMA 5.0 object model
ms:assetid: 8a0bef0f-7819-45bd-b601-cdc2544009b5
ms:mtpsurl: https://msdn.microsoft.com/library/Dn465975(v=office.16)
ms:contentKeyID: 65239902
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA 5.0 object model


**Applies to**: Skype for Business 2015

The major components that appear in a Microsoft Unified Communications Managed API 5.0 application are [LocalEndpoint](/dotnet/api/microsoft.rtc.collaboration.localendpoint) (of which two implementations are [ApplicationEndpoint](/dotnet/api/microsoft.rtc.collaboration.applicationendpoint) and [UserEndpoint](/dotnet/api/microsoft.rtc.collaboration.userendpoint)), [Conversation](https://msdn.microsoft.com/library/hh349224\(v=office.16\)), and [CollaborationPlatform](/dotnet/api/microsoft.rtc.collaboration.collaborationplatform). A **CollaborationPlatform** instance can manage multiple **LocalEndpoint** instances, and each **LocalEndpoint** instance can have multiple **Conversation** instances.

In addition to listing many of the UCMA 5.0 components, the elements in the illustration are arranged in two dimensions. The horizontal axis is divided into two categories: call controls and media controls. Call controls are concerned with signaling data, while media controls are concerned with the instant message (IM) and audio data that is communicated between participants.

The call control category is further subdivided into multiparty controls and two-party controls, which are concerned with, respectively, conversations among three or more participants or those between two participants.

The media control category is further subdivided into media flows, devices, and media providers. Each type of media (IM or audio/video) has its own type of flow. The devices in the devices column can be used to record an audio stream, play an audio stream, and send or receive telephone keypad tones. There are also two devices that, when used in conjunction with Microsoft.Speech object model, can be used to recognize speech, and to synthesize speech. Two of the media providers shown are provided with UCMA 5.0. The third (labeled as ContosoProvider in the illustration) is not provided, but can be implemented by third-party developers. Media providers are not directly accessible, but the flows they provide are accessible.

The color-coded components at the same horizontal level represent the components that take part in a particular communication mode. For example, the **AudioVideoProvider** sends audio/video media to an **AudioVideoFlow**, and then to either an [AudioVideoCall](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall) (for two parties) or to an [AudioVideoMcuSession](https://msdn.microsoft.com/library/hh385298\(v=office.16\)) (for more than two parties). The objects shown in the Devices column can attach an [AudioVideoFlow](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow), from which audio media can come ([Recorder](/dotnet/api/microsoft.rtc.collaboration.audiovideo.recorder), [ToneController](/dotnet/api/microsoft.rtc.collaboration.audiovideo.tonecontroller), [SpeechRecognitionConnector](/dotnet/api/microsoft.rtc.collaboration.audiovideo.speechrecognitionconnector)), or to which audio media can go ([Player](/api/microsoft.rtc.collaboration.audiovideo.player), **ToneController**, [SpeechSynthesisConnector](/dotnet/api/microsoft.rtc.collaboration.audiovideo.speechsynthesisconnector)).

The vertical axis is divided into two principal categories: single modal and multimodal. These categories indicate whether communication occurs by means of a single mode (for example, using IM only) or by multiple modes (for example, using IM and audio).

UCMA 5.0 provides built-in support for instant messaging and audio communication modalities. The platform can be extended to provide support for other modalities. The components in the top row in **Conversation** show the components that third-party developers can create to provide this support.

The relationships among the major components of UCMA 5.0 appear in the following illustration.

![Major components of UCMA 4.0](images/Dn465975.UCMAArch04a(Office.16).jpg "Major components of UCMA 4.0")

## See also

- [Microsoft.Rtc.Collaboration Namespace](/dotnet/api/microsoft.rtc.collaboration)
- [Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml Namespace](/dotnet/api/Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml)
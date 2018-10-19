---
title: UCMA 5.0 object model
TOCTitle: UCMA 5.0 object model
ms:assetid: 8a0bef0f-7819-45bd-b601-cdc2544009b5
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465975(v=office.16)
ms:contentKeyID: 65239902
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA 5.0 object model


_**Applies to:** Skype for Business 2015_

The major components that appear in a Microsoft Unified Communications Managed API 5.0 application are [LocalEndpoint](https://msdn.microsoft.com/en-us/library/hh349887\(v=office.16\)) (of which two implementations are [ApplicationEndpoint](https://msdn.microsoft.com/en-us/library/hh384825\(v=office.16\)) and [UserEndpoint](https://msdn.microsoft.com/en-us/library/hh348819\(v=office.16\))), [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)), and [CollaborationPlatform](https://msdn.microsoft.com/en-us/library/hh385176\(v=office.16\)). A **CollaborationPlatform** instance can manage multiple **LocalEndpoint** instances, and each **LocalEndpoint** instance can have multiple **Conversation** instances.

In addition to listing many of the UCMA 5.0 components, the elements in the illustration are arranged in two dimensions. The horizontal axis is divided into two categories: call controls and media controls. Call controls are concerned with signaling data, while media controls are concerned with the instant message (IM) and audio data that is communicated between participants.

The call control category is further subdivided into multiparty controls and two-party controls, which are concerned with, respectively, conversations among three or more participants or those between two participants.

The media control category is further subdivided into media flows, devices, and media providers. Each type of media (IM or audio/video) has its own type of flow. The devices in the devices column can be used to record an audio stream, play an audio stream, and send or receive telephone keypad tones. There are also two devices that, when used in conjunction with Microsoft.Speech object model, can be used to recognize speech, and to synthesize speech. Two of the media providers shown are provided with UCMA 5.0. The third (labeled as ContosoProvider in the illustration) is not provided, but can be implemented by third-party developers. Media providers are not directly accessible, but the flows they provide are accessible.

The color-coded components at the same horizontal level represent the components that take part in a particular communication mode. For example, the **AudioVideoProvider** sends audio/video media to an **AudioVideoFlow**, and then to either an [AudioVideoCall](https://msdn.microsoft.com/en-us/library/hh383901\(v=office.16\)) (for two parties) or to an [AudioVideoMcuSession](https://msdn.microsoft.com/en-us/library/hh385298\(v=office.16\)) (for more than two parties). The objects shown in the Devices column can attach an [AudioVideoFlow](https://msdn.microsoft.com/en-us/library/hh383533\(v=office.16\)), from which audio media can come ([Recorder](https://msdn.microsoft.com/en-us/library/hh381624\(v=office.16\)), [ToneController](https://msdn.microsoft.com/en-us/library/hh349643\(v=office.16\)), [SpeechRecognitionConnector](https://msdn.microsoft.com/en-us/library/hh383253\(v=office.16\))), or to which audio media can go ([Player](https://msdn.microsoft.com/en-us/library/hh349780\(v=office.16\)), **ToneController**, [SpeechSynthesisConnector](https://msdn.microsoft.com/en-us/library/hh349773\(v=office.16\))).

The vertical axis is divided into two principal categories: single modal and multimodal. These categories indicate whether communication occurs by means of a single mode (for example, using IM only) or by multiple modes (for example, using IM and audio).

UCMA 5.0 provides built-in support for instant messaging and audio communication modalities. The platform can be extended to provide support for other modalities. The components in the top row in **Conversation** show the components that third-party developers can create to provide this support.

The relationships among the major components of UCMA 5.0 appear in the following illustration.

![Major components of UCMA 4.0](images/Dn465975.UCMAArch04a(Office.16).jpg "Major components of UCMA 4.0")


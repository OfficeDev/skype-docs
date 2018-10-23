---
title: Audio devices
TOCTitle: Audio devices
ms:assetid: e8820e34-eadb-42c5-bbc5-b8c8ccb7671f
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466031(v=office.16)
ms:contentKeyID: 65239972
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Audio devices

**Applies to**: Skype for Business 2015

The topics in this section describe the audio devices that are available in Microsoft Unified Communications Managed API 5.0: [Player](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.player?view=ucma-api), [Recorder](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.recorder?view=ucma-api), [ToneController](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.tonecontroller?view=ucma-api), [SpeechRecognitionConnector](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.speechrecognitionconnector?view=ucma-api), and [SpeechSynthesisConnector](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.speechsynthesisconnector?view=ucma-api). These devices, respectively, can be used to play recorded audio, record audio, send and receive telephone dial tones, convert speech to text, or convert text to speech.

> [!IMPORTANT]
> The **SpeechRecognitionConnector** and **SpeechSynthesisConnector** classes are designed to work exclusively with classes in the **Microsoft.Speech** namespace.

In UCMA 5.0, devices are users of media data, and have [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api) instances attached to them. Although these devices are represented as properties on the **AudioVideoFlow** class, they are not automatically instantiated and each device is independent of the **AudioVideoFlow** instance that is attached to it. Before any of these devices can be used, it must be created, and then attached to an **AudioVideoFlow** instance. Although most of the devices are associated with a single **AudioVideoFlow** instance, a **Player** device can have multiple **AudioVideoFlow** instances attached to it. In addition, an **AudioVideoFlow** instance can be detached from a given **Player**, and another **AudioVideoFlow** instance can be attached.

The following code example shows the steps required to create a **Player** and attach an existing **AudioVideoFlow** instance to it.

```csharp
    Player myPlayer = new Player();
    myPlayer.AttachFlow(avFlow);
```

Each of the devices has an **AttachFlow** method that it uses to attach an **AudioVideoFlow** instance. Similarly, each device has a **DetachFlow** method that it uses to detach the previously-attached **AudioVideoFlow** instance. The **AttachFlow** and **DetachFlow** operations can happen at any time, independent of the state of the **AudioVideoFlow** instance.

Except for a **Player** device, a given **AudioVideoFlow** instance can be attached to only one device at a time. The **AudioVideoFlow** can remove the channel spontaneously if the Real-Time Protocol (RTP) connection is broken or if the far end requests a hold or renegotiation.

When a device is shut down, the attached **AudioVideoFlow** is not automatically detached. The **AudioVideoFlow** remains attached until it is detached. The **AudioVideoFlow** must eventually be detached, or a memory leak will result. A long-lived device such as a **Player** can have many flows attached to it, and each flow and all related objects remain in memory even when the **Player** is shut down.

This section contains the following topics:

- [Player](player.md)
- [Recorder](recorder.md)
- [ToneController](tonecontroller.md)
- [SpeechRecognitionConnector](speechrecognitionconnector.md)
- [SpeechSynthesisConnector](speechsynthesisconnector.md)



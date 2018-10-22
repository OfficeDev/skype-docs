---
title: Connecting to audio resources in UCMA 5.0
TOCTitle: Connecting to audio resources in UCMA 5.0
ms:assetid: 8da6dc06-69b0-4e7c-b42e-1011cad4223f
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466126(v=office.16)
ms:contentKeyID: 65240067
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Connecting to audio resources in UCMA 5.0

**Applies to**: Skype for Business 2015

A hosting .NET application uses the **SetAudioVideoCall()** method to bind an instance of the [Browser](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.voicexml.browser?view=ucma-voice) with an [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api) object.

An **AudioVideoCall** object contains an instance of [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api), which includes the audio devices in Microsoft Unified Communications Managed API 5.0 that will be used to record audio, play audio, send and receive telephone keypad tones, and connect to speech engines that convert spoken utterances into text and convert text into synthesized speech during the processing of a call. 

The .NET application must ensure that the call is established and is active prior to calling **SetAudioVideoCall()**. After the call to the **SetAudioVideoCall** method has been made, the .Net application can launch the Browser.


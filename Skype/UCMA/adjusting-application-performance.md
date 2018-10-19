---
title: Adjusting application performance
TOCTitle: Adjusting application performance
ms:assetid: cad3fdf3-04b0-4db0-9ef9-bf5e7929e87f
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466096(v=office.16)
ms:contentKeyID: 65240042
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Adjusting application performance


_**Applies to:** Skype for Business 2015_

Because codecs have varying performance characteristics, UCMA 5.0 SDK exposes the UseHighPerformance property, which enables developers to choose between better server performance or better audio quality for their applications. **UseHighPerformance** is a property on the AudioChannelTemplate class in the Microsoft.Rtc.Collaboration.AudioVideo namespace.

Setting the **UseHighPerformance** property to true causes the UCMA 5.0 SDK platform to exclude computationally complex audio codes such as RTAudio-WB and RTAudio-NB from the set of codecs offered/negotiated. Setting the property to false causes the platform to include these codecs. Usage of these codecs may offer higher quality at reduced network bandwidth consumption at the expense of CPU cycles.

Performance gain comes from reducing the time spent encoding and decoding audio, of which the more time-expensive operation is encoding. Encoding is particularly expensive with RTAudio. The **UseHighPerformance** setting limits the choice of codecs to those that can more quickly encode and decode audio packets.

## Using the UseHighPerformance property

For UCMA 5.0 SDK applications that require optimal server performance and scalability, set the **UseHighPerformance** property to true.

The following code shows a handler for the **AudioVideoFlowConfigurationRequested** event on an **AudioVideoCall** instance. The handler can be used for incoming and outgoing audio/video calls. In this code example, the **UseHighPerformance** property is set to true, thereby selecting server performance over audio quality.

``` csharp
private void UserAvCall_AudioVideoFlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
{
  AudioVideoCall call = sender as AudioVideoCall;
  AudioVideoFlowTemplate template = new AudioVideoFlowTemplate(call.Flow);

  // Set High Performance
  template.Audio.Channels[ChannelLabel.AudioMono].UseHighPerformance = true;
  call.Flow.Initialize(template);
}
```


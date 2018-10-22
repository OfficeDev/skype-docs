---
title: Using an AudioVideoFlowTemplate
TOCTitle: Using an AudioVideoFlowTemplate
ms:assetid: d73d357f-5fe1-4a7d-b1c6-be1a2dec2882
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466033(v=office.16)
ms:contentKeyID: 65239969
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Using an AudioVideoFlowTemplate

**Applies to**: Skype for Business 2015

The [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api) class has no public constructors, but instead relies on settings in an [AudioVideoFlowTemplate](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflowtemplate?view=ucma-api) instance to initialize or modify an **AudioVideoFlow** instance.

After an **AudioVideoFlowTemplate** instance has been created, its settings can be copied to an **AudioVideoFlow** instance by a call to the [Initialize](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow.initialize?view=ucma-api) method on the **AudioVideoFlow** class. This initialization must occur within the body of a handler for the [AudioVideoFlowConfigurationRequested](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall.audiovideoflowconfigurationrequested?view=ucma-api) event. Alternatively, an **AudioVideoFlowTemplate** instance can be set and then passed in a call to the [BeginApplyChanges](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow.beginapplychanges?view=ucma-api#overloads) method on the **AudioVideoFlow** class.

**AudioVideoFlowTemplate** and **AudioVideoFlow** instances are independent of one another. A new **AudioVideoFlowTemplate** instance can be created each time an **AudioVideoFlow** instance is to be set or modified, or a single **AudioVideoFlowTemplate** instance can be used to set or modify multiple **AudioVideoFlow** instances.

## Initializing an AudioVideoFlow instance

The technique of calling [Initialize](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow.initialize?view=ucma-api) to initialize an **AudioVideoFlow** instance can be used only when the **AudioVideoFlow** is in the **Idle** state, and only within the body of a handler for the [AudioVideoFlowConfigurationRequested](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall.audiovideoflowconfigurationrequested?view=ucma-api) event.

```csharp
AudioVideoFlow audioVideoFlow;
...

public void audioVideoCall_FlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
{
  audioVideoFlow = e.Flow;
  
  // Register an event handler for the StateChanged event.
  // When the flow becomes Active, as indicated by the StateChanged event, the program will perform media-related actions.
  audioVideoFlow.StateChanged += new EventHandler<MediaFlowStateChangedEventArgs>(audioVideoFlow_StateChanged);

  // Change the default behavior before the negotiation is completed.
  
  // Create a template based on the current AudioVideoFlow.
  AudioVideoFlowTemplate template = new AudioVideoFlowTemplate(audioVideoFlow);
            
  // Accept only 8Khz audio sampling rate codecs.
  template.Audio.Channels[ChannelLabel.AudioMono].SamplingRate = AudioSamplingRate.EightKhz;
  // Change audioVideoFlow settings according to the template.
  audioVideoFlow.Initialize(template);
}

// Very simple handler for the StateChanged event.
void audioVideoCall_StateChanged(object sender, CallStateChangedEventArgs e)
{
  Console.WriteLine("Call has changed state. The previous call state was: " + e.PreviousState + " and the current state is: " + e.State);
}
```


> [!IMPORTANT]
> The **AudioVideoFlowTemplate** constructor throws **InvalidOperationException** if the state of its **AudioVideoFlow** parameter is in the **Terminated** state.

## Modifying an AudioVideoFlowTemplate instance

The settings of an **AudioVideoFlow** instance can also be changed by a call to **BeginApplyChanges**.

In the following code example, which is shown only for purposes of illustration, it is assumed that *AVCall*, an [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api) instance, previously has been created, and that the state of *audioVideoFlow* is **Active**.

```csharp
AudioVideoFlow audioVideoFlow;
...

AudioVideoFlowTemplate AVFTemplate = new AudioVideoFlowTemplate(AVCall.Flow);
AVFTemplate.Audio.GetChannels()[ChannelLabel.AudioMono].AllowedDirection = MediaChannelDirection.SendOnly;
audioVideoFlow.BeginApplyChanges(AVFTemplate, ApplyChangesCallback, null);

...

void ApplyChangesCallback(IAsyncResult asyncResult)
{
  try
  {
    audioVideoFlow.EndApplyChanges(asyncResult);
  }
  catch
  {
    // handle exceptions
  }
}
```


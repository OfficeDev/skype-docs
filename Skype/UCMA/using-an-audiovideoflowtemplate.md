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


_**Applies to:** Skype for Business 2015_

The [AudioVideoFlow](https://msdn.microsoft.com/en-us/library/hh383533\(v=office.16\)) class has no public constructors, but instead relies on settings in an [AudioVideoFlowTemplate](https://msdn.microsoft.com/en-us/library/hh349157\(v=office.16\)) instance to initialize or modify an **AudioVideoFlow** instance.

After an **AudioVideoFlowTemplate** instance has been created, its settings can be copied to an **AudioVideoFlow** instance by a call to the [Initialize](https://msdn.microsoft.com/en-us/library/hh381417\(v=office.16\)) method on the **AudioVideoFlow** class. This initialization must occur within the body of a handler for the [AudioVideoFlowConfigurationRequested](https://msdn.microsoft.com/en-us/library/hh383342\(v=office.16\)) event. Alternatively, an **AudioVideoFlowTemplate** instance can be set and then passed in a call to the [BeginApplyChanges](https://msdn.microsoft.com/en-us/library/hh384566\(v=office.16\)) method on the **AudioVideoFlow** class.

**AudioVideoFlowTemplate** and **AudioVideoFlow** instances are independent of one another. A new **AudioVideoFlowTemplate** instance can be created each time an **AudioVideoFlow** instance is to be set or modified, or a single **AudioVideoFlowTemplate** instance can be used to set or modify multiple **AudioVideoFlow** instances.

## Initializing an AudioVideoFlow instance

The technique of calling [Initialize](https://msdn.microsoft.com/en-us/library/hh381417\(v=office.16\)) to initialize an **AudioVideoFlow** instance can be used only when the **AudioVideoFlow** is in the **Idle** state, and only within the body of a handler for the [AudioVideoFlowConfigurationRequested](https://msdn.microsoft.com/en-us/library/hh383342\(v=office.16\)) event.

``` csharp
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
> <P>The <STRONG>AudioVideoFlowTemplate</STRONG> constructor throws <STRONG>InvalidOperationException</STRONG> if the state of its <STRONG>AudioVideoFlow</STRONG> parameter is in the <STRONG>Terminated</STRONG> state.</P>



## Modifying an AudioVideoFlowTemplate instance

The settings of an **AudioVideoFlow** instance can also be changed by a call to **BeginApplyChanges**.

In the following code example, which is shown only for purposes of illustration, it is assumed that *AVCall*, an [AudioVideoCall](https://msdn.microsoft.com/en-us/library/hh383901\(v=office.16\)) instance, previously has been created, and that the state of *audioVideoFlow* is **Active**.

``` csharp
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


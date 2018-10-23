---
title: Recorder
TOCTitle: Recorder
ms:assetid: f1d1be77-652c-4781-b8de-9ab797231774
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466038(v=office.16)
ms:contentKeyID: 65239974
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Recorder

**Applies to**: Skype for Business 2015

A [Recorder](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.recorder?view=ucma-api) is an entity that can receive audio media that comes from an [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api) instance and record it to a file. Depending on the state of the attached **AudioVideoFlow** instance, a **Recorder** can automatically start or stop.

Unlike the [Player](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.player?view=ucma-api) class, which has two modes of operation, the **Recorder** class can be thought of as having only one mode of operation, which is similar to **Automatic** mode in the **Player** class. If an application calls the [Start](https://msdn.microsoft.com/en-us/library/hh383534\(v=office.16\)) method on a **Recorder** instance, nothing will be recorded until an active **AudioVideoFlow** instance is attached to it. In addition, if there is no active **AudioVideoFlow** instance attached to a **Recorder** instance, or if the **AudioVideoFlow** instance is detached or terminated, the **Recorder** instance stops automatically.

If a **Recorder** instance has started recording a file, an application can cause recording to stop by calling the [Stop](https://msdn.microsoft.com/en-us/library/hh381306\(v=office.16\)) method. In addition, state changes in the attached **AudioVideoFlow** instance can also cause the **Recorder** instance to stop. For example, if the **State** property on the **AudioVideoFlow** instance changes from **Active** to **Terminated**, the **Recorder** instance stops. Non-state configuration changes in the **AudioVideoFlow** instance, such as Hold, Retrieve, Mute, and changing the media channel direction, do not stop the **Recorder** instance. For example, if a call that is being recorded remains on hold for five minutes, the **Recorder** instance records five minutes of silence.

A **Recorder** will not record to a file with the hidden attribute.

> [!WARNING]
> Files recorded by a **Recorder** device do not support Digital Rights Management (DRM). This means that conversations recorded by a **Recorder** device can be played by any player that supports the Windows Media Audio (WMA) media format, potentially exposing personal or confidential information.

## Example – Using a recorder

The following code example shows the steps involved in using a [Recorder](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.recorder?view=ucma-api).

> [!WARNING]
> This code example is not a complete example. Several methods, properties, and events are used in this example, but are not defined within the example.

The essential points of creating and using a **Recorder** instance appear in the following steps. Each step is associated with a line of code that is preceded by a comment with a number in it.

1.  Create a **Recorder** instance, using the constructor for this class.

2.  Attach the flow to the recorder, using the [AttachFlow](https://msdn.microsoft.com/en-us/library/hh381868\(v=office.16\)) method on the **Recorder** class.

3.  Create the object that receives the recording, using the [WmaFileSink](https://msdn.microsoft.com/en-us/library/hh382490\(v=office.16\)) constructor. The parameter to this constructor is a string that contains the name of the WMA file that will be created. As previously mentioned, the file should not be a hidden file.

4.  Register for the **StateChanged** event.

5.  Call the recorder’s [SetSink](https://msdn.microsoft.com/en-us/library/hh348664\(v=office.16\)) method, which informs the recorder of where the recorded data should go.

6.  Start the recorder, using the [Start](https://msdn.microsoft.com/en-us/library/hh383534\(v=office.16\)) method on the **Recorder** instance.

7.  Pause the recorder, using the [Pause](https://msdn.microsoft.com/en-us/library/hh349541\(v=office.16\)) method on the **Recorder** instance.

8.  Stop the recorder, using the [Stop](https://msdn.microsoft.com/en-us/library/hh381306\(v=office.16\)) method on the **Recorder** instance.

9.  Detach the flow from the **Recorder** instance, using the [DetachFlow](https://msdn.microsoft.com/en-us/library/hh385116\(v=office.16\)) method.

<!-- end list -->

```csharp
public void Run()
{

  // Initialize and start up the platform.
  ClientPlatformSettings clientPlatformSettings = new ClientPlatformSettings(_applicationName, Microsoft.Rtc.Signaling.SipTransportType.Tls);
  collabPlatform = new CollaborationPlatform(clientPlatformSettings);
  collabPlatform.BeginStartUp(EndPlatformStartup, _collabPlatform);

  // The EndPlatformStartup callback method, not shown in this sample, would call EndStartup to
  // complete the platform startup operation.

  // Initialize and register the endpoint, using the credentials of the user the application will be acting as.
  UserEndpointSettings userEndpointSettings = new UserEndpointSettings(_userURI, _userServer);
  userEndpointSettings.Credential = _credential;
  userEndpoint = new UserEndpoint(_collabPlatform, userEndpointSettings);
  userEndpoint.BeginEstablish(EndEndpointEstablish, _userEndpoint);

  // The EndEndpointEstablish callback method, not shown in this sample, would call EndEstablish to
  // complete endpoint establishment.


  // Set up the conversation and place the call.
  ConversationSettings convSettings = new ConversationSettings();
  convSettings.Priority = _conversationPriority;
  convSettings.Subject = _conversationSubject;
  // Conversation represents a collection of modalities in the context of a dialog with one or multiple callees.
  Conversation conversation = new Conversation(_userEndpoint, _calledParty, convSettings);

  _audioVideoCall = new AudioVideoCall(conversation);

  // Call: StateChanged: Only hooked up for logging.
  _audioVideoCall.StateChanged += new EventHandler<CallStateChangedEventArgs>(audioVideoCall_StateChanged);

  // Subscribe to the AudioVideoFlowConfigurationRequested event; the flow will be used to send the media.
  // Ultimately, as a part of the callback, the media will be sent/received.
  _audioVideoCall.AudioVideoFlowConfigurationRequested += new EventHandler<AudioVideoFlowConfigurationRequestedEventArgs>(audioVideoCall_FlowConfigurationRequested);



  // Place the call to the remote party.
  // Note that the EndCallEstablish callback method is not shown in this sample.
  IAsyncResult asyncResult = _audioVideoCall.BeginEstablish(EndCallEstablish, _audioVideoCall);

  // Sync; wait for the call to complete.
  _audioVideoCall.EndEstablilsh(asyncResult);

  // Sync; wait for the AudioVideoFlow to go Active.
  autoResetAVFlowActiveEvent.WaitOne();

  // Set up a recorder to record the audio from the remote side.
  // 1) Create a recorder.
  Recorder recorder = new Recorder();
  // 2) Attach the flow to the recorder.
  recorder.AttachFlow(_audioVideoFlow);

  // 3) Register for StateChanged event.
  recorder.StateChanged += new EventHandler<RecorderStateChangedEventArgs>(recorder_StateChanged);

  // 4) Create a WmaFileSink in which to record. 
  WmaFileSink sink = new WmaFileSink("recording.wma");
  // 5) Inform the recorder about the recording destination.
  recorder.SetSink(sink);

  // 6) Start the recorder.
  recorder.Start();

  Thread.Sleep(9000);

  // 7) Pause the recorder.
  recorder.Pause();

  // 8) Stop the recorder.
  recorder.Stop();

  // 9) Detach the flow from the recorder.
  recorder.DetachFlow();

  collabPlatform.BeginShutdown(EndPlatformShutdown, _collabPlatform);

  // Wait for shutdown to occur.
  autoResetShutdownEvent.WaitOne();
}
```


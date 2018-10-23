---
title: Player
TOCTitle: Player
ms:assetid: fc050a2e-0b1d-47be-8c4a-6164ce9854f0
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466037(v=office.16)
ms:contentKeyID: 65239973
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Player

**Applies to**: Skype for Business 2015

The [Player](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.player?view=ucma-api) class represents an entity that is capable of playing media to one or more media flows, and as such, provides a simple, scalable way to play an audio segment in a Windows Media Audio (WMA) file to multiple listeners. A **Player** instance renders the media that is represented by an instance of a subclass of the [MediaSource](https://msdn.microsoft.com/en-us/library/hh348635\(v=office.16\)) abstract base class. The output from a **Player** instance goes to one or more [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api) instances.

A **Player** acts as a pointer to a **MediaSource** object, and determines the current position to play in the media.

## Modes of operation

The **Player** class has two modes of operation:

- **Automatic** (the default)
    
  In **Automatic** mode, the behavior of a **Player** instance depends on the state of the attached **AudioVideoFlow** instance. If an application calls the **Start** method on the **Player** instance, the **State** property on the **Player** instance changes to **Started**, but nothing is played until there is an active **AudioVideoFlow** instance attached to the **Player** instance (that is, until the **State** property on the **AudioVideoFlow** instance is **Active**).
    
  If the last active **AudioVideoFlow** instance is detached or terminated while a **Player** instance in **Automatic** mode is playing, the **Player** instance stops automatically.

- **Manual**
    
  In **Manual** mode, a **Player** instance can operate with or without an attached **AudioVideoFlow** instance, although no one would be able to listen to the file being played if there were no attached **AudioVideoFlow** instance. If the last active **AudioVideoFlow** instance is detached or terminated while a **Player** instance in **Manual** mode is playing, the **Player** instance continues playing. In this regard, a **Player** instance in **Manual** mode behaves like a CD player whose speakers are disconnected.

## Playback speed

The [PlaybackSpeed](https://msdn.microsoft.com/en-us/library/hh161922\(v=office.16\)) property on the **Player** class provides the capability to play back a WMA file at a different speed. This property gets or sets the rate at which the **Player** renders audio. An application can use **PlaybackSpeed** to develop an interface that gives the user more control in consuming the content. A message can be played back faster or slower depending on the **PlaybackSpeed** specified by the application.

The values in the **PlaybackSpeed** enumeration are **Half**, **ThreeQuarters**, **Normal**, **OneAndAQuarter**, **OneAndAHalf**, **OneAndThreeQuarters**, and **TwoTimes**. The values represent the multiple of normal playback speed. For example, **ThreeQuarters** represents 75% of normal playback speed.

## Player arrangements

The simplest arrangement is a single **Player** instance with a single attached **AudioVideoFlow** instance, as shown in the following illustration.

**Single player, single AudioVideoFlow instance**
  
![Single Player, single AudioVideoFlow instance](images/Dn466037.OnePlayerOneFlow(Office.16).png "Single Player, single AudioVideoFlow instance")

Another arrangement is to have a single **Player** instance that has multiple **AudioVideoFlow** instances, as shown in the following illustration. In this arrangement, the audio media to be played has a single play position and all attached **AudioVideoFlow** instances receive exactly the same audio simultaneously. This capability is useful for providing Music on Hold, or for broadcasting audio to multiple listeners.

**Single Player, multiple AudioVideoFlow instances**
  
![Single Player, multiple AudioVideoFlow instances](images/Dn466037.OnePlayerMultFlow(Office.16).png "Single Player, multiple AudioVideoFlow instances")

A third arrangement is to have multiple **Player** instances with multiple attached **AudioVideoFlow** instances, as shown in the following illustration. Each **Player** instance determines a different playing point in the media source. The AudioVideoFlow instances attached to a given Player instance (for example, AudioVideoFlow 1 and AudioVideoFlow 2 that are attached to Player 1) receive the same audio at the same time, but AudioVideoFlow instances attached to different Player instances receive different audio segments at any given time (for example, AudioVideoFlow 1 and AudioVideoFlow 3 that are attached, respectively, to Player 1 and Player 2). This capablity is useful for providing Welcome messages to listeners when they arrive at a conference.

**Multiple Player instances, multiple AudioVideoFlow instances**
  
![Multiple Player and AudioVideoFlow instances](images/Dn466037.OnePlayerMultFlow(Office.16).png "Multiple Player and AudioVideoFlow instances")

The **Player** class is scalable. In buffered mode, the **MediaSource** caches encoded versions of media. Given a [WmaFileSource](https://msdn.microsoft.com/en-us/library/hh381280\(v=office.16\)) object, an application can play audio that was previously cached (**MediaSourceOpenMode.Buffered**) or from a file on demand in unbuffered mode (**MediaSourceOpenMode.Unbuffered**). When buffered mode is used, playing the cached stream provides significant performance gains.

## Example – using a player

The following code example shows the steps involved in using a [Player](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.player?view=ucma-api).

> [!WARNING]
> This code example is not a complete example. Several methods, properties, and events are used in this example, but are not defined within the example.

The essential points of creating and using a **Player** instance are shown in the following steps. Each step is associated with a line of code that is preceded by a comment with a number in it.

1.  Create a **Player** instance, using the constructor for this class.

2.  Attach the flow to the player, using the [AttachFlow](https://msdn.microsoft.com/en-us/library/hh365942\(v=office.16\)) method on the **Player** class.

3.  Register for the player’s **StateChanged** event.

4.  Load the file that is to be played into memory by creating a [WmaFileSource](https://msdn.microsoft.com/en-us/library/hh381280\(v=office.16\)) instance. The string parameter is the name of the WMA file to be played.

5.  Prepare the source file, using the [BeginPrepareSource](https://msdn.microsoft.com/en-us/library/hh349115\(v=office.16\)) method on the **WmaFileSource** instance. The caller can specify whether the file is to be opened in **Buffered** mode or in **Unbuffered** mode. In the example code, the argument to [EndPrepareSource](https://msdn.microsoft.com/en-us/library/hh382429\(v=office.16\)) is the return value from **BeginPrepareSource**.

6.  Set the player’s mode, using the [SetMode](https://msdn.microsoft.com/en-us/library/hh382589\(v=office.16\)) method. In the example, the mode is set to **Manual**, which means that the player will begin playing only when the **State** of the attached **AutoVideoFlow** instance is **Active**. The Player stops when it reaches the end of the file or when there are no attached **AudioVideoFlow** instances that are **Active**. If the mode were set to **Manual**, the player would start playing immediately.

7.  Inform the player about the source of the media it will play, using the **SetSource** method.

8.  Start the player, using the player’s [Start](https://msdn.microsoft.com/en-us/library/hh382632\(v=office.16\)) method.

9.  Pause the player, using the player’s [Pause](https://msdn.microsoft.com/en-us/library/hh381641\(v=office.16\)) method. This step is shown only to demonstrate how to pause the player.

10. Restart the player after it was paused, using the **Start** method.

11. Stop the player, using the player’s [Stop](https://msdn.microsoft.com/en-us/library/hh350156\(v=office.16\)) method.

12. Close the source, using the [Close](https://msdn.microsoft.com/en-us/library/hh384406\(v=office.16\)) method on the **WmaFileSource** instance. An application must close the source when it is no longer needed. A source that is not closed can result in a memory leak.

13. Detach the flow, using the player’s [DetachFlow](https://msdn.microsoft.com/en-us/library/hh383520\(v=office.16\)) method.

<!-- end list -->

```csharp
public void Run()
{
  // Initialize and start the platform.
  ClientPlatformSettings clientPlatformSettings = new ClientPlatformSettings(_applicationName, Microsoft.Rtc.Signaling.SipTransportType.Tls);
  _collabPlatform = new CollaborationPlatform(clientPlatformSettings);
  _collabPlatform.BeginStartup(EndPlatformStartup, _collabPlatform);

  // Sync; wait for the startup to complete.
  // The EndPlatformStartup callback method, not shown in this sample, would call EndStartup
  // to complete platform startup.


  // Initialize and register the endpoint, using the credentials of the user the application will be acting as.
  UserEndpointSettings userEndpointSettings = new UserEndpointSettings(_userURI, _userServer);
  userEndpointSettings.Credential = _credential;
  _userEndpoint = new UserEndpoint(_collabPlatform, userEndpointSettings);
  _userEndpoint.BeginEstablish(EndEndpointEstablish, _userEndpoint);

  // The EndEndpointEstablish callback method, not shown in this sample, would call EndEstablish
  // to complete the establishment of the endpoint.


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



  // Place the call to the remote party;
  // Note that the EndCallEstablish callback method is not shown in this sample.
  IAsyncResult asyncResult = _audioVideoCall.BeginEstablish(EndCallEstablish, _audioVideoCall);

  // Sync; wait for the call to complete.
  _audioVideoCall.EndEstablish(asyncResult);

  // Sync; wait for the AudioVideoFlow goes Active
  _autoResetAVFlowActiveEvent.WaitOne();

  // 1) Create a Player and attach to AVFlow
  Player player = new Player();
  // 2) Attach the Player to the AVFlow
  player.AttachFlow(_audioVideoFlow);

  // 3) Subscribe to player StateChanged event, including the play completed event. 
  player.StateChanged += new EventHandler<PlayerStateChangedEventArgs>(player_StateChanged);

  // 4) Load the file into memory
    WmaFileSource source = new WmaFileSource("startup.wma");
  
  // 5) Prepare the source for the player.
  source.EndPrepareSource(source.BeginPrepareSource(MediaSourceOpenMode.Buffered, null, null));

  // 6) Set the play mode. In automatic mode, player will start playing only when the flow is in the active state.
  // In manual mode, player will start playing immediately.
  player.SetMode(PlayerMode.Manual);

  // 7) Inform the player about the source.
  player.SetSource(source);

  // 8) Start the player.
    player.Start();

    Thread.Sleep(2000);

  // 9) Pause the player.
    player.Pause();

    Thread.Sleep(2000);

  // 10) Resume playing.
  player.Start();

    Thread.Sleep(5000);

  // 11) Stop the player.
  player.Stop();

  // 12) Source must be closed after it is no longer needed, otherwise memory will not be released even after garbage collection.
  source.Close();

  // 13) Player must be detached from the flow, otherwise if the player is rooted, it will keep the flow in memory.
  player.DetachFlow(_audioVideoFlow);


  _collabPlatform.BeginShutdown(EndPlatformShutdown, _collabPlatform);

  // Wait for shutdown to occur.
  _autoResetShutdownEvent.WaitOne();
} 
```


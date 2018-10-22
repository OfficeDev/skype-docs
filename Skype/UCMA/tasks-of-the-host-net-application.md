---
title: Tasks of the host .NET application
TOCTitle: Tasks of the host .NET application
ms:assetid: 0dee2b50-283a-45de-ac00-bd2aca78c8a6
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466127(v=office.16)
ms:contentKeyID: 65240068
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Tasks of the host .NET application

**Applies to**: Lync 2013 | Lync Server 2013 | Skype for Business 2015



The host .NET application that you create performs tasks before, during, and after a VoiceXML session to support automated processing of calls.

Before the VoiceXML session:

  - Instantiate a [Browser](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.voicexml.browser?view=ucma-voice) instance.

  - Register for events that the **Browser** generates during a session.

  - Listen for calls at a UCMA endpoint.

  - Take calls that arrive at the UCMA endpoint.

  - Verify that a call is established and active.

  - Connect to audio devices in UCMA.

  - Launch an instance of the **Browser** and bind it to an active call.

During the VoiceXML session:

  - Respond to events from the **Browser**.

After the VoiceXML session:

  - Handle the call when the **Browser** exits the VoiceXML application, if required.

The following example illustrates a typical instantiation and launching of an asynchronous instance of the VoiceXML **Browser** (vxb) in Microsoft Unified Communications Managed API 5.0.

## Launching the browser

In UCMA 5.0, there must always be an active call before the hosting .NET application launches the [Browser](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.voicexml.browser?view=ucma-voice). Normally, the call to the **Run(Uri, CookieContainer)** or **RunAsync(Uri, CookieContainer)** methods will be in the event handler for a received phone call.


> [!NOTE]
> <P>Only use the synchronous <STRONG>Run(Uri, CookieContainer)</STRONG> method for prototyping and experimentation. Do not use it in a production application.</P>



## Example of .NET application tasks

The following sequence includes sample code that illustrates the typical tasks that your .NET application performs before, during, and after a VoiceXML call.

1.  Set up a UCMA 5.0 endpoint. (See ApplicationEndpoint.)

2.  Instantiate a VoiceXML [Browser](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.voicexml.browser?view=ucma-voice) instance (*vxb* in the following sample) and add delegates to handle events that occur after an asynchronous Browser instance has been launched and the session is in progress.
    
    ```csharp
    vxb = new Browser(); 
    vxb.Transferring += new EventHandler(HandleTransferring);
    vxb.Transferred += new EventHandler(HandleTransferred);
    vxb.Disconnecting += new EventHandler(HandleDisconnecting);
    vxb.Disconnected += new EventHandler(HandleDisconnected);
    vxb.SessionCompleted += new EventHandler(HandleSessionCompleted);
    ```

3.  Bind a call handler, *AvCallReceived*, with the endpoint:
    
    ```csharp
    endpoint.RegisterForIncomingCall<AudioVideoCall>(AvCallReceived);
    ```

4.  Accept the call and subscribe to the **StateChanged** event in the [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api) class. After the value of the [StateChanged](https://msdn.microsoft.com/en-us/library/hh365997\(v=office.16\)) event switches to **Active**, the .NET application can call the Browser’s **SetAudioVideoCall()** method. Then the .NET application can call either the **Run(Uri, CookieContainer)** or the **RunAsync(Uri, CookieContainer)** method.
    
    ```csharp
    // Declare an instance of AudioVideoCall that will be passed to SetAudioVideoCall.
    private AudioVideoCall currentCall;
    
    // Add a handler that uses the event that is raised when an incoming call arrives to the endpoint, see step 3, above.
    private void AudioVideoCallReceived(object sender, CallReceivedEventArgs<AudioVideoCall> e)
    {
      //Assign the current call to the global keeper.
      currentCall = e.Call;
    
      currentCall.AudioVideoFlowConfigurationRequested += new EventHandler<AudioVideoFlowConfigurationRequestedEventArgs>(Call_AudioVideoFlowConfigurationRequested);
    
      // Accept the incoming call and wait for state and configuration requests.
      currentCall.EndAccept(currentCall.BeginAccept(null, null));
    }
    
    
    // Add a handler for the AudioVideoFlowConfigurationRequested event.
    private void Call_AudioVideoFlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
    {
      currentCall.Flow.StateChanged += new EventHandler<MediaFlowStateChangedEventArgs>(Flow_StateChanged);
    }
    
    
    // Add a handler for the AudioVideoFlow state change event.
    private void Flow_StateChanged(object sender, MediaFlowStateChangedEventArgs e)
    {
      if (e.State == MediaFlowState.Active)
      {
        vxb.SetAudioVideoCall(currentCall);
        vxb.RunAsync(startPage, null);
      }
    }
    ```

5.  Handle events from the **Browser** as they occur using the event handlers instantiated in step 2.

6.  Take appropriate action when the **Browser** exits the page, which occurs after any of the following take place:
    
      - An error occurs in attempting to load the VoiceXML start page. The members of the **ExitReason** enumeration indicate possible errors that prevented loading the VoiceXML start page, such as **PageNotFound** or **MalFormedXml**.
    
      - The VoiceXML interpreter encounters an event thrown within the VoiceXML page that is not handled by the VoiceXML **\<error\>** element.
    
      - The application calls the **StopAsync()** method.
    
      - The VoiceXML interpreter executes an **\<exit\>** or a **\<disconnect\>** element.
    
      - The VoiceXML interpreter arrives at the end of a dialog and there is no successor element.
    
      - The VoiceXML interpreter executes a **\<transfer bridge="false"\>** element or a **\<transfer type="blind"\>** element.
    
      - The VoiceXML interpreter executes a **\<transfer type="consultation"\>** element in which the call transfer is successful.
    
      - The audio stream disconnects.
    

    > [!NOTE]
    > <P>Any of the preceding actions causes the <STRONG>Browser</STRONG> to cease processing the page and to reset state so that the <STRONG>Browser</STRONG> object can be reused. If the <STRONG>Browser</STRONG> was started synchronously (by a call to the <STRONG>Run(Uri, CookieContainer)</STRONG> method), control returns to the calling program. If running asynchronously, the <STRONG>Browser</STRONG> raises a <STRONG>SessionCompleted()</STRONG> event when it exits the VoiceXML page.</P>



7.  Use name/value pairs in the **Namelist()** property on the **VoiceXmlResult** object for further processing or debugging, if desired.

8.  Reuse or dispose of the **Browser** instance.

## Remarks

You can instantiate and run multiple **Browser** objects simultaneously, for example Browser1, Browser2, Browser3. You cannot run the same **Browser** instance concurrently on multiple sessions, for example Browser1(a), Browser1(b), Browser1(c).

The arguments of the **Run(Uri, CookieContainer)** and **RunAsync(Uri, CookieContainer)** methods contains the URI of the VoiceXML start page to interpret.

After launching in asynchronous mode, the **Browser** generates events that inform the hosting .NET application about the status of phone calls and the state of the **Browser** session.

The .NET application can terminate the VoiceXML session by calling the **StopAsync()** method.

Unless the VoiceXML interpreter processes a **\<disconnect\>** element, it will leave the call active on exit.

You can find a complete example of a .NET application for processing VoiceXML with your installation. The sample is located in the %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts folder.


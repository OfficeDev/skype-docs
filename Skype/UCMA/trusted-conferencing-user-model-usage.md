---
title: Trusted conferencing user model usage
TOCTitle: Trusted conferencing user model usage
ms:assetid: 444ffba4-37d3-4ba4-8ce6-c23db12ba190
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466007(v=office.16)
ms:contentKeyID: 65239948
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Trusted conferencing user model usage


**Applies to**: Skype for Business 2015

This topic provides the details of using the trusted conferencing user model in an application.

### To use the trusted conferencing user capability in an application

1.  Create and start a [CollaborationPlatform](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform?view=ucma-api) instance.

2.  Create and establish an [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) instance.

3.  Create a [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) instance.

4.  Join the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)).

5.  Create an [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api) instance.

6.  Establish the **AudioVideoCall** instance.

7.  Specify customized MCU routing.

8.  Issue a command in another user’s context.

9.  Terminate and dispose the call.

The steps in the preceding procedure are described in detail in the following sections.

### Creating and starting the CollaborationPlatform

Create a server [CollaborationPlatform](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform?view=ucma-api) instance with MTLS, a server certificate, and a trusted service GRUU.


> [!NOTE]
> <P>The variables shown as parameters to the <A href="https://msdn.microsoft.com/en-us/library/hh385152(v=office.16)">ServerPlatformSettings()</A> constructor in the following example must be set to appropriate values before they are used.</P>



```csharp
private void InitializePlatform()
{
  // Create and start a server CollaborationPlatform instance.
  // Because this computer should have a trusted service associated with it, no credentials are necessary.
  List<X509Certificate2> certs = GetCerts2(_localhost);
  ServerPlatformSettings settings = new ServerPlatformSettings(_applicationName, _localhost, _servicePort, _serverGruu, certs[0]);
  _collabPlatform = new CollaborationPlatform(settings);
  _collabPlatform.BeginStartup(PlatformStartupCompleted, null);
  Console.WriteLine("Starting the platform.");
}

// AsyncCallback for the platform start operation.
private void PlatformStartupCompleted(IAsyncResult result)
{
  try
  {
    _collabPlatform.EndStartup(result);
    Console.WriteLine("The platform has now started.");

    // Create an application endpoint from the details in an ApplicationEndpointSettings instance.
    InitializeRegisteredApplicationEndpoint();

  }
  catch (ConnectionFailureException connFailEx)
  {
    // ConnectionFailureException will be thrown when the platform cannot connect.
    // It is left to the developer to write real error handling code.
    Console.WriteLine(connFailEx.ToString());
  }
  catch (InvalidOperationException ioe)
  {
    // InvalidOperationException will be thrown when the platform is already started or terminated.
    // It is left to the developer to write real error handling code.
    Console.WriteLine(ioe.ToString());
  }
}
```

### Establishing an ApplicationEndpoint instance

The application then establishes an [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) instance.


> [!NOTE]
> <P>The variables shown as parameters to the <A href="https://msdn.microsoft.com/en-us/library/hh382372(v=office.16)">ApplicationEndpointSettings()</A> constructor in the following example must be set to appropriate values before they are used.</P>



```csharp
// Given a CollaborationPlatform, initialize and register an Application Endpoint.
private void InitializeRegisteredApplicationEndpoint()
{
  // Initialize and register the endpoint, using the credentials of the user the application will be acting as.
  ApplicationEndpointSettings settings = new ApplicationEndpointSettings(_ownerURI, _csServer, _port);
  settings.UseRegistration = true;
  _applicationEndpoint = new ApplicationEndpoint(_collabPlatform, settings);
  _applicationEndpoint.BeginEstablish(EndpointEstablishCompleted, null);
  Console.WriteLine("Establishing the endpoint.");
}

// AsyncCallback for the endpoint registration operation.
private void EndpointEstablishCompleted(IAsyncResult result)
{
  try
  {
    _applicationEndpoint.EndEstablish(result);
    Console.WriteLine("The Application Endpoint owned by URI: ");
    Console.WriteLine(_applicationEndpoint.OwnerUri);
    Console.WriteLine(" is now established and registered.");

    // Other code as appropriate to the application.

  }
  catch (ConnectionFailureException connFailEx)
  {
    // ConnectionFailureException will be thrown when the endpoint cannot connect to the server, or the credentials are invalid.
    // It is left to the developer to write real error handling code.
    Console.WriteLine(connFailEx.ToString());
  }
  catch (InvalidOperationException iOpEx)
  {
  // InvalidOperationException will be thrown when the endpoint is not in a valid state to connect. To connect, the platform must be started and the Endpoint Idle.
  // It is left to the developer to write real error handling code.
  Console.WriteLine(iOpEx.ToString());
  }
  catch (RegisterException regEx)
  {
    // RegisterException will be thrown when the endpoint cannot be registered (usually due to bad credentials).
    // It is left to the developer to write real error handling code. 
    Console.WriteLine(regEx.ToString());
  }
  catch (AuthenticationException ae)
  {
    // AuthenticationException will be thrown when a general authentication-related problem occurred.
    // It is left to the developer to write real error handling code. 
    Console.WriteLine(ae.ToString());
  }
  catch (OperationTimeoutException ote)
  {
    //OperationTimeoutException will be thrown when server did not respond for Register request.
    // It is left to the developer to write real error handling code. 
    Console.WriteLine(ate.ToString());
  }
}
```

### Creating a conversation instance

The application creates a [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) object for each conference that it serves. If needed, an application can create multiple **Conversation** objects for a conference and load balance its users among those conversation instances.

### Joining the conference as a trusted user

To join as a trusted user, the application must supply a [ConferenceJoinOptions](https://msdn.microsoft.com/en-us/library/hh385064\(v=office.16\)) instance to the [BeginJoin()](https://msdn.microsoft.com/en-us/library/hh349641\(v=office.16\)) method on the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) class, setting the [JoinMode](https://msdn.microsoft.com/en-us/library/hh384536\(v=office.16\)) property to **TrustedParticipant**. If the application has been configured correctly, it will be joined to the conference and will not appear in the roster of any Skype for Business 2015 clients. The following code sample shows how to accomplish this.

```csharp
ConferenceJoinOptions options = new ConferenceJoinOptions();
options.JoinMode = JoinMode.TrustedParticipant;

conversation.ConferenceSession.BeginJoin(
    options,
    this.JoinCompleted,
    null /*state*/);
```

### Creating an AudioVideoCall instance

The application now creates an [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api) instance. It should set the [RemoveFromDefaultRouting](https://msdn.microsoft.com/en-us/library/hh349908\(v=office.16\)) property to true. This property can be accessed from the [AudioVideoMcuDialInOptions](https://msdn.microsoft.com/en-us/library/hh348625\(v=office.16\)) property on an [AudioVideoCallEstablishOptions](https://msdn.microsoft.com/en-us/library/hh382857\(v=office.16\)) instance.

```csharp
AudioVideoCallEstablishOptions callOptions = new AudioVideoCallEstablishOptions();
callOptions.AudioVideoMcuDialInOptions.RemoveFromDefaultRouting = true; // Required

AudioVideoCall avCall = new AudioVideoCall(conversation); 
```

Setting the **RemoveFromDefaultRouting** property to true is required because the call is established without any MCU routing; therefore any audio coming from the call is not routed to other MCU participants, and any audio from all the other MCU participants is not routed to the call.

Establishing the call without MCU routing avoids audio blips and simplifies implementation logic for the application developer. The application developer can attach players to the created flow immediately without being concerned about audio leaking to or from the call.

### Establishing the call

Before establishing the call, the application sets the [ApplicationContext](https://msdn.microsoft.com/en-us/library/hh383130\(v=office.16\)) property on the call to that of the user’s [ConversationParticipant](https://msdn.microsoft.com/en-us/library/hh366199\(v=office.16\)) instance and registers for notification of the relevant call events. The application can then establish the call as shown in the following example.


> [!NOTE]
> <P>The callOptions parameter was initialized in the previous example.</P>



```csharp
// The application sets the context to the ConversationParticipant instance of the user it represents.
avCall.ApplicationContext = pstnUser1;
avCall.AudioVideoFlowConfigurationRequested += ConfigurationRequested;

avCall.BeginEstablish(callOptions, AvCallEstablished, state);
```

**MCU activation**

MCUs are not necessarily activated immediately after the application has joined the conference. A call should be placed after the MCU becomes active (when the [StateChanged](https://msdn.microsoft.com/en-us/library/hh349760\(v=office.16\)) event on the [McuSession](https://msdn.microsoft.com/en-us/library/hh384975\(v=office.16\)) or [AudioVideoMcuSession](https://msdn.microsoft.com/en-us/library/hh385298\(v=office.16\)) instance is raised). However, in order to improve usability for the application developer, Microsoft Unified Communications Managed API 5.0 allows applications to invoke **BeginEstablish** on the call immediately after the join process is completed. If the MCU is not yet active, the dial-in request is placed in a queue for up to 30 seconds. If the 30-second period expires without the MCU becoming active, UCMA 5.0 still sends the dial-in request as a best effort in case the MCU activation notification was dropped.

**Multi-call establishment and support for multiple points of conference**

The application can establish as many **AudioVideoCall** instances as it requires. This is possible only by the use of the TCU model. Non-TCU conversations and peer-to-peer scenarios do not permit multiple calls for the same modality. MCUs in Skype for Business Server 2015 do not support multiple points of conference. To work around this limitation, the platform creates each call with a fictitious identity.


> [!NOTE]
> <P>To avoid confusion, this identity is not exposed to the application. The generated identity is controlled by the <A href="https://msdn.microsoft.com/en-us/library/hh382405(v=office.16)">UseGeneratedIdentityForTrustedConference</A> property.</P>



The platform is responsible for using the appropriate identity whenever an operation is performed on the call, so the application does not need to know this identity. The fictitious identity is also part of the \<trusted-users\> section of the roster so that ordinary clients such as Skype for Business 2015 do not display it in their user interfaces. Calls can be established in parallel. Applications can choose to create a pool of calls (all associated with an [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) instance) based on their own needs. It is the application’s responsibility to track the call instance that serves a specific user or group of users. It is recommended that the application use the [ApplicationContext](https://msdn.microsoft.com/en-us/library/hh383130\(v=office.16\)) property to accomplish this.

### Customizing MCU routing

After the call is established, the application can specify custom audio routes to and from the call. The following example code demonstrates routing customization. Note that routing customization is valid only when the application has joined the conference as a trusted user and the call has been established. The following example demonstrates how to specify custom routing.

```csharp
// This code demonstrates a personal virtual assistant (PVA) scenario with an incoming route that is DTMF enabled.
List<OutgoingAudioRoute> outgoingRoutes = new List<OutgoingAudioRoute>();
outgoingRoutes.Add(new OutgoingAudioRoute(pstnUser1Endpoint));

IncomingAudioRoute incomingRoute = new IncomingAudioRoute(pstnUser1Endpoint);
incomingRoute.IsDtmfEnabled = true;
avCall.AudioVideoMcuRouting.BeginUpdateAudioRoutes(
                                outgoingRoutes,
                                incomingRoute,
                                this.UpdateCompleted,
                                null);
```

To customize MCU routing, the platform needs information in the notification sent by the Focus to the application. This information indicates that the application has joined the audio-video MCU, and includes the following:

  - The identity of the flow owner, which in this case is the fictitious ID.

  - The GUID of the endpoint that is assigned to the connection in the C3P notification.

  - The source and sink endpoint GUIDs from the C3P notification.

As shown in the preceding list, sending an MCU routing command is dependent on receiving specific C3P notifications. This leads to the following race conditions:

1.  Receiving notification for call establishment
    
    This information comes from the notification sent by the MCU by way of the Focus to the application. However, because call establishment and MCU notification occur on separate dialogs, ordering of notifications relative to call establishment is not guaranteed. For this reason, it is possible for the notification to arrive after the call is completed.
    
    To simplify the developer experience, the application can call **AudioVideoMcuRouting.BeginUpdateAudioRoutes** on the call as soon as the call is established. The platform responds by queuing this request for up to 30 seconds. If no notification is received, this request fails.
    
    The platform also guarantees order. If the application calls **BeginUpdateAudioRoutes** several times on the same call, and the notification from the MCU has not been received, the calls to **BeginUpdateAudioRoutes** are queued and resumed in the order they were received. If within 30 seconds the relevant notification is not received, the operation fails with an **OperationFailureException**. This alleviates much work on the application side. As an example, as soon as the call is established, the application can begin updating its outgoing routes to include new PSTN participants joining the audio-video MCU. The application can then call **BeginUpdateAudioRoutes** as new users join without having to wait for the previous call to **BeginUpdateAudioRoutes** to complete.

2.  Routing audio from a remote source
    
    When the application calls **BeginUpdateAudioRoutes** to route audio from a remote source to itself, it is possible for the remote source to leave the conference before the update operation completes. In this circumstance the platform throws **InvalidOperationException** if it determines that the remote source left before the update request was sent. If the update request was sent before the remote source left, the platform relies on the audio-video MCU to fail the request and return the appropriate diagnostics information. The platform in turn throws [ConferenceFailureException](https://msdn.microsoft.com/en-us/library/hh382829\(v=office.16\)), which exposes diagnostics information.

3.  Routing audio to remote sinks
    
    When the application calls **BeginUpdateAudioRoutes** to route audio to remote sinks, it is possible for some of these sinks to leave the conference before the update operation completes.
    
      - If all sinks have left (causing the list of sinks to be empty or null), the platform throws **ArgumentException**. If all participants have left the conference, the platform throws **InvalidOperationException**.
    
      - If some (but not all) participants have left the conference before the update operation completes, the update operation is sent for the remaining participants. The audio-video MCU should not fail this request.
    
      - If all participants left the conference after the command is sent on the wire, the audio-video MCU fails the request. In this case the platform throws [ConferenceFailureException](https://msdn.microsoft.com/en-us/library/hh382829\(v=office.16\)).

### Issuing a command in the context of another user

The application is now ready to serve its users. Generally speaking, the application will need to monitor the entire conference roster or a specific MCU roster to be able to make announcements or play specific media clips to its users. As mentioned previously, it is the application’s responsibility to determine which call instance (and in particular which flow instance) corresponds to a specific user that it serves.

Using the TCU model, the application can issue commands within the context of other users who are unable to issue conference commands because they are not connected to the Focus (such as PSTN users). The following example demonstrates issuing a mute command in the context of a PSTN user.

```csharp
MuteOptions options = new MuteOptions();
options.Issuer = pstnUser1;

// The mute operation is performed in the context of pstnUser1.
avmcuSession.BeginMute(endpointToMute, options, this.MuteCompleted, null);
```

### Terminating and disposing the call

The application should terminate the call based on its own criteria or when all of the participants it serves have left the conference. The platform terminates the call if the audio-video MCU rolls over or if it sends a notification indicating the call is no longer connected to the audio-video MCU. UCMA 5.0 considers that an application has left the conference if it receives a notification from the server.


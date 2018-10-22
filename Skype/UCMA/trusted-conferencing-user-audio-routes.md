---
title: Trusted conferencing user audio routes
TOCTitle: Trusted conferencing user audio routes
ms:assetid: 380ef3fd-5da8-48ec-8df8-3a453c030914
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466008(v=office.16)
ms:contentKeyID: 65239950
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Trusted conferencing user audio routes


**Applies to**: Skype for Business 2015



This topic describes a number of operations that can be performed after a trusted conferencing user has joined a conference.

## Silent monitoring

In each of the help desk scenarios a customer makes a call to the help desk to talk with an agent to resolve a problem. The agent and the customer are initially joined to a conference so that if necessary, a third party (an expert), can be joined easily to help the agent.

From time to time, a supervisor might want to monitor conversations between the agent, expert, and customer to ensure things are running smoothly. The supervisor wants to be invisible to the other participants by not showing up in their attendance lists. (This invisibility is a feature of the TCU model.) In addition, the default audio configuration for new participants is for each participant to be able to send and receive audio from all other participants, which differs from the desire of the supervisor. The supervisor wants to hear what is happening, but does not want to contribute any audio to the conference.

To configure the supervisor correctly requires changing individual audio routes, so that the supervisor hears the audio from the other participants, but the others do not hear the supervisor. The correct technique is to turn off the default routing behavior of the audio-video MCU (AVMCU) when the supervisor joins, and then set the routes as desired. Because there are two ways a supervisor can be added to the conference, the appropriate options must be set.

When the application is joining the AVMCU on behalf of the supervisor by calling in, the [RemoveFromDefaultRouting](https://msdn.microsoft.com/en-us/library/hh349908\(v=office.16\)) property must be set on the [AudioVideoCallEstablishOptions](https://msdn.microsoft.com/en-us/library/hh382857\(v=office.16\)) instance as shown in the following example.

```csharp
AudioVideoCallEstablishOptions options = new AudioVideoCallEstablishOptions();
options.AudioVideoMcuDialInOptions.RemoveFromDefaultRouting = true;

avCall.BeginEstablish(options, null, null);
```

If the AVMCU is asked to call the application that represents the supervisor using a dial out, then the setting must be made on the [RemoveFromDefaultRouting](https://msdn.microsoft.com/en-us/library/hh384839\(v=office.16\)) property on the [AudioVideoMcuDialOutOptions](https://msdn.microsoft.com/en-us/library/hh384541\(v=office.16\)) class.

```csharp
AudioVideoMcuDialOutOptions options = new AudioVideoMcuDialOutOptions();
options.RemoveFromDefaultRouting = true;

avmcu.BeginDialOut(
    "sip:supervisor@contoso.com",
    options,
    this.DialOutCompleted,
    null /*state*/);
```

After joining, the supervisor has no audio routes and cannot hear anything or be heard by anyone. It is now necessary to add audio routes that allow the audio from the existing participants to be heard by the supervisor. This can be accomplished with code similar to what is shown in the following example.

```csharp
List<OutgoingAudioRoute> outgoingRoutes = new List<OutgoingAudioRoute>();
List<IncomingAudioRoute> incomingRoutes = new List<IncomingAudioRoute>();

IncomingAudioRoute incomingRoute = null;
            
incomingRoute = new IncomingAudioRoute(_customerParticipantEndpoint);
incomingRoute.IsDtmfEnabled = false;
incomingRoute.Operation = RouteUpdateOperation.Add;
incomingRoutes.Add(incomingRoute);

incomingRoute = new IncomingAudioRoute(_agentParticipantEndpoint);
incomingRoute.IsDtmfEnabled = false;
incomingRoute.Operation = RouteUpdateOperation.Add;
incomingRoutes.Add(incomingRoute);

incomingRoute = new IncomingAudioRoute(_expertParticipantEndpoint);
incomingRoute.IsDtmfEnabled = false;
incomingRoute.Operation = RouteUpdateOperation.Add;
incomingRoutes.Add(incomingRoute);

callToMcu.AudioVideoMcuRouting.BeginUpdateAudioRoutes(
    outgiongRoutes, 
    incomingRoutes, 
    this.UpdateRoutesCompleted,
    null /*state*/);
```

## Private audio channel

After monitoring the conversation for a while, the supervisor might want to consult privately with the agent on how to work with the customer. To do this, an outgoing audio route must be added between the supervisor and the agent. In addition, any responses the agent might make to the supervisor would be heard by both the customer and the expert so the routes should be modified to make the responses private.

Two steps are needed. The first step is to remove the agent from the default audio mix so that there are no routes. The second step is to add incoming and outgoing routes between the agent and the supervisor. This can be accomplished using code similar to the following example.

```csharp
private void EstablishPrivateAudioChannel()
{
  RemoveFromDefaultRoutingOptions options =
       new RemoveFromDefaultRoutingOptions();
  options.Duration = TimeSpan.FromMilliseconds(3600000);

  _avmcu.BeginRemoveFromDefaultRouting(
        _agentParticipantEndpoint,
        options,
        this.RemoveFromDefaultRoutingCompleted,
        null /*state*/);                                                              
}

private void RemoveFromDefaultRoutingCompleted(IAsyncResult result)
{
  try
  {
    _avmcu.EndRemoveFromDefaultRouting(result);

    // Now add private audio channel between supervisor and agent.
    OutgoingAudioRoute outgoingRoute = 
          new OutgoingAudioRoute(_agentParticipantEndpoint);
    outgoingRoute.Operation = RouteUpdateOperation.Add;
    IncomingAudioRoute incomingRoute = 
          new IncomingAudioRoute(_agentParticipantEndpoint);
    incomingRoute.IsDtmfEnabled = true;
    incomingRoute.Operation = RouteUpdateOperation.Add;

    _callToMcu.AudioVideoMcuRouting.BeginUpdateAudioRoutes(
        new List<OutgoingAudioRoute>() { outgoingRoute },
        new List<IncomingAudioRoute>() { incomingRoute },
        this.UpdateAudioRoutesCompleted,
        null /*state*/);
  }

  catch (RealTimeException exception)
  {
      // TODO: Handle exception properly
  }
}
```


> [!NOTE]
> <P>This solution does not account for the fact that the agent can no longer hear the customer, and the customer might not know that he is temporarily unable talk to the agent. For a better customer experience, the application design might require some additional routes to be modified.</P>




> [!NOTE]
> <P>If agents are aware that they cannot respond to the supervisor because no route is set up, it might not be necessary to remove the agent from the default mix as illustrated in this example. The supervisor can add the agent back into the default routing by adding the agent back to the default mix.</P>



## Supervisor barge-in

While monitoring the agent, the supervisor might decide that the agent is not ready to solve the customer problem. The supervisor might need to take over the conversation by talking directly with the customer. To accomplish this, the application can add outbound audio routes to each participant in the AVMCU so that the supervisor’s audio can be heard. This can be done with code similar to the following.

```csharp
List<OutgoingAudioRoute> outgoingRoutes = new List<OutgoingAudioRoute>();
List<IncomingAudioRoute> incomingRoutes = new List<IncomingAudioRoute>();

OutgoingAudioRoute outgoingRoute = null;
            
outgoingRoute = new OutgoingAudioRoute(_customerParticipantEndpoint);
outgoingRoute.Operation = RouteUpdateOperation.Add;
outgoingRoutes.Add(outgoingRoute);

outgoingRoute = new OutgoingAudioRoute(_agentParticipantEndpoint);
outgoingRoute.Operation = RouteUpdateOperation.Add;
outgoingRoutes.Add(outgoingRoute);

outgoingRoute = new OutgoingAudioRoute(_expertParticipantEndpoint);
outgoingRoute.Operation = RouteUpdateOperation.Add;
outgoingRoutes.Add(outgoingRoute);
            
_callToMcu.AudioVideoMcuRouting.BeginUpdateAudioRoutes(
        outgoingRoutes,
        incomingRoutes,
        this.UpdateAudioRoutesCompleted,
        null /*state*/);
```


> [!NOTE]
> <P>There is a disadvantage in this technique because the customer would not see the supervisor in the roster of the Skype for Business 2015 application as was the case with the agent. For this technique to work as expected, the supervisor should join the conference again, not as a trusted conference user, but under his own identity, so that he appears in the roster.</P>



## Adding to and removing from the default mix

At any time, it might be necessary to put the customer on hold during the session with the help desk. To accomplish this, an application such as a music-on-hold (MoH) server joined as a TCU, can remove the customer from the default mix, and can add an outgoing route on which to play music, from itself to the customer. While the customer is on hold, the agent, expert, and any supervisors can discuss strategy while the customer is waiting. The following example shows an application acting as a music-on-hold server. The example assumes that the MoH application is already joined to the conference.

```csharp
private void PutCustomerOnHold()
{
  RemoveFromDefaultRoutingOptions options = new RemoveFromDefaultRoutingOptions();
  options.Duration = TimeSpan.FromMilliseconds(3600000);

  _avmcu.BeginRemoveFromDefaultRouting(
      _customerParticipantEndpoint,
      options,
      this.RemoveFromDefaultRoutingCompleted,
      null /*state*/);                                                              
}


private void RemoveFromDefaultRoutingCompleted(IAsyncResult result)
{
  try
  {
      _avmcu.EndRemoveFromDefaultRouting(result);

      // Now add private audio channel between supervisor and agent.
      OutgoingAudioRoute outgoingRoute = 
            new OutgoingAudioRoute(_agentParticipantEndpoint);
      outgoingRoute.Operation = RouteUpdateOperation.Add;

      _callToMcu.AudioVideoMcuRouting.BeginUpdateAudioRoutes(
            new List<OutgoingAudioRoute>() { outgoingRoute },
            new List<IncomingAudioRoute>(),
            this.UpdateAudioRoutesCompleted,
            null /*state*/);
  }

  catch (RealTimeException exception)
  {
      // TODO: Handle error properly
  }
}
```


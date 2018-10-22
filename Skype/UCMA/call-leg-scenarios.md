---
title: Call leg scenarios
TOCTitle: Call leg scenarios
ms:assetid: f3eced9e-fb4e-4be8-8125-6cb46e164ca9
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466021(v=office.16)
ms:contentKeyID: 65239935
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Call leg scenarios


**Applies to**: Skype for Business 2015

 

The [BackToBackCall](https://msdn.microsoft.com/en-us/library/hh365598\(v=office.16\)) class can be used in two scenarios:

  - Incoming-Idle scenario

  - Click-to-call scenario

## Incoming-idle scenario

The following illustration shows the schematic relationship between a customer (the inbound call), the back-to-back user agent, and the agent (the outbound call).

![Relationship between a customer and agents](images/Dn466021.UCMA3-AstanaArch(Office.16).png "Relationship between a customer  and agents")

The following illustration shows the signaling and media channels for this scenario. A disadvantage of this arrangement is that only one far end (the agent) is possible. An advantage is that this arrangement is very scalable, due to the peer-to-peer flow of media between the two participants.

![Signaling and media channels - customer and agents](images/Dn466021.UCMA3-AstanaChannels(Office.16).png "Signaling and media channels - customer and agents")

The following code example demonstrates how to create a [BackToBackCall](https://msdn.microsoft.com/en-us/library/hh365598\(v=office.16\)) instance. The callback method referred to by the EstablishCompleted parameter in the call to [BeginEstablish(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh384667\(v=office.16\)) is not shown.

```csharp
// Create a new call for the outgoing leg.
AudioVideoCall call2 = new AudioVideoCall(conversation2);

// Call settings for the incoming call leg
// call1 is the existing AudioVideoCall instance for the incoming call
BackToBackCallSettings call1Settings = new BackToBackCallSettings(call1);

// Call settings for the outgoing call leg
BackToBackCallSettings call2Settings = new BackToBackCallSettings(call2, destinationUri2);

BackToBackCall b2bCall = new BackToBackCall(call1Settings, call2Settings);
b2bCall.BeginEstablish(EstablishCompleted, b2bCall);
```

## Connecting incoming-idle call legs - two-party call

The Incoming-Idle scenario is also referred to as "agent-hunting." In this scenario, an incoming call is received from a customer. The application searches for an available agent and establishes a back-to-back call between the customer and an appropriate agent without putting the customer on hold. Schematically, this scenario is similar to the previous ("click to call") scenario, except that it includes an exception where the customer initially calls an interactive voice response (IVR) application. After determining the nature of the customer’s call, the application selects an agent, and then transfers the customer’s call from the IVR to the back-to-back user agent.

The following code example shows how to create a **BackToBackCall** instance for this scenario. This example assumes the existence of a previously created [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) instance, and that conversation is a reference to this instance. The incoming call from the customer (incomingCall) is also assumed to be initialized appropriately. As in the previous example, the callback method referred to by the EstablishCompleted parameter in the call to [BeginEstablish(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh384667\(v=office.16\)) is not shown.

```csharp
// The conversation for the outbound call is assumed to already exist.
AudioVideoCall outboundCall = new AudioVideoCall(conversation);
BackToBackCallSettings outboundCallSettings = new BackToBackCallSettings(outboundCall, agentUri);

// incomingCall is assumed to be already stored in the class.
BackToBackCallSettings incomingCallSettings = new BackToBackCallSettings(incomingCall);
BackToBackCall b2bCall = new BackToBackCall(inboundCallSettings, outboundCallSettings);
b2bCall.BeginEstablish(EstablishCompleted, b2bCall);
```

## Connecting incoming-idle call legs - conference call

In an Incoming-Idle call in a conference, the customer call is hooked as back-to-back to a conference, and the agent joins the conference. The following illustration shows the relationships between the customer, the conference, and several agents. Advantages of this type of arrangement are the possibility of multiple agents in the conference simultaneously, conferencing command and control, and conference recording. The disadvantage of this arrangement is that it is less scalable than the two-party call, particularly if the MCU is distant geographically from the customer and agents.

![Relationship among customer, conference and agents](images/Dn466021.UCMA3-AspectArch(Office.16).jpg "Relationship among customer, conference and agents")

The following illustration shows the signaling and media channels for the conference call arrangement. The dashed lines indicate the signaling channel and the solid lines indicate the media channel.

![Signaling and media channels for conference call](images/Dn466021.UCMA3-AspectChannels(Office.16).png "Signaling and media channels for conference call")

The following code example shows the call to [BeginJoin(ConferenceJoinOptions, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh348502\(v=office.16\)).

```csharp
ConferenceJoinOptions cjo = new ConferenceJoinOptions();
cjo.JoinAsTrustedApplication = false;
conferenceSession.BeginJoin(cjo, this.ConferenceJoinCompleted, conferenceSession);
```

The following code example shows the definition of the callback method that appears as a parameter in the previous example. In this example, a **BackToBackCall** instance is created with the customer’s incoming call as one leg and the connection to the conference as the other leg.

```csharp
private void ConferenceJoinCompleted(IAsyncResult result)
{
  ConferenceSession session = (ConferenceSession)result.AsyncState;
  session.EndJoin(result);
  AudioVideoCall outboundCall = new AudioVideoCall(session.Conversation);
  BackToBackCallSettings outboundCallSettings = new BackToBackCallSettings(outboundCall);

  // incomingCall is assumed to be already stored in the class. 
  BackToBackCallSettings incomingCallSettings = new BackToBackCallSettings(incomingCall);

  BackToBackCall b2bCall = new BackToBackCall(inboundCallSettings, outboundCallSettings);
  b2bCall.BeginEstablish(EstablishCompleted, b2bCall);
}
```

## Click-to-call scenario

In the click-to-call scenario, a Web page provides customer information (contextual data) to the UCMA 5.0 application. When the customer clicks a control in a Web page, an interactive voice response (IVR) application calls the customer. The UCMA 5.0 application then self-transfers the call from the IVR application to the UCMA 5.0 application. The self-transfer turns the click-to-call scenario into the incoming-idle scenario (the agent or conference call leg is idle). Using the customer’s contextual data, the UCMA 5.0 application connects the back-to-back user agent to the appropriate agent or conference.

## See also

- [Video demo - Lync 2013: How to do back-to-back calls using UCMA 4](https://channel9.msdn.com/posts/lync-2013-how-to-do-back-to-back-calls-using-ucma-4)


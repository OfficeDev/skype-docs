---
title: Establishing an outgoing call
TOCTitle: Establishing an outgoing call
ms:assetid: 95252d82-78b0-4ae6-b678-126114b57684
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465988(v=office.16)
ms:contentKeyID: 65239930
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Establishing an outgoing call


**Applies to**: Skype for Business 2015

When an application establishes an outgoing call, it can specify custom signaling headers to be sent out with the outgoing INVITE, and optionally, custom MIME parts to be included in any offer.

The following sample demonstrates creating a new audio/video conversation, implementing and registering a call event handler, and establishing the call. The code for an instant message call is similar.


> [!NOTE]
> Applications must register for call events before the call is established.




```csharp
// Set up the conversation and place the call.
ConversationSettings convSettings = new ConversationSettings();
convSettings.Priority = conversationPriority;
convSettings.Subject = conversationSubject;

// A conversation represents a collection of modalities in the context of a dialog with one or multiple callees.
Conversation conversation = new Conversation(userEndpoint, convSettings);

audioVideoCall = new AudioVideoCall(conversation);


// Subscribe for the flow configuration requested event; the flow will be used to send the media.
// Ultimately, as a part of the callback, the media will be sent/received.
audioVideoCall.AudioVideoFlowConfigurationRequested += this.audioVideoCall_FlowConfigurationRequested;
 
// The party to call.
calledParty = "sip:alice@contoso.com";

// Place the call to the remote party.
audioVideoCall.BeginEstablish(calledParty, null, EndCallEstablish, audioVideoCall);
.
.
.
// Handler for the AudioVideoFlowConfigurationRequested event on the AudioVideoCall.
// Flow created indicates that there is a flow present to begin media operations with, and that it is no longer null.
public void audioVideoCall_FlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
{
  Console.WriteLine("Flow Created.");
  audioVideoFlow = e.Flow;
 
  // Now that the flow is non-null, bind the event handler for StateChanged.
  // When the flow goes active, (as indicated by the state changed event) the program can choose to take media-related actions on the flow.
  audioVideoFlow.StateChanged += new EventHandler<MediaFlowStateChangedEventArgs>(audioVideoFlow_StateChanged);
}

// Handler for the StateChanged event on the AudioVideoFlow.
private void audioVideoFlow_StateChanged(object sender, MediaFlowStateChangedEventArgs e)
{
  Console.WriteLine("Flow state changed from " + e.PreviousState + " to " + e.State);

  // When flow is active, media operations can begin.
  if (e.State == MediaFlowState.Active)
  {
    // Take appropriate action when the flow is Active.
  }
}


private void EndCallEstablish(IAsyncResult ar)
{
  Call call = ar.AsyncState as Call;
  try
  { 
    call.EndEstablish(ar);
    Console.WriteLine("The call with Local Participant: " + call.Conversation.LocalParticipant + " and Remote Participant: " + call.RemoteEndpoint.Participant + " is now in the established state.");
  }

  catch (OperationFailureException opFailEx)
  {
    // OperationFailureException: Indicates failure to connect the call to the remote party.
    // An application should perform real error-handling here.
     Console.WriteLine(opFailEx.ToString());
  }

  catch (RealTimeException exception)
  {
    // RealTimeException may be thrown on media or link-layer failures.
    // An application should perform real error-handling here.
    Console.WriteLine(exception.ToString());
  }

}
```


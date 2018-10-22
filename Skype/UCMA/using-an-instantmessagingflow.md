---
title: Using an InstantMessagingFlow
TOCTitle: Using an InstantMessagingFlow
ms:assetid: 7ba32e50-db6b-43a3-b3f2-f6bf6963740c
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466035(v=office.16)
ms:contentKeyID: 65239963
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Using an InstantMessagingFlow

**Applies to**: Skype for Business 2015

[InstantMessagingFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow?view=ucma-api) instances represent an instant messaging (IM) media flow. The **InstantMessagingFlow** class is derived from the [MediaFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaflow?view=ucma-api) base class and provides methods to send and receive IM messages, delivery notifications, and typing notifications.

## Composing notifications

A composing notification indicates whether a participant is actively composing a message. A composing notification is an INFO message that is sent to the remote endpoint when the local participant is typing.

The **InstantMessagingFlow** class exposes a LocalComposingState property to represent the local composing state. Setting the [LocalComposingState](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.localcomposingstate?view=ucma-api) property to Composing causes the platform to automatically send composing notifications to the remote participant every five seconds. The platform stops sending composing notifications if the application explicitly sets the value of **LocalComposingState** to **Idle**. When the user sends the instant message, the platform stops sending composing notifications and automatically sets the value of **LocalComposingState** to **Idle**.

The InstantMessageFlow class raises a [RemoteComposingStateChanged](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.remotecomposingstatechanged?view=ucma-api) event to represent the composing state of the remote participant in an IM conversation. The event is raised whenever the remote participant sends a composing notification in an INFO message and when the state goes to Idle as a result of a timeout resulting from not getting the composing message on time. For this reason, the **RemoteComposingStateChanged** event is raised even when there is no state change, to allow IM bridging implementations to propagate typing indications properly.

## Creating and manipulating an IM media flow

The following code demonstrates creating an IM media flow, sending IM messages on the flow, and sending composing notifications. In this example, an IM message is sent in the call to one of the overloaded [BeginSendInstantMessage](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.beginsendinstantmessage?view=ucma-api#overloads) messages.

```csharp
// Must be done before Establish is called.
imCall.InstantMessagingFlowConfigurationRequested += IMCall_InstantMessagingFlowConfigurationRequested; 

private void IMCall_InstantMessagingFlowConfigurationRequested (object sender, InstantMessagingFlowConfigurationRequestedEventArgs e)
{
  InstantMessagingFlow imFlow = e.Flow;
  imFlow.StateChanged += IMFlow_StateChanged;
  imFlow.RemoteComposingStateChanged += IMFlow_RemoteComposingStateChanged;
}

private void IMCall_RemoteComposingStateChanged (object sender, ComposingStateChangedEventArgs e)
{
  // e.Participant indicates the remote participant.
  // e.ComposingState indicates the composing state of this remote participant.
}

private void IMCall_StateChanged(object sender, StateChangedEventArgs<MediaFlowState> e)
{
  // Idle, Active, Terminated are the states. 
  // If Active, the flow is ready to send and receive media.
}

// Send a message on the flow.
myFlow.BeginSendInstantMessage("hi", IMFlow_SendMessageCompleted, myFlow);

// Sending a composing notification.
myFlow.LocalComposingState = ComposingState.Composing;

// Setting the composing state to Idle cancels the automatic sending of composing notifications.
// Sending the instant message also cancels the automatic sending of composing notifications.
myFlow.LocalComposingState = ComposingState.Idle;
```


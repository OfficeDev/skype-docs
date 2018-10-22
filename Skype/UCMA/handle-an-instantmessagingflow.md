---
title: Handle an InstantMessagingFlow
TOCTitle: Handle an InstantMessagingFlow
ms:assetid: 5d21b345-350d-4a09-915e-03bd48545f70
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466028(v=office.16)
ms:contentKeyID: 65239964
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Handle an InstantMessagingFlow


**Applies to**: Skype for Business 2015


[InstantMessagingFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow?view=ucma-api) instances represent an instant messaging (IM) media flow. The **InstantMessagingFlow** class is derived from the [MediaFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaflow?view=ucma-api) base class and provides methods to send and receive IM messages, delivery notifications, and typing notifications.

## InstantMessagingFlowTemplate

The **InstantMessagingFlow** class has no public constructors, but instead relies on settings in an [InstantMessagingFlowTemplate](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflowtemplate?view=ucma-api) instance to initialize or modify an **InstantMessagingFlow** instance. After an **InstantMessagingFlowTemplate** instance has been created, its settings can be copied to an **InstantMessagingFlow** instance by a call to the [Initialize(InstantMessagingFlowTemplate)](https://msdn.microsoft.com/en-us/library/hh382523\(v=office.16\)) method on the **InstantMessagingFlow** class. This initialization must occur within the body of a handler for the [InstantMessagingFlowConfigurationRequested](https://msdn.microsoft.com/en-us/library/hh384346\(v=office.16\)) event.

The [InstantMessagingSettings](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform.instantmessagingsettings?view=ucma-api) property provides access to global configuration settings for instant messaging (IM) flows using an **InstantMessagingFlowTemplate** object.

Applications specify their consumption mode using the [InstantMessageConsumptionMode](https://msdn.microsoft.com/en-us/library/hh366078\(v=office.16\)) enumeration. The following table lists the enumeration values for this type.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Enumeration value</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><strong>ConsumedLocally</strong></p></td>
<td><p>The message is consumed locally as soon as it is delivered. This is the default consumption mode.</p></td>
</tr>
<tr class="even">
<td><p><strong>ProxiedToRemoteEntity</strong></p></td>
<td><p>The message is proxied to a remote entity. The success or failure of the delivery is known only later.</p></td>
</tr>
</tbody>
</table>


If the application hosts a remote client, there can be a delay when the client sends the typing indications to a backend server. For this reason, the default value of three seconds for the idle time-out period may not be long enough. The application can set the idle time-out period to a larger value using flow settings.

The [SupportedFormats](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflowtemplate.supportedformats?view=ucma-api) property lists the content types that the application supports for consuming messages received in the flow.

The internal IM media provider supports the sending and receiving of delivery notification messages that are sent by applications that do not directly consume messages, but serve as proxies for delivering the received messages to remote entities. A user-created Web server application is an example of such an application. If an application configures the flow as **ProxiedToRemoteEntity**, the application must register for the [MessageReceived](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.messagereceived?view=ucma-api) event on the flow, and must send message delivery notifications with the [BeginSendSuccessDeliveryNotification(InstantMessageId, AsyncCallback, Object)](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.beginsendsuccessdeliverynotification?view=ucma-api) or [BeginSendFailureDeliveryNotification(InstantMessageId, Int32, AsyncCallback, Object)](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.beginsendfailuredeliverynotification?view=ucma-api) method. Because the messages are serialized, there might be an impact on application performance.

**InstantMessagingFlow** exposes the [DeliveryNotificationReceived](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.deliverynotificationreceived?view=ucma-api) event, which is raised when an incoming delivery notification message is received. **InstantMessagingFlow** also exposes two methods to send delivery notifications to the message sender: BeginSendFailureDeliveryNotification and BeginSendSuccessDeliveryNotification.

## Composing notifications

A composing notification indicates whether a participant is actively composing a message. A composing notification is a SIP INFO message that is sent to the remote endpoint when the local participant is typing.

The **InstantMessagingFlow** class exposes a LocalComposingState property to represent the local composing state. Setting the [LocalComposingState](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.localcomposingstate?view=ucma-api) property to Composing causes the platform to automatically send composing notifications to the remote participant every five seconds. The platform stops sending composing notifications if the application explicitly sets the value of **LocalComposingState** to **Idle**. When the user sends the instant message, the platform stops sending composing notifications and automatically sets the value of **LocalComposingState** to **Idle**.

The InstantMessageFlow class raises a [RemoteComposingStateChanged](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.remotecomposingstatechanged?view=ucma-api) event to represent the composing state of the remote participant in an IM conversation. The event is raised only when the remote participant sends a composing notification; setting the **LocalComposingState** property for an endpoint does not raise the **RemoteComposingStateChanged** event on the remote participant. For this reason, and because composing notifications are sent every five seconds, if the **RemoteComposingStateChanged** event is not raised within six seconds, the application should assume that the remote participant is idle.

## Creating and manipulating an IM media flow

The following code demonstrates creating an IM media flow, sending IM messages on the flow, and sending composing notifications.

```csharp
// Must be done before Establish is called.
imCall.InstantMessagingFlowConfigurationRequested += IMCall_InstantMessagingFlowConfigurationRequested; 

private void IMCall_InstantMessagingFlowConfigurationRequested (object sender, InstantMessagingFlowConfigurationRequestedEventArgs e)
{
  InstantMessagingFlow imFlow = e.Flow;
  imFlow.StateChanged += IMFlow_StateChanged;
  imFlow.RemoteComposingStateChanged += IMFlow_RemoteComposingStateChanged;
}

private void IMCall_ RemoteComposingStateChanged (object sender, ComposingStateChangedEventArgs e)
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
myFlow.BeginSendMessage("hi", IMFlow_SendMessageCompleted, myFlow);

// Sending a composing notification.
myFlow.LocalComposingState = ComposingState.Composing;

// Setting the composing state to Idle cancels the autmomatic sending of composing notifications.
// Sending the instant message also cancels the automatic sending of composing notifications.
myFlow.LocalComposingState = ComposingState.Idle;
```


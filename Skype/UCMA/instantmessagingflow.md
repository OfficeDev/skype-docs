---
title: InstantMessagingFlow
TOCTitle: InstantMessagingFlow
ms:assetid: 5cb8451a-a295-4a0b-b6b5-391b27a6c485
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466025(v=office.16)
ms:contentKeyID: 65239962
ms.date: 07/27/2015
mtps_version: v=office.16
---

# InstantMessagingFlow


**Applies to**: Skype for Business 2015

An [InstantMessagingFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow?view=ucma-api) instance represents an instant messaging connection with a single remote participant.

The [ComposingTimeoutValue](https://msdn.microsoft.com/en-us/library/hh349262\(v=office.16\)) property is used to control the time-out value that reverts the local composing state to **Idle**. If an application sets [LocalComposingState](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.localcomposingstate?view=ucma-api) to **Composing**, it is automatically reverted to **Idle** if the application does not set **LocalComposingState** to **Composing** again within this idle time-out. Several seconds can elapse after the time-out occurs for the state to change to **Idle**. An application cannot set **LocalComposingState** to **Idle**.

Getting or setting **LocalComposingState** will result in a "Typing" or "Idle" notification to be sent to remote participants. After being set to **Typing**, automatic refresh of the **Typing** state occurs every three seconds until a message is sent or the state is set explicitly to **Idle**. If a message is sent, it is not necessary to set the state to **Idle** because the composing state will be automatically reset to **Idle**. A [RemoteComposingStateChanged](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.remotecomposingstatechanged?view=ucma-api) event will not be raised when this property is set to **Idle**.

An application uses the [Initialize(InstantMessagingFlowTemplate)](https://msdn.microsoft.com/en-us/library/hh382523\(v=office.16\)) method to configure an **InstantMessagingFlow** instance as **ConsumedLocally** (the default) or as **ProxiedToRemoteEntity**, values of the [InstantMessageConsumptionMode](https://msdn.microsoft.com/en-us/library/hh366078\(v=office.16\)) enumeration. If the flow is configured for **ProxiedToRemoteEntity**, an application must register for the [MessageReceived](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.messagereceived?view=ucma-api) event and must send message delivery notifications using [BeginSendSuccessDeliveryNotification(InstantMessageId, AsyncCallback, Object)](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.beginsendsuccessdeliverynotification?view=ucma-api) or [BeginSendFailureDeliveryNotification(InstantMessageId, Int32, AsyncCallback, Object)](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow.beginsendfailuredeliverynotification?view=ucma-api) on each received message. Failure to send these delivery notification messages can result in a memory leak.


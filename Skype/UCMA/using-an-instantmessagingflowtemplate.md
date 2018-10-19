---
title: Using an InstantMessagingFlowTemplate
TOCTitle: Using an InstantMessagingFlowTemplate
ms:assetid: 742acc0f-282c-4c39-b37f-cd371d3b84d9
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466056(v=office.16)
ms:contentKeyID: 65239961
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Using an InstantMessagingFlowTemplate


_**Applies to:** Skype for Business 2015_

The [InstantMessagingSettings](https://msdn.microsoft.com/en-us/library/hh382884\(v=office.16\)) property provides access to global configuration settings for instant message (IM) flows using an [InstantMessagingFlowTemplate](https://msdn.microsoft.com/en-us/library/hh384311\(v=office.16\)) object.

The following table shows the settings that can be configured for IM flows (represented by [InstantMessagingFlow](https://msdn.microsoft.com/en-us/library/hh383312\(v=office.16\)) objects) using the **InstantMessagingFlowTemplate** class.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Member</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh348847(v=office.16)">InstantMessagingFlowTemplate()</a></p></td>
<td><p>Constructor. Create a new instance of the <strong>InstantMessagingFlowTemplate</strong> class.</p>
<p>InstantMessagingFlowTemplate()</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382980(v=office.16)">InstantMessagingFlowTemplate(InstantMessagingFormat)</a></p></td>
<td><p>Constructor. Create a new instance of the <strong>InstantMessagingFlowTemplate</strong> class, specifying the formats that can be used.</p>
<p>InstantMessagingFlowTemplate(InstantMessagingFormat)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh365904(v=office.16)">InstantMessagingFlowTemplate(InstantMessagingFlowTemplate)</a></p></td>
<td><p>Constructor. Create a new instance of the <strong>InstantMessagingFlowTemplate</strong> class, using values from the specified flow template.</p>
<p>InstantMessagingFlowTemplate(InstantMessagingFlowTemplate)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh350047(v=office.16)">ComposingTimeoutValue</a></p></td>
<td><p>Property. Gets or sets the value (in seconds) for composing time-out.</p>
<p>This property is used to control the default value used for all instant messaging flows. The default value is 3 seconds, and the maximum value is 30 seconds.</p>
<p>int ComposingTimeoutValue {get; set;}</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh383470(v=office.16)">MessageConsumptionMode</a></p></td>
<td><p>Property. Gets or sets the value that an application can use to indicate how it intends to handle instant messages.</p>
<p>The incoming messages are handled accordingly. If the application consumes messages locally, incoming messages are automatically acknowledged with a 200 response code. If the application proxies the messages, the incoming messages are automatically acknowledged with a 202 response code (if the remote supports message delivery notifications) or if the response is delayed. The application is responsible for sending a delivery notification later for every message received. The default value is <a href="https://msdn.microsoft.com/en-us/library/hh366078(v=office.16)">InstantMessageConsumptionMode</a>.<strong>ConsumedLocally</strong>.</p>
<p>InstantMessageConsumptionMode MessageConsumptionMode {get; set;}</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh348968(v=office.16)">SupportedFormats</a></p></td>
<td><p>Property. Gets the value that indicates the consumption mode of the application.</p>
<p></p>
<p>InstantMessagingFormat SupportedFormats {get; set;}</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh350305(v=office.16)">ToastFormatSupport</a></p></td>
<td><p>Property. Gets or sets the toast format support value, a value of the <a href="https://msdn.microsoft.com/en-us/library/hh383956(v=office.16)">CapabilitySupport</a> enumeration.</p>
<p>CapabilitySupport ToastFormatSupport {get; set;}</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh381556(v=office.16)">WaitingForDeliveryNotificationDisabled</a></p></td>
<td><p>Property. Gets or sets the value that controls whether the <a href="https://msdn.microsoft.com/en-us/library/hh349533(v=office.16)">BeginSendInstantMessage</a> operation will wait for delivery notifications before completion.</p>
<p>bool WaitingForDeliveryNotificationDisabled {get; set;}</p></td>
</tr>
</tbody>
</table>


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

The [SupportedFormats](https://msdn.microsoft.com/en-us/library/hh348968\(v=office.16\)) property lists the content types that the application supports for consuming messages received in the flow.

The internal IM media provider supports the sending and receiving of delivery notification messages that are sent by applications that do not directly consume messages, but serve as proxies for delivering the received messages to remote entities. A user-created Web server application is an example of such an application. If an application configures the flow as **ProxiedToRemoteEntity**, the application must register for the [MessageReceived](https://msdn.microsoft.com/en-us/library/hh383170\(v=office.16\)) event on the flow, and must send message delivery notifications with the [BeginSendSuccessDeliveryNotification(InstantMessageId, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh366216\(v=office.16\)) or [BeginSendFailureDeliveryNotification(InstantMessageId, Int32, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh381150\(v=office.16\)) method. Because the messages are serialized, there might be an impact on application performance.

**InstantMessagingFlow** exposes the [DeliveryNotificationReceived](https://msdn.microsoft.com/en-us/library/hh383496\(v=office.16\)) event, which is raised when an incoming delivery notification message is received. **InstantMessagingFlow** also exposes two methods to send delivery notifications to the message sender: [BeginSendFailureDeliveryNotification](https://msdn.microsoft.com/en-us/library/hh381150\(v=office.16\)) and [BeginSendSuccessDeliveryNotification](https://msdn.microsoft.com/en-us/library/hh366216\(v=office.16\)).

An application that uses either of the overloaded [BeginSendInstantMessage](https://msdn.microsoft.com/en-us/library/hh349533\(v=office.16\)) methods has no need to use the **DeliveryNotificationReceived** event, unless **WaitingForDeliveryNotificationDisabled** is set to true. The **BeginSendInstantMessage** methods automatically match incoming delivery notification messages to pending "SendMessage" operations that wait for them.


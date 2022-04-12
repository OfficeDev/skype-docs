---
title: RemotePresenceView (Unified Communications Managed API 5.0)
TOCTitle: RemotePresenceView
ms:assetid: ad07c6ae-299d-4823-a1b9-2979ef545132
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466022(v=office.16)
ms:contentKeyID: 65239954
ms.date: 07/27/2015
mtps_version: v=office.16
---

# RemotePresenceView


**Applies to**: Skype for Business 2015



A presence view is a unified way to request presence information for a set of presentities. The [RemotePresenceView](https://msdn.microsoft.com/library/hh381152\(v=office.16\)) class provides an implementation of this capability.

## RemotePresenceView capabilities

An application can create one or more **RemotePresenceView** instances, each with its own set of remote presentities. Each **RemotePresenceView** instance can be configured by passing a [RemotePresenceViewSettings](https://msdn.microsoft.com/library/hh382861\(v=office.16\)) instance to the **RemotePresenceView** constructor. The [SubscriptionMode](https://msdn.microsoft.com/library/hh349827\(v=office.16\)) property on a **RemotePresenceViewSettings** instance determines how the presence information is obtained for presentities, specifically the subscription mode and polling interval. The [PollingInterval](https://msdn.microsoft.com/library/hh381308\(v=office.16\)) property on the **RemotePresenceViewSettings** instance specifies how often polling should take place.

The **SubscriptionMode** property on a **RemotePresenceViewSettings** instance can have one of the following values.

<table>
<thead>
<tr class="header">
<th><p>Value</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><strong>Default</strong></p></td>
<td><p>The most commonly used mode, where persistent subscription is attempted. If the subscription attempt fails, the view automatically switches to polling mode under certain error conditions (such as when the subscription limit is reached for the presentity).</p></td>
</tr>
<tr class="even">
<td><p><strong>Persistent</strong></p></td>
<td><p>All presence changes receive immediate notifications.</p></td>
</tr>
<tr class="odd">
<td><p><strong>Polling</strong></p></td>
<td><p>A presentity's presence is polled for at specific time intervals. The interval is specified by the <a href="https://msdn.microsoft.com/library/hh385013(v=office.16)">PollingInterval</a> property.</p>
<p>Polling to Public Internet Cloud (PIC) or Federated presentities does not result in presence notifications, as they do not support that subscription mode. For such presentities, use a persistent subscription.</p></td>
</tr>
</tbody>
</table>


Presentities can be dynamically added to or removed from the view any time during the operation of the view, by the use of the [StartSubscribingToPresentities(IEnumerable\<RemotePresentitySubscriptionTarget\>)](https://msdn.microsoft.com/library/hh382387\(v=office.16\)) and [StartUnsubscribingToPresentities(IEnumerable\<String\>)](https://msdn.microsoft.com/library/hh349575\(v=office.16\)) methods. At any time, an application can obtain a snapshot of all presentities in a view, using the [GetPresentities()](https://msdn.microsoft.com/library/hh348629\(v=office.16\)) method.

The state of a subscription to each presentity can be tracked by registering to receive notifications when the [SubscriptionStateChanged](https://msdn.microsoft.com/library/hh383546\(v=office.16\)) event is raised.

If a subscription succeeds, the state of the presence subscription becomes **Subscribed** or **Polling** (values of the [RemotePresentitySubscriptionState](https://msdn.microsoft.com/library/hh382767\(v=office.16\)) enumerated type). The actual presence notifications can be received by registering to receive notifications when the [PresenceNotificationReceived](https://msdn.microsoft.com/library/hh382238\(v=office.16\)) event is raised.

If an exception is thrown in an attempt to subscribe to a presentity, the state of the subscription becomes **Terminated** (a value of the [RemotePresentitySubscriptionState](https://msdn.microsoft.com/library/hh382767\(v=office.16\)) enumerated type)and the remote presentity is automatically removed from the view.

When a view is no longer needed, it can be unattached from the endpoint by calling the [BeginTerminate(AsyncCallback, Object)](https://msdn.microsoft.com/library/hh349615\(v=office.16\)) and [EndTerminate(IAsyncResult)](https://msdn.microsoft.com/library/hh383726\(v=office.16\)) methods.

There is no limit to the number of presentities in a view or the number of views that can be created from an endpoint. All views that have not been terminated on an endpoint can be obtained by calling the [GetRemotePresenceViews()](https://msdn.microsoft.com/library/hh382996\(v=office.16\)) method. This method can be accessed through the on the [PresenceServices](https://msdn.microsoft.com/library/hh384331\(v=office.16\)) property on the endpoint.

**RemotePresenceView** is designed to be more scalable and to optimize network traffic by batching several polling requests and caching the presence of remote presentities locally. Persistent subscriptions to bots and automated services are switched to polling mode automatically as their presence information rarely changes.

The number of presence categories being subscribed to for the **Default** or **Persistent** subscription mode can be modified before the endpoint is created (by modifying the **RemotePresenceSubscriptionCategories** property on either the [UserPresenceSettings](https://msdn.microsoft.com/library/hh350117\(v=office.16\)) or [ApplicationEndpointPresenceSettings](https://msdn.microsoft.com/library/hh161759\(v=office.16\)) objects), but cannot be changed after the endpoint has been created. By default, the contact card, state, services, note, and calendar data presence categories are subscribed to.

In contrast, a view created for polling can configure the list of categories at any time during its lifetime by a call to the [SetPresenceSubscriptionCategoriesForPolling(IEnumerable\<String\>)](https://msdn.microsoft.com/library/hh384657\(v=office.16\)) method.

All views belonging to an endpoint can be refreshed at a single time by calls to the [BeginRefreshRemotePresenceViews(AsyncCallback, Object)](https://msdn.microsoft.com/library/hh365971\(v=office.16\)) and [EndRefreshRemotePresenceViews(IAsyncResult)](https://msdn.microsoft.com/library/hh384430\(v=office.16\)) methods on the endpoint’s **PresenceServices** property. Refreshing all views is resource-intensive, so applications are advised to limit its usage.

Subscriptions to targets connected to a given server can be batched.

## Context data

To subscribe to remote presentities in a presence view, a [RemotePresentitySubscriptionTarget](https://msdn.microsoft.com/library/hh349759\(v=office.16\)) instance must be created for each target. This **RemotePresentitySubscriptionTarget** instance indicates the SIP URI of that target and an optional context data property, [ContextData](https://msdn.microsoft.com/library/hh348487\(v=office.16\)), that indicates whether the presentity is to be informed about this subscription. When the **ContextData** property is non-null, the remote presentity is notified of the subscription. This notification is intended to prompt the remote presentity to assign an appropriate container membership, such as **Workgroup** or **Friends and Family**. For more information, see [LocalOwnerPresence](localownerpresence.md).

In other situations, such as in an *ad hoc* conversation window or looking at presence in an email, the **RemotePresenceView** can be used to obtain presence information based on the current default permissions. In this case the **SubscriptionContext** property should not be set; otherwise unnecessary prompts are sent to the subscription targets.

## Presence query

If a one-time presence query to a remote presentity is desired, creating a view and tearing it down is a suboptimal solution for an application. In addition, the application needs to wait and track whether all presence information has been received.

An alternative is to use the [BeginPresenceQuery(IEnumerable\<String\>, \[\], EventHandler\<RemotePresentitiesNotificationEventArgs\>, AsyncCallback, Object)](https://msdn.microsoft.com/library/hh383136\(v=office.16\)) and [EndPresenceQuery(IAsyncResult)](https://msdn.microsoft.com/library/hh366226\(v=office.16\)) methods on the endpoint’s **PresenceServices** property.

## RemotePresenceView state transitions

The **RemotePresenceView** state transitions for the subscription state of a target are shown in the following illustration. The state values are the members of the [RemotePresentitySubscriptionState](https://msdn.microsoft.com/library/hh382767\(v=office.16\)) enumerated type.

![RemotePresenceView state transitions](images/Dn466022.StateMach_RemotePresence(Office.16).jpg "RemotePresenceView state transitions")

1.  The transition from **Idle** to **Subscribing** occurs when the application calls [StartSubscribingToPresentities(IEnumerable\<RemotePresentitySubscriptionTarget\>)](https://msdn.microsoft.com/library/hh382387\(v=office.16\)) with the [SubscriptionMode](https://msdn.microsoft.com/library/hh349827\(v=office.16\)) property on a [RemotePresenceViewSettings](https://msdn.microsoft.com/library/hh382861\(v=office.16\)) instance set to **Default** or **Persistent**.

2.  The transition from **Subscribing** to **Subscribed** occurs when the subscription to the remote presentity succeeds.

3.  The transition from **Subscribing** to **Terminating** occurs when the subscription to the remote presentity fails.

4.  The transition from **Subscribing** to **WaitingForRetry** occurs for either of the following reasons:
    
      - For a Communicator 2005 presentity, the server requests UCMA 5.0 to try subscribing again at a later time. At the end of this period the Communicator 2005 presentity is converted to an enhanced presence user.
    
      - For a remote presentity that belongs to a cross pool, the server requests UCMA 5.0 to try subscribing to the cross-pool server.

5.  The transition from **WaitingForRetry** to **Subscribing** occurs within 10 seconds, when the attempt to subscribe is made again for the reasons given in step 4.

6.  The transition from **Subscribed** to **WaitingForRetry** occurs for a number of reasons, such as connection failures (for response codes such as 481 Call Leg Unavailable, 480 Temporarily Unavailable, or 504 Server Timeout) or when the server requests UCMA 5.0 to close this subscription and create a new subscription. This action can happen at any time.

7.  The transition from **Subscribed** to **Terminating** occurs when the application calls [StartUnsubscribingToPresentities(IEnumerable\<String\>)](https://msdn.microsoft.com/library/hh349575\(v=office.16\)) or [BeginTerminate(AsyncCallback, Object)](https://msdn.microsoft.com/library/hh349615\(v=office.16\)).

8.  The transition from **Terminating** to **Terminated** occurs when the subscription ends, whether or not it is successful.

9.  The transition from **WaitingForRetry** to **Terminating** occurs when the application is unsubscribing to the target, and the state is **WaitingForRetry**.

10. The transition from **Idle** to **Polling** occurs when the application calls **StartSubscribingToPresentities** and the value of the [SubscriptionMode](https://msdn.microsoft.com/library/hh349410\(v=office.16\)) property is **Polling**.

11. The transition from **Subscribing** to **Polling** can occur when the presentity being subscribed to is an automated service.

12. The transition from **Polling** to **Terminating** can occur when **StartUnsubscribingToPresentities** or **BeginTerminate** is called on the view.


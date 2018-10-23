---
title: Incoming message dispatching
TOCTitle: Incoming message dispatching
ms:assetid: e0f99fce-57b2-4ff5-8ed4-f4c1e6e99968
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466043(v=office.16)
ms:contentKeyID: 65239982
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Incoming message dispatching

**Applies to**: Skype for Business 2015

This topic describes two endpoint properties, **OwnerUri** and **EndpointUri**, and how a Mutual TLS server platform uses them when it dispatches incoming messages to the appropriate endpoint.

## Owner URI

The [LocalEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.localendpoint?view=ucma-api) class has a property that contains the URI of the endpoint’s owner-[OwnerUri](https://msdn.microsoft.com/en-us/library/hh385287\(v=office.16\)). This property is inherited by the [UserEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.userendpoint?view=ucma-api) and [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) classes. Because users can sign on using a variety of devices (desktop and laptop computers, phones, or other devices), it is not unusual for two or more endpoints to have the same owner. In addition, because a single application can be deployed on several computers for better scalability, multiple endpoints can have the same owner URI. The owner URI can be used to target a message to a specific user. Skype for Business Server 2015 forks such messages to all **UserEndpoint** instances associated with that owner URI. The owner URI for a user endpoint matches a user record with that URI in the active directory. The owner URI for an application matches a contact record with that URI in the active directory.

## Endpoint URI for a user endpoint

Every endpoint also has a property that contains the URI of the endpoint itself-[EndpointUri](https://msdn.microsoft.com/en-us/library/hh381014\(v=office.16\)). This property is likewise inherited by the [UserEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.userendpoint?view=ucma-api) and [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) classes. During **UserEndpoint** registration, each user endpoint is dynamically assigned a Globally Routable Unique URI (GRUU) that uniquely identifies the endpoint. A message that targets this GRUU is routed to the specific user endpoint instance.

## Endpoint URI for an application endpoint

An application endpoint is usually associated with two possible GRUU values: application pool GRUU and application instance GRUU. The following table describes these GRUU types.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>GRUU type</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Application pool GRUU</p></td>
<td><p>A GRUU that is associated with the application’s pool FQDN and port.</p></td>
</tr>
<tr class="even">
<td><p>Application instance GRUU</p></td>
<td><p>A GRUU that is associated with the FQDN of the computer that is running an application instance, and the port the application is using.</p></td>
</tr>
</tbody>
</table>


If an application is deployed to leverage DNS-based load balancing or hardware load balancing, it is normally assigned an application pool GRUU. The contact record in Active Directory that contains the owner URI of the application is typically bound to the application pool GRUU for the application. When a message targets an application’s owner URI or application pool GRUU, Skype for Business Server maps the owner URI or application pool GRUU to a pool FQDN and port, and routes the message. The pool FQDN either points to a hardware load balancer or resolves to all available machine FQDNs that correspond to the various application instances. This permits any message to be routed to a particular application instance.

> [!NOTE]
> Skype for Business Server 2015 routes messages to one specific instance of the application irrespective of whether the message uses the owner URI of the application or the pool GRUU.

Every application endpoint instance is also assigned a GRUU that maps to the local FQDN and port of the application instance that owns this endpoint instance. When a message targets this GRUU, Skype for Business Server 2015 maps the GRUU to the FQDN and port for the application, and routes the message to this application instance. This GRUU is usually set in the collaboration platform at the time of construction, but can be set when the platform is auto-provisioned. The GRUU is typically used for multimodal communications for an application endpoint.

## Targeting a message to a user or application

When the target of a message (typically an invitation for IM chat or an audio call) is a user’s owner URI, Skype for Business Server 2015 forks the message to all endpoint instances belonging to that user. An application such as Skype for Business usually notifies the user that the incoming call can be picked up at any of that user’s endpoints. If the user is away from the device or takes no action, instant messaging calls are automatically accepted at the most active endpoint (the endpoint that published the most recent presence state), and audio calls are typically routed to a voice mail system.

Unlike messages that target a user, a message whose target is the owner URI of an application is routed to a single application instance. A message that does not directly target an owner URI of an application can still reach the application by the use routing rules. When Skype for Business Server 2015 does this, it adds a special header (ms-application-aor) that indicates the owner URI being targeted. The collaboration platform can use this header internally to route the message to the endpoint that correctly matches the owner URI.

## Targeting a message to a specific endpoint

When the target of a message is the GRUU of a user endpoint, Skype for Business Server 2015 routes the message to the specific endpoint associated with that GRUU. When the target of a message is the application instance GRUU assigned to an application, the message is routed to the specific application endpoint instance. Targeting a specific endpoint is important when an existing conversation is enhanced with a new modality.

> [!NOTE]
> An application instance can host many **ApplicationEndpoint** instances, each corresponding to a different owner URI. If a message arrives that targets an application instance but does not contain the "ms-application-aor" header that resolves to a specific **ApplicationEndpoint** instance, the platform uses the conversation ID to resolve the message to a specific endpoint. This is done to support multimodal escalation (for example, when an audio/video call is added to a conversation involving an instant messaging call, or vice versa) for a conversation that is hosted in a specific **ApplicationEndpoint** instance.

## Message dispatching by the collaboration platform

Some messages create dialogs. For example, SIP INVITE or SUBSCRIBE messages are normally used to create a dialog between the local endpoint and a remote endpoint or Skype for Business Server 2015. After a dialog is established, messages can be exchanged inside the dialog for the lifetime of the dialog. These messages carry unique dialog identifiers and hence are routed to the component that manages the dialog. Messages that create dialogs are normally routed by the collaboration platform. Sometimes a message that belongs to a dialog is also routed by way of the collaboration platform when the corresponding dialog was already terminated or does not exist at the local endpoint. In addition, there are messages that do not create dialogs but can be exchanged directly between endpoints. Such messages are called page-mode messages.

When a message arrives at the collaboration platform, the information in the message is used to determine which endpoint should process the message. If a matching endpoint is found, the message is delivered to the endpoint to handle it. If there is no matching endpoint, but a default routing endpoint is found, the message is routed to that endpoint. Otherwise, the message is automatically rejected with a 481 response code (if the message belongs to a dialog) or a 500 response code.

Every endpoint is assigned a unique identifier (ms-opaque) in the collaboration platform. When an endpoint registers with Skype for Business Server 2015, this identifier is included in the contact header. The GRUU assigned by Skype for Business Server 2015 for this registration is tied to this unique identifier. When a message targets this registration GRUU, Skype for Business Server 2015 adds this identifier to the request URI of the message. Thus, the collaboration platform can uniquely identify the endpoint to which this message should be dispatched.

When the target of a message is the owner URI of a user or application endpoint, the **To** URI of the message usually carries this URI. This can also be used to match an endpoint for routing. If there are multiple endpoints that match the owner URI, the endpoint ID (EPID) (another identifier of the endpoint that is unique for a given owner URI) or URI parameters, if any, are used to find the best matching endpoint. If the message includes a special header for the owner URI (ms-application-aor), the special header overrides the **To** header for matching purposes.

When the target of a message is the application instance GRUU of an application, the message might not carry an identifier that is specific to the endpoint. If the message contains a conversation ID, it is used to find a unique endpoint that has a conversation matching that ID. If there are two endpoints that match the conversation ID, the message is sent to the endpoint that does not match the originator (**From** URI) of the message. When there is ambiguity, the message is rejected.

The platform supports a special type of application endpoint that can be designated as the default routing endpoint. At most one endpoint can be designated as the default routing endpoint. When a message cannot be matched uniquely to an endpoint, the platform dispatches such messages to the default routing endpoint.


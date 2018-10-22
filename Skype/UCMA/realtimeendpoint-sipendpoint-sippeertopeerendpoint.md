---
title: RealTimeEndpoint, SipEndpoint, SipPeerToPeerEndpoint
TOCTitle: RealTimeEndpoint, SipEndpoint, SipPeerToPeerEndpoint
ms:assetid: 4d8dea46-c93a-4ea9-b800-a578c0eb9fd7
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466055(v=office.16)
ms:contentKeyID: 65239987
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# RealTimeEndpoint, SipEndpoint, SipPeerToPeerEndpoint


**Applies to**: Skype for Business 2015

 

An endpoint is a routable entity in a SIP network. A real-time communications application creates a real-time endpoint to enable a user to communicate in real time with other users, using multiple devices of a variety of types. Each device corresponds to a unique endpoint for the user. For example, a user sending an instant message from a desktop client does so through an endpoint. The same user answering a call on a mobile phone does so through another endpoint. The situation is known as Multiple Points of Presence (MPOP).

## RealTimeEndpoint

In the UCMA 5.0 Signaling layer, an endpoint represents the local context for such real-time services such as establishing and maintaining a signaling session, a publication and subscription session, and page-mode messaging. In UCMA 5.0, such an endpoint is modeled by a [RealTimeEndpoint](https://msdn.microsoft.com/en-us/library/hh366081\(v=office.16\)) object, which is also referred to as a real-time endpoint. A real-time endpoint provides the entry point for all functionality defined in the Signaling layer.

A **RealTimeEndpoint** uses a default transport type for outgoing messages. The default transport type is used unless it is overridden by the transport parameter in the request URI.

Every real-time endpoint is uniquely identified in one of three ways:

  - A URI and a unique endpoint ID for the given URI. A client can assign an endpoint ID value when a real-time endpoint is created. Alternatively, it can leave the platform to specify the endpoint ID. In either case, the endpoint ID value is accessible from the Id property. When a real-time endpoint is a [SipEndpoint](https://msdn.microsoft.com/en-us/library/hh348350\(v=office.16\)), it can be routed to a user even when it has no GRUU. Skype for Business Server 2015 supports routing to a user or routing to a specific endpoint identified by a GRUU.

  - Globally Routable User-Agent URI (GRUU), as defined in the SIP Internet-Draft (draft-ietf-sip-gruu-13). Endpoints identified by GRUU are routable. An endpoint's GRUU value can be created only by the server and becomes available to an application after the endpoint is registered successfully. The application can examine this GRUU value by reading the [Gruu](https://msdn.microsoft.com/en-us/library/hh382641\(v=office.16\)) property, provided that the endpoint remains registered.

  - A SIP instance. For example, Microsoft Live Meeting console clients use a SIP instance to represent themselves and do not register themselves with the server.

To discover and communicate with other users, use one of the following techniques:

  - A mechanism such as a registration-free network or email system that uses [SipPeerToPeerEndpoint](https://msdn.microsoft.com/en-us/library/hh161884\(v=office.16\)) objects.

  - A SIP proxy to keep the presence information updated, that uses registration-based real-time endpoints, represented by [SipEndpoint](https://msdn.microsoft.com/en-us/library/hh348350\(v=office.16\)) objects.

The following sections describe the two kinds of endpoints in more detail.

## SipEndpoint

The [SipEndpoint](https://msdn.microsoft.com/en-us/library/hh348350\(v=office.16\)) class derives from the abstract **RealTimeEndpoint** class and registers with Skype for Business Server 2015. A **SipEndpoint** instance must register with a SIP server before it can communicate with other endpoints of this type. An application can use a **SipEndpoint** instance to enable a user to publish or subscribe to data in subscription sessions or to send or receive invitations using signaling sessions.

The **SipEndpoint** class has a number of constructors that can be used to create a **SipEndpoint** object depending on the specific usage. These overloaded constructors create the endpoint by supplying various parameters. After the endpoint is created, the application can optionally register it with the server. Even though registration is optional, it is highly recommended because some functionality, such as presence subscription, may not work without registration. For example, a **SipEndpoint** cannot receive incoming invitations without registration. After a successful registration, the application can terminate the registration at any time. When the endpoint is no longer needed, it can be disposed.A **SipEndpoint** can be used in conjunction with a [RealTimeServerConnectionManager](https://msdn.microsoft.com/en-us/library/hh383452\(v=office.16\)) instance or a [RealTimeClientConnectionManager](https://msdn.microsoft.com/en-us/library/hh382693\(v=office.16\)) instance.

A **SipEndpoint** can be used for both TCP and TLS connections. A **SipEndpoint** caches the outbound connection used for registration and reuses it for sending subsequent SIP requests. If the connection is disconnected for some reason, a new one is automatically created when the next message is sent out. All messages are routed to a registration server (Skype for Business Server 2015 is the only allowed registrar) or a proxy server.

For user authentication, **SipEndpoint** supports NTLM and Kerberos authentication protocols. The application is responsible for supplying the user credentials for performing the authentication. Typically, user credentials are used when Skype for Business Server 2015 challenges an outgoing SIP request. Skype for Business Server 2015 challenges the first request sent by a user's endpoint and will challenge a SIP request again after eight (8) hours. The application also has the option to indicate that default Windows logon credentials can be used.

A client application might need to enable support the forking of incoming messages when registering the endpoint with Skype for Business Server 2015. This involves calling the **BeginRegister** method on a **SipEndpoint** instance and passing in a specific header, as in the following code example.

```csharp
List<SignalingHeader> headers = new List<SignalingHeader>();
headers.Add(SignalingHeader.MicrosoftSupportedForking);
sipEndpoint.BeginRegister(headers, registerCallback, null);
```

The call to the **EndRegister**, which is not shown in the previous sample, would occur in the *registerCallback* callback method.

## SipPeerToPeerEndpoint

The [SipPeerToPeerEndpoint](https://msdn.microsoft.com/en-us/library/hh161884\(v=office.16\)) class derives from the abstract [RealTimeEndpoint](https://msdn.microsoft.com/en-us/library/hh366081\(v=office.16\)) class. A **SipPeerToPeerEndpoint** can participate in signaling sessions and can send page-mode messages. Subscription sessions are not supported for this type of endpoint.

A **SipPeerToPeerEndpoint** typically routes outbound SIP requests through an outbound SIP proxy server to other **SipPeerToPeerEndpoint** endpoints without registering with a registration server. When a SIP Proxy/Registrar is used, this endpoint type can communicate with a **SipEndpoint**.

The server connection manager manages connections to or from **SipPeerToPeerEndpoint** endpoints. A **SipPeerToPeerEndpoint** can be used in conjunction with a **RealTimeServerConnectionManager** instance, but not with a **RealTimeClientConnectionManager** instance.

As already mentioned, a **SipPeerToPeerEndpoint** can be configured to use an outbound SIP proxy server or SIP public switched telephone network (PSTN) gateway. In such cases, all messages sent from an application-side **SipPeerToPeerEndpoint** instance are delivered to the proxy. The proxy will attempt to deliver the message based on the URI parameter of the message or signaling session target.

This endpoint type does not support user authentication. If the endpoint is used with a proxy server that can challenge requests, the server must be configured to trust the computer hosting this endpoint.Unlike **SipEndpoint**, a **SipPeerToPeerEndpoint** does not cache a connection. When an application attempts to send a page-mode message or out-of-dialog SIP request from a local **SipPeerToPeerEndpoint** to a remote **SipPeerToPeerEndpoint**, a connection is created and used for sending the message. After the message is sent, the connection is disconnected. The disconnected connection is then removed in about one (1) minute unless another message is sent within this time period. However, if a session is established between this endpoint and a remote endpoint, the session will cache the connection during the life time of the session and thus the connection will stay alive. If the connection remains idle for a long time, it can be disconnected as well.

An application can send messages from a **SipPeerToPeerEndpoint** without going through a SIP proxy server, as long as the application is able to provide the FQDN and port of the target for the message.

Two representative uses of **SipPeerToPeerEndpoint** endpoints are in large-scale chat rooms and in custom alert broadcasters. An important difference between the two application types is that the large-scale chat room uses a **SipPeerToPeerEndpoint** to listen on ports for incoming traffic, while a custom alert broadcaster uses a **SipPeerToPeerEndpoint** to send outgoing messages and has no need to listen to incoming traffic.


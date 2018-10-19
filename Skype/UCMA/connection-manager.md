---
title: Connection manager
TOCTitle: Connection manager
ms:assetid: 7d926fff-8d44-49b6-b7da-418948d766e1
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466049(v=office.16)
ms:contentKeyID: 65239985
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Connection manager


_**Applies to:** Skype for Business 2015_

**In this article**  
General features  
Connection pool  
Client connection manager  
Server connection manager  

The primary functionality of a connection manager is to manage incoming and outgoing connections. An instance of a class that inherits from the abstract [RealTimeConnectionManager](https://msdn.microsoft.com/en-us/library/hh382456\(v=office.16\)) class can be used to manage outbound connections. A [RealTimeServerConnectionManager](https://msdn.microsoft.com/en-us/library/hh383452\(v=office.16\)) implementation (the [RealTimeServerTcpConnectionManager](https://msdn.microsoft.com/en-us/library/hh382485\(v=office.16\)) class or the [RealTimeServerTlsConnectionManager](https://msdn.microsoft.com/en-us/library/hh382910\(v=office.16\)) class) must be used to manage incoming connections.

## General features

A connection manager encapsulates the low-level SIP functionalities and provides connection services for real-time endpoints. The services include the following:

  - Making TCP or TLS connections to a specified host on a given network interface (*Host*), using a specific port (*Port*), and using a chosen type of transport (*Transport*). The (*Host*, *Port*, *Transport*) parameters define the destination tuple. Applications can define connection pools to limit the number of outbound TCP or TLS connections with a remote tuple.

  - Dispatching incoming out-of-dialog requests to the intended endpoint.

  - Responding to incoming messages automatically when the message cannot be dispatched to a real-time endpoint. This can happen when no endpoint has been associated with the connection manager. In this case, the message sender receives a response indicating an internal error.

  - Controlling the lifetime of connections. A new incoming connection is disconnected if no valid message arrives on the connection in about one (1) minute. An established (incoming or outgoing) connection will be disconnected if it remains idle for about 15 minutes. However, an application can control the idle time-out for either incoming or outgoing connections by setting the ([DefaultIncomingConnectionTimeout](https://msdn.microsoft.com/en-us/library/hh383907\(v=office.16\)) or [DefaultOutgoingConnectionTimeout](https://msdn.microsoft.com/en-us/library/hh349955\(v=office.16\)) property. The setting can be applied on an individual connection or across all the connections globally.

  - Managing the local certificate. A local certificate is needed when mutual TLS (MTLS) is used.

  - Enabling or disabling listening for incoming connections. This applies only to server connection managers.

  - Maintaining a list of host or domain names trusted for incoming connections. This applies only to a server connection manager using the MTLS transport. Entries from this list are checked to match against the subject name (SN) and subject alternate name (SAN) or, if the subject is missing, the subject alternate name of the certificate supplied by the remote host.

  - Managing enhanced key usage for controlling acceptable categories of certificates. Typically, a certificate falls into a category for client authentication, server authentication, or both.

A Microsoft Unified Communications Managed API 5.0 application uses a server connection manager if it functions as a server or middle-tier application. The application uses a client connection manager if it serves only as a client.

When an application makes a connection, it can explicitly create a connection manager or use a default connection manager. The application makes the choice when it instantiates an endpoint. If no connection manager is explicitly assigned to the endpoint, the default connection manager is used.

## Connection pool

Every endpoint is associated with a single connection manager. The connection manager manages both outgoing and incoming connections for the endpoint. For outgoing connections, a connection manager can be configured to support connection sharing. For this, the connection manager maintains one or more connection pools. Each connection pool contains a fixed, but configurable, number of connections. Connection sharing can be optimized by adjusting the pool sizes. Incoming connections are not pooled.

A connection pool is associated with a destination tuple: (*Host*, *Port*, *Transport*). It identifies the connections to a specified host (*Host*) on a given network interface (*Port*) and using a chosen type of transport (*Transport*). *Host* is a host name that can be an IP address. Destination tuples with different host names correspond to two different connection pools even if the host names are resolved into the same IP address.

A connection pool can maintain zero (0) or more connections. An application can specify the maximum number of connections permitted on a pool. This can be set for an individual pool or for all the connection pools used in the application. When an application attempts to make a connection, the connection manager creates a new connection if the maximum number of connections has not yet been reached. When the pool already contains the maximum number of connections, the connection manager will reuse an existing connection in a round-robin fashion.

The maximum number of connections permitted on a pool is also known as the pool size. In general, a smaller pool size means more connection sharing. A larger pool size means less connection sharing. When the pool size is set to the maximum value supported by the host computer (in .NET Framework, this is **Int32.MaxValue**), connection sharing is effectively turned off and every request amounts to creating a new connection. If the pool size is too small, a pending request might need to wait for a long time. If the pool size is too large, a significant portion of the allocated resource can be underutilized. By choosing an appropriate pool size, the application can optimize connection resources while maintaining optimal performances.

To optimize server-to-server communications, an application can use connection pooling, which places an upper bound on the number of connections that endpoints, page-mode requests, and sessions use to communicate with a remote server. By limiting the number of connections, the application can reuse connections in the connection pool, thereby reducing the overhead of establishing new connections.

## Client connection manager

The client connection manager (the [RealTimeClientConnectionManager](https://msdn.microsoft.com/en-us/library/hh382693\(v=office.16\)) class), implemented from the abstract [RealTimeConnectionManager](https://msdn.microsoft.com/en-us/library/hh382456\(v=office.16\)) class, is the simplest type of connection manager. This type of connection manager does not listen for any incoming connection and is therefore more secure. Typically, an application serving as a client uses the client connection manager because listening for an incoming request is not necessary. Lync or other Lync-like applications are some of the examples.

A client connection manager can make outbound TCP or TLS connections. When using a TLS connection, the client connection manager does not present any certificate to the server, but verifies the server certificate.

A client connection manager can host one or more endpoints. For example, a simple Lync-like client uses a single endpoint. It makes a single outbound connection to a server and reuses that connection for incoming traffic from the server. Typically, a server controls the throughput for such connections to regulate network traffic. If the client sends a request when there are already 20 outstanding requests from that client on the server, the server will fail additional requests. This is known as client throttling. In this case, sharing connections between endpoints can be problematic. As a result, the client connection manager disables connection sharing automatically. The client connection manager can enable client throttling by setting the [IsEndpointThrottled](https://msdn.microsoft.com/en-us/library/hh349573\(v=office.16\)) property on a [SipEndpoint](https://msdn.microsoft.com/en-us/library/hh348350\(v=office.16\)) instance to true.

Only **SipEndpoint** can be used with a client connection manager; a [SipPeerToPeerEndpoint](https://msdn.microsoft.com/en-us/library/hh161884\(v=office.16\)) instance must not be used. When a **SipEndpoint** instance is instantiated, if no client connection manager is passed to the constructor, a default client connection manager is used, and connection sharing is disabled on this default connection manager. If an application passes a client connection manager to the constructor of a **SipEndpoint**, the application must disable sharing on the connection manager unless the server is known not to throttle the connections.

## Server connection manager

A server connection manager, implemented from the abstract [RealTimeServerConnectionManager](https://msdn.microsoft.com/en-us/library/hh383452\(v=office.16\)) class in two related subclasses (described in the next two sections), adds the capability of listening for incoming connections. Applications acting as a server or a middle-tier agent need to listen for incoming requests by calling [StartListening](https://msdn.microsoft.com/en-us/library/hh382009\(v=office.16\)) on the associated connection manager. A server connection manager can host either endpoints that must register with a SIP server (**SipEndpoint**) or peer-to-peer endpoints (**SipPeerToPeerEndpoint**).

Depending on the type of transports used in a connection, the server connection manager is further divided into the TCP server connection manager and the TLS server connection manager.

When multiple **SipEndpoint** instances are present, Microsoft Communications Server manages the dispatching of incoming messages. If the message header contains unique context information, the message is dispatched to the endpoint corresponding to that context. If the message target includes a GRUU, the message is dispatched to the specified endpoint.

### TCP server connection manager

The TCP server connection manager, implemented in the [RealTimeServerTcpConnectionManager](https://msdn.microsoft.com/en-us/library/hh382485\(v=office.16\)) class, can listen for incoming TCP connections. Otherwise, it inherits all the capabilities of the server connection manager. The application can provide an [IPEndPoint](http://msdn2.microsoft.com/en-us/library/fzszfbba) object to control how the listening is enabled. For example, setting [Port](http://msdn2.microsoft.com/en-us/library/5bawssed) to zero (0) indicates listening on any port. Specifying the [Address](http://msdn2.microsoft.com/en-us/library/9kx5cdw6) as **IPAddress.Any** indicates any IP v4 addresses are permissible. IP v6 addresses are not supported.

It is recommended that you *not* use a TCP connection manager, as the less-trusted relationship with the server is not as secure and is more prone to failure, especially given that the owner of a given IP address can frequently change, or be spoofed.

### TLS server connection manager

The TLS server connection manager, implemented in the [RealTimeServerTlsConnectionManager](https://msdn.microsoft.com/en-us/library/hh382910\(v=office.16\)) class, can listen for incoming TLS connections. The application can also choose whether to use MTLS. For TLS connections, only the server side is required to present a certificate. The client side verifies the server certificate. For MTLS connections, certificates must be installed on both the server side and the client side.

The TLS server connection manager allows an application to specify a list of trusted hosts or domains. Incoming connections from such allowed hosts or domains are automatically trusted. Incoming connections from a host or domain that is not among the entries in this list are not allowed.

The application can enumerate, add, remove, and change a trusted domain by reading the [AllowedDomains](https://msdn.microsoft.com/en-us/library/hh384268\(v=office.16\)) property, manipulating the resulting list, and writing the modified list back to the **AllowedDomains** property.

The format of the entries must be a fully qualified domain name (FQDN) of a host ("computer1.contoso.com") or a dot-prefixed domain name (".contoso.com"). The latter means that all of the computers from the specified domain are trusted. The "." prefix of the latter example indicates that the entry is a domain name and not an FQDN.

By default, the **AllowedDomains** list contains "*localhost*" as the only trusted entry. This means that incoming connections from any other hosts are not permitted. If this list is empty, any host or domain is trusted. This is a security threat that an application must avoid. The subject or alternate subject (if the subject is empty) in the remote certificate is used as the search key in the list of allowed hosts and domains. If the list is non-empty, only computers that have matching subject names in this list are allowed. The [AddToAllowedDomains(String)](https://msdn.microsoft.com/en-us/library/hh348280\(v=office.16\)) method is used to add entities to the **AllowedDomains** list.

The allowed host and domain list is used only when mutual TLS is enabled, because the remote certificate is needed for verification. By default, the connection manager automatically adds the remote host of an outgoing connection to the list of allowed domains if mutual TLS is enabled and the list is nonempty.


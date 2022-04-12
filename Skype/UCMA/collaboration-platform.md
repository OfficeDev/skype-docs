---
title: Collaboration platform
TOCTitle: Collaboration platform
ms:assetid: bad16211-f49a-4897-a978-14345082898c
ms:mtpsurl: https://msdn.microsoft.com/library/Dn465954(v=office.16)
ms:contentKeyID: 65239866
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Collaboration platform


**Applies to**: Skype for Business 2015

 

The collaboration platform, an instance of the [CollaborationPlatform](/dotnet/api/microsoft.rtc.collaboration.collaborationplatform) class, provides capabilities for managing connections to and from the platform, guarantees a trust relationship with Skype for Business Server 2015 for server-mode **CollaborationPlatform** instances, and provides a mechanism whereby developers can extend the capabilities of the platform beyond the built-in media types that are supported in Microsoft Unified Communications Managed API 5.0.

Depending on the constructor used, a **CollaborationPlatform** instance is created either as a client platform or as a server platform. A typical use for a client platform is to permit the creation of a number of clients that can be used to simulate users connected to an application.

The endpoints in a UCMA application share various settings, resources, and services, all of which are managed by the **CollaborationPlatform**. These services are made possible by shared resources such as the SIP stack, and include connection management, message routing to individual endpoints, and endpoint auto-provisioning. Shared settings such as the trusted GRUU and user agent enable endpoints to construct messages in a uniform manner throughout the application. Media extensions registered with the **CollaborationPlatform** enable endpoints to handle media types other than those built into UCMA.

## Connection management

The connection manager encapsulates the SIP stack and is used to provide services for endpoints. Each platform is associated with a single connection manager that endpoints can use. The connection manager provides connection sharing, certificate management, and takes care of outgoing and incoming connections for the endpoint. In UCMA 5.0 there are three non-abstract connection manager types: a client connection manager and two server connection managers, with one of the server connection managers using TCP as the transport type, and the other server connection manager using TLS as the transport type. For more information, see [Connection manager](connection-manager.md).

The **CollaborationPlatform** class exposes the connection manager through its [ConnectionManager](https://msdn.microsoft.com/library/hh384799\(v=office.16\)) property. Using this property, an application can enforce connection policy and provide connection throttling, ensure that incoming connections come from trusted servers, and host authentication authority.

The following illustration shows the relationship between a UCMA 5.0 application and Skype for Business Server 2015.

![Connection Manager](images/Dn465954.UCMA-Connections(Office.16).png "Connection Manager")

## Managing trust with Skype for Business Server

When the **CollaborationPlatform** instance is created, a trusted GRUU is assigned to it, which ensures that the platform is trusted at the SIP level (that is, by the SIP stack). To confer this trust relationship, Skype for Business Server 2015 reads the Active Directory trusted service object (**msRTCSIP-TrustedService**) and assigns the associated GRUU to the platform. This GRUU is tied to a particular FQDN and port combination. For more information, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md). Because only one process on a computer can listen on a given port, only one process can use the trusted GRUU—the process that is allowed to listen on that port.

## Extensible communication framework

The **CollaborationPlatform** contains all of the factories that are required for the media types supported in UCMA 5.0. In other words, factories for the [InstantMessagingCall](https://msdn.microsoft.com/library/hh161841\(v=office.16\)), **InstantMessagingProvider**, [AudioVideoCall](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall), and **AudioVideoProvider** classes are already registered with **CollaborationPlatform**. Developers who wish to implement custom media types can do so by creating custom call and media provider classes that inherit from, respectively, the [Call](/dotnet/api/microsoft.rtc.collaboration.call) and [MediaProvider](/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaprovider) abstract base classes. For more information, see the topics in [Extending the UCMA platform](extending-the-ucma-platform.md).

## Auto-provisioning

A **CollaborationPlatform** instance for a trusted server application can be initialized for auto-provisioning. Such a platform instance reads the configuration for the trusted application and its endpoints from Skype for Business Server 2015 Central Management Store and Active Directory, and initializes UCMA with this configuration data. The platform instance also keeps track of certain changes in the configuration data and notifies the application of them, as necessary.


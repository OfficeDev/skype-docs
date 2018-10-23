---
title: Extending the UCMA platform
TOCTitle: Extending the UCMA platform
ms:assetid: b1575336-4ee4-46a0-80ac-62b1731deec9
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466092(v=office.16)
ms:contentKeyID: 65240017
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Extending the UCMA platform


**Applies to**: Skype for Business 2015

Microsoft Unified Communications Managed API 5.0 provides built-in support for three media types: audio, message, and *image* (Fax). For many developers, no other media types are necessary. For developers whose applications require additional media types, the UCMA 5.0 platform can be extended to work with the new, custom media types. Because these new media types are not present in the UCMA 5.0 platform, they are considered platform extensions.

In the UCMA 5.0 platform, the process where a factory class is used to create a variety of objects that share basic functionality, but have additional functionality that is tailored to specific media type constraints, is based on two concepts: call factory and media provider factory. The call factory and media provider factory concepts are represented in UCMA 5.0 by the **CallFactory** and **MediaProviderFactory** abstract base classes, both of which inherit from the **MediaBasedFactory** abstract class. The UCMA 5.0 platform uses subclasses of these types to create instances of the default call types, **AudioVideoCall** and **InstantMessagingCall**, as well as (internal) media provider instances for these call types. **AudioVideoCall** and **InstantMessagingCall** are classes that inherit from **Call**, an abstract class.

Third parties who intend to provide support for additional media types can do so by implementing **Call**, **MediaProvider**, and **MediaFlow** subclasses tailored to the media type they want to support, and then by implementing **CallFactory** and **MediaProviderFactory** subclasses that create instances of the **Call** and **MediaProvider** subclasses that match their new media type.

The following illustration shows the relationships between the abstract classes provided in UCMA 5.0 SDK and the classes that must be implemented by developers who wish to extend the UCMA 5.0 SDK platform to support new media types. The subclasses for the **MediaBasedFactory**, **CallFactory**, and **MediaProviderFactory** abstract classes appear in the second illustration.

**Platform extension architecture**
  
![Platform extension architecture](images/Dn466092.ExtensionArch(Office.16).png "Platform extension architecture")

The topics in this section discuss minimum requirements for implementing subclasses of the abstract classes appearing in the previous illustration.

- [Extending the Call class](extending-the-call-class.md)
- [Extending the MediaProvider class](extending-the-mediaprovider-class.md)
- [MediaProvider and Call architecture](mediaprovider-and-call-architecture.md)
- [Extending the MediaFlow class](extending-the-mediaflow-class.md)
- [Extending the CallFactory and MediaProviderFactory classes](extending-the-callfactory-and-mediaproviderfactory-classes.md)


---
title: Extending the MediaFlow class
TOCTitle: Extending the MediaFlow class
ms:assetid: e6f88071-233c-43b2-b718-59a323bd039c
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466106(v=office.16)
ms:contentKeyID: 65240025
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Extending the MediaFlow class

**Applies to**: Skype for Business 2015

The [MediaFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaflow?view=ucma-api) abstract class represents a generic flow concept for objects that send or receive media. A nonabstract subclass represents the flow of a specific media type. Microsoft Unified Communications Managed API 5.0 provides two **MediaFlow** subclasses: [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api) and [InstantMessagingFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow?view=ucma-api). These classes represent flows whose media type is, respectively, audio/video and message.

A developer who intends to support a different media type must implement a **MediaFlow** subclass that represents that media type.

None of the methods, properties, and events on the abstract **MediaFlow** class are themselves abstract. As a result, a subclass that inherits from the **MediaFlow** class is not required to provide overriding method, property, or event definitions.


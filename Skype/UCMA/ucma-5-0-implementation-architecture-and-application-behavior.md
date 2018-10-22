---
title: UCMA 5.0 implementation architecture and application behavior
TOCTitle: UCMA 5.0 implementation architecture and application behavior
ms:assetid: 583aba66-4638-4158-b8fd-e22619d89dad
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466075(v=office.16)
ms:contentKeyID: 65240009
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA 5.0 implementation architecture and application behavior

**Applies to**: Skype for Business 2015

This section is intended for application developers who use Microsoft Unified Communications Managed API 5.0, and discusses general platform behavior that is expected across various components. The topics in this section discuss guidelines that developers can use to properly create application code and identify code defects when correct behavior is not observed. 

These guidelines have been followed in the development of UCMA 5.0, with the goal of improving the quality of the platform by ensuring a consistent behavior across various components. These guidelines are also intended to make it easier for an application developer to implement the desired functionality, knowing that the platform guarantees a specific set of behaviors.

UCMA 5.0 exposes a variety of concepts in the API, such as platform, endpoint, conversation, call, conference session, MCU session, and media flow. All of these components expose asynchronous methods and events. An application can invoke these methods and register for the events that are raised. UCMA 5.0 uses thread-pool worker threads to make callbacks or to raise events. Typically, queues are used for raising events and for method completion callbacks. 

The topics in this section discuss issues for which the application can expect certain intended behavior. Some issues are discussed in more than one of the following topics, under different categories. This is done explicitly to make them easier to find.

> [!NOTE]
> These behaviors apply only to UCMA 5.0 and may not be honored by the underlying signaling layer. The signaling layer is also accessible to an application that uses UCMA 5.0.

This section includes the following topics:

- [Asynchronous pattern](asynchronous-pattern.md)
- [Threading model](threading-model.md)
- [Queue usage model](queue-usage-model.md)
- [Platform behaviors (methods and properties)](platform-behaviors-methods-and-properties.md)
- [Shutdown and termination](shutdown-and-termination.md)
- [Application behavior](application-behavior.md)

## See also

- [Microsoft.Rtc.Collaboration Namespace](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration?view=ucma-api-5.0)
- [Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml Namespace](https://docs.microsoft.com/dotnet/api/Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml?view=ucma-voice)
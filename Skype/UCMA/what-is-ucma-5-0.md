---
title: What is UCMA 5.0
TOCTitle: What is UCMA 5.0
ms:assetid: ebbfeb40-02ad-4045-bf46-b073406a5c26
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465943(v=office.16)
ms:contentKeyID: 65239785
ms.date: 07/27/2015
mtps_version: v=office.16
---

# What is UCMA 5.0

**Applies to**: Skype for Business 2015

Microsoft Unified Communications Managed API 5.0 enables developers to build applications that leverage the full power of the platform. Applications built on UCMA 5.0 incorporate unified communications concepts such as presence, call, conversation, and conference.

UCMA 4.0 provides a flexible managed-code platform for unified communication and collaboration that allows developers to implement communication-enabled and collaboration-enabled services against Skype for Business Server 2015.

## UCMA 5.0 features

- The platform contains a managed code endpoint API that is based on Session Initiation Protocol (SIP), and is written in Visual C\#.

- The platform is multilayered, with different levels of abstraction, as shown in the following illustration. The components that make up the platform are shown in the blue blocks.
    
  - Unified Communications and Collaboration protocol layer (UCMA 5.0). The classes in this layer are exposed in the [Microsoft.Rtc.Collaboration](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration?view=ucma-api) namespace. The protocols include Enhanced Presence, Centralized Conference Control Protocol (C3P), Contacts and Groups, and Call Control.
        
    The **Microsoft.Speech** namespace can be used to provide speech recognition and speech synthesis capabilities in UCMA 5.0 applications.
        
    Interactive Voice Response (IVR) applications that can optionally use Voice XML can be created from classes contained in the [Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.voicexml?view=ucma-voice) and **Microsoft.Speech.VoiceXml** namespaces (Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml.dll and Microsoft.Speech.VoiceXml.dll, respectively). For more information, see [VoiceXML support in UCMA 5.0](voicexml-support-in-ucma-5-0.md).
    
  - Signaling layer. This layer provides access to the SIP/SIMPLE infrastructure. The classes in this layer are exposed in the [Microsoft.Rtc.Signaling](https://docs.microsoft.com/dotnet/api/microsoft.rtc.signaling?view=ucma-api) namespace.
        
    The Audio stack is used by some classes in UCMA 5.0, but does not expose any public classes.
    
  > [!NOTE]
  > UCMA 5.0 is contained in a single assembly, Microsoft.Rtc.Collaboration.dll.

  ![Platform abstraction layers](images/Dn465943.UCMAOverallArch(Office.16).png)

- The platform is highly scalable.
    
  The platform is able to support thousands of endpoints and concurrent communications and collaborations. The platform is designed for server operating systems (the recommended operating systems are Windows Server 2008 SP2 64-bit or Windows Server 2008 R2), and is multi-threaded.

- The platform provides high availability.
    
  The deployment model permits multiple, load-balanced application instances that allow for server-grade load balancing and fail-over. Dialog resiliency is built in to update the route set in the event of intermediate hop failures.

- The platform is extensible.
    
  New modalities can be added to the existing communication framework, and extension headers and URI parameters can be supplied and consumed through the APIs.



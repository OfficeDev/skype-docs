---
title: Using an AudioVideoFlow
TOCTitle: Using an AudioVideoFlow
ms:assetid: 8d2f16be-724d-4d32-ad34-5ba3e65c80a6
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466032(v=office.16)
ms:contentKeyID: 65239970
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Using an AudioVideoFlow


**Applies to**: Skype for Business 2015

The [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api), [AudioControl](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiocontrol?view=ucma-api), and [AudioChannel](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiochannel?view=ucma-api) classes can be thought of as having the logical relationship shown in the following illustration. 

The [Audio](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow.audio?view=ucma-api) property on an **AudioVideoFlow** instance provides access to the **AudioControl** instance, and the [GetChannels()](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiocontrol.getchannels?view=ucma-api) method on the **AudioControl** instance returns a read-only **IDictionary** that can be used to find an **AudioChannel** instance by its label.

> [!NOTE]
> In Microsoft Unified Communications Managed API 5.0, an **AudioControl** instance must have exactly one [AudioChannel](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiochannel?view=ucma-api) instance.

**Logical AudioVideoFlow structure**
 
![Logical AudioVideoFlow structure](images/Dn466032.AVFlow(Office.16).png "Logical AudioVideoFlow structure")

The preceding illustration also shows several of the properties on the three classes. Each property on an outer class affects not only its own class, but can affect an inner class. For example, the [EncryptionPolicy](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow.encryptionpolicy?view=ucma-api) indicates whether channel encryption is not supported, supported, or required, and therefore affects the value of the **Encrypted** property on the **AudioChannel** instance. The [Encrypted](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.mediachannel.encrypted?view=ucma-api) value can be true if channel encryption is supported or required, but must be false if channel encryption is not supported.

Because the **AudioVideoFlow** class has no public constructors, it is not possible to directly create an instance of this class. Instead, an application must use the **AudioVideoFlow** instance created when an [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api) instance is created (and accessible through the [Flow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall.flow?view=ucma-api) property on the **AudioVideoCall** instance). If an application intends to change the settings on an **AudioVideoFlow** instance, it must create an [AudioVideoFlowTemplate](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflowtemplate?view=ucma-api) instance to do so. For more information, see Using an **AudioVideoFlowTemplate**.


---
description: Learn about the logical relationship between AudioVideoFlow, AudioControl, and AudioChannel classes.
title: Using an AudioVideoFlow
TOCTitle: Using an AudioVideoFlow
ms:assetid: 8d2f16be-724d-4d32-ad34-5ba3e65c80a6
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466032(v=office.16)
ms:contentKeyID: 65239970
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Using an AudioVideoFlow

**Applies to**: Skype for Business 2015

The [AudioVideoFlow](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?), [AudioControl](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiocontrol?), and [AudioChannel](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiochannel?) classes can be thought of as having the logical relationship shown in the following illustration. 

The [Audio](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow.audio?) property on an **AudioVideoFlow** instance provides access to the **AudioControl** instance, and the [GetChannels()](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiocontrol.getchannels?) method on the **AudioControl** instance returns a read-only **IDictionary** that can be used to find an **AudioChannel** instance by its label.

> [!NOTE]
> In Microsoft Unified Communications Managed API 5.0, an **AudioControl** instance must have exactly one [AudioChannel](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiochannel?) instance.

**Logical AudioVideoFlow structure**
 
![Logical AudioVideoFlow structure](images/Dn466032.AVFlow(Office.16).png "Logical AudioVideoFlow structure")

The preceding illustration also shows several of the properties on the three classes. Each property on an outer class affects not only its own class, but can affect an inner class. For example, the [EncryptionPolicy](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow.encryptionpolicy?) indicates whether channel encryption is not supported, supported, or required, and therefore affects the value of the **Encrypted** property on the **AudioChannel** instance. The [Encrypted](/dotnet/api/microsoft.rtc.collaboration.audiovideo.mediachannel.encrypted?) value can be true if channel encryption is supported or required, but must be false if channel encryption is not supported.

Because the **AudioVideoFlow** class has no public constructors, it is not possible to directly create an instance of this class. Instead, an application must use the **AudioVideoFlow** instance created when an [AudioVideoCall](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?) instance is created (and accessible through the [Flow](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall.flow?) property on the **AudioVideoCall** instance). If an application intends to change the settings on an **AudioVideoFlow** instance, it must create an [AudioVideoFlowTemplate](/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflowtemplate?) instance to do so. For more information, see Using an **AudioVideoFlowTemplate**.


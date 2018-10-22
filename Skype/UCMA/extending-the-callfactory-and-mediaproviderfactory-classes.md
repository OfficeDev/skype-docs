---
title: Extending the CallFactory and MediaProviderFactory classes
TOCTitle: Extending the CallFactory and MediaProviderFactory classes
ms:assetid: 5e604df7-b1ed-4dfe-af25-6577f449af5f
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466109(v=office.16)
ms:contentKeyID: 65240026
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Extending the CallFactory and MediaProviderFactory classes


**Applies to**: Skype for Business 2015



After creating subclasses of the [Call](https://msdn.microsoft.com/en-us/library/hh384235\(v=office.16\)), [MediaProvider](https://msdn.microsoft.com/en-us/library/hh383767\(v=office.16\)), and [MediaFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.componentmodel.mediaflow?view=ucma-api) classes, you must create factory classes for the **Call** and **MediaProvider** subclasses. These factory classes are subclasses of the [CallFactory](https://msdn.microsoft.com/en-us/library/hh384820\(v=office.16\)) and [MediaProviderFactory](https://msdn.microsoft.com/en-us/library/hh382428\(v=office.16\)) abstract classes. These factory classes create instances of the **Call** and **MediaProvider** subclasses that you created previously. After the factory classes are created, they must be registered with the platform. If the custom media type is replacing an existing media type, the existing extension must be unregistered first. If the custom media type is not replacing an existing media type, applications should be designed to attempt to remove the media type before adding it.

The skills required for creating the factory classes are more extensive than those required for registering these classes with the platform. The two sections that follow describe how to register and unregister platform extensions, and provide tips on managing media types. These tasks can be allocated to less experienced developers. The last section in this topic describes how to create the factory classes, a task that should be assigned to more experienced developers.

## Registering and unregistering platform extensions

The **RegisterPlatformExtension** method can be used to register custom extensions to the platform to replace existing behavior for media types that are already supported, or to add new media types to the platform. For most application developers, the custom extensions that need to be used are already installed and simply need to be registered so that their applications can use them. Some developers might need to customize the way media is handled, or add new media types to extend the platform. For these developers, understanding how to write an extension is required.

The following methods are available for installing and uninstalling custom extensions.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Method</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh366223(v=office.16)">RegisterPlatformExtension(MediaBasedFactory)</a></p></td>
<td><p>Registers an extension with the platform. Extensions such as call factories that support specific media types can be registered only if there is no existing extension for the media type.</p>
<p>public void RegisterPlatformExtension(MediaBasedFactory extension)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh349063(v=office.16)">UnregisterPlatformExtension(String, String)</a></p></td>
<td><p>Unregisters the extension, and returns the extension that was removed, or null if no extensions were installed. If an extension is returned, it can be saved for later installation.</p>
<p>public MediaBasedFactory UnregisterPlatformExtension(string extensionType, string mediaType)</p></td>
</tr>
</tbody>
</table>


In UCMA 5.0, only platform extensions that are tied to one or more media types are supported. Two types of platform extensions are already installed with the platform and require no further action:

  - InstantMessaging ("message")

  - AudioVideo ("audio", "video", and "image")

Platform extension media types match the media types that can be found on the "m=" lines in the session description that the extension is expected to support. When you register an extension, the platform queries the extension media types that it supports. The platform does some basic checking to ensure there are no conflicts with existing installed extensions, and then registers the extension. When a request of the appropriate media type can be matched to the extension based on the supported media types, the platform queries to the extension on whether the extension can handle the request.

Before registering an extension that supports existing media types, the application should call UnregisterPlatformExtension to clear the old extension that handled that media type, and then register the new extension, using RegisterPlatformExtension. The following example shows how to replace an existing audio extension.

```csharp
ContosoMediaProviderFactory factory = new ContosoMediaProviderFactory();

platform.UnregisterPlatformExtension(factory.ExtensionType, MediaType.Audio);
platform.RegisterPlatformExtension(factory);
```

To unregister a platform extension, specify the media type and the platform extension type. For convenience, the string constants on the **PlatformExtensionType** static class are available for this purpose. If you have an instance of the new extension that you want to install, you can use the **ExtensionType** property on the new instance to remove the old instance.

For UCMA 5.0, only subclasses of the **CallFactory** and **MediaProviderFactory** classes are supported.

## Managing media types

When you configure platform extensions, ensure that there is no overlap between the supported media types. For example, the default audio extension also supports video. If you remove it to add an alternate audio/only extension, you will lose the video support of the default extension in the process. You will need to install a video-only extension if you want to use your custom audio extensions with video.

When you remove extensions with multiple supported media types (for example, audio/video) you need to specify only one of them when you remove the extension because only one extension can be installed for each media type.

## Creating factory classes for custom call and MediaProvider subclasses

The [MediaBasedFactory](https://msdn.microsoft.com/en-us/library/hh383987\(v=office.16\)) abstract class is a common base for all factories (platform extensions) that are associated with a specific media type. The **CallFactory** and **MediaProviderFactory** abstract classes are derived from **MediaBasedFactory**. These classes belong to the Microsoft.Rtc.Collaboration.ComponentModel namespace and can be used to create custom **CallFactory** and **MediaProviderFactory** subclasses. Each of these classes implements the **ExtensionType** property in a way that is appropriate to the type of factory. To create a new **CallFactory** or **MediaProviderFactory**, create a class that inherits from the applicable abstract base class, and provide implementations of the following **MediaBasedFactory** members:

  - **MediaTypes** property

  - **Create** method

The **CallFactory** and **MediaProviderFactory** classes inherit these **MediaBasedFactory** members, but neither class provides implementations. Any **CallFactory** or **MediaProviderFactory** subclass must provide implementations of these two members.

The **MediaTypes** property returns a list of all of the media types that are supported by the new **Call** or **MediaProvider** subclass. This list contains the media types as they would appear in an SDP session description for incoming requests. This is a full list (for example, "audio" or "video") and is expected not to change or to be computed.

A **Create** method defined on an instance of the **CallFactory** subclass is used to create an instance of a new **Call** subclass. Similarly, a **Create** method defined on an instance of the **MediaProviderFactory** subclass is used to create an instance of a new **MediaProvider** subclass. In either case, the particular type of instance that is created depends on the media types and additional information specified when **Create** is called. The **Create** method you implement on a subclass of **CallFactory** or **MediaProviderFactory** has the following declaration:

```csharp
public object Create(IEnumerable<string> mediaTypes, FactoryContext context);
```

The mediaTypes parameter represents the media types supported by the factory base class, and is a subset of the media types for which the factory base class advertises support in its **MediaTypes** property. The context parameter represents information that the factory can use to make decisions about whether to create a subclass instance, and to initialize the object being created. In UCMA 5.0 this parameter contains only the conversation that the created object (a **Call** or **MediaProvider** subclass instance) belongs to.

The **Create** method is called when a request is matched with the extension, and a request should be handled. **Create** returns an object that can handle the request or null if the factory determines that the request cannot be handled, in which case another factory is queried to handle the request. The **Create** method can use any algorithm to decide whether to handle a request. For example, **Create** can return null if the media stack is not initialized or if there is no camera installed.

After the factories and the **Call** and **MediaProvider** subclasses are created, they can be packaged in a DLL and installed on a system for use by other application writers. Application writers can then use the process described in the previous section (Registering and Unregistering Platform Extensions) to install the platform extensions when the application starts up.

## Managing media types

When you configure platform extensions, ensure that there is no overlap between the supported media types. For example, the default audio extension also supports video. If you remove it to add an alternate audio/only extension, you will lose the video support of the default extension in the process. You will need to install a video-only extension if you want to use your custom audio extensions with video.

When you remove extensions with multiple supported media types (for example, audio/video) you need to specify only one of them when you remove the extension because only one extension can be installed for each media type.


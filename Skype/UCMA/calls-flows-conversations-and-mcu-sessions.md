---
title: Calls, flows, conversations, and MCU sessions
TOCTitle: Calls, flows, conversations, and MCU sessions
ms:assetid: 8eac0275-344c-4546-a83e-2104ce8f7c6e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465961(v=office.16)
ms:contentKeyID: 65239884
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Calls, flows, conversations, and MCU sessions


_**Applies to:** Skype for Business 2015_

During a conversation, data is communicated across one of two types of channels that represent the concepts of *call* and *media flow*.

The [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) class is a container for multiparty communication in Microsoft Unified Communications Managed API 5.0. The properties and methods of this class provide access to many of the other building blocks for communication.

## Two-party communication

### Calls

The call represents a signaling session, based on Session Initiation Protocol (SIP), and provides support for a media session, based on Session Description Protocol (SDP), on top of the signaling session. All call control signals are delivered through the call control channel, which is also known as the signaling channel. A conversation can involve multiple call modalities; however, each modality is associated with only one call in a conversation. For example, a local participant can be engaged with a remote participant in a two-party conversation in which they use both audio and instant messaging. In this example, the conversation will maintain a single call control channel for audio and a single call control channel for instant messaging.

### Supported media types

The default media providers in UCMA 5.0 support two media types:

  - Message
    
    The Message media type is used in [InstantMessagingCall](https://msdn.microsoft.com/en-us/library/hh161841\(v=office.16\)) sessions.

  - Audio
    
    The Audio media type is used in [AudioVideoCall](https://msdn.microsoft.com/en-us/library/hh383901\(v=office.16\)) sessions.

The Video media type is not supported in UCMA 5.0.

### Flows

A flow (or media flow) represents the audio or instant messaging (IM) data that participants send to each other during a conversation. A media flow is delivered through a media-specific channel. For example, IM media is delivered through an IM channel, while audio media is delivered through an audio channel.

After a call is established, media flows are created and managed by media providers. UCMA 5.0 natively supports IM and audio/video providers, and also enables applications to supply their own media providers. A media provider can support one or more media types. The media provider exposes its media flow to the application by way of the call, and the application can configure the media flow using flow settings. The media providers themselves are not exposed to the application nor is there a need for an application to interact with them directly. The conversation connects the call with the appropriate provider depending on the media types supported by the call. For example, an IM conversation will connect to the InstantMessagingProvider, which will create and manage an [InstantMessagingFlow](https://msdn.microsoft.com/en-us/library/hh383312\(v=office.16\)). The media provider for a conversation must support all of the media types that are supported by the call.

UCMA 5.0 provides support for a group of devices (recorder, player, tone controller, speech recognition, and speech synthesis) through its audio/video flow. For more information, see [Devices in UCMA](https://msdn.microsoft.com/en-us/library/dd280152\(v=office.16\)).

### Two-party conversation-unimodal

To begin a two-party conversation, an application must establish a call between the local participant and a remote participant. For a unimodal conversation, an [AudioVideoCall](https://msdn.microsoft.com/en-us/library/hh383901\(v=office.16\)) instance or an [InstantMessagingCall](https://msdn.microsoft.com/en-us/library/hh161841\(v=office.16\)) instance performs call control operations. A media flow type that matches the call type, either [AudioVideoFlow](https://msdn.microsoft.com/en-us/library/hh383533\(v=office.16\)) or **InstantMessagingFlow**, performs media control operations. The media flow instance can be configured by the use of the applicable template, either [AudioVideoFlowTemplate](https://msdn.microsoft.com/en-us/library/hh349157\(v=office.16\)) or [InstantMessagingFlowTemplate](https://msdn.microsoft.com/en-us/library/hh384311\(v=office.16\)).

The following illustration depicts a two-party conversation using a single media modality: IM. This and other figures in this topic should be considered high-level representations, and as such, are somewhat simplified. Each conversation participant has a [UserEndpoint](https://msdn.microsoft.com/en-us/library/hh348819\(v=office.16\)) associated with it, although a [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) instance can be associated with more than one endpoint.

Each conversation participant has a **Conversation** instance on his or her computer or device. For each **Conversation** instance [LocalParticipant](https://msdn.microsoft.com/en-us/library/hh350132\(v=office.16\)) represents the user to which this **Conversation** instance belongs. Similarly, the Remote Participant (the actual property on **Conversation** is a collection named [RemoteParticipants](https://msdn.microsoft.com/en-us/library/hh349440\(v=office.16\))) represents the other participant in the conversation. Given that this conversation involves IM, each **Conversation** instance has an **InstantMessagingCall**, which in turn has an **InstantMessagingFlow**. The text of the instant messages flows from one **Conversation** instance to the other through the Skype for Business Server 2015 infrastructure. Signaling information (the path for this is not shown) passes from one **InstantMessagingCall** instance to the other, by way of the Skype for Business Server 2015 infrastructure.

![Two-party conversation with IM](images/Dn465961.Two-party-Conversation-IM(Office.16).png "Two-party conversation with IM")

### Two-party conversation-multimodal

The second illustration depicts a two-party conversation using two media modalities: IM and audio/video. The only difference between this illustration and the previous one is that now each **Conversation** instance also has an **AudioVideoCall**, each of which has an **AudioVideoFlow**. The IM portion of the conversation media flows through the **InstantMessagingFlow** instances, while the audio and video flow through the **AudioVideoFlow** instances. As in the previous illustration, IM data is passed between the **InstantMessagingFlow** instances by way of the Skype for Business Server 2015 infrastructure. Audio data is passed between the **AudioVideoFlow** instances, also by way of the Skype for Business Server 2015 infrastructure. Signaling information passes from one **InstantMessagingCall** instance to the other, by way of the Skype for Business Server 2015 infrastructure.

![Two-party conversation with IM and audio/video](images/Dn465961.Two-party-Conversation-IM+AV(Office.16).png "Two-party conversation with IM and audio/video")

### Two-party conversation with impersonation

The third illustration depicts a two-party conversation using audio, but in this illustration an application (Application A) impersonates a user (User A). Unlike the two previous scenarios, the endpoint owner and the local participant of the associated conversation instance are different. Another difference between this scenario and the two previous ones is that the endpoint for Application A must be an [ApplicationEndpoint](https://msdn.microsoft.com/en-us/library/hh384825\(v=office.16\)), rather than a **UserEndpoint**.

![Two-party conversation with impersonation](images/Dn465968.Two-party-Impersonation-AV(Office.16).png "Two-party conversation with impersonation")

## Multiparty communication

When a conversation involves three or more participants (the local participant and two or more remote participants), the conversation must be escalated to a conference. Depending on the media mode or modes in use, one or more MCUs become involved. Communication between the local and remote participants is mediated by a modality-specific multipoint control unit (MCU). An audio/video MCU mediates audio and video in a conversation, and an IM MCU mediates IM messages in a conversation.

### Unimodal MCU session

The next illustration depicts a unimodal IM conversation that has been escalated to a conference. The significant differences between this conference and the two-party conversations shown earlier:

  - A [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) appears in each conversation instance.

  - An IM MCU Session instance (the actual class name is [InstantMessagingMcuSession](https://msdn.microsoft.com/en-us/library/hh382004\(v=office.16\))) appears in each **ConferenceSession**.

  - The Focus and IM MCU now play roles in the Skype for Business Server 2015 infrastructure.

As the next illustration shows, signaling information passes from a given IM MCU Session to the Focus, and then to the IM MCU Sessions on the conversation instances of the other participants. Signaling information also passes from the Focus to the IM MCU. IM information (identified as media in the illustration) passes from the [InstantMessagingFlow](https://msdn.microsoft.com/en-us/library/hh383312\(v=office.16\)) in one conversation instance to the IM MCU, and then to the **InstantMessagingFlow** of each other participant.

![Three-party conversation with IM](images/Dn465961.Three-party-Conference-IM(Office.16).png "Three-party conversation with IM")

### Multimodal MCU session

The last illustration in this topic depicts a multimodal conference. The most significant differences between this type of conference and the unimodal conference shown in the previous illustration:

  - Each **ConferenceSession** instance now has an AV MCU Session instance (the actual name of this class is [AudioVideoMcuSession](https://msdn.microsoft.com/en-us/library/hh385298\(v=office.16\))). Each media modality requires its own MCU Session to handle call control.

  - Each **Conversation** instance now also has an **AudioVideoCall** instance, with its associated **AudioVideoFlow** instance.

  - An additional MCU is shown in the Skype for Business Server 2015 infrastructure.

The channels for signaling and media are similar to, but slightly more complex than those for a unimodal conference. In a given conversation instance, the MCU Sessions in **ConferenceSession** send signaling information to the Focus and to their corresponding MCUs, and the Focus passes this information on to the MCU Sessions in the other conference participants. IM and audio/video information travels from an **InstantMessagingFlow** or **AudioVideoFlow** on one **Conversation** instance to the appropriate MCU. From there, this information passes to the **InstantMessagingFlow** or **AudioVideoFlow** on the other **Conversation** instances.

![Three-party conversation with IM and audio/video](images/Dn465961.Three-party-Conference-IM+AV(Office.16).png "Three-party conversation with IM and audio/video")


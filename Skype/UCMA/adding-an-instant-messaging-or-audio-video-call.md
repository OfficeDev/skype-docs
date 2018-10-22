---
title: Adding an instant messaging or audio/video call
TOCTitle: Adding an instant messaging or audio/video call
ms:assetid: d6107a1b-5f2a-40c5-8bbe-df2214acd04d
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466018(v=office.16)
ms:contentKeyID: 65239929
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Adding an instant messaging or audio/video call


**Applies to**: Skype for Business 2015

The steps required for adding an instant message call or audio-video call are similar, but the Skype for Business Server 2015 and components that are involved are different. This topic describes the actions that must be taken for each type of call, and describes the differences at the component level of each call type.


### Adding an instant messaging call

After a conversation is created, you can add instant messaging (IM) to the conversation by using [InstantMessagingCall(Conversation)](https://msdn.microsoft.com/en-us/library/hh348307\(v=office.16\)) to create an IM call, passing the conversation in the constructor.

The remote participant for the call is inferred from the conversation type. For a two-party conversation, the remote participant is the remote client. For a conference conversation, the remote participants are the other members of the conference. Information about these participants comes from the conversation roster.

The associated conversation monitors and manages the call, relays state information (a conversation is the agglomeration of the associated call states), and provides a handle to terminate the associated calls.

The following code demonstrates how to add an IM call to a conversation.

```csharp
InstantMessagingCall imCall = new InstantMessagingCall(conversation);
```

### Adding an audio/video call

After a conversation is created, you can add audio to the conversation by creating an audio/video call.

As with adding an instant messaging call, the remote participant for the call is inferred from the conversation type. For a two-party conversation, the remote participant is the remote client. For a conference conversation, the remote participants are the other members of the conference. Information about these participants comes from the conference roster. The default media type is used to find a matching provider when the call is established. Incoming calls with no Session Description Protocol (SDP) INVITE message (for example, incoming calls routed through a PBX) are assumed to be audio calls. This assumption is made because the most common scenario for an SDP-less INVITE that involves Gateway interoperability or back-to-backing is the one in which voice is the core service that is provided.

The following code demonstrates how to add audio to a conversation.

```csharp
AudioVideoCall avCall = new AudioVideoCall(conversation);
```


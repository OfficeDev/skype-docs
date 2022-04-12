---
description: Learn how to create and join a conference in Skype for Business 2015.
title: Joining a conference
TOCTitle: Joining a conference
ms:assetid: ff8f9269-c9b8-4030-9ef3-081f7ae79ba5
ms:mtpsurl: https://msdn.microsoft.com/library/Dn465994(v=office.16)
ms:contentKeyID: 65239941
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Joining a conference


**Applies to**: Skype for Business 2015

A conversation can be created initially as a conference, or an existing two-party conversation can be escalated to a conference. In both scenarios, the application must join the conference before inviting new participants. An application can access the conference session from the conversation by means of the conversation’s [ConferenceSession](https://msdn.microsoft.com/library/hh381097\(v=office.16\)) property.

## Example: joining a conference

The following code demonstrates creating a conference session and then joining the conference.

```csharp
ConferenceSession conferenceSession = conversation.ConferenceSession;
ConferenceJoinOptions cjo = new ConferenceJoinOptions();
cjo.JoinAsTrustedApplication = false;
conferenceSession.BeginJoin(cjo, Conference_JoinCompleted, conferenceSession);
```

The [BeginJoin()](https://msdn.microsoft.com/library/hh349641\(v=office.16\)) method also has an overload that can be used to join a scheduled conference.

If a participant joins a conference, and the [Role](https://msdn.microsoft.com/library/hh385044\(v=office.16\)) property on the [ConferenceParticipantEndpointProperties](https://msdn.microsoft.com/library/hh384773\(v=office.16\)) object is set to Leader, the participant is considered to be the conference Leader.

An application cannot use the impersonation API without being trusted by the server, as provisioned as a GRUU in Active Directory. When impersonated, an application can join any conference as if it were that user.

## Joining a conference as a trusted user or as an ordinary user

Classes and methods in UCMA 5.0 enable an application to join a conference as a trusted user or as an ordinary user.

### Trusted join

To join a conference as a trusted user, an application should set the [JoinMode](https://msdn.microsoft.com/library/hh384536\(v=office.16\)) property on a [ConferenceJoinOptions](https://msdn.microsoft.com/library/hh385064\(v=office.16\)) instance to **TrustedParticipant**, a value of the [JoinMode](https://msdn.microsoft.com/library/hh381559\(v=office.16\)) enumeration. The conversation must belong to an [ApplicationEndpoint](/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?) instance that is provisioned with a trusted service GRUU.

### Default join

To join a conference as an ordinary user, an application should set the **JoinMode** property to **Default**, another value in the **JoinMode** enumeration. The application might be placed in the lobby, depending on the current conference access level. An application can also specify the maximum amount of time it is willing to wait for in the lobby using the [LobbyTimeout](https://msdn.microsoft.com/library/hh349095\(v=office.16\)) property on a **ConferenceJoinOptions** instance. The default timeout value is 15 minutes.


---
title: Conference scheduling and management
TOCTitle: Conference scheduling and management
ms:assetid: 288ecb11-01d8-42af-9de0-24c98bb70216
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465962(v=office.16)
ms:contentKeyID: 65239880
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Conference scheduling and management


**Applies to**: Skype for Business 2015



An application can use the members of the [ConferenceServices](https://msdn.microsoft.com/en-us/library/hh348907\(v=office.16\)) class to schedule, modify, or cancel a conference. The **ConferenceServices** property on an endpoint (either [UserEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.userendpoint?view=ucma-api) or [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api)) is a reference to a **ConferenceServices** object.

## Scheduling a conference

The organizer of a conference can specify who will be allowed to participate in the conference. When the conference is scheduled, the organizer can choose from the following admission policies for the conference:

  - **SameEnterprise**  
    Participants from the same company are admitted into the conference.

  - **Locked**  
    Only the conference organizer and persons designated by the organizer as leaders are admitted into the conference.

  - **Invited**  
    Only invited participants from the same company are admitted into the conference.

  - **Everyone**  
    The conference is open to everyone.

The [ConferenceAccessLevel](https://msdn.microsoft.com/en-us/library/hh385275\(v=office.16\)) enumeration contains the preceding admission policy values.

A conference organizer can also specify that public switched telephone network (PSTN) users can be included as participants. To permit PSTN users to join the conference, the following must be true:

1.  The conference admission policy must be **Anonymous**.

2.  Phone access must be enabled.

3.  A passcode must be set for the conference. Even if participants are not required to enter a passcode, a passcode must be set for the conference. For more information, see the following section, PSTN Participants.

The conference organizer can also perform other tasks using members of the **ConferenceServices** class, such as configuring the expiry time of the conference (the time when the conference will be deleted), creating the list of participants, designating which participants will be leaders, configuring a property that determines whether a passcode is required, and adding the MCUs that the conference will use (by default a conference has two MCUs, one for instant messages, and another for audio/video media).

After the conference has been scheduled, a conference organizer can retrieve a summarized list of all conferences he or she has scheduled, modify the conference information, or cancel the conference.

## PSTN participants

A PSTN user can join the conference provided that the conference organizer has scheduled the conference with the **Anonymous** admission policy, and has granted phone access. To join the conference, the PSTN user dials the phone number supplied by the conference organizer, and when prompted, enters the telephone conference ID. The PSTN user may or may not be asked for a passcode for the conference, depending on whether a passcode is required. After successfully entering the required information, the PSTN user joins the conference.

## Joining a conference

An application can join an *ad hoc* conference using the [BeginJoin(ConferenceJoinOptions, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh348502\(v=office.16\)) method. Microsoft Unified Communications Managed API 5.0 schedules a conference that is valid for eight hours and adds an MCU for each **ConferenceMcuSessionFactory** registered in the platform.

Before joining the conference, it is recommended that the application register for the roster events on the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) instance and the other [McuSession](https://msdn.microsoft.com/en-us/library/hh384975\(v=office.16\)) instances the application is interested in.

```csharp
// It is sufficient for most applications to monitor the roster on the conversation level.

// Monitor new participants joining the conference.
conversation.RemoteParticipantAttendanceChanged += Conversation_RemoteAttendanceChanged;

// Monitor main changes to participant properties
// (including local participant) such as active media types and conferencing role changes.
conversation.ParticipantPropertiesChanged += Conversation_ParticipantPropertiesChanged;

// Register for ConferenceSession state changes.
conversation.ConferenceSession.StateChanged += ConferenceSession_StateChanged;

// Register for InstantMessagingMcuSession state changes.
conversation.ConferenceSession.InstantMessagingMcuSession.StateChanged += InstantMessagingMcuSession_StateChanged;

// Applications that require detailed monitoring of the roster can subscribe to ConferenceSession and McuSession events.

// Monitor the ConferenceSession roster.
conversation.ConferenceSession.ParticipantEndpointAttendanceChanged += ConferenceSession_ParticipantEndpointAttendanceChanged;

// Monitor Instant Messaging MCU roster.
conversation.ConferenceSession.InstantMessagingMcuSession.ParticipantEndpointAttendanceChanged += InstantMessagingMcuSession_ParticipantEndpointAttendanceChanged;

conversation.ConferenceSession.InstantMessagingMcuSession.ParticipantEndpointPropertiesChanged += InstantMessagingMcuSession_ParticipantEndpointPropertiesChanged;

ConferenceJoinOptions cjo = new ConferenceJoinOptions();
// The new attendee is not a trusted application.
cjo.JoinAsTrustedApplication = false;
// Join the conversation to the conference.
conversation.ConferenceSession.BeginJoin(cjo, JoinCallback, state);
```

An application can also join a previously scheduled conference if the conference URI is known. The last line in the previous code example can be replaced with the following statement.

```csharp
conversation.ConferenceSession.BeginJoin(conferenceURI, cjo, JoinCallback, state);
```

An application can also join as a trusted application by setting the [JoinMode](https://msdn.microsoft.com/en-us/library/hh384536\(v=office.16\)) property to TrustedParticipant, a value of the [JoinMode](https://msdn.microsoft.com/en-us/library/hh381559\(v=office.16\)) enumeration. Trusted applications automatically join conferences as leaders and are allowed elevated privileges. Trusted applications are marked as hidden participants so that ordinary clients (such as Skype for Business 2015 and Live Meeting) do not expose their details in the conference roster to the user.

An example of a trusted application is a bot that archives instant messages sent in a conference. When the bot joins a conference as a trusted application, other clients can detect that it should be hidden from the roster.

The endpoint for an application must support elevated privileges so that it can join as a trusted application. In other words, the endpoint must be an application endpoint and the underlying collaboration platform must be initialized with a trusted GRUU.

When trusted applications join a conference, the [ConversationParticipant](https://msdn.microsoft.com/en-us/library/hh366199\(v=office.16\)) object representing the application has its [RosterVisibility](https://msdn.microsoft.com/en-us/library/hh383991\(v=office.16\)) property set to **ConferencingRosterVisibility**.**Hidden**. UCMA 5.0 guarantees that upon successful completion of a join operation, the **ConferenceSession** state is set to **Connected**. However, the state of the child MCU session (typically, an [InstantMessagingMcuSession](https://msdn.microsoft.com/en-us/library/hh382004\(v=office.16\)) or [AudioVideoMcuSession](https://msdn.microsoft.com/en-us/library/hh385298\(v=office.16\)) instance) might be set to **Active** after the join operation completes. The first participant joining the conference signals the Focus to activate the MCUs scheduled for this conference. Skype for Business Server 2015 does not guarantee that all MCUs are activated before the participant completes the join process. In fact, some MCUs might not be activated at all if they are disabled by the administrator or if an error is encountered.

## Conference commands and events

After a successful join operation, the application is connected only to the Focus and there are no active media flows with the MCUs. For more information, see "Adding a Call to an MCU" later in this topic.

### Role of the Focus in Centralized Conferencing Control Protocol (C3P)

The Centralized Conferencing Control Protocol (C3P or CCCP) defines the Focus as the central hub for roster notifications and conference commands. All MCU notifications are sent to the Focus first for aggregation and are then sent to the respective conference participants. In addition, any conference command is first sent to the Focus which then dispatches it to the appropriate MCU. Command responses are proxied back to the conference participant through the Focus as well. However, all media flows occur directly with the MCUs for efficiency.

### ConferenceSession commands

After the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) state becomes **Connected**, the application can issue conference commands through the **ConferenceSession**. The **ConferenceSession** sends these commands to the Focus, which carries them out.

These commands generally impact the state of the entire conference (Focus and MCUs). Two examples are ejecting all endpoints of a specific participant from the conference, and locking the conference to prevent additional users from joining.

```csharp
conversation.ConferenceSession.BeginEject(conversation.RemoteParticipants[0], EjectCallback, state);
```

```csharp
conversation.ConferenceSession.BeginLockConference(LockConferenceCallback, state);
```

### McuSession commands

Conference commands not intended for the Focus are performed by the MCUs. However, in accordance with C3P, all commands directed to the MCU are sent first to the Focus, which then proxies these commands to the MCUs. The MCUs send responses to the Focus, which then proxies the responses back to the application. This means an application can send MCU commands without having an active media flow with the MCU.

Applications can request most MCU operations without the need for expensive media resources. For example, a trusted application can request the AV MCU to dial out to a PSTN user without having an active media flow with the AV MCU.

As mentioned in the previous section, an MCU can be activated after the application joins the conference. If the application invokes an [McuSession](https://msdn.microsoft.com/en-us/library/hh384975\(v=office.16\)) method before the MCU has been activated, UCMA 5.0 queues the command and waits for up to 30 seconds for the MCU to become active and execute the command. If the MCU does not become active within this period, the command is likely to fail. For more information, see "McuSession Operations," later in this topic.

### Conference events

Any changes to the conference state, such as locking a conference, are communicated by means of specific events on the **ConferenceSession** class or on **McuSession** subclasses.

The [PropertiesChanged](https://msdn.microsoft.com/en-us/library/hh381027\(v=office.16\)) event notifies the application of changes to the conference properties. A similar event is available for the **AudioVideoMcuSession** state to notify the application of changes to audio and video capabilities of the conference MCU.

Applications can subscribe to the [RemoteParticipantAttendanceChanged](https://msdn.microsoft.com/en-us/library/hh381630\(v=office.16\)) event, to be notified of new participants who join or leave the conference. Applications can also subscribe to the [ParticipantPropertiesChanged](https://msdn.microsoft.com/en-us/library/hh365662\(v=office.16\)) event, to be notified of changes to the participant properties (such as conferencing role and active media types). Participant active media types indicate the MCUs that the participant is connected to. For example, if Alice has only "message" as an active media type, then she is connected to the IM MCU. A participant who has no active media types is connected only to the Focus.

Applications that require more roster details can subscribe to the [ParticipantEndpointAttendanceChanged](https://msdn.microsoft.com/en-us/library/hh383640\(v=office.16\)) and [ParticipantEndpointPropertiesChanged](https://msdn.microsoft.com/en-us/library/hh381330\(v=office.16\)) events on the **ConferenceSession** instance and the **McuSession** subclass instance.

The **ConferenceSession**.**ParticipantEndpointAttendanceChanged** event notifies the application of endpoints joining and leaving the Focus. A like-named event is exposed on **McuSession** subclasses to notify an application of endpoints joining and leaving the MCU. An application can detect changes to the properties of the endpoints by the **ParticipantEndpointPropertiesChanged** event.

As with conference commands, an application receives MCU-related events even if there is no active media flow with that MCU. This allows applications to monitor the roster before the media is established. For this reason, it is important for the application to register for the applicable **McuSession** subclass events before the **ConferenceSession**.**BeginJoin** method is called.

## Adding a call to an MCU

After the **ConferenceSession** is connected, the application can add an IM call with the IM MCU to exchange instant messages with other participants. The same applies to adding an audio/video call with the AV MCU.

```csharp
InstantMessagingCall imcall = new InstantMessagingCall(conversation);
imcall.BeginEstablish(EstablishCallback, state);
```

```csharp
AudioVideoCall avcall = new AudioVideoCall(conversation);
avcall.BeginEstablish(EstablishCallback, state);
```

As previously mentioned in this topic, in the Conference Commands and Events section, an MCU can become active after the **ConferenceSession** join operation has completed. If a call was established to an MCU that has not yet become active, UCMA 5.0 queues the call request and executes it within 30 seconds if the MCU becomes active. If the MCU does not become active within 30 seconds, so that the call operation fails, it is the application’s responsibility to try again, or monitor the state of the **McuSession** subclass and retry after it becomes active.

## McuSession operations

During a conference it is possible to request an MCU to dial out to another participant. The MCU initiates a call to the required destination and reports the results to the application.

An application might execute the following sample code for a situation in which two participants are in an audio conference discussing a problem and find that they need the help of a third person, Alice. One participant requests the AV MCU to dial out to the Alice’s cell phone so that she can become a conference participant.

```csharp
McuDialOutOptions mcuDialOutOptions = new McuDialOutOptions();
mcuDialOutOptions.ParticipantUri = "sip:alice@contoso.com";
mcuDialOutOptions.ParticipantDisplayName = "Alice";
mcuDialOutOptions.PreferredLanguage = CultureInfo.GetCultureInfo("en-us");

conversation.ConferenceSession.AudioVideoMcuSession.BeginDialOut("tel:+14255551234", mcuDialOutOptions, dialOutCallback, state);
```

The preceding example demonstrates how an application can add a remote participant to the AV MCU. If no participant information is provided (**ParticipantUri** and **ParticipantDisplayName**), UCMA 5.0 uses the identity of the added remote participant.


---
title: Conference operations
TOCTitle: Conference operations
ms:assetid: 4c48fb22-4f9a-4784-894a-cdd7c12ff50e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465991(v=office.16)
ms:contentKeyID: 65239939
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Conference operations


_**Applies to:** Skype for Business 2015_

**In this article**  
Mute-all mode  
Conference termination countdown  
Entry and exit announcements  
Conference recording  
Scheduling templates  

This topic describes the settings that can be modified on a conference, either at the time the conference is scheduled, or during an active conference.

## Mute-all mode

The [AudioVideoMcuSession](https://msdn.microsoft.com/en-us/library/hh385298\(v=office.16\)) has the capability to enable and disable the mute-all mode. The mute-all mode mutes all participants except for the leader who requests the mute. Leaders can always unmute themselves even though they did not issue the command. Depending on the options in use, attendees might be able to unmute themselves.

To mute all participants, call the [BeginEnableMuteAllMode](https://msdn.microsoft.com/en-us/library/hh384702\(v=office.16\)) method on the **AudioVideoMcuSession** instance. By default, attendees are not allowed to unmute themselves. The [EnableMuteAllModeOptions](https://msdn.microsoft.com/en-us/library/hh382921\(v=office.16\)) parameter can be specified with its [SelfUnmuteAssignment](https://msdn.microsoft.com/en-us/library/hh382298\(v=office.16\)) property set to **Everyone**, which allows all attendees to unmute themselves.

## Conference termination countdown

Microsoft Unified Communications Managed API 5.0 supports notifications to participants in the conference when a conference termination starts or stops. This allows participants to be aware that their conversation might soon be terminated automatically. This normally happens when a conference has finished and the only remaining participants are those who have called in anonymously through the voice gateway for the purpose of freeing up server resources. The countdown can be stopped by an enterprise user rejoining the conference.

Termination countdown information can be monitored by registering for the [TerminationCountdownStatusChanged](https://msdn.microsoft.com/en-us/library/hh384066\(v=office.16\)) event on the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) class. The event indicates whether the countdown has been started or stopped, and if started, approximately how much time, in seconds, remains until the countdown expires. Actual termination will occur after that time and depends on server policies, so can vary from the time reported. If the termination is to happen in the past, the time will be reported as 0 seconds.

It is possible that additional anonymous users may join a conference when the countdown is active, or the interval has expired. In such cases, these anonymous users will receive the notification upon joining the conference.

## Entry and exit announcements

Announcements that give indications when people join and leave are available for participants listening to audio. For most applications, there is a user interface that can be used to track participants joining or leaving. In the case of a participant calling in through a voice gateway, there might be no indication other than the audio notifications.

Entry and exit announcements can be enabled or disabled using the [BeginModifyAttendanceAnnouncements](https://msdn.microsoft.com/en-us/library/hh366272\(v=office.16\)) method on the **AudioVideoMcuSession** class.

## Conference recording

### Recording policy

The [RecordingPolicy](https://msdn.microsoft.com/en-us/library/hh383156\(v=office.16\)) property on the [ConferenceSession](https://msdn.microsoft.com/en-us/library/hh349315\(v=office.16\)) class reports the currently active policy that applications should use to decide whether recording should be allowed. This policy is not enforced by the server or the platform, so each application must check this property before starting to record.

The values of this property indicate which kinds of user access are allowed to record: **SameEnterprise**, **Everyone**, or **Unauthorized** (no recording is allowed).

### Recording notifications

Applications can determine whether other participants are actively recording by checking the [IsRecording](https://msdn.microsoft.com/en-us/library/hh383146\(v=office.16\)) property on the [ConversationParticipant](https://msdn.microsoft.com/en-us/library/hh366199\(v=office.16\)) class. Adding information about the client recording activity is not enforced by the server, and is provided voluntarily by applications joining the conference. For this reason, applications checking the recording properties should be cautious in asserting that the conference is not being recorded.

## Scheduling templates

When a conference is being scheduled, a [SchedulingTemplate](https://msdn.microsoft.com/en-us/library/hh348859\(v=office.16\)) instance can be used to specify how the conference is to be scheduled. There are two kinds of scheduling templates:

  - Organizer-supplied
    
    This type, the default, should be used by most applications.

  - Administrator-supplied
    
    This type is intended to be used by advanced applications.

Using the organizer-supplied template, an application can specify most of the aspects of the conference being scheduled. The application can schedule multiple conferences up to the per-organizer limit configured at the server.

The administrator-supplied template is for changing the properties of a single, always-present meeting that each user account has for its use. This special meeting is usually modified only by the Outlook plug-in that is created for this purpose, and has a restricted subset of the settings that can be changed. For this reason, applications should normally not make scheduling changes using the administrator-supplied template.


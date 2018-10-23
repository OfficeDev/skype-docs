---
title: 'Pre-meeting: conference scheduling'
TOCTitle: 'Pre-meeting: conference scheduling'
ms:assetid: 8a7c5693-48d0-4c22-a4b7-e7c1e1719573
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465923(v=office.16)
ms:contentKeyID: 65239791
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Pre-meeting: conference scheduling

**Applies to**: Skype for Business 2015
 
The features described in this section apply to a conference before it becomes an active conference; that is, before the conference session comes into existence.

## Conferencing capabilities

As in previous UCMA versions, conferencing capabilities include a number of very important concepts for developers. By creating applications that take advantage of these capabilities, users can fine-tune application code to take into account deployment specifics as they relate to conferencing.

Notable conferencing capabilities include: the list of Multipoint Control Units (MCUs) in service in the current installation, the Phone Access capability (which determines whether phone users can dial-into a conference in the current installation), and the supported and recommended values for fields of the Scheduling Template.

An application developer can periodically query these capabilities at run time to adjust the behavior of the application before scheduling new conferences.

## Scheduling template

To enable organizers to not only schedule online meetings (that is, conferences), and to satisfy their most common needs with "one button click", but to also give them the flexibility to configure conferences for special purposes (for example, a large conference with one presenter or a confidential meeting with few invitees), Microsoft Unified Communications Managed API 5.0 introduces the concept of the Scheduling Template.

The Scheduling Template represents the list of configurable options (Conference Information) that is available to the conference organizer. The organizer can decide to use a default Administrator-supplied scheduling template (one that is filled in with the recommended values described in the previous section) or to create her own scheduling template by providing specific Conference Schedule.

The configurable scheduling options include the following:

- A description of the conference

- General information about the conference (which can be read by an organizer or by participants on joining the conference)

- The list of invitees and their respective roles (Attendee or Leader)

- The Access Level of the conference

- The modalities that can be used during the conference

- Whether phone users (people calling through a SIP PSTN gateway or IP-PBX) can dial in to the conference

## Pre-meeting conference access level and conference lobby

In UCMA 5.0, the conference access level specifies the people who have access to a conference based on their authentication method and source network. The following table summarizes the values of the [ConferenceAccessLevel](https://msdn.microsoft.com/en-us/library/hh385275\(v=office.16\)) enumeration.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Conference Access Level</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><strong>Everyone</strong></p></td>
<td><p>Everyone including people outside my company (anonymous and federated users) can join the Conference.</p></td>
</tr>
<tr class="even">
<td><p><strong>SameEnterprise</strong></p></td>
<td><p>People from the same company as the organizer’s one can join the conference. Anonymous and Federated Users join the conference lobby.</p></td>
</tr>
<tr class="odd">
<td><p><strong>Invited</strong></p></td>
<td><p>Only people invited by the organizer and from the same company as the organizer’s one can join the conference. Other people join the conference lobby.</p></td>
</tr>
<tr class="even">
<td><p><strong>Locked</strong></p></td>
<td><p>Only the organizer can join the conference. Other people join the conference lobby.</p></td>
</tr>
</tbody>
</table>


Lync Server 2010 introduced the concept of the *conference lobby*. This mechanism ensures that a participant who is not authorized to join a conference is instead placed in a waiting area before being admitted to or denied entrance into the conference itself. The presenters of a conference are notified of the presence of lobby participants, and the presenters can admit or reject these lobby participants. A participant who is placed in the lobby is effectively put on hold and notified to wait until he or she is admitted. If no presenters take action, the lobby participant will be unable to join the conference, and will be ejected after a time-out period elapses.

Conference scheduling with the **Locked** access level is tightly coupled with the conference lobby concept. Scheduling a **Locked** conference implies that only its organizer can join and activate the Conference. All other participants will be placed in the conference lobby. This can be a practical means for organizing a conference with multiple customers or partners who should not be talking to one another until the conference organizer joins the conference.

A conference with an access level of **Everyone** can be thought of as a conference in which the lobby is opened.

## Phone access and lobby bypass

Audio conferencing providers have traditionally given easy access to phone users to join audio conferences in the enterprise and do not authenticate participants. To accommodate the same flexibility for online meetings while protecting the confidentiality of content shared during meetings, conference Lobby Bypass for phone users was introduced in Lync Server 2010 (that is, PSTN callers who participate only in the audio portion of a conference but where other modalities such as data collaboration or application sharing can be used). When the conference is being scheduled, the organizer can choose whether to admit phone users to the conference by default by enabling or disabling Lobby Bypass.

## Entry-exit announcement

At the time the conference is scheduled, an organizer can decide whether an Entry-Exit announcement should be turned on to alert participants about who is joining or leaving the conference. If a meeting has a large number of participants, it might be advisable to turn off the Entry-Exit announcement to avoid too many interruptions.

## Automatic leader role assignment

When the conference is being scheduled, an organizer can decide who will automatically be promoted to leader on entry to the conference: leaders only, people from the same enterprise, or everyone.

Unlike Attendees, leaders of a Skype for Business Server 2015 conference can share their desktops and applications, present files, and manage conference options, such as admitting participants into the meeting from the Lobby.

## Modality extensibility

Although the primary modalities of UCMA 5.0 are IM and audio, an organizer can supply information when the conference is being scheduled about additional Multipoint Control Units (MCUs) for other modalities such as data collaboration or application sharing. In such cases, the organizer supplies C3P protocol extensions to the UCMA 5.0 Conference Schedule Information.

> [!NOTE]
> Extending modalities is an advanced scenario. For more information, see [Extending the UCMA platform](extending-the-ucma-platform.md).

## Conference web URL

Starting with Microsoft Office Communications Server 2007 R2, a conference is uniquely identified by its conference ID. The conference entry points include a SIP conference URI and a list of phone numbers (Phone Information) coupled with a PSTN conference ID (a numerical sequence that can easily be entered using Dual Tone Multi Frequency (DTMF) tones on a touch-tone phone).

In addition to the previously listed conference entry points, a conference in UCMA 5.0 is assigned a Web URL such as `https://meet.contoso.com/johndoe/TO765HML` that can be shared with new conference invitees before, during, or after a meeting.

Clicking this Web URL is the preferred technique for joining a meeting from a computer. The Web URL points to a Web server from which is downloaded a Join launcher agent that will join the meeting either from Skype for Business 2015, if installed on the computer, or from .

## Managing conferences

An organizer can list existing conferences, and modify or delete them as needed.


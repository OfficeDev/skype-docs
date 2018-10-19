---
title: ConferenceManagement (QuickStart)
TOCTitle: ConferenceManagement (QuickStart)
ms:assetid: 0e7252c6-1677-4b10-8713-1326830ab9e1
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454817(v=office.16)
ms:contentKeyID: 65240092
ms.date: 07/27/2015
mtps_version: v=office.16
---

# ConferenceManagement (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: ConferenceManagement

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\ConferenceManagement

## Description

The application initializes a server platform, four [UserEndpoint](https://msdn.microsoft.com/en-us/library/hh348819\(v=office.16\)) objects, and one [ApplicationEndpoint](https://msdn.microsoft.com/en-us/library/hh384825\(v=office.16\)) object. The UserEndpoint objects act as the conference organizer and participants. The ApplicationEndpoint endpoint impersonates a phone user in the conference, simulating someone joining from a PSTN gateway. The organizer endpoint schedules an audio/video conference using the conference scheduling APIs, with the following conference scheduling options:

  - Conference access level set to **Invited**. Only participants on the invited list can be admitted.

  - Participant A is in the invited list of the conference.

  - Participants who are not on the invited list are sent to the lobby when they join the conference.

  - Automatic leadership assignment (automatic promotion to leader) is enabled for users from the same company.

  - A participant who joins the conference as a gateway participant bypasses the lobby.

The sample application logic proceeds as follows:

1.  Participant A is invited to the conference and joins the conference successfully. Participant A is automatically promoted to leader.

2.  Participants B and C, who are not in the invited list, join the conference and land in the conference lobby.

3.  Participant A is notified that Participants B and C are in the lobby.

4.  The [ApplicationEndpoint](https://msdn.microsoft.com/en-us/library/hh384825\(v=office.16\)) endpoint, impersonating a phone user, joins the conference successfully as a gateway participant.

5.  The [ApplicationEndpoint](https://msdn.microsoft.com/en-us/library/hh384825\(v=office.16\)) is notified that Participant A is in the conference and Participants B and C are in the conference lobby.

6.  Organizer joins the conference successfully and receives notification that Participants B and C are in the lobby and participants A and phone user are in the conference.

7.  Organizer admits participant B from the lobby and denies access to participant C.

8.  Organizer modifies the conference access level to [SourceNetwork](https://msdn.microsoft.com/en-us/library/hh385294\(v=office.16\)).**SameEnterprise** (opens the conference), and then participant C joins the conference successfully.

For information about provisioning trusted applications and endpoints in Microsoft Lync Server, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

## Features

  - Conference scheduling and initialization.

  - Joining a conference using the default join mode.

  - Joining a conference as a gateway participant.

  - Conference lobby operation (admission and denial of lobby participants).

  - ConferenceSession notifications.

  - AudioVideoMcuSession notifications.

  - Automatic promotion to leader for participants in the same company where the conference is scheduled.

  - Lobby bypass for gateway participants.

  - Conversation impersonation.

## Prerequisites

  - .

  - Two users enabled to use Skype for Business Server 2015.

  - The credentials for each user and a client capable of signing in to Skype for Business Server 2015.

  - A provisioned and trusted application endpoint.

  - A signed-in Skype for Business 2015 client.

## Running the sample

1.  Supply the configuration settings to be used by the sample in the accompanying App.config file, or you will be prompted for any settings when you run the sample.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.


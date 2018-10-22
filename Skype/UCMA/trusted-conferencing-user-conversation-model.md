---
title: Trusted conferencing user conversation model
TOCTitle: Trusted conferencing user conversation model
ms:assetid: 49a76bad-e38e-4e1b-9660-490748041d32
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466016(v=office.16)
ms:contentKeyID: 65239947
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Trusted conferencing user conversation model


**Applies to**: Skype for Business 2015

Using the Trusted Conferencing User model, an application can create multiple [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api) instances in the context of a single [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) instance. This call structure allows for a closer alignment with the problem domain. An application not using this model must manage one conversation for each user, all of whom communicate with the same conference. The TCU model reduces management overhead for the application by having it manage only one conversation for each conference, resulting in fewer objects to manage.

The following illustration shows the architecture of the TCU model.

![TCU model architecture](images/Dn466016.UCMA3-TCUArch(Office.16).png "TCU model architecture")

### Shared command channel

A Trusted Conferencing User conversation serves multiple users. Some of these users might not have a Focus channel in which to issue conference control commands such as muting self, muting or unmuting others, dialing out to another user, and similar operations. A TCU conversation enables an application to provide a Focus channel for users who would not ordinarily have a Focus channel. The commands are issued within the context of a specific user, so the server enforces the appropriate security policies for that user. For example, an attendee cannot mute others.


> [!NOTE]
> <P>An application that uses the TCU conversation model can still issue commands under its own identity. However, because the application is trusted, it is automatically treated as a presenter. For this reason, an application also must have the ability to execute commands in the context of another user to avoid an elevation of privilege.</P>



### Shared notification channel

Only one notification channel is established to receive conference notifications. Applications using the TCU model monitor the roster through the shared notification channel in order to display announcements or take actions in context of its users using the shared command channel. Having one subscription channel is a major performance gain. Having several subscriptions can cause a flood of notifications, all which are identical.

To further illustrate this concept, consider a TCU conversation that is acting as a private virtual assistant (PVA) for a user connected by telephone. If the user is muted, the TCU conversation detects that this user has been muted from the notification received on the shared notification channel. The TCU conversation then prompts the user by playing a beep as a notification that he or she has been muted.


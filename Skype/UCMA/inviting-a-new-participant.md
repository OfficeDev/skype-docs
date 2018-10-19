---
title: Inviting a new participant
TOCTitle: Inviting a new participant
ms:assetid: 3075d768-6784-44ff-aa61-9c7598c7cd6c
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465996(v=office.16)
ms:contentKeyID: 65239942
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Inviting a new participant


_**Applies to:** Skype for Business 2015_

The standard way of inviting a new participant into a conference is to create an invitation for the participant, and then deliver the invitation to the participant. The basic steps for inviting a new participant to a conference are as follows.

  - Create a [ConferenceInvitation](https://msdn.microsoft.com/en-us/library/hh349823\(v=office.16\)) instance.

  - Use one of the [BeginDeliver()](https://msdn.microsoft.com/en-us/library/hh381143\(v=office.16\)) methods to deliver the invitation to a recipient.

The [State](https://msdn.microsoft.com/en-us/library/hh366182\(v=office.16\)) property and the [StateChanged](https://msdn.microsoft.com/en-us/library/hh349590\(v=office.16\)) event on the **ConferenceInvitation** class can be used to monitor changes in state of the invitation.

When an application receives an invitation, it is normally expected to accept the invitation and join the conference so that it shows up in the list of participants. Different applications have different capabilities, so not all of them will be able to join a given conference. In some cases, the **BeginDeliver** method can detect that a remote application is unable to join the conference on its own, so **BeginDeliver** requests the MCU to make a call to the remote application. In this way, an application that is unable to join the conference as a participant can still take part in the conference. If this happens, even though the invitation succeeded and the remote application has joined the conference, the application might not appear in the list of participants. Some applications might not support conferencing at all, in which case the [EndDeliver(IAsyncResult)](https://msdn.microsoft.com/en-us/library/hh382300\(v=office.16\)) method throws an exception.

For most applications that send invitations, being able to detect whether the invitation was accepted is of utmost importance. The following example demonstrates how this can be done.

``` csharp
public void InviteToConference()
{
  ConferenceInvitation invitation = new ConferenceInvitation(m_conversation);

  invitation.StateChanged += this.InvitationStateChanged;

  ConferenceInvitationDeliverOptions deliverOptions = 
      new ConferenceInvitationDeliverOptions();
  deliverOptions.ToastMessage = new ToastMessage("This is the popup message or toast.");

  try
  {
    invitation.BeginDeliver(
        "sip:mary@contoso.com",
        deliverOptions,
        this.DeliverCompleted,
        invitation);
  }
  catch (InvalidOperationException)
  {
    // Conversation was terminated while trying to add a participant.
  }
}

private void DeliverCompleted(IAsyncResult result)
{
  RealTimeException ex = null;
  ConferenceInvitation invitation = result.AsyncState as ConferenceInvitation;
  
  try
  {
    invitation.EndDeliver(result);

    // Invitation was accepted
  }
  catch (RealTimeException exception)
  {
    // Invitation was not accepted.
    ex = exception;
  }
  finally
  {
    .
    . 
    .
  }
}
```


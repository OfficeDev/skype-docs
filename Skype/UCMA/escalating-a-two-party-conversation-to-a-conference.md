---
title: Escalating a two-party conversation to a conference
TOCTitle: Escalating a two-party conversation to a conference
ms:assetid: 15038dcf-f491-4212-9d1d-d8c0adb66a5f
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465987(v=office.16)
ms:contentKeyID: 65239924
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Escalating a two-party conversation to a conference


_**Applies to:** Skype for Business 2015_

An application can escalate a two-party IM conversation to a conference conversation only after the conference has been successfully joined, and the [State](https://msdn.microsoft.com/en-us/library/hh161754\(v=office.16\)) of the conference session is **Connected**. The escalation process involves moving the existing IM calls into the Multipoint Control Unit (MCU) session, requesting that the remote participant from the existing two-party IM conversation join the ad hoc conference, and terminating the two-party IM session with the remote participant. If the escalation is unsuccessful, the conversation reverts to a two-party call.


> [!IMPORTANT]
> <P>A two-party audio/video conversation cannot be escalated to a conference. The audio/video provider that is supplied with Microsoft Unified Communications Managed API 5.0 does not support escalation of this type. If your application must enable the escalation of an audio/video conversation to a conference, you must provide a custom subclass of the <A href="https://msdn.microsoft.com/en-us/library/hh383767(v=office.16)">MediaProvider</A> abstract class. For more information, see <A href="extending-the-mediaprovider-class.md">Extending the MediaProvider class</A> and the other topics in the <A href="extending-the-ucma-platform.md">Extending the UCMA platform</A> section.</P>



The following code demonstrates escalating an IM conversation to a conference.

``` csharp
conversation.BeginEscalateToConference(Conference_EscalationCompleted, conferenceSession);
```


> [!IMPORTANT]
> <P>Before the conversation can be escalated to a conference, a <A href="https://msdn.microsoft.com/en-us/library/hh349315(v=office.16)">ConferenceSession</A> instance must be created, and this instance must be joined to the conference by a call to the <A href="https://msdn.microsoft.com/en-us/library/hh349641(v=office.16)">BeginJoin</A> method on the <STRONG>ConferenceSession</STRONG> instance. For more information, see <A href="joining-a-conference.md">Joining a conference</A>.</P>



When an application is in a two-party IM conversation, a remote participant can escalate this two-party conversation to a conference. When this happens, the application receives an incoming escalation request. To be able to accept and complete the escalation operation, the application must join the conference and escalate the two-party conversation to a multi-party conversation.

The following example shows how to register to receive notifications of incoming escalation requests. In this example, the application registers a handler for the [EscalateToConferenceRequested](https://msdn.microsoft.com/en-us/library/hh366360\(v=office.16\)) event.

``` csharp
conversation.EscalateToConferenceRequested += this.Conversation_EscalateToConferenceRequested;
```

The following code demonstrates responding to an incoming escalation request.

``` csharp
void Conversation_EscalateToConferenceRequested(object sender, EscalateToConferenceRequestedEventArgs e)
{
  Conversation conversation = sender as Conversation;
 
  // The conference session of the escalating conversation must be joined.
  conversation.ConferenceSession.BeginJoin(default(ConferenceJoinOptions), ConferenceSessionJoinCompleted, conversation.ConferenceSession);
}

private void ConferenceSessionJoinCompleted(IAsyncResult ar)
{
  ConferenceSession confSession = ar.AsyncState as ConferenceSession;
  confSession.EndJoin(ar);
 
  // BeginEscalateToConference is called here to indicate to the endpoint that requested 
  // the escalation, that the recipient has completed the escalation procedure.
  confSession.Conversation.BeginEscalateToConference(EscalateToConferenceCompleted, confSession.Conversation);
}
 
private void EscalateToConferenceCompleted (IAsyncResult ar)
{
  Conversation conversation = ar.AsyncState as Conversation;
  conversation.EndEscalateToConference(ar);
}
```


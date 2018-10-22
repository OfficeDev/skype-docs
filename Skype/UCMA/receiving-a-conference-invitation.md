---
title: Receiving a conference invitation
TOCTitle: Receiving a conference invitation
ms:assetid: b1e27f55-ca3d-418a-96a0-ec9d15ffd555
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466003(v=office.16)
ms:contentKeyID: 65239944
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Receiving a conference invitation


**Applies to**: Skype for Business 2015

At the SIP level, a [Call](https://msdn.microsoft.com/en-us/library/hh384235\(v=office.16\)) is represented as a SIP INVITE with Session Description Protocol (SDP) that contains information about the media types that are included in the call, together with other information that helps negotiate a connection between the sender and recipient of the INVITE.

Similarly, a [ConferenceInvitation](https://msdn.microsoft.com/en-us/library/hh349823\(v=office.16\)) is also represented at the SIP level as a SIP INVITE, although the SDP contains XML data that describes the conference and the media types that are available in the conference.

The following list describes the steps an application must perform when it receives a conference invitation. Several steps refer to the code example that follows the list.

1.  The application registers for the [ConferenceInvitationReceived](https://msdn.microsoft.com/en-us/library/hh366294\(v=office.16\)) event (an event on the [LocalEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.localendpoint?view=ucma-api) class).

2.  When the **ConferenceInvitationReceived** event is raised, the application accepts the invitation by calling the **BeginAccept** method on the [Invitation](https://msdn.microsoft.com/en-us/library/hh348650\(v=office.16\)) property. This is shown in the following example, in the *ConferenceInvitationReceived* method.
    
    For the sake of simplicity, the example does not ask the application user whether to accept the invitation. If the [IsImmediateAutoAcceptNeeded](https://msdn.microsoft.com/en-us/library/hh380867\(v=office.16\)) property on *invite* is true, the application should accept the invitation without consulting the user. If **IsImmediateAutoAcceptNeeded** is false, the application should register for the [AutoAcceptNeeded](https://msdn.microsoft.com/en-us/library/hh366101\(v=office.16\)) event and should immediately consult the user to ask whether the invitation should be accepted. If, after 15 seconds, the user has not responded to the query, the **AutoAcceptNeeded** event is raised, provided that the invitation contains **MediaType.Message** (see the first if statement in the *ConferenceSession\_JoinCompleted* method) and **eventArgs.IsForked** is true. If the **AutoAcceptNeeded** event is raised, the application must accept the invitation without further consultation with the user.

3.  After the conference invitation has been accepted successfully, the application joins the conference by calling one of the overloaded [BeginJoin()](https://msdn.microsoft.com/en-us/library/hh349641\(v=office.16\)) methods. This is shown in the following example, in the *ConferenceInvitation\_AcceptCompleted* method.

4.  If the join operation has completed successfully, (when the [EndJoin](https://msdn.microsoft.com/en-us/library/hh348936\(v=office.16\)) method on the ConferenceSession instance returns successfully), the application can participate by creating an instant message call or an audio/video call, and then establishing the call, using **BeginEstablish**. This is shown in the following example, in the *ConferenceInvitation\_JoinCompleted* method.

5.  After the call is established successfully, (when the [EndEstablish](https://msdn.microsoft.com/en-us/library/hh349248\(v=office.16\)) method returns successfully), the application can use the call to exchange media (that is, the application is ready to send and receive instant messages in an [InstantMessagingCall](https://msdn.microsoft.com/en-us/library/hh161841\(v=office.16\)) or to send and receive audio in an [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api)). The *ImCall\_EstablishCompleted* and *AvCall\_EstablishCompleted* methods in the following example show how to check that an instant message call and an audio/video call, respectively, have been successfully established.

<!-- end list -->

```csharp
public class ConferenceInvitationExample
// This class is shown for demonstration purposes only. It is incomplete,
// and so will fail to compile.
{
  private Conversation m_conversation;
  private InstantMessagingCall m_imCall;
  private AudioVideoCall m_avCall;
  private LocalEndpoint m_endpoint;

  public ConferenceInvitationExample()
  {
  }

  public void RegisterForConferenceInvitationReceived()
  {
    m_endpoint.ConferenceInvitationReceived += new EventHandler<ConferenceInvitationReceivedEventArgs>(ConferenceInvitationReceived);
  }

  private void ConferenceInvitationReceived(object sender, ConferenceInvitationReceivedEventArgs eventArgs)
  {
    ConferenceInvitation invite = eventArgs.Invitation;
    invite.BeginAccept(ConferenceInvitation_AcceptCompleted, invite);
  }

  private void ConferenceInvitation_AcceptCompleted(IAsyncResult result)
  {
    try
    {
      ConferenceInvitation invite = result.AsyncState as ConferenceInvitation;
      invite.EndAccept(result);
      m_conversation = invite.Conversation;
      RegisterConversationHandlers();
      ConferenceJoinOptions cjo = new ConferenceJoinOptions();
      cjo.JoinAsTrustedApplication = false;
      m_conversation.ConferenceSession.BeginJoin(cjo, ConferenceSession_JoinCompleted, invite);
    }
    catch (RealTimeException ex)
    {
      Trace.TraceError("invite.EndAccept failed. Exception: {0}", ex.ToString());
    }
    catch (InvalidOperationException ex)
    {
      Trace.TraceError("m_conversation.ConferenceSession.BeginJoin failed. Exception: {0}", ex.ToString());
    }
  }

  private void ConferenceSession_JoinCompleted(IAsyncResult result)
  {
    try
    {
      ConferenceInvitation invite = result.AsyncState as ConferenceInvitation;
      Collection<string> activeMediaTypes = invite.GetAvailableMediaTypes();

      m_conversation.ConferenceSession.EndJoin(result);
      if (activeMediaTypes.Contains(MediaType.Message))
      {
        try
        {
          InstantMessagingCall imCall = new InstantMessagingCall(m_conversation);
          imCall.BeginEstablish(ImCall_EstablishCompleted, imCall);
        }
        catch (InvalidOperationException ex)
        {
          Trace.TraceError("imCall.BeginEstablish failed. Exception: {0}", ex.ToString());   
        }
      }
      if (activeMediaTypes.Contains(MediaType.Audio))
      {
        try
        {
          AudioVideoCall avCall = new AudioVideoCall(m_conversation);
          avCall.BeginEstablish(AvCall_EstablishCompleted, avCall);
        }
        catch (InvalidOperationException ex)
        {
          Trace.TraceError("avCall.BeginEstablish failed. Exception: {0}", ex.ToString());
        }
      }
    }
    catch (RealTimeException ex)
    {
      Trace.TraceError("m_conversation.ConferenceSession.EndJoin failed. Exception: {0}", ex.ToString());
    }
  }

  private void ImCall_EstablishCompleted(IAsyncResult result)
  {
    try
    {
      InstantMessagingCall imCall = result.AsyncState as InstantMessagingCall;
      imCall.EndEstablish(result);
      m_imCall = imCall;
    }
    catch (RealTimeException ex)
    {
      Trace.TraceError("imCall.EndEstablish failed. Exception: {0}", ex.ToString());
    }
  }

  private void AvCall_EstablishCompleted(IAsyncResult result)
  {
    try
    {
      AudioVideoCall avCall = result.AsyncState as AudioVideoCall;
      avCall.EndEstablish(result); 
      m_avCall = avCall;
    }
    catch (RealTimeException ex)
    {
      Trace.TraceError("avCall.EndEstablish failed. Exception: {0}", ex.ToString());
    }
  }

  public void RegisterConversationHandlers()
  {
    Debug.Assert(null != m_conversation);
    m_conversation.EscalateToConferenceRequested += Conference_EscalateToConferenceRequested;
    m_conversation.InviteRemoteParticipantUpdate += Conversation_InviteRemoteParticipantUpdate;
    m_conversation.PropertiesChanged += Conversation_PropertiesChanged;
    m_conversation.StateChanged += Conversation_StateChanged;
    m_conversation.RemoteParticipantAttendanceChanged += Conversation_RemoteAttendanceChanged;
    m_conversation.ParticipantPropertiesChanged += Conversation_RemoteParticipantPropertyChanged;
  }

}
```


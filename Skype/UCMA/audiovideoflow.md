---
title: AudioVideoFlow
TOCTitle: AudioVideoFlow
ms:assetid: 095bc495-8338-4cd7-8e1f-6964861728df
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466030(v=office.16)
ms:contentKeyID: 65239968
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# AudioVideoFlow


_**Applies to:** Skype for Business 2015_

**In this article**  
AudioVideoFlow properties  
AudioVideoFlow methods  
Negotiation between local and remote endpoints  
AudioVideoFlow state transitions  

An [AudioVideoFlow](https://msdn.microsoft.com/en-us/library/hh383533\(v=office.16\)) instance represents a media connection with a single remote participant.

## AudioVideoFlow properties

The [Audio](https://msdn.microsoft.com/en-us/library/hh161753\(v=office.16\)) property provides access to an [AudioControl](https://msdn.microsoft.com/en-us/library/hh161771\(v=office.16\)) instance, which provides access to the audio channel and can be used such purposes as muting and unmuting the audio channel, changing the sampling rate, and changing the direction. The [Player](https://msdn.microsoft.com/en-us/library/hh383679\(v=office.16\)) property provides access to a [Player](https://msdn.microsoft.com/en-us/library/hh349780\(v=office.16\)) instance, which can be used to play audio data captured previously. The [Recorder](https://msdn.microsoft.com/en-us/library/hh382678\(v=office.16\)) property provides access to a [Recorder](https://msdn.microsoft.com/en-us/library/hh381624\(v=office.16\)) instance, which can be used to record audio and video data. The [SpeechRecognitionConnector](https://msdn.microsoft.com/en-us/library/hh365919\(v=office.16\)) property provides access to a [SpeechRecognitionConnector](https://msdn.microsoft.com/en-us/library/hh383253\(v=office.16\)) instance, which can be used in conjunction with the **Microsoft.Speech** namespace to perform speech recognition. The [SpeechSynthesisConnector](https://msdn.microsoft.com/en-us/library/hh382006\(v=office.16\)) property provides access to a [SpeechSynthesisConnector](https://msdn.microsoft.com/en-us/library/hh349773\(v=office.16\)) instance, which can be used in conjunction with the **Microsoft.Speech** namespace to render text as speech. The [ToneController](https://msdn.microsoft.com/en-us/library/hh348941\(v=office.16\)) property provides access to a [ToneController](https://msdn.microsoft.com/en-us/library/hh349643\(v=office.16\)) instance. A **ToneController** can be used to send and receive Dual-Tone Multi-Frequency (DTMF) tones and to receive Fax tones.

The [Player](https://msdn.microsoft.com/en-us/library/hh383679\(v=office.16\)), [Recorder](https://msdn.microsoft.com/en-us/library/hh382678\(v=office.16\)), [ToneController](https://msdn.microsoft.com/en-us/library/hh348941\(v=office.16\)), [SpeechRecognitionConnector](https://msdn.microsoft.com/en-us/library/hh365919\(v=office.16\)), and [SpeechSynthesisConnector](https://msdn.microsoft.com/en-us/library/hh382006\(v=office.16\)) properties are considered to be devices. Although these devices are represented as properties on the **AudioVideoFlow** class, they are not automatically instantiated. Before any of these devices can be used, it must be created, and then attached to an **AudioVideoFlow** instance. The following code example shows the steps required to create a **Player** and attach it to an existing **AudioVideoFlow** instance.

``` csharp
Player myPlayer = new Player();
myPlayer.AttachFlow(avFlow);
```

When the device is no longer needed, the **AudioVideoFlow** instance that is attached to the device must be detached, as this does not happen automatically. The following sample shows how this is done.

    myPlayer.DetachFlow(avFlow);

For more information about these devices, see [Devices in UCMA](https://msdn.microsoft.com/en-us/library/dd280152\(v=office.16\)).

## AudioVideoFlow methods

An application that intends to configure the **AudioVideoFlow** by using [Initialize(AudioVideoFlowTemplate)](https://msdn.microsoft.com/en-us/library/hh381417\(v=office.16\)) must process the [AudioVideoFlowConfigurationRequested](https://msdn.microsoft.com/en-us/library/hh383342\(v=office.16\)) event synchronously. **AudioVideoFlowConfigurationRequested** is an [AudioVideoCall](https://msdn.microsoft.com/en-us/library/hh383901\(v=office.16\)) event. The application can assume that the **AudioVideoFlow** [State](https://msdn.microsoft.com/en-us/library/hh349893\(v=office.16\)) will not change from **Idle** during the event callback, so no state check is necessary. Only an invalid template configuration can possibly cause **Initialize** to throw an exception.

## Negotiation between local and remote endpoints

After an **AudioVideoFlow** instance has been initialized or modified, negotiation with the remote participant is carried out. Settings such as [HoldStatus](https://msdn.microsoft.com/en-us/library/hh349483\(v=office.16\)) and **AllowedDirection** on the remote endpoint can affect the resulting **AudioVideoFlow** settings. For example, if the application sets **AllowedDirection** to **MediaChannelDirection**.**SendReceive**, but the remote has previously set **AllowedDirection** to **MediaChannelDirection**.**SendOnly**, the **Direction** setting on the local endpoint will become **MediaChannelDirection**.**ReceiveOnly**.

The following steps show the values of **AllowedDirection**, **Direction**, and **HoldStatus** after negotiations have finished:

1.  The local endpoint sets **AllowedDirection** to **MediaChannelDirection**.**SendReceive**, and **HoldStatus** is **HoldType**.**None**. On the remote endpoint, **AllowedDirection** = **MediaChannelDirection**.**SendOnly**.
    
    Result: **Direction** on the local endpoint will be set to **MediaChannelDirection**.**ReceiveOnly**.

2.  The local endpoint applies a hold to both ends, using a call to **BeginHold**(**HoldType**.**BothEndpoints**, , ).
    
    Results: **AllowedDirection** is **MediaChannelDirection**.**SendReceive**, [HoldStatus](https://msdn.microsoft.com/en-us/library/hh349483\(v=office.16\)) is set to **HoldType**.**BothEndpoints**, and **Direction** on the local endpoint will be set to **MediaChannelDirection**.**Inactive**.

3.  The local endpoint removes the hold, using a call to [BeginRetrieve](https://msdn.microsoft.com/en-us/library/hh381101\(v=office.16\)).
    
    Results: **AllowedDirection** is **MediaChannelDirection**.**SendReceive**, **HoldStatus** is set to **HoldType**.**None**, and **Direction** on the local endpoint will depend on the response from the remote endpoint. If the remote endpoint responds with **MediaChannelDirection**.**SendReceive**, then **Direction** will be set to **MediaChannelDirection**.**SendReceive**. If the remote endpoint responds with **MediaChannelDirection**.**SendOnly** (as in step 1), then **Direction** on the local endpoint will be set to **MediaChannelDirection**.**ReceiveOnly**.

In negotiation with a remote endpoint, if the local endpoint sends an offer that places a restriction on the media flow by a change in the direction or by placing a hold, it does not matter if the remote endpoint rejects the offer. On the other hand, if the local endpoint sends an offer that removes existing restrictions, it waits for the remote endpoint to accept or reject the offer.

Operations that mute a media flow do not require negotiation.

## AudioVideoFlow state transitions

The **AudioVideoFlow** state transitions are shown in the following illustration.

![AudioVideoFlow state transitions](images/Dn466030.StateMach_AVFlow(Office.16).jpg "AudioVideoFlow state transitions")

1.  The transition from **Idle** to **Active** occurs when the **AudioVideoFlow** instance determines that media can now be exchanged (or should be able to be exchanged) with the remote peer. The most common causes for this transition are:
    
      - Media (RTP or RTCP) is received from the remote peer.
    
      - For an incoming call, acknowledgement from the caller that it has received a provisional acceptance of the call (by the application calling [BeginEstablishEarlyMedia()](https://msdn.microsoft.com/en-us/library/hh365657\(v=office.16\))). That is, a SIP PRACK message is received from the caller.
    
      - Special case: The remote endpoint negotiates the audio channel to be disabled. Disabled media means that no transport connection will be established; in SDP, the media is identified with a ‘c=IN IP4 0.0.0.0’ connection address line.

2.  The transition from **Active** to **Terminated** occurs when a call ends after the media session is established. The most common causes for this transition are:
    
      - One of the endpoints terminates the call.
    
      - The media session fails due to loss of media (for example, an RTP timeout).
    
      - An unrecoverable error (for example, a SIP timeout) occurs during a renegotiation (re-INVITE) request sent to the remote peer.

3.  The transition from **Idle** to **Terminated** occurs when there is a failure in establishing the call. The most common causes for this transition are:
    
      - The call is rejected.
    
      - There is a failure in the offer/answer negotiation (for example, a failure to negotiate encryption parameters (SRTP)).
    
      - The application terminates the call before the media session is established.
    
      - The media session fails to establish a working transport connection between endpoints.


> [!IMPORTANT]
> <P>A transition of the state of an <STRONG>AudioVideoFlow</STRONG> instance to <STRONG>Active</STRONG> does not imply that the associated <A href="https://msdn.microsoft.com/en-us/library/hh383901(v=office.16)">AudioVideoCall</A> has been accepted or established. The <STRONG>AudioVideoFlow</STRONG> instance’s state can change to <STRONG>Active</STRONG> before the call is established. In this situation, any media exchanged on the <STRONG>AudioVideoFlow</STRONG> is called "early media." The application should avoid sending "important" media (such as a prompt that must be responded to) during this early media phase, because the remote endpoint might not render it. For important media, the application should wait for both the <STRONG>AudioVideoFlow</STRONG> to transition to the <STRONG>Active</STRONG> state and for the <STRONG>AudioVideoCall</STRONG> to transition to the <STRONG>Established</STRONG> state. During early media, the application might defer sending any media at all, or might instead just send some background music.</P>



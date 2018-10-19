---
title: Trusted conference users
TOCTitle: Trusted conference users
ms:assetid: a0dca201-f920-45ca-b2e4-2f0d43b315d7
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465963(v=office.16)
ms:contentKeyID: 65239853
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Trusted conference users


_**Applies to:** Skype for Business 2015_

**In this article**  
Conference "join as Trusted User"  
Conferencing commands on behalf of other participants  
Scalable and multi-channel  
Audio route management  
Default audio routing  
Common use cases  

The Trusted Conferencing User model can be used to enable advanced telephony scenarios. At a high level, the Trusted Conferencing User model is a programming model that gives application writers a way to seamlessly enhance the communication experience of one or more participants engaged in a conversation.

This model requires the application to run with elevated permissions granted by the Skype for Business Server 2015 Administrator.

## Conference "join as Trusted User"

Trusted user privileges include the ability to join a conference as Trusted User. Traditional participants of a conference are end-users who are either leaders or attendees, whose user names are displayed in the roster of every client connected to the conference. In contrast, a trusted user (typically an application) does not appear in the conference roster, yet has full control over the conference.

## Conferencing commands on behalf of other participants

A trusted user can issue conferencing commands on behalf-of participants to the extent their respective role allows them to issue these commands. For example, if a participant joined as an attendee, the trusted user can mute the attendee on her behalf, but would fail muting another participant on her behalf as muting others requires the participant to be a Leader. Now, if the participant were to be a Conference Leader, the trusted user would have successfully performed muting the other participant on her behalf.

## Scalable and multi-channel

A trusted user can concurrently connect multiple channels of a given modality to a given conference while sharing the same conference state amongst these channels to scale better. A trusted user can have, for example, one hundred audio channels concurrently connected to the same audio conference, where each audio channel serves a dedicated purpose. One audio channel can be used to play music-on-hold back to the conference participants, and other channels can be used to establish a full-duplex private audio channel with a given participant, and another channel can be used to listen silently to all the participants of a conference or to make culture-specific announcements to a subset of the audio conference participants.


> [!NOTE]
> <P>The trusted conferencing user model can be extended to support media types other than the default types (audio and text) provided in UCMA 5.0. For more information, see <A href="extending-the-ucma-platform.md">Extending the UCMA platform</A>.</P>



## Audio route management

After joining a conference, a Trusted Conferencing User can manage how audio is routed to and from the audio-video MCU on an audio channel basis. Each audio channel consists of two parts: a media source and a media sink. A media sink is typically a speaker, a recorder, a tone controller (to receive dual-tone multifrequency (DTMF) tones), or a Speech Transcription engine (for automatic speech recognition). A media source can be a Player (for file playback), a Speech Synthesizer (for blending file playback with Text-To-Speech), or a Tone Controller to send DTMF tones.

There are two types of audio routes in a conference: incoming audio routes and outgoing audio routes. An incoming audio route represents the path followed by the audio coming from a particular conference participant and routed to the media sink of a Trusted User audio channel. An outgoing audio route represents the path followed by the audio coming from the media source of a Trusted User audio channel and routed to a particular conference participant.

## Default audio routing

Default audio routing designates the way the audio-video MCU connects the "ear" and the "mouth" of every participant to, respectively, the "mouth" and "ear" of each other participant of the audio conference. Default audio routing represents the forward mixing that occurs at the audio-video MCU when participants commonly join an audio conference.

A Trusted User can remove a participant from the default audio routing, and later bring the participant back. A participant who is removed from the default audio routing cannot hear other conference participants. The participant hears only "dead air" unless the Trusted User sets up an outgoing audio route to the participant. A potential use for this technique is to play music-on-hold to a participant who is waiting for a response of some kind.

## Common use cases

To illustrate the concept of default audio routing and audio route management, consider the following use case inspired by a customer interaction with an agent, an expert, and a supervisor in a Contact Center.

In this example use case, the customer is connected to an available agent. The agent is unable to answer a question from the customer, and escalates the call to an expert. The identities of both the agent and the expert are concealed from the customer through the use of a back-to-back user agent. At this point of the customer interaction, the customer, agent, and expert can all hear one another as their respective audio streams are being mixed in the default audio routing. Note that this is the default state of participants joining an audio conference; there is no Trusted Conferencing User intervention up to this point. The following illustration shows the routing of the audio and signaling channels.

![Audio routing - 1](images/Dn465963.TCU_AudioRouting1(Office.16).png "Audio routing - 1")

The following table represents the default audio routing of audio-video MCU that performs a forward mix of the customer, agent, and expert audio streams. This table is also known as the "who’s talking to whom" table and shows which audio sources are connected to which audio sinks.

![TCU routing table - 1](images/Dn465963.TCU_RoutingTbl1(Office.16).jpg "TCU routing table - 1")

In the next illustration, the agent requests a side bar with the expert to discuss an important matter regarding the customer’s problem. The agent places the customer on hold. As the following illustration shows, the customer is removed from the default audio routing by a Trusted Conferencing User joined into the Conference.

![Audio routing - 2](images/Dn465963.TCU_AudioRouting2(Office.16).png "Audio routing - 2")

The corresponding audio-video MCU routing table is shown next. Note the new row and column representing the Trusted Conferencing User (Music On Hold). The Trusted Conferencing User joins the audio conference outside the Default Audio Routing, and the music that is being played cannot be heard by any participant. Also note that the customer cannot hear or be heard by any other participant of the conference. The agent and expert remain in the Default Audio Routing and receive a visual indication in the roster displayed in Skype for Business 2015 that the customer is outside the Default Audio Routing.

![TCU routing table - 2](images/Dn465963.TCU_RoutingTbl2(Office.16).jpg "TCU routing table - 2")

After removing the customer from the Default Audio Routing, the Trusted Conferencing User creates an outgoing audio route targeting the customer. The customer begins receiving music from the Music on Hold server while the agent talks with the expert on the same audio conference.

![Audio routing - 3](images/Dn465963.TCU_AudioRouting3(Office.16).png "Audio routing - 3")

The audio-video MCU routing table for this situation is shown in the following table.

![TCU routing table - 3](images/Dn465963.TCU_RoutingTbl3(Office.16).jpg "TCU routing table - 3")

In the following illustration, the agent resumes the call with the customer. The customer is brought back into the Default Audio Routing by the Trusted Conferencing User.

![Audio routing - 4](images/Dn465963.TCU_AudioRouting1(Office.16).png "Audio routing - 4")

The corresponding audio-video MCU routing appears in the follow table.

![TCU routing table - 4](images/Dn465963.TCU_RoutingTbl4(Office.16).jpg "TCU routing table - 4")

For quality purposes, the agent’s supervisor begins listening silently to the customer interaction. The supervisor can hear the customer, agent, and expert, but cannot be heard by any of them. He is not in the Default Audio Routing, but is instead connected to the audio conference as a Trusted Conferencing User by way of a Back-To-Back User Agent.

![Audio routing - 5](images/Dn465963.TCU_AudioRouting5(Office.16).png "Audio routing - 5")

Note that the Trusted Conferencing User has joined outside the Default Audio Routing and has then set up three incoming audio routes, coming respectively from the customer, the agent, and the expert. The audio-video MCU table is shown here.

![TCU routing table - 5](images/Dn465963.TCU_RoutingTbl5(Office.16).jpg "TCU routing table - 5")

In the following illustration, the supervisor needs to whisper coaching instructions to the agent. To do this, the supervisor (Trusted Conferencing User) sets up an outgoing audio route that targets the agent. The customer and the expert cannot hear what the supervisor says.

![Audio routing - 6](images/Dn465963.TCU_AudioRouting6(Office.16).png "Audio routing - 6")

The following illustration shows the audio-video MCU routing table for this situation.

![TCU routing table - 6](images/Dn465963.TCU_RoutingTbl6(Office.16).jpg "TCU routing table - 6")

Finally, the supervisor decides to intervene by barging into the customer interaction. The Trusted Conferencing User sets up two additional outgoing audio routes with the customer and the expert (the outgoing audio route to the agent was set up in a previous step). Every participant of the audio conference can hear every other participant.

![Audio routing - 7](images/Dn465963.TCU_AudioRouting7(Office.16).png "Audio routing - 7")

The following illustration shows the audio-video MCU routing table in which each participant can hear all the others.

![TCU routing table - 7](images/Dn465963.TCU_RoutingTbl7(Office.16).jpg "TCU routing table - 7")


---
title: Contact center
TOCTitle: Contact center
ms:assetid: 68d51b20-9fd5-4b24-b3f7-23b81168536e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465935(v=office.16)
ms:contentKeyID: 65239779
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Contact center


_**Applies to:** Skype for Business 2015_

**In this article**  
Front-office interactions  
Back-office interactions  
UCMA system  
Typical call flow usage  
Customer care features  

With Microsoft Unified Communications Managed API 5.0, you can turn your communications infrastructure into a profit center. UCMA 5.0-based Contact Center applications are able to match customers (the Front-Office) with agents (the Back-Office), integrating traditional interactive voice response (IVR), customer relations management (CRM) data, and data from , to pass customer information to the agent, anonymizing agent-customer interactions, and managing agent and supervisor pools. The /UCMA 5.0 solution also allows applications to incorporate "whisper channels" in the back office, so that agents can silently communicate with supervisors, be coached, or monitor a call in progress.

Caller-agent pairing can be one-to-one, one-to-many, many-to-one, or many-to-many.

The following illustration shows the components involved in a contact center application.

![Contact Center details](images/Dn465935.UCMA-ContactCenter2(Office.16).png "Contact Center details")

Other names for the Contact Center scenario include Customer Care, Helpdesk, Call Center, Outbound Campaign, Service Desk and Automated Call Dispatch (ACD).

Skype for Business Server 2015 server and client extensibility (using UCMA 5.0, Skype for Business Server 2015, and Skype for Business SDK) enables a Contact Center platform that supports the following interactions.

## Front-office interactions

  - Anywhere access: Customers can belong to a variety of networks:
    
      - Same-enterprise Microsoft Skype for Business 2015 users, either on-site or remote.
    
      - Skype for Business Server 2015-federated Microsoft Skype for Business 2015 users.
    
      - Custom Web click-to-call, click-to-communicate controls, and discoverability through custom AJAX/HTML presence controls.
    
      - Skype for Business Server 2015-federated endpoint: PIC (Windows Live Messenger (IM, audio, video), Yahoo\!™ (IM) or AOL™ (IM)), Skype for Business Server 2015-Skype for Business Server 2015 inter-company, and external, remote users in the same company.
    
      - PSTN/cell phone (traditional).

  - Multimodal communications: Customers can choose the one or more modalities to use in their interactions with a customer service representative:
    
      - IM Chat
    
      - Audio
    
      - Video
    
      - Desktop sharing or application sharing

  - Contextual communications: Customer context can be communicated in either direction between the Contact Center and the agent to achieve the following goals:
    
      - Enabling quicker connection to customer service representatives by eliminating or reducing repetition of information, and by eliminating the need to switch devices to initiate communications.
    
      - Promoting better customer service and higher agent productivity by using richer communication context from Customer Relations Management (CRM)/Enterprise Resource Planning (ERP) and other data sources. Communication context is far richer than traditional caller ID.

  - Data Collection: Built-in IVR/Microsoft Speech/UCMA Player, Recorder, and Tone Controller technology, including the ability to execute VoiceXML, play and record audio, and send and receive dual-tone multi-frequency (DTMF) tones and Fax tones.

## Back-office interactions

  - Agent/expert/customer anonymity.

  - Agent matchmaking: Skills-based routing and presence-aware expert escalation.

  - Supervisor silent monitoring, whispering, barging-in supported by Skype for Business Server 2015 trusted conferencing model.

## UCMA system

  - Fully customizable: .NET Framework platform exposes simple concepts and integrates easily with other Microsoft platforms such as SQL, CRM, Exchange, and Sharepoint.

  - On-premise or off-premise.

  - Highly scalable and reliable solution.

## Typical call flow usage

The general call flow typical of a customer care application is as follows:

1.  A new incoming call arrives at the application, and is back-to-backed to the Skype for Business Server 2015 MCU infrastructure.

2.  The application adds the IVR to the call on the MCU, and receives customer information.

3.  The application then begins to look for agents, often utilizing Presence to determine an agent’s status. (It can also add Music on Hold to the MCU call, and remove the IVR).

4.  The application then contacts the agent, and adds the agent to the MCU call.

5.  Over the lifetime of the call, the customer care application can add or remove other agents, add or remove supervisors and whisper channels, and transfer the call, if necessary.

6.  During the call, the customer care application can record the call (using the MCU infrastructure), and add contextual data to the agent channel. The contact center can consume data from other sources such as customer relations management (CRM) and enterprise resource planning (ERP) and can pass custom application information to client-side extension applications.

## Customer care features

  - Anonymization, call control, and command and control
    
    UCMA-based contact centers make use of a back-to-back user agent to sit between the caller (Front Office) and the agent/target pool (Back Office). This back-to-back user agent allows the application to selectively manipulate the call, rendering the agents and the caller anonymous, forwarding or redirecting the call as needed, and intercepting IM commands from the Back Office, all without ever using the media channel. This allows the UCMA-based contact center to be modality agnostic; any modality supported by the endpoints is supported by the contact center.

  - Audio control (silent monitoring, whisper, coaching, barge-in, and recording)
    
    The UCMA-based trusted application can manipulate the audio channels of the conversation (by using trusted conference user (see [Trusted conferencing user](trusted-conferencing-user.md)) and the Skype for Business Server 2015 MCU infrastructure (see [Media handling - InstantMessage](media-handling-instantmessage.md) and [Media handling - AudioVideo](media-handling-audiovideo.md))) to selectively alter who is talking to whom at any time, enabling scenarios in which a supervisor can hear the entire call, and coach the agent without being heard by the customer. Alternatively, a supervisor can issue commands to the application to take over the customer call, add or remove agents, and place the customer on hold or remove the customer from hold. As the call is being handled, the UCMA application can silently add recording to the call, enabling traditional IVR call recording for quality purposes.

  - Contextual information
    
    Contextual information can be supplied in both directions between trusted UCMA applications and a number of agents using standard Microsoft Skype for Business 2015, or custom clients based on or UCMA. This contextual information allows all needed data about a user to reach the agent immediately at the time of call establishment, dynamically starting plug-in applications on the agent side at run time. By the use of these custom extension pane applications, CRM and ERP sources can be incorporated seamlessly for a richer customer experience.

  - Always-online
    
    UCMA applications can publish ‘always online’, to allow large groups of watchers to all be aware of the application (for example, a Helpdesk).


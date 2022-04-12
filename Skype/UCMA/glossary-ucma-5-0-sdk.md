---
description: This article contains the glossary for UCMA 5.0 SDK. 
title: Glossary (UCMA 5.0 SDK)
TOCTitle: Glossary
ms:assetid: b863b5e6-f008-474b-9e1a-f38173cca48e
ms:mtpsurl: https://msdn.microsoft.com/library/Dn465933(v=office.16)
ms:contentKeyID: 65239776
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Glossary (UCMA 5.0 SDK)


**Applies to**: Skype for Business 2015

<table>
<colgroup>

</colgroup>
<thead>
<tr class="header">
<th><p>Term</p></th>
<th><p>Definition</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Activated Conference</p></td>
<td><p>A conference that has at least one participant. A conference becomes activated when the first participant joins the conference. Conferences are deactivated after the last participant leaves the conference. Any conference properties that have changed while the conference was activated are lost after the conference is deactivated.</p></td>
</tr>
<tr class="even">
<td><p>ApplicationEndpoint</p></td>
<td><p>An endpoint type that is principally used by server applications, and that provides communication and collaboration services for end users. An <strong>ApplicationEndpoint</strong> is designed for high availability and resiliency against dialog route failure and is trusted by Skype for Business Server 2015. Because an <strong>ApplicationEndpoint</strong> is trusted, an application using this endpoint type must be provisioned before it is deployed. Provisioning ensures that the relevant information needed for the application and Skype for Business Server 2015 is in persistent stores such as Active Directory and the Central Management Store (used by Skype for Business Server 2015).</p>
<p>The trust relationship that an <strong>ApplicationEndpoint</strong> has with Skype for Business Server 2015 makes it possible for any Skype for Business Server 2015 Front End server to reach such an endpoint directly, which makes an <strong>ApplicationEndpoint</strong> more reliable than a <strong>UserEndpoint</strong>. If the preferred registrar of a <strong>UserEndpoint</strong> ceases operation, up to two minutes can elapse before other servers can be made aware of the problem. Although an <strong>ApplicationEndpoint</strong> is more reliable than a <strong>UserEndpoint</strong>, there are some operations that a <strong>UserEndpoint</strong> can perform that an <strong>ApplicationEndpoint</strong> cannot. For example, an <strong>ApplicationEndpoint</strong> cannot publish presence or schedule conferences on behalf of another user, or receive calls for specific users. For these purposes, an application must use a <strong>UserEndpoint</strong> object in conjunction with a trusted server platform.</p>
<p>An <strong>ApplicationEndpoint</strong> can also be used for some limited scenarios that do not involve Skype for Business Server 2015.</p></td>
</tr>
<tr class="odd">
<td><p>Back-to-Back User Agent</p></td>
<td><p>A back-to-back user agent allows one party (typically known as the call controller) to set up and manage communication between two or more other parties. A back-to-back user agent is also known as a third-party call control (3PCC) in telephony.</p></td>
</tr>
<tr class="even">
<td><p>Branch Office (BO)</p></td>
<td><p>In unified communications terminology, any remote location other than the Data Center that hosts the user agent for communications.</p></td>
</tr>
<tr class="odd">
<td><p>Browser</p></td>
<td><p>The object that sets up an audio user interface that callers use to communicate with (&quot;browse&quot;) an IVR application, which is composed of one or more VoiceXML documents. A VoiceXML <a href="https://msdn.microsoft.com/library/gg452712(v=office.16)">Browser</a> contains a VoiceXML interpreter and includes APIs that provide access to telephony and audio resources that facilitate communication with a caller. The terms &quot;Browser&quot; and &quot;VoiceXML Browser&quot; are used interchangeably in this documentation.</p></td>
</tr>
<tr class="even">
<td><p>Call</p></td>
<td><p>A communication session between the local endpoint and a remote endpoint. The <a href="https://msdn.microsoft.com/library/hh384235(v=office.16)">Call</a> abstract class provides the base class implementation needed to handle a Session Description Protocol (SDP)-based communication session. The SDP is used to negotiate media characteristics between the two endpoints. The base <strong>Call</strong> class implements the common functionality that is independent of the media types involved, and this functionality can be leveraged by derived classes. <a href="https://msdn.microsoft.com/library/hh161841(v=office.16)">InstantMessagingCall</a> and <a href="/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?">AudioVideoCall</a> are two subclasses that are provided by the Microsoft Unified Communications Managed API 5.0 for instant messaging and audio-video sessions. A call always belongs to a conversation.</p>
<p>The <strong>Call</strong>, <a href="https://msdn.microsoft.com/library/hh383767(v=office.16)">MediaProvider</a>, and <a href="https://msdn.microsoft.com/library/hh366262(v=office.16)">MediaFlow</a> abstract classes are the building blocks of the communication framework. A developer can create classes that are derived from these abstract classes to extend the platform, to establish a media channel between two endpoints for arbitrary media types.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/library/hh384820(v=office.16)">CallFactory</a></p></td>
<td><p>A call factory is used to create a call to handle a collection of media types. By default, the platform incorporates two call factories to handle the creation of <strong>InstantMessagingCall</strong> and <strong>AudioVideoCall</strong> instances. An application can remove these factories or add new custom call factories, although call factories that are registered with the platform cannot overlap in the media types supported. For simplicity, only incoming sessions with active media types that are supported by a registered call are exposed to the application. Incoming sessions with media types not supported by a registered call are rejected automatically by the UCMA platform.</p></td>
</tr>
<tr class="even">
<td><p>Call Admission Control (CAC)</p></td>
<td><p>Industry-standard terminology for controlling the creation of new calls, based on business rules (policy) that are coordinated through a policy distribution point (PDP) and enforced by a policy enforcement point (PEP). Call Admission Control can be used to manage bandwidth usage in an organization.</p></td>
</tr>
<tr class="odd">
<td><p>Call Parking</p></td>
<td><p>A user involved in a two party audio call can park a call at a Call Park server so that the call can be retrieved at another device or by another user. Typically, the user who parks the call is given an &quot;orbit&quot; number that uniquely identifies the parked call. The call can be retrieved by any user who has the orbit number. The user whose call is parked typically hears music while waiting for someone to retrieve the call. The Call Park server provides the means for users to be able to park and retrieve calls. This concept is typically applied to audio calls, but can be extended to other media types when appropriate.</p></td>
</tr>
<tr class="even">
<td><p>CollaborationPlatform</p></td>
<td><p>The <a href="https://msdn.microsoft.com/library/hh385176(v=office.16)">CollaborationPlatform</a> class is the foundational class for any application built using . This class is used to initialize the platform with such settings as listening port, the certificate to be used, and the list of trusted servers or domains. It is responsible for managing connections between servers (connection pooling, connection throttling), establishing trust with Skype for Business Server 2015 computers, and providing extensibility to calls of different modalities, such as audio and instant messaging.</p>
<p>A <strong>CollaborationPlatform</strong> instance represents one SIP stack instance. An application cannot create an endpoint without first creating a collaboration platform. Although a single platform instance is sufficient for most applications, it is possible to create multiple instances of the platform. Typically, this is useful for supporting Mutual TLS and TCP for connections. Creating more than a small number of platform instances is not recommended because memory consumption increases significantly with each instance.</p>
<p>There are two types of collaboration platform that are available. A client platform can be used for quick prototyping or to develop a tool to simulate a large number of client instances that target an application server. This is useful to measure the performance of an application server built using the platform. A server platform is typically used for a production application that might run on multiple servers. The server platform enables sharing of connections across multiple endpoints and enables the application to accept incoming connections from trusted sources. To create an application that is trusted by Skype for Business Server 2015, it is important to provision the application so that persistent information for the trust relationship is created in persistent stores such as the Active Directory or Central Management Store. A trusted application server can create application endpoints or user endpoints that are not challenge for user credentials.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/library/hh348915(v=office.16)">Conference</a></p></td>
<td><p>A conference represents an online meeting place for participants to discuss topics of interest, using one or more media types, such as instant messaging or audio-video. A conference organizer typically schedules a conference in advance by using a Web application or add-in tools for email programs such as Microsoft Outlook. An impromptu or <em>ad hoc</em> conference can also be created. A conference becomes active when the first participant joins the conference. Conference participants who are neither leaders nor the organizer are called attendees. A conference leader typically has more control over a conference than an attendee. For example, only leaders have the ability to mute all participants or admit or deny entrance to users who are in the conference lobby.</p></td>
</tr>
<tr class="even">
<td><p>Conference Leader</p></td>
<td><p>A conference participant who is able to can perform certain privileged operations on an activated conference. However, a conference leader cannot eject the conference organizer from the organizer’s own conference.</p>
<p>A pre-invited leader is a participant whom the conference organizer has designated as a leader, when the conference is being scheduled. Such a person becomes a leader when he or she joins an active conference.</p></td>
</tr>
<tr class="odd">
<td><p>Conference Lobby</p></td>
<td><p>A conference can be controlled by the organizer or presenter so that new users who join the conference are placed in a virtual Conference Lobby instead of being directly connected to the conference. The Leader has the ability to see those users in the lobby and can selectively admit certain users from the lobby into the conference, and deny admission to others.</p>
<p>Lobby participants are unable to admit themselves into the conference or perform conference commands such as mute, unmute, lock, unlock, and others.</p></td>
</tr>
<tr class="even">
<td><p>Conference Organizer</p></td>
<td><p>The person who schedules the conference. A conference organizer always joins his or her conference as a conference leader. A conference can be cancelled or modified only by its organizer.</p></td>
</tr>
<tr class="odd">
<td><p>ConferenceServices</p></td>
<td><p>The <a href="https://msdn.microsoft.com/library/hh348907(v=office.16)">ConferenceServices</a> class offers a variety of services related to the management of conferences before they become active. Users can create a scheduled conference, modify an existing conference before it becomes active, or delete a previously scheduled conference.</p></td>
</tr>
<tr class="even">
<td><p>ConferenceSession</p></td>
<td><p>The <a href="https://msdn.microsoft.com/library/hh349315(v=office.16)">ConferenceSession</a> class can be used for joining either an <em>ad hoc</em> conference or a scheduled conference. The conference is hosted by the Focus, which is responsible for enforcing conference policies as specified by the conference organizer. The conference session allows a user to perform a variety of operations within a conference to change the characteristics of an active conference. For example, the leader of the conference can eject a participant or can lock the conference to prevent new participants from joining it. Skype for Business Server 2015 uses the Centralized Conferencing Control Protocol (C3P) for implementing the primitives needed for the conference.</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/library/hh381065(v=office.16)">Contact</a></p></td>
<td><p>A contact is usually another user who has the ability to use real-time communications, but can also be an application, such as a &quot;bot&quot; or a Help Desk service.</p>
<p>Users of real-time communications often maintain a list of &quot;buddies&quot; (or contacts in Skype for Business Server 2015 terminology) with whom they frequently communicate. Every contact has a uniquely identifiable SIP URI and display name, as well as other attributes.</p>
<p>The contact list can exist in different stores such as email servers or Communications Server backend stores. UCMA provides an API for managing contacts that are stored in the Skype for Business Server 2015 backend stores. An application written using UCMA can use other mechanisms as well, if necessary.</p>
<p>An application can organize its contacts into contact groups, and usually subscribes to the presence (the online state of the contact) of users in a contact list.</p>
<p>A contact is different from an Active Directory contact object.</p></td>
</tr>
<tr class="even">
<td><p>Contact Group</p></td>
<td><p>A contact group is a logical group of contacts controlled by the user.</p></td>
</tr>
<tr class="odd">
<td><p>ContactGroupServices</p></td>
<td><p>The <a href="https://msdn.microsoft.com/library/hh381099(v=office.16)">ContactGroupServices</a> class contains methods that can be used to add, delete, or update contacts and groups. The class is available only to <strong>UserEndpoint</strong> instances. It is not available to <strong>ApplicationEndpoint</strong> instances.</p></td>
</tr>
<tr class="even">
<td><p>Conversation</p></td>
<td><p>A conversation represents a multi-party, multi-modal communication context between a local participant and one or more remote participants. A conversation involving only one remote participant is called a two-party conversation. The <a href="https://msdn.microsoft.com/library/hh349224(v=office.16)">Conversation</a> class manages and coordinates between components such as calls, the conference session, MCU sessions, and media providers that are needed for the communication experience. A conversation that involves more than two participants involves a conference session. A two-party conversation is normally escalated into a conference when it is desired to add a new participant or a new modality that requires the services of an MCU.</p></td>
</tr>
<tr class="odd">
<td><p>Contact Object</p></td>
<td><p>An Active Directory contact object represents a communication and collaboration-enabled service. Similar to a user object, an Active Directory contact object is assigned a SIP URI, Display Name, and telephone number, and belongs to an Skype for Business Server 2015 pool.</p>
<p>An application can also have a dedicated SIP URI assigned to it based on an Active Directory contact object entry for the application, similar to an end user having a SIP URI in the Active Directory user object that represents the user. Note that an Active Directory contact object is unrelated to a contact.</p></td>
</tr>
<tr class="even">
<td><p>ConversationParticipant</p></td>
<td><p>A conversation participant represents an information worker or a communication-enabled service (local or remote) engaged in a conversation. It exposes properties of a participant such as the URI, network source, phone URI, and endpoints of the participant in the conversation.</p></td>
</tr>
<tr class="odd">
<td><p>DNS Load Balancing</p></td>
<td><p>Hardware load balancers are typically used to balance the load among a set of computers that are configured behind the load balancer. Because configuring hardware load balancers can be difficult at times, the UCMA platform supports Domain Name System (DNS) load balancing to avoid the use of hardware load balancers. In DNS load balancing, a set of IP addresses are assigned to a specific fully-qualified domain name (FQDN). The UCMA platform distributes all calls to the given FQDN among the various computers who share that FQDN (but have different IP addresses).</p></td>
</tr>
<tr class="even">
<td><p><strong>DTMF (Dual-Tone Multi-Frequency)</strong></p></td>
<td><p>Tones produced by touching keys on a phone. IVR applications may allow a caller to provide information by speaking or by entering touch tones, or both.</p></td>
</tr>
<tr class="odd">
<td><p>Enhanced Presence</p></td>
<td><p>The presence information as represented by category instances supported by Skype for Business Server 2015. The enhanced presence data model provides the presence publisher with a more flexible and more granular control of how presence can be tracked by other users.</p></td>
</tr>
<tr class="even">
<td><p>Enhanced Presence Schema</p></td>
<td><p>An XML schema that defines the structure of enhanced presence data represented by the value of a category instance as XML elements and attributes. The schema helps an application create, parse, and validate the enhanced presence data before publication, or after receiving a fresh set of information as the result of a subscription.</p></td>
</tr>
<tr class="odd">
<td><p>Focus</p></td>
<td><p>In Skype for Business Server 2015, the Focus represents an active conference. The Focus is created when the first user joins the conference. The Focus is responsible for authenticating users, supplying the roster, and enabling indirect communication with Multipoint Controller Units (MCUs) that support different media types, such as an AudioVideo MCU, an InstantMessaging MCU, or a Data MCU. The Focus is responsible for ensuring that conference policies and settings are followed by all participants of a conference.</p></td>
</tr>
<tr class="even">
<td><p>Focus Factory</p></td>
<td><p>In Skype for Business Server 2015, the Focus Factory is a component that offers conference creation, conference deletion, and conference update services for users. The <strong>ConferenceServices</strong> class communicates with the Focus Factory to offer its services.</p></td>
</tr>
<tr class="odd">
<td><p>GRUU</p></td>
<td><p>A Globally Routable Unique URI (GRUU) represents a URI that can be used to uniquely address an endpoint or an application process in the Skype for Business Server 2015 infrastructure. When an endpoint registers with Skype for Business Server 2015, it is dynamically assigned a GRUU (a registered GRUU) that can be used to route messages to that specific endpoint.</p>
<p>An application that is provisioned so that it is trusted by Skype for Business Server 2015 also is assigned a static GRUU in the back end storage used by the server. This static GRUU uniquely addresses a specific process on a machine. A message that is routed by Skype for Business Server 2015 to the process using this static GRUU can have additional information that identifies an endpoint that is hosted in that process. The UCMA platform uses this additional information to route the message to that specific endpoint. If the additional information is not present, special rules are used to route the message to an endpoint, if possible. For example, if an application endpoint is designated as the default endpoint (by a value of true in its <a href="https://msdn.microsoft.com/library/hh350100(v=office.16)">IsDefaultRoutingEndpoint</a> property), messages without the additional endpoint-specific information are routed to this endpoint.</p>
<p>If an endpoint is registered and has a registered GRUU assigned to it, and the platform has a static GRUU assigned to it, the registered GRUU is used only for operations related to presence publication. The static GRUU is used for all other operations.</p></td>
</tr>
<tr class="even">
<td><p>Grammar</p></td>
<td><p>A list of words or phrases that an application will recognize. A VoiceXML application might use multiple grammars, each of which defines what a user can say for a portion of a call. Other grammars might be active across multiple portions of a call.</p>
<p>A grammar can assign meanings to words, typically assigning the same meaning to multiple words. For example, the spoken words &quot;Yes&quot;, &quot;Yup&quot;, &quot;Yeah&quot;, &quot;You Bet&quot;, and &quot;Affirmative&quot; can all be given the meaning &quot;Yes&quot;.</p>
<p>UCMA 5.0 supports XML-format grammars that conform to the <a href="https://www.w3.org/tr/speech-grammar/">Speech Recognition Grammar Specification (SRGS) Version 1.0</a>. Grammars may be included inline in a VoiceXML document, or may be separate files that the VoiceXML document references with links.</p></td>
</tr>
<tr class="odd">
<td><p>Host Application</p></td>
<td><p>A .NET application that contains a <strong>Browser</strong> and can process the markup language in VoiceXML documents.</p></td>
</tr>
<tr class="even">
<td><p>LocalEndpoint</p></td>
<td><p>An endpoint represents the basic communication unit needed by a user or application to perform any meaningful operations in the . An endpoint is created in the context of a collaboration platform. The <a href="https://msdn.microsoft.com/library/hh348819(v=office.16)">UserEndpoint</a> and <strong>ApplicationEndpoint</strong> classes are two subclasses of the <strong>LocalEndpoint</strong> class.</p></td>
</tr>
<tr class="odd">
<td><p>LocalOwnerPresence</p></td>
<td><p>The <a href="https://msdn.microsoft.com/library/hh382370(v=office.16)">LocalOwnerPresence</a> class can be used to manage the presence of the local (self) presentity. A presentity can use this class to publish presence categories, subscribe to self presence notifications, and assign and manage access control lists.</p></td>
</tr>
<tr class="even">
<td><p>McuSession</p></td>
<td><p>A multipoint controller unit (MCU) is a server component that offers media specific services in a conference. For example, an AudioVideo MCU is useful for mixing audio of the participants in a conference. In the UCMA 5.0 SDK, the <a href="https://msdn.microsoft.com/library/hh384975(v=office.16)">McuSession</a> class is the base class that provides services to control an MCU. The <a href="https://msdn.microsoft.com/library/hh382004(v=office.16)">InstantMessagingMcuSession</a> and <a href="https://msdn.microsoft.com/library/hh385298(v=office.16)">AudioVideoMcuSession</a> classes are concrete platform implementations that extend the <strong>McuSession</strong> class. MCU-specific commands such as ejecting a user from a specific MCU or requesting the MCU to send an invitation to a specific user (dial-out) can be issued using this class. An <strong>McuSession</strong> subclass instance cannot exist without a supporting <strong>ConferenceSession</strong> instance because communication with the MCU happens through the Focus.</p></td>
</tr>
<tr class="odd">
<td><p>MediaFlow</p></td>
<td><p>The <a href="https://msdn.microsoft.com/library/hh366262(v=office.16)">MediaFlow</a> abstract class represents a media channel that is created as a result of offer/answer exchange in a call. A media flow is created and managed by a media provider that is bound to a conversation. When a media flow is created, an application can configure the flow settings that are used when it generates the SDP needed for the invitation. The <a href="https://msdn.microsoft.com/library/hh383312(v=office.16)">InstantMessagingFlow</a> and <a href="https://msdn.microsoft.com/library/hh383533(v=office.16)">AudioVideoFlow</a> classes are platform-supplied concrete implementations of a media flow.</p>
<p>The <strong>Call</strong>, <a href="https://msdn.microsoft.com/library/hh383767(v=office.16)">MediaProvider</a>, and <a href="https://msdn.microsoft.com/library/hh366262(v=office.16)">MediaFlow</a> abstract classes are the building blocks of the communication framework. A developer can create classes that are derived from these abstract classes to extend the platform, to establish a media channel between two endpoints for arbitrary media types.</p>
<p>When a call is used to provide back-to-back services (also called third-party call control, or 3PCC), the provider bound to the call does not create the <strong>MediaFlow</strong>, because the media does not flow through the local endpoint.</p></td>
</tr>
<tr class="even">
<td><p>MediaProvider</p></td>
<td><p>A media provider represents the media platform that offers support for a specific media type. The <strong>MediaProvider</strong> abstract class is the base class that must be derived from by media providers to extend media support for typical applications. The includes <strong>AudioVideoProvider</strong> and <strong>InstantMessagingProvider</strong> class implementations to enable the consumption of the audio and message media types. An application does not need to create or deal with provider instances directly. The media provider implementations built into the for audio and instant messaging are internal and cannot be accessed by the application.</p>
<p>The <strong>Call</strong>, <strong>MediaProvider</strong>, and <strong>MediaFlow</strong> abstract classes are the building blocks of the communication framework. A developer can create classes that are derived from these abstract classes to extend the UCMA 5.0 platform, to establish a media channel between two endpoints for arbitrary media types.</p></td>
</tr>
<tr class="odd">
<td><p>MediaProviderFactory</p></td>
<td><p>A media provider factory is used to create media providers. By default, the collaboration platform supports the creation of platform-supported media providers to handle calls. An application that is intended to support a custom media type should register a factory instance that is responsible for creating an appropriate <strong>MediaProvider</strong> type to handle the custom media type.</p></td>
</tr>
<tr class="even">
<td><p>Player</p></td>
<td><p>An audio device in UCMA 5.0 that plays a pre-recorded audio file to communicate with a caller.</p></td>
</tr>
<tr class="odd">
<td><p>Pool</p></td>
<td><p>A collection of one or more computers that are affiliated with a site and serve as an installation point for a service role. All computers in a pool are homogeneous in nature (the service roles installed on all of the computers in the cluster are identical). For example, a pool for Skype for Business Server 2015 can have a pool FQDN assigned to it that points to one or more Front End servers to enable load balancing.</p></td>
</tr>
<tr class="even">
<td><p>Presence Category</p></td>
<td><p>Provides the basic identification of the kind of data that is published. A category name is an identifier of the contract between publisher and subscriber. It identifies the nature of the published data, and implies an agreed-upon schema for how the data should be interpreted. Some of the predefined categories in Skype for Business are <strong>note</strong>, <strong>contactCard</strong>, <strong>calendarData</strong>, <strong>state</strong>, and <strong>device</strong>.</p>
<p>A presence category represents a type to capture specific Rich Presence Data. A number of categories are defined and used in the Skype for Business Server 2015 infrastructure. Some of the categories are Note, ContactCard, CalendarData, UserState, and DeviceState. Each category has metadata that is interpreted by Skype for Business Server 2015 and includes such data as Category Name, expiry time, and instance ID. In addition, a category instance carries actual data for that category that is used and interpreted by some server components and clients. Each category has well defined XML schema and semantics associated with the various fields.</p></td>
</tr>
<tr class="odd">
<td><p>Presence Category Expiry Policy</p></td>
<td><p>Indicates the lifetime of a category publication. The publication lasts until it is unpublished.</p></td>
</tr>
<tr class="even">
<td><p>Presence Category Instance</p></td>
<td><p>Enables the publication of the same category of data multiple times by the same publisher.</p>
<p>It is sometimes desirable to publish multiple instances of a specific presence category from one or more endpoints. Each publication carries a presence category instance that uniquely identifies the specific instance that is published. When a single instance across all of a user’s endpoints is to be published, a predefined instance number (such as 0) can be used for the publication. For example, an offline presence note is published by all endpoints using a specific instance ID.</p></td>
</tr>
<tr class="odd">
<td><p>Presence Category Version</p></td>
<td><p>Allows multiple clients that publish the same category instance to synchronize themselves. Each published item has a server-maintained version number. The publication is updated only if the publisher presents the current version number. The version number is then incremented atomically with the data update by the server.</p></td>
</tr>
<tr class="even">
<td><p>Presence Container</p></td>
<td><p>A data structure that holds a set of published items and a membership list describing who can see those items. A presence container allows a publisher to publish different data values of the same category and instance for the purpose of allowing different subscribers to see different values. Containers are identified by container IDs. A set of well-known containers is defined for use by the Skype for Business Server 2015 infrastructure.</p></td>
</tr>
<tr class="odd">
<td><p>Presence Document</p></td>
<td><p>The XML data that is defined by a schema and represented by the document object model (DOM). The presence document is used in the publication and subscription of enhanced presence.</p></td>
</tr>
<tr class="even">
<td><p>Presence Information Document Format (PIDF)</p></td>
<td><p>This commonly used data format represents presence based on the rules specified in RFC 3863, and is used by Skype for Business Server 2015 when presence notifications are delivered to a PIC user.</p></td>
</tr>
<tr class="odd">
<td><p>Presence Publication</p></td>
<td><p>A user publishes his or her presence data to inform other users that he or she is willing, available, or even able to communicate or collaborate with them. With Skype for Business Server 2015, presence publication can be thought of as placing specified category instances in the appropriate category containers, at a specified membership scope.</p></td>
</tr>
<tr class="even">
<td><p>Presence View</p></td>
<td><p>The <a href="https://msdn.microsoft.com/library/hh381152(v=office.16)">RemotePresenceView</a> class can be used to subscribe to remote presentities either persistently or through polling at possibly different intervals. Using this class, and application can obtain up-to-date presence information as often as it specifies. For example, the Contacts list should be a persistent subscription, updated immediately. In contrast, a conference call with two other participants does not need immediate update information, and can be set to poll every five minutes. The <strong>RemotePresenceView</strong> class is an alternative to the <a href="https://msdn.microsoft.com/library/hh349758(v=office.16)">RemotePresence</a> class.</p>
<p>The <strong>RemotePresenceViews</strong> maintains a list of active <strong>RemotePresenceView</strong> objects.</p></td>
</tr>
<tr class="odd">
<td><p>Presentity</p></td>
<td><p>An application or user whose presence information can be tracked. A presentity registers its status, location, and other attributes with Skype for Business Server 2015.</p></td>
</tr>
<tr class="even">
<td><p>Public Internet Cloud (PIC) User</p></td>
<td><p>A user on a public internet cloud service provider, such as MSN, AOL, or Yahoo!.</p></td>
</tr>
<tr class="odd">
<td><p>Publisher</p></td>
<td><p>A user who publishes his or her presence for the use of other users who subscribe to it. The enhanced presence model allows a publisher to publish presence in a more flexible way to control how the specified presence data can be received by different subscribers.</p></td>
</tr>
<tr class="even">
<td><p>Recorder</p></td>
<td><p>An audio device in UCMA 5.0 that captures audio and records it to a file.</p></td>
</tr>
<tr class="odd">
<td><p>Session</p></td>
<td><p>Represents the contents of a phone call between a single user and an IVR application. A session begins when the hosting .NET application launches a <strong>Browser</strong> instance to process the documents in a VoiceXML application in response to an incoming phone call. A session ends when the <strong>Browser</strong> exits a VoiceXML document.</p></td>
</tr>
<tr class="even">
<td><p>Session Description Protocol (SDP)</p></td>
<td><p>An RFC-based protocol that describes the offer/answer payload format for an INVITE-based session to indicate a variety of information such as the media types for the session, the codec formats that are supported, the bandwidth information, and security properties. For more information, see RFC 4566.</p></td>
</tr>
<tr class="odd">
<td><p>Session Initiation Protocol (SIP)</p></td>
<td><p>An RFC-based protocol for real-time communications, supported by Skype for Business and Skype for Business Server 2015. The platform supports SIP-based message abstractions. For more information, see RFC 3261.</p></td>
</tr>
<tr class="even">
<td><p>Speech Recognition</p></td>
<td><p>The conversion of spoken words into text. UCMA 5.0 uses the speech recognition engines in Microsoft.Speech to match speech or keypad tones from callers to lists of acceptable input. Lists of acceptable input are contained in a grammar, see above. Speech recognition is also referred to as SR or ASR (automatic speech recognition).</p></td>
</tr>
<tr class="odd">
<td><p>Speech Synthesis</p></td>
<td><p>The conversion of text to computer-generated spoken words. Also known as text-to-speech (TTS). UCMA 5.0 uses the speech synthesis engines in Microsoft.Speech to speak text as instructed by a VoiceXML application. Speech Synthesis is also known as text-to-speech (TTS). An instruction to speak text in a VoiceXML application is also known as a prompt.</p></td>
</tr>
<tr class="even">
<td><p>Subscriber</p></td>
<td><p>A presentity who subscribes to presence data published by another presentity. This documentation uses subscriber and watcher interchangeably.</p></td>
</tr>
<tr class="odd">
<td><p>Subscription</p></td>
<td><p>A long-lived session in which a presentity receives published presence data that the presentity has subscribed to. A presentity can elect to subscribe to its own presence data or to the presence data of remote presentities.</p></td>
</tr>
<tr class="even">
<td><p><strong>Tone Controller</strong></p></td>
<td><p>An audio device in UCMA 5.0 that can receive or send telephone keypad tones during a call.</p></td>
</tr>
<tr class="odd">
<td><p>Uniform Resource Identifier (URI)</p></td>
<td><p>A URI represents an identity across multiple devices, either of an application or a user. URIs are used for routing in the Skype for Business Server 2015 network. A SIP URI uses the <strong>sip</strong> scheme and a telephony URI uses the <strong>tel</strong> scheme. The supports both SIP and telephony URIs. A URI can also be used to refer to a specific device instance in advanced scenarios.</p></td>
</tr>
<tr class="even">
<td><p>UserEndpoint</p></td>
<td><p>A <a href="https://msdn.microsoft.com/library/hh348819(v=office.16)">UserEndpoint</a> primarily represents an endpoint for a user with a SIP identity. This endpoint type enables an Information Worker to manage his or her contacts and groups, share his or her presence status, subscribe to the presence of others, schedule conferences, and communicate with others. An application can use a <strong>UserEndpoint</strong> instance to emulate an end user or to emulate large numbers of test clients in order to stress test an application built using the platform. A <strong>UserEndpoint</strong> instance must be used in conjunction with Skype for Business Server 2015, because it depends on a variety of features and protocols supported by Communications Server.</p>
<p>Unlike an <strong>ApplicationEndpoint</strong>, a UserEndpoint is not automatically trusted, and must register with Skype for Business Server 2015 components. This registration requirement has an impact on the reliability of the <strong>UserEndpoint</strong> in some scenarios. For more information, see the <strong>ApplicationEndpoint</strong> entry in this glossary.</p></td>
</tr>
<tr class="odd">
<td><p><strong>VoiceXML Document</strong></p></td>
<td><p>An XML-format file that contains markup language that conforms to the VoiceXML standard. A VoiceXML document is also called a VoiceXML page, and both terms are used in this documentation. For more information about VoiceXML, see <a href="https://www.w3.org/tr/2004/rec-voicexml20-20040316/">VoiceXML 2.0</a>.</p></td>
</tr>
<tr class="even">
<td><p><strong>VoiceXML Interpreter</strong></p></td>
<td><p>Interprets the markup language in VoiceXML documents and passes the instructions in the markup language to the Browser, which transacts the actual communication with the caller.</p></td>
</tr>
<tr class="odd">
<td><p>Watcher</p></td>
<td><p>A presentity who subscribes to presence data published by another presentity. This documentation uses subscriber and watcher interchangeably.</p></td>
</tr>
</tbody>
</table>


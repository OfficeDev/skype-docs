# Key programming concepts

Trusted Application is modelled on the general principles, capabilities, API style, and API concepts of the Skype for Business [Unified Communications Web API (UCWA)](https://ucwa.skype.com). The [UCWA Key Programming Concepts](https://ucwa.skype.com/documentation/key-programming-concepts) gives you 
detailed information about RESTful programming concepts. You should be familiar with these UCWA concepts to greatly simplify learning the **Trusted Application API**.

This topic introduces the following new **Trusted Application API** concepts:


## Discovery for Service Applications

The discovery flow that Service Applications (SA) built on the **Trusted Application API** use is different from the UCWA autodiscover flow. A Service Application uses a standard URL for discovery - `https://api.skypeforbusiness.com/platformservice/discover`.
A GET on this url with a valid Oauth token returns a **[service:applications](https://ucwa.skype.com/trustedapplicationapi/Resources/service_applications.html)** resource, which is the starting point for the Service Application scenarios.

You can learn more about the **Trusted Application API** discovery flow at [Discovery for Service Applications](./DiscoveryForServiceApplications.md)

## Discovery for anonymous clients

Anonymous clients follow a different discovery flow. The anonymous client discovery flow starts when the client requests a discovery link from an SA. The SA requests the link using the **Trusted Application API**. The API response is returned to the client as an [**service:discover**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_discover.html) link. 

The anonymous client issues a GET on the discovery link and an **[anonApplications](https://ucwa.skype.com/trustedapplicationapi/Resources/anonApplications.html)** resource is returned. This resource is the starting point for all anonymous clients scenarios.

You can get the details of how an SA gets the **[anonApplications](https://ucwa.skype.com/trustedapplicationapi/Resources/anonApplications.html)** resource from the API: [Anonymous Meeting Join](./AnonymousMeetingJoin.md)



## Anonymous Application Tokens:
An SA gets an Anonymous Applications Token and a discover link ([**service:discover**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_discover.html)) and shares these objects with an anonymous client so that the client can send chat invitations and messages.

The anonymous client uses the following pattern to send messages:

1. Follows the discover link supplied by the SA.
1. Gets the **anonApplications** link from the discovery response.
1. Sends a GET request on the **anonApplications** link
1. Gets the [**service:anonApplicationTokens**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_anonApplicationTokens.html) resource in the response to the previous step.
1. Sends a **Trusted Application API** request to the **anonApplications** link that includes the token in a request header.


## Adhoc Meeting:

An adhoc meeting is also known as an "on demand meeting". It is a temporarily generated meeting with a limited expiration.  You can create an adhoc meeting by a POST on [**service:adhocMeetings**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_adhocMeetings.html). 
The [**service:adhocMeetings**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_adhocMeetings.html) resource is returned when you do a GET on the url returned after the Discovery step.

Default expiration time: 8 hours.  


## Start Adhoc Meeting:

When a SA wants to create an adhoc (aka on demand) meeting and join it as a trusted participant who has full access to all info pertaining to the meeting, it can POST on this resource [**service:adhocMeetings**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_adhocMeetings.html). This avoids a two-step process of creating and then joining a meeting.

This link is available on the [**service:messagingInvitation**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_messagingInvitation.html) event to allow creating and joining an adhoc meeting, and later bridging the messaging leg into a conference.



## Conversation Bridge:

When the API has to bridge communications between a peer to peer call and a conference, the Service Application creates a conversation bridge entity to connect the conference with the peer to peer call.  To let SfB Online users send messages to the peer to peer call leg, a Service Application adds the users as bridged participants to the conversation bridge. 

>Note: If users are not bridged, the peer to peer call leg will not be able to see messages from those users. 

There are scenarios where it useful to avoid bridging certain conversation participants. For example, in a contact center supervisor scenario, your app should not bridge the supervisor, so a customer does not see the supervisor's messages


Conceptually, an Conversation Bridge is similar Back-to-back Agent in UCMA, with some key differences:

- A [Conversation Bridge](https://ucwa.skype.com/trustedapplicationapi/Resources/service_conversationBridge.html) can only connect a P2P call leg to a conference, unlike a Back-to-back agent which can connect two P2P call legs

- An Conversation Bridge (current release IM only) in the future can include multiple conversation modalities, and both Audio/Video and IM.

- A UCMA B2B User Agent can only connect 1 modality and only supports audio/video


## Message Filter:

Within a conversation bridge, the SA can allow or disallow sending messages from a participant on the conference leg. When disallowed, messages are not sent to the peer to peer leg where the peer to peer recipient is an anonymous user. Setting the filter state to **disabled** allows messages to be sent from the bridged conference participant to the peer to peer client.  Setting the message filter State to **enabled** prevents message sending from the bridged conference participant to the peer to peer client.  

Message Filter State should be set for each [bridged participant](https://ucwa.skype.com/trustedapplicationapi/Resources/service_bridgedParticipants.html) that has been added to a conversation.  If Message Filter State is not added for the participant, by default messages will not be bridged to the peer to peer client. When adding bridged participant it is REQUIRED to also set display name for the participant in the bridge.  An anonymous user on the peer to peer leg will see the display name of the sender (bridged participant) when he receives a message.



## Accept and Bridge:

In order to create a Conversation Bridge (when the Service Application wants to bridge an incoming invitation with an existing conference) it can use this [**Accept and Bridge**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_acceptAndBridge.html) link (this release includes IM only). This link is available on the incoming invitation and takes in a conference id as a request parameter.



## Callback Url:

Callback functionality is implemented using [Webhooks](./Webhooks.md). 


## Programming tips

When coding with the Trusted Application , you should keep the following tips in mind:

>Unless otherwise mentioned, all call flows shown in the documentation require an Oauth token to be sent in every request made to the API.

>The API can return relative paths in the links returned in the response. When this happens the application is expected to use the same base FQDN for these links, as the base FQDN for that request.

>Capabilities enabled for a Service Application, depend on the Application Permissions that your Service Application selected for Skype for Business Online in the Azure management portal.

## In this section

- [Webhooks](./Webhooks.md) 


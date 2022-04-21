---
title: session
description: Represents a session in a call.
---

# session

 _**Applies to:** Skype for Business 2015_


Represents a session in a call. 
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

### Properties



|**Name**|**Description**|
|:-----|:-----|
|remoteEndpoint|The remote endpoint of a session.|
|sessionContext|The context of the session.|
|state|The state of the session.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|applicationSharing|Represents the application sharing modality in the corresponding [conversation](conversation_ref.md).|
|audioVideo|Represents the audio/video modality in the corresponding [conversation](conversation_ref.md).|
|conversation|Represents the local participants perspective on a multi-modal, multi-party communication.|
|dataCollaboration|Represents the data collaboration modality in the corresponding [conversation](conversation_ref.md).|
|publishCallQualityFeedback|Represents publishCallQualityFeedback operation.|
|renegotiations|Represents the collection of renegotiations.|


### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.

|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

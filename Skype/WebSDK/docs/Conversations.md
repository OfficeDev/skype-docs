---
title: Conversations in the Skype Web SDK

---
# Conversations in the Skype Web SDK


 _**Applies to:** Skype for Business 2015_

## Conversations

The Skype Web SDK provides the following objects to start or join IM messaging, and to send and receive messages in a conversation.


- Use a [ConversationsManager](http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.conversationsmanager.html) object to start an outgoing conversation. For incoming invitations, the accept and reject actions are taken on the incoming [Conversation](http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.conversation.html) object.
    
- Use a [Conversation](http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.conversation.html) object to exchange messages, send and receive audio-visual content, and share applications in the conversation.
    
- Use the [Skype Web Conversation Control](PTUIControlsConversationControl.md) to host an IM conversation in your webpage. The **Conversation Control** encapsulates the model, view, and view model so that you only write code to host and render the control itself. Use the control when you want to let the Skype Web SDK provide the chat UI. If you use the conversation control, your webpage can host a complete IM dialog with as few as three Skype Web SDK API calls.
    
A conversation is a logical container for communication between two or more persons. The properties of a conversation include **selfParticipant**, **participants**, **historyService** in the conversation, and conversation services in use. Each of these objects is represented in Figure 1, and the relationship between conversation objects is represented by connectors in the figure. You can add participants to a conversation before it starts or at any time after starting. Adding participants to a 1-on-1 conversation will escalate that conversation to a multiparty meeting.


**Figure 1. The relationship between objects in the Skype Web SDK conversation model**

![SkypeWebSDK_ConvObjects](../images/7bb0af54-be7a-4c3b-a41c-516b8e7bcd04.png)
### Conversation types

A conversation can be thought of as two-dimensional, with the number of conversation services representing one dimension, and the number of participants representing the other.


- Single modal and multi-modal conversation: A single modal conversation is an Instant Message (IM) or an audio conversation, which is actually audio/video. A multi-modal conversation uses two media modalities: IM and audio/video.
    
- Two-party conversation: A two-party conversation is a peer-to-peer conversation. A multi-party conversation is escalated to a meeting when the third-party joins the conversation.
    

### Conversation services

Several conversation service types are supported by the SDK:


- [ChatService](http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.chatservice.html)  
    
- [AudioService](http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.audioservice.html)  
    
- [VideoService](http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.videoservice.html)  
    
- [HistoryService](http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.historyservice.html)  

### Starting a conversation

In short, starting a conversation takes three steps:

- Get a conversation object.
- Add participant objects.
- Start a chat/audio/video service or just send a message.

If just one participant object is added, the SDK starts a peer-to-peer (1:1) conversation. If no participants or a few participants are added, then the SDk starts a multi-party (1:N) conversation which is also known as an ad-hoc online meeting.

For the simplest case - starting a 1:1 conversation - use the `getConversation` method that creates new or _gets an existing_ 1:1 conversation with the specified person.

```js
conv = app.conversationsManager.getConversation("sip:johndoe@contoso.com");
conv.chatService.sendMessage("Hi");
```

If there was no conversation with this person, it will be created and the conversation object will have one item in the `participants` collection. The created participant object will generally have just one property set: `.person.id` which is the given SIP URI. Other properties will become available once the conversation is started.

Another way to get the same result is to use a person object.

```js
p = app.personsAndGroupsManager.all.persons(3);
conv = app.conversationsManager.getConversation(p);
conv.chatService.sendMessage("Hi");
```

In this case the created participant object will use the given person object that may have all the properties filled with data. When starting the conversation, the SDK will do a `p.id.get()` to get the SIP URI, which will be a noop if the SIP URI was already available.

After a 1:1 conversation is started, more participants can be added to turn the peer-to-peer conversation into a multi-party one:

```js
conv = app.conversationsManager.getConversation("sip:johndoe@contoso.com");
conv.chatService.sendMessage("Hi").then(() => {
  // joesmith will get an invitation to join the ad-hoc meeting
  conv.participants.add("sip:joesmith@contoso.com");
});
```

Participants can also be removed from the conversation. However removing all the participants except one from a multiparty conversation doesn't convert it back into a peer-to-peer one: the conversation remains an onlinead-hoc meeting with two participants (the local participant and the remote participant).

```js
pt0 = conv.participants(0)
conv.participants.remove(pt0).catch(err => {
  console.log("looks like the current user doesn't have permissions to remove participants:", err);
});
```

It's possible to create an empty ad-hoc meeting and invite participants later.

```js
conv = app.conversationsManager.createConversation();
conv.chatService.start(); // sendMessage also works
```

Note, that the chat service was used as an example. It's possible to start audio or video service to start conversations.

Before a conversation is started, it's possible to set the subject/topic of that conversation. After the conversation is started, its topic cannot be changed.

```js
conv = app.conversationsManager.createConversation();
conv.topic.set("ABC");
conv.chatService.start(); // sendMessage also works
```

### Ending a conversation

This is as simple as invoking the `leave` method:

```js
conversation.leave();
```

### Sending context as part of conversation invites

In certain scenarios your application may want to send some additional context as part of a conversation invite. For instance, if both the sender and recipient are applications written by you on top of the Skype Web SDK then the conversation initiator may want to indicate to the recipient that an incoming conversation be handled in some special way. This can be accomplished by passing contextual data as part of an invite:

#### e.g. For the chat service:
```js
conversation.chatService.start({
  .. other parameters
  context: 'Some random data',
  contextType: 'text/plain'
});
```
#### e.g. For the audio service:
```js
conversation.audioService.start({
  .. other parameters
  context: { "key1": "value1" },
  contextType: 'text/json'
});
```
#### Description of the parameters:
* `context` : Strings, numbers, booleans or objects. Objects are JSON stringified before sending over the wire.
* `contextType`: This is an optional parameter indicating to the recipient the mime.type of the context parameter. If contextType is unspecified then the SDK uses text/plain for primitive types or text/json for anything else.

#### Obtaining the context on incoming conversations:
The recipient can use the following approach to get the context and contextType sent by the sender.
```js
app.conversationsManager.conversations.added(function(conversation) {
  var context = conversation.context();
  var contextType = conversation.context && conversation.context.type();
});
```
    
## See also


- [Start a conversation](StartConversation.md)
- [Respond to a conversation invitation](RespondToInvitation.md)
- [Add participants to a conversation](AddParticipants.md)
- [Use the Skype Web Conversation Control in a webpage](PTUIControlsConversationControl.md)

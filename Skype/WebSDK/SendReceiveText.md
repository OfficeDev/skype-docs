
# Send and receive text in a conversation


 _**Applies to:** Skype for Business 2015_

## Sending and receiving a message in a conversation

Receiving messages in a conversation involves getting a conversation object and conversation instant message modality. When the conversation is connected, listen for new messages. When a message is received, get the sender name and message text.


### Receive a message


1. Get the conversation history service.

  ```js
  var historyService = conversation.historyService;
  ```

2. Listen for new incoming messages.


  ```js
  historyService.activityItems.added(function (message) {…});
  ```

3. Determine the type and direction of the activity item.


  ```js
  if (item.type() == 'TextMessage') { …}
  ```


  ```js
  if (item.direction() == 'Incoming') { … }
  ```

4. Get the message sender's name.
    
    The displayName property contains the display name of the sender. If the display name is not available, the sender's id value is returned. Otherwise, the example returns 'Unknown participant'.


  ```js
  item.sender.person.displayName() || item.sender.person.id() || 'Unknown participant';
  ```

5. Get the message text.


  ```js
  item.text();
  ```


    The message can also be sent as an HTML-formatted message.
    


  ```js
  item.html();
  ```




### Send a message

Sending a message involves getting a connected conversation and the conversation chat service. When the user has provided text for a message, send the message on the chatService of the conversation.

1. Get the conversation chat service.


  ```js
  var chatService = conversation.chatService;
  ```

2. Listen for the conversation.SelfParticipant.Chat.state to change to "Connected."


  ```js
  conversation.selfParticipant.chat.state.when('Connected', function () {…});
  ```

3. Send message text.


  ```js
  chatService.sendMessage(text)
  ```

#### Examples

The following example puts the previous procedure steps together to show how to receive text messages.




```js
// add the IM modality
var historyService = conversation.historyService;
historyService.activityItems.added(function (item) {
    if (item.type() == 'TextMessage') {
        if (item.direction() == 'Incoming') {
            console.log('received a text message: ', item.text());
        } else if (item.direction() == 'Outgoing') {
            console.log('sent a text message: ', item.text());
        }
    }
});

```

The following example puts the previous procedure steps to together to show how to send a message.




```js
// get the chat service
var chatService = conversation.chatService;
// when the selfParticipant chat state becomes connected
conversation.selfParticipant.chat.state.when('Connected', function () {
    chatService.sendMessage(text);
});


```


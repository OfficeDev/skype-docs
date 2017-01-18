
# Incoming P2P/PSTN Call

 _**Applies to:** Skype for Business 2015_

[!INCLUDE[ChromeWarning](includes/P2PChromeWarning.md)]

## Listening for incoming call notifications

When a remote user starts a call we will receive an invitation to join the call.
In order to see the notification we need to:

1. Listen to the conversation collection for newly added conversations

    ```js
    application.conversationsManager.conversations.added(function (conversation) {
        // ...
    });
    ```

2. For every added conversation we need to observe the `audioService.accept.enabled` command.

    ```js
    conversation.audioService.accept.enabled.when(true, function () {
        // ....
    })
    ```

3. When the command becomes available we have received a notification. We can now prompt the user to accept ot decline the invitation.
When the user accepts, we execute `conversation.audioService.accept()`. When they reject `conversation.audioService.reject()` is executed.
```js
if (confirm('Accept incoming Audio invitation?')) {
    conversation.audioService.accept();
} else {
    conversation.audioService.reject();
}
```

## Conversation State
We can subscribe to the conversation state to get information about the overall state of the conversation.
If a conversation's state is `Connected`, it means that we are receiving live updates about state changes within the
conversation, and will receive updates when the state of any active modality in the conversation changes, or
when other participants connected to the conversation attempt to add or remove modalities. The conversation state
being `Connected` does not mean that any particular modality is active.

```js
conversation.state.changed(function (newValue, reason, oldValue) {
    //...
});
```

**Possible Conversation States:**

|||
|--------------|------------------------------------------|
| *Created* | ...When conversation was created
| *Connecting*    | ...When establishing a connection           |
| *Connected* | ...When the conversation was successfully connected |
| *Disconnected* | ...When the conversation got disconnected |

## Participants in Conversation
In case the invitation is accepted we should subscribe to the `participants` collection on the `conversation` object to be notified when new participants enter the conversation.

```js
conversation.participants.added(function (participant) {
    // ...
});
```

## Ending an Audio Call

There are 2 ways to end an audio call: either stop the audio modality by calling `conversation.audioService.stop()`
or leave the conversation entirely by calling `conversation.leave()`. If a modality other than audio, such
as chat, is active in the conversation, calling `conversation.leave()` will disconnect that as well and
cause the `conversation.state()` to become `Disconnected`. If you want to hang up audio in a call but remain
connected to the conversation by chat, call `conversation.audioService.stop()`.

```js
conversation.leave().then(function () {
    // successfully left the conversation
}, function (error) {
    // error
});

// OR

conversation.audioService.stop().then(function () {
    // successfully stopped audio
}, function (error) {
    console.log("Failed to stop audio: " + error);
});
```

## Complete Code Sample
Here is the code combined:

```js
conversationsManager.conversations.added(function (conversation) {
    conversation.audioService.accept.enabled.when(true, function () {
        if (confirm('Accept incoming Audio invitation?')) {
            conversation.audioService.accept();
            conversation.participants.added(function (participant) {
                console.log('Participant:', participant.displayName(), 'has been added to the conversation');
            });
        } else {
            conversation.audioService.reject();
        }
    });

    conversation.state.changed(function (newValue, reason, oldValue) {
        console.log('Conversation state changed from', oldValue, 'to', newValue);
    });
});
```

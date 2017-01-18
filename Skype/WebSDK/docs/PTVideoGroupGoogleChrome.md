
# Group Video in Google Chrome

 _**Applies to:** Skype for Business 2015_

The Skype Web SDK differentiates rendering of group videos for different browsers:
* Google Chrome
* and all other browsers

## Google Chrome

In Google Chrome, due to technical limitations, the video streams of the remote participants are transported over one channel. This 
channel is accessible through the **[Active Speaker API](http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.activespeaker.html)**.

The channel always containes the video of the currently speaking participant. This updates automatically 
when the currently speaking participant changes.

### Active Speaker API

#### How fo I know when to use the Active Speaker API?
The API is automatically initialised when loading the SDK in Google Chrome.
You can detect this by checking the value of `videoService.videoMode()`.

```js
var conversation = application.conversationsManager.createConversation();
if (conversation.videoService.videoMode() === 'ActiveSpeaker') {
    // use the Active Speaker API
}
```

#### How do I render the Active Speaker Channel?
```js
// set up listener to detect if someone is actively speaking
conversation.videoService.activeSpeaker.participant.changed(function (participant) {
    // participant.displayName() is speaking

    // lets render the video if the currently speaking participant is not myself
    if (participant !== conversation.selfParticipant) {
        var channel = conversation.videoService.activeSpeaker.channel;

        // add listener to turn video on/off
        channel.isVideoOn.changed(function (isVideoOn) {
            channel.source.sink.container(/* DOM node */);
            channel.isStarted(isVideoOn);
        });
    }
});
```
## All Other Browsers

In all other browsers one channel per participant is available to transport the video which means
that multiple remote videos can be displayed at the same time.

Please refer to [Group Video](PTVideoGroup.md) for more details.
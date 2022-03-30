---
title: Use the SDK to join a meeting with an iOS device
description: This article shows an iOS developer how to join the Skype for Business meeting using a meeting URL and enable core features like Text chat, Audio/Video chat in your app.
ms.date: 03/30/2022
---

# Use the SDK to join a meeting with an iOS device

This article shows an iOS developer how to join the **Skype for Business meeting** using a [**meeting URL**](/skype-sdk/appsdk/getmeetingurl.md) and enable core **Skype for Business App SDK** features like Text chat, Audio/Video chat in your app. 

Android developers should read [Use the SDK to join a meeting with an Android device](HowToJoinMeeting_Android.md).

> [!NOTE]
> If the anonymous meeting your app joins is hosted in a **Skype for Business Online** service and your app is not enabled for Skype for Business preview features, then your app must get a **discovery Url** and an **anonymous meeting token** to join. A meeting Url does not give you meeting access in this scenario. Read [Use the App SDK and the Trusted Application API to join an Online meeting - iOS](HowToJoinOnlineMeeting_iOS.md)

No **Skype for Business** credentials are used to join the meeting.

## Prerequisites

 **Objective C**

- **Import the SDK header file**: import the required header files.

 ```objective-c
        #import <SkypeForBusiness/SkypeForBusiness.h>
            
       //Import SfBConversationHelper classes for Audio/Video Chat
        #import "SfBConversationHelper.h"
 ```

**Swift**

- **Create Swift Bridging - Header file**: Create the bridging-header file and add the following import statement.

```swift
   //Add SfBConversationHelper classes for Audio/Video Chat
       #import "SfBConversationHelper.h"
```

> [!NOTE]
> Be sure to read [Getting started with Skype App SDK development](GettingStarted.md) to learn how to configure your iOS project for the **Skype for Business** App SDK.  In particular, the following steps assume you have added the _ConversationHelper_ class to your source to let you complete the scenario with a minimum of code.

## How to get started

1. In your code, initialize the **App SDK** application :

  **Objective C**

```objectivec 
 SfBApplication *sfb = SfBApplication.sharedApplication;
```

  **Swift**

```swift 
 let sfb:SfBApplication? = SfBApplication.sharedApplication()
```

2. You can handle application level Skype configurations like requireWifiForAudio, maxVideoChannels, requireWifiForVideo, setActiveCamera, get available cameras list  and other types of information that can impact the Skype session, for example, by default, video service will be disabled while not on Wi-Fi network. To allow video call on any network connection, we can configure requireWifiForVideo as follow:

  **Objective C**

```objectivec

 sfb.configurationManager.requireWifiForVideo = NO;
```

  **Swift**

```swift
 sfb.configurationManager.requireWifiForVideo = false
```

   > [!NOTE]
   > Please refer [SfBApplication](https://ucwa.skype.com/reference/appSDK/IOS/Classes/SfBApplication.html),  [SfBConfigurationManager](https://ucwa.skype.com/reference/appSDK/IOS/Classes/SfBConfigurationManager.html),  [SfBVideoService](https://ucwa.skype.com/reference/appSDK/IOS/Classes/SfBVideoService.html), [SfBDevicesManager]    (<https://ucwa.skype.com/reference/appSDK/IOS/Classes/SfBDevicesManager.html>) and other classes in [SkypeForBusiness] (<https://ucwa.skype.com/reference/appSDK/IOS/>) framework to handle application level Skype configurations.

3. Start joining the meeting by calling [_Application.joinMeetingAnonymously(String displayName, URI meetingUri)_](https://ucwa.skype.com/reference/appSDK/IOS/Classes/SfBApplication.html#//api/name/joinMeetingAnonymousWithUri:displayName:error:). This function returns the new conversation instance that represents the meeting.  

 **Objective C**

  ```objectivec

   SfBConversation *conversation = [sfb joinMeetingAnonymousWithUri:[NSURL URLWithString:meetingURLString]
                                        displayName:meetingDisplayName 
                                        error:&error];
  ```

 **Swift**

  ```swift
   let conversation: SfBConversation  = try sfb.joinMeetingAnonymousWithUri(NSURL(string:meetingURLString)!, displayName:  meetingDisplayName)
  ```

   > [!NOTE]
   > All of the SDK’s interfaces must be used only from the application main thread (main run loop).  Notifications are  delivered in the same thread as well. As a result, no synchronization around the SDK’s interfaces is required.  The SDK,  however, may create threads for internal purposes.

4. Initialize the conversation helper with the conversation instance obtained in the previous step and delegate object that should receive callbacks from this conversation helper. This will automatically start incoming and outgoing video. The delegate class must conform to _SfBConversationHelperDelegate_ protocol.
  
  **Objective C**

  ```objectivec

       if (conversation) {
       _conversationHelper = [[SfBConversationHelper alloc] initWithConversation:conversation
                                                     delegate:self
                                                     devicesManager:sfb.devicesManager
                                                     outgoingVideoView:self.selfVideoView
                                                     incomingVideoLayer:(CAEAGLLayer *) self.participantVideoView.layer
                                                     userInfo:@{DisplayNameInfo:meetingDisplayName}];
                                                     
      }
  ```
  
  **Swift**

  ```swift
   self.conversationHelper = SfBConversationHelper(conversation: conversation,
                                                            delegate: self,
                                                            devicesManager: sfb.devicesManager,
                                                            outgoingVideoView: self.selfVideoView,
                                                            incomingVideoLayer: self.participantVideoView.layer as! CAEAGLLayer,
                                                            userInfo: [DisplayNameInfo:meetingDisplayName])
  ```

5. Show video codec license

   As per the license terms, before you start video for the first time after install, you **must** prompt the user to accept the Microsoft end-user license (also included in the SDK).  

   This code snippet shows the use of the new Skype App SDK **"setEndUserAcceptedVideoLicense"** api. This is required to proceed with features that potentially use video codecs.

   Until this method is called, any attempt to use those features will fail. Once the api has been called, the user is considered in acceptance of the third party video codec license that we use to support video.  Subsequent meetings do not require the license acceptance.  

   [!code-Swift [sample](VideoLicense_iOS.md)]

6. Implement SfBConversationHelperDelegate methods to handle video service state changes.

  **Objective C**
   
   ```objectivec 

      #pragma mark - Skype Delegates
    
    // At incoming video, unhide the participant video view
    - (void)conversationHelper:(SfBConversationHelper *)avHelper didSubscribeToVideo:(SfBParticipantVideo *)video {
        self.participantVideoView.hidden = NO; 
    }

    // When it's ready, start the video service and show the outgoing video view.
    - (void)conversationHelper:(SfBConversationHelper *)avHelper videoService:(SfBVideoService *)videoService didChangeCanStart:(BOOL)canStart {
        if (canStart) {
            [videoService start:nil];
            
            if (self.selfVideoView.hidden) {
                self.selfVideoView.hidden = NO;
            }       
        } 
    }

    // When incoming video is ready, show it.
    - (void)conversationHelper:(SfBConversationHelper *)avHelper didSubscribeToVideo:(SfBParticipantVideo *)video {
        self.participantVideoView.hidden = NO; 
    }
  ```

  **Swift**

  ```swift
    // When it's ready, start the video service and show the outgoing video view.
    func conversationHelper(conversationHelper: SfBConversationHelper, videoService: SfBVideoService, didChangeCanStart canStart: Bool) {     
        if (canStart) {
            do {
                try videoService.start()
            }
            catch let error as NSError {
                print(error.localizedDescription)
                                
            }
            if (self.selfVideoView.hidden) {
                self.selfVideoView.hidden = false
            }
        }
    }
   
     
     //MARK - Skype SfBConversationHelperDelegate methods
     
     // At incoming video, unhide the participant video view
    func conversationHelper(conversationHelper: SfBConversationHelper, didSubscribeToVideo video: SfBParticipantVideo?) {
        self.participantVideoView.hidden = false
    }
    
    // When video service is ready to start, unhide self video view and start the service.
    func conversationHelper(conversationHelper: SfBConversationHelper, videoService: SfBVideoService, didChangeCanStart canStart: Bool) {
        
        if (canStart) {
            if (self.selfVideoView.hidden) {
                self.selfVideoView.hidden = false
            }
            do{
                try videoService.start()
            }
            catch let error as NSError {
                print(error.localizedDescription)
                                
            }
        }
    }
     // When the audio status changes, reflect in UI
    func conversationHelper(avHelper: SfBConversationHelper, selfAudio audio: SfBParticipantAudio, didChangeIsMuted isMuted: Bool) {
        if !isMuted {
            self.muteButton.setTitle("Unmute", forState: .Normal)
        }
        else {
            self.muteButton.setTitle("Mute", forState: .Normal)
        }
    }

  ```

7. To end the video meeting, monitor the _canLeave_ property of a conversation to prevent leaving prematurely.

   **Objective C**

   ```objectivec

        //Add observer to _canLeave_ property
    if (conversation) {
            [conversation addObserver:self forKeyPath:@"canLeave" options:NSKeyValueObservingOptionInitial | NSKeyValueObservingOptionNew context:nil];
    }

        // Monitor canLeave property of a conversation to prevent leaving prematurely

            - (void)observeValueForKeyPath:(NSString *)keyPath ofObject:(id)object change:(NSDictionary<NSString *,id> *)change context:(void *)context {

            if ([keyPath isEqualToString:@"canLeave"]) {
            self.endCallButton.enabled = _conversationHelper.conversation.canLeave;
        }
        }

        // Use SfBConversation class leave function to leave the conversation.
        NSError *error = nil;
        [_conversationHelper.conversation leave:&error];

        if (error) {
            [self handleError:error];
        }
        else {
            [_conversationHelper.conversation removeObserver:self forKeyPath:@"canLeave"];
        }
   ```

   **Swift**

   ```swift
        // Add observer to _canLeave_ property
            conversation.addObserver(self, forKeyPath: "canLeave", options: [.Initial, .New] , context: nil)
            
        // Monitor canLeave property of a conversation to prevent leaving prematurely
        
            override func observeValueForKeyPath(keyPath: String?, ofObject object: AnyObject?, change: [String : AnyObject]?, context: UnsafeMutablePointer<Void>) {
            if (keyPath == "canLeave") {
                self.endCallButton.enabled = (self.conversationHelper?.conversation.canLeave)!
            }
        }
        
        // Use SfBConversation class leave function to leave the conversation.
            do{
                try self.conversationHelper?.conversation.leave()
                self.conversationHelper?.conversation.removeObserver(self, forKeyPath: "canLeave")
            }
   ```

## Error handling

[SkypeForBusiness](https://ucwa.skype.com/reference/appSDK/IOS/) SDK API has both [_SfBApplication_](https://ucwa.skype.com/reference/appSDK/IOS/Classes/SfBApplication.html) and [_SfBConversation_](https://ucwa.skype.com/reference/appSDK/IOS/Classes/SfBConversation.html) level delegate method for handling possible errors or exceptions. The _SfBApplication_ alertDelegate handles global level concerns, while the _SfBConversation_ alertDelegate handles alerts specific to the conversation instance.

The delegate method _didReceiveAlert_ is called when new alert appears in the context where alertDelegate is attached.

## Text chat 

> [!NOTE] 

> ChatHandler is the helper class that can be used to integrate Skype text chat feature into your application. It can be integrated in similar manner to SfBConversationHelper. For more information, see [iOS sample apps](https://github.com/OfficeDev/skype-ios-app-sdk-samples).

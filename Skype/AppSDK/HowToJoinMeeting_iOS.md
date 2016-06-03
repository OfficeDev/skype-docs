# Use the SDK to join a meeting with an iOS device

This article shows an iOS developer how to join the **Skype for Business meeting** using a [**meeting URL**](https://msdn.microsoft.com/en-us/skype/appsdk/getmeetingurl) and enable messaging, audio and video in your app. Android developers should read [Use the SDK to join a meeting with an Android device](HowToJoinMeeting_Android.md). 

The SDK joins the meeting as a "guest".  No **Skype for Business** credentials are used.

##Prerequisites
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
 
  >**Note**: Be sure to read [Getting started with Skype App SDK development](GettingStarted.md) to learn how to configure your iOS project for the **Skype for Business** App SDK.  In particular, the following steps assume you have added the _SfBConversationHelper_ class to your source to let you complete the scenario with a minimum of code. 

## How to get started
1. In your code, initialize the **App SDK** application :

 **Objective C**
  ```Objectivec 
   SfBApplication *sfb = SfBApplication.sharedApplication;
   ```
 **Swift**
  ```swift 
   let sfb:SfBApplication? = SfBApplication.sharedApplication()
```
2. Set up any top-level configuration.  In particular, pay attention to the settings for _requireWifiForAudio_ and _requireWifiForVideo_.  By default, the SDK is configured to disable video when not on wifi.  If you want to allow video on any network connection, configure this as follows:

 **Objective C**
  ```Objectivec 
sfb.configurationManager.requireWifiForVideo = NO;
```
 **Swift**
  ```swift 
  sfb.configurationManager.requireWifiForVideo = false
  ```
 
3. Start joining the meeting by calling _Application.joinMeetingAnonymousWithUri(String displayName, URI meetingUri)_. This function returns the new conversation instance that represents the meeting.  

    **Objective C**
  ```Objectivec 
   SfBConversation *conversation = [sfb joinMeetingAnonymousWithUri:[NSURL URLWithString:meetingURLString]
                                        displayName:meetingDisplayName 
                                        error:&error];
  ```
    **Swift**
  ```Swift
   let conversation: SfBConversation  = try sfb.joinMeetingAnonymousWithUri(NSURL(string:meetingURLString)!, displayName: meetingDisplayName)
  ```
 > Note: all of the SDK’s interfaces must be used only from the application main thread (main run loop).  Notifications are delivered in the same thread as well.  As a result, no synchronization around the SDK’s interfaces is required.  The SDK, however, may create threads for internal purposes.      

4. Initialize the conversation helper with the conversation instance obtained in the previous step and delegate object that should receive callbacks from this conversation helper.  This will automatically start incoming and outgoing video. The delegate class must conform to _SfBConversationHelperDelegate_ protocol.  The following examples assume you have created views for incoming video (selfVideoView) and outgoing video (participantVideoView).  See the sample apps ([Objective C](https://github.com/OfficeDev/skype-ios-app-sdk-samples/tree/master/BankingAppObjectiveC), [Swift](https://github.com/OfficeDev/skype-ios-app-sdk-samples/tree/master/BankingAppSwift)) for examples of this.  If you only need messaging, take a look the _ChatHandler_ class in the sample apps ([Objective C](https://github.com/OfficeDev/skype-ios-app-sdk-samples/blob/master/BankingAppObjectiveC/BankingApp/ChatHandler.h), [Swift](https://github.com/OfficeDev/skype-ios-app-sdk-samples/blob/master/BankingAppSwift/BankingAppSwift/ChatHandler.h)).  This class is based on SfBConversationHelper, but it does not include audio and video support.
  
  **Objective C**
     ```Objectivec 
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
     ```Swift
   self.conversationHelper = SfBConversationHelper(conversation: conversation,
                                                            delegate: self,
                                                            devicesManager: sfb.devicesManager,
                                                            outgoingVideoView: self.selfVideoView,
                                                            incomingVideoLayer: self.participantVideoView.layer as! CAEAGLLayer,
                                                            userInfo: [DisplayNameInfo:meetingDisplayName])
```
  > Note: as per the license terms, before you start video for the first time after install, you must prompt the user to accept the Microsoft end-user license (also included in the SDK).  Subsequent versions of the SDK preview will include code to assist you in doing so.
      
5. Implement SfBConversationHelperDelegate methods to handle video and audio service state changes.

    **Objective C**
     ```Objectivec 
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

    // When incoming video is ready, show it.
    func conversationHelper(conversationHelper: SfBConversationHelper, didSubscribeToVideo video: SfBParticipantVideo?) {
        self.participantVideoView.hidden = false
    }
     ```
    >Note: For more information on _SfBConversationHelperDelegate_ methods, please refer _SfBConversationHelper_ class in SkypeForBusiness framework.
     
6. Now the meeting is established, you can use additional APIs to switch camera, mute/hold audio, pause video, send/receive messages, leave the meeting etc.  See the [API reference documentation](http://aka.ms/sfbAppSDKRef_iOS) for more details and the sample apps ([Objective C](https://github.com/OfficeDev/skype-ios-app-sdk-samples/tree/master/BankingAppObjectiveC), [Swift](https://github.com/OfficeDev/skype-ios-app-sdk-samples/tree/master/BankingAppSwift)) for examples of how to do this.  You may also want to adapt SfBConversationHelper for your own needs: its source code is available in the SDK download.
     
     
## Error Handling

SkypeForBusiness SDK API has both _SfBApplication_ and _SfBConversation_ level delegate method for handling possible errors or exceptions. The _SfBApplication_ alertDelegate handles global level concerns, while the _SfBConversation_ alertDelegate handles alerts specific to the conversation instance.

The delegate method _didReceiveAlert_ is called when new alert appears in the context where alertDelegate is attached.


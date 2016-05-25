# Use the SDK to join a meeting with an iOS device

This article shows an iOS developer how to join the **Skype for Business meeting** using a [**meeting URL**](https://msdn.microsoft.com/en-us/skype/appsdk/getmeetingurl) and enable core **Skype for Business App SDK** features like Text chat, Audio/Video chat in your app. Android developers should read
[Use the SDK to join a meeting with an Android device](HowToJoinMeeting_Android.md). 

 No **Skype for Business** credentials are used to join the meeting.

##Prerequisites
 **Objective C**
 1. **Import the SDK header file**: import the required header files.

      ```objective-c
             #import <SkypeForBusiness/SkypeForBusiness.h>
         //Import ChatHandler helper classes for text Chat
             #import "ChatHandler.h"
         //Import SfBConversationHelper classes for Audio/Video Chat
             #import "SfBConversationHelper.h"
      ```

**Swift**

1. **Create Swift Bridging - Header file**: Create the bridging-header file and add the following import statement as required.

      ```swift
        //Add ChatHandler helper classes for text Chat
            #import "ChatHandler.h"
        //Add SfBConversationHelper classes for Audio/Video Chat
            #import "SfBConversationHelper.h"
```
 


>Note: Be sure to read [Getting started with Skype App SDK development](GettingStarted.md) to learn how to configure your iOS project 
for the **Skype for Business** App SDK.  In particular, the following steps assume you have added the _ConversationHelper_ class
 to your source to let you complete the scenario with a minimum of code. 


1. In your code, initialize the **App SDK** application :

 **Objective C**
  ```Objectivec 
   SfBApplication *sfb = SfBApplication.sharedApplication;
   ```
 **Swift**
  ```swift 
   let sfb:SfBApplication? = SfBApplication.sharedApplication()
```

 > Note: Please refer SfBApplication, SfBConfigurationManager and SfBDevicesManager classes in SkypeForBusiness framework to handle application level Skype configurations like requireWifiForAudio, requireWifiForVideo, cameras list etc.

2. Start joining the meeting by calling _Application.joinMeetingAnonymously(String displayName, URI meetingUri)_. This function returns the new conversation instance that represents the meeting.  

  > Note: all of the SDK’s interfaces must be used only from the application main thread (main run loop).  Notifications are delivered in the same thread as well.  As a result, no synchronization around the SDK’s interfaces is required.  The SDK, however, may create threads for internal purposes.      

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
  
3. Initialize the conversation helper with the conversation instance obtained in the previous step and delegate object that should receive callbacks from this conversation helper.  This will automatically start incoming and outgoing video. The delegate class must conform to _SfBConversationHelperDelegate_ protocol.
  
  > Note: as per the license terms, before you start video for the first time after install, you must prompt the user to accept the Microsoft end-user license (also included in the SDK).  Subsequent versions of the SDK preview will include code to assist you in doing so.

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
        
4. Implement SfBConversationHelperDelegate methods to handle video service state changes.

    **Objective C**
     ```Objectivec 
      #pragma mark - Skype Delegates
    
    // At incoming video, unhide the participant video view
    - (void)conversationHelper:(SfBConversationHelper *)avHelper didSubscribeToVideo:(SfBParticipantVideo *)video {
        self.participantVideoView.hidden = NO; 
    }

    // When video service is ready to start, unhide self video view and start the service.
    - (void)conversationHelper:(SfBConversationHelper *)avHelper videoService:(SfBVideoService *)videoService didChangeCanStart:(BOOL)canStart {
        if (canStart) {
            if (self.selfVideoView.hidden) {
                self.selfVideoView.hidden = NO;
            }
        
            [videoService start:nil];
        } 
   }

    // When the audio status changes, reflect in UI
    - (void)conversationHelper:(SfBConversationHelper *)avHelper selfAudio:(SfBParticipantAudio *)audio didChangeIsMuted:(BOOL)isMuted {

        if (!isMuted) {
            [self.muteButton setTitle:@"Unmute" forState:UIControlStateNormal];
        }
        else {
            [self.muteButton setTitle:@"Mute" forState:UIControlStateNormal];
        } 
    }
    ```   
    
    **Swift**
     ```swift
     
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
     
5. To end the video meeting, monitor _canLeave_ property of a conversation to prevent leaving prematurely.

    **Objective C**
     ```Objectivec 
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

## Text Chat 

 >Note: ChatHandler is the helper class that can be used to integrate Skype text chat feature into your application. It can be integrated in similar manner to SfBConversationHelper. Please refer [App SDK samples](Samples.md) for further details.
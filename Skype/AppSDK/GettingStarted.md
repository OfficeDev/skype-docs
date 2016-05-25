# Getting started with Skype App SDK development 

This section shows how to get started developing mobile applications with the Skype App SDK. It also provides guidance on using the Skype App SDK samples.

## Download the Skype App SDK

The SDKs for iOS and Android are available for download from Microsoft. 
* [Skype for Business App SDK - iOS](http://aka.ms/sfbAppSDKDownload_ios)
* [Skype for Business App SDK - Android](http://aka.ms/sfbAppSDKDownload_android)

>Note: We maintain a set of [App SDK samples](Samples.md) for Android and iOS on **GitHub**. These samples are configured to use the App SDK and are ready to run.  See the readme.md in each of these samples for instructions.


## Configure your project for the Skype for Business App SDK

You can start coding with the App SDK after you complete the following configuration tasks for your platform.

#### iOS

The configuration steps are:

1. **Add embedded binary**: In XCode, select the project node and open the project properties pane. Add SkypeForBusiness.framework as an "Embedded Binary" (not a "Linked Framework"). 

  > Note: The SDK comes with a binary for use on physical devices (recommended) and a binary for running the iOS simulator (limited because audio and video function won't work correctly).  The binaries have the same name but are in separate folders. To run your app on a **device**, navigate to the location where you downloaded the App SDK and select the _SkypeForBusiness.framework_ file in the _AppSDKiOS_ folder. To run your app in a **simulator**,  selec the _SkypeForBusiness.framework_ file in the _AppSDKiOSSimulator_ folder.

2. **Add the helper classes into your project**: The SDK comes with two helper classes _ChatHandler and SfBConversationHelper_. _ChatHandler class_  can be used to  integrate Skype text chat feature into your application. _SfBConversationHelper class_ is used to add audio/video chat feature. These helper classes simplify interaction with the core APIs in mainline scenarios.  To use these, add SfBConversationHelper.h/SfBConversationHelper.m and ChatHandler.h/ChatHandler.m files from the _Helpers_ folder in your SDK download into your app's source code. 

3. Make sure _Enable Bitcode_ option is set to NO in your iOS project . In the Project Navigator, select your project, go to the Editor pane, select Project -> Build Settings -> select All tab -> Build Options -> Enable Bitcode = NO


#### Android

The configuration steps are:

1. **Copy the contents of the _AppSDKAndroid_ folder into your project**: Copy from your App SDK download folder into the _libs_ folder of your project module.

2. **Add the Conversation Helper into your project (optional)**: The SDK comes with an optional "conversation helper" class that simplifies interaction with the core APIs in mainline scenarios.  To use it, add SfBConversationHelper.java from the _Helpers_ folder in your App SDK download into your app's source code.

3. **Update the Conversation Helper package name**: If using the conversation helper, set it to match your app's own package name.
  
4. **Add the SDK libraries to the module Gradle dependencies struct:** 
> Note: Be sure to include the ```compile fileTree(dir: 'libs', include: ['*.jar'])``` statement. 
 
  ```java
  dependencies {
      compile fileTree(dir: 'libs', include: ['*.jar'])
      compile(name: "libucmp", ext: 'aar')
      compile(name: "platform", ext: 'aar')
      compile(name: "SkypeForBusiness", ext: 'aar')
      compile(name: "TelemetryService", ext: 'aar')
      compile(name: "ucmp-enums", ext: 'aar')
      compile(name: "TelemetryClient2", ext: 'aar')
      compile(name: "injector", ext: 'aar')
    
  }

  ```
4. **Add app permissions**: Add _uses-permission_ tags to the project **AndroidManifest.xml** file. 

  ```java
      <uses-permission android:name="android.permission.INTERNET" />
      <uses-permission
          android:name="android.permission.WRITE_EXTERNAL_STORAGE"
          tools:node="replace" />
      <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
      <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
      <uses-permission android:name="android.permission.CHANGE_WIFI_STATE" />
      <uses-permission android:name="android.permission.CHANGE_NETWORK_STATE" />
      <uses-permission android:name="android.permission.CHANGE_WIFI_MULTICAST_STATE" />
      <uses-permission android:name="android.permission.AUTHENTICATE_ACCOUNTS" />
      <uses-permission android:name="android.permission.GET_ACCOUNTS" />
      <uses-permission android:name="android.permission.MANAGE_ACCOUNTS" />
      <uses-permission android:name="android.permission.READ_PHONE_STATE" />
      <uses-permission android:name="android.permission.VIBRATE" />
      <uses-permission android:name="android.permission.CALL_PHONE" />
      <uses-permission android:name="android.permission.MODIFY_AUDIO_SETTINGS" />
      <uses-permission android:name="android.permission.RECORD_AUDIO" />
      <uses-permission android:name="android.permission.WAKE_LOCK" />
      <uses-permission android:name="android.permission.BLUETOOTH" />
      <uses-permission android:name="android.permission.CAMERA" />
      <uses-permission android:name="android.permission.READ_CONTACTS" />
      <uses-permission android:name="android.permission.WRITE_CONTACTS" />
      <uses-permission android:name="android.permission.WRITE_SETTINGS" />
      <uses-permission android:name="android.permission.READ_SYNC_STATS" />
      <uses-permission android:name="android.permission.READ_SYNC_SETTINGS" />
      <uses-permission android:name="android.permission.WRITE_SYNC_SETTINGS" />
      <uses-permission android:name="android.permission.BROADCAST_STICKY" />
      <uses-permission android:name="android.permission.READ_LOGS" />
      <uses-permission android:name="android.permission.SYSTEM_ALERT_WINDOW" />
      <uses-permission android:name="android.permission.READ_PROFILE" />

  ```

>Note: Subsequent versions of the SDK will eliminate any uneccesary permissions.
  
  
  

## Next steps
Now that you've configured your project to code against the **App SDK** API, learn how to get the URL of a **Skype for Business** meeting and then use the API to enable your mobile app to join the meeting:

* [Get a meeting URL](GetMeetingURL.md)
* [Use the SDK to join a meeting with an Android device](HowToJoinMeeting_Android.md)
* [Use the SDK to join a meeting with an iOS device](HowToJoinMeeting_iOS.md)

## Additional resources
Here are some more resources to help you build apps with the **Skype for Business App SDK**

* [App SDK samples](Samples.md) 
* [Submit your questions, bugs, feature requests, and contributions](Feedback.md)

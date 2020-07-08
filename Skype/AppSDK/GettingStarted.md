# Getting started with Skype App SDK development 

This section shows how to get started developing mobile applications with the Skype App SDK. It also provides guidance on using the Skype App SDK samples.

## Download the Skype App SDK

The SDKs for iOS and Android are available for download from Microsoft. 
* [Skype for Business App SDK - iOS](https://aka.ms/sfbAppSDKDownload_ios)
* [Skype for Business App SDK - Android](https://aka.ms/sfbAppSDKDownload_android)

>[!NOTE]
 We maintain a set of [App SDK samples](Samples.md) for Android and iOS on **GitHub**. These samples are configured to use the App SDK and are ready to run.  See the readme.md in each of these samples for instructions.


## Configure your project for the Skype for Business App SDK

You can start coding with the App SDK after you complete the following configuration tasks for your platform.

#### iOS

The configuration steps are:

1. **Add embedded binary**: In XCode, select the project node and open the project properties pane. Add SkypeForBusiness.framework as an "Embedded Binary" (not a "Linked Framework"). 

  > [!NOTE] 
  The SDK comes with a binary for use on physical devices (recommended) and a binary for running the iOS simulator (limited because audio and video function won't work correctly).  The binaries have the same name but are in separate folders. To run your app on a **device**, navigate to the location where you downloaded the App SDK and select the _SkypeForBusiness.framework_ file in the _AppSDKiOS_ folder. To run your app in a **simulator**,  selec the _SkypeForBusiness.framework_ file in the _AppSDKiOSSimulator_ folder.

2. **Add the Conversation Helper** into your project (optional): The SDK comes with an optional "conversation helper" class that can be used to  integrate Skype Audio/Video chat feature into your application. These helper classes simplify interaction with the core APIs in mainline scenarios.  To use these, add SfBConversationHelper.h/SfBConversationHelper.m files from the _Helpers_ folder in your SDK download into your app's source code. 
 > [!NOTE]
  To add text chat feature in your application, you can refer _ChatHandler_ helper class in our [iOS sample apps](https://github.com/OfficeDev/skype-ios-app-sdk-samples). _ChantHandler_ class works similar to _conversation helper _ class and can be used to facilitate text chat integration.
3. Make sure _Enable Bitcode_ option is set to NO in your iOS project . In the Project Navigator, select your project, go to the Editor pane, select Project -> Build Settings -> select All tab -> Build Options -> Enable Bitcode = NO

4. **Add description of required permissions** to the application’s Info.plist (use appropriate messages):
```xml
<key>NSCameraUsageDescription</key>
<string>Access to the camera is required for making video calls.</string>
<key>NSContactsUsageDescription</key>
<string>Access to your address book is required for making calls to contacts.</string>
<key>NSMicrophoneUsageDescription</key>
<string>Access to the microphone is required for making calls.</string>
```

5. **Configure AVAudioSession** before attempting to use audio:
```swift
let audioSession = AVAudioSession.sharedInstance()
try audioSession.setCategory(AVAudioSessionCategoryPlayAndRecord, withOptions: [.AllowBluetooth, .MixWithOthers, .DuckOthers])
try audioSession.setMode(AVAudioSessionModeVoiceChat)
```

6. **Configure background modes** to allow continuing an audio call while application is in background. Add Audio and VoIP.

#### Android

The configuration steps are:

1. **Copy the contents of the _AppSDKAndroid_ folder into your project**: Copy from your App SDK download folder into the _libs_ folder of your project module.

2. **Add the Conversation Helper into your project (optional)**: The SDK comes with an optional "conversation helper" class that simplifies interaction with the core APIs in mainline scenarios.  To use it, add SfBConversationHelper.java from the _Helpers_ folder in your App SDK download into your app's source code.

3. **Update the Conversation Helper package name**: If using the conversation helper, set it to match your app's own package name.
  
4. **Add the SDK libraries to the module Gradle dependencies struct:** 
> [!NOTE]
 Be sure to include the ```compile fileTree(dir: 'libs', include: ['*.jar'])``` statement. 
 
  ```gradle
  dependencies {
    compile fileTree(dir: 'libs', include: ['*.jar'])
    compile(name: "SkypeForBusinessNative", ext: 'aar')
    compile(name: "SkypeForBusinessPlatform", ext: 'aar')
    compile(name: "SkypeForBusiness", ext: 'aar')
    compile(name: "SkypeForBusinessTelemetryService", ext: 'aar')
    compile(name: "SkypeForBusinessNativeEnums", ext: 'aar')
    compile(name: "SkypeForBusinessTelemetryClient", ext: 'aar')
    compile(name: "SkypeForBusinessInjector", ext: 'aar')
    compile(name: "android-database-sqlcipher", ext: 'aar')
    
  }

  ```
4. **Add app permissions**: Add _uses-permission_ tags to the project **AndroidManifest.xml** file. 

  ```xml
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

    <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />

  ```

>[!NOTE] 
Subsequent versions of the SDK will eliminate any unneccessary permissions.
  
  
## Configure your Android application as a MultiDex application
The libraries that support the Android Skype for Business App SDK include a large number of methods. If the total number of methods in your application - including the App SDK methods - exceed 64,000, then you
must configure your app as a [MultiDex](https://developer.android.com/studio/build/multidex.html) application. To enable a basic MultDex configuration, you will add options to your module **build.gradle** file and 
the top level application class. 

### MultiDex support in build.gradle

1. Change the defaultConfig structure in your module **build.gradle** file. Add `multiDexEnabled true` to the structure.

   ```gradle
       defaultConfig {
           applicationId 'com.microsoft.office.sfb.samples.healthcare'
           minSdkVersion 17
           targetSdkVersion 22
           versionCode 2
           versionName "2.1"
           multiDexEnabled true
       }
   ``` 
1. Add a **dexOptions** structure to the module **build.gradle** file.

   ```gradle
       dexOptions {
        preDexLibraries=false
        jumboMode = true
        javaMaxHeapSize "4g"
    }
   ```
### Extend your application class as a **MultiDexApplication**

1. If your application does not have a class that extends the Application class, you must create one. Before you add an application class to your module, update 
your **AndroidManifest.xml** `Application` node to include the attribute, `android:name="<YOUR PACKAGE NAME>.<YOUR APPLICATION CLASS NAME>">`

1. Create or update your application class to extend **MultiDexApplication**. Be sure to override the **attachBaseContext** method.

```java
package com.microsoft.office.sfb.healthcare;

import android.content.Context;
import android.support.multidex.MultiDex;
import android.support.multidex.MultiDexApplication;


public class SkypeApplication extends MultiDexApplication{
    @Override
    protected void attachBaseContext(Context base) {
        super.attachBaseContext(base);
        MultiDex.install(this);
    }
}
```


## Next steps
Now that you've configured your project to code against the **App SDK** API, learn how to get the URL of a **Skype for Business** meeting and then use the API to enable your mobile app to join the meeting:


In most cases, use a meeting Url to join a meeting anonymously. Otherwise, you'll need to use a **Trusted Application API**-enabled service application to get the Discover Url and anonymous meeting token. Your mobile app
will call the service application to get these resources before joining a meeting. To learn more about this, see [Use the App SDK and the Trusted Application API to join an Online meeting - Android](HowToJoinOnlineMeeting_Android.md) or
[Use the App SDK and the Trusted Application API to join an Online meeting - iOS](../Trusted-Application-API/docs/ImplementingAnonymousClientWithSkypeAppSDK.md).  The following table shows you what resources to use for your SfB deployment scenario.

|Skype for Business topology|Enable preview features enabled|Enable preview features disabled|Meeting join resource|
|:----|:----|:----|:----|
|CU June 2016|Chat, AV|Chat only|Meeting Url|
|CU November 2016|Chat, AV| Chat, AV|Meeting Url|
|SfB Online|Chat, AV|n/a|Meeting Url|
|SfB Online|n/a|Chat, AV|Discover Uri, Anon Token|

* [Get a meeting URL](GetMeetingURL.md)
* [Use the SDK to join a meeting with an Android device](HowToJoinMeeting_Android.md)
* [Use the App SDK and the Trusted Application API to join an Online meeting - Android](HowToJoinOnlineMeeting_Android.md)
* [Use the SDK to join a meeting with an iOS device](HowToJoinMeeting_iOS.md)

## See also
Here are some more resources to help you build apps with the **Skype for Business App SDK**

* [App SDK samples](Samples.md) 
* [Submit your questions, bugs, feature requests, and contributions](Feedback.md)

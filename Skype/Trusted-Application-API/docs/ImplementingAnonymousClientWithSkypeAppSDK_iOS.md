# Implementing a Anonymous Client with the Skype App SDK - iOS

This article will walk you through the sample code to enable the core  **Skype for Business Online** anonymous meeting join scenario in your iOS app involving the client-side functionality
of the Skype App SDK. 

Android developers should read [Implementing a Anonymous Client with the Skype App SDK - Android.](ImplementingAnonymousClientWithSkypeAppSDK.md)

If the anonymous meeting your app joins is hosted in a **Skype for Business Online** service and 
your app is **not** enabled for Skype for Business preview features, then your app must get a **discovery Url** and an **anonymous meeting token** to join. 

**The Trused Application API-based service application** provides discovery Url and anonymous meeting token based on the meeting join Url. This **meeting join Url** can be obtained in different ways as described in [Anonymous Meeting Scheduling.](./AnonymousMeetingSchedule.md) 
This article includes the sample code to obtain the **meeting join Url** programmatically, using **The Trused Application API-based service application**.

After completing the steps in this article, your app can join a **Skype for Business Online** video meeting with discovery Url and anonymous meeting token. No **Skype for Business Online** credentials are used to join the meeting.

## Prerequisites

1. Please read [Getting started with Skype App SDK development](https://msdn.microsoft.com/en-us/skype/appsdk/gettingstarted) to learn how to configure your iOS project for the **Skype for Business** App SDK.  In particular, the following steps assume that you have added the _SfBConversationHelper.h/.m_ class to your source to let you complete the scenario with a minimum of code. 

2. Create and deploy a **Trusted Application API Service Application** for Skype for Business Online. Please refer [Developing Trusted Application API applications for Skype for Business Online](./DevelopingApplicationsforSFBOnline.md) for more details.
This service application will provide RESTful Trusted Application API endpoint to creates ad-hoc meetings, provides meeting join Urls, discovery Uris, and anonymous meeting tokens to your iOS app.

The rest of this article describes how to enable your iOS app to call into such a service application. You can read more about the [Trusted Application API](./Trusted_Application_API_GeneralReference.md) to learn
about all of the features of this Skype for Business service application api.

We've published two service application [examples](https://github.com/OfficeDev/skype-docs/tree/master/Skype/Trusted-Application-API/samples) in GitHub to get you started.

## Sample code walkthrough

The following example code is taken from our GitHub [Banking app sample](https://github.com/OfficeDev/skype-ios-app-sdk-samples). The example steps include:

- Call into Trusted Application API Service Applications to get a join Url for a new ad-hoc meeting that is created by the service application
- Use the join url to get an anonymous meeting token and a discovery Uri from the service application
- Call the **joinMeetingAnonymously** method, passing the two resources from the previous step.
- Show use of new Skype App SDK **setEndUserAcceptedVideoLicense** api that is required to proceed with features that potentially use video codecs.
- Get a **Conversation** object from the asynchronously returned **AnonymousSession** object.


### 1. Get anonymous meeting join Url from your Trusted Application API Service Application

**Swift**

```Swift

/* POST Request on Custom listening API in your Trusted Service Application. 
*  POST on this link will return an adhocmeeting URL.
*  Your service code will need to implement this job. 
*/ 

let request = NSMutableURLRequest(URL: NSURL(string: " https://YourServiceApplication.cloudapp.net/YourServiceApplicationCustomGetAdhocMeetingJob")!)
        request.HTTPMethod = "POST"
        request.HTTPBody = "Subject=adhocMeeting&Description=adhocMeeting&AccessLevel=".dataUsingEncoding(NSUTF8StringEncoding)

        NSURLSession.sharedSession().dataTaskWithRequest(request) {
            (data: NSData?, response: NSURLResponse?, error: NSError?) in
            NSRunLoop.mainRunLoop().performBlock() {
                do {
                    guard error == nil, let data = data else {
                        throw error!
                    }

                    let json = try NSJSONSerialization.JSONObjectWithData(data, options: []) as! [String: String]
                    self.meetingUrl.text = json["JoinUrl"]
                    } catch {
                    UIAlertView(title: "Getting meeting URL failed", message: "\(error)", delegate: nil, cancelButtonTitle: "OK").show()
                }
            }
        }.resume()

 /** Response data:
 *    {
 *    "OnlineMeetingUri":"**", 
 *    "DiscoverUri": "**", 
 *    "OrganizerUri": "**", 
 *    "JoinUrl": "**", // Meeting join Url we are interested in
 *    "ExpireTime": "**"
 *    }
 **/
```

### 2. Get a Discovery Uri and token

When the user decides to join the meeting, it pings the Service Application with the meeting join Url.
The user gets the _anonymous application token_ and _Discovery Uri_ based on this _meeting join Url_(should be in same tenant)

**Swift**

```swift
/** POST Request on Custom listening API in your Trusted Service Application.  
*   POST on this link with your meetingURL and receive a response with the DiscoverURL and token.
*   Your service code will need to implement this job.
**/ 

let request = NSMutableURLRequest(URL: NSURL(string: "https://YourServiceApplication.cloudapp.net/YourServiceApplicationCustomGetAnonTokenJob")!)
        request.HTTPMethod = "POST"
        let meetintURL = meetingUrl.text!;
        request.HTTPBody = "ApplicationSessionId=AnonMeeting&AllowedOrigins=http%3a%2f%2flocalhost%2f&MeetingUrl=\(meetintURL)".dataUsingEncoding(NSUTF8StringEncoding)
        NSURLSession.sharedSession().dataTaskWithRequest(request) {
            (data: NSData?, response: NSURLResponse?, error: NSError?) in
            NSRunLoop.mainRunLoop().performBlock() {
                do {
                    guard error == nil, let data = data else {
                        throw error!
                    }


                    let json = try NSJSONSerialization.JSONObjectWithData(data, options: []) as! [String: String]
                    self.discoveryURI = json["DiscoverUri"]!
                    self.token = json["Token"]
                } catch {
                    UIAlertView(title: "Getting Discover URL failed", message: "\(error)", delegate: nil, cancelButtonTitle: "OK").show()
                }
            }
        }.resume()

 /** Response data:
*{
*  “DiscoverUri”:”**”,
*  “ExpireTime”:”**”,
*  “TenantEndpointId”:”**”,
* “Token”:”” 
* }
**/
```
 
### 3. Join the new adhoc meeting anonymously as a 'guest'

Joins a meeting anonymously via Skype App SDK using the anonymous token and discovery Uri from previous request as your sign-in parameters. It calls **joinMeetingAnonymously**, gets an **AnonymousSession**, and then the **Conversation** that
represents the adhoc meeting.

> [!NOTE]
The sample code shows the use of the new **setEndUserAcceptedVideoLicense** api. This API must be called before a user can join video in a meeting. Once the api has been called, the user 
is considered in acceptance of the third party video codec license that we use to support video. It is necessary that your app presents the terms of this license to the user before a meeting 
is started. Subsequent meetings do not require the license acceptance.

#### Show video codec license

**Swift**

```Swift
    /**
     * Shows a video license acceptance dialog if user has not been prompted before. If user
     * accepts license, call is started. Else, user cannot start Audio/Video call.
     */

            let sfb: SfBApplication = SfBApplication.sharedApplication()
            let config: SfBConfigurationManager = sfb.configurationManager
            let key = "AcceptedVideoLicense"
            let defaults = NSUserDefaults.standardUserDefaults()
            
            if defaults.boolForKey(key) {
            /**
            * Notify that user has accepted the Video license.
            *
            * This is required to proceed with features that potentially use video codecs.
            * Until this method is called, any attempt to use those features will fail.
            */
                config.setEndUserAcceptedVideoLicense()
                self.performSegueWithIdentifier("joinOnlineAudioVideoChat", sender: nil)
                
            } else {
                
                /** Show video license acceptance view. 
                *   MicrosoftLicenseViewController is class that shows video license 
                *   and stores user's acceptance or rejection of the video license
                **/

                let vc = self.storyboard?.instantiateViewControllerWithIdentifier("MicrosoftLicenseViewController") as! MicrosoftLicenseViewController
                vc.delegate = self
                self.presentViewController(vc, animated: true, completion: nil)
            }

     /**
     * Writes the user's acceptance or rejection of the video license
     **/

        let key = "AcceptedVideoLicense"
        let defaults = NSUserDefaults.standardUserDefaults()
        defaults.setBool(true, forKey: key)
        let sfb: SfBApplication = SfBApplication.sharedApplication()
        let config: SfBConfigurationManager = sfb.configurationManager
        config.setEndUserAcceptedVideoLicense()
        self.performSegueWithIdentifier("joinOnlineAudioVideoChat", sender: nil)
        

```

#### Join the meeting

**Swift**
```swift

    /** Joins a meeting anonymously as a 'guest', without requiring sign-in to
     * a Skype for Business account
     *
     * @param discoverUrl The discover URL to join.
     * @param authToken The authorization token.
     * @param displayName Name of the guest user, which will be visible in all
     * Skype for Business clients that also join the meeting.
     * @return A session containing single conversation that represents this meeting.
     * Observe [SfBConversation state] property to determine when connection to
     * the meeting is fully established.
     *
     * @note This method can be called repeatedly at any time. It automatically
     * disconnects any existing meetings.
     **/
     
       do {
            
            let session = try sfb!.joinMeetingAnonymousWithDiscoverUrl(NSURL(string: self.discoveryURI!)!, authToken: self.token!, displayName: self.displayName.text!)
            conversation = session.conversation
            return true
        } catch {
            UIAlertView(title: "Joining online meeting failed. Try again later!", message: "\(error)", delegate: nil, cancelButtonTitle: "OK").show()
            return false
        }
```

#### The video license terms

These are the terms:

**MICROSOFT SOFTWARE LICENSE TERMS**
**SOFTWARE FOR VIDEO CONFERENCING IN MOBILE APPLICATIONS POWERED BY SKYPE FOR BUSINESS**
    
These license terms are an agreement between you and Microsoft Corporation (or one of its affiliates). They apply to the software named above and any Microsoft services or software updates (except to the extent such services or updates are accompanied by new or additional terms, in which case those different terms apply prospectively and do not alter your or Microsoft’s rights relating to pre-updated software or services). IF YOU COMPLY WITH THESE LICENSE TERMS, YOU HAVE THE RIGHTS BELOW.
1.	INSTALLATION AND USE RIGHTS.

    a)  General. You may run copies of the software on your devices solely with versions of software applications that communicate with validly licensed Microsoft Skype for Business Server or Skype for Business Online. 
   
    b)  Third Party Applications. The software may include third party applications that Microsoft, not the third party, licenses to you under this agreement. Any included notices for third party applications are for your information only. 
2.	SCOPE OF LICENSE. The software is licensed, not sold. Microsoft reserves all other rights. Unless applicable law gives you more rights despite this limitation, you will not (and have no right to):
   
    a)  work around any technical limitations in the software that only allow you to use it in certain ways; 
   
    b)  reverse engineer, decompile or disassemble the software; 
    
    c)  remove, minimize, block, or modify any notices of Microsoft or its suppliers in the software; 
   
    d)  use the software in any way that is against the law or to create or propagate malware; or 
   
    e)  share, publish, or lend the software (except for any distributable code, subject to the applicable terms above), provide the software as a stand-alone hosted solution for others to use, or transfer the software or this agreement to any third party. 
3.	EXPORT RESTRICTIONS. You must comply with all domestic and international export laws and regulations that apply to the software, which include restrictions on destinations, end users, and end use. For further information on export restrictions, visit (aka.ms/exporting). 
4.	SUPPORT SERVICES. Microsoft is not obligated under this agreement to provide any support services for the software. Any support provided is “as is”, “with all faults”, and without warranty of any kind. 
5.	ENTIRE AGREEMENT. This agreement, and any other terms Microsoft may provide for supplements, updates, or third-party applications, is the entire agreement for the software. 
6.	APPLICABLE LAW. If you acquired the software in the United States, Washington law applies to interpretation of and claims for breach of this agreement, and the laws of the state where you live apply to all other claims. If you acquired the software in any other country, its laws apply. 
7.	CONSUMER RIGHTS; REGIONAL VARIATIONS. This agreement describes certain legal rights. You may have other rights, including consumer rights, under the laws of your state or country. Separate and apart from your relationship with Microsoft, you may also have rights with respect to the party from which you acquired the software. This agreement does not change those other rights if the laws of your state or country do not permit it to do so. For example, if you acquired the software in one of the below regions, or mandatory country law applies, then the following provisions apply to you:
   
    a)  Australia. You have statutory guarantees under the Australian Consumer Law and nothing in this agreement is intended to affect those rights. 
   
    b)  Canada. If you acquired this software in Canada, you may stop receiving updates by turning off the automatic update feature, disconnecting your device from the Internet (if and when you re-connect to the Internet, however, the software will resume checking for and installing updates), or uninstalling


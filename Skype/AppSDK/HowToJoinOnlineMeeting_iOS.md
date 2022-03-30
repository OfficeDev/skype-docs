---
title: Use the App SDK and the Trusted Application API to join an Online meeting - iOS
description: Learn how to enable the core Skype for Business Online anonymous meeting join scenario in your iOS app.
ms.date: 03/30/2022
---

# Use the App SDK and the Trusted Application API to join an Online meeting - iOS

This article shows you how to enable the core **Skype for Business Online** anonymous meeting join scenario in your iOS app. Android developers should read
[Use the App SDK and the Trusted Application API to join an Online meeting - Android](HowToJoinOnlineMeeting_Android.md).

If the anonymous meeting your app joins is hosted in a **Skype for Business Online** service and
your app is **not** enabled for Skype for Business preview features, then your app must get a **discovery Url** and an **anonymous meeting token** to join. This workflow requires that you create and deploy a Trused Application API-based service application that creates ad-hoc meetings, provides meeting join Urls, discovery Uris, and anonymous meeting tokens to the mobile apps that
request them.

>[!NOTE]
**For mobile apps that enabled preview features:** If the anonymous meeting your app joins is hosted in a **Skype for Business Online** service and
your app is enabled for Skype for Business preview features, then your app can use a meeting Url to join. A Trusted Application API service application is **not** needed to complete the scenario in this case. To learn
how to use a meeting Url, read [Use the App SDK to join a meeting with an iOS device](HowToJoinMeeting_iOS.md)

## Create and deploy a Trusted Application API-based service Application

The service application you create will give your mobile app access to the needed anonymous meeting join resources - discovery Url and anonymous meeting token. You'll use the RESTful Trusted Application API endpoint to schedule a meeting, get
the discovery Url and token. The rest of this article describes how to enable your iOS app to call into such a service application. You can read more about the [Trusted Appplication API](../Trusted-Application-API/docs/Trusted_Application_API_GeneralReference.md) to learn
about all of the features of this Skype for Business service application api.

We've published two service application [examples](https://github.com/OfficeDev/skype-docs/tree/johnau/ucapdocs/Skype/Trusted-Application-API/samples) in GitHub to get you started.

## Add anonymous online meeting code to your mobile app

Please read [Implementing a Anonymous Client with the Skype App SDK - iOS.](https://github.com/OfficeDev/skype-docs/blob/johnau/ucapdocs/Skype/Trusted-Application-API/docs/ImplementingAnonymousClientWithSkypeAppSDK_iOS.md) for the sample code.
The sample code is taken from our GitHub [Banking app sample](https://github.com/OfficeDev/skype-ios-app-sdk-samples). The example code steps include:

- Call into a service application sample to get a join Url for a new ad-hoc meeting that is created by the service application
- Use the join url to get an anonymous meeting token and a discovery Uri from the service application
- Call the **joinMeetingAnonymously** method, passing the two resources from the previous step.
- Show use of new Skype App SDK **setEndUserAcceptedVideoLicense** api that is required to proceed with features that potentially use video codecs.
- Get a **Conversation** object from the asynchronously returned **AnonymousSession** object.

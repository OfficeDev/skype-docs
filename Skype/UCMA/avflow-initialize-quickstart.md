---
title: AVFlow-Initialize (QuickStart)
TOCTitle: AVFlow-Initialize (QuickStart)
ms:assetid: 2e489f3a-14ab-4632-99c2-94da0b12827e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466140(v=office.16)
ms:contentKeyID: 65240074
ms.date: 07/27/2015
mtps_version: v=office.16
---

# AVFlow-Initialize (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: AVFlow-Initialize

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\AVFlow-Initialize

Change the settings of an active **AudioVideoFlow**.

## Description

The application places an audio/video call to the designated target, after initializing platform and endpoints.

After the call is connected, the application uses the **Initialize** method on the **AudioVideoFlow** instance to change the settings of the currently idle audio/video flow, before the flow goes active (that is, before the value of the **State** property on the flow is **Active**). A moment later, the flow goes active, and the changes take effect. (In this case, the endpoint is set to use only audio 8 kHz codecs). The application prints the step-by-step actions it performs to the console, and then exits, performing normal platform shutdown.

## Features

  - Changing the settings on an audio/video call and its associated flow at run time to support or to remove support for codecs and extra data types.

  - Basic audio/video call placement.

  - **AudioVideoFlow** handling and control.

## Prerequisites

  - Skype for Business Server 2015.

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Replace the credentials in the variables at the beginning of the code sample with the credentials and server of the users from your Skype for Business Server 2015 topology.

2.  Substitute the address of the called user in the code sample with the address of a valid, currently signed-in user capable of receiving audio calls.

3.  Open the project in Microsoft Visual Studio, and then press **F5**.


---
title: AVFlow-Hold (QuickStart)
TOCTitle: AVFlow-Hold (QuickStart)
ms:assetid: b2cb3951-b424-4678-ae0d-518a152c0c50
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466138(v=office.16)
ms:contentKeyID: 65240073
ms.date: 07/27/2015
mtps_version: v=office.16
---

# AVFlow-Hold (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: AVFlow-Hold

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\AVFlow-Hold

Place a call, put the call on hold, and then retrieve the call from hold.

## Description

The application places an audio/video call to the designated target, after initializing platform and endpoints.

After the call is connected, the application places the active **AudioVideoFlow** on hold. A moment later, the application retrieves the call from hold. The application prints the step-by-step actions it performs to the console, and then exits, performing normal platform shutdown.

## Features

  - Placing an audio/video call on hold and then resuming the call

  - Basic audio/video call placement

  - **AudioVideoFlow** handling and control

## Prerequisites

  - Skype for Business Server 2015.

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Replace the credentials in the variables at the beginning of the code sample with the credentials and server of the users from your Skype for Business Server 2015 topology.

2.  Substitute the address of the called user in the code sample with the address of a valid, currently signed-in user capable of receiving audio calls.

3.  Open the project in Microsoft Visual Studio development system, and then press **F5**.


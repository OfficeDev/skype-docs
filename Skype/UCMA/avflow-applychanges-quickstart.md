---
title: AVFlow-ApplyChanges (QuickStart)
TOCTitle: AVFlow-ApplyChanges (QuickStart)
ms:assetid: 6bf7708b-ca30-4418-b36f-5f4e91141ca6
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466136(v=office.16)
ms:contentKeyID: 65240072
ms.date: 07/27/2015
mtps_version: v=office.16
---

# AVFlow-ApplyChanges (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\AVFlow-ApplyChanges

Apply a configuration change to an active [AudioVideoFlow](https://msdn.microsoft.com/en-us/library/hh383533\(v=office.16\)) instance.

## Description

The application places an audio/video call to the designated target, after initializing platform and endpoints. After the call is connected, the application waits for the **AudioVideoFlow** to go active (that is, for the value of its **State()** property to change to **Active**), applies a configuration change to the flow to change the allowed direction of media travel from Send/Receive to Send only, and then the platform is shut down normally.

## Features

  - Basic audio/video call placement

  - Platform and endpoint initialization

  - **AudioVideoFlow** handling and control

  - Applying and changing the settings of an active **AudioVideoFlow**

## Prerequisites

  - Skype for Business Server 2015.

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Replace the credentials in the variables at the beginning of the code sample with the credentials and server of the users from your Skype for Business Server 2015 topology.

2.  Substitute the address of the called user in the code sample with the address of a valid, currently signed-in user capable of receiving audio calls.

3.  Open the project in Microsoft Visual Studio, and then press **F5**.


---
title: AVFlow-Mute (QuickStart)
TOCTitle: AVFlow-Mute (QuickStart)
ms:assetid: 1d213bf7-9044-44cb-90dd-a54cd10a0f69
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466146(v=office.16)
ms:contentKeyID: 65240075
ms.date: 07/27/2015
mtps_version: v=office.16
---

# AVFlow-Mute (QuickStart)


**Applies to**: Skype for Business 2015

 

**Sample name**: AVFlow-Mute

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\AVFlow-Mute

## Description

The application places an audio/video call to the designated target, after initializing platform and endpoints. After the call is connected, the application waits until the **State** property on the **AudioVideoFlow** changes to **Active**, and then the application mutes the call.

A moment later, the application unmutes the call. The application prints the step-by-step actions it performs to the console, and then exits, performing normal platform shutdown.

## Features

  - Mute control on an **AudioVideoFlow**

  - Basic audio/video call placement

  - **AudioVideoFlow** handling and control

## Prerequisites

  - Skype for Business Server 2015.

  - Two users capable of sending and receiving voice calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Replace the credentials in the variables at the beginning of the code sample with the credentials and server of the users from your Skype for Business Server 2015 topology.

2.  Substitute the address of the called user in the code sample with the address of a valid, currently signed-in user capable of receiving audio calls.

3.  Open the project in Microsoft Visual Studio development system, and then press **F5**.


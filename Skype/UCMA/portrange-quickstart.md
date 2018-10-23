---
title: PortRange (QuickStart)
TOCTitle: PortRange (QuickStart)
ms:assetid: 50620ffb-d04f-474b-bba5-68965ee89026
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454815(v=office.16)
ms:contentKeyID: 65240078
ms.date: 07/27/2015
mtps_version: v=office.16
---

# PortRange (QuickStart)


**Applies to**: Skype for Business 2015

 

**Sample name**: PortRange

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\PortRange

## Description

The application places an audio/video call to the designated target, after initializing platform and endpoints. It then uses **AudioVideoSettings** to change the port range to be used by the media stack at run time. After successfully placing the audio/video call, the application prints the content of the change to the console, and then exits, performing normal platform shutdown.

## Features

  - Subscribing to a change in flow properties and dealing with being put on hold.

  - Basic audio/video call placement.

  - **AudioVideoFlow** handling and control.

  - Changing the configuration of an active **AudioVideoFlow**.

## Prerequisites

  - Skype for Business Server 2015.

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Replace the credentials in the variables at the beginning of the code sample with the credentials and server of the users from your Skype for Business Server 2015 topology.

2.  Substitute the address of the called user in the code example with the address of a valid, currently signed-in user capable of receiving audio calls.

3.  Open the project in Microsoft Visual Studio development system, and then press **F5**.


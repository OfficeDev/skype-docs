---
title: Player (QuickStart)
TOCTitle: Player (QuickStart)
ms:assetid: 479967c3-7fc8-4884-83f7-ea92c9893026
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454813(v=office.16)
ms:contentKeyID: 65240077
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Player (QuickStart)


**Applies to**: Skype for Business 2015



**Sample name**: Player

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\Player

## Description

The application initializes the platform and places a call to the specified user, plays a WMA file, pauses and then resumes playing the file, and then stops. The application then tears down the player, file source, and platform. The application prints logging log results to the console, and then exits, performing normal platform shutdown.

## Features

  - Call and conversation cleanup and use

  - Basic call placement

  - Basic **Player** and **WmaFileSource** usage

## Prerequisites

  - Skype for Business Server 2015.

  - One user capable of receiving audio calls.

  - The credentials for the user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Replace the credentials in the variables at the beginning of the code sample with the credentials and server of the users from your Skype for Business Server 2015 topology.

2.  Substitute the address of the called user in the code sample with the address of a valid, currently signed-in user capable of receiving audio calls.

3.  Open the project in Microsoft Visual Studio, and then press **F5**.


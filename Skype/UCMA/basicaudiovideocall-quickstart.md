---
title: BasicAudioVideoCall (QuickStart)
TOCTitle: BasicAudioVideoCall (QuickStart)
ms:assetid: 062e5bc2-0f8b-4584-8335-f5a88beaf43e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466147(v=office.16)
ms:contentKeyID: 65240076
ms.date: 07/27/2015
mtps_version: v=office.16
---

# BasicAudioVideoCall (QuickStart)


**Applies to**: Skype for Business 2015



**Sample name**: BasicAudioVideoCall

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\BasicAudioVideoCall

Make a basic **AudioVideoCall** and perform basic flow handling.

## Description

The application places an audio/video call to the designated target, after initializing platform and endpoints. After the call is connected, the application subscribes to events that monitor basic **AudioVideoFlow** transitions and states.

The application prints log results to the console. After the call is completed, the application exits, performing normal platform shutdown.

## Features

  - Basic call placement

  - Basic use of **AudioVideoCall**

  - Basic **AudioVideoFlow** handling and control

## Prerequisites

  - Skype for Business Server 2015.

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015 (Skype for Business 2015 is the recommended client).

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  You may either supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio development system, and then press **F5**.


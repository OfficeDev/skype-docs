---
title: ForwardIncomingCall (QuickStart)
TOCTitle: ForwardIncomingCall (QuickStart)
ms:assetid: 33f4303f-c3dc-47e5-b44e-3edfeb666a5e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454826(v=office.16)
ms:contentKeyID: 65240095
ms.date: 07/27/2015
mtps_version: v=office.16
---

# ForwardIncomingCall (QuickStart)

**Applies to**: Skype for Business 2015

**Sample name**: ForwardIncomingCall

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\ForwardIncomingCall

## Description

The application initializes the platform and logs in on behalf of a user. The sample then waits for an incoming audio-video call, and forwards the call to the designated target. After forwarding the call, the sample then terminates the call, conversation, and platform. The sample then exits.

## Features

  - Call and conversation cleanup and use

  - Call forwarding

  - [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api) handling and control

## Prerequisites

  - Three users, enabled to use Skype for Business Server 2015.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.

3.  Make a voice call from the user logged in to to the URI of the user whose credentials the sample is using.


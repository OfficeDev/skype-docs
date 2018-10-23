---
title: CallModalityAddition (QuickStart)
TOCTitle: CallModalityAddition (QuickStart)
ms:assetid: 741ef143-e2d2-468d-8af1-0ec530ee933e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454814(v=office.16)
ms:contentKeyID: 65240088
ms.date: 07/27/2015
mtps_version: v=office.16
---

# CallModalityAddition (QuickStart)

**Applies to**: Skype for Business 2015

**Sample name**: CallModalityAddition

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\CallModalityAddition

## Description

The application initializes the platform to log in a pair of users and initiates an instant messaging call between them. After the instant messaging call is established, the application reuses the existing conversation to send an audio-video call between the two users as part of the same dialog. After the audio-video call is established, the application prints logging to the console, and then quits, shutting down the platform normally. Note that no audio-video media or instant messages are actually exchanged between the users.

## Features

  - Modality addition in a call and conversation

  - Basic call placement

  - Conversation reuse

## Prerequisites

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.


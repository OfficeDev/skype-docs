---
title: DeclineIncomingCall (QuickStart)
TOCTitle: DeclineIncomingCall (QuickStart)
ms:assetid: 72db6c20-e348-489d-bf98-160d72329a4d
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454825(v=office.16)
ms:contentKeyID: 65240093
ms.date: 07/27/2015
mtps_version: v=office.16
---

# DeclineIncomingCall (QuickStart)

**Applies to**: Skype for Business 2015

**Sample name**: DeclineIncomingCall

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\DeclineIncomingCall

## Description

The application initializes the platform to start listening on behalf of a user, rejects the first incoming instant messaging call, and then tears down the call, conversation, and platform. The application then quits.

The sample expects a user logged on to a client (such as Skype for Business 2015) to send an instant message to the user who is logged on to the sample application.

## Features

  - Call and conversation cleanup and use

  - Handling of incoming instant messages

  - [InstantMessagingFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.instantmessagingflow?view=ucma-api) handling and control

## Prerequisites

  - Two users, enabled to use Skype for Business Server 2015.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.

3.  Send an instant message to the user whose credentials the endpoint is using.


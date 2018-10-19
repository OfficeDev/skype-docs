---
title: InstantMessagingCall (QuickStart)
TOCTitle: InstantMessagingCall (QuickStart)
ms:assetid: 76574248-8d00-4c66-aad5-a5df2f9f7607
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454827(v=office.16)
ms:contentKeyID: 65240096
ms.date: 07/27/2015
mtps_version: v=office.16
---

# InstantMessagingCall (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: InstantMessagingCall

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\InstantMessagingCall

## Description

The application initializes the platform, and logs in on behalf of a user. The sample then sends an instant message, consisting of the text "Hello World", to another user who is logged on to a client such as Skype for Business 2015. The sample echoes instant messages sent by the user logged in to Skype for Business 2015. When the user logged on to Skype for Business 2015 responds with a message that consists of the text 'bye', or closes the instant messaging window, the sample shuts down the platform and exits.

## Features

  - Call and conversation cleanup and usage

  - Basic call placement

  - Instant messaging message usage, including replying to incoming messages

  - [InstantMessagingFlow](https://msdn.microsoft.com/en-us/library/hh383312\(v=office.16\)) handling and control

## Prerequisites

  - .

  - Two enabled to use Skype for Business Server 2015.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio development system, and then press **F5**.

3.  The sample sends a "Hello World" message to the called party.

4.  Log on as the called party in Skype for Business 2015 and send an instant message to the user the sample is logged in as.

5.  Terminate the sample by sending a message with the text "bye" to the user that the sample is logged in as.


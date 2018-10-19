---
title: ConversationContextChannel (QuickStart)
TOCTitle: ConversationContextChannel (QuickStart)
ms:assetid: 629b6197-eb45-4810-99fe-66bd44881d21
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454821(v=office.16)
ms:contentKeyID: 65240094
ms.date: 07/27/2015
mtps_version: v=office.16
---

# ConversationContextChannel (QuickStart)


**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: ConversationContextChannel

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\ConversationContextChannel

## Description

The application initializes the platform to start listening on behalf of a user, accepts the first incoming instant messaging call, and then terminates the call. This is followed by tearing down the conversation, and then the platform. The application prints logging data to the console, and then quits, shutting down the platform normally. Note that the sample does not send a response to the instant message that it receives.

## Features

  - [ConversationContextChannel](https://msdn.microsoft.com/en-us/library/hh161849\(v=office.16\)) establishment

  - Sending context data to the remote user on the **ConversationContextChannel** instance

  - Receiving context data from the remote user on the **ConversationContextChannel** instance

## Prerequisites

  - .

  - A Skype for Business user who is logged on to Skype for Business Server 2015.

  - Skype for Business SDK installed on the computer where a user is signed on to Skype for Business.

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.

3.  Log on to a client (Skype for Business 2015) using the second user's credentials.

4.  Open the project in Visual Studio, and press **F5**.

## See also

#### Concepts

[Contextual communication](contextual-communication.md)

[Conversation context channel](conversation-context-channel.md)

#### Other resources

[Register Contextual Conversation Packages](https://msdn.microsoft.com/en-us/library/gg253680\(v=office.16\))


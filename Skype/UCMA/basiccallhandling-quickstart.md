---
title: BasicCallHandling (QuickStart)
TOCTitle: BasicCallHandling (QuickStart)
ms:assetid: c989d841-a7e8-4896-8dd0-479ac75bd571
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466144(v=office.16)
ms:contentKeyID: 65240084
ms.date: 07/27/2015
mtps_version: v=office.16
---

# BasicCallHandling (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: BasicCallHandling

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\BasicCallHandling

## Description

The application initializes the platform to start listening on behalf of a user, accepts the first incoming instant messaging call, and then terminates the call. This is followed by tearing down the conversation, and then the platform. The application prints logging data to the console, and then quits, shutting down the platform normally. Note that the sample does not send a response to the instant message that it receives.

## Features

  - Basic Call placement

  - Handling of an incoming instant messaging call

  - Call and Conversation cleanup and use

  - Call termination on an established call

## Prerequisites

  - .

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.

3.  Log in to a client (Skype for Business 2015) using the second user's credentials and send an instant message to the user whose credentials the endpoint is using.


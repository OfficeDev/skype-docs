---
title: SpeechRecognitionConnector (QuickStart)
TOCTitle: SpeechRecognitionConnector (QuickStart)
ms:assetid: 989144b9-9967-4bd2-99cf-71530111566d
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454828(v=office.16)
ms:contentKeyID: 65240080
ms.date: 07/27/2015
mtps_version: v=office.16
---

# SpeechRecognitionConnector (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: SpeechRecognitionConnector

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\SpeechRecognitionConnector

## Description

The application initializes the platform to start listening on behalf of a user, accepts the first incoming instant messaging call, and then ends the call. The application then tears down the call, conversation, and platform. The application prints log results to the console, and then exits, performing normal platform shutdown.


> [!WARNING]
> <P>The application runs indefinitely in an idle state until it is called.</P>



## Features

  - Call and conversation cleanup and use

  - Proper call termination on an established call

  - Basic incoming call handling

  - Basic instant messaging incoming call usage

  - **AudioVideoFlow** handling and control

## Prerequisites

  - Skype for Business Server 2015.

  - Two users capable of sending and receiving audio calls.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Replace the credentials in the variables at the beginning of the code sample with the credentials and server of the users from your Skype for Business Server 2015 topology.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.

3.  Send an instant message to the user whose credentials the endpoint is using.


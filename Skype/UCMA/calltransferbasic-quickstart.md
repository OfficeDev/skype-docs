---
title: CallTransferBasic (QuickStart)
TOCTitle: CallTransferBasic (QuickStart)
ms:assetid: 5a4b0a5a-4db1-45be-b9a7-c899066faaf0
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454816(v=office.16)
ms:contentKeyID: 65240089
ms.date: 07/27/2015
mtps_version: v=office.16
---

# CallTransferBasic (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: CallTransferBasic

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\CallTransferBasic

## Description

This sample involves communication between three users: a transferor, a transferee, and a transfer target. The sample logs in as the transferor, who is the user who receives an incoming audio-video call from the transferee, logged in to Skype for Business 2015.The transferor accepts the call from the transferee, and initiates a transfer to the transfer target. This sample demonstrates the ATTENDED transfer-type; hence after the transfer completes, the initial incoming call is disconnected.

The user can choose between attended or unattended transfers by changing the transfer-type in the code.

The difference between attended and unattended transfer is that unattended transfers begin the transfer (send the REFER to the caller) and terminate the initial call on receipt of the transfer request response (202-Reply) from the caller. Attended transfers, on the other hand, wait to terminate the call until the transfer either succeeds or fails. The application prints logging to the console, and then quits, shutting down the platform normally.

## Features

  - Call transfer, and established call activities

  - Basic call placement

  - Basic incoming audio/video call use

  - **AudioVideoFlow** handling and control

## Prerequisites

  - .

  - Three users capable of making and receiving audio calls.

  - The credentials for each user, and a client (such as Skype for Business 2015) capable of signing in to Skype for Business Server 2015.

  - A currently logged-in user on Skype for Business 2015, using one of the three users above, that will initiate the call sequence.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio development system, and then press **F5**.

3.  Make a voice call to the user whose credentials the endpoint is using.


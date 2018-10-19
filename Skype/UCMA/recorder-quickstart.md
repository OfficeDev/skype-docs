---
title: Recorder (QuickStart)
TOCTitle: Recorder (QuickStart)
ms:assetid: e76144e2-b8f4-47bf-8d39-4f079a376a88
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454822(v=office.16)
ms:contentKeyID: 65240079
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Recorder (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: Recorder

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\AudioVideoCall\\Recorder

## Description

The application initializes the platform and places a call to the specified user, records what the user says, and then tears down the platform. The application prints log results to the console, and then exits, performing normal platform shutdown.

## Features

  - Call and conversation cleanup and use

  - Basic call placement

  - Basic **Recorder** and **WmaFileSink** usage

## Prerequisites

  - Skype for Business Server 2015.

  - One user capable of sending and receiving audio calls.

  - The credentials for the user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Replace the credentials in the variables at the beginning of the code sample with the credentials and server of the users from your Skype for Business Server 2015 topology.

2.  Substitute the address of the called user in the code sample with the address of a valid, currently signed-in user capable of receiving audio calls.

3.  Open the project in Microsoft Visual Studio, and then press **F5**.


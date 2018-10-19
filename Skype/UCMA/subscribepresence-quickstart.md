---
title: SubscribePresence (QuickStart)
TOCTitle: SubscribePresence (QuickStart)
ms:assetid: 0d48b640-c58c-442a-9e13-2ff1d9dc5397
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454835(v=office.16)
ms:contentKeyID: 65240101
ms.date: 07/27/2015
mtps_version: v=office.16
---

# SubscribePresence (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: SubscribePresence

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\SubscribePresence

## Description

The application initializes the platform and endpoint and subscribes to the presence of a target user. Throughout the process, the application listens for incoming notifications from a user logged on to Skype for Business 2015 and reflects the updated state of that user in the console.

## Features

  - Presence subscription

  - Monitoring of presence-related notifications

## Prerequisites

  - .

  - Two users, enabled to use Skype for Business Server 2015.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Supply the configuration settings to be used by the sample in the accompanying App.config file, you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio development system, and then press **F5**.

3.  Change the presence of the user logged on to Skype for Business 2015, and see the presence change in the console.


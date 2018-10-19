---
title: SubscribePresenceView (QuickStart)
TOCTitle: SubscribePresenceView (QuickStart)
ms:assetid: 4a3d3894-80f2-4553-b719-25cc7fce92fc
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454838(v=office.16)
ms:contentKeyID: 65240102
ms.date: 07/27/2015
mtps_version: v=office.16
---

# SubscribePresenceView (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: SubscribePresenceView

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\SubscribePresenceView

## Description

The application initializes the platform and endpoint and subscribes to a target user. The application uses two [RemotePresenceView](https://msdn.microsoft.com/en-us/library/hh381152\(v=office.16\)) objects, each configured with different [RemotePresenceViewSubscriptionMode](https://msdn.microsoft.com/en-us/library/hh381952\(v=office.16\)) values: **Persistent** and **Polling**.

After the subscription is complete, the application listens for incoming notifications from a user logged in to Skype for Business 2015 and displays the notifications in the console.

## Features

  - Creation and configuration of **RemotePresenceView** objects

  - Presence subscription using a **RemotePresenceView** object

  - Monitoring of presence-related notifications

## Prerequisites

  - .

  - Two users, enabled to user Skype for Business Server 2015.

  - The credentials for each user, and a client capable of signing in to Skype for Business Server 2015.

  - A client signed in to Skype for Business Server 2015.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.

3.  Change the presence of the user signed in to Skype for Business 2015, and see the presence change in the console.


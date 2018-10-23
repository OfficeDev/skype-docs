---
title: PublishPresence (QuickStart)
TOCTitle: PublishPresence (QuickStart)
ms:assetid: d3ae55a9-9cf6-40aa-9cab-33aeceb2ae37
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454837(v=office.16)
ms:contentKeyID: 65240100
ms.date: 07/27/2015
mtps_version: v=office.16
---

# PublishPresence (QuickStart)

**Applies to**: Skype for Business 2015

**Sample name**: PublishPresence

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\PublishPresence

## Description

The application initializes the platform and a user endpoint and subscribes to the sample user's presence (self-presence). The sample then publishes the user state, machine state, note, and contact card. The note is published using raw XML, while all other categories are published using strongly typed objects. The sample then deletes the presence categories that it published, terminates the endpoint, and shuts down the platform, exiting normally.

To see the categories being published, you can log on to a client (such as Skype for Business 2015), as the same user as the sample is using.

## Features

  - Presence publication using a grammar and strongly-typed categories.

## Prerequisites

  - One user, enabled to use Skype for Business Server 2015.

  - The credentials for that user.

## Running the sample

1.  Supply the configuration settings to be used by the sample in the accompanying App.config file, you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio development system, and then press **F5**.

3.  Change the presence of the user logged on to Skype for Business 2015, and see the presence change in the console.


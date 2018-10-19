---
title: ConferenceEscalation (QuickStart)
TOCTitle: ConferenceEscalation (QuickStart)
ms:assetid: f9865881-2532-4e22-83dc-e4db22a294ed
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454824(v=office.16)
ms:contentKeyID: 65240091
ms.date: 07/27/2015
mtps_version: v=office.16
---

# ConferenceEscalation (QuickStart)


_**Applies to:** Skype for Business 2015_

**In this article**  
Description  
Features  
Prerequisites  
Running the sample  

Sample name: ConferenceEscalation

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\ConferenceEscalation

## Description

The application initializes a platform and two endpoints, each on behalf of a different user. The endpoints establish a peer-to-peer instant messaging call between themselves. One of the endpoints escalates the instant messaging call into a conference. After escalation, the conversation is used for basic command and control, including user privilege elevation, conference locking, and ejection from conferences. After the last of the command and control activities complete, the application quits, shutting down the platform normally.

## Features

  - *Ad hoc* conference initialization

  - Conference command and control using the **ConferenceServices** API

  - **AudioVideoCall** conference use

## Prerequisites

  - .

  - Two users enabled to use Skype for Business Server 2015.

  - The credentials for each user.

## Running the sample

1.  Supply the configuration settings to be used by the sample in the accompanying App.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio, and then press **F5**.


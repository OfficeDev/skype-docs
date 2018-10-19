---
title: Auto-provisioning and PowerShell activation
TOCTitle: Auto-provisioning and PowerShell activation
ms:assetid: 95c3c4ca-18d8-4759-8b06-94ac3ceb8c85
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465932(v=office.16)
ms:contentKeyID: 65239810
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Auto-provisioning and PowerShell activation


_**Applies to:** Skype for Business 2015_

Application activation is the process in which configuration data is written to the Central Management Store database, Active Directory forest, and local computer. Microsoft Unified Communications Managed API 5.0 includes Windows PowerShell activation, which makes it simple to activate or deactivate an application. UCMA 4.0 also introduced automatic provisioning (auto-provisioning), to simplify the task of monitoring and reacting to changes in configuration data.

Activating a UCMA 5.0 application against Skype for Business Server 2015 typically involves setting a trust relationship between the application and Skype for Business Server 2015, and setting up routing-specific configurations. PowerShell-based programmable activation enables application developers to easily integrate the details of UCMA 5.0 activation into their overall application activation.

Automatic provisioning provides an abstraction of Skype for Business Server 2015 data sources, using an object model already familiar to developers. Using automatic provisioning, developers can create applications that monitor and react to Skype for Business Server 2015-specific configuration changes that are detected by the UCMA 5.0 platform.

In addition to Skype for Business Server 2015-specific trust and route settings, most applications will need to provision data that is specific to their application. Application developers are responsible for storing and managing any such data in their own application store, and to create the bindings between their application store and the Central Management Store so that the two data sets can be reconciled.

For more information, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md). The topics in the referred-to section also discuss PowerShell activation.


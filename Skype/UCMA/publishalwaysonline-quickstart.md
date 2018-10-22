---
title: PublishAlwaysOnline (QuickStart)
TOCTitle: PublishAlwaysOnline (QuickStart)
ms:assetid: cf7992a1-db45-417d-b0bc-b4e896779c8c
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454833(v=office.16)
ms:contentKeyID: 65240099
ms.date: 07/27/2015
mtps_version: v=office.16
---

# PublishAlwaysOnline (QuickStart)


**Applies to**: Skype for Business 2015



Sample name: PublishAlwaysOnline

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\PublishAlwaysOnline

## Description

Based on the Application ID specified, the sample initializes the platform and any trusted application endpoints corresponding to the provisioned application. Upon establishment, the endpoints publish a static presence of "alwaysonline" by setting the [AutomaticPresencePublicationEnabled](https://msdn.microsoft.com/en-us/library/hh381653\(v=office.16\)) property on an [ApplicationEndpointSettings](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpointsettings?view=ucma-api) object to true. The sample establishes as many application endpoints as have been provisioned on the particular trusted application within the deployment.

For more information about provisioning trusted applications and endpoints in , see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md) as well as [General application activation](general-application-activation.md) and [Activating a manually-provisioned application](activating-a-manually-provisioned-application.md).

Throughout the lifetime of the process, the sample application listens for the **StateChanged** event on the application endpoint and reflects the endpoint state changes in the console.

## Features

  - 
  - Custom presence publication

  - **ApplicationEndpoint** establishment

  - **ApplicationEndpointSettings** creation by registering a delegate with **RegisterForApplicationEndpointSettings**

  - Endpoint state change notification handling

## Prerequisites

  - Skype for Business Server 2015.

  - Provisioned trusted application endpoint.

## Running the sample

1.  Supply the user credentials in the accompanying app.config file, or you will be prompted for them when you run the sample.

2.  Open the project in Microsoft Visual Studio development system, and then press F5.


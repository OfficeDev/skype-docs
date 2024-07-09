---
title: Skype for Business App SDK
description: Introduction to the Skype for Business App SDK
ms.date: 07/03/2024
---
# Skype for Business App SDK

 **Last modified:** July 3, 2024

The Skype for Business App SDK has been retired and is not available for download and use. Instead, we recommend [Azure Communication Services](/azure/communication-services/concepts/sdk-options), where we will continue to invest our development resources.

## Migrating to Azure Communication Services

Azure Communication Services is a cloud-based communications service that lets you add voice, video, chat, and telephony to your apps. ACS applications have the ability to join Teams meetings (as a guest), and we encourage implementers of the SfB App SDK to transition to Azure Communication Services. The ability to [join Teams meetings as a guest using ACS](/azure/communication-services/concepts/join-teams-meeting) is now available.

### Azure SDKs that join Teams meetings

Azure Communication Services has multiple SDKs that can join Teams meetings listed below.

|SDK| Implementation Complexity| Customization Ability| Calling| Chat|
|---|---|---|---|---|
|[Core SDKs](/azure/communication-services/concepts/sdk-options)|High|High|✔|✔ |
|[Base Components](/azure/communication-services/concepts/ui-framework/ui-sdk-overview)|Medium|Medium|✔|✔|
|[Open-Source Composite](/azure/communication-services/concepts/ui-framework/ui-sdk-overview)|Low|Low|✔|✔|

More specific guidance for transitioning a SfB Mobile SDK implementation to ACS Meetings:

1. Explore key ACS concepts:

- [Overview](/azure/communication-services/overview)
- [Identity](/azure/communication-services/concepts/identity-model)
- [Client/server architecture](/azure/communication-services/concepts/client-and-server-architecture)
- [Authentication](/azure/communication-services/concepts/authentication?tabs=csharp)
- [UI Library](/azure/communication-services/concepts/ui-library/ui-library-overview)

2. [Create an Azure Communication Services resource](/azure/communication-services/quickstarts/create-communication-resource) in the Azure portal
3. [Allocate user access tokens](/azure/communication-services/quickstarts/access-tokens)
4. Use the Calling SDK to implement chat and calling communication in your app. [Samples](/azure/communication-services/samples/calling-hero-sample) are available to help get started.

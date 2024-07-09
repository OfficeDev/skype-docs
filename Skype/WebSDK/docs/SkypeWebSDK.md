---
title: Skype Web SDK
description: 'Get an overview Skype Developer Platform for Web.'
ms.date: 03/29/2022
---

# Skype Web SDK

We will be retiring the Skype Developer Platform for Web ("Skype Web SDK") on September 30, 2024. After that time the SDK will no longer be available for download. Instead, we recommend [Azure Communication Services](/azure/communication-services/concepts/sdk-options), where we will continue to invest our development resources.

## Skype for Business Online Retirement

Azure Communication Services (ACS) is a cloud-based communications service that lets you add voice, video, chat, and telephony to your apps. Azure communication services allow developers to extend their applications with Teams interoperability, where users [communicate as guests](/azure/communication-services/concepts/join-teams-meeting) (Teams anonymous users) or [communicate as Teams users](/azure/communication-services/concepts/teams-endpoint). We encourage implementers of the SfB Web SDK to transition to Azure Communication Services and Microsoft Graph APIs. Microsoft Graph APIs control behavior specifically in the M365 ecosystems, such as presence. It also provides chat support for Teams users.

The table below maps SfB Web SDK sub-systems to Graph and ACS capabilities.

| SfB SDK System | Description  | Alternative   |
|----------|-----------|------------|
| Presence | Use presence information to help users decide whether and how they should person other users. | [Graph Presence APIs](/graph/api/resources/presence)  |
| Local user | Use the **mePerson** object to represent the currently signed-in user. | [Graph User APIs](/graph/api/user-get)  |
| Conversations | Use conversation services to determine the ways for communication between persons. | [ACS Calling SDK for voice/video](/azure/communication-services/concepts/sdk-options) and [Graph for chat](/graph/api/resources/chat) |
| Listening for and generating presence events | Use events to get a person's current presence. | [Graph Presence APIs](/graph/api/resources/presence.md)  |
| Persons | Use person objects to represent individual users. | [Graph User APIs](/graph/api/user-get)  |
| Devices | Select Cameras, Microphones, and Speakers to use for audio and video conversations. | [ACS Calling SDK](/azure/communication-services/how-tos/calling-sdk/manage-video#device-management.md)  |

Azure Communication Service SDKs are designed with a substantially different API model than SfB Web, and we do not host the SDK on a content delivery network (CDN). Your web application hosts the SDK, and you have the choice of using one of several SDKs listed below. The core Calling and Chat SDKs have essentially no GUI and allow for robust customization of the end-user experience. But for most SfB Web SDK implementations, using the open-source composites is recommended to reduce the amount of UI development required by replacing SfB.

| **SDK** | **Implementation Complexity** | **Customization Ability** | **Calling** | **Chat** |
|---------|-------------------------------|---------------------------|-------------|----------|
| [Core SDKs](/azure/communication-services/concepts/sdk-options) | High | High | ✔ | ✔ |
| [Base Components](/azure/communication-services/concepts/ui-framework/ui-sdk-overview) | Medium | Medium | ✔ | ✔ |
| [Open-Source Composite](/azure/communication-services/concepts/ui-framework/ui-sdk-overview) | Low | Low | ✔ | ✔ |

To use Graph and ACS SDKs as an SfB Web SDK replacement, you will leverage these features:

| Product   | **Feature**                                                                             | **Available**     |
|:----------|:----------------------------------------------------------------------------------------|:----------------|
| **Graph** | [Log-in a user to M365](/graph/auth.md)                                                 | ✔ |
| **Graph** | [Set presence](/graph/api/presence-setpresence.md)                                      | ✔ |
| **Graph** | [Get Presence](/graph/api/presence-get.md)                                              | ✔ |
| **Graph** | [Send and receive chat messages](/graph/api/chatmessage-post.md)                        | ✔ |
| **Azure** | [Create an ACS token for an M365 user](/azure/communication-services/quickstarts/manage-teams-identity)                                                    | ✔  |
| **Azure** | [M365 user can call a user in Teams](/azure/communication-services/quickstarts/voice-video-calling/get-started-with-voice-video-calling-custom-teams-client)                                                      | ✔  |
| **Azure** | [M365 user can join a Teams meeting, interface with the video, audio, and chat channels](/azure/communication-services/quickstarts/voice-video-calling/get-started-teams-interop)  | ✔  |
| **Azure** | [M365 user can initiate PSTN calls from Teams-allocated phone numbers](/azure/communication-services/how-tos/cte-calling-sdk/manage-calls)                    | ✔  |
| **Azure** | [M365 user can receive PSTN calls from Teams-allocated phone numbers](/azure/communication-services/how-tos/cte-calling-sdk/manage-calls)                     | ✔  |

---
title: Skype for Business Bot - hybrid environment support
description: Learn how Skype for Business bots can be connected to the Skype for Business Server users if hybrid connectivity has been deployed in the environment.
ms.date: 03/30/2022
---

# Skype for Business Bot - hybrid environment support

Skype for Business bots can be connected to the Skype for Business Server users if hybrid connectivity has been deployed in the environment.

Hybrid connectivity between Skype for Business Server and Skype for Business Online means users of a domain, such as contoso.com, are split between using Skype for Business Server on-premises and Skype for Business Online. Some of the domain users are homed on-premises, and some users are homed online. Bots will be configured as online users reachable by the on-premises users.  

## Getting started

- For more information about how to deploy hybrid connectivity between Skype for Business Server and Skype for Business Online, see [Deploy hybrid connectivity between Skype for Business Server and Skype for Business Online](/skypeforbusiness/skype-for-business-hybrid-solutions/deploy-hybrid-connectivity/deploy-hybrid-connectivity).

- Configuring hybrid connectivity requires **Active Directory synchronization** to keep your on-premises and online users synchronized. **[Azure AD Connect](/azure/active-directory/connect/active-directory-aadconnect)** is the best way to connect your on-premises directory with Azure Active Directory (Azure AD) and Office 365. For more information about using Azure AD Connect for hybrid environment configuration, see [Integrate your on-premises directories with Azure Active Directory](/azure/active-directory/connect/active-directory-aadconnect).

- Read [Skype for Business Bot - Common Errors](Bot-Common-Errors.md) to troubleshoot some of the common errors encountered during the Skype for Business Bot setup.

## Bot setup for Skype for Business hybrid environment

After you have successfully deployed the hybrid environment, follow these steps to build and enable a Skype for Business bot:

1. Create the bot using the [Microsoft Bot Framework](https://dev.botframework.com/). See [Creating a Skype for Business bot](overview.md) section for details.

2. Launch the [Connecting your bot to Skype for Business Online](/azure/bot-service/bot-service-channel-connect-skype) page and follow all the instructions to add your bot to Skype for Business Online. You will be required to sign in as a Tenant Administrator of the Skype for Business Online environment and run the **New-CsOnlineApplicationEndpoint** PowerShell cmdlet.

    ```powershell
        New-CsOnlineApplicationEndpoint -ApplicationID <AppID generated from Bot Framework Portal like 41ec7d50-ba91-1207-73ee-136b88859725> -Name <NameOfTheBot> -Uri sip:<bothandle@yourdomain.com>
    ```

    For the **Skype for Business Hybrid environment**, the **New-CsOnlineApplicationEndpoint** cmdlet will output an additional on-premises cmdlet to be run in your [Skype for Business Server (on-premises) Management Shell](https://technet.microsoft.com/library/gg398474.aspx). The additional cmdlet is covered in more detail in the next step.

    > [!NOTE]
    > Read [Skype for Business Bot - Common Errors](Bot-Common-Errors.md) to troubleshoot some of the common Bot setup issues.

3. Create an application endpoint on the [Skype for Business Server (on-premises) Management Shell](https://technet.microsoft.com/library/gg398474.aspx) using the following on-premises cmdlet:  

    ```powershell
      New-CsHybridApplicationEndpoint -ApplicationId <AppID generated from Bot Framework Portal like 41ec7d50-ba91-1208-73ee-136b88859725> -DisplayName <NameOfTheBot> -SipAddress sip:<bothandle@yourdomain.com> –OU <ou=Redmond,dc=litwareinc,dc=com>
    ```

    > [!NOTE]
    > Ensure that the **New-CsHybridApplicationEndpoint** parameters: ApplicationId, DisplayName, and SipAddress have the same values as (step 2) **New-CsOnlineApplicationEndpoint** parameters: ApplicationID, Name and Uri, respectively.
    >
    > **Skype for Business Server Cumulative Update 5 or greater** is required to run this cmdlet.

    |**Parameters**|**Required**|**Type**|**Description**|
    |:-----|:-----|:-----|:-----|
    |ApplicationId|Required|Guid|The ApplicationId or Client Id for which the endpoint is being created.|
    |DisplayName|Required|String|Friendly name for the application endpoint.|
    |SipAddress|Required|String|The SipUri for the Endpoint. SIP Uri must be lowercase.|
    |LineUri|Optional|String|Valid phone number for the application endpoint. (Not currently supported through BOT framework.)|
    |OU|Required|String|Azure Directory Organizational Unit (OU) of the user.|

    The successful execution of the **New-CsHybridApplicationEndpoint** cmdlet will create a disabled user object on Active Directory and show a **"Successfully initiated provisioning of application endpoint on-prem"** message.

4. Wait for the newly created user object to be directory synced to the Azure Active Directory or start a new directory sync cycle by running the [Start-ADSyncSyncCycle](/azure/active-directory/connect/active-directory-aadconnectsync-feature-scheduler#start-the-scheduler.md) on the domain controller machine. To learn more about Azure AD Connect directory sync, see [Azure AD Connect sync: Scheduler](/azure/active-directory/connect/active-directory-aadconnectsync-feature-scheduler) and [Integrate your on-premises directories with Azure Active Directory](/azure/active-directory/connect/active-directory-aadconnect).

5. Ensure that you wait for 8 hours before the endpoint is discovered from the Skype for Business clients for the newly created application ids. An on-premises user should be able to search for the BOT from the client and initiate the chat conversations.

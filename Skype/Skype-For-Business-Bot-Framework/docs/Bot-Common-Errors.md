---
title: Skype for Business Bot - common errors
description: This article lists the most common errors encountered during the Skype for Business bot setup and what you can do to solve them.
ms.date: 04/11/2022
---

# Skype for Business Bot - common errors

This article lists the most common errors encountered during the Skype for Business bot setup and what you can do to solve them.

 
## New-CsOnlineApplicationEndpoint cmdlet-related errors

|Error|Resolution|
|:---|:---|
|**+ FullyQualifiedErrorId : Error processing cmdlet request**<br/><br/> "type":"Microsoft.Skype.EnterpriseVoice.CbdService.DataAccess.**EntityNotFoundException**"|Add a Url for CallbackUri in the messaging Url field in botframework properties.|
|**+ FullyQualifiedErrorId : Could not get application endpoint or the Uri is already present as a User in BVD**|Delete the existing user account with the same sipuri in tenant, or run the cmdlet using a sipuri that does not already exist in the tenant.|
|**+ FullyQualifiedErrorId : Error processing cmdlet request**<br/><br/>"message":"Validation failed on entity"<br/><br/>"type":"System.ArgumentException"<br/><br/>"message":"SipUri cannot be NULL or Empty and it has to be lowercase"<br/><br/>"type":"System.ComponentModel.DataAnnotations.ValidationException"|This error is caused when the  *New-CsOnlineApplicationEndpoint* -Uri sip parameter value has uppercase characters. Use all lowercase for -Uri sip parameter.|
|**+ FullyQualifiedErrorId : Error processing cmdlet request**<br/><br/>"type":"Microsoft.Rtc.Management.Hosted.PlatformService.ProvisioningLibrary.ApplicationEndpointProvisioningException was thrown."|This error is caused by timing issues in the provisioning. Sometimes this error is also seen with *Set-CsOnlineApplicationEndpoint -Uri*, although the issue resolves itself after a few minutes.<br/><br/>Run `Set-CsOnlineApplicationEndpoint -Uri <app@domain.com>` followed by `Get-CsOnlineApplication -Uri <app@domain.com>` to verify that there are no issues.|
|**+ FullyQualifiedErrorId : Error processing cmdlet**<br/><br/>**+ FullyQualifiedErrorId : {"odata.error":{"code":"Request_BadRequest","message":{"lang":"en","value":"Property immutableId is invalid."}**|This error is caused by running a cmdlet on a hybrid topology with a federated domain. Workaround is to use a non-federated domain.|
|Processing data from remote server failed with the following error message: **The user 'usera@contoso.com' does not have permission to manage this tenant.**|To add your bot to Skype for Business, you must sign in as the tenant administrator of a Skype for Business Online environment. For more information, see [About the Skype for Business admin role](/skypeforbusiness/skype-for-business-online?redirectSourcePath=%252fen-us%252farticle%252fAbout-the-Skype-for-Business-admin-role-aeb35bda-93fc-49b1-ac2c-c74fbeb737b5).|


## Other common errors

|Error|Resolution|
|:---|:---|
|There was an error sending the message to your bot.<br/><br/>HTTP status code Gateway Timeout|SFB can only call back to 443 port for external URLs. SFB does not allow other ports for security reasons.<br/><br/>Set the bot callback to listen on 443 port.|
|In a federation/multi-tenant scenario, external users are unable to contact the bot. |Skype for Business bots can only communicate to users in the same domain/tenant. In most cases, we recommend that a bot “instance” be created in each domain/tenant.|
|Skype for Business bot presence appears as "Unknown".|The Bot Framework Skype for Business Channel is in Developer Preview and does not currently support presence.|
|Skype for Business bot display picture is not set.|The Bot Framework Skype for Business Channel is in Developer Preview and does not currently support bot display pictures.|
 
 
 
 
 
 
 
 
 

# Application permissions

This article is a reference of permissions or roles that can be granted to an application to access specific set of capabilities.

Application Permissions or Roles limit the access of your application to Skype for Business Online to a specific set of capabilities.  To configure your app for Skype for Business Online, you first need to specify which permissions your app requires. You specify these permissions in [Azure Active Directory registration](./RegistrationInAzureActiveDirectory.md) for your app using the [Azure management portal](http://manage.windowsazure.com) for **Microsoft Azure Active Directory** (Azure AD). Select the application permissions type, as these are directly assigned to the application.

 
The following application permissions are available to Skype for Business Online service applications:
 
|Application Permission|Description|
| ------------- |---|
|Join and Manage Skype Meetings (preview) | Allows the app to join and manage Skype meetings|
|Create on-demand Skype meetings (preview)|Allows the app to create on-demand Skype meetings (short term expiry)
|Send/Receive Instant Messages (preview)|Allows the app to send and receive instant messages; and manage instant messaging service scenarios
|Send/Receive PSTN (preview)|Allows the app to send and receive voice calls; and manage PSTN service scenarios
|Send/Receive Audio and Video (preview)|Allows the app to send and receive audio and video; and manage audio/video service scenarios
|Guest user join services (preview)|Allows the app create an on-demand Skype meeting and join guest users into Skype for Business services

 
>**Note:** that these capabilities are selected in the [AAD management portal](http://manage.windowsazure.com) during the Service Application's [Registration in Azure Active Directory](./RegistrationInAzureActiveDirectory.md) and have [Tenant Admin Consent](./TenantAdminConsent.md), before they can be used for a tenant.
 
 
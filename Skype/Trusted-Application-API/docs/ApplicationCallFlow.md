# Service application initiated chat conversations

When a service application starts a chat conversation, it sends requests to its Trusted Application (Trusted Application) endpoint to discover and use the capabilities in the **Trusted Application API**s. Messaging capabilities must be supported in the application endpoint to enable chat conversations.
  
## Configure a service application for chat
A service application must be configured in Azure to request instant messaging permissions before it can call Trusted Application messaging APIs. The following actions are taken to configure a service application:

- Open the [Azure Active Directory (AAD) management portal](https://manage.windowsazure.com) and choose the Active Directory node from the tool bar on the left side of the window.
- Choose the directory that contains the service application that you are configuring.
- Choose the applications tab and then choose the application you are configuring. 
- Select the **Send/Receive Instant Messages (preview)** Application Permission. 

   >Note: This process is covered in greater detail in [Registering your application in Azure AD](./RegistrationInAzureActiveDirectory.md)

- Get tenant administrator consent to grant the service application permission request.  See [Tenant Admin Consent.](./TenantAdminConsent.md)

The Service Application now can send requests to the **Trusted Application API**s to use the "Send/Receive Instant Messages (preview)" capability for that tenant.
 
## Service application initiated chat Callflow example
Let us go through this call flow with a simple example:
 
The Service Application uses the **Trusted Application API**s to send an instant message to a user in SFB online. The user's sip uri is already known to the Service Application via some other means.

The details of this example are available in [Messaging Call Flow](./MessagingCallFlow.md).

# Customer care client initiated chat conversations

 
When a client sends a request to the tenant's service application endpoint, the **Trusted Application API** starts the call flow by invoking the registered callback uri on the service application endpoint. 

>Note: The service application default callback url is set while [Registering a Trusted Application in Skype for Business Online](./SfBRegistration.md).
 
## Callflow example

Let us go through this call flow with an example:
 
Litware is a service application (SA) developer that sells a chat support infrastructure to Skype for Business (SfB) online tenants. The chat infrastructure includes a service application and a chat widget. 

Contoso is a Litware customer that uses Litware's product to connect chat support requests directed to Contoso's customer service representatives. The representatives use a Skype for Business Online client. Contoso developers added Litware's chat help widget to a Contoso owned product webpage. 

Contoso developers used the [Azure Active Directory](https://manage.windowsazure.com) (AAD) management portal to request meeting permissions for chat. See [Registration in Azure Active Directory](RegistrationInAzureActiveDirectory.md) for the details of registration.

The administrator of the Contoso tenant needs to consent to the application. See  [Tenant Admin Consent](./TenantAdminConsent.md).
 
With configuration complete, the Litware service application can send requests to the **Trusted Application API** to use chat capabilities for this scenario in the tenant.

### Callflow details

- Litware chat widget loads in the Contoso page and sends an HTTP request to the Litware service application endpoint to get an authentication token. The token is inserted in requests from the Litware chat widget to the **Trusted Application API**
- A Contoso page visitor clicks on the chat support link in the Litware widget. The widget sends a request to a Trusted Application endpoint. 
   >Note: Litware provisioned the chat widget with the Contoso's application endpoint which was registered during ["Registration in SFB Online"](./SfBRegistration.md)
- On receiving a callback, the Litware SA creates an on demand meeting on the **Trusted Application API**. It adds customer support agents to the meeting to handle the visitor's chat request. The **Trusted Application API** bridges the visitor chat messages into the meeting.

   >Note: The API has a messaging filter feature, which can be used to allow/block messages from a customer support agent from being sent to the visitor, but still sent to the other customer support agents in the conference.
- The meeting is terminated by the SA when the visitor has ended the session.


 
 
### Required meeting permissions  
|||
| ------------- |---|
|Join and Manage Skype Meetings (preview) | Allows the app to join and manage Skype meetings|
|Create on-demand Skype meetings (preview) | Allows the app to create on-demand Skype meetings (short term expiry)
|Send/Receive Instant Messages (preview)|Allows the app to send and receive instant messages; and manage instant messaging service scenarios
|Guest user join services (preview)|Allows the app create an on-demand Skype meeting and join guest users into Skype for Business services
 
## Implementing the customer chat scenario

The following topics expand on the customer chat scenario callflows.

- [Bootstrap chat widget](BootstrapChatWidget.md)
- [Discovery by chat client](DiscoveryChatClient.md)
- [Cross Origin Requests from browser based chat clients](CORChatClient.md)
- [Customer Chat care call flow](ChatCallflowDetail.md)
- [Implementing a Chat Client with the Skype Web SDK](ImplementingChatClientWithSkypeWebSDK.md)
 

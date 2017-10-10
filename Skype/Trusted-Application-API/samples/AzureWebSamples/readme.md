# Trusted Application - Azure web app samples

These quick-start Trusted Application Azure web app samples can be  deployed to an Azure Cloud Service to run simple Trusted Application scenarios in just few simple steps. 

- **AudioVideoIVRSample** - This sample plays an audio promt and enables the PSTN incoming IVR with DTMF recognition.

- **AvBridgeSample** - This sample creates an adhoc meeting, bridges the incoming PSTN call to the meeting, plays a prompt and invites agent mentioned in sample's web.config file to the meeting.

- **AvBridgeToSipUriSample** -  This sample bridges the incoming PSTN call to the agent mentioned in sample's web.config file.

- **CallForwardSample** -  This sample forwards an incoming PSTN call to the agent mentioned in sample's web.config file.

- **CallTransferSample** - This sample enables PSTN incoming call with an audio prompt and transfers the call to a SIP user URI as mentioned in sample's web.config file.

- **IMBridge** - This sample creates an adhoc meeting, bridges the incoming IM call to the meeting, sends a welcome message, adds the agent mentioned in web.config as a bridged participant to the meeting (this allows caller to see messages from the agent), adds the agent to the meeting, if the original IM invite contained any custom content it is sent as a message to the conference.

>Note: See the [Key programming concepts](https://msdn.microsoft.com/en-us/skype/trusted-application-api/docs/newconcepts) to know more about [adhoc meeting](https://msdn.microsoft.com/en-us/skype/trusted-application-api/docs/newconcepts#adhoc-meeting), [conversation bridge](https://msdn.microsoft.com/en-us/skype/trusted-application-api/docs/newconcepts#conversation-bridge) and other new programming concepts. 

<a name="prerequisite"></a>
## Prerequisite 

1. **Aquire the service phone number**
   
    In the upcoming section, You will need to register the application in Azure Active Directory and assign a service phone number to the application's trusted endpoint. So make sure that you complete the steps mentioned in [Acquire the service phone number](../../docs/AquireServiceNumber.md)  before proceeding.

2. **Clone the samples and restore the NuGet packages**
3. **Deploy the sample to Azure App Service** 

    These samples run in a cloud service such as Azure. Please follow the steps in [Publish To Azure](https://docs.microsoft.com/en-us/azure/app-service-web/app-service-web-get-started-dotnet#publish-to-azure) to deploy the sample app to Azure app service. Publishing the sample without configuring the web.config file will launch the sample application in your default browser with a **“Server Error in ‘/’ Application”** error. Ignore this error for now and  **save the address bar URL** for use in the upcoming section, for example: **http://audiovideoivrsample20170515115729.azurewebsites.net/**.

      ![alt text](../../docs/images/AzureWebAppPic1.png "image")

## Getting Started
<a name="Registration"></a>
### 1. Registration

Using the [quick registration tool](https://aka.ms/skypeappregistration) for registering Skype for Business Trusted Applications in Azure and Skype for Business Online, eliminates the need to register an Application manually in Azure portal. The steps are as follows.

a) **Sign in** - Use an account which has azure subscription available to sign in to [quick registration tool](https://aka.ms/skypeappregistration)

b) **Name of your application** - Set the name to  your desired application name.

c) **App ID URI and Callback uri for your application** - Retrieve the saved URL from the step 3 of **[prerequisite](#prerequisite)** and append **/callback** to it. Be sure that the Callback uri is **_https_**, for example: **https://audiovideoivrsample20170515115729.azurewebsites.net/callback**. Set this URL as your application's **Callback uri**.

 You do not need to change the prefilled value of **App ID URI** for these samples. 

d) **Application Permissions** - Select the relevant permissions for the sample application.

e) **Create my application** - This step successfully registers your application with Azure and generates an Application ID and Client Secret for your application. **Please save  App ID URI, Callback uri, Application ID, and Client Secret somewhere safe for future use.**

>NOTE: You can optionally manually register your application in Azure Portal, where you will get a Client ID and set an App ID URI. Refer to [Registration in Azure Active Directory](https://github.com/OfficeDev/skype-docs/blob/master/Skype/Trusted-Application-API/docs/RegistrationInAzureActiveDirectory.md) for details.

<a name="Provisioning"></a>
### 2.  Tenant admin consent and Trusted Endpoint creation

The final application registration step 3 of [quick registration tool](https://aka.ms/skypeappregistration)   includes Tenant Admin consent and Trusted Endpoint setup.

a) **Tenant admin consent**

When the application is registered in AAD, it is registered in the context of a tenant.  For a tenant to use the Service Application, for example, when the application is developed as a multi-tenant application, it must be consented to by that tenant's admin. 
   
Follow the instructions in step 3 of [quick registration tool](https://aka.ms/skypeappregistration) to provide the tenant consent. See the [Tenant Admin Consent](https://github.com/OfficeDev/skype-docs/blob/master/Skype/Trusted-Application-API/docs/TenantAdminConsent.md) for more detail.

 >Note: Ignore the **Page not found** screen after accepting the permissions.
    
b) **Trusted endpoint setup**
      
Setup application's endpoint by using the following command in the Skype for Business Admin PowerShell. Update the placeholder parameter to a desired SIP Address within your tenant domain. e.g. helpdesk@contoso.com 
    
>Note: These samples require you to assign a service phone number to the applications's endpoint. Anyone can call this number to reach  your application. Be sure to complete the steps in [Aquire the service phone number](../../docs/AquireServiceNumber.md) page before running the following PowerShell cmdlet. 
    
```PowerShell
  New-CsOnlineApplicationEndpoint -Uri "sip:sample_endpoint@domain.com" -ApplicationId "Your_Application_ID" -Name "avbridgesample" -PhoneNumber Your_Service_Number
```
    
>Note: See the [Setting up a Trusted Application Endpoint](../../docs/TrustedApplicationEndpoint.md) page for more detail on PowerShell cmdlets and related parameters. 

## Running the sample

1. Open the sample project with Visual Studio

2. Modify the web.config as follow:
    
    
| Key  |  Replace the value with  |
| ------------- |:-------------:|
| MyAudienceUri   |Application's App ID URI (refer [Registration](#Registration) section)| 
|MyCallbackUri |Application' s Callback uri (refer [Registration](#Registration) section)|
| ApplicationEndpointId |The Trusted endpoint's sip uri ID(refer [Trusted endpoint setup](#Provisioning) section)|
| AAD_ClientId |Application's ID (refer [Registration](#Registration) section)|
| AAD_ClientSecret |Application's Client Secret value (refer [Registration](#Registration) section)|
|MyAgentList/MyAgent|sip uri of any agent who should receive the incoming calls|
|MyPhoneNumber|Service phone number assigned to the application's Trusted endpoint(refer [Trusted endpoint setup](#Provisioning) section) |

>Note: The **sip uri** for **ApplicationEndpointId** and **MyAgentList/MyAgent** should belong to the same tenant domain. e.g. ApplicationEndpointId = helpdesk@contoso.com and MyAgentList = abc@contoso.com  

3. Redeploy the sample. See the [Update the app and redeploy](https://docs.microsoft.com/en-us/azure/app-service-web/app-service-web-get-started-dotnet#update-the-app-and-redeploy) section for more detail.

4. Call the service phone number associated with the application 

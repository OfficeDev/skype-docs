# About these samples
The Trusted Application API Samples contain samples for the trusted agent (server-side) as well how to interact with the trusted application from a client-side browser.

The trusted agent sample deploys to an Azure Cloud Service and:
- Permits anonymous sign-in
- Sends IM messages to other Skype for Business users
- Handles incoming PSTN calls

The sample web application interacts with the agent sample and contains two demos:
- IMBridge: Obtains a token from the trusted application to perform anonymous sign-in
- SimpleNotify: Instructs the agent to send an IM message to a user
- AdhocMeeting: Obtains a meeting URL from trusted application, and then used the meeting URL to get token 

# Getting started
## Prerequisites
1.	Create Azure storage used to save event messages from Platform Service, where you will get an account name and account key. Refer to [this link](https://azure.microsoft.com/en-us/documentation/articles/storage-create-storage-account/) for details.
2.	Create Azure service bus for the process of saving the event messages to storage, from where you will get connection string. Refer to [this link](https://azure.microsoft.com/en-us/documentation/articles/service-bus-dotnet-get-started-with-queues/) for details.
3.	Create a Cloud Service from Azure and give it a name to reserve a *.cloudapp.net URL. Refer to [this link](https://azure.microsoft.com/en-us/documentation/services/cloud-services/) for details.
> The Trusted Application API requires the use of SSL and https for your cloud service.  You will need to create a DNS CName that points to your *.cloudapp.net cloud service to give it a custom domain.  Please refer to [this link](https://azure.microsoft.com/en-us/documentation/articles/cloud-services-custom-domain-name-portal/)
4.	Register your application in Azure AD, where you will get a Client ID and set an App ID URI (base your App ID URI on the custom domain for the *.cloudapp.net created in step 3). Refer to [this link](https://skypedevtap.visualstudio.com/_git/Trusted%20App%20API%20Documentation?path=%2FTrusted_Application_API%2FRegistrationInAzureActiveDirectory.md&version=GBmaster&createIfNew=true&_a=contents) for details.
> You can use the [quick registration tool](https://skypeappregistration.azurewebsites.net) for registering Skype for Busines Trusted Applications in Azure and Skype for Business Online, that eliminates the need to register an Application manually in Azure portal.
5.  Configure authentication for either client secret authentication or certificate-based authentication.  If using client secret, record the client secret from step 4.  If using certificates, configure certificate credential authentication:
    1. Update your Azure AD application's manifest with the certificate details. If you are not the domain admin, then ask your admin to provide you with the credentials (a certificate) used for authentication. Refer to [this link](https://skypedevtap.visualstudio.com/_git/Trusted%20App%20API%20Documentation?path=%2FTrusted_Application_API%2FAADS2S.md&version=GBmaster&createIfNew=true&_a=contents) for details.
    2. Upload the Azure AD application credentials (a certificate) to AAD for authentication.
6.	Registration in Skype for Business Online: Use the Client ID and App ID URI from Azure AD and register your Trusted Application with Skype for Business Online.  Your application endpoint will also need to be registered within a Skype for Business Tenant. Refer to [this link](https://skypedevtap.visualstudio.com/_git/Trusted%20App%20API%20Documentation?path=%2FTrusted_Application_API%2FSfBRegistration.md&version=GBmaster&createIfNew=true&_a=contents) for details.
> You can use the [quick registration tool](https://skypeappregistration.azurewebsites.net) for registering Skype for Busines Trusted Applications in Azure and Skype for Business Online.  Application endpoint registration must still be done by the Tenant Admin. Refer to [this link](https://skypedevtap.visualstudio.com/_git/Trusted%20App%20API%20Documentation?path=%2FTrusted_Application_API%2FSfBRegistration.md&version=GBmaster&createIfNew=true&_a=contents) for details. 
7.	Deploy the the client (anonymous webpage) code contained in the `WebsiteSamples` folder to a web server (e.g. IIS on localhost, or Azure App Service to deploy to *.azurewebsites.net)
8.	Download [Azure SDK v2.9](http://go.microsoft.com/fwlink/?LinkId=746481) or above.


## Deployment
Once you have satisfied the prerequisites, it is time to configure the agent source code and deploy it to the Azure Cloud Service created in step 3.

If you plan on pointing a custom domain or subdomain to your `*.cloudapp.net` server, use that FQDN as your base URL. Otherwise, use `resourcename.cloudapp.net` as your base URL, as created in step 3.

### SSL certificate
You will need to enable HTTPS on your Cloud Service in order for the agent to function properly. You can do this either by generating a self-signed certificate (not recommended), or by obtaining a certificate for a custom FQDN mapped to the Cloud Service (e.g. with the help of [Let's Encrypt](https://letsencrypt.org/)).

You will need to obtain the SSL certificate in .pfx format, so that the single file carries the public & private certificates, and then upload it to the Cloud Service (see [here](https://docs.microsoft.com/en-us/azure/cloud-services/cloud-services-configure-ssl-certificate-portal#step-3-upload-a-certificate) for details).

Once uploaded to the Cloud Service, take note of the SSL certificate's thumbprint for use below.

### Trusted Application Agent
1. Clone from `https://skypedevtap.visualstudio.com/Trusted%20Application%20API%20Samples/_git/Trusted%20Application%20API%20Samples`
2. Open the `PlatformServiceSamples` solution with VS2015
3. Restore NuGet packages and build
4. Edit the `ServiceConfiguration.Prod.cscfg` file, substituting `[parameters]` as follows:
    ```
    <ConfigurationSettings>
        <Setting name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" value="[Storage connection string from step 1]" />
        <Setting name="Microsoft.ServiceBus.ConnectionString" value="[ServiceBus connection string from step 2]" />
        <Setting name="AppTokenCertThumbprint" value="[Certificate thumbprint from step 5]" />
        <Setting name="AAD_ClientId" value="[Application Client ID from Step 4]" />
		<Setting name="AAD_ClientSecret" value="[Application secret from Step 4]" /> //This can be optional, use this to avoid using appTokenCertThumbprint
        <Setting name="SkypeAudienceUri" value="[Get from Prerequisite step 6 above or ask domain admin]" />
        <Setting name="ApplicationEndpointId" value="[Get from Prerequisite step 6 above or ask domain admin]" />
        <Setting name="CallbackUriFormat" value="[Get from Prerequisite step 6 above or ask domain admin]" />
        <Setting name="ResourcesUriFormat" value="[Get from Prerequisite step 6 above or ask domain admin]" />
        <Setting name="AudienceUri" value="[Base URL]" />
        <Setting name="LogFullHttpRequestResponse" value="true" /> //false if do not need log traffic
    </ConfigurationSettings>
    <Certificates>
        <Certificate name="HttpsCert" thumbprint="[SSL certificate thumbprint]" thumbprintAlgorithm="sha1" />
        <Certificate name="OauthApplicationCert" thumbprint="[Certificate thumbprint from Step 5 or ask domain admin]" thumbprintAlgorithm="sha1" />
    </Certificates>
    ```
5.	Update the `EnableCorsAttribute.cs` file from `FrontEnd` project to allow CORS requests on your Base URL:
    ```
        // Add allowed origins.
        _policy.Origins.Add("http://localhost");
        _policy.Origins.Add("https://your.baseurl"); // add this line
    ```
6. Publish the application (right-click `PlatformServicSamplesAzureService` and select *Publish...*) using the *Production* slot

When visiting your base URL (e.g. https://name.cloudapp.net), the expected response is a simple 403.

### Client webpage
You will need to modify the `WebsiteSamples\Samples\scripts\trustedappwebclient.js` file and replace `https://[cloudAppName].cloudapp.net with your trusted agent's base URL.
You will need to modify the `WebsiteSamples\Samples\scripts\trustedappwebclient.js' file and replace InviteTargetUri 'toshm@metio.onmicrsoft.com' with your valid user in your own tenant.

# Questions & troubleshooting

**Q: How does these two solutions (PlatformSDK and PlatformServiceSamples) build and work?**

The `PlatformSDK` project builds a `Microsoft.SfB.PlatformService.SDK.0.1.0.nupkg` nuget package which is used by `PlatformServiceSamples` solution.

If you have just cloned the git repository, you want to build `PlatformSDK` first, and then build `PlatformServiceSamples`. If you have already done this and want to bring some new changes from SDK to samples, you will have to delete the SDK's dlls from `PlatformServiceSamples\packages` directory.

Please note that during the TAP stage, `PlatformSDK` should not be built manually and a signed NuGet package is provided in the repository.

**Q: I published to my Cloud Service, but get a generic application error page**
If you see this error message:

*An application error occurred on the server. The current custom error settings for this application prevent the details of the application error from being viewed remotely (for security reasons). It could, however, be viewed by browsers running on the local server machine.*

You need to adjust web.config on the Cloud Service to set `customError="off"` per the error message.

1. Enable Remote Desktop on the `FrontEnd` role instance - see [here](https://docs.microsoft.com/en-us/azure/cloud-services/cloud-services-role-enable-remote-desktop#remote-into-role-instances) for details
2. Remote it and start the IIS Manager
3. Find the Under *Sites*, right-click the configured website (there should only be one) and select *Explore...*.
4. Edit `web.config` and allow custom errors on remote hosts, as per the error page's instructions.
5. Make the original web request again, and the correct error code & description should be returned.

**Q: I published my Cloud Service, but get a 500 error**

You may want to remote into the machine (see question above) and [enable IIS failed request tracing](https://www.iis.net/learn/troubleshoot/using-failed-request-tracing/troubleshooting-failed-requests-using-tracing-in-iis) to view the traceback details.

**Q: I published to my Cloud Service but get a 401 error on the client samples**

Verify the error description - if you see reference to Azure Storage or Azure Service Bus, you likely have provided an incorrect connection string in your service definition file. Proper connection strings can be obtained from the [Azure Portal](https://portal.azure.com). Ensure that there are no leading or trailing spaces in the provided connection string.

**Q: Exception from Azure log like 'A job with ID ffbc8e5b8e644b2d83047155de10036e is already listening, please stop it before starting a new one **

Delete current listening job by: DELETE https://[name].cloudapp.net/ListeningJob
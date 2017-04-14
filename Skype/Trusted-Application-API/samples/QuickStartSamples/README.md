# Introduction
The Trusted Application API is a Rest API that enables developers to build Skype for Business Online back-end communications services for the cloud.  The Trusted Application API Samples contain samples for the bankend Trusted Application and samples for how to interact with the trusted application from a client-side browser using the Skype Web SDK. 

The Trusted Application QuickStart console app sample demonstrates:

- Authenticating a Trusted Application with Client Credentials Grant Flow
- Trusted Application Discovery
- Scheduling an AdhocMeeting
- Retreiving an Anonymous Token and Discover URL for the Adhocmeeting (allows clients to join the meeting anonymously)

# Getting Started

**1. Registration**

Use the [quick registration tool](https://aka.ms/skypeappregistration) for registering Skype for Business Trusted Applications in Azure and Skype for Business Online, that eliminates the need to register an Application manually in Azure portal.

>NOTE: You can optionally manually register your application in Azure Portal, where you will get a Client ID and set an App ID URI. Refer to [Registration in Azure Active Directory](https://github.com/OfficeDev/skype-docs/blob/master/Skype/Trusted-Application-API/docs/RegistrationInAzureActiveDirectory.md) for details.

**2. Register trusted application endpoints**

Register Trusted Endpoints in a Skype for Business Online tenant using PowerShell.   Refer to [Setting up a Trusted Application Endpoint](https://github.com/OfficeDev/skype-docs/blob/master/Skype/Trusted-Application-API/docs/TrustedApplicationEndpoint.md) for details.

**3. Provide consent**

When the application is registered in AAD, it is registered in the context of a tenant.  For a tenant to use the Service Application, for example, when the application is developed as a multi-tenant application, it must be consented to by that tenant's admin. Refer to [Tenant Admin Consent](https://github.com/OfficeDev/skype-docs/blob/master/Skype/Trusted-Application-API/docs/TenantAdminConsent.md) for more details.

**4. Clone samples and Restore NuGet Packages** 

Configure required parameters in the <code>app.config</code> file, including authentication using client secret from the registration portal.
```xml
    <add key="AAD_ClientId" value="318fbf0c-d180-4cd9-8d13-7d0e2cacab9e" />
    <add key="AAD_ClientSecret" value="Z3YhEZAoknXcPRl++RrzdS2bnRTy6KKOx4zHf/vsuvU=" />
    <add key="ApplicationEndpointId" value="sip:yourtrustedapp@contoso.onmicrosoft.com" />

    <!--
       MyCallbackUri
         Publicly accessible domain/ip address for receiving callbacks from Trusted Application API.
         This field will override the global callback uri specified at application registration. This override works
         only for conversations initiated by your application. Incoming conversation notifications will still be
         posted on the global callback uri specified at application registration.
    -->

    <!-- If using ngrok, populate these keys as specified below -->
    <!--
    <add key="MyCallbackUri" value="https://6afb33d7.ngrok.io/callback" />
    <add key="LocalServerListeningAddress" value="http://localhost:9000" />
    -->

    <!-- If the host machine is publicly accessible, uncomment and populate these keys -->
    <add key="MyCallbackUri" value="https://mydevbox.contoso.com/callback" />
```

If the sample that you are trying to run requires notifications from Trusted Application API but you do not have a publicly visible IP/domain, please see
**Running samples on a machine which is not publicly accessible** on this page. If a sample solution has a dependency on WebEventChannel, it needs
notifications from Trusted Application API to run the scenario.

**5. Run samples**

You can simply run/debug the samples from your visual studio

## Running samples on a machine which is not publicly accessible

Most of the samples in this directory use [owin to self host](https://docs.microsoft.com/en-us/aspnet/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api)
callback controller. Callback is required to receive notifications/events from Skype for Business's Trusted Application APIs. This means your application needs to have a
public IP address for you to receive these notifications. You can, of course, deploy your application on Azure but to develop and debug applications, you can also use ngrok.
Ngrok is used to expose your local server to the internet. Visit [ngrok's official website](https://ngrok.com) to see on how it works. To run these samples on a machine
with no public IP address, you need to perform these steps before running the project:

1. Set <code>LocalServerListeningAddress</code> to something similar to <code>http://localhost:9000</code> in the sample's <code>app.config</code> file

2. Assuming you have set <code>LocalServerListeningAddress</code> to <code>http://localhost:9000</code>, you can start ngrok with these parameters :

        ngrok.exe http 9000 -host-header="localhost:9000"

3. Copy the value of **HTTPS** <code>Forwarding</code> from ngrok's output into <code>MyCallbackUri</code> key in the sample's <code>app.config</code> file and add <code>/callback</code> to it.
    That is, if this is the output of ngrok:

        ngrok by @inconshreveable                                                                               (Ctrl+C to quit)
        Session Status                online
        Update                        update available (version 2.2.3, Ctrl-U to update)
        Version                       2.1.18
        Region                        United States (us)
        Web Interface                 http://127.0.0.1:4040
        Forwarding                    http://6afb33d7.ngrok.io -> localhost:9000
        Forwarding                    https://6afb33d7.ngrok.io -> localhost:9000
        Connections                   ttl     opn     rt1     rt5     p50     p90
                                      68      0       0.00    0.00    1.31    100.31
    Then set <code>MyCallbackUri</code> to <code>https://6afb33d7.ngrok.io/callback</code>
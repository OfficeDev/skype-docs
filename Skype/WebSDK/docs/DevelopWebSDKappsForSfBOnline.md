
# Developing Web SDK applications for Skype for Business Online


 _**Applies to:** Skype for Business | Skype for Business Online_

 **In this article**  
[App registration](#app-registration)  
[Update your code](#update-your-code)  
[Tenant Administrator Consent Flow](#tenant-administrator-consent-flow)  
[Additional Resources](#additional-resources)  


This section shows how to develop a Skype Web SDK client application for Skype for Business Online. As a prerequisite, you will need to have a tenant on Office 365 with a user who is assigned a Skype for Business license. You will also need to set up a tenant in Azure Active Directory if you don't have one already. You can get to the Azure portal from the Admin Centers section of your Office 365 Admin Portal.

> [!NOTE] 
> This topic does not apply to on-premises or hybrid server scenarios; only to online scenarios.


## App registration
<a name="sectionSection0"> </a>

Skype for Business Online uses Azure Active Directory (Azure AD) to provide authentication services that your application can use to obtain rights to access the service APIs. Following is a brief overview of the steps required, with further details on each step below:

1.  **Register your application in Azure AD**. To allow your application access to the Skype Web SDK APIs, you need to register your application in Azure AD. This will allow you to establish an identity for your application and specify the permission levels it needs in order to access the APIs. For details, see **Registering your application in Azure AD**.
    
2.  **Add a sign in feature to your app**. When a user visits your website and initiates sign-in, your application makes a request to the Microsoft common OAuth2 login endpoint. Azure AD validates the request and responds with a sign-in page, where the user signs in. A user must explicitly grant consent to allow your application to access user data by means of the Skype Web SDK APIs. The user reads the descriptions of the access permissions that your application is requesting, and then grants or denies the request. After consent is granted, the UI redirects the user back to your application. If authentication and authorization are successful, Azure AD returns a token and grants access to Skype for Business Online and identifies the current signed-in user. For details on authentication, see [Authentication Using Azure AD](https://technet.microsoft.com/library/f66482d2-ac35-4b25-9c8b-0f5f7ab89b01.aspx). For details of authorization, see [Skype for Business Online Scope Permissions](https://technet.microsoft.com/library/e292d8ef-3b05-4442-9983-378f6f958f8b.aspx).
    
3.  **Call the Skype Web SDK APIs**. The token that Azure AD provided when redirecting back to your app need not be saved, as the SDK can get its own tokens from AAD (on subsequent requests the browser passes along the requisite cookies to obtain tokens). Pass the `client_id` and `redirect_uri` registered in AAD to the SDK and it will be able to sign in.
    

### Registering your application in Azure AD

Sign in to the Azure Management Portal, then do the following:


1. Click the  **Active Directory** node in the left column and select the directory linked to your Skype for Business subscription.
    
2. Select the  **Applications** tab and then **Add** at the bottom of the screen.
    
3. Select  **Add an application my organization is developing**.
    
4. Choose a name for your application, such as  `skypewebsample`, and select  **Web application and/or web API** as its **Type**. Click the arrow to continue.
    
5. The value of  **Sign-on URL** is the URL at which your application is hosted.
    
6. The value of  **App ID URI** is a unique identifier for Azure AD to identify your application. You can use `http://{your_subdomain}/skypewebsample`, where  `{your_subdomain}` is the subdomain of the tenant you specified while signing up for your Skype for Business Web App (website) on Azure. Click the check mark to provision your application. For example, a tenant with name skypesample.onmicrosoft.com could use an App ID URI of `http://app.skypesample.onmicrosoft.com`.

7. Select the **Configure** tab and set **Application is Multi-Tenant** to true if you would like to allow users from other tenants to sign in to the application hosted on your tenant. For a single tenant application leave this at false.

   > [!NOTE] 
   > In a multi-tenant scenario a global admin from the other tenant has to first sign in and consent to allowing users from that tenant to use your application.

8. Specify one or more reply urls that your application may use. The reply url is typically what you provide to both Azure AD (when the user is initally redirected to enter credentials) and the `signInManager.signIn` method as the value of the redirect_uri parameter. Wild cards are allowed. For example, if your web application is hosted at `https://mycompany.onmicrosoft.com/myapp/signin/index.html` you could potentially provide a reply url like `"https://mycompany.onmicrosoft.com/myapp/*"`.
    
   > [!NOTE] 
   > During development it is a common practice to host your application on `http://localhost` and provide `http://localhost/*` as a reply url. Apart from this being a security issue (anyone can host a website on `http://localhost` and authenticate against your application if they know your client_id) this approach does not work for Internet Explorer because of how security zones work on Internet Explorer. `http://localhost` is in your local computer's trusted zone while Azure AD (login.microsoftonline.com) is in the Internet zone, and there is no cookie sharing between these zones. Hence, even if a user signs in on the Azure AD login page, the browser does not carry forward the necessary cookies on subsequent authentication requests to Azure AD from the SDK. 
    
9. Select the  **Configure** tab, scroll down to the **Permissions** to other applications section, and click the **Add application** button.
    
10. In order to show how to create online meetings, add the  **Skype for Business Online** application. Click the plus sign in the application's row and then click the check mark at the top right to add it. Then click the check mark at the bottom right to continue.
    
11. In the  **Skype for Business Online** row, select **Delegated Permissions**, and choose the permissions you wish to use.
    
12. Click  **Save** to save the application's configuration.
    
These steps register your application with Azure AD, but you still need to configure your app's manifest to use OAuth implicit grant flow, as explained below.


### IMPORTANT: Configure your app for OAuth implicit grant flow

In order to get an access token for Skype for Business API requests, your application should use the OAuth implicit grant flow. You need to update the application's manifest to allow the OAuth implicit grant flow because it is not turned on by default.

1. Select the  **Configure** tab of your application's entry in the Azure Management Portal.
    
2. Using the  **Manage Manifest** button in the drawer, download the manifest file for the application and save it to your computer.
    
3. Open the manifest file with a text editor. Search for the  `oauth2AllowImplicitFlow` property. By default it is set to false; change it to **true** and save the file.
    
4. Using the  **Manage Manifest** button, upload the updated manifest file.
    
This will register your application with Azure AD. In order for your Skype Web application to access Skype for Business Server resources (such as messaging or presence), it needs to obtain an access token using implicit grant flow. This token gives the application permission to access the resource.


## Update your code
<a name="sectionSection1"> </a>

To update your code to support Skype for Business Online, you'll need to update a web page in the app to show the Azure sign in screen. In addition, you'll need to make changes in JavaScript to initialize the Skype Web SDK API entry point. Finally, you'll need to call the `signInManager.signIn` method on the SDK (either on some button click or automatically when the user gets redirected back to your application from Azure AD).

### Support for OAuth2 in Internet Explorer

You'll need to create additional folders in your web app directory for users who start the app in Internet Explorer. The path must match the redirect uri that you specify when signing a user in.

The following example shows the parameters that are required when signing in to Skype for Business Online. 
1. Set the client_id parameter to the one that was auto generated for you in your Azure AD application. 
2. Do not change the origins parameter.
3. cors must be set to true. Without this being true you will encounter 403 errors like **service does not allow cross domain requests from this origin** when trying to perform auto discovery.
4. The redirect_uri parameter value in this sample is the URL of an empty html page **in a folder below the web app folder**. You can use any name for the folder and the file.
5. Provide an applicationame and version for your application.


```js
app.signInManager.signIn({
     "client_id": "...",  // GUID obtained from Azure app registration.
     "origins": [ "https://webdir.online.lync.com/autodiscover/autodiscoverservice.svc/root" ],
     "cors": true, 
     "redirect_uri": '/an/empty/page.html',
     "version": '<YourAppName>/1.0.0.0'
});
```


> [!NOTE] 
> If `redirect_uri` is not specified, the SDK picks a random one. This doesn't work in Internet Explorer because when it sends a GET to it and gets back a 404, it does an extra redirect to `res://ieframe.dll/http_404.htm` and drops the access token from the original URL. If `redirect_uri` points to a folder, implying the `index.html` file under it, then IE will also drop the access token from the original URL.


### Authenticate a user with Office 365 Online

When a user visits your website and initiates sign-in, your application redirects the user to the Azure AD authorization endpoint. Azure AD validates the request and responds with a sign-in page, where the user signs in. Once the user signs in successfully, Azure AD redirects back to the redirect_uri specified by you with the access token in the location.hash. 

The following HTML content shows the Azure AD sign in page to the user when loaded. Be sure to replace  `<add your client id here>` with the client id you got from Azure AD when you registered your app.

```HTML
<!doctype html>
<html>
<head>
    <title>OAuth</title>
</head>
<body>
    <script>
    	var hasToken = /^#access_token=/.test(location.hash);
    	var hasError = /^#error=/.test(location.hash);
    	
    	var client_id = '<add your client id here>';
    
        // redirect to Org ID if there is no token in the URL
        if (!hasToken && !hasError) {
            location.assign('https://login.microsoftonline.com/common/oauth2/authorize?response_type=token' +
                '&client_id=' + client_id +
                '&redirect_uri=' + location.href +
                '&resource=https://webdir.online.lync.com');
        }

        // show the UI if the user has signed in
        if (hasToken) {
		// Use Skype Web SDK to start signing in               
        }
        
        if (hasError) {
        	console.log(location.hash);
        }
    </script>
</body>
</html>
```

To start using the API, you need to get the Skype Web application object with code like the following:

```js
Skype.initialize({ 
    apiKey: 'a42fcebd-5b43-4b89-a065-74450fb91255',
    correlationIds: {
        sessionId: 'mySession123', // Necessary for troubleshooting requests, should be unique per session
    }}, function (api) {
        app = new api.application();
});
```



The apiKey value in the previous example is valid for the preview SDK. At general availability, these values will change.

> [!NOTE] 
> If you are unable to troubleshoot an issue with the SDK and need to submit a troubleshooting request, make sure to include the **correlationIds.sessionId** parameter when initializing the SDK as above, and the **version** parameter when calling **signInManager.signIn** of the form **{your-app-name}/{version-number}** (see below). The **sessionId** parameter should be generated by your client code and should be unique-per-session, so that if you want to ask what went wrong during a particular session, you can provide us the unique session ID and version to allow us to locate records specific to your session in our telemetry. For more information, see [How can I submit an error report?](troubleshooting/gatheringLogs/GatherLogs.md).

When you have the application object, you sign a user into Skype for Business Online with code like the following example.

```js
// the SDK will get its own access token
app.signInManager.signIn({
    client_id: client_id,
    cors: true,
    redirect_uri: '/an/empty/page/for/ie.html',
    origins: [ "https://webdir.online.lync.com/autodiscover/autodiscoverservice.svc/root" ],
    version: '<your-app-name>/<version-number>' // Necessary for troubleshooting requests; identifies your application in our telemetry
});
```

> [!NOTE] 
> The specified redirect page must exist on the site.

You may see sign in issues with IE, if you have tried using multiple AAD identities. Please use the following steps to resolve that issue:

1. Clear cache/cookies.
2. Start afresh.
3. Use private browsing session.

### Tenant Administrator Consent Flow

The Skype for Business Online permissions are tenant administrator consent only. For an app to be used by all users of an O365 tenant, a tenant administrator must provide consent. To provide consent for all users in the tenant, construct the following URL for your app as shown in the example below.
The tenant administrator consent flow is important in two scenarios:
1. Users from your tenant want to sign in to a multi-tenant application hosted on a different tenant.
2. Some properties of the multi-tenant application have changed requiring the tenant administrator to consent once again. 

> [!NOTE] 
> Update the  **client Id** and **redirect Uri** for your app.

```js
https://login.microsoftonline.com/common/oauth2/authorize?response_type=id_token
	&client_id= ...
	&redirect_uri=https://app.contoso.com/
	&response_mode=form_post
	&resource=https://webdir.online.lync.com
	&prompt=admin_consent
```

Access the URL and authenticate using a tenant administrator credentials and accept the application permissions. Users will now be able to access the application.

## Additional Resources
<a name="bk_addresources"> </a>

- [How to get an Azure Active Directory tenant](https://azure.microsoft.com/documentation/articles/active-directory-howto-tenant/)
    
- [Managing Applications with Azure Active Directory (AD)](https://azure.microsoft.com/documentation/articles/active-directory-enable-sso-scenario/)
    
- [Registering Your Application in Azure AD](https://technet.microsoft.com/library/9e4d9905-a17c-458d-8cf5-d384f5bd65fd.aspx)

- [Integrating Applications with Azure Active Directory](https://azure.microsoft.com/en-us/documentation/articles/active-directory-integrating-applications/)

- [Active Directory Authentication Scenarios](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios/)
    

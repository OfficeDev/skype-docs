# Azure Active Directory - Service to Service calls using Client Credentials


The **Trusted Application API** only accepts an AAD Oauth token issued to an application identity.  In order that your application gets such a token from AAD, you need to either upload your application's credentials ( a certificate ) to AAD, and use that certificate whenever you need an Oauth token, or use a client secret.
 
When challenged for authentication by Skype for Business Online service, your application must perform authentication against Azure AD to receive an Oauth token.

**Trusted Application API** requires use of HTTPS and certificates for both AAD Service-to-service authentication and SSL.We require the use of publicly-signed certificates.  If needed, you should be able to create a record or CName to point your own custom domain to your cloudapp.net Azure cloud service.
 
For example, create a DNS CName `abc.contoso.com`, and that DNS CName points to trustedapp.cloudapp.net (This allows you to avoid creating a certificate with SN: trustedapp.cloudapp.net, but use a certificate with SN: abc.contoso.com)
 
Please refer to the following information and examples for how to get an **Oauth token** and implement service to service calls.
 
## Additional information

[Building service aps in Office 365](https://msdn.microsoft.com/en-us/office/office365/howto/building-service-apps-in-office-365)

[Active Directory authentication scenarios](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios)

[Active Directory Certificate Credentials](https://github.com/Azure-Samples/active-directory-dotnet-daemon-certificate-credential)

[Configuring a custom domain name for an Azure cloud service](https://azure.microsoft.com/en-us/documentation/articles/cloud-services-custom-domain-name-portal)

[Authorize access to web applications using OAuth 2.0 and Azure Active Directory](https://msdn.microsoft.com/en-us/library/azure/dn645543.aspx)

[Authentication Scenarios for Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios)

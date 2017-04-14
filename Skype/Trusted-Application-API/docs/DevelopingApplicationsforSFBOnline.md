
# Developing **Trusted Application API** applications for Skype for Business Online

Learn how to develop **Trusted Application API** service applications.

**Trusted Application API** uses Azure Active Directory (Azure AD) to provide authentication services that your application can use to get the right to access a set of capabilities. To do this, you need to do the following:

## Getting started

**1. Registration**

Use the [quick registration tool](https://aka.ms/skypeappregistration) for registering Skype for Business Trusted Applications in Azure and Skype for Business Online, that eliminates the need to register an Application manually in Azure portal.

You can also manually register your application in Azure Portal, where you will get a Client ID and set an App ID URI. Refer to [Registration in Azure Active Directory](./RegistrationInAzureActiveDirectory.md) for details.

**2. Register trusted application endpoints**

Register Trusted Endpoints in a Skype for Business Online tenant using PowerShell.   Refer to [Setting up a Trusted Application Endpoint](./TrustedApplicationEndpoint.md) for details.

**3. Provide consent**

When the application is registered in AAD, it is registered in the context of a tenant.  For a tenant to use the Service Application, for example, when the application is developed as a multi-tenant application, it must be consented to by that tenant's admin. Refer to [Tenant Admin Consent](./TenantAdminConsent.md) for more details.
 
**4. Authentication** 

Authenticate using either Client Secret or Certificate to obtain an Oauth token. Read [Azure Active Directory - Service to Service calls using Client Credentials](./AADS2S.md)
for details.

**5. Call the Trusted Application API.**
 
### Auto Discovery
It is the act of finding the **Trusted Application API**s home server using the discovery endpoint. This enables you to 
connect to the API and use the exposed capabilities.
  
>Note: For **Service applications**, please refer [Discovery for Service Applications](./DiscoveryForServiceApplications.md)

### Authentication using client credentials

All **Trusted Application API** endpoints require an oauth token with an Application Identity from Azure Active Directory using the client credential grant flow.
This grants the permissions to the application  to access the **Trusted Application API** resource. Please refer [Azure Active Directory - Service to Service calls using Client Credentials](./AADS2S.md)
for more details.


## In this section

- [Registration in Azure Active Directory](./RegistrationInAzureActiveDirectory.md)
- [Azure Active Directory - Service to Service calls using Client Credentials](./AADS2S.md)
- [Tenant Admin Consent](./TenantAdminConsent.md)
- [Set up a trusted Application Endpoint](./TrustedApplicationEndpoint.md)
- [Registering a Trusted Application in Skype for Business Online](./SfBRegistration.md)
- [Discovery for Service Applications](./DiscoveryForServiceApplications.md)
- [Authentication and Authorization](./AuthenticationAndAuthorization.md)


 
The following topics also apply to the **Trusted Application API** Online service application workflow:

- [Webhooks (Events)](./Webhooks.md)
- [**Trusted Application API** Reference Library](./ReferenceLibrary.md)
 

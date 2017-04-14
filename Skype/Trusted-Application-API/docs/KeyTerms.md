# Definition of key terms

## Platform Service
 
The Skype for Business Platform Service is a web services layer built on top of the core Skype for Business infrastructure and is exposed to developers in the form of 'user' and 'application' APIs. 
 
The User API is known as the Unified Communication Web (UCWA) API. It allows developers to build immersive communication experiences that represent a user. These applications can leverage the [Skype App SDK](https://msdn.microsoft.com/en-us/skype/appsdk/skypeappsdk) or the [Skype Web SDK](https://msdn.microsoft.com/en-us/skype/websdk/skypewebsdk) to connect to the User API. Developers can build and release single tenant or multi-tenant apps for Office 365 using the SDKs. Developers can also build client apps that connect directly to the RESTful UCWA API to build additional experiences. Experiences for user scenarios which aren't addressed by the current SDKs. Directly connecting to the UCWA API is necessary for platforms not currently supported by the App SDK.
 
The **Trusted Application API** allows developers to build Service Applications (SA) that drive scenarios such as **Customer care**, **Smart virtual agents**, **Business process automation**, **Communication extensions** and more.
 
##  The **Trusted Application API** Library Reference Documentation
 
 [**Trusted Application API**](Trusted_Application_API_GeneralReference.md)
 
   
##  Trusted Application Endpoint
 
 A **Trusted Application API** Endpoint is a specific instance of a 3rd party application running in the context of a single tenant.  Registration of a **Trusted Application API** Endpoint is the responsibility of a Tenant Administrator. When registering an endpoint, a Tenant Admin must assign the following values:
 
 - SIP URI that identifies the endpoint, for example: **helpdesk@contoso.com**
 - Phone number (for PSTN-enabled Service Applications)
 - Callback URI
 - [Audience URL](https://azure.microsoft.com/en-us/documentation/articles/active-directory-token-and-claims/)
 
 >Note: All the **Trusted Application API** endpoints require authentication using **OAuth token**. Please refer [Azure Active Directory - Service to Service calls using Client Credentials](./AADS2S.md) for more information on how to get a OAuth Token.
   

## Unified Communications Web API
[The UCWA API](https://ucwa.skype.com/) help documentation has interactive documentation and samples for the UCWA API.
 
## Unified Communications Managed API
[UCMA API](https://msdn.microsoft.com/en-us/library/office/dn454984.aspx) - MSDN Library Documentation for the sfb on-premise UCMA API.
 
##  Skype Web SDK
 
Built on top of the User API, the Skype Developer Platform for Web ("Skype Web SDK") is a set of JavaScript Web APIs and HTML controls that enable you to build web experiences that seamlessly integrate a wide variety of real-time collaboration models leveraging Skype for Business services. Please follow [Skype Web SDK](https://msdn.microsoft.com/en-us/skype/websdk/skypewebsdk) for more details.
   
##  Azure Active Directory (Azure AD)
 
Azure Active Directory is a comprehensive identity and access management cloud solution that provides a robust set of capabilities to manage users, groups, and applications. It helps secure access to on-premises and cloud applications, including Microsoft online services like Office 365 and many non-Microsoft Service Applications.  Skype for Business Online uses Azure AD (AAD) for 3rd party application registration, management, authentication, and authorization. Learn more at [Azure Active Directory documentation](https://azure.microsoft.com/en-us/documentation/services/active-directory/).
 
##  Service Application (SA)
 
Also referred to as a Trusted Application or SaaS Application, this is a third-party cloud application developed using the **Trusted Application API**.  It is recommended to host and manage Service Applications in Microsoft Azure for the best development experience with Skype for Business Online.
 
## Litware
 
A fictitious enterprise used for illustration purposes.  Litware is an Independent Software Vendor (ISV) who develops multi-tenant applications for Skype for Business Online using the Skype Developer Platform.
 
## Contoso

A fictitious enterprise used for illustration purposes.  Contoso is a customer of Skype for Business Online with an O365 tenant.  Contoso is also a customer of Skype for Business applications developed by Independent Software Vendor (ISV), Litware.
 
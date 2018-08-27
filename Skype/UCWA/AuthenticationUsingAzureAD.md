
# Authentication using Azure AD
Learn how a UCWA 2.0 online application authenticates a user with Azure AD to access Skype for Business resources.


In order for your UCWA 2.0 application to access Skype for Business resources (such as messaging or presence), it needs to obtain an access token using implicit grant flow. This token gives the application permission to access the resource.


>Note: If your application authenticates against an online server, you must follow the Azure AD authorization flow as described in this article. If the application authenticates against an on-premises server, this step is not necessary, and you should follow the steps in [Authentication in UCWA](https://msdn.microsoft.com/EN-US/library/dn356686%28v=office.16%29.aspx).

The authentication and authorization flow comprises the following steps:

- Register your application in Azure AD.
 
- Sign in your application to the Azure AD authorization endpoint.
 
- Perform autodiscovery to find the user's UCWA home pool.
 
- Request an access token for the Autodiscover URL, using Oauth implicit grant flow.
 
- Resend an autodiscovery request with the access token.
 
- Request an access token for the UCWA home pool resource returned in the autodiscovery response, using implicit grant flow.
 
- Use the access token to access the UCWA resource.
 
For more information on the Office 365 authentication flow, see [Understanding authentication with Office 365 APIs](https://msdn.microsoft.com/en-us/office/office365/howto/common-app-authentication-tasks).

## Registering an application in Azure AD
<a name="sectionSection0"> </a>

If the application authenticates against an online server, you must first register it with Azure Active Directory (Azure AD) so that it can access Office 365 APIs. You'll need an Office 365 business account and an Azure AD subscription associated with that account.

As part of registration, you specify whether your app is a Web application, such as an MVC or Web Forms solution, or a native app, such as a smart phone or other mobile device. Azure AD uses this information to generate resources your app will need to authenticate with Azure.

Log into the Azure Management Portal using your Office 365 account credentials. In the the Active Directory node, you then add your application to the active directory linked to your Office 365 subscription.

If your application is a browser-based web app, you need to configure your app's manifest to allow the Oauth implicit grant flow, and specify whether the web app is single or multi-tenant.

Finally, you need to specify exactly what permissions your app requires of the Skype for Business Online APIs. To do so, you add access to the Skype for Business Online service containing the API you require to your app, and then specify the permissions you need from the APIs in that service.

You can do all of these steps in the Azure Management Portal. The registration procedure is described in detail in [Manually register your app with Azure AD so it can access Office 365 APIs](https://msdn.microsoft.com/en-us/office/office365/howto/add-common-consent-manually).


## Sign-in
<a name="sectionSection1"> </a>

When a user visits your website and initiates sign-in, your application redirects the user to the Azure AD authorization endpoint. Azure AD validates the request and responds with a sign-in page, where the user signs in. The user initiates sign in, for example, by clicking a sign-in button or link. The browser sends a GET request to the Azure AD authorization endpoint. This request includes the client ID and reply URI in the query parameters, as in the following example:


```
GET https://login.microsoftonline.com/oauth2/authorize?response_type=token&amp;client_id=aeadda0b-4350-4668-a457-359c60427122&amp;redirect_uri=https%3A%2F%2Flocalhost%3A44326%2F HTTP/1.1
```

Azure AD validates this reply URI against the registered reply URI that you configured in the Azure Management Portal. The user then enters credentials on the sign-in page. If authentication is successful, Azure AD returns cookies that identify the current signed-in user. The user agent persists the cookies. The cookies are set for the OrgID domain (login.microsoftonline.com) and if this domain and the site are in different trusted zones in Internet Explorer, OAuth will not work because Internet Explorer will not let the website use the OrgID cookies.

Your application now performs autodiscovery.


## Autodiscovery
<a name="sectionSection2"> </a>

The goal of autodiscovery is to find the user's UCWA home pool. Skype for Business services are distributed, so servers running in different pools respond to requests. Your application needs to determine which pool serves the authenticated user. The home pool URI that the autodiscovery service returns contains `webdirX.online.lync.com`, where X is alphanumeric; for example `webdir0a.online.lync.com`.

First your application sends a GET request to the autodiscovery service endpoint:




```
GET https://webdir.online.lync.com/autodiscover/autodiscoverservice.svc/root
```

The service response URI will contain a path including 'oauth/user'; your application sends a GET request to this URI:




```
GET https://webdirX.online.lync.com/autodiscover/autodiscoverservice.svc/root/oauth/user
```

The service response will be an HTTP 401 error with an authorization URI:




```
https://login.microsoftonline.com/oauth2/authorize
```


## Requesting an access token using implicit grant flow
<a name="sectionSection3"> </a>

After your application retrieves the home pool from the autodiscovery service, it requests an access token from Azure AD. To obtain an access token, send an HTTP GET request to a common or tenant-specific Azure AD authorization endpoint:


```
https://login.microsoftonline.com/common/oauth2/authorize
```

For example, the access token request might look like the following:




```
GET https://login.microsoftonline.com/oauth2/authorize?
response_type=token
&client_id=aeadda0b-4350-4668-a457-359c60427122
&redirect_uri=https%3A%2F%2Flocalhost%3A44326%2F
&state=8f0f4eff-360f-4c50-acf0-99cf8174a58b
&resource=webdirX.online.lync.com 
HTTP/1.1
```


### Access token request

The GET request to the Azure AD authorization endpoint has the following format:


```
GET https://login.microsoftonline.com/oauth2/authorize?
response_type=token
&client_id=<your-application's-client-id>
&redirect_uri=<reply-uri-for-your-app>
&state=<unique-string-to-track-requests>
&resource=webdirX.online.lync.com 
HTTP/1.1
```

The following table describes the valid parameters in the request:




|**Parameter**|**Description**|
|:-----|:-----|
|response_type| [Required] The type of content to be sent in the response. Specify `token`.|
|client_id| [Required] The client ID of the native client application.To find the application's client ID, in the [Azure Management Portal](https://manage.windowsazure.com/), click Active Directory, click the directory, click the application and then click Configure.|
|redirect_uri| [Required] Specifies the Reply URI of the application. For example, https://localhost/myapp/. This value must match the value of the `redirect_uri` parameter in the authorization code request.To find the application's Reply URI, in the [Azure Management Portal](https://manage.windowsazure.com/), click Active Directory, click the directory, click the application, and then click Configure.|
|state| [Required] A long unique string value of your choice that is hard to guess. Used to prevent cross-site request forgery. For example, this can be a GUID generated by your application with each authorization request.|
|resource| [Required] The home pool URI received in the autodiscovery step. This has the form webdirX.online.lync.com where X is an alphanumeric combination such as 0a.|

### Access token response

The response to a request for an access token has the following format:


```
{
 "access_token": "<requested-access-token>",
 "token_type": "<token-type-value>",
 "expires_in": "<validity-time-in-seconds>",
 "expires_on": "<access-token-expiration-date-time>",
 "resource": "<app-id-uri>",
 "refresh_token": "<oauth2-refresh-token>",
 "scope": "user_impersonation",
 "id_token": "<unsigned-JSON-web-token>"
}

```

The access token response contains the following parameters:




|**Parameter**|**Description**|
|:-----|:-----|
|access_token|The requested access token.|
|token_type|The token type value. The only type value that Azure AD supports is 'Bearer'. For more information about bearer tokens, see [OAuth 2.0 Authorization Framework: Bearer Token Usage](http://www.rfc-editor.org/rfc/rfc6750.txt).|
|expires_in|The time period (in seconds) that the access token is valid.|
|expires_on|Time in Unix epoch when the access token expires. The date is represented as the number of seconds from 1970-01-01T0:0:0Z UTC until the expiration time. This value is used to determine the lifetime of cached tokens.|
|resource|The App ID URI of the Skype for Business API to which the access token applies.|
|refresh_token|An OAuth 2.0 refresh token. Your application can use this token to acquire additional access tokens after the current one expires.|
|scope|Impersonation permissions granted to the client application. The default permission is user_impersonation. The owner of the secured resource can register additional values in Azure AD.|
|id_token|An unsigned JSON Web Token (JWT). Your application can use this token to request information about the user who consented. The application can cache the values and display them. For more information about JSON web tokens, see the [JWT IETF draft specification](http://go.microsoft.com/fwlink/?LinkId=392344).|
For more information on access token contents, see [Authorization Code Grant Flow](https://msdn.microsoft.com/en-us/library/azure/dn645542.aspx).

User login is not requested again after this point because the user is already signed in and your application has a set of cookies for the authenticated user. If authentication is successful, Azure AD creates an ID token and returns it as a URL fragment to the application's Reply URI ( `redirect_uri`). For a production application, this Reply URL should be HTTPS. The returned token includes claims about the user and Azure AD that the application requires to validate the token. The JavaScript client code running in the browser extracts the token from the response to use in securing calls.


## Resending an autodiscovery request with the bearer token
<a name="sectionSection4"> </a>

To finalize the authentication process, your client application running in the browser sends another autodiscover request with the access token in the Authorization header using the Bearer authorization scheme. The request will be as follows, where the 'X' in webdirX is a number. Set the Authorization header in this request to 'Bearer' plus the returned access token.


```
GET https://webdirX.online.lync.com/autodiscover/autodiscoverservice.svc/root/oauth/user HTTP/1.1
Authorization: Bearer <access-token>
Accept: application/json
X-Ms-Origin: http://app.yourdomain.com
X-Requested-With: XMLHttpRequest
```

A successful response returns HTTP 200 OK. The response body contains a link to the applications resource, which you will use in the next step:




```
HTTP/1.1 200 OK
...
{
 "_links":{
 "self":{"href":"https://webdirX.online.lync.com/Autodiscover/AutodiscoverService.svc/root/user"},
 "applications":{"href":"https://webpoolXY.infra.lync.com/ucwa/oauth/v1/applications"}
 }
}
```

If the user is homed at a different location, the user resource targeted in the previous autodiscovery request will send a redirect link with the location of the new autodiscover service. If this happens, repeat the challenge-response process to get the user authenticated again and to obtain a new OAuth token.


## Access the applications resource
<a name="sectionSection5"> </a>

Now that you have user credentials, your request to access UCWA as a user will be granted and you will receive a resource with a single link to the [applications](applications_ref.md) resource. You use the applications resource to register your application with UCWA as an agent of the user whose credentials you obtained in a previous step.

To request access to UCWA resources, send a POST request to the applications resource. The applications resource represents a collection of individual application resources. In UCWA, the convention to create or add an item to a collection is to perform a POST request on the collection. The applications resource takes three input parameters, which are shown in the body of the following request:




```
POST https://webpoolXY.infra.lync.com/ucwa/oauth/v1/applications HTTP/1.1
Accept: application/json
Content-Type: application/json
Authorization: Bearer <access-token>
...
{
 "UserAgent":"UCWA Samples",
 "EndpointId":"a917c6f4-976c-4cf3-847d-cdfffa28ccdf",
 "Culture":"en-US"
}
```

Your request presents the access token to the resource in the Authorization header using the Bearer authorization scheme. The [RFC 6750](http://www.rfc-editor.org/rfc/rfc6750.txt) specification explains how to use bearer tokens in HTTP requests to access protected resources. When the web API receives and validates the token, your client application has access to the resource.

The response from the previous POST request should be 201 Created, which indicates that your application is now registered with the UCWA server. The body of the response contains the application resource, which contains basic information about the application, links to related resources such as batch and the events, and four useful resources embedded in the application resource: [me](me_ref.md), [people](people_ref.md), [onlineMeetings](onlineMeetings_ref.md), and [communication](communication_ref.md).

An example of the response body might be as follows:




```
HTTP/1.1 201 Created
Date: Sat, 12 Jan 2013 01:00:47 GMT
Content-Type: application/json; charset=utf-8
...
{
 "culture":"en-US",
 "userAgent":"UCWA Samples",
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/105"},
 "policies":{"href":"/ucwa/oauth/v1/applications/105/policies"},
 "batch":{"href":"/ucwa/oauth/v1/applications/105/batch"},
 "events":{"href":"/ucwa/oauth/v1/applications/105/events?ack=1"}
 },
 "_embedded":{
 "me":{
 "name":"Lene Aaling",
 "uri":"sip:lenea@contoso.com",
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/105/me"},
 "makeMeAvailable":{"href":"/ucwa/oauth/v1/applications/105/me/makeMeAvailable"},
 "callForwardingSettings":{"href":"/ucwa/oauth/v1/applications/105/me/callForwardingSettings"},
 "phones":{"href":"/ucwa/oauth/v1/applications/105/me/phones"},
 "photo":{"href":"/ucwa/oauth/v1/applications/105/photos/lenea@contoso.com"}
 },
 "rel":"me"
 },
 "people":{
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/105/people"},
 "presenceSubscriptions":{"href":"/ucwa/oauth/v1/applications/105/people/presenceSubscriptions"},
 "subscribedContacts":{"href":"/ucwa/oauth/v1/applications/105/people/subscribedContacts"},
 "presenceSubscriptionMemberships":{"href":"/ucwa/oauth/v1/applications/105/people/presenceSubscriptionMemberships"},
 "myGroups":{"href":"/ucwa/oauth/v1/applications/105/people/groups"},
 "myGroupMemberships":{"href":"/ucwa/oauth/v1/applications/105/people/groupMemberships"},
 "myContacts":{"href":"/ucwa/oauth/v1/applications/105/people/contacts"},
 "myPrivacyRelationships":{"href":"/ucwa/oauth/v1/applications/105/people/privacyRelationships"},
 "myContactsAndGroupsSubscription":{"href":"/ucwa/oauth/v1/applications/105/people/contactsAndGroupsSubscription"},
 "search":{"href":"/ucwa/oauth/v1/applications/105/people/search"}
 },
 "rel":"people"
 },
 "onlineMeetings":{
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/105/onlineMeetings"},
 "myOnlineMeetings":{"href":"/ucwa/oauth/v1/applications/105/onlineMeetings/myOnlineMeetings"},
 "onlineMeetingDefaultValues":{"href":"/ucwa/oauth/v1/applications/105/onlineMeetings/defaultValues"},
 "onlineMeetingEligibleValues":{"href":"/ucwa/oauth/v1/applications/105/onlineMeetings/eligibleValues"},
 "onlineMeetingInvitationCustomization":{"href":"/ucwa/oauth/v1/applications/105/onlineMeetings/customInvitation"},
 "onlineMeetingPolicies":{"href":"/ucwa/oauth/v1/applications/105/onlineMeetings/policies"},
 "phoneDialInInformation":{"href":"/ucwa/oauth/v1/applications/105/onlineMeetings/phoneDialInInformation"},
 "myAssignedOnlineMeeting":{"href":"/ucwa/oauth/v1/applications/105/onlineMeetings/myOnlineMeetings/1XAW22CG"}
 },
 "rel":"onlineMeetings"
 },
 "communication":{
 "4d221db7-4af0-476b-9443-8bc874283085":"please pass this in a PUT request",
 "supportedModalities": [],
 "supportedMessageFormats": ["Plain"],
 "_links":{
 "self":{"href":"/ucwa/oauth/v1/applications/105/communication"},
 "conversations":{"href":"/ucwa/oauth/v1/applications/105/communication/conversations?filter=active"},
 "startMessaging":{"href":"/ucwa/oauth/v1/applications/105/communication/messagingInvitations"},
 "startOnlineMeeting":{"href":"/ucwa/oauth/v1/applications/105/communication/onlineMeetingInvitations?onlineMeetingUri=adhoc"},
 "joinOnlineMeeting":{"href":"/ucwa/oauth/v1/applications/105/communication/onlineMeetingInvitations"}
 },
 "rel":"communication",
 "etag":"3140562359"
 }
 },
 "rel":"application"
}
```


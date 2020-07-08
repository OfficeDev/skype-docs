
# Authentication in UCWA
Learn how an application authenticates a user in UCWA 2.0.


 _**Applies to:** Skype for Business 2015_

In order to authenticate a user in UCWA, an application needs to perform a number of steps as described in this article:

- When a person using your web application wants to access the Skype for Business service, your web application redirects to the service endpoint. The user enters credentials to authenticate and consents to grant access to service resources, if required.
 
 The service responds with a challenge containing the OAuth token issuer's URL and the types of authorization code grants it supports.
 
- The client application uses this information obtain an authorization token from the OAuth service.
 
- The client application uses the OAuth token to make requests to the service.
 

> [!NOTE] 
> If your application authenticates against an on-premises server, follow the authentication flow as described in this article. If the application authenticates against an online server, follow the Azure AD authentication flow as described in [Authentication using Azure AD](https://msdn.microsoft.com/en-us/skype/ucwa/authenticationusingazuread).


## Authentication Flow
<a name="sectionSection0"> </a>

Authentication flow is the process that your application goes through to respond to a challenge from the Skype for Business Autodiscover service and the Skype for Business UCWA 2.0 service. When your client application makes an initial request, the service responds with an HTTP 401 containing a WWW-Authenticate header, as shown in the following example:


```
WWW-Authenticate: MsRtcOAuth href=https://contoso.com/WebTicket/oauthtoken,grant_type="urn:microsoft.rtc:windows,urn:microsoft.rtc:anonmeeting,password"

```

The WWW-Authenticate header contains the following information.


1. MsRtcOAuth
 
2. The OAuth token issuer's URL.
 
3. The types of authorization code grants supported.
 
   - **password**: The password grant flow contains mandatory username and password parameters. This type of authentication is commonly used in forms-based authentication.
 
   - **urn:microsoft.rtc:windows**: This grant type is used when Integrated Windows Authentication (IWA) is used. The request is the same as the password grant, except that username and password parameters must not be present.
 
     > [!NOTE] 
     > Not all browsers support IWA. Additionally, if your flow contains redirects between domains or servers, the user may be prompted with an authentication dialog window.

   - **urn:microsoft.rtc:passive**: This grant type is used when any type of passive authentication is used. This works for Active Directory Federation Services-based scenarios and is useful when you need to delegate authentication to other directories. The request is the same as the password grant, except that username and password parameters must not be present.
 
     > [!NOTE] 
     > For more information about ADFS, see [Active Directory Federation Services (AD FS) 2.0](https://technet.microsoft.com/en-us/library/dd727958%28WS.10%29.aspx). For more information about configuring ADFS with Lync Server, see "Enabling Multi-Factor Authentication for Lync Web App" in [Deploying Lync Web App](https://technet.microsoft.com/en-us/library/jj205190.aspx).

   - **urn:microsoft.rtc:anonmeeting**: This grant type can be used to allow users join meetings anonymously.
 
A step-by-step example of how to do autodiscovery and authentication with an on-premises server is provided in [Create an application](https://msdn.microsoft.com/EN-US/library/dn356799%28v=office.16%29.aspx).


## Obtaining a token from the OAuth service
<a name="sectionSection1"> </a>

After your application receives an HTTP 401 Unauthorized response, it responds with a POST request directed to the provided OAuth service link, specifying the grant type desired. The service responds with an OAuth token. The following show how to obtain tokens with the various types of grants:


### Password grant type

 **Request**


```
POST https://contoso.com/WebTicket/oauthtoken HTTP/1.1
Content-Type: application/x-www-form-urlencoded;charset=UTF-8
.
.
.
grant_type=password&amp;username=johndoe&amp;password=A3ddj3w
```

 **Response**




```
HTTP/1.1 200 OK
Content-Type: application/json;charset=UTF-8
{
 "access_token":"cwt=2YotnFZFEjr1zCsicMWpAA...",
 "token_type":"Bearer",
 "expires_in":3600
}

```


### Windows grant type

 **Request**


```
POST https://contoso.com/WebTicket/oauthtoken HTTP/1.1
Content-Type: application/x-www-form-urlencoded;charset=UTF-8
.
.
.
grant_type=urn:microsoft.rtc:windows
```


> [!NOTE] 
> Using this grant type might cause a Windows authentication dialog to be displayed, asking the user to enter credentials.

 **Response**

Same as the response for the password grant type.


### Anonymous meeting grant type

 **Request**


```
POST https://contoso.com/WebTicket/oauthtoken HTTP/1.1 
Content-Type: application/x-www-form-urlencoded;charset=UTF-8 
.
.
.
grant_type=urn:microsoft.rtc:anonmeeting&amp;password=G03W98W4&amp;ms_rtc_conferenceuri=sip%John%contoso.com;gruu;opaque=app:conf:focus:id:G03W98W4
```

The Anonymous meeting attendee grant type uses two extension parameters: password and ms_rtc_conferenceuri. The password is the conference key.

The format of the ms_rtc_conferenceuri parameter is:<Organizer SIP URI>; gruu;opaque=app:conf:focus:id:<Conference ID>.

 **Response**

Same as the response for the password grant type.


### Passive grant type

 **Request**


```
POST https://contoso.com/WebTicket/oauthtoken HTTP/1.1
Content-Type: application/x-www-form-urlencoded;charset=UTF-8
Content-Length: 36
.
.
.
grant_type=urn:microsoft.rtc:passive
```

 **Response**




```
HTTP/1.1 400 Bad Request
Content-Type: application/json
X-Ms-diagnostics: 28020;source="server.contoso.com";reason="No valid security token."
X-MS-Server-Fqdn: server.contoso.com
X-Content-Type-Options: nosniff
Content-Length: 134

{"error":"invalid_grant","ms_rtc_passiveauthuri":"https:\/\/server.contoso.com\/PassiveAuth\/PassiveAuth.aspx"}
```


## Using the OAuth token
<a name="sectionSection2"> </a>

After your client application receives an access token from the OAuth service, it can use the token in requests to the UCWA 2.0 server using "Bearer" plus the OAuth token in the Authorization header as shown in the following example:


> [!NOTE] 
> UCWA 2.0 requires the presence of the Authorization header in each request.


## Error conditions
<a name="sectionSection3"> </a>

If the request for the access token fails, the service returns a 400 response and an error body, in a similar manner shown in the following example:


```
HTTP/1.1 400 Bad Request
Content-Type: application/json;charset=UTF-8
Cache-Control: no-store
Pragma: no-cache
X-Ms-diagnostics: 28029;source="server.contoso.com";reason="Authentication type not allowed."

{
 "error":"unsupported_grant_type"
}

```

The possible values for "error" are:


- **invalid_request**: The request is missing a required parameter, includes an unsupported parameter value (other than grant type), or is otherwise malformed.
 
- **invalid_grant**: The credentials provided could not be verified.
 
- **unsupported_grant_type**: The authorization grant type is not supported by the authorization server.
 
- **invalid_scope**: The requested scope is invalid, unknown, or malformed. The scope property does not need to be sent by the client. As defined by this proposal, the only supported value is "all".
 
- **server_error**: There was an unexpected error on the server that prevented the request from being honored.
 

> [!NOTE] 
> It is recommended that you do not take a code dependency against the X-Ms-diagnostics header.


## Refreshing an OAuth token
<a name="sectionSection4"> </a>

The lifetime of a token is eight (8) hours for authenticated users. The client application should monitor the expiration time and refresh the token as required. Refreshing a token for authenticated users is the same flow as acquiring a new token.

The lifetime of a token for anonymous meeting join is one (1) hour. It is possible to renew to the token by reusing the same token.

The client can use the ms_rtc_renew parameter to pass the original access token. This is necessary so that the anonymous identity originally generated is preserved throughout the meeting.

The following example shows the ms_rtc_renew parameter being used. Note that for brevity, the OAuth token is not entirely shown.




```
POST https://contoso.com/WebTicket/oauthtoken HTTP/1.1
Content-Type: application/x-www-form-urlencoded;charset=UTF-8

grant_type=urn:microsoft.rtc:anonmeeting&amp;password=5LB7MRBC&amp;ms_rtc_conferenceuri=sip:john@contoso.com;gruu;opaque=app:conf:focus:id:5LB7MRBC&amp;ms_rtc_renew=cwt%3dAA...L940

```


> [!NOTE] 
> The body content must be URL-encoded.

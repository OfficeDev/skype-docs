# Webhooks (Events)

In UCWA, a client had a pending GET parked on the server. The pending GET let a client receive any events from the UCWA server.
For Service applications (SA), web hooks are used to get events from the **Trusted Application API**.  Web hooks are HTTP callbacks that are usually started by some event.  When an event occurs, the Trusted App API makes an HTTP request to the URI configured as a Callback.
 
The event structure and syntax is similar to the UCWA event structure.
 
1. When a tenant specific application endpoint is registered with Skype for Business (sfb) Online, a default web hook callback url is registered for the SA, which can be unique for that tenant application endpoint.
  For example:
  If litware.com was the SA developer, a tenant application endpoint for IM modality for a contoso tenant is `helpdesk@contoso.com`, and can have a default url as `https://litware.com/callback`
2. For every conversation started or joined by the SA, a custom url can be set with context in the url itself
 
Using the previous example, if the SA was starting a new messaging invitation to an SFB online user in the contoso tenant, a custom url can be set in the `POST` on **messagingInvitation** request sent to the Trusted App API.
The API would then raise web hook events on that url for all events related to that conversation.
 
```
Post /platformservice/v1/applications/1627259584/communication/messagingInvitations?endpointId=sip:helpdesk@contoso.com

HTTP
client-request-id : ResExp/ff7c92a5-9349-47e6-93d5-b44351205018
Content-Type : multipart/related; boundary="805b28b2-04d8-4937-b38e-f4dcab9a4ea8"; type="application/json"
Content-Length : 290
--805b28b2-04d8-4937-b38e-f4dcab9a4ea8
Content-Type: application/json; charset=utf-8
 
{"operationId":"1000","subject":"IME2EPlainTextWithIdentity","to":"sip:UcapE2EUser1@contoso.com","callbackUrl":"https://litware.com/calback/customContext=xxx"}
--805b28b2-04d8-4937-b38e-f4dcab9a4ea8--
```
 
3. When the SA receives an invitation on the default url, it can request the API to send future calls related to that conversation, to be sent with some callback context
 
**Example:**
 
Call back received by SA at the default callback it set during application registration -
 
`Post https://litware.com:8999/callback`

The 200 OK response sent from SA to the API now has a custom callback context in it -

```HTTP
    200 OK
 
    Content-Type : application/json
    Content-Length : 23
    {
        "CallbackContext": "0"
    }
```
 
The next web hook event received by the SA will now be sent with this new context.
 
`Post https://litware.com:8999/callback?callbackContext=0`
 


## Authentication between Trusted App API and the Web Hook callback URL
 
Because your application should only accept web hook events from the Trusted App API, the web hook events are sent to your registered web hook url with an Azure Active Directory (AAD) oauth token. Use the token to validate the event.
 
Whenever the API raises a web hook event, the API gets an oauth token from AAD, with audience as the SA's AppID uri (set during [Registration in Azure Active Directory](onenote:#Registration%20in%20Azure%20Active%20Directory&section-id={309DC58E-4D90-43ED-B73E-6F306CD5675C}&page-id={14E37764-23D4-40F7-9821-A4CD5EE59904}&end&base-path=https://microsoft.sharepoint.com/teams/skypedevex/SiteAssets/Skype%20DevEx%20Notebook/Platform%20Service/Skype%20For%20Business%20Platform%20Onboarding.one)) and adds it in the Authorization header as a Bearer token.
 
The SA is expected to validate this token before accepting the callback request
 
 
```
Post https://litware.com/calback/customContext=xxx
 
HTTP
X-MS-Correlation-Id : 3953004368
client-request-id : e9168689-37ac-472e-b02a-c9977c8023a0
Authorization : Bearer XXX
Expect : 100-continue
Host : callback.litware.com
Content-Type : application/json; type="application/vnd.microsoft.com.ucwa+json"; charset=utf-8
Content-Length : 6797
{
    "_links": {
        "self": {
```
 
The oauth token would have values like the following, and will be signed by AAD.

```JavaScript
{
"typ": "JWT",
"alg": "RS256",
"x5t": "J8rFnzvEFkMYEEY70PM6H2M94fI",
"kid": "J8rFnzvEFkMYEEY70PM6H2M94fI"
}{
"aud": "https://litware.com",
"iss": "https://sts.windows.net/1fdd12d0-4620-44ed-baec-459b611f84b2/",
"iat": 1466741440,
"nbf": 1466741440,
"exp": 1466745340,
"appid": "00000004-0000-0ff1-ce00-000000000000",
"appidacr": "2",
"idp": "https://sts.windows.net/1fdd12d0-4620-44ed-baec-459b611f84b2/",
"oid": "2d452913-80c9-4b56-8419-43a7da179822",
"sub": "2d452913-80c9-4b56-8419-43a7da179822",
"tid": "1fdd12d0-4620-44ed-baec-459b611f84b2",
"ver": "1.0"
}
```

 
* **aud** (audience) is the App ID uri specified for the SA during [Registration in Azure Active Directory](onenote:#Registration%20in%20Azure%20Active%20Directory&section-id={309DC58E-4D90-43ED-B73E-6F306CD5675C}&page-id={14E37764-23D4-40F7-9821-A4CD5EE59904}&end&base-path=https://microsoft.sharepoint.com/teams/skypedevex/SiteAssets/Skype%20DevEx%20Notebook/Platform%20Service/Skype%20For%20Business%20Platform%20Onboarding.one)
* **tid** is the tenant id for contoso
* **iss** is the token issuer, which is AAD's url
 
The listener interface on the url can validate the oauth token, checking whether AAD indeed issued and signed the token. We recommend you check whether audience matches your AppID uri before accepting the callback request.
 
### Additional information

You can read more about [AAD token validation](http://www.cloudidentity.com/blog/2014/03/03/principles-of-token-validation/) in Vittorio Bertocci's **Cloud Identity** blog.

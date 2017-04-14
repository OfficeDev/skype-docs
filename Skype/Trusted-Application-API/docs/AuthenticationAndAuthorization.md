# Authentication and Authorization

 The application does a `GET` on the **service:applications** link returned by the [discover](DiscoveryForServiceApplications.md) operation, after appending an application endpoint id to the value in the link.

The following snippet is an example of such a GET.
```
GET https://api.skypeforbusiness.com/platformService/v1/applications?endpointId=sip:helpdesk@contoso.com
```

The client receives a 401 challenge in a response. The client responds to the authentication challenge by sending an AAD oauth token along with the next request.

**Trusted Application API** endpoints require an oauth token with an Application Identity from Azure Active Directory using the client credential grant flow.

## Additional information

- [Authorize access to web applications using OAuth 2.0 and Azure Active Directory](https://msdn.microsoft.com/en-us/library/azure/dn645543.aspx)

- [Authentication Scenarios for Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios)

 
## Authentication details
The audience for the oauth token should be the base fqdn of the discover url: https://api.skypeforbusiness.com

 

The token is sent in the Authorization header, for example:

 
```
GET https://api.skypeforbusiness.com/platformService/v1/applications?endpointId=sip:helpdesk@contoso.com
Authorization: Bearer <token from AAD>
```
 
### API request validation

Each HTTP request sent by an Trusted Application client must contain an oauth token in the **Authorization** header. The API performs the following validity checks against the presented oath token:

- The API uses the oauth token to identify the application that made the GET request.
- The API gets the tenant id from the oauth token to associate the request with a tenant.
- Validates the **endpointid** query parameter against an application endpoint configured by the tenant. 
- On failure to match the application or the application endpoint, sends a 403 with the subcode **ApplicationNotFound**.
- On application or the application endpoint match, creates a dedicated url to service the application and returns it as below:

#### Example 
The following is an example of a json object sent in response to a matching application endpoint

**service:application:** 

```json
{"href": "/platformservice/v1/applications/1627259584?endpointId=sip%3ahelpdesk%40contoso.com"}
```

When the Trusted Application client sends a GET request on this new URL, The API returns the capabilities supported for that application endpoint.

```
 Get https://api.skypeforbusiness.com/platformService/v1/applications/1627259584?endpointId=sip:helpdesk%40contoso.com
```
The json payload of the response to the GET request is strucured as in the following example. 

```json

{
    "_links": {
        "self": {
            "href": "/platformservice/v1/applications/1627259584?endpointId=sip%3ahelpdesk%40contoso.com"
        }
    },
    "_embedded": {
        "service:communication": {
            "_links": {
                "self": {
                    "href": "/platformservice/v1/applications/1627259584/communication?endpointId=sip:helpdesk@contoso.com"
                },
                "service:startMessaging": {
                    "href": "/platformservice/v1/applications/1627259584/communication/messagingInvitations?endpointId=sip:helpdesk@contoso.com"
                }
            },
            "rel": "service:communication",
            "etag": "4294967295"
        }
    },
    "rel": "service:application"
}
```

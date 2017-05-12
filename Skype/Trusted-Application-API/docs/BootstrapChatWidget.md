# Bootstrap chat widget

When the chat widget provided by the Service Application (SA) starts up, it has to send an HTTP request to the SA backend to get a token and an endpoint to authenticate. After getting a token and authenticating, it sends messages directed to chat support.
The SA sends HTTP requests to the Trusted Applications (Trusted Application) API anonymous token endpoint to get this information
 
 
1. The SA runs the **Trusted Application API** Discovery, authentication and authorization steps to get the capabilities of Trusted Application.
The call flows for this step are described earlier in these topics: 
   - [Discovery for Service Applications](DiscoveryForServiceApplications.md)
   - [Authentication and Authorization](./AuthenticationAndAuthorization.md)
 
The following example shows how the capabilities selected and consented earlier, would appear in the response.
 
 
```json
{
"_links": {
"self": {
"href": "/platformservice/v1/applications/1627259584?endpointId=sip%3ahelpdesk%40contoso.com"
},
"service:anonApplicationTokens": {
"href": "/platformservice/v1/applications/1627259584/anonApplicationTokens?endpointId=sip:helpdesk@contoso.com"
}
},
"_embedded": {
"service:communication": {
"_links": {
"self": {
"href": "/platformservice/v1/applications/1627259584/communication?endpointId=sip:helpdesk@contoso.com"
},
"service:joinOnlineMeeting": {
"href": "/platformservice/v1/applications/1627259584/communication/onlineMeetingInvitations?endpointId=sip:helpdesk@contoso.com"
},
"service:inviteUserToMeeting": {
"href": "/platformservice/v1/applications/1627259584/communication/userMeetingInvitations?endpointId=sip:helpdesk@contoso.com"
},
"service:startMessaging": {
"href": "/platformservice/v1/applications/1627259584/communication/messagingInvitations?endpointId=sip:helpdesk@contoso.com"
}
},
"rel": "service:communication",
"etag": "4294967295"
},
"service:adhocMeetings": {
"_links": {
"self": {
"href": "/platformservice/v1/applications/1627259584/adhocMeetings?endpointId=sip:helpdesk@contoso.com"
}
},
"rel": "service:adhocMeetings"
}
},
"rel": "service:application"
}
 ```
 
The SA uses the **anonApplicationTokens** endpoint to get a token for the anonymous users of the chat widget.
 
```HTTP 
Post /platformservice/v1/applications/1627259584/anonApplicationTokens?endpointId=sip:helpdesk@contoso.com
 
client-request-id : ResExp/c7d3f4d4-9cb1-4ea4-9787-948e75480abe
Content-Type : application/json; charset=utf-8
Content-Length : 143
{
    "applicationSessionId": "DFEDde...",
    "allowedOrigins": "https://contoso.com;https://litware.com"
}
``` 

 

**200 OK**
```json
{
    "token": "psat=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik5TSV9rVzg1cnFMTEN0VTE1dWlnQ2gxTlZfYyJ9.eyJuYmYiOjE0NjY5NjI5MzMsImV4cCI6MTQ2Njk5MTczMywicnV1Ijoic2lwOlVjYXBVc2VyMTNAdWNhcHRlbmFudC5jb20iLCJhc2kiOiJra2trIiwiYWV1Ijoic2lwOmhlbHBkZXNrQHVjYXB0ZW5hbnR0aGlyZHBhcnR5LmNvbSIsImFvIjoiaHR0cHM6Ly9jb250b3NvLmNvbTtodHRwczovL2xpdHdhcmUuY29tO2h0dHA6Ly93d3cubWljcm9zb2Z0c3RvcmUuY29tIn0.NDlu02-C1snxPHrdoHrext_rDhvRHL49KV2OB1kjin1aJbGsg9nqH6KhCAJ4cwqY1tk6LIydmHx5INV6Lp-8ftQuz6BiM23D0zEj9_j_HuiBpgKzP_BMwm2zrhIdZ6hDiWBI843wwJFmdEB6FbavuewHs_sGNHk3rwPnF_PtJpQ_5hptwN9usf9U7gR0EunXJyPKRtCIFodnztF6MWw9CqhCxPlb6g6EX_kFPr4Btx6X4ncacQAGRu3A-ja3cke_Dy0bDAHA3LK56XaEJwKpfPoDHr9Wc7-leBfkknH4XpkzdNjtWuMuqyTAQH3cLPsy-eW3DIP5RuFNW1-p-gwj5w",
    "expiryTime": "2016-06-27T01:42:13.094Z",
    "_links": {
        "self": {
            "href": "https://ext.vdomain.com:4443/platformservice/v1/applications/1627259584/anonApplicationTokens?endpointId=sip:helpdesk@contoso.com"
        },
        "service:discover": {
            "href": "https://api.skypeforbusiness.com/platformService/discover?anonymousContext=psat%253deyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik5TSV9rVzg1cnFMTEN0VTE1dWlnQ2gxTlZfYyJ9.eyJuYmYiOjE0NjY5NjI5MzMsImV4cCI6MTQ2Njk5MTczMywicnV1Ijoic2lwOlVjYXBVc2VyMTNAdWNhcHRlbmFudC5jb20iLCJhc2kiOiJra2trIiwiYWV1Ijoic2lwOmhlbHBkZXNrQHVjYXB0ZW5hbnR0aGlyZHBhcnR5LmNvbSIsImFvIjoiaHR0cHM6Ly9jb250b3NvLmNvbTtodHRwczovL2xpdHdhcmUuY29tO2h0dHA6Ly93d3cubWljcm9zb2Z0c3RvcmUuY29tIn0.NDlu02-C1snxPHrdoHrext_rDhvRHL49KV2OB1kjin1aJbGsg9nqH6KhCAJ4cwqY1tk6LIydmHx5INV6Lp-8ftQuz6BiM23D0zEj9_j_HuiBpgKzP_BMwm2zrhIdZ6hDiWBI843wwJFmdEB6FbavuewHs_sGNHk3rwPnF_PtJpQ_5hptwN9usf9U7gR0EunXJyPKRtCIFodnztF6MWw9CqhCxPlb6g6EX_kFPr4Btx6X4ncacQAGRu3A-ja3cke_Dy0bDAHA3LK56XaEJwKpfPoDHr9Wc7-leBfkknH4XpkzdNjtWuMuqyTAQH3cLPsy-eW3DIP5RuFNW1-p-gwj5w"
        }
    },
    "rel": "service:anonApplicationToken"
}
```
 
**Request Parameters:**

- **applicationSessionId** is a unique value you can use to track the user's session with, and will be embedded in the token returned. Note that the value of applicationSessionId should be unique across all sessions and should not be re-used for the same user on multiple sign-in attempts.
- **allowedOrigins** is a list of http origins from which the chat widget can send cross origin requests. This is typically the scheme and authority segments of the URI where the chat widget is loaded
 
**Response:**
 
- The **token** link has the authentication token that is sent back to the chat widget for the anonymous user for authentication.
- **service:discover** link that chat client goes to first, to get the UCWA url to send the chat messages.
 
 
 
The SA sends the info from the response, to the chat widget.
 
 
## Next step
Now that the chat widget is bootstrapped and has the anonymous application token, it can get the Trusted Application endpoint.
- [Discovery by chat client](./DiscoveryChatClient.md) 
 
 
 
 
 
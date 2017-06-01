# Discovery by chat client

The [Bootstrap chat widget](BootstrapChatWidget.md) topic showed how to get the Trusted Application API discovery link and anonymous application token. The token is used for any future authentication challenge.
 
In discovery, the client sends an HTTP GET request on the discover link and gets an **anonApplications** link in the response. This is the [ucwa applications resource](https://msdn.microsoft.com/en-us/skype/ucwa/applications_ref), where the client can create a Trusted Application and start sending messages to the chat support endpoint.
 
```http 
Get /platformservice/discover?anonymousContext=psat%253deyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik5TSV9rVzg1cnFMTEN0VTE1dWlnQ2gxTlZfYyJ9.eyJuYmYiOjE0NjY5NjI5MzMsImV4cCI6MTQ2Njk5MTczMywicnV1Ijoic2lwOlVjYXBVc2VyMTNAdWNhcHRlbmFudC5jb20iLCJhc2kiOiJra2trIiwiYWV1Ijoic2lwOmhlbHBkZXNrQHVjYXB0ZW5hbnR0aGlyZHBhcnR5LmNvbSIsImFvIjoiaHR0cHM6Ly9jb250b3NvLmNvbTtodHRwczovL2xpdHdhcmUuY29tO2h0dHA6Ly93d3cubWljcm9zb2Z0c3RvcmUuY29tIn0.NDlu02-C1snxPHrdoHrext_rDhvRHL49KV2OB1kjin1aJbGsg9nqH6KhCAJ4cwqY1tk6LIydmHx5INV6Lp-8ftQuz6BiM23D0zEj9_j_HuiBpgKzP_BMwm2zrhIdZ6hDiWBI843wwJFmdEB6FbavuewHs_sGNHk3rwPnF_PtJpQ_5hptwN9usf9U7gR0EunXJyPKRtCIFodnztF6MWw9CqhCxPlb6g6EX_kFPr4Btx6X4ncacQAGRu3A-ja3cke_Dy0bDAHA3LK56XaEJwKpfPoDHr9Wc7-leBfkknH4XpkzdNjtWuMuqyTAQH3cLPsy-eW3DIP5RuFNW1-p-gwj5w
 
client-request-id : ResExp/fb793a9d-78b4-4c21-a2e5-c7a9482b0140
Origin : http://contoso.com
```
 
**200 OK**
 
```JSON 
{
    "_links": {
           "anonApplications": {
            "href": "https://ext.vdomain.com:4443/ucwa/psanon/v1/applications"
        }
    },
    "rel": "service:discover"
}
``` 
## Next step
Now that the browser client has the Trusted Application API endpoint, it must insure that any cross origin requests are honored.
- [Cross Origin Requests from browser based chat clients](CORChatClient.md)

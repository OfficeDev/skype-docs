# Cross Origin Requests from browser based chat clients

Browser chat clients will send cross origin requests to endpoints returned to clients in discovery responses.
 
The popular browsers implement the cross origin resource sharing (CORS) spec defined at [w3.org](http://www.w3.org/TR/cors/). The spec defines the logic that allows or prohibits responses from **Trusted Application API** when those responses cross origins. When a response is allowed, it reachs the javascript client in the browser. **Trusted Application API** inserts a response header to signal browser CORS logic that the **Trusted Application API** accepts cross origin requests from a browser client.
 
 
The **Trusted Application API** requires the list of websites from which the browser based chat clients can send cross origin requests. This list of web origins are sent to Trusted Application as a request parameter **allowedOrigins** when requesting the Anonymous Applications Token. The web origins value can be a semicolon separated list.
 
The following example creates an anonymous application token by sending a POST request to [**service:anonApplicationTokens**](https://ucwa.skype.com/trustedapplicationapi/Resources/service_anonApplicationTokens.html)
 
```http
Post /platformservice/v1/applications/1627259584/anonApplicationTokens?endpointId=sip:helpdesk@contoso.com
 
client-request-id : ResExp/c7d3f4d4-9cb1-4ea4-9787-948e75480abe
Content-Type : application/json; charset=utf-8
Content-Length : 143
{
    "applicationSessionId": "DSFWE...",
    "allowedOrigins": "https://contoso.com"
}
``` 
  
When the chat client later uses this token from one of the websites mentioned in the **allowedOrigins** request parameter, the **Trusted Application API** will respond with the required CORS headers.
 
## Next step
Now that you're familiar with bootstrapping the client, discovering the application endpoint, and CORS support, you should review a complete client chat callflow.
- [Customer Chat care call flow](CallFlow1.md) 
 
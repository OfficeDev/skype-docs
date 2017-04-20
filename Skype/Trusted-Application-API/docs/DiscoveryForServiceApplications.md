# Discovery for Service Applications

 
**Trusted Application API**s are discovered using the discovery endpoint at **https://api.skypeforbusiness.com/platformservice/discover**.
This is a standard url which the **Trusted Application API** will expose in order for Service Applications to connect to the API and use the capabilities exposed. 

Discovery request must be authenticated with a valid **OAuth token**. Please refer [Azure Active Directory - Service to Service calls using Client Credentials](./AADS2S.md) for more information on how to get OAuth token.

 

Example:

Running a `GET` request on **https://api.skypeforbusiness.com/platformservice/discover** with a valid **Oauth token** returns a json response with the following link. The link is the starting point for all operations by Service Applications on the API

```json
"service:applications":{"href":"https://api.skypeforbusiness.com/platformService/v1/applications"}
```


 
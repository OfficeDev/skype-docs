# Anonymous Meeting Join

Anonymous meeting join is a powerful feature of the Skype Developer Platform that enables you to join guest users into Skype Meetings to deliver "remote advisor" and other Business-to-Consumer solutions.  For Skype for Business Online, anonymous meeting join is supported through the **Trusted Application API**.  The guest join meeting flow for anonymous meetings can use the **Trusted Application API** plus the client-side features of either the Skype Web SDK, App SDK, or UCWA.
The anonymous user can join into Skype meetings by using a meeting's URI.
An online meeting can be scheduled using the Skype for Business Client or Outlook, or even programmatically using UCWA or the **Trusted Application API**.  The meeting's URL is passed to the service application, which calls the Trusted Application endpoint and enables anonymous users to join the online meeting.

You'll need to use a **Trusted Application API**-enabled service application to get the Discover Url and anonymous meeting token. Your mobile app
will call the service application to get these resources before joining a meeting. To learn more about this, see 
- [Use the App SDK and the Trusted Application API to join an Online meeting - Android](../../AppSDK/HowToJoinOnlineMeeting_Android.md)
- [Use the App SDK and the Trusted Application API to join an Online meeting - iOS](../../AppSDK/HowToJoinOnlineMeeting_iOS.md)
- [Implementing a Anonymous Client with the Skype Web SDK](ImplementingAnonymousClientWithSkypeWebSDK.md)

## Prerequisites:
- Please read [Developing **Trusted Application API** applications for Skype for Business Online](./DevelopingApplicationsforSFBOnline.md) to learn how to develop **Trusted Application API** service applications for anonymous meeting join call flow.
In [Registering your application in Azure AD](./RegistrationInAzureActiveDirectory.md) section, please make sure that the following application permissions are selected for **Anonymous meeting join** 

### Required permissions
- Guest user join services (preview)
- Send/Receive Audio and Video (preview)
- Create on-demand Skype meetings (preview)
- Send/Receive Instant Messages (preview)

![alt text](./images/GuestMeetingJoinTenantConsent.png "image") 

![alt text](./images/RegistrationForGuestMeetingJoin.png "image")

## Call Flow

1. An online meeting is created either using UCWA APIs or any other means. Please refer [Anonymous Meeting Scheduling](./AnonymousMeetingSchedule.md) for more information on scheduling an Anonymous meeting.

2. This meeting's url is shared with the client who wants to join the meeting anonymously.

3. When the client decides to join the meeting, it pings the Service Application with the meeting's url.
    ```
    Request to join "https://meet.lync.com/contoso/testuser/1SD8D0WZ"
    ```

4. The Service Application talks to the **Trusted Application API** endpoints using the [discovery](./DiscoveryForServiceApplications.md) and [authentication](./AuthenticationAndAuthorization.md) mechanism.

   >Note: All the **Trusted Application API** endpoints require authentication using **OAuth token**. Please refer [Azure Active Directory - Service to Service calls using Client Credentials](./AADS2S.md) for more information on how to get a OAuth Token.
    
    - The Service Application gets an anonymous application token, and a discover url, when it passes in the meeting url to the AnonApplicationsToken endpoint of the **Trusted Application API**. The flow is as follow:
    
        1. **Discovery**
            - Discover request - The Service Application also known as SaaS application(SA) discovers the location of the **Trusted Application API**. This GET request must be authenticated with a valid **OAuth token**.

                 ```
                GET https://api.skypeforbusiness.com/platformservice/discover
                ```
            - Discovery Response - returns the link to the Trusted Applications API.
                ```
                200 OK, "service:applications":{"href":"https://api.skypeforbusiness.com/platformService/v1/applications"}
                ```
        2. **Get the capabilities**
       
            - We send a GET request with Trusted Applications API link received from the previous discovery Request. This GET request must be authenticated with a valid **OAuth token**.
                
                ```
                    //Capabilities request without valid Oauth token gets '401 Unauthorized' response
            
                    GET https://api.skypeforbusiness.com/platformService/v1/applications

                    //Capabilities request with valid Oauth token
            
                    GET https://api.skypeforbusiness.com/platformService/v1/applications
                    Authorization: Bearer XXXX

                    //Capabilities Response - Anonymous application tokens capabilities.

                    200 OK,service:anonApplicationTokens":{"href":"/platformservice/v1/applications/1627259584/anonApplicationTokens?endpointId=sip:helpdesk@contoso.com"}
                ```
        3. **Request for Token and service:discover links**
        
            - Post on anonApplicationTokens link to get the "token" and "service:discover" links**
                
                ```
                    Post /platformservice/v1/applications/1627259584/anonApplicationTokens?endpointId=sip:helpdesk@contoso.com
                    "applicationSessionId":"uniqueString","allowedOrigins":"https://contoso.com;https://litware.com","meetingUrl":"https://meet.lync.com/contoso/testuser/1SD8D0WZ"
                ```
            - Response - 200 OK with token and discovery link
                ```
                    "token":"psat=eyJ0eX...","expiryTime":"2016-06-27T01:42:13.094Z","service:discover":{"href":"https://api.skypeforbusiness.com/platformService/discover?anonymousContext=psat%253deyJ0eX..."}
                ```
                > Note: It is important to pass in a unique applicationSessionId parameter for every client that wants to join a meeting anonymously. This can be a Guid. 
   
    - The Service Application then passes the token and the discover url to the client that initially pinged it, in order to join the meeting anonymously.
        ```
        send service:discover url and token
        ```

5. The client then does a GET on the discover url, to know the _'AnonApplications Endpoint'_ and the _'Conference ID'_. The anonApplications endpoint is the UCWA API endpoint to which the client has to connect.
    
    - GET on service:discover url to find "anonApplications" resource and conference id
        ```
         Get /platformservice/discover?anonymousContext=psat%253deyJ0e...
         Origin : https://litware.com
        ```
    - Response
        ```
        200 OK
        anonApplications": {"href": "/ucwa/psanon/v1/applications"}
        conferenceId=sip:testuser@contoso.onmicrosoft.com;gruu;opaque=app:conf:focus:id:1SD8D0WZ
        ```

6. The client does a POST on the anonApplications endpoint, with the token sent to it by the Service Applacation, in the Authorization header, creating the UCWA application endpoint.
    - Create Ucwa application at "anonApplications"  link using token returned earlier
        ```
        Post /ucwa/psanon/v1/applications
        Origin : https://litware.com {"anonymousDisplayName":"JohnDoe","culture":"en-us","userAgent":"foo"}
        Authorization: Bearer psat=eyJ0eX....
        ```
    - Reponse - '201 Created'

7. From the resources in the response, the client then does a POST on onlineMeetingInvitations, passing in the conference from Step5, ultimately joining the meeting anonymously.
    - Create the meeting
        ```
        POST /ucwa/psanon/v1/applications/10485150973/communication/onlineMeetingInvitations
        Origin : https://litware.com
        {"anonymousDisplayName":"John Doe","importance":"Normal",
        "onlineMeetingUri":"sip:testuser@contoso.onmicrosoft.com;gruu;opaque=app:conf:focus:id:1SD8D0WZ",
        "operationId":"abc","subject":"mysubject"}   
        ```
> Note: Multiple such clients can join the meeting anonymously. In case a client drops out and needs to rejoin for any reason, you can pass in the same applicationSessionId parameter to get a new token and discover url. The client can use the new values, to rejoin the meeting.

![alt text](./images/CallFlowAnonMeetingJoin.jpg "image")

## Related links
- [Anonymous Meeting Scheduling](./AnonymousMeetingSchedule.md)


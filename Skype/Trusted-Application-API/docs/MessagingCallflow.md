# Simple messaging call flow

This topic introduces a simple messaging callflow between a service application, the Skype Trusted Application endpoint, and a user on a Skype for Business online client. To implement this callfow, your SaaS app must
**initialize the API** and then **get the capabilites** of the Trusted Application endpoint. After that, the order of simple message callflow logic is dictated by
the actions of the users at either end of the conversation. The callflow in this topic is contrived to show you typical RESTful requests and responses in a 
messaging conversation rather than a well defined sequence of steps after initializing and getting capabilities.


The call flow is as follows:

## Initialize the API
   
1. **Discovery**
    
    a. The Service Application also known as SaaS application(SA) discovers the location of the **Trusted Application API**.
    
    ```
    GET https://api.skypeforbusiness.com/platformservice/discover
    ```
    b. Discovery Response - returns the link to the Trusted Applications API.

    ```
    200 OK, "service:applications":{"href":"https://api.skypeforbusiness.com/platformService/v1/applications"}    
    ```
    
2. **Get the capabilities**
       
   a. We send a **GET** request with Trusted Applications API link received from the previous discovery Request.
    **This GET request must be authenticated with a valid OAuth token.**
    
    ```
    GET  https://api.skypeforbusiness.com/platformService/v1/applications
    ```
     
    >Note: A request without a valid OAuth token will return '401 Unauthorized response'.

   b. Capabilities **GET** Request with a valid OAuth token.
    ```
    https://api.skypeforbusiness.com/platformService/v1/applications
    Authorization: Bearer XXXX
    ```
   c. Capabilities Response - You receive SFB Online user's sip URI in response.
   In this case, only messaging capability is available

    ```
 
    messaging capability:200 OK, "service:startMessaging": {
    "href": "/platformservice/v1/applications/1627259584/communication/messagingInvitations?endpointId=sip:helpdesk@contoso.com"} 
    ```
![call flow](./images/MessagingCallFlowsInitialize.jpg)
    
## The Service Application (SA) starts messaging to the SfB online User

1. **The Service Application (SA) sends a messaging invitation --> **Trusted Application API** forwards it --> SfB online User**

   a. Message invitation - We send **POST** request using SFB Online user's sip URI. Also, specifying operation ID to track this conversation and custom callback url for this conversation
    ```
    Post /platformservice/v1/applications/1627259584/communication/messagingInvitations?endpointId=sip:helpdesk@contoso.com
    {"operationId":"1000",
    "subject":"IME2EPlainTextWithIdentity",
    "to":"sip:UcapE2EUser1@contoso.com",
    "callbackUrl":"https://litware.com/callback"} 
    ```
   b. Successful conversation creation returns '201 Response status'
   c. The **Trusted Application API** informs the Service Application (SA) via a **callback** that a new conversation is created with messaging invitation details including operation ID for tracking. 
    ```
    Post https://litware.com/callback
    "rel": "service:communication", 
    "href": "/platformservice/v1/applications/1627259584/communication?endpointId=sip:helpdesk@contoso.com"
    "service:messagingInvitation": {"direction": "Outgoing","state": "Connecting","operationId": "1000","importance": "Normal","subject": "IME2EPlainTextWithIdentity",...
     {"rel": "service:conversation","href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20?endpointId=sip:helpdesk@contoso.com"
    }
    ```
    > Note: SFB online user can be using any client (sip, mobile, web client, etc.)
 
2. **SFB online user joins the conversation**

   a. The **Trusted Application API** informs the Service Application (SA) via a **callback** that SFB online user has joined the conversation.
    
    ```
    Post https://litware.com/callback
    ": { "rel": "service:participant", 
    "href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584/communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/participants/ucape2euser1@contoso.com?endpointId=sip:helpdesk@contoso.com",
    "title": UcapE2EUser1"}
    "type": "added"
   ```
![call flow](./images/MessagingCallFlowsStartMessaging.jpg)

## SFB online user sends a plain text message
    
1. The **Trusted Application API** informs the Service Application (SA) via a **callback** that the participant has sent a message, and the message body itself.

   a. CallBack 
    ```
    Post https://litware.com/callback
    {"rel": "service:message", "href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/messages/2?endpointId=sip:helpdesk@contoso.com"}...
    "plainMessage": { "href": "data:text/plain;charset=utf-8,Message+1+from+2+to+1"}
    ```
![call flow](./images/MessagingCallFlowsUserSendsMessage.jpg)

## The Service Application(SA) replies to the message

1. Before replying, configure the conversation to show that the Service Application is typing a message.

   a. is typing message 
    ```
    Post /platformservice/tgt-c39794177c465e6c922bd0737e01fd03
    /v1/applications/1627259584/communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/typing?endpointId=sip:helpdesk@contoso.com
    ```
    
2. The Service Application send message to the **Trusted Application API** in the conversation, setting an operationid for context. Further, the **Trusted Application API** forwards it to the SFB online user.

   a. POST send message request
    ```
    Post /platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications
    /1627259584/communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/messages?endpointId=sip:helpdesk@contoso.com&operationId=1001
    Content-Type : text/plain; charset=utf-8
    Content-Length : 21
    Message 1 from 1 to 2
    ```
    
   b. The Service application is informed via a callback that the message was delivered successfully.
   
    ``` 
    Post https://litware.com/callback
    "rel": "service:message",
    "href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/messages/3?endpointId=sip:helpdesk@contoso.com"}                   
    "status": "Success","_embedded": {"service:message": {"direction": "Outgoing",
    ```
![call flow](./images/MessagingCallFlowsAppSendsMessage1.jpg)

## SFB online user sends a html message

1. The **Trusted Application API** informs the Service Application via **callback** that SFB online user is typing.
   - CallBack

    ```
    Post https://litware.com/callback
    "rel": "service:participant",  
    "href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/participants/ucape2euser1@contoso.com?endpointId=sip:helpdesk@contoso.com", 
    "title": "UcapE2EUser1"}
    "in": {"rel": "service:typingParticipants","href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03
    /v1/applications/1627259584/communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/typingParticipants?endpointId=sip:helpdesk@contoso.com"
    "type": "added"
    ```
 
2. The **Trusted Application API** sends a message sent state change to the Service Application via **callback** indicating the participant has sent a message, and the message body itself.
   - CallBack 
 
    ```
    Post https://litware.com/callback
    "rel": "service:message",
    "href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/messages/4?endpointId=sip:helpdesk@contoso.com"
    "htmlMessage": {"href": "data:text/html;charset=utf-8,%3cp%3eMessage+2+from+%3ci%3e2%3c%2fi%3e+to+%3cb%3e1%3c%2fb%3e%3c%2fp%3e"}}
    ```
![call flow](./images/MessagingCallFlowsUserSendsHtmlMessage.jpg)

## The Service Application responds with a html message

1. Before replying to the message, set the conversation to show that the service application is typing a message
   a. Is typing message 
    ```
    Post /platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/typing?endpointId=sip:helpdesk@contoso.com
    ```

2.  The Service Application(SA) sends the message in the conversation, setting an **operationid** for the context to the **Trusted Application API**, which forwards it to the SFB online user.
    b. Send message POST request
    ```
    Post /platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/messages?endpointId=sip:helpdesk@contoso.com&operationId=1001
    Content-Type : text/html; charset=utf-8 Content-Length : 55 
    <html><p>Message 2 from <i>1</i> to <b>2</b></p></html>
     ```
3. The Service Application is informed via a **callback**  that the message was successfully delivered .
    
    ```
    Post https://litware.com/callback
    "rel": "service:message", "href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20/messaging/messages/5?endpointId=sip:helpdesk@contoso.com"}                   
    "status": "Success",
    "_embedded": {"service:message": {"direction": "Outgoing",
    ```
![call flow](./images/MessagingCallFlowsAppsendsmessage2.jpg)

## SFB online user terminates the conversation
 
1. The **Trusted Application API** informs the Service Application via **callback** that the conversation has no more participants, no more messaing and conversation has been deleted.
   a. CallBack
    ```
    Post https://litware.com/callback
    "rel": "service:conversation", 
    "href": "/platformservice/tgt-c39794177c465e6c922bd0737e01fd03/v1/applications/1627259584
    /communication/conversations/f503a6b3-8622-4d61-9f44-b0298a83de20?endpointId=sip:helpdesk@contoso.com"},
    "type": "deleted"
    ```
![call flow](./images/MessagingCallFlowsEndConversation.jpg)

>Note: **Trusted Application API** informs the Service Application via **callback** url. In order for the Service Application to keep context when starting a new invitation or message, the service application should pass an operation id in the request. This id will be sent back in the callback event.
 
The general principles, capabilities, API style, API concepts are modeled to closely follow the previously released **UCWA** API. Please see this link for detailed info - [UCWA API reference](https://msdn.microsoft.com/en-us/skype/ucwa/ucwa2_0apireference)
Being familiar with the UCWA concepts greatly simplifies understanding the **Trusted Application API**s
  
The call flows are shown in the article diagrams. Each of the capability related APIs, related callbacks are modeled on the **UCWA** capabilities and callback structure. Please refer to the [UCWA API reference](https://msdn.microsoft.com/en-us/skype/ucwa/ucwa2_0apireference) to get detailed description about these  calls.



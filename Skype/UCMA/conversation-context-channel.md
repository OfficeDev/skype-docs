---
title: Conversation context channel
TOCTitle: Conversation context channel
ms:assetid: a09a4e37-02f8-4a13-bb29-527dbd5ba086
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466012(v=office.16)
ms:contentKeyID: 65239945
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Conversation context channel


**Applies to**: Skype for Business 2015



The [ConversationContextChannel](https://msdn.microsoft.com/en-us/library/hh161849\(v=office.16\)) class enables a UCMA 5.0 endpoint to launch and interact with a Conversation Extension window in Skype for Business 2015.

## Registering an application extension for a conversation context channel

If a Skype for Business 2015 client is being used, the endpoint must register an application extension on the local machine for the client to process contextual data. This is done by adding the application to the registry on the target machine. For more information see "Register Contextual Conversation Packages" in the Skype for Business SDK documentation. This protects against unauthorized applications being popped on the client by a rogue remote endpoint. Further, to prevent spamming attacks, only UCMA-based trusted applications are permitted to supply data to client extensibility applications as part of a conference.

## Conversation context channel data traffic and limitations

- The contextual channel is currently exposed through , Skype for Business 2015, and Microsoft Unified Communications Managed API 5.0. The initial channel is established with an INVITE message. After being established, the channel exchanges data using the INFO method.
    
  The initial INVITE payload must be well-formed XML, and less than 4 KB in size. The INFO payload is an unrestricted, generic message.

- Application data can be refreshed and sent multiple times over the course of the call. It is treated as a ‘raw,’ two-way data pipe between applications, and can be used for everything from contextual data to commands outside the normal communication channels.

- Multiple applications can be spawned during the course of a call, with each such application having its own data channel. These data channels can be addressed separately by the application, to enable scenarios in which there are a basic set of controls provided to all users, and an advanced add-on for managers, super-users, and administrators.
    
  > [!IMPORTANT]
  > Multiple requests for a given client application from different server application are sent to a single client application instance.

- Contextual channels can be established at any time during the conversation, and can exist for the length of the conversation.

- In a UCMA-based communication; only UCMA 5.0 can initiate the contextual channel. UCMA 5.0 does not process new INVITE messages coming from the application.


## Typical conversation context channel scenario

1.  The user interacts with a customer-facing web page with a click-to-chat button.

2.  Based on the Web interaction, the Web Server captures information about both the customer and the context, including customer contact information, current interests on the page, clicked-on links, and cookies.

3.  The Web server interacts by way of an application-to-application back channel to the UCMA 5.0 application (Contact Center), and initiates an Automated Call Dispatch-style interaction, passing along the user’s information. There are two variants:
    
    1.  The Web server Front End serves as a UCMA 5.0/Web gateway to allow the user to IM with the Contact Center, or simply cause the Contact Center to start user interaction.
    
    2.  The Front End calls the user, and presents an interactive voice response (IVR) interface, to gather information from the user. The Front End then transfers the user to the Contact Center.

4.  The UCMA 5.0 Back End reaches out to the Agents (on Skype for Business 2015, using presence-based routing logic and knowledge of agent skills), and asks the agent whether he or she can accept the call.

5.  If the agent accepts:
    
    1.  (Optional) The agent can be added to a conference on the Back End at this time, using the audio-video MCU and a trusted conference user conversation for media control.
    
    2.  The Contact Center provides a special application INVITE to the Skype for Business 2015 Agent at this point, which includes the GUID and Name of an application to be launched, and a raw XML data blob.
        
        The two important requirements from UCMA 5.0 are that there must be one field that is Microsoft-enforced and ‘rigid’, for the application and any URI parameters; and a second field that is a generic data blob, for custom application data.
    
    3.  At this point, the agent should be notified in the application pane in Skype for Business 2015, similar to any modality addition. The application pane should be passed the data from the application INVITE, and should present the agent with a view of any additional context relevant to the customer, including (potentially) CRM, Web interactions, transaction history, and purchasing preferences.

6.  Other agents can be added to or removed from the call as needed by the Backend Contact Center, which will need to be able to go through all the steps outlined in step 5.

7.  The customer is served, and the call closed.

## Using the ConversationContextChannel class

Before a **ConversationContextChannel** instance can be used, it must first be constructed, and then established. After the **ConversationContextChannel** has been established, it can be used to exchange messages.

### Creating a ConversationContextChannel instance

To create a **ConversationContextChannel** instance, call its constructor with the conversation it will be part of and the remote participant endpoint it will communicate with. The conversation is used for two purposes. First, it dictates the lifetime of the channel. As a result, when the last call or conference in the conversation is terminated, the channel is also terminated. Second, by default, the remote endpoint uses the ID of this conversation to choose the conversation window for the application. If the remote application should be launched in a different conversation window, set the value of the [RemoteConversationId](https://msdn.microsoft.com/en-us/library/hh383368\(v=office.16\)) property to the ID of the conversation with which you want it to be associated.

Next, register for events on the channel. The following example creates a **ConversationContextChannel** instance.

```csharp
conversationContextChannel = new ConversationContextChannel(conversation, remoteParticipantEndpoint); 
// Register for changes in state. 
conversationContextChannel.StateChanged += this.conversationContextChannel_StateChanged; 
// Register for data received from the remote endpoint. 
conversationContextChannel.DataReceived += this.conversationContextChannel_DataReceived; 
```

### Establishing a ConversationContextChannel instance

A **ConversationContextChannel** instance can be established only if the conversation that it is tied to is in the **Established** or **Conferenced** state.

The following example shows the steps involved in establishing a **ConversationContextChannel** instance.

```csharp
ConversationContextChannelEstablishOptions establishOptions = new ConversationContextChannelEstablishOptions(); 
// Pass the name for your application. The remote endpoint uses this name to display messages
// about your application. For example if the application is not 
// installed on the remote machine, an error can be shown with the application name. 
establishOptions.ApplicationName = ″Contoso Corporate Performance Management″; 
// Pass a brief message to be shown when the request from the channel arrives at the remote endpoint. 
establishOptions.Toast = ″Good morning! ″; 
// A link to where the application can be installed (used only when the application is not installed). 
establishOptions.ApplicationInstallerPath = ″www.Contoso.com/Download″; 
// Shows a simple URL in the conversation history. 
establishOptions.SimpleLink = ″https://www.microsoft.com/en/us/default.aspx″; 
// Override the conversation ID which will be used to launch the application in the correct conversation window. 
establishOptions.RemoteConversationId = conversation.Id; 
// Pass any data that the application needs for initialization. 
establishOptions.ContextualData = ″My data″; 
conversationContextChannel.BeginEstablish(guid, establishOptions, this.EstablishCompleted, null);
```

[BeginEstablish(Guid, ConversationContextChannelEstablishOptions, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh384061\(v=office.16\)) causes a SIP INVITE message to be sent to the remote participant endpoint. The INVITE message contains a body with content type = "application/ms-session-invite+xml" and appears similar to the following.

```xml
    <session>
      <application-id>{40499119-4B60-45A6-9A7A-DC7A384D5670} </application-id>
      <additional-properties>
        <entry><name>data</name><value>My data</value></entry>
        <entry><name>simpleLink</name></value>https://www.microsoft.com/en/us/default.aspx </value></entry>
        <entry><name>customToast</name><value>Good morning!</value></entry> 
        <entry><name>name</name><value>Contoso Corporate Performance Management</value></entry> 
        <entry><name>installLink</name><value>www.Contoso.com/Download</value></entry> 
      </additional-properties>
    </session>
```


The following limits apply to the data being sent:

  - The contextual data is base-64 encoded before being sent. After encoding it can be at most 4KB in length.

  - The application name can be at most 50 characters.

  - The toast message can have at most 80 characters.

After the remote accepts the INVITE, the channel is established and data can flow. Establishment can fail for the following reasons:

  - The remote endpoint (such as Microsoft Office Communicator 2007 R2 or earlier), does not support the Conversation Extension Window.

  - The Silverlight application is not installed on the remote machine.

  - The remote user rejects the invitation.

  - There are fatal server errors.

The Contact Center sample treats the context channel as optional and hence ignores such failures. Your application can choose this or a different behavior to deal with failures in channel establishment.

### Exchanging messages

After the channel is established it can be used to send or receive data. To send data, call [BeginSendData(ContentType, \[\], AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh383530\(v=office.16\)) with custom content type and body. To receive data, register for notification of the [DataReceived](https://msdn.microsoft.com/en-us/library/hh383082\(v=office.16\)) event, and write a handler for this event.

The following example shows a typical call to **BeginSendData**, and the outline of a handler for the **DataReceived** event.

```csharp
conversationContextChannel.BeginSendData( 
        new System.Net.Mime.ContentType(″application/myApplication+xml″), 
        new ASCIIEncoding().GetBytes(myDataString), 
        this.SendDataCompleted,
        null); 


conversationContextChannel_DataReceived(object sender, ConversationContextChannelDataReceivedEventArgs e) 
{ 
// Use e.ContentDescription.ContentType to determine the type of message. 
// Parse the message body from e.ContentDescription.GetBody(). 
} 
```

### Termination

The **ConversationContextChannel** can be terminated in the following ways after the channel is established:

  - [BeginTerminate(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh383868\(v=office.16\)) is called on the channel.

  - The conversation that the **ConversationContextChannel** is bound to is terminated either explicitly or when all calls or conferences are terminated.

  - The remote endpoint terminates the channel by sending a BYE.

  - A fatal failure is received.

## ConversationContextChannel state transitions

The **ConversationContextChannel** state transitions are shown in the following illustration. The state values are the members of the [ConversationContextChannelState](https://msdn.microsoft.com/en-us/library/hh349729\(v=office.16\)) enumerated type.

![ContextChannel states](images/Dn466012.StateMach_ContextChannel(Office.16).jpg "ContextChannel states")

1.  The transition from **Idle** to **Establishing** occurs when [BeginEstablish(Guid, ConversationContextChannelEstablishOptions, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh384061\(v=office.16\)) is called.

2.  The transition from **Idle** to **Terminating** occurs when **BeginTerminate(AsycnCallback, Object)** is called without the channel being established.

3.  The transition from **Establishing** to **Established** occurs when a channel is successfully established.

4.  The transition from **Establishing** to **Terminating** occurs when channel establishment fails.

5.  The transition from **Established** to **Recovering** occurs when connection to the remote endpoint is temporarily lost.

6.  The transition from **Established** to **Terminating** occurs when **BeginTerminate** is called, the remote endpoint terminates the channel, or the server returns a fatal failure.

7.  The transition from **Recovering** to **Established** occurs when connection to the remote endpoint is restored.

8.  The transition from **Recovering** to **Terminating** occurs when **BeginTerminate** is called or the server returns a fatal error.

9.  The transition from **Terminating** to **Terminated** occurs when the terminate operation is complete.


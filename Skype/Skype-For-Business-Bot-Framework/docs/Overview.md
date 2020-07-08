# Skype for Business Bot Framework (Preview)

> [!NOTE] 
> The Skype for Business Bot Framework channel is in Developer Preview and is not licensed for production usage. This channel will not be supported after 6/30/2019. As part of Microsoft’s intelligent communications vision, we’re building extensible communications capabilities into Teams. 

In a few simple steps you can build and connect intelligent bots to interact with your Skype for Business users using text, cards, or speech.

This article will help you build and enable a Skype for Business bot using the Microsoft Bot Framework. 

<a name="create-bot"></a>

## Create a Skype for Business bot

The Skype for Business bots are created by using the [Microsoft Bot Framework](https://dev.botframework.com/). Bots created with the framework can easily be configured to work with Skype for Business. See the Microsoft Bot Framework Overview to learn how to:

1. Build a bot by using the [C# SDK](https://docs.botframework.com/en-us/csharp/builder/sdkreference/), [Node.js SDK](https://docs.botframework.com/en-us/node/builder/chat-reference/modules/_botbuilder_d_.html), or [REST API](https://docs.botframework.com/en-us/restapi/connector/#navtitle).

2. Test it by using the [Bot Framework Emulator](https://docs.botframework.com/en-us/tools/bot-framework-emulator/).

3. Deploy the bot to a cloud service, such as [Microsoft Azure](https://azure.microsoft.com/).

4. [Register](https://dev.botframework.com/bots/new) the bot with the Microsoft Bot Framework, and select **Add Skype for Business as a channel**.  Enable the channel on the configuration page.

5. Add the bot to a Skype for Business tenant by selecting **Add to Skype for Business**.

6. Copy and paste the following code snippet to embed the **Add to Skype for Business** button to your website:

   <a href='https://skypeappregistration.azurewebsites.net/bot/561bae84-ea1c-4f75-a512-b84566779c2c'><img src='https://dev.botframework.com/ChannelConfigs/Images/skypeforbusiness/Add-To-SkypeForBusiness-Buttons.png'></a> 

   ```html
    <a href='https://skypeappregistration.azurewebsites.net/bot/561bae84-ea1c-4f75-a512-b84566779c2c'><img src='https://dev.botframework.com/ChannelConfigs/Images/skypeforbusiness/Add-To-SkypeForBusiness-Buttons.png'></a>
   ```


<a name="add-bot"></a>

## Add a bot to Skype for Business

Bots can only be added to Skype for Business by tenant administrators of a Skype for Business Online environment. By selecting **Add to Skype for Business**, you launch the **[Connecting your bot to Skype for Business Online](https://skypeappregistration.azurewebsites.net/bot/29415286-5a43-4a00-9dc5-bcbc2ce1f59e)** page, which has the instructions for registering your bot. 

The instructions contain the following information:

To add your bot to Skype for Business, you must be the tenant administrator of a Skype for Business Online environment. A tenant administrator can easily add your bot by using the **[Skype for Business Admin PowerShell cmdlets](https://docs.microsoft.com/en-us/SkypeForBusiness/set-up-your-computer-for-windows-powershell/set-up-your-computer-for-windows-powershell)**. 

### To add a bot

1. [Download and install the Skype for Business Online Connector module](https://go.microsoft.com/fwlink/?LinkId=294688).

2. Open Windows PowerShell as Administrator and run the following:

   ```powershell
    Import-PSSession (New-CsOnlineSession -Credential (Get-Credential))
   ```

   > [!NOTE] 
   > To learn more about PowerShell usage, see [Connecting to Skype for Business Online by using Windows PowerShell](https://docs.microsoft.com/en-us/SkypeForBusiness/set-up-your-computer-for-windows-powershell/set-up-your-computer-for-windows-powershell).

3. You will be prompted with the Windows PowerShell credential dialog box. Sign in by using the tenant admin credentials.

4. Run the following PowerShell cmdlet:

   ```powershell
    New-CsOnlineApplicationEndpoint -ApplicationID <AppID generated from BotFramework Portal like 41ec7d50-ba91-1208-73ee-136b88859725> -Name <NameOfTheBot> -Uri sip:<bothandle@yourdomain.com>
   ```

   > [!NOTE] 
   > For the **Skype for Business hybrid environment**, the **New-CsOnlineApplicationEndpoint** cmdlet will output an additional on-premises cmdlet to be run in your [Skype for Business Server (on-premises) Management Shell](https://technet.microsoft.com/en-us/library/gg398474.aspx). For more information about bot setup for the Skype for Business hybrid environment, see [Skype for Business Bot - Hybrid environment support](Bot-Hybrid-Support.md).
   >
   > Also, see [Skype for Business Bot - Common Errors](Bot-Common-Errors.md) to troubleshoot some of the common bot setup issues. 
 
   This cmdlet registers an instance of your bot with a Skype for Business Online tenant. A Skype for Business Admin can replace the **_Name_** parameter with their desired bot display name, and replace the **_Uri_** parameter with a unique user account from their domain (for example, mybot@contoso.com).

   > [!NOTE] 
   > A newly registered bot can take up to 8 hours to be discoverable by all users in the Skype for Business tenant. For more information about using PowerShell to manage bots and apps for Skype for Business, see [Set up a Trusted Application Endpoint](https://docs.microsoft.com/en-us/skype-sdk/trusted-application-api/docs/trustedapplicationendpoint).

<a name="feature-support"></a>

## Feature support

- **Messages**: Your bot can send plain text, Markdown, and emoticons to a user or group. Users can send rich text and emoticons to your bot. Some Skype for Business clients may not support Markdown or HTML rendering, so bot messages will fall back to text on those clients.

- **Typing indicator**: Your bot can send a typing indicator by using the [C# SDK](https://docs.botframework.com/en-us/csharp/builder/sdkreference/), [Node.js SDK](https://docs.botframework.com/en-us/node/builder/chat-reference/modules/_botbuilder_d_.html), or [REST API](https://docs.botframework.com/en-us/restapi/connector/#navtitle) to tell the user that the bot is preparing to respond, for example, when processing a request.

- **Basic format**: The Skype for Business ID of a user is available for your bot, and is sent along with the _**display name**_ with every message.

   ```http 
    {
      "type": "message",
      "timestamp": "2017-05-02T18:22:05.4521432Z",
      "serviceUrl": "https://webpoolbl20r02.infra.lync.com/platformservice/tgt-b5eccd26b71c5653a9f3e8faecd568db/botframework",
      "channelId": "skypeforbusiness",
      "from": {
        "id": "sip:user@contoso.com",
        "name": "Contoso User"
      },
      "conversation": {    "isGroup": true,
        "id": "ZmMyYmY4OGMjc2lwOmppZWluY29taW5nYm90ZXA4QDBtY29ycDJjbG91ZHBlcmYub25taWNyb3NvZnQuY2t"
      },
      "recipient": {
        "id": "sip:bot@contoso.com",
        "name": "sip:bot@contoso.com"
      },
      "text": "Hello World",
      "attachments": [],
      "entities": [],
      "replyToId": "2"
    }
    }
   ```

   The _**from**_ field contains the unique user Skype for Business identity _**Sip Uri**_ and _**user display name**_.

   The _**to**_ field contains the App Skype for Business identity _**Sip Uri**_ and _**bot display Name**_.

- **Skype for Business emoticons**: Skype for Business emoticons can be sent by using an emoticon keyword.

   ```http
    {
      "text": ":)"
    }
   ```

- **Cards and buttons**: Skype for Business does not support rendering cards and buttons. The cards and buttons sent by a bot will fall back to text in the Skype for Business clients.

- **Groups**: A bot can be enabled for groups in outgoing bot scenarios. A bot can message multiple users in a group conversation. Skype for Business does not support adding an additional participant to a 1:1 bot conversation. 

<a name="create-outbound-bots"></a>

## Create outbound bots

Skype for Business bots can initiate outbound messages to users proactively (as long as the user is in a user-initiated conversation with the bot and still has the Skype conversation window open) because they are enabled by a tenant administrator. To initiate an outbound message, your bot must be Skype for Business tenant-specific (your code must be written for a bot that will only be deployed in a single tenant, for example, bot@contoso.com). 

In addition, to initiate an outbound message, your bot must also have the Skype for Business identity of the user or users that the bot will be messaging directly, for example, user@contoso.com.

Your bot also needs to add the Skype for Business Channel entry point URL (`https://api.skypeforbusiness.com/platformservice/botframework`) to the Trusted Service URLs.

Following is a code snippet that illustrates how to initiate an outbound message to a Skype for Business user by using the C# Bot Builder SDK.

```cs
MicrosoftAppCredentials.TrustServiceUrl("https://api.skypeforbusiness.com/platformservice/botframework");     
to = new string[] {"sip:usera@contoso.com", "sip:userb@contoso.com"};
public async Task MakeOutboundCall(string[] to, string topic)
        {
            var connector = new ConnectorClient(new Uri(https://api.skypeforbusiness.com/ platformservice/botframework/)); //initial entry point to talk to the Skype for Business channel
            List<ChannelAccount> participants = new List<ChannelAccount>();
            foreach (string t in to)
            {
                participants.Add(new ChannelAccount(t, t + "name"));
            }
            ConversationParameters cpMessage = new ConversationParameters(true, new ChannelAccount(sip:bot@contoso.com, "DisplayNameofBot"),  participants, topic);
            ConversationResourceResponse  response = await connector.Conversations.CreateConversationAsync(cpMessage);
            ConversationId = response.Id;
            this.UpdateServiceUrl(response.ServiceUrl); //remember response ServiceURL to send additional message requests to the same conversation
        }
```

<a name="version-suport"></a>

## Supported Skype for Business versions

The Skype for Business Bot Framework channel is currently supported for **Skype for Business Online** and **Skype for Business Hybrid** environments only. 

> [!NOTE] 
> Skype for Business Server 2015 is currently not a supported [Microsoft Bot Framework](https://dev.botframework.com/) channel.

<a name="hybrid-suport"></a>

### Skype for Business hybrid environment support

Skype for Business bots can be connected to Skype for Business Server users if hybrid connectivity has been deployed in the environment. For more information about hybrid connectivity and bot setup, see [Skype for Business Bot - Hybrid environment support](Bot-Hybrid-Support.md).

<a name="remove-bot-dev"></a>

## Remove a bot - developer
 
To delete your bot completely, go to your bot dashboard, select edit the Skype for Business channel and click the Delete button at the bottom.

<a name="remove-bot-tenant"></a>

## Remove a bot – Skype for Business tenant administrator

To delete a bot completely from a Skype for Business tenant, you must be the tenant administrator of a Skype for Business Online environment. A tenant administrator can easily delete your bot by using the Skype for Business Admin **[PowerShell cmdlets](https://docs.microsoft.com/en-us/SkypeForBusiness/set-up-your-computer-for-windows-powershell/set-up-your-computer-for-windows-powershell)**. 

### To delete a bot

1. [Download and install the Skype for Business Online Connector module](https://go.microsoft.com/fwlink/?LinkId=294688).

2. Open Windows PowerShell as Administrator and run the following:

   ```powershell
      Import-PSSession (New-CsOnlineSession -Credential (Get-Credential))
   ```

3. You will be prompted with a Windows PowerShell credential dialog box. Sign in by using the tenant admin credentials.

4. Run the following PowerShell cmdlet: 

   ```powershell
    Remove-CsOnlineApplicationEndpoint -Uri sip:username@yourdomain.com
   ```

This command removes the instance of a bot with a Skype for Business Online tenant. A Skype for Business Admin must replace the Uri parameter with the user account from their domain (for example, @contoso.com), which they created when adding the bot. 

<a name="common-errors"></a>

## Common errors

For a list of common errors encountered during the Skype for Business bot setup and what you can do to solve them, see [Skype for Business Bot - Common Errors](Bot-Common-Errors.md).



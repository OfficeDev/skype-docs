---
title: Creating a Skype for Business bot
TOCTitle: Creating a Skype for Business bot
ms:assetid: bede739a-ec48-4e6d-b52c-acbf04b245bb
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454840(v=office.16)
ms:contentKeyID: 65240111
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- powershell
- xml
- csharp
---

# Creating a Skype for Business bot



To create a bot that runs in Skype for Business, complete the steps in the following procedure.

### To create a bot that runs in Skype for Business

1.  Create a trusted application and a trusted application endpoint for the UC bot

2.  [Create message handlers](creating-a-generic-bot.md)

3.  Instantiate the UCBotHost environment

4.  (Optional) [Handle feedback events](creating-a-generic-bot.md)

5.  [Test your bot](creating-a-generic-bot.md)

6.  Deploy the UC bot using a Windows Service


> [!NOTE]
> <P>Microsoft Unified Communications Managed API 5.0 must be installed on the computer that will host the bot.</P>



Some steps of this procedure are described in [Creating a generic bot](creating-a-generic-bot.md). The other steps are described in this topic.

## Create a trusted application and a trusted application endpoint for the UC bot

In typical production scenarios, your bot will run on an [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) instance. You will need to create a trusted application and a trusted application endpoint in the Skype for Business environment.

If your Skype for Business Server is not set up for provisioning UCMA 5.0 applications, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

To create a trusted application and trusted application endpoint, use the following PowerShell script.

```powershell
# Create new trusted application and application end point 
Write-Host
Write-Host "This will create a trusted application and an application endpoint"
Write-Host "For Trusted application"
Write-Host
$ApplicationId= Read-Host "Enter the ApplicationId(Ex: urn:application:samplebot)"
$ApplicationFqdn= Read-Host "Enter the application Fqdn(Ex: ts.fabrikam.com)"
$PortNo = Read-Host "Enter the port number (Ex. 10605)"

# Get User Information
$UserInfo = Get-CsUser $User

# Create Trusted Application
New-CSTrustedApplication –ApplicationId $ApplicationId -TrustedApplicationPoolFqdn $ApplicationFqdn -Port $PortNo

#Enable the topology
Enable-CSTopology

Write-Host "For application endpoint"
$ApplicationSipAddress= Read-Host "Enter the sip address for application end point (Ex: sip:samplebot@fabrikam.com)"
$DisplayName= Read-Host "Enter the display name for application end point (Ex: Sample Bot)"

New-CSTrustedApplicationEndpoint –ApplicationId $ApplicationId -TrustedApplicationPoolFqdn  $ApplicationFqdn -SipAddress $ApplicationSipAddress -DisplayName 

$DisplayName
Write-Host
$ApplicationEndPoint=get-CSTrustedApplicationEndpoint $ApplicationSipAddress
# Final Words
Write-Host
Write-Host "End point created: -"   $ApplicationEndPoint.DisplayName
Write-Host
```

In Skype for Business Management Shell, run the following script.

```powershell
# Create Trusted Application
New-CSTrustedApplication –ApplicationId $ApplicationId -TrustedApplicationPoolFqdn $ApplicationFqdn -Port $PortNo
#Enable the topology
Enable-CSTopology
New-CSTrustedApplicationEndpoint –ApplicationId $ApplicationId -TrustedApplicationPoolFqdn  $ApplicationFqdn -SipAddress $ApplicationSipAddress -DisplayName
```

Enter the information that you are prompted for by this script.

If a trusted application was successfully created, you will see a message as "End point created: - \<Display Name\>".

## Instantiate the UCBotHost environment

When you develop a generic bot (see [Creating a generic bot](creating-a-generic-bot.md)), you need to instantiate and configure the *Bot* class. For a Skype for Business bot, you need to instantiate and configure the *BuildABot.UC.UCBotHost* environment. It is highly recommended that you do so in a console application first, for test purposes. Then you will be ready to deploy the UC bot using a Windows Service.

### To instantiate the UCBotHost environment

1.  Create a Console Application test project.

2.  Add references to the following Build a Bot assemblies:
    
      - BuildABot.Core
    
      - BuildABot.UC
    
      - BuildABot.Util

3.  Create a configuration (config) file as shown here and use it to get the application urn and application user agent. Notice that the config file has a *startup* element that enables your UCMA 5.0 application to consume the UCMA 5.0 SDK.
    
    ```xml
    <?xml version="1.0"?>
    <configuration>
      <startup useLegacyV2RuntimeActivationPolicy="true">
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
      </startup>
      <runtime>
        <generatePublisherEvidence enabled="false"/>
      </runtime>
      <appSettings>
        <add key="applicationurn" value="urn:application:samplebot/>
        <add key="applicationuseragent" value="samplebot/>
      </appSettings>
    </configuration>
    ```

4.  Create a main method like the following one in your Main class to instantiate a *UCBotHost* object.
    
    ```csharp
    static void Main(string[] args)
     {
       // Print all Debug.WriteLine calls to the console to make it
       // easier to determine the cause of any errors.
       Debug.Listeners.Add(new ConsoleTraceListener());
     
       String applicationUserAgent = ConfigurationManager.AppSettings["applicationuseragent"];
       String applicationurn = ConfigurationManager.AppSettings["applicationurn"];
       UCBotHost ucBotHost = new UCBotHost(applicationUserAgent, applicationurn);
       ucBotHost.Run();
    }
    ```
    
    Note the parameters of the *UCBotHost* constructor:
    
      - *applicationUserAgent*: The part of the user agent string that identifies the application urn of created trusted application.
    
      - *applicationURN*: The unique identifier for the application in the deployment. It is assign when the application is provisioned.
    
      - Help text (optional): The help text to be displayed when the bot does not understand the user message.
    
    The UC bot host automatically handles the bot's *Replied* and *FailedToUnderstand* events, sending reply messages to the user through the Skype for Business window. Optionally, you can handle some additional events, such as the following.
    
      - *MessageReceived*—This event is raised when a message is received by the UC bot host. This event can be useful for logging purposes.
    
      - *ErrorOccurred*—This event is raised when the UC bot host throws an error. Exceptions thrown by the bot's message handlers will raise this event. This can also happen when the bot is initializing and before it shuts down.
    
      - *FeedbackEngine.FeedbackCollected*— This event is raised when feedback is collected from the user. For more information, see [Handle feedback events](creating-a-generic-bot.md).

5.  Run your console application and verify that the bot user account appears as online in Skype for Business. Do not discard any code you have developed so far. You will use the contents of the Main method you developed above to deploy the UC bot using a Windows service.

## Deploy the UC bot using a Windows Service

You will probably want to run your bot as a Windows service in the background, instead of as a console application. When your test console application shows that everything seems to be working correctly (for more information, see [Instantiate the UCBotHost environment](creating-a-skype-for-business-bot.md)), you can now move to a Windows Service implementation.

### To deploy your application as a Windows service

1.  Add a new Windows Service project.

2.  Add references to the following assemblies:
    
      - BuildABot.Core
    
      - BuildABot.UC
    
      - BuildABot.Util
    
      - The assemblies that contain your message handlers
    
      - Any other assemblies that your message handlers depend on

3.  Add to your Windows Service project the same config file you added in the [Instantiate the UCBotHost environment](creating-a-skype-for-business-bot.md) section.

4.  In the "service launcher" class of the Windows Service project, create a method containing the *UCBotHost* initialization. This is very similar to the code that was shown in the "Instantiate the UCBotHost environment" section.
    
    ```csharp
    private void StartBotListenerService(object state)
    {
       String applicationUserAgent = ConfigurationManager.AppSettings["applicationuseragent"];
       String applicationUrn = ConfigurationManager.AppSettings["applicationurn"];
    
       UCBotHost ucBotHost = new UCBotHost(applicationUserAgent, applicationUrn),
       ucBotHost.Run();
    }
    ```

5.  In the "service launcher" class of the Windows Service project, add a *System.Threading.Timer* object that will launch the *StartBotListenerService* method and initialize the timer in the *OnStart* method, as shown in the following sample.
    
    ```csharp
    private Timer botTimer;
    protected override void OnStart(string[] args)
    {
       botTimer = new Timer(StartBotListenerService, null, 0, Timeout.Infinite);
    }
    ```

6.  Double-click the service launcher file in Visual Studio's **Solution Explorer**. Right-click the gray pane, and then select **Add Installer**.

7.  Double-click ProjectInstaller.cs in Visual Studio's **Solution Explorer**, and then select the service installer object. In the Visual Studio **Properties** window (press F4 to see it), set the properties that will determine how this service will appear in the Windows Services snap-in, such as the service name, description, and so on.

8.  In the server computer that will host the bot, run the InstallUtil.exe tool (typically located at c:\\Windows\\Microsoft.NET\\Framework\\v4.5.xxxxx\\) to install the Windows service, passing the compiled Windows service executable as a parameter. This should be done from an elevated command prompt. You might be requested for credentials (domain\\user and password) under which the bot Windows service should run.

9.  Open the Windows Services snap-in (run "services.msc"). The bot Windows Service should be there. Start the service and your bot should wake up.


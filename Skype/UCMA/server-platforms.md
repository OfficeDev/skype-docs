---
title: Server platforms
TOCTitle: Server platforms
ms:assetid: 078a6a3f-9610-4a70-8cf7-2fc7f14977ca
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466051(v=office.16)
ms:contentKeyID: 65239980
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Server platforms

**Applies to**: Skype for Business 2015

When a [CollaborationPlatform](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform?view=ucma-api) instance is created, it can be created as a client platform, a server platform, or as a provisioned application server platform. Each of the three **CollaborationPlatform** constructors can be used to create one type of platform.

A server platform enables the development of robust applications that server a large number of clients. This type of platform can support large numbers of both endpoint types, [UserEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.userendpoint?view=ucma-api) and [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api). A server platform is usually configured to run as a trusted application, enabling it to perform operations not permitted to a regular user, such as impersonating another user. Server platforms can be manually provisioned by an application, or automatically provisioned by the UCMA platform.

## Server platform characteristics

A server platform has the following characteristics:

- A server platform listens for incoming connections.

- An endpoint that is tied to a server platform is not limited to a single connection for all traffic. It can use different connections for sending or receiving different messages to the same server.

- Any connection on a server platform can be shared among different endpoints and operations.

- The number of outstanding messages is not throttled on a server platform, so it can handle larger traffic volumes.

Trusted applications are configured with a variety of settings that are stored primarily in Active Directory and the Central Management Store. Both the application and Microsoft Unified Communications Managed API 5.0 must be provisioned at run time with these settings. When these settings change, the application must update itself accordingly. Microsoft Unified Communications Managed API 5.0 enables trusted applications to manually provision UCMA by explicitly initializing it with configuration data that can be obtained through Windows PowerShell cmdlets that are run offline. However, the recommended and far simpler approach is to ask UCMA to automatically provision itself. 

For more information, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

## Types of provisioning data

The provisioning data for trusted applications can be divided into two broad categories:

- Trusted application configuration data
    
  Configuration data that is needed by the trusted application.

- Endpoint configuration data
    
  Configuration data that is needed for each endpoint used by the application.

In UCMA 5.0 the [CollaborationPlatform](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform?view=ucma-api) class provides services such as connection management and message dispatching to the endpoints. A **CollaborationPlatform** instance must be configured with a trusted application configuration. [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) instances also must be configured with individual trusted application endpoint configurations.

## Manual provisioning

To manually provision a trusted application, one should first create a **CollaborationPlatform** instance using an initialized [ServerPlatformSettings](https://msdn.microsoft.com/en-us/library/hh382156\(v=office.16\)) instance. The **ServerPlatformSettings** instance must be supplied with all of the trusted application configuration data-local FQDN, listening port, application GRUU, and certificate. Each **ApplicationEndpoint** instance must also be provisioned with a manually populated [ApplicationEndpointSettings](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpointsettings?view=ucma-api) instance for which the [OwnerUri](https://msdn.microsoft.com/en-us/library/hh161899\(v=office.16\)) and [OwnerPhoneUri](https://msdn.microsoft.com/en-us/library/hh385129\(v=office.16\)) properties are appropriately initialized. 

If any of the settings changes (such as the certificate), it is the responsibility of the application to discover such changes by offline means and to apply the new values to the **CollaborationPlatform** instance or **ApplicationEndpoint** instances, depending on which setting changed.

## Auto-provisioning

An auto-provisioned **CollaborationPlatform** instance automatically discovers **ApplicationEndpoint** settings that were configured for the application, and invokes the handler passed to the **[RegisterForApplicationEndpointSettings](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform.registerforapplicationendpointsettings?view=ucma-api) (EventHandler\<ApplicationEndpointSettingsDiscoveredEventArgs\>)** method. This provides the application an opportunity to automatically create **ApplicationEndpoint** instances for each previously created contact object that is associated with the application ID (by the use of the New-CsTrustedApplicationEndpoint PowerShell cmdlet).

All of the configuration data for a trusted application is associated with a single application ID. Instead of provisioning UCMA with each individual item of provisioning data, the application can instead do so using just the application ID. UCMA then queries the relevant configuration stores to fetch both the trusted application configuration data and the endpoint configuration data for each endpoint.

UCMA also keeps abreast of changes to the configuration data. For example, if the certificate that is specified for the computer changes, UCMA automatically detects and applies this change. If a new AD contact is created, UCMA automatically alerts the application about this change.

## Auto-provisioning details

The following list shows the steps that are required to use auto-provisioning in a UCMA-based application.

### To auto-provision an application

1. Create and auto-provision the **CollaborationPlatform** instance.
    
   The following example creates an auto-provisioned **CollaborationPlatform** instance.
    
   To create an auto-provisioned application, a **CollaborationPlatform** instance must be created by using the constructor that takes a [ProvisionedApplicationPlatformSettings](https://msdn.microsoft.com/en-us/library/hh385058\(v=office.16\)) argument. Platform startup will succeed if provisioning data for the application is successfully fetched.
    
   ```csharp
    ProvisionedApplicationPlatformSettings platformSettings = new ProvisionedApplicationPlatformSettings (″myUserAgent″, ″myApplicationId″);
    // Populate other properties on platformSettings.
    platform = new CollaborationPlatform(platformSettings);
    
    // Register a handler to be alerted of each endpoint’s configuration as it is discovered.
    platform.RegisterForApplicationEndpointSettings(ApplicationEndpointDiscoveredHandler);
    
    // Register a handler to be alerted of non-fatal failures after initial startup has succeeded.
    platform.ProvisioningFailed += ProvisioningFailedHandler;
    platform.BeginStartup(PlatformStartupCallback, null);
   ```

2. Create **ApplicationEndpoint** instances.
    
   The handler passed to [RegisterForApplicationEndpointSettings](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform.registerforapplicationendpointsettings?view=ucma-api) method on the **CollaborationPlatform** instance will receive configuration data for all contact objects for which endpoints have not been created when the handler is registered. The application can register a single handler and create each endpoint with its own endpoint settings when the handler is called.
    
   Applications that have certain endpoints that must be functioning before the remaining endpoints can be created and established will require a different approach. Such an application might require one or more endpoints that are used for the application’s logic rather than for directly servicing calls to the application. 
   
   For example, in a contact center application, one endpoint might subscribe to the presence of agents while other endpoints represent individual response groups. 
   
   To simplify application logic in this case, an application can register multiple handlers. One handler can process the [ApplicationEndpointSettings](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpointsettings?view=ucma-api) instance that corresponds to the presence watcher endpoint, while the other handler can process the **ApplicationEndpointSettings** instances that correspond to the individual response groups. The application can register just the first handler; after the presence watcher endpoint is up and running, it can register the second handler, because the application is now ready to route calls to agents. 
   
   The *PresenceWatcherEndpointSettingsHandler* method shown in the following example should be registered in the calling code before the other handler is registered.
    
   ```csharp
    private void PresenceWatcherEndpointSettingsHandler(object sender, ApplicationEndpointSettingsDiscoveredEventArgs e)
    {
      ApplicationEndpointSettings endpointSettings = e.ApplicationEndpointSettings;
      if(CheckIfPresenceWatcherUri(endpointSettings.OwnerUri))
      {
        ConfigurePresenceWatcherEndpointSettings(endpointSettings);
        presenceWatcherEndpoint = new ApplicationEndpoint(platform, endpointSettings);
        // Register for events on endpoint.
        .
        .
        .
        // Establish the endpoint and subscribe to presence of agents.
        .
        .
        .
      }
    }
   ```
    
   > [!IMPORTANT]
   > - In auto-provisioning, **ApplicationEndpoint** creation using manual settings is not permitted until the platform is established.
   > - An application should not create a mix of auto-provisioned and manually-provisioned endpoints.

   When the endpoint is ready to create and service endpoints, it can register the other handler which would only create endpoints for response groups. The application normally bases its check for the endpoint type on the endpoint’s URI, which should be created using a predetermined rule. 
   
   For example, the setup scripts for the Contact Center sample, which can be found in %Program Files%\\Microsoft UCMA 4.0\\SDK\\Core\\Sample Applications\\Reference\\ContactCenter\\, can ensure that the URI of the presence watcher endpoint has a prefix of "PresenceWatcher", thereby enabling a simple implementation of the *CheckIfPresenceWatcherUri* method shown in the preceding example.

3. Check for changes to endpoints.
    
   The latest provisioning data for an endpoint can be retrieved by a call to the [BeginGetProvisioningData(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh384928\(v=office.16\)) method on the **ApplicationEndpoint** instance. This method passes the refreshed [ProvisioningData](https://msdn.microsoft.com/en-us/library/hh385025\(v=office.16\)) object into the callback delegate. The **ProvisioningData** class exposes other data as well as the configuration data in the contact object. Endpoint configuration from the contact object can be accessed through the [EndpointConfiguration](https://msdn.microsoft.com/en-us/library/hh382149\(v=office.16\)) property on the **ProvisioningData** instance. If the endpoint configuration changes, this property is automatically updated. If the application is intended to keep abreast of the changes to configuration data, it should register for notification of the [OwnerPropertiesChanged](https://msdn.microsoft.com/en-us/library/hh382763\(v=office.16\)) event.

4. Terminate an endpoint.
    
   An application can terminate an endpoint at any time by calling the [BeginTerminate(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh348966\(v=office.16\)) method on the **ApplicationEndpoint** instance. UCMA can also automatically terminate an **ApplicationEndpoint** instance if the contact object for the endpoint is disabled or deleted from Active Directory.

5. Handle errors.
    
   An auto-provisioned **CollaborationPlatform** instance cannot be started successfully unless the provisioning data for the application is successfully fetched. If the application is not configured correctly, or a connection to Active Directory cannot be made, or Central Management Store is not running, **CollaborationPlatform** startup will fail, causing a [ProvisioningFailureException](https://msdn.microsoft.com/en-us/library/hh385160\(v=office.16\)) exception to be thrown. The application should alert the administrator in such a case to take corrective action.
    
   After the **CollaborationPlatform** instance is started, it is resilient to failures in accessing provisioning data or faulty configuration. For example, if a new certificate is incorrectly configured for the machine while it is running or if Active Directory connectivity is lost, UCMA continues using the existing settings, but raises the [ProvisioningFailed](https://msdn.microsoft.com/en-us/library/hh365940\(v=office.16\)) event on the **CollaborationPlatform** instance. The application must alert the administrator of this failure so that prompt action can be taken to configure a certificate correctly or restore Active Directory connectivity.


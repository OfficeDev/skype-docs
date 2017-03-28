---
title: Configuring SDN Interface Dialog Listener
ms.prod: SKYPE
ms.assetid: e66f9787-ab6b-4a77-8895-0ae39a3a5ee1
---


# Configuring SDN Interface Dialog Listener

 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015
 
In general, the Skype for Business Dialog Listener component obtains its configuration settings using two mechanisms: 
  
    
    


- From the DialogListener.exe.config file. These settings have the primary responsibility for locating the applicable configuration service. 
    
  
- From configuration settings provisioned by the configuration service. These settings how the Dialog Listener receives, processes and forwards data from the Skype for Business Server front end as well as how the requests are processed by the SDN Manager. 
    
  
This article explains how to configure Skype for Business Dialog Listener by editing the DialogListener.exe.config file. The configuration file is located in the Dialog Listener installation directory, whose default directory location is  `C:\\Program Files\\Microsoft Skype for Business Server\\Microsoft Skype for Business DialogListener\\`. 
## Editing the DialogListener.exe.config file

You can modify some of the configuration options for the Dialog Listener by directly editing the DialogListener.exe.config file. These options allow the Dialog Listener to locate the appropriate configuration service so it can retrieve additional configuration settings. The configurations service is part of the SDN Manager. 
  
    
    
These additional settings that you need to set re execution options, which cannot be configured using the SDN configuration service, but must be modified manually in the configuration file for each Dialog Listener instance. 
  
    
    

### To manually edit Dialog Listener execution options


1. Navigate to the Dialog Listener installation directory, which is located by default at  `C:\\Program Files\\Microsoft Skype for Business Server\\Microsoft Skype for Business DialogListener\\`
    
  
2. Open the DialogListener.exe.config file with a text editor of choice. 
    
  
3. Locate the  `<appSettings>` section and edit the relevant entries as appropriate.
    
  
The following example shows an excerpt of the Dialog Listener configuration file that contains the Dialog Listener execution options: 
  
    
    



```XML

<appSettings>
       <add key="configurationserviceuri" value="http://localhost:9333/Settings" />
       <add key="configurationcertificate" value="" /> <!-- thumbprint of a client certificate to use to authenticate the DL with the SM -->
       <add key="configurationrefresh" value="00:01:00" /> <!-- Period for refreshing the settings from the configuration service -->
       <add key="checkdns" value="false" />   <!-- use a URI provided by the DNS SRV record for locating the configuration service -->
       <add key="msplidentifier" value="SDN22" />
  </appSettings>

```

In the XML code example, the **configurationserviceuri** key value specifies the URI to the appropriate configuration service. For HTTP, the default port number is 9333; for HTTPS the default port number is 9332.
  
    
    
The **configurationrefresh** key value specifies the time span between which the Dialog Listener checks for updates to its settings. In the example, this value is set to one minute (00.01:00).
  
    
    
The **configurationcertificate** key value contains the thumbprint of an installed client certificate for authenticating the current Dialog Listener to the SDN Manager if HTTPS is used and mutual authentication is required.
  
    
    
The **checkdns** key value is used to override the **configurationserviceuri** key. If it is set to **true**, the Dialog Listener is forced to use the URI defined in the DNS SRV record to locate the SDN Manager. For more information, see  [Setting up a DNS service location record](setting-up-a-dns-service-location-record.md). 
  
    
    
The **msplidentifier** key value allows you to change the identifier that is used to register with the Skype for Business Server.
  
    
    

## Additional resources
<a name="bk_addresources"> </a>


-  [Configuring Skype for Business SDN Interface](configuring-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  
-  [Setting up a DNS service location record](setting-up-a-dns-service-location-record.md)
    
  


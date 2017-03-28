---
title: Getting ready to install Skype for Business SDN Interface
ms.prod: SKYPE
ms.assetid: c5b5083a-a25e-4409-a496-2616bb2b15a2
---


# Getting ready to install Skype for Business SDN Interface

 **Last modified:** February 27, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015 

To ensure that the Software Defined Network (SDN) Interface components are successfully installed and started, do the following before you run the installation packages: 
  
    
    


1. Set up a DNS record to define a round-robin FQDN for the SDN Manager pool. 
    
  
2. Set up an optional DNS SRV record to support the Dialog Listeners (as well as subscribers) to locate the configuration service. (see below) 
    
  
3. Close the **Service Control Panel** to prevent the Windows service installation from hanging.
    
  
4. Ensure that the Skype for Business Server front ends and the SDN Manager servers have reasonably synchronized clocks. This is especially important for the SDN Manager processing timeouts, which rely on time stamps. Note, too, that significantly divergent times will cause unexpected behavior as well as uneven load balancing. 
    
  
5. If you choose, install all certificates necessary for mutual TLS. Create and place the same client certificate on every Dialog Listener, and obtain client certificates expected and authorized by each the network controller on all SDN Managers. 
    
  
6. Set up a Redis cache to enable using Redis as your data store (see below). 
    
  
7. Set up SQL Server for using a database as your data store (see below). 
    
  
8. To install Dialog Listener, first run the SkypeForBusinessDialogListener.msi on a Skype for Business Server front end using an administrator account that is also a part of the  *RTC Server Applications*  local group. To add the administrator account to the *RTC Server Applications*  group, follow these steps:
    
   1. Log on to the Skype for Business Server front end server computer as the administrator and start the server manager snap-in. 
    
  
   2. Add the administrator account to the  *RTC Server Applications*  local group.
    
  

    When you run Steps 8.1 and 8.2, log off and log on again for the changes to take effect. The Dialog Listener service account must be a member of the RTC Local Server Group. 
    
  

> [!NOTE]
> You should install SDN Manager on a separate application server to maximize the performance of the Skype for Business Server front end. 
  
    
    


## In this section


-  [Activating QoE recording in Skype for Business Server](activating-qoe-recording.md)
    
  
-  [Setting up a DNS service location record](setting-up-a-dns-service-location-record.md)
    
  
-  [Setting up SQL Server for a SDN manager database](setting-up-sql-server-for-a-sdn-manager-db.md)
    
  
-  [Setting up a Redis cache system](setting-up-a-redis-cache-system.md)
    
  
-  [Installing security certificates](installing-security-certificates.md)
    
  

## Additional resources


-  [Installing Skype for Business SDN Interface](installing-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  

  
    
    


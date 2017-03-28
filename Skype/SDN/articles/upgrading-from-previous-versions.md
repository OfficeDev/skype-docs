---
title: Upgrading from previous version of the SDN Interface
ms.prod: SKYPE
ms.assetid: 8d3c28fa-dabe-4a52-9882-a6663ced5217
---


# Upgrading from previous version of the SDN Interface

 **Last modified:** February 24, 2017
  
    
    

**Applies to:** Lync Server 2013 | Skype for Business 2015

This topic covers upgrading Skype for Business SDN Interface from earlier versions to version 3.0. 
  
    
    


## Upgrading to version 3.0 from version 2.4 or newer

When upgrading from version 2.4 to 3.0, follow the instructions below without dropping the database: 
  
    
    

1. For each SDN Manager instance of each SDN Manager pool: 
    
  - Uninstall the 2.4 SdnManager. 
    
  
  - Install the 3.0 SdnManager. 
    
  
  - While upgrade is being performed on an SDN Manager in a pool, the other SDN Managers in the pool may continue to run on the previous version. 
    
  
  - Once upgraded, an SDN Manager can be started up while other SDN Managers continue to run on the older version, as the pool is upgraded one server at a time. 
    
  
2. For each Dialog Listener instance: 
    
  - Uninstall the 2.4 Dialog Listener. 
    
  
  - Install the 3.0 Dialog Listener. 
    
  
  - The SDN Managers can continue to receive streams from the 2.4 Dialog Listeners while the upgrade is in progress. 
    
  

## Upgrading to version 3.0 from version 2.2

When upgrading from version 2.2 to 3.0, follow the instructions below without dropping the database: 
  
    
    

1. Prior to upgrade, capture existing Listener/Subscriber configurations for post-upgrade re-application: 
    
  - Invoke `SdnManager /d s <Identifier> Subscriber.<Identifier>.xml` to capture each individual Subscriber configuration. 
    
  
  - Invoke `SdnManager /d l <Identifier> Listener.<Identifier>.xml` to capture each individual Listener pool configuration. 
    
  
  - The files created will have the settings/format compatible for 3.0 uploading, post-upgrade. 
    
  
2. For each SdnManager instance of each SdnManager pool: 
    
  - Uninstall the 2.2 SdnManager. 
    
  
  - Install the 3.0 SdnManager. 
    
  
  - While upgrade is being performed on an SdnManager in a pool, the other SdnManagers in the pool may continue to run on the previous version. 
    
  
  - Once upgraded, an SdnManager can be started up while other SdnManagers continue to run on the older version, as the pool is upgraded one server at a time. 
    
  
3. Remove and re-add each Subscriber (For each 'Subscriber.<Identifier>.xml' file): 
    
  - Invoke `SdnManager /r s <Identifier>` to remove the individual 2.2 Subscriber. 
    
  
  - Invoke `SdnManager /u s <Identifier> Subscriber.<Identifier>.xml` to re-add an individual 3.0 Subscriber configuration. 
    
  
4. For each DialogListener instance: 
    
  - Uninstall the 2.2 DialogListener. 
    
  
  - Install the 3.0 DialogListener. 
    
  
  - The SdnManagers can continue to receive streams from the 2.2 DialogListeners while the upgrade is in progress. 
    
  
5. Remove and re-add each Listener pool (For each `Listener.<Identifier>.xml` file): 
    
  - Invoke `SdnManager /r l <Identifier>` to remove the individual 2.2 Listener pool. 
    
  
  - Within 10 seconds, repeat the invoking of `SdnManager /r l <Identifier>` to remove the individual 2.2 Listener pool. 
    
  
  - The second time will probably return an ignorable error ("Identifier not Found"), but since Listener pool registration is automatic, it is probable that automatic registration has occurred. 
    
  
  - Invoke `SdnManager /u l <Identifier> Listener.<Identifier>.xml` to re-add an individual 3.0 Listener pool configuration. 
    
  
  - This last step is only necessary if version 2.2 settings were changed from the default. A new Listener pool will be automatically created with the default settings. 
    
  

> [!NOTE]
> The removal and re-adding of the subscribers and listeners is only needed if your deployment has an Redis database. It is not required for deployments using an SQL database. <br/> If the deployment consists of a single SdnManager then upgrade process will results in loss of events from the Dialog Listener during the time of the upgrade. 
<br/> Additionally, there may be loss of events (signaling or quality update events not reported to subscribers) during the time that it takes to remove and add the subscribers after the SdnManager uninstall/install. 
<br/>During the Dialog Listener uninstall/reinstall only events originating from the other Listeners will be reported. 
<br/>Similarly, there may be loss of events during the time that it takes to remove and add the Listener settings. 
  
    
    


## Upgrading to version 3.0 from version 2.1.1 or prior

When moving from version 2.1.1 or prior to 3.0 you must uninstall and reinstall all components including the database. 
  
    
    

## Additional resources


-  [Installing the SDN Manager](installing-the-sdn-manager.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  


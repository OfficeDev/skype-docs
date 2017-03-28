---
title: Configuring SDN Interface using the command prompt
ms.prod: SKYPE
ms.assetid: b411b6d6-4597-416f-8a68-292527d3f226
---


# Configuring SDN Interface using the command prompt

 **Last modified:** February 24, 2017
  
    
    

 **Applies to:** Lync Server 2013 | Skype for Business 2015

Most settings used by the Dialog Listener and SDN Manager are kept in the central data store (local cache, SQL Server database, or Redis cache) that is managed by the configuration service. These settings can be accessed and modified through a command line tool (SDNManager.exe). SDNManager.exe can also get and set limits for quality metrics of call streams from the QoEMetrics database. 
  
    
    


> [!NOTE]
> The command line tool must connect to running Configuration Service/SDN Manager (or SDN Manager pool). Using a local copy of the SDNManager.exe allows you to connect to any configuration service, but the default instance is the one used by the installation itself. 
  
    
    

Configuration settings are divided into three groups: 
- **Manager**. (m)anager settings are used by the SDN Manager component.
    
  
- **Listener**. (l)istener settings are used by the Dialog Listener component installed on a specified pool of Skype for Business Server front end machines. When a Dialog Listener installed on a new pool connects to the configuration service (which is running on an SDN Manager) a new group of Dialog Listener settings is generated for the fully qualified domain name of the Skype for Business Server front end pool.
    
  
- **Subscriber**. (s)ubscriber settings are used for a specified receiver of the media stream, call, and quality messages. You need to manually create a settings group for each receiver.
    
  

> [!NOTE]
> Listener and manager settings are populated by default, but subscriber settings are not set until a subscriber is created. Also note that only one manager setting can be used. 
  
    
    

This article contains the following section: 
-  [Command-line commands](configuring-sdn-interface-using-the-command-prompt.md#bk_commands)
    
  
-  [Example commands for viewing settings](configuring-sdn-interface-using-the-command-prompt.md#bk_examples)
    
  
-  [SDN manager configuration settings](configuring-sdn-interface-using-the-command-prompt.md#bk_manager)
    
  
-  [Dialog Listener configuration settings](configuring-sdn-interface-using-the-command-prompt.md#bk_listener)
    
  
-  [Subscriber configuration settings](configuring-sdn-interface-using-the-command-prompt.md#bk_subscriber)
    
  

## Command-line commands
<a name="bk_commands"> </a>

The command-line executable, SDNManager.exe, is located in the SDN Manager installation directory, which by default is located at  `C:\\Program Files\\Microsoft Skype for Business Server\\Microsoft Skype for Business SDN Manager`. The command-line interface provides three categories of commands: 
  
    
    

- CRUD (create, read, update, delete) operation commands on listener, subscriber, and manager settings. 
    
  
- A command to download and sync threshold limit settings from the QoEMetrics database. 
    
  
- Replay recorded log commands, which are debugging and test operations. For more information, see  [Debugging the SDN Interface SDN Manager](debugging-the-sdn-manager.md). 
    
  
Each command allows you to explicitly specify the URI and, if necessary, client certificate thumbprint to connect to the configuration service to update settings or to the SDN Manager service to execute a replay file. 
  
    
    
To access help on the available commands, enter the following at the command prompt:  `C:\\ > SDNManager.exe /?`. The command produces the following output: 
  
    
    



```ms-dos

Skype for Business SDN Manager  3.0, Build Version: 7.0.1459.1
Copyright (C) Microsoft Corporation.  All rights reserved.
    /?        Help
     /status [http(s)://site] [thumbprint]
             ....   Retrieve some statistics mostly related to Listners connections

    /subscriberstatus subscriberid [http(s)://site] [thumbprint]
             ....   Retrieve some statistics about the connection to each subscriber
    /u(pload) m(anager) filename [http(s)://site] [thumbprint]
    /u(pload) s(ubscriber) subscriberid filename [http(s)://site] [thumbprint]
    /u(pload) l(istener) dialoglistenerid filename [http(s)://site] [thumbprint]
             ....   Upload &amp; store
                    configuration into the datastore
                    through the SDN Manager service referred to by the url or the local service
                    using the client certificate thumbprint.
    /d(ownload) m(anager) [filename] [http(s)://site/Settings] [thumbprint]
    /d(ownload) s(ubscriber) [subscriberid] [filename] [http(s)://site] [thumbprint]
    /d(ownload) l(istener) [dialoglistenerid] [filename] [http(s)://site] [thumbprint]
             ....   Download configuration from the store into a local file from the
                    SDN Manager service (referred to by the url or the local service)
                    using the client certificate thumbprint.
    /p(arameter) m(anager) name=value [http(s)://site] [thumbprint]
    /p(arameter) s(ubscriber) subscriberid name=value [http(s)://site] [thumbprint]
    /p(arameter) l(istener) dialoglistenerid name=value [http(s)://site] [thumbprint]
             ....   Set an individual
                    configuration value in the SDN Manager service (referred to by the url
                    or the local service) using the client certificate thumbprint.
    /r(emove) s(ubscriber) subscriberid [http(s)://site] [thumbprint]
    /r(emove) l(istener) dialoglistenerid [http(s)://site] [thumbprint]
             ....   Remove or reset a settings group
                    in the SDN Manager service (referred to by the url
                    or the local service) using the client certificate thumbprint.
    /e filename  [http(s)://site] [thumbprint]
             ....   Send content of the file as messages to the
                    SDN Manager service (referred to by the url or the local service)
                    using the client certificate thumbprint
                    and process these messages.
    /et filename  [http(s)://site] [thumbprint]
             ....   Send content of file as messages to the
                    SDN Manager service (referred to by the url or the local service)
                    using the client certificate thumbprint
                    and process these messages with the given timing preserved.
    /q dbserver [user] [password] [http(s)://site] [thumbprint]
             ....   Retrieve threshold values from a QoE database with the specified user
                    name and password and store them in the SDN Manager service
                    (referred to by the url or the local service)
                    using the client certificate thumbprint.
      []  ... optional parameter
      Example for SDNManager uris: http://server:9333/Settings or http://localhost:9333/DL



```


## Example commands for viewing settings
<a name="bk_examples"> </a>

Use the following example commands to view settings. 
  
    
    

- **Sdnmanager.exe d l**
    
    Displays all configured Dialog Listener settings. You will get one record after a Dialog Listener has started up and requested settings. 
    
  
- **Sdnmanager.exe d s**
    
    Displays all configured subscriber settings. 
    
  
- **Sdnmanager.exe p s mynms submituri=http://my_nms/sdn**
    
    Modifies the **submituri** setting for **my_nms** or creates new settings group for **my_nms** if one does not already exist. Similarly, you can then modify other settings to adjust the output behavior (signaling, quality, sendrawsdp, and so forth).
    
  
- **Sdnmanager.exe d s mynms mysettings.xml https://sdnhost:9333/Settings ‎23991123649b4cfcb48ccf14f2d08601221caa2c**
    
    Connects to a configuration service running on **sdnpool** using https; a client certificate selected using its thumbprint is used to authenticate the request. The request downloads the **mynms** subscriber settings and saves them in the mysettings.xml file.
    
  
- **Sdnmanager.exe u s mynms mysettings.xml**
    
    Upload settings saved using the previous commands. 
    
  
For a complete example of the output, see  [Appendix to Skype for Business SDN Interface](appendix.md). In addition, you can get all the threshold settings from the Skype for Business Server QoEMetrics database. The SDN Manager uses the downloaded thresholds to update the corresponding **manager** settings, as shown here ``C:\\>SDNManager.exe /q mySfB_BackEnd\\monitoring``. In the example, the fragment  ``mySfB_BackEnd\\monitoring`` represents the SQL Server instance hosting the Skype for Business Server QoEMetrics database. The command updates the threshold limits used to determine stream quality.
  
    
    

## SDN manager configuration settings
<a name="bk_manager"> </a>

The **manager** settings group provides settings for the general operation of the SDN Manager. An example of the output of the **manager** settings can be found in the [Appendix to Skype for Business SDN Interface](appendix.md). 
  
    
    


|**Setting**|**Description**|
|:-----|:-----|
| `calltimeout`|Maximum time expected for a call, after the call is assumed to be terminated and cleaned up. |
| `invitetimeout`|Maximum time expected for ringing, before either an error should be processed or the user should have picked up the call. |
| `qoetimeout`|Maximum time to wait for the second raw QoE report before forwarding a merged report to the subscriber(s). |
| `endedtimeout`|Maximum time to wait after an Ended message is received before cleaning up the call if no raw QoE report is received. |
| `maxcachesize`|Maximum number of call states cached in the internal cache when in cache mode. |
| `timeouthandlerperiod`|Time interval between checks for call timeouts in the data store. |
| `hidepii`|Set to **true** (the default value) to obfuscate or hide personal identifiable information (PII) in messages. Set to **false** to see PII, in particular a full SIP UI that includes the account name and telephone numbers.|
   
In addition to these configuration settings, the configuration file also contains a list of threshold values used by SDN Manager to determine stream quality. These threshold values are used for determining call quality. 
  
    
    
There are two threshold values for each metric: **Optimal** and **Acceptable**. The values define the three quality bands for each modality. For a call stream to have the  _Good_ quality, all the quality metrics must be below (better than) the **Optimal** level. A call is of _Poor_ quality when all the metrics are below the **Acceptable** threshold and one or more of them are above the **Optimal** threshold. A call is _Bad_ when one or more of the metrics are above (worse than) the **Acceptable** threshold.
  
    
    
You can modify these threshold settings by using the SDNManager.exe  `/p` the `/q` command. The `/p` command updates the value individually and the `/q` command downloads and updates all of the threshold settings from the Skype for Business Server QoEMetrics database.
  
    
    

> [!NOTE]
> When Video-based Screen Sharing (VbSS) is active, SDN Manager uses the following bandwidth limits for evaluating bandwidth limits of application sharing media streams: >  
`<parameter key="applicationsharing-AppliedBandwidthLimitAcceptable">14000</parameter>`
 `<parameter key="applicationsharing-AppliedBandwidthLimitOptimal">15000</parameter>`
  
    
    


## Dialog Listener configuration settings
<a name="bk_listener"> </a>

The **listener** settings group contains settings for the Dialog Listener running on a pool of Skype for Business front-end servers. Each server pool connected to the configuration service has its own settings group; however, all server front-ends in the pool use the same settings. Each pool is identified by its pool fully qualified domain name. You can find examples of output for the **listener** settings in the [Appendix to Skype for Business SDN Interface](appendix.md). 
  
    
    


|**Setting**|**Description**|
|:-----|:-----|
| `submituri`|Specifies the URI to the SDN Manager instance or pool. |
| `alternativeuri`|Specifies an alternative SDN Manager or disaster failover SDN Manager pool. |
| `clientcertificateid`|If a HTTPS connection is used to connect with the SDN Manager instance or pool, this parameter contains the thumbprint of the client certificate to use to authenticate the requests to the server. |
| `submitqueuelen`|Sets the maximum unanswered and waiting messages to the SDN Manager. Change this value only if network conditions require a longer queue length caused by fluctuating delays in message deliveries to the SDN Manager instance or pool. |
| `maxretry`|Specifies the maximum number of retransmission attempts before a message is dropped. |
| `maxdelaylimit`|Specifies the number of transmission failures before slowing down the transmission of call and quality messages. |
| `maxbackoff`|Specifies the maximum number of seconds to delay message delivery upon transmission errors. The delay starts at one second and increments another second with every failed delivery up to the specified  `maxbackoff` setting.|
| `maxopen`|Specifies the maximum number of messages sent concurrently. |
| `maxretrybeforefailover`|Specifies the maximum number of message transmission failures before attempting to fail over to the alternative SDN Manager instance or pool, if configured. |
   

## Subscriber configuration settings
<a name="bk_subscriber"> </a>

The **subscriber** settings group describes the behavior that each subscriber expects. You can see an example of output from subscriber settings in the [Appendix to Skype for Business SDN Interface](appendix.md). 
  
    
    


|**Setting**|**Description**|
|:-----|:-----|
| `submituri`|Specifies the URI of the receiver (network controller, network management system, or ITPro tool) that receives media stream and stream quality data. |
| `outputschema`|Specifies the format of the output messages. Choose "C" for generating the same message structure as in Lync SDN Interface 2.1 and 2.1.1. Select "D" (the default) for the new expanded structure introduced with Skype for Business SDN Interface 2.2. Both schemas are documented in  [Skype for Business SDN Interface schema reference](http://msdn.microsoft.com/library/b64912bd-27b1-40c6-99ab-8984f8706bd3.aspx). |
| `clientcertificateid`|If a HTTPS connection is used to connect to the recipients, this parameter contains the thumbprint of the client certificate for authenticating requests to the server. |
| `domainfilters`|A comma-separated list of partial domain names for users. This subscriber receives call and quality messages for users that are in the specified domains. If no domain name is specified, no messages are filtered. |
| `subnetfilters`|A comma-separated list of subnets (IP4 or IPv6). The subscriber receives data about calls originating or being received in one of these subnets. To specify a subnet, use the format:  `196.168.0.0/16` or `2001:4898::dc76:194f%32`. |
| `tenantfilters`|A comma-separated list of TenantIds as used in Skype for Business Online, for which the subscriber receives call and stream quality data. |
| `trunkfilters`|A comma-separated list of SIP trunk names defined in Skype for Business, for which the subscriber receives call and stream quality data. |
| `quality`|Boolean that specifies whether to send the subscriber QualityUpdate and InCallQuality messages. **True** to send messages; **False** if no messages are to be sent.|
| `signaling`|Boolean that specifies whether to send  `<Invite>`,  `<Error>` and `<Bye>` messages to the subscriber. **True** to send messages.|
| `sendrawsdp`|Set value to **True** to forward SDP information to the subscriber (using the `<RawSDP>` tag). Set to **False** to prevent sending raw SDP content to this subscriber.|
| `maxretry`|Specifies the maximum number of retransmission attempts before a message is dropped. |
| `maxdelaylimit`|Specifies the number of transmission failures before slowing down the transmission of call and quality messages. |
| `maxbackoff`|Specifies the maximum number of seconds to delay message delivery upon transmission errors. The delay starts at one second and increments another second with every failed delivery up to the specified  `maxbackoff` setting.|
| `maxopen`|Specifies the maximum number of messages sent concurrently to the subscriber. |
| `submitqueuelen`|Specifies the maximum unanswered and waiting messages to send to this recipient. Change this value only if network conditions require a longer queue length caused by fluctuating delays in messages delivered to the subscriber. |
| `obfuscationseed`|Specifies an individual seed used for the obfuscation of SIP aliases and telephone numbers if the manager setting hidepii is set to true. |
| `schemaextension`|Boolean value, when 'True' (the default) additional fields are presented to the subscriber. (See  [Schema Extensions](schema-extensions.md)) |
   

## Additional resources
<a name="bk_addresources"> </a>


-  [Configuring Skype for Business SDN Interface](configuring-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  


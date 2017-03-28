---
title: Configuring the SDN manager by editing the config file
ms.prod: SKYPE
ms.assetid: 717466aa-c6b2-42ab-8492-8d2f4bed7765
---


# Configuring the SDN manager by editing the config file

 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

You can edit the SDNManager.exe.config file to change configuration settings for the SDN Manager. The SDNManager.exe.config file is located in the default installation directory, where you will also find SDNManager.exe. 
  
    
    

The following example shows how to edit the  `<appSettings>` section in the configuration file:


```xml

<appSettings>
  <add key="configurationserviceuri" value="http://localhost:9333/Settings"/>
  <add key="configurationcertificate" value=""/>
  <add key="configurationrefresh" value="00:00:50"/>
  <add key="checkdns" value="false"/>
  <add key="mode" value="Database"/>
  <add key="statedbserver" value="dblneprod"/>
  <add key="statedbname" value="SDNManager22"/>
  <add key="statedbusername" value=""/>
  <add key="statedbpassword" value=""/>
  <add key="redisconnectstring" value=""/>
  <add key="redispassword" value=""/>
  <add key="usedapi" value="False"/>
  <add key="identifier" value="MySDNDB"/>
</appSettings>

```

In the example code, the **Mode** key value describes the operational mode of the SDN Manager (cache, database, or Redis). All SDN Manager instances in a SDN Manager pool must have the same setting. The SDN Manager itself uses the configuration service to periodically retrieve updates to the configuration settings. Usually, it will use its own configuration service, but the **configurationserviceuri** key value lets you point to another SDN Manager to maintain the configuration data.The **configurationrefresh** key value indicates how often the SDN Manager updates changes in its data store. In the example, this value is set to every 30 seconds (00:00:30) The **configurationcertificate** key value is used to authenticate the SDN Manager to the configuration service.The **checkdns** key value is a Boolean and, if **true**, indicates there is an alternative to using a DNS SRV record to locate the configuration service. The **StateDbServer**, **StateDbName**, **StateDbUserName**, and **StateDbPassword** key values describe the connection to the shared SDN Manager database. If no user name is specified, integrated security is used. The **statedbserver** value can be an SQL Server database name (other than "localhost") or an SQL Server instance name.Similarly, the **RedisConnectString**, **RedisPassword**, and **usedapi** key values are used to connect a Redis cache service. The **Identifier** key value allows you to distinguish between different SDN Manager instances in a pool using the same Redis data store. The **Identifier** is case-sensitive
> [!IMPORTANT]
> Changes to the configuration file will take effect only after restarting the service. 
  
    
    


## Additional resources
<a name="bk_addresources"> </a>


-  [Configuring Skype for Business SDN Interface](configuring-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  


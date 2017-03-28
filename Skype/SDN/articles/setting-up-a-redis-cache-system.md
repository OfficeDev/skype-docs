---
title: Setting up a Redis cache system
ms.prod: SKYPE
ms.assetid: 6266c208-25a0-4f59-a66d-990cfda79052
---


# Setting up a Redis cache system

 **Last modified:** February 17, 2016
  
**Applies to:** Lync Server 2013 | Skype for Business 2015
 
Redis ( [http://redis.io/](http://redis.io/)) is a fast, light weight, scalable in-memory key-value cache system that you can use for a data store for your Skype for Business SDN Interface implementation 
  
Microsoft Azure offers a Redis service to which you can connect; however, you can also run Redis on Windows ( [https://github.com/MSOpenTech/redis](https://github.com/MSOpenTech/redis)) or on other operating systems. Using the Redis connection string and the configuration file (for example, redis-windows.conf) you can configure to use SSL, active/passive failover, as well as number of other parameters. 
  
    
    
In a pool configuration, Skype for Business SDN Interface needs a data store to share call states for concurrently ongoing calls or for configuration settings among multiple SDN Manager instances, which can be a Redis No-SQL key-value store. 
  
    
    

> [!NOTE]
> Redis must be set up and running when SDN Manager is installed in Redis mode to prevent errors during service startup. The setup will not verify this. <br/> It is recommended setting up the connection to the Redis datastore using a TLS connection as well as using a Redis password for encrypting the database. This Redis Password and other connection parameters can be specified during the setup and in the SDNManager.exe.config file. Further info can be found https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/Configuration.md. <br/>  In enterprise deployments, we recommend using the Azure Redis Service or a dedicated cluster of a primary and a secondary Redis server using Redis Sentinels for managing fail-over behavior. Alternatively, depending on the load pattern, Redis may also be co-located on SDN Manager servers.
  
    
    


## Additional resources


-  [Getting ready to install Skype for Business SDN Interface](getting-ready-to-install-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  


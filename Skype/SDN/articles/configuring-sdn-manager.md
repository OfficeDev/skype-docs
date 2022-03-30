---
title: Configure Interface SDN Manager
description: Learn how to configure and run SDN Manager in three different operational modes: cache, database, and Redis.
ms.date: 03/29/2022
---

# Configure Interface SDN Manager

**Applies to**: Lync Server 2013 | Skype for Business 2015 | Skype for Business 2019

SDN Manager can run in three different operational modes: cache, database, and Redis. Which of these three the SDN Manager uses depends on how it stores and shares the state of call streams, as well as its configurations settings.
  
- **Cache mode**. When in cache mode, stream state is kept only in local memory and is not shared among SDN Manager instances. You can only use this option if you run the SDN Manager as a singleton, or in a primary-secondary failover configuration. When using this option, ensure that there is enough memory to hold call state data for all active calls during peak times.

- **Database mode**. In the database mode, stream state is stored in a SQL Server database. All of the configuration settings are shared among all SDN Manager instances that are connected to the same database.

- **Redis mode**. In Redis mode, the stream state is stored in a Redis in-memory cache system.

When the SDN Manager is asked for settings through its configuration service, it uses the same data store to maintain these settings. In Cache mode, settings are persisted to prevent you from needing to reconfigure them after a service restart.

## Operational modes of the SDN Manager

The SDN Manager operational mode you select depends on how the Skype for Business SDN Interface is deployed. For more information about the deployment scenarios, see [Deploying Skype for Business SDN Interface](deploying-the-sdn-interface.md).
  
Typically, the SDN Manager installation determines all the necessary configuration settings, such as those for selecting the deployment mode ( `mode`), connecting to the database (`statedbserver`, `statedbusername`, `statedbpassword`), or connecting to Redis. To change these settings, you must edit them directly in the configuration file. After the configuration file is edited, you must restart the SDN Manager service for the changes to take effect. Any further changes to the configuration settings must thereafter be completed by using the command line interface.
  
You can also configure SDN Manager to maintain logging information to assist with debugging. For information about setting or updating the logging configuration, see [Configuring SDN Interface logging options](configuring-logging-options.md).
  
## See also


- [Configuring Skype for Business SDN Interface](configuring-sdn-interface.md)
- [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)

---
title: Setting up SQL Server for a SDN manager database
ms.prod: SKYPE
ms.assetid: 2b38badb-b30d-4003-8d18-c84c150feb91
---


# Setting up SQL Server for a SDN manager database

 **Last modified:** August 17, 2015
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015
 
In a pool configuration, Skype for Business SDN Interface requires a data store to share call states of concurrently ongoing calls or configuration settings among multiple SDN Managers, which can be a SQL Server or other data store. However, this SDN Manager database is hosted on a SQL Server database. Make sure to select the SQL Server host that supports appropriate behaviors in the presence of fault.
  
    
    

To minimize hardware investments, and after evaluating available performance resources on the Skype for Business backend SQL Server database, you may consider using the SDN Manager server. You can use any SQL Server edition, including the SQL Server Express edition. The installer creates and configures a new database or joins a new SDN Manager installation with an existing database.
## Additional resources


-  [Getting ready to install Skype for Business SDN Interface](getting-ready-to-install-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  


---
title: SDN Interface Performance Overview
ms.prod: SKYPE
ms.assetid: 2ba10414-fcc4-40be-b87e-dc52f517c626
---


# SDN Interface Performance Overview
Provides some guidelines on performance benchmarks for Skype for Business SDN Interface.
 **Last modified:** February 23, 2017
  
    
    

**Applies to:** Lync Server 2013 | Skype for Business 2015
 
Versions 2.2 and later of the Skype for Business SDN Interface were tested in a production environment and in lab setups but no formal scalability testing has been conducted. Nevertheless, the following performance data should give you an idea what you can expect. Please consider these numbers as example only.
  
    
    


## Performance benchmarks


- A pool of two SDN Manager servers can support the traffic from 48 Skype for Business front-end servers supporting around 80,000 users in one continent. CPU and memory impact of the Dialog Listener on the front-ends is hardly noticeable, while the load in the two SDN Manager servers reaches overall 50% in high load situations.
    
  
- Memory consumption is low and fairly stable.
    
  
- Sample machine configuration:
    
  - **Stand-alone SDN Manager server**: Windows Server 2012 R2
    
  
  - **CPU**: 8 Cores, 8GB of memory
    
  
  - **Hard drive**: 1 TB
    
  
- In lab scenarios, we execute load of 400 audio calls per minute against two front-end servers and two SDN Manager instances. These topologies consist of virtual machines only with not more than two cores and 8 GB of memory each.
    
  
- Bandwidth includes the RTCP stream, which may use a different port.
    
  
- If a pool configuration is used around a database, the SQL Server is expected to be production level server with state-of-the art memory and CPU. Similarly, a REDIS cache server must be configured to handle the load.
    
  
Results with default parameters, SDN Manager supported: 
  
    
    

- A sustained call rate of 400 CPM with 2 (active) Subscribers.
    
  
- A sustained call rate of 200 CPM with 8-9 (active) Subscribers.
    
  
- A sustained call rate of 100 CPM with 22 (active) Subscribers.
    
  


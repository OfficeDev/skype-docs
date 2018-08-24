---
title: Understanding Skype for Business SDN Interface
 
ms.assetid: 4c97bce1-4b8b-4c13-8ec6-99eed59d88fc
---


# Understanding Skype for Business SDN Interface


  
    
    

 **Applies to**: Lync Server 2013 | Skype for Business 2015
 
In any given Skype for Business Server deployment, poor network performance can adversely affect the end user experience. Many factors can cause low performance or media quality. Exposing these is often difficult, as well as time and resource intensive. 
  
    
    

Skype for Business provides tools that help identify issues in its system and network infrastructure, as do infrastructure and network manufacturers. The primary purpose of the SDN Interface is to bridges these two worlds to enable a deeper understanding of the entire end-to-end system. 
## The Microsoft Skype for Business SDN Interface

The Skype for Business SDN Interface is distributed as a downloadable installation package that supports multiple Lync and Skype for Business versions. This content explains how to install and configure the Skype for Business SDN Interface to work with one or more known network management systems and network controllers. It also provides a technical reference to the Skype for Business call and quality data that is provided. To get a contextual framework for the technical information, take a look at the conceptual framework of the Skype for Business SDN Interface. 
  
    
    

> [!IMPORTANT]
> The Skype for Business SDN Interface version 3.0 adds significant new functionality, as well as changes to the message format. It also fixes bugs in the previous release but provides backward compatibility so that its message format and semantics are fully compatible with the Lync SDN Interface 2.1.1. 
  
    
    


## The Microsoft Skype for Business SDN Interface schema

In Skype for Business SDN Interface, the call and quality data is defined by the new Skype for Business SDN Interface schema ( **SDNInterface.Schema.D.xsd** ). This schema expands on the schema file that was supported in Lync SDN API version 2.0 ( **SDNInterface.Schema.C.xsd** ). In **Schema D**, more data types are added and some improvements have been introduced. However, the schema structure is very similar and most message parsers written for **Schema C** should also accept a **Schema D** implementation without changes. Documentation of both schemas can be found in [Skype for Business SDN Interface schema reference](http://msdn.microsoft.com/library/553e325e-d48a-4e7b-b7ac-042f87253ed8.aspx). 
  
    
    

> [!NOTE]
> With release 3.0, the presence of some fields provided to a Subscriber (primarily Wi-Fi quality metrics) can be suppressed; see the `schemaextension` [subscriber configuration setting](configuring-sdn-interface-using-the-command-prompt.md#subscriber-configuration-settings). 

    
    


## In this section


-  [Using the Skype for Business SDN Interface](using-the-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface architecture](interface-architecture.md)
    
  
-  [Deploying Skype for Business SDN Interface](deploying-the-sdn-interface.md)
    
  
-  [New features in Skype for Business SDN Interface](new-features.md)
    
  
-  [In-call QoE algorithm and throttling](in-call-qoe-algorithm-and-throttling.md)
    
  

## See also

-  [Overview of Skype for Business SDN Interface](overview.md)  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  

  
    
    


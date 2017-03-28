---
title: HostIP element (ErrorEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 57312cc7-b82d-4f38-896a-2352a255cf0a
---


# HostIP element (ErrorEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
IP address of the endpoint's host in the local network. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [ipAddressPattern](ipaddresspattern-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="HostIP"  type="ipAddressPattern" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [EndPoint](endpoint-element-errortype-complextype-1.md)| [ErrorEndPointType](errorendpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


---
title: HostIP element (QualityEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 96c8959c-df01-9e05-c485-8b4c63d0745b
---


# HostIP element (QualityEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
IP address of the the media stream source or destination. 
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
| [From](from-element-qualitytype-complextype.md)| [QualityEndPointType](qualityendpointtype-complextype-1.md)|The source of the reported media stream. |
| [To](to-element-qualitytype-complextype.md)| [QualityEndPointType](qualityendpointtype-complextype-1.md)|Destination of the media stream. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


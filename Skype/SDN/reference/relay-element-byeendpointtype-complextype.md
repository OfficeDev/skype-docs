---
title: Relay element (ByeEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 2aa217f7-eff3-d4d5-efcd-24ebc0c06459
---


# Relay element (ByeEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
IP Address of the first relay used in the media traffic. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [ipAddressPattern](ipaddresspattern-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Relay"  type="ipAddressPattern" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [EndPoint](endpoint-element-byetype-complextype.md)| [ByeEndPointType](byeendpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


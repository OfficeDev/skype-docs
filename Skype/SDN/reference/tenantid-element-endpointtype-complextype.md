---
title: TenantId element (EndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: de127ded-f67e-6a89-d2fa-c2068e65c66b
---


# TenantId element (EndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Identifier for the tenanat that this endpoing belongs to. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="TenantId"  type="xs:string" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [EndPoint](endpoint-element-endedtype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
| [From](from-element-endedtype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
| [From](from-element-startorupdatetype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Source of the media stream. |
| [From](from-element-errortype-complextype.md)| [EndPointType](endpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
| [To](to-element-endedtype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
| [To](to-element-startorupdatetype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Destination of the media stream. |
| [To](to-element-errortype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


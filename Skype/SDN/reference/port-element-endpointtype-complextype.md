---
title: Port element (EndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 938795a2-465e-a4c2-1644-13a29afd14da
---


# Port element (EndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Port number of the destination or source of the media stream used by this endpoint. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [PortPattern](portpattern-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Port"  type="PortPattern">
    
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
  
    
    


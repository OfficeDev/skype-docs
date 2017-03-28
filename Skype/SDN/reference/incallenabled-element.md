---
title: IncallEnabled element (EndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: c3ef3b10-fbe9-5e0f-cb2b-3c8d35708fc0
---


# IncallEnabled element (EndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Whether the endpoint (Skype for Business client) is capable to send incall quality update messages. This flag does not indicate whether the client is configured to send incall QoE reports. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:boolean |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="IncallEnabled"  type="xs:boolean" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Callee](callee-element.md)| [EndPointType](endpointtype-complextype-1.md)|Properties of the callee. |
| [Caller](caller-element-1.md)| [EndPointType](endpointtype-complextype-1.md)|Properties of the caller. |
| [EndPoint](endpoint-element-errortype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [EndPoint](endpoint-element-endedtype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [EndPoint](endpoint-element-byetype-complextype-1.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [From](from-element-endedtype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [From](from-element-startorupdatetype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Source of the media stream. |
| [From](from-element-errortype-complextype-1.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [To](to-element-endedtype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [To](to-element-startorupdatetype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Destination of the media stream. |
| [To](to-element-errortype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


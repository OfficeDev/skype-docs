---
title: URI element (EndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: bb410e90-f652-b1be-2dc7-7e350698e3b9
---


# URI element (EndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
SIP URI of the user signed in via the endpoint as extracted from the SIP header.. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:anyURI |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="URI"  type="xs:anyURI">
    
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
  
    
    


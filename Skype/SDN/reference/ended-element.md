---
title: Ended element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: a5910d3e-5fd6-326e-4fa1-33697da7c879
---


# Ended element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Event that a Sip call has ended and all media stream terminated. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [EndedType](endedtype-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Ended"  type="EndedType">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [LyncDiagnostics](lyncdiagnostics-element-1.md)| [MessageType](messagetype-complextype-1.md)|The root element for output from the Skype for Business SDN Manager. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [EndPoint](endpoint-element-endedtype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [From](from-element-endedtype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [Properties](properties-element-endedtype-complextype-1.md)| [EndedProperties](endedproperties-complextype-1.md)|Properties of the Error. |
| [To](to-element-endedtype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:string |required |Modality or media type for which the quality metrics are reported. The supported options are audio, video and applicationsharing. |Values of the xs:string type. |
   


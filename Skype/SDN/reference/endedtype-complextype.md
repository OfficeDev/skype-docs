---
title: EndedType complexType (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: dda03093-dc9d-8c38-9913-a17e91917115
---


# EndedType complexType (Skype for Business SDN Interface 2.2, Schema "C")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="EndedType">
         <xs:attribute name="Type" type="xs:string" use="required"/>
  
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

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
   


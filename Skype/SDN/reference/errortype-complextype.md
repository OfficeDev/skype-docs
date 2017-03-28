---
title: ErrorType complexType (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: bce95c26-6495-9562-4b1f-985f8d0d77bd
---


# ErrorType complexType (Skype for Business SDN Interface 2.2, Schema "C")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="ErrorType">
         <xs:attribute name="Type" type="xs:string" use="optional"/>
  
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [EndPoint](endpoint-element-errortype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [From](from-element-errortype-complextype-1.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
| [Properties](properties-element-errortype-complextype-1.md)| [ErrorProperties](errorproperties-complextype.md)|Properties of the Error. |
| [To](to-element-errortype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:string |optional |Modality or media type for which the quality metrics are reported. The supported options are audio, video and applicationsharing. |Values of the xs:string type. |
   


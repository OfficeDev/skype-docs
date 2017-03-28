---
title: StartOrUpdateType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 7023ae43-40d9-c4b8-d8eb-530cb383130e
---


# StartOrUpdateType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="StartOrUpdateType">
         <xs:attribute name="Type" type="xs:string" use="required"/>
  
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [From](from-element-startorupdatetype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Source of the media stream. |
| [Properties](properties-element-startorupdatetype-complextype.md)| [StartPropertiesType](startpropertiestype-complextype-1.md)|Properties of the started or updated media stream. |
| [To](to-element-startorupdatetype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Destination of the media stream. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:string |required |Modality or media type for which the quality metrics are reported. The valid options are audio, video and applicationsharing |Values of the xs:string type. |
   


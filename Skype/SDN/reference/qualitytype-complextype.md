---
title: QualityType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 300b4904-2f47-ee80-bc52-0598bbc9dfe7
---


# QualityType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="QualityType">
         <xs:attribute name="Type" type="ConnectionModalities" use="required"/>
  
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [From](from-element-qualitytype-complextype.md)| [QualityEndPointType](qualityendpointtype-complextype-1.md)|The source of the reported media stream. |
| [Properties](properties-element-qualitytype-complextype-1.md)| [QualityPropertiesType](qualitypropertiestype-complextype.md)|Properties of the media stream, including a selected set of quality metrics reported and thresholds that are used to determine a bad call. |
| [Route](route-element-qualitytype-complextype-1.md)| [RouteType](routetype-complextype-1.md)|Network path of the media stream only provided in Skype for Business 2013 and when the traceRoute feature is activated in Skype for Business. |
| [To](to-element-qualitytype-complextype.md)| [QualityEndPointType](qualityendpointtype-complextype-1.md)|Destination of the media stream. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type | [ConnectionModalities](connectionmodalities-simpletype.md)|required |Modality or media type for which the quality metrics are reported. The supported options are audio, video and applicationsharing. |Values of the ConnectionModalities type. |
   


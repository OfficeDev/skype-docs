---
title: DynamicCapabilityPercent element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 70886cb7-42f8-c94c-b1a3-48498304b176
---


# DynamicCapabilityPercent element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Percentage of time that the client is running under capability of less than 70% of expected capability for this type of CPU. Inbound and Outbound are identical because it measures the capability of the client instead of the channel. This metric is reported for video streams when available. (percent) 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [DoubleBetween0And100](doublebetween0and100-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="DynamicCapabilityPercent"  type="DoubleBetween0And100">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Properties](properties-element-qualitytype-complextype-1.md)| [QualityPropertiesType](qualitypropertiestype-complextype.md)|Properties of the media stream, including a selected set of quality metrics reported and thresholds that are used to determine a bad call. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


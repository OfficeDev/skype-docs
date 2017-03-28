---
title: PacketLossRate element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 8100f94b-c89d-cc0a-5228-5fee575a4c6b
---


# PacketLossRate element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Average fraction lost computed over the duration of the session, as specified in [RFC3550] section 6.4.1. This metric is reported for all available modalities and media types. (percent) 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [DoubleBetween0And100](doublebetween0and100-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="PacketLossRate"  type="DoubleBetween0And100">
    
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
  
    
    


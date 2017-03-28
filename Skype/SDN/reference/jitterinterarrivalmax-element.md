---
title: JitterInterArrivalMax element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: e1b198ae-9856-8c49-160e-4c49b0aef912
---


# JitterInterArrivalMax element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Maximum inter-arrival jitter, as specified in [RFC3550] section 6.4.1. This metric is reported for all modalities/media types when available. (ms) 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="JitterInterArrivalMax"  type="xs:string">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Properties](properties-element-qualitytype-complextype.md)| [QualityPropertiesType](qualitypropertiestype-complextype-1.md)|Properties of the media stream, including a selected set of quality metrics reported and thresholds that are used to determine a bad call. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


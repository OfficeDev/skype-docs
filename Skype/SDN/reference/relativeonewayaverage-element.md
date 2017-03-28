---
title: RelativeOneWayAverage element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: c2169339-f0d9-8b20-426d-0fabc1b0463c
---


# RelativeOneWayAverage element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Average amount of one-way latency. Relative one-way latency measures the delay between the client and the server. This metric is only reported for application sharing streams using Skype for Business 2013. (ms) 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:double |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="RelativeOneWayAverage"  type="xs:double">
    
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
  
    
    


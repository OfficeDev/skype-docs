---
title: RDPTileProcessingLatencyBurstDensity element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: f9b089bb-0197-e654-3292-6fc2e83fcbbf
---


# RDPTileProcessingLatencyBurstDensity element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Burst density in the processing time for remote desktop protocol (RDP) tiles. A "bursty" transmission is a transmission where data flows in unpredictable bursts as opposed to a steady stream. This metric is only reported for application sharing streams using Skype for Business 2013. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [NonNegativeDouble](nonnegativedouble-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="RDPTileProcessingLatencyBurstDensity"  type="NonNegativeDouble">
    
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
  
    
    


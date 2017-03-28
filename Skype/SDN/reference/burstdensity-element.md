---
title: BurstDensity element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 7e9ced1d-5232-3d1a-e36b-08af16380c27
---


# BurstDensity element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Average burst density, as specified in [RFC3611] section 4.7.2, is computed with a Gmin=16 for the received RTP packets. This metric is reported for audio streams when available and measures the average density of packet Loss during bursts of losses during the call. This field MUST be populated and MUST be set to zero if no packets have been received. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:unsignedInt |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="BurstDensity"  type="xs:unsignedInt">
    
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
  
    
    


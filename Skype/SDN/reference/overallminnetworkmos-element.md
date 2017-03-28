---
title: OverallMinNetworkMOS element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: a5010e57-ad91-c3e9-576d-ae5afc0bf4fb
---


# OverallMinNetworkMOS element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Minimum of MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2, based on the audio codec used, the observed packet loss and inter-arrival packet jitter. This metric is reported for audio streams when available. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [NonNegativeDouble](nonnegativedouble-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="OverallMinNetworkMOS"  type="NonNegativeDouble">
    
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
  
    
    


---
title: SpeakerGlitchRate element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: cd4db9f8-5e04-9276-d752-0c57eea65ccc
---


# SpeakerGlitchRate element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Average glitches per five minutes for the loudspeaker rendering. For good quality, this should be less than one per five minutes. This will not be reported by audio/video conferencing servers, mediation servers, or IP phones. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|Not defined |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="SpeakerGlitchRate" >
    
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
  
    
    


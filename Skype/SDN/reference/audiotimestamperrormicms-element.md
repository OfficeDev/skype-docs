---
title: AudioTimestampErrorMicMs element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 7a2c150d-342d-333b-8898-772018dbc1fe
---


# AudioTimestampErrorMicMs element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Speaking device clock drift rate, relative to CPU clock. Average error of microphone-captured-stream time stamp, in milliseconds, for the last 20 seconds of a call. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:double |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="AudioTimestampErrorMicMs"  type="xs:double">
    
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
  
    
    


---
title: RecvSignalLevel element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: faec1495-0a59-9bd3-2ecf-f24e2963d293
---


# RecvSignalLevel element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Received signal level in units of dB. This metric is reported for audio streams when available. Average energy level of received audio is classified as mono speech, or left channel of stereo speech. (dB) 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:int |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="RecvSignalLevel"  type="xs:int">
    
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
  
    
    


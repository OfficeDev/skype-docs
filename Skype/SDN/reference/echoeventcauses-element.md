---
title: EchoEventCauses element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: d4ef7e39-31c8-333a-ed96-7d441f12d1aa
---


# EchoEventCauses element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Reasons of device echo detection and reported for audio streams when available. The causes are coded with the following bit flags: "0x01" - Sample timestamps from capture or render device were poor quality. "0x04" - High level of echo remained after echo cancellation. "0x10" - Signal from capture device had significant instances of maximum signal level. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:unsignedInt |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="EchoEventCauses"  type="xs:unsignedInt">
    
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
  
    
    


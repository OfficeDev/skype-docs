---
title: DeviceClippingEventRatio element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 5c19d36b-dbbd-442c-4dd3-a7d00e9c8109
---


# DeviceClippingEventRatio element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Percentage of sessions the DeviceClipping event was fired when a speaker clips the microphone, causing the remote listener receives clipping-induced distortions. It is important to avoid the microphone clipping. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [DoubleBetween0And100](doublebetween0and100-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="DeviceClippingEventRatio"  type="DoubleBetween0And100">
    
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
  
    
    


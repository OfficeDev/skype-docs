---
title: VideoLocalFrameLossPercentageAvg element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: c23227a1-3a58-b0b7-ef01-cd8804169e3f
---


# VideoLocalFrameLossPercentageAvg element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Average percentage of video frames lost as they are displayed to the user, including frames recovered from network losses. This metric is reported for video streams when available. (percent) 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [DoubleBetween0And100](doublebetween0and100-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="VideoLocalFrameLossPercentageAvg"  type="DoubleBetween0And100">
    
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
  
    
    


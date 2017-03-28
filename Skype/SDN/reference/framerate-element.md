---
title: FrameRate element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: fe6e2a77-2f44-2eab-25f1-f256bbafc84e
---


# FrameRate element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Average frame rate (in frames per second). When available, this metric is only reported for application sharing streams and only for Skype for Business 2013. (frames/s) 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:decimal |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="FrameRate"  type="xs:decimal">
    
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
  
    
    


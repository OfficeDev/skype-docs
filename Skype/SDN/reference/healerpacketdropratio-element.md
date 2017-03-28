---
title: HealerPacketDropRatio element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 9a2c95a6-2ed0-1128-5976-1968d3cc0207
---


# HealerPacketDropRatio element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Ratio of audio packets dropped by a healer over total number of audio packets received by the healer. This metric is reported for all modalities/media types when available. (percent) 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="HealerPacketDropRatio"  type="xs:string">
    
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
  
    
    


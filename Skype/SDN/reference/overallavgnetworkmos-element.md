---
title: OverallAvgNetworkMOS element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 4bd89431-d42b-1271-f30d-282c75c154b8
---


# OverallAvgNetworkMOS element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Average of MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2, based on the audio codec used, the observed packet loss and inter-arrival packet jitter. This metric is reported for audio streams when available. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="OverallAvgNetworkMOS"  type="xs:string">
    
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
  
    
    


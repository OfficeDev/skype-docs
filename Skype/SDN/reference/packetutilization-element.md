---
title: PacketUtilization element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: e13be3e1-1e9d-8014-494e-047b4730e72e
---


# PacketUtilization element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Number of Real-time Transport Protocol (RTP) packets received in the session. This metric is reported for all available modalities and media types. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:int |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="PacketUtilization"  type="xs:int">
    
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
  
    
    


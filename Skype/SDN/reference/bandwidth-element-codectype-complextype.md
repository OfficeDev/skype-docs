---
title: Bandwidth element (CodecType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 73e20c7f-93d5-586a-fd1b-f15bb6bd50a0
---


# Bandwidth element (CodecType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Average estimated bandwidth. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [BandwidthDataType](bandwidthdatatype-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Bandwidth"  type="BandwidthDataType" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Codec](codec-element-qualitypropertiestype-complextype.md)| [CodecType](codectype-complextype-1.md)|Describes the last codec used for the media. |
| [Codec](codec-element-startpropertiestype-complextype.md)| [CodecType](codectype-complextype-1.md)|Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling). |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


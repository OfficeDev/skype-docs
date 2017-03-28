---
title: MaxBandwidth element (CodecType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: c548981f-4a0b-f4cb-cdb8-28cd2b38e303
---


# MaxBandwidth element (CodecType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Upper limit of the estimated bandwidth. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="MaxBandwidth"  type="xs:string" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Codec](codec-element-qualitypropertiestype-complextype-1.md)| [CodecType](codectype-complextype.md)|Describes the last codec used for the media. |
| [Codec](codec-element-startpropertiestype-complextype-1.md)| [CodecType](codectype-complextype.md)|Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling). |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


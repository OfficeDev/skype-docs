---
title: Codec element (StartPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 580d5efd-e174-cc1d-9992-15d996f7cfe3
---


# Codec element (StartPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling). 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [CodecType](codectype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Codec"  type="CodecType" maxOccurs="unbounded" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Properties](properties-element-startorupdatetype-complextype-1.md)| [StartPropertiesType](startpropertiestype-complextype.md)|Properties of the started or updated media stream. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Bandwidth](bandwidth-element-codectype-complextype-1.md)|xs:string |Average estimated bandwidth. |
| [MaxBandwidth](maxbandwidth-element-1.md)|xs:string |Upper limit of the estimated bandwidth. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Name |xs:string |required |Name of the standard SIP codec used for the media stream. |Values of the xs:string type. |
   


---
title: Codec element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 80291521-b022-7aee-8e35-88d370287c31
---


# Codec element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Describes the last codec used for the media. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [CodecType](codectype-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Codec"  type="CodecType">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Properties](properties-element-qualitytype-complextype-1.md)| [QualityPropertiesType](qualitypropertiestype-complextype.md)|Properties of the media stream, including a selected set of quality metrics reported and thresholds that are used to determine a bad call. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Bandwidth](bandwidth-element-codectype-complextype.md)| [BandwidthDataType](bandwidthdatatype-simpletype.md)|Average estimated bandwidth. |
| [MaxBandwidth](maxbandwidth-element.md)| [BandwidthDataType](bandwidthdatatype-simpletype.md)|Upper limit of the estimated bandwidth. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Name |xs:string |required |Name of the standard SIP codec used for the media stream. |Values of the xs:string type. |
|PayLoadType |xs:int |optional |Payload type used when this codec is active. |Values of the xs:int type. |
   


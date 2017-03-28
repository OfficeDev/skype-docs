---
title: CodecType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 890c900f-33ef-f8ad-f909-73824c8126e5
---


# CodecType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="CodecType">
         <xs:attribute name="Name" type="xs:string" use="required"/>
  
         <xs:attribute name="PayLoadType" type="xs:int" use="optional"/>
  
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

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
   


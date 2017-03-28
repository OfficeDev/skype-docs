---
title: StartPropertiesType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 963421e3-524b-0997-ce67-04875aafa72c
---


# StartPropertiesType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="StartPropertiesType">
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Bandwidth](bandwidth-element-startpropertiestype-complextype.md)| [BandwidthType](bandwidthtype-complextype-1.md)|Describes the maximum and average amount of bandwidth needed by this stream. It takes the possible codecs and stream multiplexing into account. |
| [Codec](codec-element-startpropertiestype-complextype.md)| [CodecType](codectype-complextype-1.md)|Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling). |
| [Protocol](protocol-element-startpropertiestype-complextype-1.md)| [ProtocolTypes](protocoltypes-simpletype.md)|Transmission protocol of the media stream such as TCP or UDP. |
| [ReferredBy](referredby-element.md)|xs:string |Content of the REFERRED BY SIP tag. |
| [Replaces](replaces-element.md)|xs:string |Content of the REPLACES SIP tag. |
| [Via](via-element.md)|xs:string |Content of the VIA SIP tags. |
   

### Attributes

None. 
  
    
    


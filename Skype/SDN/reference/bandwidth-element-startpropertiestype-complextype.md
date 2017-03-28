---
title: Bandwidth element (StartPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: c8eb0297-a57c-0f7e-1d2a-c3c37e8cae5e
---


# Bandwidth element (StartPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Describes the maximum and average amount of bandwidth needed by this stream. It takes the possible codecs and stream multiplexing into account. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [BandwidthType](bandwidthtype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Bandwidth"  type="BandwidthType" minOccurs="0">
    
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
| [Average](average-element-1.md)|xs:long |Estimated average amount of the bandwidth. |
| [Maximum](maximum-element.md)|xs:long |Estimated upper limit of the bandwidth needed by this stream. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Multiplexed |xs:int |optional |Number of times this stream is multiplexed (if > 1). This might mean the overall bandwidth requirement could be up to as many times. |Values of the xs:int type. |
   


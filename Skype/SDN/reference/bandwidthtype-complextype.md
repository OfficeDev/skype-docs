---
title: BandwidthType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: a1e91fc5-070b-be19-ee72-96940ee01137
---


# BandwidthType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="BandwidthType">
         <xs:attribute name="Multiplexed" type="xs:int" use="optional"/>
  
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Average](average-element.md)|xs:long |Estimated average amount of the bandwidth. |
| [Maximum](maximum-element-1.md)|xs:long |Estimated upper limit of the bandwidth needed by this stream. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Multiplexed |xs:int |optional |Number of times this stream is multiplexed (if > 1). This might mean the overall bandwidth requirement could be up to as many times. |Values of the xs:int type. |
   


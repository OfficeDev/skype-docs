---
title: BandwidthDataType simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 58c03120-8c23-de41-406d-4e5f74129b1b
---


# BandwidthDataType simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:int |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="BandwidthDataType">
    
      <xs:restriction base="xs:int">
    
      <xs:minInclusive value="-1"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



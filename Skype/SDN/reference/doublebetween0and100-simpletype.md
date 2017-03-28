---
title: DoubleBetween0And100 simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 9798d530-04fe-6208-a73f-732c8ffdf5b5
---


# DoubleBetween0And100 simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:double |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="DoubleBetween0And100">
    
      <xs:restriction base="xs:double">
    
      <xs:minInclusive value="0"/>
    
      <xs:maxInclusive value="100"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



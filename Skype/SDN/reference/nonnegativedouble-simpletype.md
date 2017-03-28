---
title: NonNegativeDouble simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 2064bd04-273e-df5d-c7b7-9f55c1c62a41
---


# NonNegativeDouble simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:double |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="NonNegativeDouble">
    
      <xs:restriction base="xs:double">
    
      <xs:minInclusive value="0"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



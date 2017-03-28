---
title: PortPattern simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 48777069-7002-0023-cc09-98381b13305f
---


# PortPattern simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="PortPattern">
    
      <xs:restriction base="xs:string">
    
      <xs:pattern value="[0-9]{2,5}"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



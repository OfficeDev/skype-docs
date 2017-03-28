---
title: BSSIDPattern simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 0b7aa4c4-9022-1da0-6fb3-1a9ff8581415
---


# BSSIDPattern simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="BSSIDPattern">
    
      <xs:restriction base="xs:string">
    
      <xs:pattern value="[0-9A-F]{2}\\-[0-9A-F]{2}\\-[0-9A-F]{2}\\-[0-9A-F]{2}\\-[0-9A-F]{2}\\-[0-9A-F]{2}"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



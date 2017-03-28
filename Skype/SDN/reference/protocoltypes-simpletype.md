---
title: ProtocolTypes simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 45c7a603-1559-14bb-32f5-17a93e5e9b49
---


# ProtocolTypes simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="ProtocolTypes">
    
      <xs:restriction base="xs:string">
    
      <xs:enumeration value="UDP"/>
    
      <xs:enumeration value="tcp-pass"/>
    
      <xs:enumeration value="TCP-PASS"/>
    
      <xs:enumeration value="tcp-act"/>
    
      <xs:enumeration value="TCP-ACT"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



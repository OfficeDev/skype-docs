---
title: ConnectivityModalities simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 67b3a37a-d9a7-aa6a-536e-446a937f9457
---


# ConnectivityModalities simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="ConnectivityModalities">
    
      <xs:restriction base="xs:string">
    
      <xs:enumeration value="DIRECT"/>
    
      <xs:enumeration value="RELAY"/>
    
      <xs:enumeration value="HTTPPROXY"/>
    
      <xs:enumeration value="HTTP-PROXY"/>
    
      <xs:enumeration value="FAILED"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



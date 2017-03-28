---
title: ConnectionModalities simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: ae0503ec-337a-b6d0-795f-274d4ddb8dc0
---


# ConnectionModalities simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="ConnectionModalities">
    
      <xs:restriction base="xs:string">
    
      <xs:enumeration value="audio"/>
    
      <xs:enumeration value="video"/>
    
      <xs:enumeration value="data"/>
    
      <xs:enumeration value="applicationsharing"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



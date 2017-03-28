---
title: ConnectionPlatforms simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 3ce2b946-41fb-8db7-c4c9-1b4b72799319
---


# ConnectionPlatforms simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="ConnectionPlatforms">
    
      <xs:restriction base="xs:string">
    
      <xs:enumeration value="Wifi"/>
    
      <xs:enumeration value="wifi"/>
    
      <xs:enumeration value="Ethernet"/>
    
      <xs:enumeration value="wired"/>
    
      <xs:enumeration value="MobileBB"/>
    
      <xs:enumeration value="Tunnel"/>
    
      <xs:enumeration value="Other"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



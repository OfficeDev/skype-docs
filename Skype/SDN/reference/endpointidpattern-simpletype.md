---
title: EndpointIdPattern simpleType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 20c01d5f-044c-24bf-f9f0-a4600db1b6ce
---


# EndpointIdPattern simpleType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Base type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML

    <xs:simpleType  name="EndpointIdPattern">
    
      <xs:restriction base="xs:string">
    
      <xs:pattern value="[0-9a-fA-F]{1,40}"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```



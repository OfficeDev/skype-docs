---
title: UserAgentType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: a863d70e-aa89-a82b-7d77-7a2e1df35a92
---


# UserAgentType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|xs:string |
   

## Definition


```XML

      <xs:complexType name="UserAgentType">
        <xs:complexContent>
 
        <xs:extension base="xs:string">
      
         <xs:attribute name="Type" type="xs:unsignedInt" use="optional"/>
  
        </xs:extension>
 
        </xs:complexContent>
 
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements

None. 
  
    
    

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:unsignedInt |optional |Number describing the user agent. |Values of the xs:unsignedInt type. |
   


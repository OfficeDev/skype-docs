---
title: ResponseCodeType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 5e7155e9-7fb2-4e50-e765-e863d1a60596
---


# ResponseCodeType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|xs:string |
   

## Definition


```XML

      <xs:complexType name="ResponseCodeType">
        <xs:complexContent>
 
        <xs:extension base="xs:string">
      
         <xs:attribute name="Code" type="xs:unsignedShort" use="required"/>
  
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
|Code |xs:unsignedShort |required |SIP error code for this error. |Values of the xs:unsignedShort type. |
   


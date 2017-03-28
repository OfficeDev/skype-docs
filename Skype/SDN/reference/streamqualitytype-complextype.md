---
title: StreamQualityType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 236f8a1c-9dad-1d72-8861-26f2837522f3
---


# StreamQualityType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|xs:string |
   

## Definition


```XML

      <xs:complexType name="StreamQualityType">
        <xs:complexContent>
 
        <xs:extension base="xs:string">
      
         <xs:attribute name="Source" type="xs:string" use="optional"/>
  
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
|Source |xs:string |optional |Source that cause the stream's quality determination. |Values of the xs:string type. |
   


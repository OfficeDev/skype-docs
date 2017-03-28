---
title: Contact element (QualityEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 686ca518-a697-0cb2-9c3e-4263a2a72e1b
---


# Contact element (QualityEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
SIP URI of the user as extracted from the Contact header of the underlying SIP message. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:anyURI |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Contact"  type="xs:anyURI" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [From](from-element-qualitytype-complextype-1.md)| [QualityEndPointType](qualityendpointtype-complextype.md)|The source of the reported media stream. |
| [To](to-element-qualitytype-complextype-1.md)| [QualityEndPointType](qualityendpointtype-complextype.md)|Destination of the media stream. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


---
title: URI element (QualityEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: d7883d40-b740-81a2-9c5e-de1fc66709e1
---


# URI element (QualityEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
SIP URI of the user signed in via the endpoint as extracted from the SIP header.. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:anyURI |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="URI"  type="xs:anyURI">
    
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
  
    
    


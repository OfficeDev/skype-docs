---
title: MediaBypass element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: e7fad1d2-57f1-97b9-fc6e-dca35c59f299
---


# MediaBypass element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Denotes media bypass. It is provided only in QualityUpdate message when mediabypass was part of the call. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:boolean |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="MediaBypass"  type="xs:boolean" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [ConnectionInfo](connectioninfo-element.md)| [ConnectionInfoType](connectioninfotype-complextype.md)|Connection-related properties regardless of the media stream and end points. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


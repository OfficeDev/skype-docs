---
title: ConversationId element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 431cf980-624b-383b-3f70-0771cc30b322
---


# ConversationId element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Identifier to correlate different SIP calls involved in the same conversation. In some cases Skype for Business uses different SIP calls for different modalities. This identifier permits correlating these SIP calls in the same conversation. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="ConversationId"  type="xs:string" minOccurs="0">
    
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
  
    
    


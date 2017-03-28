---
title: CallId element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 92558a4d-2a23-4e5e-6c4a-56e65bdf994f
---


# CallId element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Unique identifier for the SIP call. This field should be used to correlate messages referring to the same call. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="CallId"  type="xs:string">
    
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
  
    
    


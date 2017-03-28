---
title: StartTime element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 52863aeb-ca6e-2940-46ec-652e8064a9e8
---


# StartTime element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Denotes the time when the conversation started. It is provided only in QualityUpdate events. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:dateTime |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="StartTime"  type="xs:dateTime" minOccurs="0">
    
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
  
    
    


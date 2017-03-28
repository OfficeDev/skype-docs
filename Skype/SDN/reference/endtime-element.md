---
title: EndTime element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 74b56124-2d3c-a048-a9da-d7248aa5f4b6
---


# EndTime element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Denotes then time when the conversation ended. It is provided only in QualityUpdate events. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:dateTime |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="EndTime"  type="xs:dateTime" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [ConnectionInfo](connectioninfo-element-1.md)| [ConnectionInfoType](connectioninfotype-complextype-1.md)|Connection-related properties regardless of the media stream and end points. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


---
title: Bye element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: b054de95-f996-b81d-703d-ca0bf9870977
---


# Bye element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Event that a Sip call has ended and all media stream terminated. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [ByeType](byetype-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Bye"  type="ByeType">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [LyncDiagnostics](lyncdiagnostics-element-1.md)| [MessageType](messagetype-complextype-1.md)|The root element for output from the Skype for Business SDN Manager. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [EndPoint](endpoint-element-byetype-complextype-1.md)| [EndPointType](endpointtype-complextype-1.md)|Endpoint involved in the ended SIP call. |
   

### Attributes

None. 
  
    
    


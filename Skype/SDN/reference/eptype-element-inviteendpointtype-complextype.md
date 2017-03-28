---
title: EPType element (InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: ae7b00f8-f3b7-d81b-9260-dffcfc9552ff
---


# EPType element (InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Indicates that this endpoint is of the Skype for Business Room System type or not, when the sendmeetingroominfo option is set to True in the Dialog Listener configeration. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="EPType"  type="xs:string" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Callee](callee-element-1.md)| [InviteEndPointType](inviteendpointtype-complextype.md)|Properties of the callee. |
| [Caller](caller-element.md)| [InviteEndPointType](inviteendpointtype-complextype.md)|Properties of the caller. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


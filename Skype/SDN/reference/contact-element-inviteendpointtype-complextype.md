---
title: Contact element (InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: a4ab18bd-b57f-2249-9d86-b389661cfa1f
---


# Contact element (InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
SIP URI of the user as extracted from the Contact header of the underlying SIP message. This field is obfuscated unless hidepii is set to false in the Dialog Listener configuration file. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:anyURI |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

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
| [Callee](callee-element-1.md)| [InviteEndPointType](inviteendpointtype-complextype.md)|Properties of the callee. |
| [Caller](caller-element.md)| [InviteEndPointType](inviteendpointtype-complextype.md)|Properties of the caller. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


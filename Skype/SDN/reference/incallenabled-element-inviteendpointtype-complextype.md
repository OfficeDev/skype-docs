---
title: IncallEnabled element (InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: e0821fc0-0360-0696-c148-bbded00770cc
---


# IncallEnabled element (InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Whether the endpoint (Skype for Business client) is capable to send incall quality update messages. This flag does not indicate whether the client is configured to send incall QoE reports. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:boolean |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="IncallEnabled"  type="xs:boolean" minOccurs="0">
    
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
  
    
    


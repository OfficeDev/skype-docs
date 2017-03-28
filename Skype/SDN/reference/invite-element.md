---
title: Invite element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 82315508-6a09-f166-b971-a7212826d73c
---


# Invite element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Event that an endpoint attempts to establish a call. DialogListener will include this element in its output if the sendcallinvites entry is set to True (activated) in the DialogListener configuration file. In addition, DialogListener will also notifies any SIP Invite messages (re-invites), not just the first one. Following this message Earlymedia may be flowing but this element is not intended to report on early media streams. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [InviteType](invitetype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Invite"  type="InviteType">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [LyncDiagnostics](lyncdiagnostics-element.md)| [MessageType](messagetype-complextype.md)|The root element for output from the Skype for Business SDN Manager. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Callee](callee-element-1.md)| [InviteEndPointType](inviteendpointtype-complextype.md)|Properties of the callee. |
| [Caller](caller-element.md)| [InviteEndPointType](inviteendpointtype-complextype.md)|Properties of the caller. |
   

### Attributes

None. 
  
    
    


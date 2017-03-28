---
title: RawSDP element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: e26c05ef-e809-02db-70ab-97b552488be0
---


# RawSDP element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Raw Session Description Protocol (SDP) data that is included as the payload of the underlying SIP messages of the Invite, LRSInvite and StartOrUpdate type, if the sendrawsdp entry is set to True in the DialogListener configuration file. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="RawSDP"  type="xs:string" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [LyncDiagnostics](lyncdiagnostics-element-1.md)| [MessageType](messagetype-complextype-1.md)|The root element for output from the Skype for Business SDN Manager. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


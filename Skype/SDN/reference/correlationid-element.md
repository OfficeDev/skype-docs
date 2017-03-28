---
title: CorrelationId element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 5726de4b-5551-06e2-c9ce-c87e2cd91ee8
---


# CorrelationId element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Identifier to correlate two SIP calls where mediation server is involved. Both SIP calls belong to the same conversation. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="CorrelationId"  type="xs:string" minOccurs="0">
    
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
  
    
    


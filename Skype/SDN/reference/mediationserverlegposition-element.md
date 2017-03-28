---
title: MediationServerLegPosition element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 340e2dd8-fec7-5728-2688-3a0bae1aef4b
---


# MediationServerLegPosition element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Indicates whether the call was incoming to a mediation server or outgoing from the medation server. It is provided only in QualityUpdate events. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="MediationServerLegPosition"  type="xs:string" minOccurs="0">
    
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
  
    
    


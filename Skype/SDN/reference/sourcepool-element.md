---
title: SourcePool element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 07174c84-2101-6879-4bc2-0589e3e3be4a
---


# SourcePool element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
(Deprecated - use below Pools instead) Name of the Skype for Business pool this message originated. If a QualityUpdate message is merged and originated from two pools only one is included here. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="SourcePool"  type="xs:string" minOccurs="0">
    
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
  
    
    


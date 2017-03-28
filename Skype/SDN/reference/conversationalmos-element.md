---
title: ConversationalMOS element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 35454d26-8984-465e-3a2b-0ad9c505bbb9
---


# ConversationalMOS element (QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Conversational clarity index for remote party, as described in [ITUP.562] section 6.3. This metric is reported for all available modalities and media types. This field is unused and deprecated for Skype for Business clients 2013 and beyond. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:int |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="ConversationalMOS"  type="xs:int">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Properties](properties-element-qualitytype-complextype-1.md)| [QualityPropertiesType](qualitypropertiestype-complextype.md)|Properties of the media stream, including a selected set of quality metrics reported and thresholds that are used to determine a bad call. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


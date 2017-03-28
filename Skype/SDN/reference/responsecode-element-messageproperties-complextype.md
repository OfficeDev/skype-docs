---
title: ResponseCode element (MessageProperties complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: cef77f6d-f6c7-f1d3-8e6f-1859e1203576
---


# ResponseCode element (MessageProperties complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Message describing the error. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [ResponseCodeType](responsecodetype-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="ResponseCode"  type="ResponseCodeType" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Properties](properties-element-messagetype-complextype-1.md)| [MessageProperties](messageproperties-complextype.md)|Details of the Error or reason for ending the streams. |
   

### Child elements

None. 
  
    
    

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Code |xs:unsignedShort |required |SIP error code for this error. |Values of the xs:unsignedShort type. |
   


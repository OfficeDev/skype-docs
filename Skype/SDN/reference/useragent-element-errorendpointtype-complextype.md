---
title: UserAgent element (ErrorEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 3c027501-8a46-98d4-df03-29d8736db129
---


# UserAgent element (ErrorEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Skype for Business client name and version. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [UserAgentType](useragenttype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="UserAgent"  type="UserAgentType" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [EndPoint](endpoint-element-errortype-complextype-1.md)| [ErrorEndPointType](errorendpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
   

### Child elements

None. 
  
    
    

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:unsignedInt |optional |Number describing the user agent. |Values of the xs:unsignedInt type. |
   


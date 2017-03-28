---
title: UserAgent element (InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 7d15d5b9-2565-f68f-aaef-380e442b7aed
---


# UserAgent element (InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Skype for Business client and version. 
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
| [Callee](callee-element-1.md)| [InviteEndPointType](inviteendpointtype-complextype.md)|Properties of the callee. |
| [Caller](caller-element.md)| [InviteEndPointType](inviteendpointtype-complextype.md)|Properties of the caller. |
   

### Child elements

None. 
  
    
    

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:unsignedInt |optional |Number describing the user agent. |Values of the xs:unsignedInt type. |
   


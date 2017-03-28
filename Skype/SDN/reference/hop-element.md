---
title: Hop element (RouteType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: ef72984e-dc13-8d32-29fe-81700779be45
---


# Hop element (RouteType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
IP address of one hop (router, gateway, switch, etc) on the path from the source to the destination of the media stream. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**|xs:string |
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Hop"  type="xs:string" maxOccurs="unbounded">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Route](route-element-messagetype-complextype.md)| [RouteType](routetype-complextype-1.md)|Network path of the media stream only provided in Skype for Business 2013 and when the traceRoute feature is activated in Skype for Business. |
| [Route](route-element-qualitytype-complextype-1.md)| [RouteType](routetype-complextype-1.md)|Network path of the media stream only provided in Skype for Business 2013 and when the traceRoute feature is activated in Skype for Business. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


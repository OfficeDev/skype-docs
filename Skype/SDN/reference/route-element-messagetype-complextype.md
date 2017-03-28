---
title: Route element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 3c183e98-7d93-0576-908a-19a75f522225
---


# Route element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Network path of the media stream only provided in Skype for Business 2013 and when the traceRoute feature is activated in Skype for Business. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [RouteType](routetype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Route"  type="RouteType" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [LyncDiagnostics](lyncdiagnostics-element-1.md)| [MessageType](messagetype-complextype-1.md)|The root element for output from the Skype for Business SDN Manager. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Hop](hop-element.md)|xs:string |IP address of one hop (router, gateway, switch, etc) on the path from the source to the destination of the media stream. |
   

### Attributes

None. 
  
    
    


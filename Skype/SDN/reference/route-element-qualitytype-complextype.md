---
title: Route element (QualityType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 998c92ca-3df6-99c0-d47b-0dd6e04af190
---


# Route element (QualityType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
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
| [InCallQuality](incallquality-element-1.md)| [QualityType](qualitytype-complextype.md)|Indicates that a significant quality related event occured in the client. Either the quality dropped into another level or improved. There are 3 levels: Good, Poor, Bad. The media stack determines the quality level. Furthermore, this event is also sent when a video stream is deescalated. Even in an issue free network at least one IncallQuality message is sent. |
| [QualityUpdate](qualityupdate-element-1.md)| [QualityType](qualitytype-complextype.md)|Specifies the event that a call has ended and contains a report of the quality metrics of individual media streams. These quality metrics for a stream may include updates provided by both endpoints which are merged. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Hop](hop-element.md)|xs:string |IP address of one hop (router, gateway, switch, etc) on the path from the source to the destination of the media stream. |
   

### Attributes

None. 
  
    
    


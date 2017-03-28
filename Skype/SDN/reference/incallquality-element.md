---
title: InCallQuality element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: a993878c-bd86-461e-4f91-7a326b45d250
---


# InCallQuality element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Indicates that a significant quality related event occured in the client. Either the quality dropped into another level or improved. There are 3 levels: Good, Poor, Bad. The media stack determines the quality level. Furthermore, this event is also sent when a video stream is deescalated. Even in an issue free network at least one IncallQuality message is sent. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [QualityType](qualitytype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="InCallQuality"  type="QualityType">
    
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
| [From](from-element-qualitytype-complextype-1.md)| [QualityEndPointType](qualityendpointtype-complextype.md)|The source of the reported media stream. |
| [Properties](properties-element-qualitytype-complextype.md)| [QualityPropertiesType](qualitypropertiestype-complextype-1.md)|Properties of the media stream, including a selected set of quality metrics reported and thresholds that are used to determine a bad call. |
| [Route](route-element-qualitytype-complextype.md)| [RouteType](routetype-complextype.md)|Network path of the media stream only provided in Skype for Business 2013 and when the traceRoute feature is activated in Skype for Business. |
| [To](to-element-qualitytype-complextype-1.md)| [QualityEndPointType](qualityendpointtype-complextype.md)|Destination of the media stream. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:string |required |Modality or media type for which the quality metrics are reported. The supported options are audio, video and applicationsharing. |Values of the xs:string type. |
   


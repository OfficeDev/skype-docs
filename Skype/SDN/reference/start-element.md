---
title: Start element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 881b61c1-292b-bd10-b35d-bbff7f8675ee
---


# Start element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Event that a media stream is started. Every Start element contains a report about a particular media stream. This event is raised when the call is established, i.e., when the call is picked up and the SIP INVITE is answered with a 200 OK response. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [StartOrUpdateType](startorupdatetype-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Start"  type="StartOrUpdateType">
    
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
| [From](from-element-startorupdatetype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Source of the media stream. |
| [Properties](properties-element-startorupdatetype-complextype-1.md)| [StartPropertiesType](startpropertiestype-complextype.md)|Properties of the started or updated media stream. |
| [To](to-element-startorupdatetype-complextype.md)| [EndPointType](endpointtype-complextype-1.md)|Destination of the media stream. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:string |required |Modality or media type for which the quality metrics are reported. The valid options are audio, video and applicationsharing |Values of the xs:string type. |
   


---
title: Update element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 38b49ac6-609a-a47c-4f7c-e83fa7d363ab
---


# Update element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Event that a media stream previously started has been updated. This event is raised when an update to core parameters of call have changed and the change is established, i.e., when the request is answered with a 200 OK response. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [StartOrUpdateType](startorupdatetype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Update"  type="StartOrUpdateType">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [LyncDiagnostics](lyncdiagnostics-element.md)| [MessageType](messagetype-complextype.md)|The root element for output from the Skype for Business SDN Manager. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [From](from-element-startorupdatetype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Source of the media stream. |
| [Properties](properties-element-startorupdatetype-complextype.md)| [StartPropertiesType](startpropertiestype-complextype-1.md)|Properties of the started or updated media stream. |
| [To](to-element-startorupdatetype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Destination of the media stream. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type |xs:string |required |Modality or media type for which the quality metrics are reported. The valid options are audio, video and applicationsharing |Values of the xs:string type. |
   


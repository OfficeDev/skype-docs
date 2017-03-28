---
title: Error element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: bee513f0-7b1e-01a4-36e7-c513d8bc9c83
---


# Error element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
This event is optional. Error event that a SIP dialog has failed. Error events are also sent for SIP calls that are terminated even before a media stream is started or for failed to be updated. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [ErrorType](errortype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Error"  type="ErrorType">
    
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
| [EndPoint](endpoint-element-errortype-complextype-1.md)| [ErrorEndPointType](errorendpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
| [From](from-element-errortype-complextype.md)| [EndPointType](endpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
| [Properties](properties-element-errortype-complextype.md)| [ErrorProperties](errorproperties-complextype-1.md)|Properties of the Error. |
| [To](to-element-errortype-complextype-1.md)| [EndPointType](endpointtype-complextype.md)|Endpoint involved in the ended SIP call. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Type | [ConnectionModalities](connectionmodalities-simpletype.md)|optional |Modality or media type for which the quality metrics are reported. The supported options are audio, video and applicationsharing. |Values of the ConnectionModalities type. |
   


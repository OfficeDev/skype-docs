---
title: MessageType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 7b0fdb3a-8f1f-9197-e3d6-d6490f10f410
---


# MessageType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="MessageType">
         <xs:attribute name="Version" type="ThisVersion" use="required"/>
  
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Bye](bye-element.md)| [ByeType](byetype-complextype.md)|Event that a Sip call has ended and all media stream terminated. |
| [ConnectionInfo](connectioninfo-element-1.md)| [ConnectionInfoType](connectioninfotype-complextype-1.md)|Connection-related properties regardless of the media stream and end points. |
| [Ended](ended-element-1.md)| [EndedType](endedtype-complextype.md)|Event that a Sip call has ended and all media stream terminated. |
| [Error](error-element-1.md)| [ErrorType](errortype-complextype.md)|This event is optional. Error event that a SIP dialog has failed. Error events are also sent for SIP calls that are terminated even before a media stream is started or for failed to be updated. |
| [InCallQuality](incallquality-element.md)| [QualityType](qualitytype-complextype-1.md)|Indicates that a significant quality related event occured in the client. Either the quality dropped into another level or improved. There are 3 levels: Good, Poor, Bad. The media stack determines the quality level. Furthermore, this event is also sent when a video stream is deescalated. Even in an issue free network at least one IncallQuality message is sent. |
| [Invite](invite-element.md)| [InviteType](invitetype-complextype.md)|Event that an endpoint attempts to establish a call. DialogListener will include this element in its output if the sendcallinvites entry is set to True (activated) in the DialogListener configuration file. In addition, DialogListener will also notifies any SIP Invite messages (re-invites), not just the first one. Following this message Earlymedia may be flowing but this element is not intended to report on early media streams. |
| [Properties](properties-element-messagetype-complextype-1.md)| [MessageProperties](messageproperties-complextype.md)|Details of the Error or reason for ending the streams. |
| [QualityUpdate](qualityupdate-element.md)| [QualityType](qualitytype-complextype-1.md)|Specifies the event that a call has ended and contains a report of the quality metrics of individual media streams. These quality metrics for a stream may include updates provided by both endpoints which are merged. |
| [RawSDP](rawsdp-element.md)|xs:string |Raw Session Description Protocol (SDP) data that is included as the payload of the underlying SIP messages of the Invite, LRSInvite and StartOrUpdate type, if the sendrawsdp entry is set to True in the DialogListener configuration file. |
| [Route](route-element-messagetype-complextype.md)| [RouteType](routetype-complextype-1.md)|Network path of the media stream only provided in Skype for Business 2013 and when the traceRoute feature is activated in Skype for Business. |
| [Start](start-element-1.md)| [StartOrUpdateType](startorupdatetype-complextype.md)|Event that a media stream is started. Every Start element contains a report about a particular media stream. This event is raised when the call is established, i.e., when the call is picked up and the SIP INVITE is answered with a 200 OK response. |
| [Update](update-element-1.md)| [StartOrUpdateType](startorupdatetype-complextype.md)|Event that a media stream previously started has been updated. This event is raised when an update to core parameters of call have changed and the change is established, i.e., when the request is answered with a 200 OK response. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Version | [ThisVersion](thisversion-simpletype.md)|required |Version number of this data structure. It provides a simple distinction between LyncDiagnostics messages formats. LyncDiagnostics message matching to this xsd use Version D. |Values of the ThisVersion type. |
   


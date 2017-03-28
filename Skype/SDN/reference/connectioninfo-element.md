---
title: ConnectionInfo element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: f8d90b19-b999-277a-881b-f50ac080f820
---


# ConnectionInfo element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Connection-related properties regardless of the media stream and end points. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [ConnectionInfoType](connectioninfotype-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="ConnectionInfo"  type="ConnectionInfoType">
    
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
| [CallId](callid-element.md)|xs:string |Unique identifier for the SIP call. This field should be used to correlate messages referring to the same call. |
| [ConferenceId](conferenceid-element.md)|xs:string |Identifier to correlate call legs that belong to the same conference. |
| [ConferenceURI](conferenceuri-element-1.md)|xs:anyURI |(Deprecated - use ConferenceId instead) Sip URI used for the conference. This field is obfuscated unless hidepii is set to false in configuration. |
| [Connectivity](connectivity-element-1.md)| [ConnectivityModalities](connectivitymodalities-simpletype.md)|(Obsolete) The inclusion of Relay Ip/port indicates that a particular endpoint uses a media relay (edge server) and if not access the remote address directly. It is provided only in QualityUpdate events. |
| [ConversationId](conversationid-element-1.md)|xs:string |Identifier to correlate different SIP calls involved in the same conversation. In some cases Skype for Business uses different SIP calls for different modalities. This identifier permits correlating these SIP calls in the same conversation. |
| [CorrelationId](correlationid-element.md)|xs:string |Identifier to correlate two SIP calls where mediation server is involved. Both SIP calls belong to the same conversation. |
| [CSEQ](cseq-element.md)|xs:unsignedInt |Call sequence number as part of SIP standard that needs to be used to filter for unrelated error messages. This field is not provided for QualityUpdates. |
| [EndTime](endtime-element-1.md)|xs:dateTime |Denotes then time when the conversation ended. It is provided only in QualityUpdate events. |
| [FrontEnds](frontends-element.md)|xs:string |Comma separated list of Skype for Business Front-Ends involved in this call. |
| [MediaBypass](mediabypass-element.md)|xs:boolean |Denotes media bypass. It is provided only in QualityUpdate message when mediabypass was part of the call. |
| [MediationServerLegPosition](mediationserverlegposition-element-1.md)|xs:string |Indicates whether the call was incoming to a mediation server or outgoing from the medation server. It is provided only in QualityUpdate events. |
| [Pools](pools-element.md)|xs:string |Comma separated list of Skype for Business Front-End pool names involved in this call. |
| [SourcePool](sourcepool-element-1.md)|xs:string |(Deprecated - use below Pools instead) Name of the Skype for Business pool this message originated. If a QualityUpdate message is merged and originated from two pools only one is included here. |
| [StartTime](starttime-element.md)|xs:dateTime |Denotes the time when the conversation started. It is provided only in QualityUpdate events. |
| [TimeStamp](timestamp-element-1.md)|xs:dateTime |UTC time when the report is processed. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Originator |xs:string |optional |Indicates source endpoint (Endpoint Id) that provided the quality metrics used for this report. It is provided only in QualityUpdate events. |Values of the xs:string type. |
   


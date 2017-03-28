---
title: ConnectionInfoType complexType (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 6a3f33fc-c7df-48b2-872f-f3c39f4a8464
---


# ConnectionInfoType complexType (Skype for Business SDN Interface 2.2, Schema "C")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="ConnectionInfoType">
         <xs:attribute name="Originator" type="xs:string" use="optional"/>
  
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [CallId](callid-element-1.md)|xs:string |Unique identifier for the SIP call. This field should be used to correlate messages referring to the same call. |
| [ConferenceId](conferenceid-element-1.md)|xs:string |Identifier to correlate call legs that belong to the same conference. |
| [ConferenceURI](conferenceuri-element.md)|xs:anyURI |(Deprecated - use ConferenceId instead) Sip URI used for the conference. This field is obfuscated unless hidepii is set to false in configuration. |
| [Connectivity](connectivity-element.md)|xs:string |(Obsolete) The inclusion of Relay Ip/port indicates that a particular endpoint uses a media relay (edge server) and if not access the remote address directly. It is provided only in QualityUpdate events. |
| [ConversationId](conversationid-element.md)|xs:string |Identifier to correlate different SIP calls involved in the same conversation. In some cases Skype for Business uses different SIP calls for different modalities. This identifier permits correlating these SIP calls in the same conversation. |
| [CorrelationId](correlationid-element-1.md)|xs:string |Identifier to correlate two SIP calls where mediation server is involved. Both SIP calls belong to the same conversation. |
| [CSEQ](cseq-element-1.md)|xs:string |Call sequence number as part of SIP standard that needs to be used to filter for unrelated error messages. This field is not provided for QualityUpdates. |
| [EndTime](endtime-element.md)|xs:dateTime |Denotes then time when the conversation ended. It is provided only in QualityUpdate events. |
| [MediaBypass](mediabypass-element-1.md)|xs:boolean |Denotes media bypass. It is provided only in QualityUpdate message when mediabypass was part of the call. |
| [MediationServerLegPosition](mediationserverlegposition-element.md)|xs:string |Indicates whether the call was incoming to a mediation server or outgoing from the medation server. It is provided only in QualityUpdate events. |
| [SourcePool](sourcepool-element.md)|xs:string |Name of the Skype for Business pool this message originated. If a QualityUpdate message is merged and originated from two pools only one is included here. Currently, the FQDN of one sourcepool is provided, expect a comma delimited list in future releases. |
| [StartTime](starttime-element-1.md)|xs:dateTime |Denotes the time when the conversation started. It is provided only in QualityUpdate events. |
| [TimeStamp](timestamp-element.md)|xs:dateTime |UTC time when the report is processed. |
   

### Attributes



|**Attribute**|**Type**|**Required**|**Description**|**Possible values**|
|:-----|:-----|:-----|:-----|:-----|
|Originator |xs:string |optional |Indicates source endpoint (Endpoint Id) that provided the quality metrics used for this report. It is provided only in QualityUpdate events. |Values of the xs:string type. |
   


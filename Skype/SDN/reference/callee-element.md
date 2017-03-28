---
title: Callee element (InviteType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: dadc7a8c-cc56-dbe1-cfa4-4e44797a8e49
---


# Callee element (InviteType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Properties of the callee. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [EndPointType](endpointtype-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Callee"  type="EndPointType">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Invite](invite-element-1.md)| [InviteType](invitetype-complextype-1.md)|Event that an endpoint attempts to establish a call. DialogListener will include this element in its output if the sendcallinvites entry is set to True (activated) in the DialogListener configuration file. In addition, DialogListener will also notifies any SIP Invite messages (re-invites), not just the first one. Following this message Earlymedia may be flowing but this element is not intended to report on early media streams. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Contact](contact-element-endpointtype-complextype-1.md)|xs:anyURI |SIP URI of the user as extracted from the Contact header of the underlying SIP message. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. |
| [EPId](epid-element-endpointtype-complextype.md)|xs:string |Endpoint Id of the endpoint. |
| [EPType](eptype-element-endpointtype-complextype-1.md)|xs:string |Indicates that this endpoint is of the Skype for Business Room System type or not. |
| [Id](id-element-endpointtype-complextype-1.md)|xs:string |Identifier of the endpoint. |
| [IncallEnabled](incallenabled-element.md)|xs:boolean |Whether the endpoint (Skype for Business client) is capable to send incall quality update messages. This flag does not indicate whether the client is configured to send incall QoE reports. |
| [IP](ip-element-endpointtype-complextype-1.md)|xs:string |IP address of the the media stream source or destination. |
| [Port](port-element-endpointtype-complextype.md)|xs:unsignedInt |Port number of the destination or source of the media stream used by this endpoint. |
| [ReflexiveIP](reflexiveip-element-endpointtype-complextype-1.md)|Not defined |IP used outside of the NAT. |
| [ReflexivePort](reflexiveport-element-endpointtype-complextype.md)|Not defined |Port used on the NAT. |
| [Relay](relay-element-endpointtype-complextype.md)|Not defined |IP Address of the first relay used in the media traffic. |
| [RelayPort](relayport-element-endpointtype-complextype-1.md)|Not defined |Port number of the relay. |
| [URI](uri-element-endpointtype-complextype.md)|xs:anyURI |SIP URI of the user signed in via the endpoint as extracted from the SIP header.. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. |
| [UserAgent](useragent-element-endpointtype-complextype-1.md)|xs:string |Skype for Business client name and version. |
   

### Attributes

None. 
  
    
    


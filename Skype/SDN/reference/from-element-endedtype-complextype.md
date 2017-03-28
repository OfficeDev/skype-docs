---
title: From element (EndedType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 9582f59a-c4f3-3f49-de55-09ec9c7e5fe0
---


# From element (EndedType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
Endpoint involved in the ended SIP call. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [EndPointType](endpointtype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="From"  type="EndPointType" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Ended](ended-element-1.md)| [EndedType](endedtype-complextype.md)|Event that a Sip call has ended and all media stream terminated. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Contact](contact-element-endpointtype-complextype.md)|xs:anyURI |SIP URI of the user as extracted from the Contact header of the underlying SIP message. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. |
| [EPId](epid-element-endpointtype-complextype-1.md)|xs:string |Endpoint Id of the endpoint. |
| [EPType](eptype-element-endpointtype-complextype.md)|xs:string |Indicates that this endpoint is of the Skype for Business Room System type or not. |
| [HostIP](hostip-element-endpointtype-complextype.md)|xs:string |IP address of the endpoint's host in the local network. |
| [HostPort](hostport-element-endpointtype-complextype.md)|xs:string |Port used on the endpoint's host. |
| [Id](id-element-endpointtype-complextype.md)|xs:string |Identifier of the endpoint. |
| [IncallEnabled](incallenabled-element-endpointtype-complextype.md)|xs:boolean |Whether the endpoint (Skype for Business client) is capable to send incall quality update messages. This flag does not indicate whether the client is configured to send incall QoE reports. |
| [IP](ip-element-endpointtype-complextype.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP address of the the media stream source or destination. |
| [PAI](pai-element-endpointtype-complextype.md)|xs:string |P-ASSERTED Identity. |
| [Port](port-element-endpointtype-complextype-1.md)| [PortPattern](portpattern-simpletype.md)|Port number of the destination or source of the media stream used by this endpoint. |
| [ReflexiveIP](reflexiveip-element-endpointtype-complextype.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP used outside of the NAT. |
| [ReflexivePort](reflexiveport-element-endpointtype-complextype-1.md)| [PortPattern](portpattern-simpletype.md)|Port used on the NAT. |
| [Relay](relay-element-endpointtype-complextype-1.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP Address of the first relay used in the media traffic. |
| [RelayPort](relayport-element-endpointtype-complextype.md)| [PortPattern](portpattern-simpletype.md)|Port number of the relay. |
| [TenantId](tenantid-element-endpointtype-complextype.md)|xs:string |Identifier for the tenanat that this endpoing belongs to. |
| [Trunk](trunk-element-endpointtype-complextype.md)|xs:string |Identifier for the SipTrunk or other SIP device used to connect to the endpoint. |
| [URI](uri-element-endpointtype-complextype-1.md)|xs:anyURI |SIP URI of the user signed in via the endpoint as extracted from the SIP header.. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. |
| [UserAgent](useragent-element-endpointtype-complextype.md)| [UserAgentType](useragenttype-complextype.md)|Skype for Business client name and version. |
| [Wireless](wireless-element-endpointtype-complextype.md)|xs:boolean |Flag indicating if the endpoint is using a wireless network. |
   

### Attributes

None. 
  
    
    


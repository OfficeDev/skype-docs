---
title: ErrorEndPointType complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 63192471-563a-1baa-1a66-a3b41731abda
---


# ErrorEndPointType complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="ErrorEndPointType">
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Contact](contact-element-errorendpointtype-complextype.md)|xs:anyURI |SIP URI of the user as extracted from the Contact header of the underlying SIP message. This field is obfuscated unless hidepii is set to false in the Dialog Listener configuration file. |
| [EPId](epid-element-errorendpointtype-complextype.md)|xs:string |Endpoint Id of the endpoint. |
| [EPType](eptype-element-errorendpointtype-complextype.md)|xs:string |Indicates that this endpoint is of the Skype for Business Room System type or not. |
| [HostIP](hostip-element-errorendpointtype-complextype.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP address of the endpoint's host in the local network. |
| [HostPort](hostport-element-errorendpointtype-complextype.md)| [PortPattern](portpattern-simpletype.md)|Port used on the endpoint's host. |
| [Id](id-element-errorendpointtype-complextype.md)|xs:string |Identifier of the endpoint. |
| [IncallEnabled](incallenabled-element-errorendpointtype-complextype.md)|xs:boolean |Whether the endpoint (Skype for Business client) is capable to send incall quality update messages. This flag does not indicate whether the client is configured to send incall QoE reports. |
| [IP](ip-element-errorendpointtype-complextype.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP address of the the media stream source or destination. |
| [PAI](pai-element-errorendpointtype-complextype.md)|xs:string |P-ASSERTED Identity. |
| [Port](port-element-errorendpointtype-complextype.md)| [PortPattern](portpattern-simpletype.md)|Port number of the destination or source of the media stream used by this endpoint. |
| [ReflexiveIP](reflexiveip-element-errorendpointtype-complextype.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP used outside of the NAT. |
| [ReflexivePort](reflexiveport-element-errorendpointtype-complextype.md)| [PortPattern](portpattern-simpletype.md)|Port useed on the NAT. |
| [Relay](relay-element-errorendpointtype-complextype.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP Address of the first relay used in the media traffic. |
| [RelayPort](relayport-element-errorendpointtype-complextype.md)| [PortPattern](portpattern-simpletype.md)|Port number of the relay. |
| [TenantId](tenantid-element-errorendpointtype-complextype.md)|xs:string |Identifier for the tenanat that this endpoing belongs to. |
| [Trunk](trunk-element-errorendpointtype-complextype.md)|xs:string |Identifier for the SipTrunk or other SIP device used to connect to the endpoint. |
| [URI](uri-element-errorendpointtype-complextype.md)|xs:anyURI |SIP URI of the user signed in via the endpoint as extracted from the SIP header.. This field is obfuscated unless hidepii is set to false in the Dialog Listener configuration file. |
| [UserAgent](useragent-element-errorendpointtype-complextype.md)| [UserAgentType](useragenttype-complextype.md)|Skype for Business client name and version. |
| [Wireless](wireless-element-errorendpointtype-complextype.md)|xs:boolean |Flag indicating if the endpoint is using a wireless network. |
   

### Attributes

None. 
  
    
    


---
title: EndPointType complexType (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: dadd849f-7969-80b7-9753-5e70029ab597
---


# EndPointType complexType (Skype for Business SDN Interface 2.2, Schema "C")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="EndPointType">
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

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
  
    
    


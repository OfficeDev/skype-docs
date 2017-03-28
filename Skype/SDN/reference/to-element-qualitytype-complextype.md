---
title: To element (QualityType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: d13327fd-b66f-078b-f740-a139fb7d9fe8
---


# To element (QualityType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Destination of the media stream. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [QualityEndPointType](qualityendpointtype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="To"  type="QualityEndPointType">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [InCallQuality](incallquality-element-1.md)| [QualityType](qualitytype-complextype.md)|Indicates that a significant quality related event occured in the client. Either the quality dropped into another level or improved. There are 3 levels: Good, Poor, Bad. The media stack determines the quality level. Furthermore, this event is also sent when a video stream is deescalated. Even in an issue free network at least one IncallQuality message is sent. |
| [QualityUpdate](qualityupdate-element-1.md)| [QualityType](qualitytype-complextype.md)|Specifies the event that a call has ended and contains a report of the quality metrics of individual media streams. These quality metrics for a stream may include updates provided by both endpoints which are merged. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [BSSID](bssid-element-1.md)|Not defined |Id of an access point for a WiFi/wireless connection. |
| [Connection](connection-element.md)|xs:string |Connection type such as "wired" or "wireless". |
| [Contact](contact-element-qualityendpointtype-complextype-1.md)|xs:anyURI |SIP URI of the user as extracted from the Contact header of the underlying SIP message. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. |
| [CPUName](cpuname-element.md)|Not defined |Name of the CPU. |
| [CPUNumberOfCores](cpunumberofcores-element.md)|Not defined |Number of CPU cores in the endpoint device. |
| [CPUProcessorSpeed](cpuprocessorspeed-element.md)|Not defined |Processor speed rating. |
| [DSCPInbound](dscpinbound-element-1.md)|Not defined |QoS category marking when the stream is received on this endpoint. This field is populated only from Skype for Business clients newer than Skype for Business 2013. |
| [DSCPOutbound](dscpoutbound-element-1.md)|Not defined |QoS category marking used on send the stream from this endpoint. This field is populated only from Skype for Business clients newer than Skype for Business 2013. |
| [EPId](epid-element-qualityendpointtype-complextype.md)|xs:string |Endpoint Id of the endpoint. |
| [EPType](eptype-element-qualityendpointtype-complextype-1.md)|xs:string |Indicates that this endpoint is of the Skype for Business Room System type or not. |
| [Id](id-element-qualityendpointtype-complextype-1.md)|xs:string |Identifier of the endpoint. |
| [Inside](inside-element.md)|xs:boolean |(Deprecated - since Skype for Business 2013, this field is not reliable anymore.) Indicates if the source is registered within the enterprise (True) or not (False). |
| [IP](ip-element-qualityendpointtype-complextype.md)|xs:string |IP address of the the media stream source or destination. |
| [LinkSpeed](linkspeed-element.md)|Not defined |Basic bandwidth of the connection. |
| [MACAddr](macaddr-element-1.md)|Not defined |MAC address of the endpoint. |
| [OS](os-element.md)|Not defined |Operating System used on the endpoint device. |
| [Port](port-element-qualityendpointtype-complextype-1.md)|xs:unsignedInt |Port number of the destination or source of the media stream used by this endpoint. |
| [ReflexiveIP](reflexiveip-element-qualityendpointtype-complextype.md)|Not defined |IP used outside of the NAT. |
| [ReflexivePort](reflexiveport-element-qualityendpointtype-complextype.md)|Not defined |Port used on the NAT. |
| [Relay](relay-element-qualityendpointtype-complextype-1.md)|Not defined |IP Address of the first relay used in the media traffic. |
| [RelayPort](relayport-element-qualityendpointtype-complextype.md)|Not defined |Port number of the relay. |
| [URI](uri-element-qualityendpointtype-complextype-1.md)|xs:anyURI |SIP URI of the user signed in via the endpoint as extracted from the SIP header.. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. |
| [UserAgent](useragent-element-qualityendpointtype-complextype-1.md)|Not defined |Skype for Business client and version. |
| [Virtualization](virtualization-element-1.md)|Not defined |Type of virtualization used. |
| [VPN](vpn-element-1.md)|xs:boolean |Indicates if the user is on VPN (True) or not (False). |
| [WifiDriverDeviceDesc](wifidriverdevicedesc-element-1.md)|Not defined |Wifi Driver Device description. |
| [WifiDriverVersion](wifidriverversion-element-1.md)|Not defined |Wifi Driver Version. |
   

### Attributes

None. 
  
    
    


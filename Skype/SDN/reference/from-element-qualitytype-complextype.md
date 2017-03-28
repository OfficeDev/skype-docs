---
title: From element (QualityType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 7fffb598-6e04-3c17-c15d-3961400aded8
---


# From element (QualityType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
The source of the reported media stream. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [QualityEndPointType](qualityendpointtype-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="From"  type="QualityEndPointType">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [InCallQuality](incallquality-element.md)| [QualityType](qualitytype-complextype-1.md)|Indicates that a significant quality related event occured in the client. Either the quality dropped into another level or improved. There are 3 levels: Good, Poor, Bad. The media stack determines the quality level. Furthermore, this event is also sent when a video stream is deescalated. Even in an issue free network at least one IncallQuality message is sent. |
| [QualityUpdate](qualityupdate-element.md)| [QualityType](qualitytype-complextype-1.md)|Specifies the event that a call has ended and contains a report of the quality metrics of individual media streams. These quality metrics for a stream may include updates provided by both endpoints which are merged. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [BSSID](bssid-element.md)| [BSSIDPattern](bssidpattern-simpletype.md)|Id of an access point for a WiFi/wireless connection. |
| [Connection](connection-element-1.md)| [ConnectionPlatforms](connectionplatforms-simpletype.md)|Connection type such as "wired" or "wireless". |
| [Contact](contact-element-qualityendpointtype-complextype.md)|xs:anyURI |SIP URI of the user as extracted from the Contact header of the underlying SIP message. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. |
| [CPUName](cpuname-element-1.md)|xs:string |Name of the CPU. |
| [CPUNumberOfCores](cpunumberofcores-element-1.md)|xs:int |Number of CPU cores in the endpoint device. |
| [CPUProcessorSpeed](cpuprocessorspeed-element-1.md)|xs:string |Processor speed rating. |
| [DSCPInbound](dscpinbound-element.md)|xs:byte |QoS category marking when the stream is received on this endpoint. This field is populated only from Skype for Business clients newer than Skype for Business 2013. |
| [DSCPOutbound](dscpoutbound-element.md)|xs:byte |QoS category marking used on send the stream from this endpoint. This field is populated only from Skype for Business clients newer than Skype for Business 2013. |
| [EPId](epid-element-qualityendpointtype-complextype-1.md)|xs:string |Endpoint Id of the endpoint. |
| [EPType](eptype-element-qualityendpointtype-complextype.md)|xs:string |Indicates that this endpoint is of the Skype for Business Room System type or not. |
| [HostIP](hostip-element-qualityendpointtype-complextype.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP address of the the media stream source or destination. |
| [HostPort](hostport-element-qualityendpointtype-complextype.md)| [PortPattern](portpattern-simpletype.md)|Port number of the destination or source of the media stream used by this endpoint. |
| [Id](id-element-qualityendpointtype-complextype.md)|xs:string |Identifier of the endpoint. |
| [Inside](inside-element-1.md)|xs:boolean |(Deprecated - since Skype for Business 2013, this field is not reliable anymore.) Indicates if the source is registered within the enterprise (True) or not (False). |
| [IP](ip-element-qualityendpointtype-complextype-1.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP address of the the media stream source or destination. |
| [LinkSpeed](linkspeed-element-1.md)|xs:double |Basic bandwidth of the connection. |
| [MACAddr](macaddr-element.md)|xs:string |MAC address of the endpoint. |
| [OS](os-element-1.md)|xs:string |Operating System used on the endpoint device. |
| [PAI](pai-element-qualityendpointtype-complextype.md)|xs:string |P-Asserted Identity. |
| [Port](port-element-qualityendpointtype-complextype.md)| [PortPattern](portpattern-simpletype.md)|Port number of the destination or source of the media stream used by this endpoint. |
| [ReflexiveIP](reflexiveip-element-qualityendpointtype-complextype-1.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP used outside of the NAT. |
| [ReflexivePort](reflexiveport-element-qualityendpointtype-complextype-1.md)| [PortPattern](portpattern-simpletype.md)|Port used on the NAT. |
| [Relay](relay-element-qualityendpointtype-complextype.md)| [ipAddressPattern](ipaddresspattern-simpletype.md)|IP Address of the first relay used in the media traffic. |
| [RelayPort](relayport-element-qualityendpointtype-complextype-1.md)| [PortPattern](portpattern-simpletype.md)|Port number of the relay. |
| [Trunk](trunk-element-qualityendpointtype-complextype.md)|xs:string |Identifier for the SipTrunk or other SIP device used to connect to the endpoint. |
| [URI](uri-element-qualityendpointtype-complextype.md)|xs:anyURI |SIP URI of the user signed in via the endpoint as extracted from the SIP header.. This field is obfuscated unless hidepii is set to false in the DialogListener configuration file. |
| [UserAgent](useragent-element-qualityendpointtype-complextype.md)| [UserAgentType](useragenttype-complextype.md)|Skype for Business client and version. |
| [Virtualization](virtualization-element.md)|xs:string |Type of virtualization used. |
| [VPN](vpn-element.md)|xs:boolean |Indicates if the user is on VPN (True) or not (False). |
| [WifiDriverDeviceDesc](wifidriverdevicedesc-element.md)|xs:string |Wifi Driver Device description. |
| [WifiDriverVersion](wifidriverversion-element.md)|xs:string |Wifi Driver Version. |
   

### Attributes

None. 
  
    
    


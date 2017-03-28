---
title: Properties element (StartOrUpdateType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 02bbebb5-4c55-7fe0-59b7-013bd0697c49
---


# Properties element (StartOrUpdateType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Properties of the started or updated media stream. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [StartPropertiesType](startpropertiestype-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Properties"  type="StartPropertiesType" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Start](start-element.md)| [StartOrUpdateType](startorupdatetype-complextype-1.md)|Event that a media stream is started. Every Start element contains a report about a particular media stream. This event is raised when the call is established, i.e., when the call is picked up and the SIP INVITE is answered with a 200 OK response. |
| [Update](update-element.md)| [StartOrUpdateType](startorupdatetype-complextype-1.md)|Event that a media stream previously started has been updated. This event is raised when an update to core parameters of call have changed and the change is established, i.e., when the request is answered with a 200 OK response. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Bandwidth](bandwidth-element-startpropertiestype-complextype-1.md)| [BandwidthType](bandwidthtype-complextype.md)|Describes the maximum and average amount of bandwidth needed by this stream. It takes the possible codecs and stream multiplexing into account. |
| [Codec](codec-element-startpropertiestype-complextype-1.md)| [CodecType](codectype-complextype.md)|Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling). |
| [Protocol](protocol-element-startpropertiestype-complextype.md)|xs:string |Transmission protocol of the media stream such as TCP or UDP. |
   

### Attributes

None. 
  
    
    


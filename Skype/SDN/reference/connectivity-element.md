---
title: Connectivity element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: 62e7e0ed-a340-b8d9-b42d-4f94f016f513
---


# Connectivity element (ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")
(Obsolete) The inclusion of Relay Ip/port indicates that a particular endpoint uses a media relay (edge server) and if not access the remote address directly. It is provided only in QualityUpdate events. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [ConnectivityModalities](connectivitymodalities-simpletype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
   

## Definition


```XML


    <xs:element name="Connectivity"  type="ConnectivityModalities" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [ConnectionInfo](connectioninfo-element-1.md)| [ConnectionInfoType](connectioninfotype-complextype-1.md)|Connection-related properties regardless of the media stream and end points. |
   

### Child elements

None. 
  
    
    

### Attributes

None. 
  
    
    


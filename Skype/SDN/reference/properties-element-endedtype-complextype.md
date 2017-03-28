---
title: Properties element (EndedType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: f07e9f41-a11c-b61a-ef95-582df82f3f17
---


# Properties element (EndedType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Properties of the Error. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [EndedProperties](endedproperties-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Properties"  type="EndedProperties" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Ended](ended-element.md)| [EndedType](endedtype-complextype-1.md)|Event that a Sip call has ended and all media stream terminated. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [MSDiagnostics](msdiagnostics-element-endedproperties-complextype.md)|xs:string |More info related to the error. |
| [MSDiagnosticsClient](msdiagnosticsclient-element-endedproperties-complextype.md)|xs:string |Info about the error related to and reported by the client. |
| [MSDiagnosticsPublic](msdiagnosticspublic-element-endedproperties-complextype.md)|xs:string |Public info about the error. |
   

### Attributes

None. 
  
    
    


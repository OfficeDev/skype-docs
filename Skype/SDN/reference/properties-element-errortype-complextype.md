---
title: Properties element (ErrorType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: b8dc6e6d-114d-55db-a16c-0a41628f0757
---


# Properties element (ErrorType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Properties of the Error. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [ErrorProperties](errorproperties-complextype.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Properties"  type="ErrorProperties" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [Error](error-element.md)| [ErrorType](errortype-complextype-1.md)|This event is optional. Error event that a SIP dialog has failed. Error events are also sent for SIP calls that are terminated even before a media stream is started or for failed to be updated. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [MSDiagnostics](msdiagnostics-element-errorproperties-complextype.md)|xs:string |More info related to the error. |
| [MSDiagnosticsClient](msdiagnosticsclient-element-errorproperties-complextype.md)|xs:string |Info about the error related to and reported by the client. |
| [MSDiagnosticsPublic](msdiagnosticspublic-element-errorproperties-complextype.md)|xs:string |Public info about the error. |
| [ResponseCode](responsecode-element-errorproperties-complextype-1.md)|xs:int |SIP Error code. |
| [ResponsePhrase](responsephrase-element-1.md)|xs:string |More info related to the error. |
   

### Attributes

None. 
  
    
    


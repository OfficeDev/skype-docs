---
title: ErrorProperties complexType (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 8c50d36b-2f48-3c80-669b-f58c6e405ec7
---


# ErrorProperties complexType (Skype for Business SDN Interface 2.2, Schema "C")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="ErrorProperties">
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

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
  
    
    


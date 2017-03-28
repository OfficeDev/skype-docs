---
title: MessageProperties complexType (Skype for Business SDN Interface 2.2, Schema "D")
ms.prod: SKYPE
ms.assetid: f7c0c752-ecdc-55bc-dfe0-06998ec1e06f
---


# MessageProperties complexType (Skype for Business SDN Interface 2.2, Schema "D")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.D.XSD |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="MessageProperties">
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [MSDiagnostics](msdiagnostics-element-messageproperties-complextype-1.md)|xs:string |Skype for Business-specific diagnostics message. |
| [MSDiagnosticsClient](msdiagnosticsclient-element-messageproperties-complextype-1.md)|xs:string |Skype for Business-specific diagnostics message from the client. |
| [MSDiagnosticsPublic](msdiagnosticspublic-element-messageproperties-complextype.md)|xs:string |Skype for Business-specific public diagnostics message. |
| [ResponseCode](responsecode-element-messageproperties-complextype-1.md)| [ResponseCodeType](responsecodetype-complextype-1.md)|Message describing the error. |
   

### Attributes

None. 
  
    
    


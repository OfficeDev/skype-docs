---
title: Properties element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: 64e8b8be-9078-487d-1470-449b609feb5e
---


# Properties element (MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")
Details of the Error or reason for ending the streams. 
 **Last modified:** October 07, 2015
  
    
    


## Element information


|||
|:-----|:-----|
|**Element type**| [MessageProperties](messageproperties-complextype-1.md)|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
   

## Definition


```XML


    <xs:element name="Properties"  type="MessageProperties" minOccurs="0">
    
    </xs:element>
  
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Parent elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [LyncDiagnostics](lyncdiagnostics-element-1.md)| [MessageType](messagetype-complextype-1.md)|The root element for output from the Skype for Business SDN Manager. |
   

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [MSDiagnostics](msdiagnostics-element-messageproperties-complextype.md)|xs:string |Skype for Business-specific diagnostics message. |
| [MSDiagnosticsClient](msdiagnosticsclient-element-messageproperties-complextype.md)|Not defined |Skype for Business-specific diagnostics message from the client. |
| [MSDiagnosticsPublic](msdiagnosticspublic-element-messageproperties-complextype-1.md)|Not defined |Skype for Business-specific public diagnostics message. |
| [ResponseCode](responsecode-element-messageproperties-complextype.md)| [ResponseCodeType](responsecodetype-complextype.md)|Message describing the error. |
   

### Attributes

None. 
  
    
    


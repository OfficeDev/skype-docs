---
title: ErrorProperties complexType Schema D
TOCTitle: ErrorProperties complexType
ms:assetid: 42d4e207-b8e1-5eee-7828-d719b0970384
ms:mtpsurl: https://msdn.microsoft.com/library/Mt171062(v=office.16)
ms:contentKeyID: 65855635
description: Provides type information and Elements.
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# ErrorProperties complexType (Schema D)

(Skype for Business SDN Interface 2.2, Schema "D")


**In this article**  
Type information  
Definition  
Elements and attributes  

## Type information

<table>
<tbody>
<tr class="odd">
<td><p><strong>Namespace</strong></p></td>
<td><p></p></td>
</tr>
<tr class="even">
<td><p><strong>Schema file</strong></p></td>
<td><p>SDNInterface.Schema.D.XSD</p></td>
</tr>
<tr class="odd">
<td><p><strong>Extension base</strong></p></td>
<td><p>None</p></td>
</tr>
</tbody>
</table>


## Definition

```xml
      <xs:complexType name="ErrorProperties">
      </xs:complexType>
      
```

## Elements and attributes

### Child elements

<table>
<thead>
<tr class="header">
<th><p>Element</p></th>
<th><p>Type</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="msdiagnostics-element-errorproperties-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">MSDiagnostics</a></p></td>
<td><p>xs:string</p></td>
<td><p>More info related to the error.</p></td>
</tr>
<tr class="even">
<td><p><a href="msdiagnosticsclient-element-errorproperties-complextype-skype-sdn-2-2-d.md">MSDiagnosticsClient</a></p></td>
<td><p>xs:string</p></td>
<td><p>Info about the error related to and reported by the client.</p></td>
</tr>
<tr class="odd">
<td><p><a href="msdiagnosticspublic-element-errorproperties-complextype-skype-sdn-2-2-d.md">MSDiagnosticsPublic</a></p></td>
<td><p>xs:string</p></td>
<td><p>Public info about the error.</p></td>
</tr>
<tr class="even">
<td><p><a href="responsecode-element-errorproperties-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">ResponseCode</a></p></td>
<td><p>xs:int</p></td>
<td><p>SIP Error code.</p></td>
</tr>
<tr class="odd">
<td><p><a href="responsephrase-element-errorproperties-complextype-skype-sdn-2-2-d.md">ResponsePhrase</a></p></td>
<td><p>xs:string</p></td>
<td><p>More info related to the error.</p></td>
</tr>
</tbody>
</table>


### Attributes

None.


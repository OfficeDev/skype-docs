---
title: ErrorType complexType (Schema C)
TOCTitle: ErrorType complexType
ms:assetid: bce95c26-6495-9562-4b1f-985f8d0d77bd
ms:mtpsurl: https://msdn.microsoft.com/library/Mt429354(v=office.16)
ms:contentKeyID: 68250796
description: Provides type information, definitions, and elements.
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# ErrorType complexType Schema C

(Skype for Business SDN Interface 2.2, Schema "C")

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
<td><p>SDNInterface.Schema.C.XSD</p></td>
</tr>
<tr class="odd">
<td><p><strong>Extension base</strong></p></td>
<td><p>None</p></td>
</tr>
</tbody>
</table>


## Definition

```xml
      <xs:complexType name="ErrorType">
         <xs:attribute name="Type" type="xs:string" use="optional"/>
  
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
<td><p><a href="endpoint-element-errortype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">EndPoint</a></p></td>
<td><p><a href="endpointtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">EndPointType</a></p></td>
<td><p>Endpoint involved in the ended SIP call.</p></td>
</tr>
<tr class="even">
<td><p><a href="from-element-errortype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">From</a></p></td>
<td><p><a href="endpointtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">EndPointType</a></p></td>
<td><p>Endpoint involved in the ended SIP call.</p></td>
</tr>
<tr class="odd">
<td><p><a href="properties-element-errortype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Properties</a></p></td>
<td><p><a href="errorproperties-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">ErrorProperties</a></p></td>
<td><p>Properties of the Error.</p></td>
</tr>
<tr class="even">
<td><p><a href="to-element-errortype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">To</a></p></td>
<td><p><a href="endpointtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">EndPointType</a></p></td>
<td><p>Endpoint involved in the ended SIP call.</p></td>
</tr>
</tbody>
</table>


### Attributes

<table>
<thead>
<tr class="header">
<th><p>Attribute</p></th>
<th><p>Type</p></th>
<th><p>Required</p></th>
<th><p>Description</p></th>
<th><p>Possible values</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Type</p></td>
<td><p>xs:string</p></td>
<td><p>optional</p></td>
<td><p>Modality or media type for which the quality metrics are reported. The supported options are audio, video and applicationsharing.</p></td>
<td><p>Values of the xs:string type.</p></td>
</tr>
</tbody>
</table>


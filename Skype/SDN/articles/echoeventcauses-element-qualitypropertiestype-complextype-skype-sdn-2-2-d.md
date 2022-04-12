---
title: EchoEventCauses element, QualityPropertiesType complexType
TOCTitle: EchoEventCauses element
description: Information about the EchoEventCauses element.
ms:assetid: d4ef7e39-31c8-333a-ed96-7d441f12d1aa
ms:mtpsurl: https://msdn.microsoft.com/library/Mt170845(v=office.16)
ms:contentKeyID: 65855422
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# EchoEventCauses element, QualityPropertiesType complexType

(Skype for Business SDN Interface 2.2, Schema "D")

Reasons of device echo detection and reported for audio streams when available. The causes are coded with the following bit flags: "0x01" - Sample timestamps from capture or render device were poor quality. "0x04" - High level of echo remained after echo cancellation. "0x10" - Signal from capture device had significant instances of maximum signal level.


**In this article**  
Element information  
Definition  
Elements and attributes  

## Element information

<table>
<colgroup>
<col>
<col>
</colgroup>
<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p>xs:unsignedInt</p></td>
</tr>
<tr class="even">
<td><p><strong>Namespace</strong></p></td>
<td><p></p></td>
</tr>
<tr class="odd">
<td><p><strong>Schema file</strong></p></td>
<td><p>SDNInterface.Schema.D.XSD</p></td>
</tr>
</tbody>
</table>


## Definition

```xml

    <xs:element name="EchoEventCauses"  type="xs:unsignedInt">
    
    </xs:element>
  
```

## Elements and attributes

### Parent elements

<table>
<colgroup>
<col>
<col>
<col>
</colgroup>
<thead>
<tr class="header">
<th><p>Element</p></th>
<th><p>Type</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="properties-element-qualitytype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">Properties</a></p></td>
<td><p><a href="qualitypropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">QualityPropertiesType</a></p></td>
<td><p>Properties of the media stream, including a selected set of quality metrics reported and thresholds that are used to determine a bad call.</p></td>
</tr>
</tbody>
</table>


### Child elements

None.

### Attributes

None.


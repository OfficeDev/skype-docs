---
title: EchoPercentMicIn element, QualityPropertiesType complexType
TOCTitle: EchoPercentMicIn element
description: The EchoPercentMicIn element contains the percentage of time when echo is detected in the audio from the capture or microphone device prior to echo cancellation.
ms:assetid: 3a042531-44a8-a8cf-61f6-5cbb384b642e
ms:mtpsurl: https://msdn.microsoft.com/library/Mt170844(v=office.16)
ms:contentKeyID: 65855420
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# EchoPercentMicIn element, QualityPropertiesType complexType

(Skype for Business SDN Interface 2.2, Schema "D")

Percentage of time when echo is detected in the audio from the capture or microphone device prior to echo cancellation. This metric is reported for audio streams when available.


**In this article**  
Element information  
Definition  
Elements and attributes  

## Element information

<table>
<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="doublebetween0and100-simpletype-skype-for-business-sdn-interface-2-2-schema-d.md">DoubleBetween0And100</a></p></td>
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

    <xs:element name="EchoPercentMicIn"  type="DoubleBetween0And100">
    
    </xs:element>
  
```

## Elements and attributes

### Parent elements

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


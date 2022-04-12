---
title: RoundTripMax element, QualityPropertiesType complexType, schema D
TOCTitle: RoundTripMax element
description: Information about the RoundTripMax element (QualityPropertiesType complexType)
ms:assetid: a66cc5d2-950b-cc8f-8fdf-5bb1d16482f3
ms:mtpsurl: https://msdn.microsoft.com/library/Mt170990(v=office.16)
ms:contentKeyID: 65855564
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# RoundTripMax element, QualityPropertiesType complexType, schema D

(Skype for Business SDN Interface 2.2, Schema "D")

Maximum network propagation round-trip time as specified in \[RFC3550\] section 6.4.1. This metric is reported for all modalities/media types when available. (ms)


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

    <xs:element name="RoundTripMax"  type="xs:unsignedInt">
    
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


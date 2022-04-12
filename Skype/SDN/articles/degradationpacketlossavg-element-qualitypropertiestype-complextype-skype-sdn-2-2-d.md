---
title: DegradationPacketLossAvg element (QualityPropertiesType complexType) Schema D
TOCTitle: DegradationPacketLossAvg element
description: DegradationPacketLossAvg element, QualityPropertiesType complexType, and Skype for Business SDN Interface 2.2.
ms:assetid: 8bc824af-dc45-1c2b-7aff-65519254dbfd
ms:mtpsurl: https://msdn.microsoft.com/library/Mt149471(v=office.16)
ms:contentKeyID: 65855417
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# DegradationPacketLossAvg element (Schema D)

(QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

Average fraction of the DegradationAvg that was caused by packet loss. This metric is reported for audio streams when available.

## Element information

<table>
<colgroup>
</colgroup>
<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="nonnegativedouble-simpletype-skype-for-business-sdn-interface-2-2-schema-d.md">NonNegativeDouble</a></p></td>
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

    <xs:element name="DegradationPacketLossAvg"  type="NonNegativeDouble">
    
    </xs:element>
  
```

## Elements and attributes

### Parent elements

<table>
<colgroup>
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


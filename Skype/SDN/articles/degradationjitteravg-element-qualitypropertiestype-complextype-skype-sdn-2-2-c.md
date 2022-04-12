---
title: DegradationJitterAvg element (QualityPropertiesType complexType) 
TOCTitle: DegradationJitterAvg element
ms:assetid: d53411a8-54d9-05a4-0417-cb22a9af26be
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404738(v=office.16)
ms:contentKeyID: 68250651
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# DegradationJitterAvg element 

(QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Average fraction of the degradation jitter average applies to inter-arrival packet jitter. This metric is reported for audio streams when available. 

## Element information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p>xs:string</p></td>
</tr>
<tr class="even">
<td><p><strong>Namespace</strong></p></td>
<td><p></p></td>
</tr>
<tr class="odd">
<td><p><strong>Schema file</strong></p></td>
<td><p>SDNInterface.Schema.C.XSD</p></td>
</tr>
</tbody>
</table>


## Definition

```xml

    <xs:element name="DegradationJitterAvg"  type="xs:string">
    
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
<td><p><a href="properties-element-qualitytype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Properties</a></p></td>
<td><p><a href="qualitypropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">QualityPropertiesType</a></p></td>
<td><p>Properties of the media stream, including a selected set of quality metrics reported and thresholds that are used to determine a bad call.</p></td>
</tr>
</tbody>
</table>


### Child elements

None.

### Attributes

None.


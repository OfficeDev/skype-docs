---
title: VideoFrameLossRate element, QualityPropertiesType complexType, schema D 
TOCTitle: VideoFrameLossRate element
description: Average fraction of frames lost on the video receiver side as computed over the duration of the session.
ms:assetid: 1af084d1-4ddf-d7d3-e816-4668be3d737f
ms:mtpsurl: https://msdn.microsoft.com/library/Mt171033(v=office.16)
ms:contentKeyID: 65855606
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# VideoFrameLossRate element, QualityPropertiesType complexType, schema D

(Skype for Business SDN Interface 2.2, Schema "D")

Average fraction of frames lost on the video receiver side as computed over the duration of the session. This metric is reported for video streams when available. (frames/s)


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

    <xs:element name="VideoFrameLossRate"  type="NonNegativeDouble">
    
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


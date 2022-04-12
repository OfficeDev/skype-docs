---
title: DeviceCaptureNotFunctioningEventRatio element (QualityPropertiesType complexType) Schema D
TOCTitle: DeviceCaptureNotFunctioningEventRatio element
description: DeviceCaptureNotFunctioningEventRatio element (Schema D), QualityPropertiesType complexType, and Skype for Business SDN Interface 2.2.
ms:assetid: 754049e9-36da-5278-7bdf-ab73350a73f6
ms:mtpsurl: https://msdn.microsoft.com/library/Mt149467(v=office.16)
ms:contentKeyID: 65855414
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# DeviceCaptureNotFunctioningEventRatio element (Schema D)

(QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

Percentage of sessions the DeviceCaptureNotFunctioning event was fired when the capture device currently being used for the session is not functioning correctly and, possibly, preventing one-way audio from working correctly. 

## Element information

<table>
<colgroup>
</colgroup>
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

    <xs:element name="DeviceCaptureNotFunctioningEventRatio"  type="DoubleBetween0And100">
    
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


---
title: Learn about DeviceClippingEventRatio element Schema D
description: Percentage of sessions the DeviceClipping event was fired when a speaker clips the microphone, causing the remote listener receives clipping-induced distortions.
TOCTitle: DeviceClippingEventRatio element
ms:assetid: 5c19d36b-dbbd-442c-4dd3-a7d00e9c8109
ms:mtpsurl: https://msdn.microsoft.com/library/Mt149468(v=office.16)
ms:contentKeyID: 65855415
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# DeviceClippingEventRatio element 

(QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

Percentage of sessions the DeviceClipping event was fired when a speaker clips the microphone, causing the remote listener receives clipping-induced distortions. It is important to avoid the microphone clipping.

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

    <xs:element name="DeviceClippingEventRatio"  type="DoubleBetween0And100">
    
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


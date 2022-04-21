---
title: FrameRate element (QualityPropertiesType complexType) (Schema D)
TOCTitle: FrameRate element
ms:assetid: fe6e2a77-2f44-2eab-25f1-f256bbafc84e
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404764(v=office.16)
ms:contentKeyID: 68250710
description: Average frame rate (in frames per second). When available, this metric is only reported for application sharing streams and only for Skype for Business 2013.
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# FrameRate element Schema D

(QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Average frame rate (in frames per second). When available, this metric is only reported for application sharing streams and only for Skype for Business 2013. (frames/s)

**In this article**  
Element information  
Definition  
Elements and attributes  

## Element information

<table>
<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p>xs:decimal</p></td>
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

    <xs:element name="FrameRate"  type="xs:decimal">
    
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


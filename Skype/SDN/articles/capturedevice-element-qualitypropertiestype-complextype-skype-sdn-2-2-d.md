---
title: CaptureDevice element (QualityPropertiesType complexType) 
TOCTitle: CaptureDevice element
ms:assetid: 8cd9db98-fec4-f564-7e30-68c8db2605af
ms:mtpsurl: https://msdn.microsoft.com/library/Mt149442(v=office.16)
ms:contentKeyID: 65855391
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# CaptureDevice element 

(QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

The name of a capture device used to produce the media of this stream. This device is in the FROM endpoint and usually represents a microphone.

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
<td><p>SDNInterface.Schema.D.XSD</p></td>
</tr>
</tbody>
</table>


## Definition

```xml

    <xs:element name="CaptureDevice"  type="xs:string">
    
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


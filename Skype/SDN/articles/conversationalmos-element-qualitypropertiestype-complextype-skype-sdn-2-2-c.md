---
title: ConversationalMOS element (QualityPropertiesType complexType) 
TOCTitle: ConversationalMOS element
ms:assetid: 256a91f0-d0e7-fe61-5d3e-de8e7d8ec75f
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404729(v=office.16)
ms:contentKeyID: 68250642
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# ConversationalMOS element 

(QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Conversational clarity index for remote party, as described in \[ITUP.562\] section 6.3. This metric is reported for all available modalities and media types. This field is unused and deprecated for Skype for Business clients 2013 and beyond. 

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

    <xs:element name="ConversationalMOS"  type="xs:string">
    
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


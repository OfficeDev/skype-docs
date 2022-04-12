---
title: Bandwidth element (CodecType complexType) 
TOCTitle: Bandwidth element (CodecType complexType)
ms:assetid: 2a15bb8d-65ae-90aa-7153-f46d763a81ce
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404701(v=office.16)
ms:contentKeyID: 68250617
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# Bandwidth element 

(CodecType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Average estimated bandwidth.
 

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

    <xs:element name="Bandwidth"  type="xs:string" minOccurs="0">
    
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
<td><p><a href="codec-element-qualitypropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Codec</a></p></td>
<td><p><a href="codectype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">CodecType</a></p></td>
<td><p>Describes the last codec used for the media.</p></td>
</tr>
<tr class="even">
<td><p><a href="codec-element-startpropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Codec</a></p></td>
<td><p><a href="codectype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">CodecType</a></p></td>
<td><p>Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling).</p></td>
</tr>
</tbody>
</table>


### Child elements

None.

### Attributes

None.


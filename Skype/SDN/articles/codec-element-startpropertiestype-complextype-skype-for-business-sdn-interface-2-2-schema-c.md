---
title: Codec element (StartPropertiesType complexType) 
TOCTitle: Codec element (StartPropertiesType complexType)
ms:assetid: 580d5efd-e174-cc1d-9992-15d996f7cfe3
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404719(v=office.16)
ms:contentKeyID: 68250632
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# Codec element 

(StartPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling).

## Element information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="codectype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">CodecType</a></p></td>
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

    <xs:element name="Codec"  type="CodecType" maxOccurs="unbounded" minOccurs="0">
    
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
<td><p><a href="properties-element-startorupdatetype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Properties</a></p></td>
<td><p><a href="startpropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">StartPropertiesType</a></p></td>
<td><p>Properties of the started or updated media stream.</p></td>
</tr>
</tbody>
</table>


### Child elements

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
<td><p><a href="bandwidth-element-codectype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Bandwidth</a></p></td>
<td><p>xs:string</p></td>
<td><p>Average estimated bandwidth.</p></td>
</tr>
<tr class="even">
<td><p><a href="maxbandwidth-element-codectype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">MaxBandwidth</a></p></td>
<td><p>xs:string</p></td>
<td><p>Upper limit of the estimated bandwidth.</p></td>
</tr>
</tbody>
</table>


### Attributes

<table>

<thead>
<tr class="header">
<th><p>Attribute</p></th>
<th><p>Type</p></th>
<th><p>Required</p></th>
<th><p>Description</p></th>
<th><p>Possible values</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Name</p></td>
<td><p>xs:string</p></td>
<td><p>required</p></td>
<td><p>Name of the standard SIP codec used for the media stream.</p></td>
<td><p>Values of the xs:string type.</p></td>
</tr>
</tbody>
</table>


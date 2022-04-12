---
title: Bandwidth element (StartPropertiesType complexType) 
TOCTitle: Bandwidth element (StartPropertiesType complexType)
ms:assetid: c8eb0297-a57c-0f7e-1d2a-c3c37e8cae5e
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404706(v=office.16)
ms:contentKeyID: 68250980
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# Bandwidth element 

(StartPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Describes the maximum and average amount of bandwidth needed by this stream. It takes the possible codecs and stream multiplexing into account.
 

## Element information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="bandwidthtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">BandwidthType</a></p></td>
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

    <xs:element name="Bandwidth"  type="BandwidthType" minOccurs="0">
    
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
<td><p><a href="average-element-bandwidthtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Average</a></p></td>
<td><p>xs:long</p></td>
<td><p>Estimated average amount of the bandwidth.</p></td>
</tr>
<tr class="even">
<td><p><a href="maximum-element-bandwidthtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Maximum</a></p></td>
<td><p>xs:long</p></td>
<td><p>Estimated upper limit of the bandwidth needed by this stream.</p></td>
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
<td><p>Multiplexed</p></td>
<td><p>xs:int</p></td>
<td><p>optional</p></td>
<td><p>Number of times this stream is multiplexed (if &gt; 1). This might mean the overall bandwidth requirement could be up to as many times.</p></td>
<td><p>Values of the xs:int type.</p></td>
</tr>
</tbody>
</table>


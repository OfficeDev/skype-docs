---
title: BandwidthType complexType 
TOCTitle: BandwidthType complexType
ms:assetid: a1e91fc5-070b-be19-ee72-96940ee01137
ms:mtpsurl: https://msdn.microsoft.com/library/Mt171054(v=office.16)
ms:contentKeyID: 65855627
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# BandwidthType complexType 

(Skype for Business SDN Interface 2.2, Schema "D")
 

## Type information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Namespace</strong></p></td>
<td><p></p></td>
</tr>
<tr class="even">
<td><p><strong>Schema file</strong></p></td>
<td><p>SDNInterface.Schema.D.XSD</p></td>
</tr>
<tr class="odd">
<td><p><strong>Extension base</strong></p></td>
<td><p>None</p></td>
</tr>
</tbody>
</table>


## Definition

```xml
      <xs:complexType name="BandwidthType">
         <xs:attribute name="Multiplexed" type="xs:int" use="optional"/>
  
      </xs:complexType>
      
```

## Elements and attributes

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
<td><p><a href="average-element-bandwidthtype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">Average</a></p></td>
<td><p>xs:long</p></td>
<td><p>Estimated average amount of the bandwidth.</p></td>
</tr>
<tr class="even">
<td><p><a href="maximum-element-bandwidthtype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">Maximum</a></p></td>
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


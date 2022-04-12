---
title: CodecType complexType 
TOCTitle: CodecType complexType
ms:assetid: 890c900f-33ef-f8ad-f909-73824c8126e5
ms:mtpsurl: https://msdn.microsoft.com/library/Mt171057(v=office.16)
ms:contentKeyID: 65855630
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# CodecType complexType

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
      <xs:complexType name="CodecType">
         <xs:attribute name="Name" type="xs:string" use="required"/>
  
         <xs:attribute name="PayLoadType" type="xs:int" use="optional"/>
  
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
<td><p><a href="bandwidth-element-codectype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">Bandwidth</a></p></td>
<td><p><a href="bandwidthdatatype-simpletype-skype-for-business-sdn-interface-2-2-schema-d.md">BandwidthDataType</a></p></td>
<td><p>Average estimated bandwidth.</p></td>
</tr>
<tr class="even">
<td><p><a href="maxbandwidth-element-codectype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">MaxBandwidth</a></p></td>
<td><p><a href="bandwidthdatatype-simpletype-skype-for-business-sdn-interface-2-2-schema-d.md">BandwidthDataType</a></p></td>
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
<tr class="even">
<td><p>PayLoadType</p></td>
<td><p>xs:int</p></td>
<td><p>optional</p></td>
<td><p>Payload type used when this codec is active.</p></td>
<td><p>Values of the xs:int type.</p></td>
</tr>
</tbody>
</table>


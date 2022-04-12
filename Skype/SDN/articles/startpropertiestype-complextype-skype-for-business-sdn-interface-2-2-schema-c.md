---
title: StartPropertiesType complexType 
TOCTitle: StartPropertiesType complexType
ms:assetid: 940a26b6-b0d5-28bf-416b-ff5109f1027d
ms:mtpsurl: https://msdn.microsoft.com/library/Mt429363(v=office.16)
ms:contentKeyID: 68250805
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# StartPropertiesType complexType 

(Skype for Business SDN Interface 2.2, Schema "C")

**In this article**  
Type information  
Definition  
Elements and attributes  

## Type information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Namespace</strong></p></td>
<td><p></p></td>
</tr>
<tr class="even">
<td><p><strong>Schema file</strong></p></td>
<td><p>SDNInterface.Schema.C.XSD</p></td>
</tr>
<tr class="odd">
<td><p><strong>Extension base</strong></p></td>
<td><p>None</p></td>
</tr>
</tbody>
</table>


## Definition

```xml
      <xs:complexType name="StartPropertiesType">
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
<td><p><a href="bandwidth-element-startpropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Bandwidth</a></p></td>
<td><p><a href="bandwidthtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">BandwidthType</a></p></td>
<td><p>Describes the maximum and average amount of bandwidth needed by this stream. It takes the possible codecs and stream multiplexing into account.</p></td>
</tr>
<tr class="even">
<td><p><a href="codec-element-startpropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Codec</a></p></td>
<td><p><a href="codectype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">CodecType</a></p></td>
<td><p>Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling).</p></td>
</tr>
<tr class="odd">
<td><p><a href="protocol-element-startpropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Protocol</a></p></td>
<td><p>xs:string</p></td>
<td><p>Transmission protocol of the media stream such as TCP or UDP.</p></td>
</tr>
</tbody>
</table>


### Attributes

None.


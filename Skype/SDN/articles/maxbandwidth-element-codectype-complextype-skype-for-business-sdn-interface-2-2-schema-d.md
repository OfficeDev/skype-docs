---
title: MaxBandwidth element (CodecType complexType) 
TOCTitle: MaxBandwidth element
ms:assetid: 2c37df4e-39d9-abbc-4b1a-4e9098947437
ms:mtpsurl: https://msdn.microsoft.com/library/Mt170914(v=office.16)
ms:contentKeyID: 65855489
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# MaxBandwidth element 

(CodecType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

Upper limit of the estimated bandwidth.


**In this article**  
Element information  
Definition  
Elements and attributes  

## Element information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="bandwidthdatatype-simpletype-skype-for-business-sdn-interface-2-2-schema-d.md">BandwidthDataType</a></p></td>
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

    <xs:element name="MaxBandwidth"  type="BandwidthDataType" minOccurs="0">
    
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
<td><p><a href="codec-element-qualitypropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">Codec</a></p></td>
<td><p><a href="codectype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">CodecType</a></p></td>
<td><p>Describes the last codec used for the media.</p></td>
</tr>
<tr class="even">
<td><p><a href="codec-element-startpropertiestype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">Codec</a></p></td>
<td><p><a href="codectype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">CodecType</a></p></td>
<td><p>Codec and estimates for the bandwidth that the codecs will use. This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to switch among these codecs at any time (without additional signalling).</p></td>
</tr>
</tbody>
</table>


### Child elements

None.

### Attributes

None.


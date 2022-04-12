---
title: Codec element (QualityPropertiesType complexType) 
TOCTitle: Codec element (QualityPropertiesType complexType)
ms:assetid: 80291521-b022-7aee-8e35-88d370287c31
ms:mtpsurl: https://msdn.microsoft.com/library/Mt149445(v=office.16)
ms:contentKeyID: 65855392
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# Codec element 

(QualityPropertiesType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

Describes the last codec used for the media. 

## Element information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="codectype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">CodecType</a></p></td>
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

    <xs:element name="Codec"  type="CodecType">
    
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


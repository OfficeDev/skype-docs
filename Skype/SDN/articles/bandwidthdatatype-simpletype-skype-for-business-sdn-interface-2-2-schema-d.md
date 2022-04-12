---
title: BandwidthDataType simpleType 
TOCTitle: BandwidthDataType simpleType
ms:assetid: 58c03120-8c23-de41-406d-4e5f74129b1b
ms:mtpsurl: https://msdn.microsoft.com/library/Mt171043(v=office.16)
ms:contentKeyID: 65855618
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# BandwidthDataType simpleType 

(Skype for Business SDN Interface 2.2, Schema "D")


## Type information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Base type</strong></p></td>
<td><p>xs:int</p></td>
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
    <xs:simpleType  name="BandwidthDataType">
    
      <xs:restriction base="xs:int">
    
      <xs:minInclusive value="-1"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```


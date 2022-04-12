---
title: ConnectivityModalities simpleType 
TOCTitle: ConnectivityModalities simpleType
ms:assetid: 67b3a37a-d9a7-aa6a-536e-446a937f9457
ms:mtpsurl: https://msdn.microsoft.com/library/Mt171046(v=office.16)
ms:contentKeyID: 65855613
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# ConnectivityModalities simpleType 

(Skype for Business SDN Interface 2.2, Schema "D")


## Type information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Base type</strong></p></td>
<td><p>xs:string</p></td>
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
    <xs:simpleType  name="ConnectivityModalities">
    
      <xs:restriction base="xs:string">
    
      <xs:enumeration value="DIRECT"/>
    
      <xs:enumeration value="RELAY"/>
    
      <xs:enumeration value="HTTPPROXY"/>
    
      <xs:enumeration value="HTTP-PROXY"/>
    
      <xs:enumeration value="FAILED"/>
    
      </xs:restriction>
      
    </xs:simpleType>
  
```


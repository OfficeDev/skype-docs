---
title: Connectivity element (ConnectionInfoType complexType) 
TOCTitle: Connectivity element
ms:assetid: 62e7e0ed-a340-b8d9-b42d-4f94f016f513
ms:mtpsurl: https://msdn.microsoft.com/library/Mt149449(v=office.16)
ms:contentKeyID: 65855396
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# Connectivity element 

(ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

(Obsolete) The inclusion of Relay Ip/port indicates that a particular endpoint uses a media relay (edge server) and if not access the remote address directly. It is provided only in QualityUpdate events.

## Element information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="connectivitymodalities-simpletype-skype-for-business-sdn-interface-2-2-schema-d.md">ConnectivityModalities</a></p></td>
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

    <xs:element name="Connectivity"  type="ConnectivityModalities" minOccurs="0">
    
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
<td><p><a href="connectioninfo-element-messagetype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">ConnectionInfo</a></p></td>
<td><p><a href="connectioninfotype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">ConnectionInfoType</a></p></td>
<td><p>Connection-related properties regardless of the media stream and end points.</p></td>
</tr>
</tbody>
</table>


### Child elements

None.

### Attributes

None.


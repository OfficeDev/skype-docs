---
title: CorrelationId element (ConnectionInfoType complexType) 
TOCTitle: CorrelationId element
ms:assetid: 5726de4b-5551-06e2-c9ce-c87e2cd91ee8
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404732(v=office.16)
ms:contentKeyID: 68250645
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# CorrelationId element 

(ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Identifier to correlate two SIP calls where mediation server is involved. Both SIP calls belong to the same conversation.

## Element information

<table>
<colgroup>
<col />
<col />
</colgroup>
<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p>xs:string</p></td>
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

    <xs:element name="CorrelationId"  type="xs:string" minOccurs="0">
    
    </xs:element>
  
```

## Elements and attributes

### Parent elements

<table>
<colgroup>
<col />
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Element</p></th>
<th><p>Type</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="connectioninfo-element-messagetype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">ConnectionInfo</a></p></td>
<td><p><a href="connectioninfotype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">ConnectionInfoType</a></p></td>
<td><p>Connection-related properties regardless of the media stream and end points.</p></td>
</tr>
</tbody>
</table>


### Child elements

None.

### Attributes

None.


---
title: ConversationId element (ConnectionInfoType complexType) 
TOCTitle: ConversationId element
ms:assetid: 431cf980-624b-383b-3f70-0771cc30b322
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404730(v=office.16)
ms:contentKeyID: 68250643
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# ConversationId element 

(ConnectionInfoType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Identifier to correlate different SIP calls involved in the same conversation. In some cases Skype for Business uses different SIP calls for different modalities. This identifier permits correlating these SIP calls in the same conversation.

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

    <xs:element name="ConversationId"  type="xs:string" minOccurs="0">
    
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


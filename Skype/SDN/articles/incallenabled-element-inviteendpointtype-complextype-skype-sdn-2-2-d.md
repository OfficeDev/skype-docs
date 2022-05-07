---
title: IncallEnabled element (InviteEndPointType complexType) Schema D
TOCTitle: IncallEnabled element (InviteEndPointType complexType)
ms:assetid: e0821fc0-0360-0696-c148-bbded00770cc
ms:mtpsurl: https://msdn.microsoft.com/library/Mt170896(v=office.16)
ms:contentKeyID: 65855471
description: Whether the endpoint (Skype for Business client) is capable to send incall quality update messages. The flag does not indicate whether the client is configured to send incall QoE reports.
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# IncallEnabled element (InviteEndPointType complexType) (Schema D)

(InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

Whether the endpoint (Skype for Business client) is capable to send incall quality update messages. This flag does not indicate whether the client is configured to send incall QoE reports.


**In this article**  
Element information  
Definition  
Elements and attributes  

## Element information

<table>
<colgroup>
<col >
<col >
</colgroup>
<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p>xs:boolean</p></td>
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

    <xs:element name="IncallEnabled"  type="xs:boolean" minOccurs="0">
    
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
<td><p><a href="callee-element-invitetype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">Callee</a></p></td>
<td><p><a href="inviteendpointtype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">InviteEndPointType</a></p></td>
<td><p>Properties of the callee.</p></td>
</tr>
<tr class="even">
<td><p><a href="caller-element-invitetype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">Caller</a></p></td>
<td><p><a href="inviteendpointtype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">InviteEndPointType</a></p></td>
<td><p>Properties of the caller.</p></td>
</tr>
</tbody>
</table>


### Child elements

None.

### Attributes

None.


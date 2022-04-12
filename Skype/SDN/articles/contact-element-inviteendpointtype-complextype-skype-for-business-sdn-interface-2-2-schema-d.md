---
title: Contact element (InviteEndPointType complexType) 
TOCTitle: Contact element (InviteEndPointType complexType)
ms:assetid: a4ab18bd-b57f-2249-9d86-b389661cfa1f
ms:mtpsurl: https://msdn.microsoft.com/library/Mt149458(v=office.16)
ms:contentKeyID: 65855403
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# Contact element 

(InviteEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

SIP URI of the user as extracted from the Contact header of the underlying SIP message. This field is obfuscated unless hidepii is set to false in the Dialog Listener configuration file.

## Element information

<table>

<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p>xs:anyURI</p></td>
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

    <xs:element name="Contact"  type="xs:anyURI" minOccurs="0">
    
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


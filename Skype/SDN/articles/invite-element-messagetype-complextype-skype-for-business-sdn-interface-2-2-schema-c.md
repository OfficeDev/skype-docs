---
title: Invite element (MessageType complexType) (Schema C)
TOCTitle: Invite element
ms:assetid: 4b51e14c-e36f-7ac5-8223-b94c5a54d80e
ms:mtpsurl: https://msdn.microsoft.com/library/Mt404776(v=office.16)
ms:contentKeyID: 68250687
description: Event that an endpoint attempts to establish a call. DialogListener will include this element in its output if the sendcallinvites entry is set to True.
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# Invite element 

(MessageType complexType) (Skype for Business SDN Interface 2.2, Schema "C")

Event that an endpoint attempts to establish a call. DialogListener will include this element in its output if the sendcallinvites entry is set to True (activated) in the DialogListener configuration file. In addition, DialogListener will also notifies any SIP Invite messages (re-invites), not just the first one. Following this message Earlymedia may be flowing but this element is not intended to report on early media streams.

**In this article**  
Element information  
Definition  
Elements and attributes  

## Element information

<table>
<colgroup>
<col />
<col />
</colgroup>
<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="invitetype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">InviteType</a></p></td>
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

    <xs:element name="Invite"  type="InviteType">
    
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
<td><p><a href="lyncdiagnostics-element-skype-for-business-sdn-interface-2-2-schema-c.md">LyncDiagnostics</a></p></td>
<td><p><a href="messagetype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">MessageType</a></p></td>
<td><p>The root element for output from the Skype for Business SDN Manager.</p></td>
</tr>
</tbody>
</table>


### Child elements

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
<td><p><a href="callee-element-invitetype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Callee</a></p></td>
<td><p><a href="endpointtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">EndPointType</a></p></td>
<td><p>Properties of the callee.</p></td>
</tr>
<tr class="even">
<td><p><a href="caller-element-invitetype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">Caller</a></p></td>
<td><p><a href="endpointtype-complextype-skype-for-business-sdn-interface-2-2-schema-c.md">EndPointType</a></p></td>
<td><p>Properties of the caller.</p></td>
</tr>
</tbody>
</table>


### Attributes

None.


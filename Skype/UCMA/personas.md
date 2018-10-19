---
title: Personas
TOCTitle: Personas
ms:assetid: c033d25f-e55a-449d-b697-f93f64cee0a6
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465949(v=office.16)
ms:contentKeyID: 65239862
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Personas


_**Applies to:** Skype for Business 2015_

Many of the more important architectural components of Microsoft Unified Communications Managed API 5.0 are associated with one or more personas, terms that describe ownership, participation, and other attributes of human participants or applications that take part in a UCMA 5.0 application. The following table describes the personas used in UCMA 5.0.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Persona</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Local owner</p></td>
<td><p>The owner of the local endpoint in a process. A local owner can own multiple endpoints, such as a Skype for Business 2015-enabled mobile telephone and a desktop computer running Skype for Business 2015.</p></td>
</tr>
<tr class="even">
<td><p>Local participant</p></td>
<td><p>The participant associated with a particular conversation instance. Each conversation instance has one local participant, and can have multiple remote participants. A participant can be a person or an application.</p></td>
</tr>
<tr class="odd">
<td><p>Remote participant</p></td>
<td><p>Relative to a given conversation instance, a participant who is associated with a different conversation instance. Each conversation instance has one local participant, and can have multiple remote participants. A participant can be a person or an application.</p></td>
</tr>
<tr class="even">
<td><p>Organizer</p></td>
<td><p>The conference organizer is the participant who schedules the conference. The organizer is not necessarily a conference participant.</p></td>
</tr>
<tr class="odd">
<td><p>Conference Leader</p></td>
<td><p>The conference leader is a participant who is granted the ability to perform certain privileged operations in an active conference, such as admitting lobby participants into the conference or ejecting participants from the conference.</p></td>
</tr>
<tr class="even">
<td><p>Conference Participant</p></td>
<td><p>An entity to whom the Organizer grants permission to participate in a conference. A conference participant can be the conference Leader.</p></td>
</tr>
<tr class="odd">
<td><p>Presentity</p></td>
<td><p>An entity that can publish presence information. A local presentity is the entity associated with a particular endpoint. Relative to the same endpoint, a remote presentity is associated with a different endpoint. A local presentity can subscribe to presence information from a remote presentity.</p></td>
</tr>
<tr class="even">
<td><p>Contact</p></td>
<td><p>A presentity whose presence information is tracked. In unified communications, a contact typically registers its status and other attributes with Skype for Business Server 2015.</p></td>
</tr>
</tbody>
</table>


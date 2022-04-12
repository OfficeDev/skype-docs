---
title: Media stack performance counters
TOCTitle: Media stack performance counters
ms:assetid: c2476bf2-bb17-4f28-b69b-060fb21924e9
description: Media stack performance counters are shipped with Skype for Business Server. They are not available on clients.
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466085(v=office.16)
ms:contentKeyID: 65240039
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Media stack performance counters


**Applies to**: Skype for Business 2015

Media stack performance counters are shipped with Skype for Business Server. They are not available on clients.

The Media stack performance counters are controlled by the following registry subkey:

<!--<pre IsFakePre="true" xmlns="https://www.w3.org/1999/xhtml">-->

<strong>HKEY_LOCAL_MACHINE</strong>

`\Software\Microsoft\RTC\Performance\EnablePerfCounters\`


The values associated with this registry subkey appear in the following table.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Value</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>0</p></td>
<td><p>Do not enable performance counters.</p></td>
</tr>
<tr class="even">
<td><p>1</p></td>
<td><p>Enable Operational, Planning, and Informational performance counters.</p></td>
</tr>
</tbody>
</table>


By default, the value is 0 for clients, and is 1 for A/V MCU, UCMA 5.0, Application-Sharing MCU, and Mediation Server. Values of 2 or larger are reserved for system use.

The following topics discuss each media stack performance counter:

- [Operations performance counters](operations-performance-counters.md)
- [Planning performance counters](planning-performance-counters.md)
- [Informational performance counters](informational-performance-counters.md)


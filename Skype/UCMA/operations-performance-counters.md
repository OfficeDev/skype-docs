---
title: Operations performance counters
TOCTitle: Operations performance counters
ms:assetid: 236d3a62-080c-4138-b281-1f4ce7373b85
description: Operations performance counters are monitored by Microsoft Operations Manager (MOM) packs that check for specific threshold values.
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466087(v=office.16)
ms:contentKeyID: 65240040
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Operations performance counters


**Applies to**: Skype for Business 2015

Operations performance counters are monitored by Microsoft Operations Manager (MOM) packs that check for specific threshold values. For counters with threshold values, alarms are activated after the values are crossed. For security-related counters, the counter values should be analyzed to ensure that a malicious user has not compromised the system.

**Global health**

<table>
<colgroup>
<col />
<col />
</colgroup>
<tbody>
<tr class="odd">
<td><p>Counter Name</p></td>
<td><p>Global health</p></td>
</tr>
<tr class="even">
<td><p>Display Name</p></td>
<td><p>MEDIA- 000 - Global health</p></td>
</tr>
<tr class="odd">
<td><p>Component</p></td>
<td><p>Media Stack</p></td>
</tr>
<tr class="even">
<td><p>Subcategory</p></td>
<td><p>Operations</p></td>
</tr>
<tr class="odd">
<td><p>Description</p></td>
<td><p>This server is operating at maximum capacity and audio/video quality in all conferences is degraded. Global health indicator - 0 is DISABLED, 1 is NORMAL, 2 is LIGHTLOAD, 3 is HEAVYLOAD, and 4 is OVERLOADED.</p></td>
</tr>
<tr class="even">
<td><p>Usage</p></td>
<td><p>Global health indicates the state of the Media stack, if enabled:</p>
<ul>
<li><p>1: A/V Conferencing Server should accept new conferences and users.</p></li>
<li><p>2: A/V Conferencing Server should accept new conferences and users. Some video and audio frames are dropped to reduce the overall load on the server.</p></li>
<li><p>3: A/V Conferencing Server should accept new conferences and users. Additional video and audio frames are dropped to reduce the overall load.</p></li>
<li><p>4: A/V Conferencing Server should stop accepting new users or conferences. No video is sent and the audio frame rate drops to reduce the load on the system.</p></li>
</ul></td>
</tr>
<tr class="odd">
<td><p>Minimum Value</p></td>
<td><p>0</p></td>
</tr>
<tr class="even">
<td><p>Maximum Value</p></td>
<td><p>4</p></td>
</tr>
<tr class="odd">
<td><p>MOM Enabled</p></td>
<td><p>Enabled</p></td>
</tr>
<tr class="even">
<td><p>Warning Threshold</p></td>
<td><p>2</p></td>
</tr>
<tr class="odd">
<td><p>Error Threshold</p></td>
<td><p>3+</p>
<p>Resolution: This might be a temporary condition. If the problem persists, consider adding more servers to distribute the load.</p></td>
</tr>
</tbody>
</table>


**TCP disconnects because remote out of sync**

<table>
<colgroup>
<col />
<col />
</colgroup>
<tbody>
<tr class="odd">
<td><p>Counter Name</p></td>
<td><p>Transmission Control Protocol (TCP) disconnects because remote is not synchronized.</p></td>
</tr>
<tr class="even">
<td><p>Display Name</p></td>
<td><p>MEDIA-001- TCP disconnects because remote is not synchronized.</p></td>
</tr>
<tr class="odd">
<td><p>Component</p></td>
<td><p>Media Stack</p></td>
</tr>
<tr class="even">
<td><p>Subcategory</p></td>
<td><p>Operations</p></td>
</tr>
<tr class="odd">
<td><p>Description</p></td>
<td><p>Number of TCP disconnects happening because the received packet is not synchronized.</p></td>
</tr>
<tr class="even">
<td><p>Usage</p></td>
<td><p>The server is under TCP data injection attack or the server cannot process the TCP channel quickly enough. If the server load is not high and there are abnormal disconnects, the server might be compromised by a malicious user.</p></td>
</tr>
<tr class="odd">
<td><p>Minimum Value</p></td>
<td><p>0</p></td>
</tr>
<tr class="even">
<td><p>Maximum Value</p></td>
<td><p>INT_MAX</p></td>
</tr>
<tr class="odd">
<td><p>MOM Enabled</p></td>
<td><p>Enabled</p></td>
</tr>
</tbody>
</table>


**Relay allocation failures**

<table>
<colgroup>
<col />
<col />
</colgroup>
<tbody>
<tr class="odd">
<td><p>Counter Name</p></td>
<td><p>Relay allocation failures</p></td>
</tr>
<tr class="even">
<td><p>Display Name</p></td>
<td><p>MEDIA-002- Relay allocation failures</p></td>
</tr>
<tr class="odd">
<td><p>Component</p></td>
<td><p>Media Stack</p></td>
</tr>
<tr class="even">
<td><p>Subcategory</p></td>
<td><p>Operations</p></td>
</tr>
<tr class="odd">
<td><p>Description</p></td>
<td><p>Number of allocations on the A/V Conferencing Edge Server that have failed.</p></td>
</tr>
<tr class="even">
<td><p>Usage</p></td>
<td><p>The server (A/V Conferencing Server or Mediation Server) cannot bind to local ports or cannot allocate a port on A/V Conferencing Edge Server. Determine whether the A/V Conferencing Edge Server is unavailable or the server has too many open connections.</p></td>
</tr>
<tr class="odd">
<td><p>Minimum Value</p></td>
<td><p>0</p></td>
</tr>
<tr class="even">
<td><p>Maximum Value</p></td>
<td><p>INT_MAX</p></td>
</tr>
<tr class="odd">
<td><p>MOM Enabled</p></td>
<td><p>Enabled</p></td>
</tr>
</tbody>
</table>


**Secure RTP failures/sec**

<table>
<colgroup>
<col />
<col />
</colgroup>
<tbody>
<tr class="odd">
<td><p>Counter Name</p></td>
<td><p>Secure Real-Time Transport Protocol (RTP) failures per second.</p></td>
</tr>
<tr class="even">
<td><p>Display Name</p></td>
<td><p>MEDIA-003- Number of packets dropped by Secure RTP per second.</p></td>
</tr>
<tr class="odd">
<td><p>Component</p></td>
<td><p>Media Stack</p></td>
</tr>
<tr class="even">
<td><p>Subcategory</p></td>
<td><p>Operations</p></td>
</tr>
<tr class="odd">
<td><p>Description</p></td>
<td><p>The number of packets dropped by the secure RTP module per second.</p></td>
</tr>
<tr class="even">
<td><p>Usage</p></td>
<td><p>The server (A/V Conferencing Server or Mediation Server) cannot authenticate/decrypt incoming RTP packets. If the problem persists, the server might be compromised by a malicious user. Consult your network security administrator.</p></td>
</tr>
<tr class="odd">
<td><p>Minimum Value</p></td>
<td><p>0.0</p></td>
</tr>
<tr class="even">
<td><p>Maximum Value</p></td>
<td><p>FLOAT_MAX</p></td>
</tr>
<tr class="odd">
<td><p>MOM Enabled</p></td>
<td><p>Enabled</p></td>
</tr>
</tbody>
</table>


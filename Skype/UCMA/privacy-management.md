---
title: Privacy management
TOCTitle: Privacy management
ms:assetid: 3eb4c8dd-8acb-487c-a92c-a6dd4098df0b
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466020(v=office.16)
ms:contentKeyID: 65239957
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Privacy management


**Applies to**: Skype for Business 2015

In Microsoft Unified Communications Managed API 5.0, user endpoints operate in **Private** mode if they are so configured by the enterprise administrator, unless they are configured to be in **Standard** mode by the endpoint owner. User endpoints can also switch to (or "be migrated to") **Standard** mode if the administrator does not state a preference or orders such a change.

## Privacy mode versus preferred privacy mode

The [PrivacyModePreference](https://msdn.microsoft.com/en-us/library/hh382914\(v=office.16\)) enumeration indicates the privacy mode that is preferred by the endpoint owner. In contrast, the [PrivacyModePreference](https://msdn.microsoft.com/en-us/library/hh382914\(v=office.16\)) enumeration indicates the privacy mode that is set by the Skype for Business Server 2015 administrator.

The values of the [PrivacyModePreference](https://msdn.microsoft.com/en-us/library/hh382914\(v=office.16\)) enumeration are shown in the following table.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Value</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>None</p></td>
<td><p>No selection has been made.</p></td>
</tr>
<tr class="even">
<td><p>Private</p></td>
<td><p>The endpoint owner wants privacy to be enabled.</p></td>
</tr>
<tr class="odd">
<td><p>Standard</p></td>
<td><p>The endpoint owner does not want privacy to be enabled.</p></td>
</tr>
</tbody>
</table>


The values of the [PrivacyMode](https://msdn.microsoft.com/en-us/library/hh382900\(v=office.16\)) enumeration are shown in the following table.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Value</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Standard</p></td>
<td><p>The local owner is not enabled for privacy.</p></td>
</tr>
<tr class="even">
<td><p>Private</p></td>
<td><p>The local owner is enabled for privacy.</p></td>
</tr>
<tr class="odd">
<td><p>MigratingToPrivate</p></td>
<td><p>The local owner is in a transitory phase and is currently being enabled for privacy.</p></td>
</tr>
<tr class="even">
<td><p>MigratingToStandard</p></td>
<td><p>The local owner is in a transitory phase and is currently being disabled for privacy.</p></td>
</tr>
</tbody>
</table>


A user endpoint can update its privacy mode using the [BeginUpdatePrivacyPreference(PrivacyModePreference, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh382158\(v=office.16\)) and [EndUpdatePrivacyPreference(IAsyncResult)](https://msdn.microsoft.com/en-us/library/hh383237\(v=office.16\)) methods that are available from the [PresenceServices](https://msdn.microsoft.com/en-us/library/hh349242\(v=office.16\)) property on the [UserEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.userendpoint?view=ucma-api) class.

The only constraint on UCMA 5.0 endpoints in participating in migration is that they must be subscribed to both [LocalOwnerPresence](https://msdn.microsoft.com/en-us/library/hh382370\(v=office.16\)) and [ContactGroupServices](https://msdn.microsoft.com/en-us/library/hh381099\(v=office.16\)). There is a tight coupling between these sessions, such that addition of a contact might require an Access Control Entry to be added in the local owner’s roaming data or vice-versa requiring both the data to be available for UCMA to perform the operation.

Migrating involves applying the appropriate manifest from the server and ensuring that the contact list and container membership list of the endpoint owner are synchronized. Any inconsistencies that are detected after migration are also corrected, thereby ensuring that the endpoint owner's data complies with the rules dictated by the server's container manifest.

## Contact addition and deletion in privacy mode

Any contact added in privacy mode will have an equivalent Access Control Entry (ACE) added to a container based on the Source Network of the contact unless indicated differently by the endpoint owner. The [BeginAddContact(String, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh384923\(v=office.16\)), [BeginAddContact(String, ContactAddOptions, AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh381403\(v=office.16\)), and [EndAddContact(IAsyncResult)](https://msdn.microsoft.com/en-us/library/hh382284\(v=office.16\)) methods on the [ContactGroupServices](https://msdn.microsoft.com/en-us/library/hh381099\(v=office.16\)) class can be used to add a contact in privacy mode.

Similarly, when a contact is deleted in privacy mode, all ACEs indicating the contact's SIP URI are automatically removed, unless the URI belongs to the endpoint owner's blocked container. For more information, see "Relationship level management" in [LocalOwnerPresence](localownerpresence.md).


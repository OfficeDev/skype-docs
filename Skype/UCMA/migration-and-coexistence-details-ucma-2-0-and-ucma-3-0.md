---
title: Migration and coexistence details (UCMA 2.0 and UCMA 3.0)
TOCTitle: Migration and coexistence details (UCMA 2.0 and UCMA 3.0)
ms:assetid: 969fa41d-6941-4327-9fd3-5879153b7977
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466121(v=office.16)
ms:contentKeyID: 65240059
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Migration and coexistence details (UCMA 2.0 and UCMA 3.0)

**Applies to**: Skype for Business 2015

The sections in this topic provide detailed information about migrating applications from previous UCMA versions to current versions, in topologies that can consist of different Microsoft Communications Server/Lync Server 2010 versions.

## When should I use WMI and when should I use PowerShell?

WMI tools such as ApplicationProvisioner.exe, a tool that shipped with the SDK, can be used to create trusted service entries and contact objects so that UCMA 2.0 applications continue to work even after an application is migrated. Before using ApplicationProvisioner.exe, you must modify the source code for this tool, and then rebuild it. For more information, see [Modifying ApplicationProvisioner.exe sample code](modifying-applicationprovisioner-exe-sample-code.md). In general, trusted application PowerShell cmdlets do not work with UCMA 2.0 deployments. The only exceptions are the following cmdlets:

- **Get-***Xxx* cmdlets.

- The **Set-CsTrustedApplicationPool** cmdlet, which can be used to retarget the Registrar for an application.

- The **Move-CsApplicationEndpoint** cmdlet, which can be used to retarget the contact object to a new application pool or populate the missing attributes of a UCMA 2.0 application contact object, when the contact object must be routed through the Lync Server 2010 Registrar.

<table>
<colgroup>
<col style="width: 33%" />
<col style="width: 33%" />
<col style="width: 33%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Scenario</p></th>
<th><p>WMI or PowerShell?</p></th>
<th><p>Comments</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>S-1: Coexistence of a UCMA 2.0 application</p></td>
<td><p>WMI</p></td>
<td><p>The application is still deployed in its own UCMA 2.0 application. Therefore no changes needed on how it is managed.</p></td>
</tr>
<tr class="even">
<td><p>S-2: Upgrading a UCMA 2.0 application to after coexistence</p></td>
<td><p>PowerShell</p></td>
<td><p>PowerShell is needed to move contact objects from UCMA 2.0 application. After the move, management of all contact objects is carried out using Powershell.</p></td>
</tr>
<tr class="odd">
<td><p>S-3: In-place migration of a UCMA 2.0 application</p></td>
<td><p>Both</p></td>
<td><p>Each time a new contact must be added, you need to add it through the WMI tool and then run PowerShell cmdlet to upgrade the contact object to have Lync Server 2010 properties.</p></td>
</tr>
<tr class="even">
<td><p>S-4: Direct deployment of a UCMA 2.0 application against pure Lync Server 2010</p></td>
<td><p>Both</p></td>
<td><p>Every time a new contact needs to be added, you need to add it through the WMI tool and then run PowerShell cmdlet to upgrade the contact object to have Lync Server 2010 properties.</p></td>
</tr>
<tr class="odd">
<td><p>S-5: Upgrading a UCMA 2.0 application to UCMA 3.0 after in-place migration</p></td>
<td><p>PowerShell</p></td>
<td><p>PowerShell is needed to move contact objects from a UCMA 2.0 application. After the move, management of all contact objects is done using Powershell.</p></td>
</tr>
<tr class="even">
<td><p>S-6: Coexistence of a UCMA 3.0 Application</p></td>
<td><p>PowerShell</p></td>
<td><p>The application is a UCMA 3.0 application, so no changes are needed in how it is managed.</p></td>
</tr>
<tr class="odd">
<td><p>S-7: Direct deployment of a UCMA 3.0 application against</p></td>
<td><p>WMI</p></td>
<td><p>The application is a UCMA 3.0 application, but it must be deployed as UCMA 2.0 application.</p></td>
</tr>
<tr class="even">
<td><p>S-8: Coexistence and migration of a UCMA V1.0 application</p></td>
<td><p>WMI or PowerShell</p></td>
<td><p>Depends on whether the newly recompiled application is deployed against or Lync Server 2010.</p></td>
</tr>
</tbody>
</table>


## Migrating trusted service entries using Microsoft Lync Server 2010 Topology Builder or PowerShell cmdlets

### Using Topology Builder

1.  Launch Microsoft Lync Server 2010, Topology Builder.

2.  After the existing topology is loaded, under **Action**, choose **Merge 2007 or 2007 R2 Topology**. This launches a wizard.

3.  Go through the wizard, keeping the default options. After the wizard has finished, check that it completed successfully. There should be no errors in the UI.

4.  Select **Publish Topology** and complete the wizard, as in the previous step.

### Using PowerShell

1.  From the **Start** menu, in the **Microsoft Lync Server 2010** program group, open **Lync Server Management Shell**.

2.  Run the following PowerShell cmdlet. Merge-CsLegacyTopology -TopologyXmlFileName D:\\output.xml

3.  Run the following PowerShell cmdlet. Run Publish-CsTopology -FileName D:\\output.xml

## Verify that trusted service entries migrated successfully

### Using Microsoft Lync Server 2010 Topology Builder

1.  Launch Topology Builder

2.  Verify that a **BackCompatSite** node appears under the Lync Server 2010 node.

3.  Under **BackCompatSite**, in the **Trusted application servers** node, you should see a pool entry for your application pool.

### Using Microsoft Lync Server 2010 Control Panel

1.  Launch Microsoft Lync Server 2010 Control Panel.

2.  Click **Topology**.

3.  Under the **Trusted Application** tab, verify that the UCMA 2.0 application has an entry with the correct pool and port numbers, as shown in the following illustration.

### Using PowerShell cmdlets

Although the **New-CsTrustedApplication***Xxx*, **Set-CsTrustedApplication***Xxx*, and **Remove-CsTrustedApplication***Xxx* versions of the Lync Server 2010 cmdlets cannot be used to manage legacy application pools, the **Get-CsTrustedApplication***Xxx* versions can be used to verify that the **Pool**, **Service**, **Application**, and **ApplicationEndpoint** entries have been created correctly. For more information about any of these cmdlets, run `get-help <cmdlet>`.

The following PowerShell cmdlet is an example: 

```powershell
  Get-CsTrustedApplicationPool –Identity TrustPool.contoso.com
```

In this example, the **Identity** parameter has been used to ensure that only one trusted application pool is retrieved, the pool whose FQDN is TrustPool.contoso.com. After running the cmdlet, you should verify the following settings in the returned value:

- **Registrar**—should still be the pool.

- **Applications**—should contain a list of all UCMA 2.0 applications running in the pool.

- **SiteId**—should be Site.BackCompatSite.

## Associate the existing backward-compatible legacy pool with the Lync Server 2010 registrar

You will need to run **Set-CsTrustedApplicationPool** with the needed values for your application pool and Lync Server 2010 registrar pool as follows: Set-CsTrustedApplicationPool –Identity TrustPool.contoso.com –Registrar CS14RegistrarPoolA.contoso.com

> [!NOTE]
> When you run this cmdlet, your application stops functioning momentarily. The contact objects are orphaned until they are pointed to a new Registrar.

## Get OCSCore.MSI from the Office Communications Server 2007 R2 installer

The most important thing about this scenario is that the UCMA 2.0 application is expected to be installed in a pure Lync Server 2010 environment using WMI tools, which require the version of OCSCore. The application also needs OCSCore.msi at run time if it uses WMI-based auto-provisioning. The OCSCore Components runtime package can be downloaded from [Office Communications Server 2007 R2, Core Components Runtime Package](https://go.microsoft.com/fwlink/?linkid=207171).

## Install the UCMA 2.0 application

Install the UCMA 2.0 application just as you would have installed it against . This includes but is not limited to installing UCMARedist(Runtime), SpeechRedist, and OCSCore, creating trusted service entries, and optionally creating Active Directory contact objects. For more information, see [Deploying a UCMA 2.0 Core Application](https://msdn.microsoft.com/en-us/library/dd280155\(v=office.13\).aspx).

## Add AllowedDomains using WBemTest

When a UCMA 2.0 application is deployed directly against Lync Server 2010, the SIP domains used in the Lync Server 2010 environment must be added to the SIP domain list before you run the Merge-CsLegacyTopology cmdlet. The application is being deployed as if it were being deployed against , and then migrated to run against Lync Server 2010.

WBemTest can be used to add the SIP domains, as described in the following procedure.

### To add AllowedDomains using WBemTest

1.  In a command prompt window, start WBemTest.exe by typing WBemTest, and pressing ENTER.

2.  In the **Windows Management Instrumentation Tester** dialog box, click **Connect**.

3.  In the **Connect** dialog box, click **Connect**.

4.  In the **Windows Management Instrumentation Tester** dialog box, click **Enum Classes**.

5.  In the **Superclass Info** dialog box, click **OK**.

6.  In the **Query Result** dialog box, scroll down the listbox to MSFT\_SIPDomainData(), and double-click this entry.

7.  In the **Object editor for MSFT\_SIPDomainData** dialog box, click **Instances**. This causes the **Query Result** dialog box to open, displaying the InstanceIDs for any instances of the MSFT\_SIPDomainData WMI class. These entries are the AllowedDomain entries.

8.  To add AllowedDomain entries, click **Add**.

9.  In the **Instance of MSFT\_SIPDomainData** dialog box, in the **Properties** listbox, double-click **Address**.

10. In the **Property Editor** dialog box, select the **Not NULL** radio button.

11. In the **Value** text input pane, type the name of the new allowed domain; for example, contoso.com.

12. Click **Save Property**.

13. In the **Instance of MSFT\_SIPDomainData** dialog box, in the **Properties** listbox, double-click **Authoritative**. The **Authoritative** property should not be Null and should be set to False.

14. Click **Save Property**.

15. In the **Instance of MSFT\_SIPDomainData** dialog box, in the **Properties** listbox, double-click **Default Domain**. The **Default Domain** property should not be Null and should be set to True.

16. Click **Save Property**.

17. In the **Instance of MSFT\_SIPDomainData** dialog box, click **Save Object**.


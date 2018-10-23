---
title: 'UCMA applications: Coexistence, migration, and upgrade scenarios'
TOCTitle: 'UCMA applications: Coexistence, migration, and upgrade scenarios'
ms:assetid: 132c6c9c-f8e1-4d3f-aa8a-da35891b05d8
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466143(v=office.16)
ms:contentKeyID: 65240057
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA applications: Coexistence, migration, and upgrade scenarios

The coexistence environments described in this topic refer to topologies that consist of *two* Communications Server or Lync Server versions. The two topology mixes described here are the following.

- in coexistence with

- in coexistence with

For information about the third possibility, in which is in coexistence with Lync Server 2010, see [UCMA 2.0 Core applications: Coexistence, migration, and upgrade scenarios](ucma-2-0-core-applications-coexistence-migration-and-upgrade-scenarios.md) or [UCMA 3.0 Core applications: Coexistence and deployment scenarios](ucma-3-0-core-applications-coexistence-and-deployment-scenarios.md).

<table>
<colgroup>
<col style="width: 20%" />
<col style="width: 20%" />
<col style="width: 20%" />
<col style="width: 20%" />
<col style="width: 20%" />
</colgroup>
<thead>
<tr class="header">
<th><p></p></th>
<th><p>Coexistence scenario</p></th>
<th><p>Migration scenario</p></th>
<th><p>Upgrade scenario</p></th>
<th><p>Direct deployment scenario (against Lync Server 2013)</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>UCMA 3.0</p></td>
<td><p>See UCMA 3.0 application on Lync Server 2010 with users on Lync Server 2013.</p></td>
<td><p>See Migrating a UCMA 3.0 application &quot;as is&quot; to Lync Server 2013.</p></td>
<td><p>See Upgrading a UCMA 3.0 application to UCMA 4.0.</p></td>
<td><p>See Deploying a UCMA 3.0 application against Lync Server 2013.</p></td>
</tr>
<tr class="even">
<td><p>UCMA 2.0</p></td>
<td><p>See UCMA 2.0 application on Communications Server 2007 R2 with users on Lync Server 2013.</p></td>
<td><p>See Migrating a UCMA 2.0 application &quot;as is&quot; to Lync Server 2013.</p></td>
<td><p>See Upgrading a UCMA 2.0 application to UCMA 4.0 after coexistence.</p></td>
<td><p>See Deploying a UCMA 2.0 application against Lync Server 2013.</p></td>
</tr>
</tbody>
</table>


## UCMA 3.0 applications in a coexistence environment: Mixed Lync Server 2010 and Lync Server 2013 topology

This section discusses UCMA 3.0 applications that are running on Lync Server 2010, with some users on Skype for Business Server 2015. Developers can choose to keep the application running in the coexistence environment, can migrate the UCMA 3.0 application to Skype for Business Server 2015, or can upgrade the UCMA 3.0 application to UCMA 4.0.

### UCMA 3.0 application on Lync Server 2010 with users on Lync Server 2013

The UCMA 3.0 application will continue to communicate and interoperate with the users on Skype for Business Server 2015.

### Migrating a UCMA 3.0 application "as is" to Lync Server 2013

Apply KB 2741295 to manage UCMA 3.0 applications against Skype for Business Server 2015.

In a mixed environment, if you create a new trusted application server after merging the Lync Server 2010 topology with Skype for Business Server 2015, and you define a new trusted application server using **Topology Builder**, you must set the next hop pool to be a Skype for Business Server 2015 pool. In a merged environment, both the Lync Server 2010 pool and the Skype for Business Server 2015 pool appear in the drop down list. Selecting the legacy pool is not an option.

### To select Lync Server 2013 as next hop when you create a trusted application server

1.  Open **Topology Builder**.

2.  In the left pane, right click **Trusted application servers** and then click **New Trusted Application Pool**.

3.  Enter the **Pool FQDN** of the trusted application pool and select whether it will be a single-server or multiple-server pool.

4.  Click **Next**.

5.  On the Select the next hop page, from the list, select the Skype for Business Server 2015 Front End pool.

6.  Click **Finish**.

7.  Select the top node **Lync Server** and from the **Action** menu, select **Publish**.
    
    Verify that the trusted application pool successfully has been created and is associated with the correct Front End pool.

### Upgrading a UCMA 3.0 application to UCMA 4.0

Simply rebuild your UCMA 3.0 application using the UCMA 5.0 SDK. Watch for compiler warnings about the use of obsolete class members. For more information, see [Obsolete or removed API members in UCMA 5.0](https://msdn.microsoft.com/en-us/library/dn465992\(v=office.16\)).

## UCMA 2.0 applications in a coexistence environment: Mixed Office Communications Server 2007 R2 and Lync Server 2013 topology

This section discusses UCMA 2.0 applications that are running on , with some users on Skype for Business Server 2015. Developers can choose to keep the application running in the coexistence environment, can migrate the UCMA 2.0 application to Skype for Business Server 2015, or can upgrade the UCMA 2.0 application to UCMA 4.0.

### UCMA 2.0 application on Communications Server 2007 R2 with users on Lync Server 2013

The UCMA 2.0 application will continue to communicate and interoperate with the users on Skype for Business Server 2015.

### Migrating a UCMA 2.0 application "as is" to Lync Server 2013

> [!IMPORTANT]
> This scenario is supported, but not recommended.

After all users have been migrated from an pool to a Skype for Business Server 2015 pool, consider decommissioning the pool. Before you decommission , consider routing to your applications through a Skype for Business Server 2015 Registrar pool.

The **Move-CsApplicationEndpoint** cmdlet can be used to populate the necessary attributes of an existing application endpoint contact object in Active Directory so that routing occurs through the Skype for Business Server 2015 Registrar.

The following illustration shows this topology after the UCMA 2.0 application has been migrated to run against Skype for Business Server 2015.

**Migrating a UCMA 2.0 application to Lync Server 2013**
  
![Migrating a UCMA 2.0 app to Lync Server 2013](images/Dn466143.Migr_Scenario3_LS2013(Office.16).png "Migrating a UCMA 2.0 app to Lync Server 2013")

1.  Verify that the steps that are described in UCMA 2.0 application on Communications Server 2007 R2 with users on Lync Server 2013 have completed successfully.

2.  Associate the existing backward-compatible UCMA 2.0 application pool with the Skype for Business Server 2015 Registrar. See the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

3.  Run the **Move-CsApplicationEndpoint** cmdlet to update the contact object so that routing occurs through the Skype for Business Server 2015 Registrar. Use the –**Force** parameter.
    
    You will need to run this cmdlet with the needed values for each contact object that your application uses.
    
    `Move-CsApplicationEndpoint -Identity sip:endpoint1@contoso.com -TargetApplicationPool ApplicationPool.contoso.com –Force`
    
    In the preceding example, no move is occurring, so the pool represented by the **TargetApplicationPool** parameter is also the source application pool.
    
    Each time you create a new contact object using the WMI tools (ApplicationProvisioner.exe), you must perform this step. If it is omitted, the contact object properties required by the Skype for Business Server 2015 Registrar for routing are not populated. You cannot use Skype for Business Server 2015 cmdlets to create new contact objects for your application, because the application pool is still a legacy pool.

4.  Verify that the **Move-CsApplicationEndpoint** cmdlet was successful by running the following cmdlet, and verifying that the output of the Registrar property points to the Skype for Business Server 2015 Registrar.
    
    `Get-CsApplicationEndpoint sip:endpoint1@contoso.com | Select-Object *`
    
    Although the **New-CsTrustedApplicationEndpoint**, **Set-CsTrustedApplicationEndpoint**, and **Remove-CsTrustedApplicationEndpoint** cmdlets cannot be used to manage legacy application pools, the **Get-CsTrustedApplicationEndpoint** cmdlet can be used to verify that the contact object entries for the **ApplicationEndpoint** have been created correctly. For more information about any of these cmdlets, run `get-help <cmdlet>`.

5.  Verify that the application still works on each Front End, even when the Front End service is stopped.

6.  Continue to use WMI-based management tools (such as ApplicationProvisioner.exe) to add and manage the application contact objects. For more information, see [Modifying ApplicationProvisioner.exe sample code](modifying-applicationprovisioner-exe-sample-code.md).
    
    **Each time you add a new contact object, perform steps 3 and 4.**
    
    > [!NOTE]
    > This point is emphasized to remind you to perform the verifications in these steps when a new contact object is added using PowerShell. PowerShell might not be available locally on the computer from which you are running the WMI tool for activating new contact objects.

### Upgrading a UCMA 2.0 application to UCMA 4.0 after coexistence

To upgrade a UCMA 2.0 application to one that is written with UCMA 4.0, the customer must create a new UCMA 4.0 trusted application pool. After the application bits are deployed, the administrator can use PowerShell cmdlets to move contact objects tied to the existing UCMA 2.0 application pool and so that they now refer to the new application pool and Skype for Business Server 2015 Registrar.

The following illustration shows the topology during the upgrade.

**Before upgrade**
  
![Before upgrade](images/Dn466143.Migr_Scenario2_LS2013(Office.16).png "Before upgrade")

The following steps are the **recommended upgrade approach**.

1.  Verify that the steps that are described in UCMA 2.0 application on Communications Server 2007 R2 with users on Lync Server 2013 have completed successfully.

2.  Stop the UCMA 2.0 application so that configuration changes can be made.

3.  Create a new pool, either by using Microsoft Lync Server 2013 Topology Builder, or by running the **New-CsTrustedApplicationPool** cmdlet followed by the **New-CsTrustedApplicationComputer** cmdlet. For more information, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

4.  Create a new application by using the **New-CsTrustedApplication** cmdlet. The **ApplicationId** should be the same as the UCMA 2.0 application’s **ApplicationName**.

5.  Run the **Enable-CsTopology** cmdlet to create the appropriate trusted service entries in Active Directory for interoperability with .
    
    `Enable-CsTopology`

6.  Stop Skype for Business Server 2015 Front End service and then restart it.

7.  Install the application software in the trusted application pool created in step 3. Stop at the point you are asked to create a contact object for the application. For more information, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

8.  Run the **Move-CsApplicationEndpoint** cmdlet to move the contact object from the UCMA 2.0 application pool to the UCMA 4.0 application pool. Use the –**Force** parameter. Run this cmdlet with the needed values for each contact object that your application uses.
    
    `Run Move-CsApplicationEndpoint -Identity sip:endpoint1@contoso.com -TargetApplicationPool UCMA3.0ApplicationPool.contoso.com -Force`
    
    The **Move-CsApplicationEndpoint** cmdlet moves an existing endpoint contact object in Active Directory from an registrar pool to a Skype for Business Server 2015 registrar pool, or from one Skype for Business Server 2015 registrar pool to another. The target pool must be a trusted application pool, and the application associated with the given endpoint must exist in the target pool.

9.  Stop and then restart the Skype for Business Server 2015 Front End service before launching the UCMA 4.0 application.

10. Use the WMI-based management tools (ApplicationProvisioner.exe) to remove the UCMA 2.0 application servers. For more information, see [Modifying ApplicationProvisioner.exe sample code](modifying-applicationprovisioner-exe-sample-code.md).

11. Migrate the trusted service entries using Microsoft Lync Server 2013 Topology Builder or the PowerShell cmdlets. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

12. Verify the following.
    
    1.  Users are able to communicate with the new version of your application.
    
    2.  You are now able to use Skype for Business Server 2015 trusted application cmdlets to manage the existing contact objects and the new contact objects.

The following illustration shows the topology after the upgrade has taken place. The computers used in the UCMA 2.0 trusted application pool can be reused for other purposes, if desired.

**After upgrade**
  
![After upgrade](images/Dn466143.Migr_Scenario2b_LS2013(Office.16).png "After upgrade")

## Direct deployment of UCMA applications against "green field" Lync Server 2013

This section discusses the deployment of UCMA 2.0 or UCMA 3.0 applications in a new Skype for Business Server 2015 topology.

### Deploying a UCMA 2.0 application against Lync Server 2013

> [!IMPORTANT]
> This scenario is supported, but not recommended.

A UCMA 2.0 application is connected directly to Skype for Business Server 2015. Check with the application vendor to see whether such a deployment is certified.

The following illustration shows this topology.

**Deploying a UCMA 2.0 application against Lync Server 2013**

![Deploy a UCMA 2.0 app against Lync Server 2013](images/Dn466143.Migr_Scenario4_LS2013(Office.16).png "Deploy a UCMA 2.0 app against Lync Server 2013")

1.  Get OCSCore.MSI from the installer. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

2.  Install the UCMA 2.0 application. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

3.  Add allowed domains using WBemTest. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

4.  Migrate the trusted service entries using Microsoft Lync Server 2013 Topology Builder or PowerShell cmdlets. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

5.  Run the **Move-CsApplicationEndpoint** cmdlet to update the contact object so that routing occurs through the Skype for Business Server 2015 Registrar. Use the –**Force** parameter. You will need to the following step with the needed values for each contact object that your application uses.
    
    `Move-CsApplicationEndpoint -Identity sip:endpoint1@contoso.com -TargetApplicationPool ApplicationPool.contoso.com –Force`
    
    In the preceding example, no move is occurring, so the pool represented by the **TargetApplicationPool** parameter is also the source application pool.
    
    Each time you create a new contact object using the WMI tools (ApplicationProvisioner.exe), you must perform this step. If it is omitted, the contact object properties required by the Skype for Business Server 2015 Registrar for routing are not populated. You cannot use Skype for Business Server 2015 cmdlets to create new contact objects for your application, because the application pool is still a legacy pool.

6.  Verify that the **Move-CsApplicationEndpoint** cmdlet was successful by running the following cmdlet, and verifying that the output of the Registrar property points to the Skype for Business Server 2015 Registrar.
    
    `Get-CsApplicationEndpoint sip:endpoint1@contoso.com | Select-Object *`

7.  Verify the following.
    
    1.  Users are able to communicate with the application.
    
    2.  You are still able to use the WMI tool (ApplicationProvisioner.exe) to create new additional contact objects.
        
        Each time you add a new contact object, steps 4 and 5 must be performed.

### Deploying a UCMA 3.0 application against Lync Server 2013

Apply KB 2741295 to manage UCMA 3.0 applications against Skype for Business Server 2015.


---
title: 'UCMA 2.0 Core applications: Coexistence, migration, and upgrade scenarios'
TOCTitle: 'UCMA 2.0 Core applications: Coexistence, migration, and upgrade scenarios'
description: 'The sections in this document present the steps needed for UCMA 2.0 application coexistence, migration, and upgrade scenarios.'
ms:assetid: 2049823f-eb02-4c15-88f5-722ff95d14de
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466131(v=office.16)
ms:contentKeyID: 65240064
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA 2.0 Core applications: Coexistence, migration, and upgrade scenarios

**Applies to**: Skype for Business 2015

The following sections present the steps needed for UCMA 2.0 application coexistence, migration, and upgrade scenarios. Because external trusted applications are represented as trusted service entries, there are no additional steps needed if an administrator has followed the steps for migrating trusted service entries as described in Migration from Communications Server 2007 R2 to Lync Server 2010.

If you have a UCMA 2.0 application deployed on multiple computers against a hardware load balancer, and you run the migration tools (ApplicationProvisioner.exe), the application is represented as a legacy external trusted application pool (UCMA 2.0 application pool) in the backward compatibility site section of the topology document. In the topology document, the **Cluster** FQDN is set to the FQDN of the hardware load balancer, and each **Machine** FQDN is set to the FQDN of one of the computers in the pool.

If you have a UCMA 2.0 application deployed on a single computer, and you run the migration tools, the application is represented as a legacy external trusted application pool (UCMA 2.0 application pool) in the backward compatibility site section of topology document. In the topology document, the **Cluster** FQDN is the same as the **Machine** FQDN.

It is possible that a UCMA 2.0 application is deployed against in a mixed - environment after the initial migration steps have been performed. In this case the migration coexistence steps to migrate trusted service entries must be redone to make the trusted service entries known to Lync Server 2010. Note that this operation migrates only the trusted service entries and route-related settings. There might be application-specific settings for which you must consult the ISV who provided the application.

## Scenario 1—Coexistence of a UCMA 2.0 application with multiple Communications Server versions

A UCMA 2.0 application is targeted at an pool in a mixed –Lync Server 2010 topology. The following steps make it possible for users homed on a Lync Server 2010 pool to call into this application.

The following illustration shows this topology.

**UCMA 2.0 application in mixed topology**
  
![Migration Scenario 1](images/Dn466131.Migr_Scenario1(Office.16).png "Migration Scenario 1")

1.  Ensure that the account used for performing the steps is a member of the **RTCUniversalServerAdmins** group.

2.  Ensure that Lync Server 2010 is deployed and working.

3.  Verify that the UCMA 2.0 application is running and is configured correctly.

4. **Migrate trusted service entries using Microsoft Lync Server 2010 Topology Builder or PowerShell cmdlets.** This is the key step of this scenario. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

5.  Verify that the trusted service entries in the previous step were successfully migrated. See the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

6.  Verify that the UCMA 2.0 application works as expected and that Lync Server 2010 users are able to communicate with the application.

7.  Continue to use WMI-based management tools (such as ApplicationProvisioner.exe) to manage the application contact objects. For more information, see [Modifying ApplicationProvisioner.exe sample code](modifying-applicationprovisioner-exe-sample-code.md).


> [!NOTE]
> If a second UCMA 2.0 application is deployed in a mixed -Lync Server 2010 environment in which the trusted service entries for an existing UCMA 2.0 application have been migrated, you will need to redo steps 3 through 7 in the preceding procedure for the new application.

## Scenario 2—Upgrading a UCMA 2.0 application to UCMA 3.0 after coexistence

To upgrade a UCMA 2.0 application to one that is written with UCMA 3.0, the customer must create a new UCMA 3.0 trusted application pool. After the application bits are deployed, the administrator can use PowerShell cmdlets to move contact objects tied to the existing UCMA 2.0 application pool and so that they now refer to the new application pool and Lync Server 2010 Registrar.

The following illustration shows the topology during the upgrade.

**Before upgrade**

![Migration Scenario 2](images/Dn466131.Migr_Scenario2(Office.16).png "Migration Scenario 2")

The following steps are the **recommended upgrade approach**.

1.  Verify that all of the steps in scenario 1 completed successfully.

2.  Stop the UCMA 2.0 application so that configuration changes can be made.

3.  Create a new pool, either by using Microsoft Lync Server 2010 Topology Builder, or by running the **New-CsTrustedApplicationPool** cmdlet followed by the **New-CsTrustedApplicationComputer** cmdlet. For more information, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

4.  Create a new application by using the **New-CsTrustedApplication** cmdlet. The **ApplicationId** should be the same as the UCMA 2.0 application’s **ApplicationName**.

5.  Run the **Enable-CsTopology** cmdlet to create the appropriate trusted service entries in Active Directory for interoperability with .
    
    `Enable-CsTopology`

6.  Stop Lync Server 2010 Front End service and then restart it.

7.  Install the application software in the trusted application pool created in step 3. Stop at the point you are asked to create a contact object for the application. For more information, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

8.  Run the **Move-CsApplicationEndpoint** cmdlet to move the contact object from the UCMA 2.0 application pool to the UCMA 3.0 application pool. Use the –**Force** parameter. Run this cmdlet with the needed values for each contact object that your application uses.
    
    `Run Move-CsApplicationEndpoint -Identity sip:endpoint1@contoso.com -TargetApplicationPool UCMA3.0ApplicationPool.contoso.com -Force`
    
    The **Move-CsApplicationEndpoint** cmdlet moves an existing endpoint contact object in Active Directory from an registrar pool to a Lync Server 2010 registrar pool, or from one Lync Server 2010 registrar pool to another. The target pool must be a trusted application pool, and the application associated with the given endpoint must exist in the target pool.

9.  Stop and then restart the Lync Server 2010 Front End service before launching the UCMA 3.0 application.

10. Use the WMI-based management tools (ApplicationProvisioner.exe) to remove the UCMA 2.0 application servers. For more information, see [Modifying ApplicationProvisioner.exe sample code](modifying-applicationprovisioner-exe-sample-code.md).

11. Migrate the trusted service entries using Microsoft Lync Server 2010 Topology Builder or the PowerShell cmdlets. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

12. Verify the following.
    
    1.  Users are able to communicate with the new version of your application.
    
    2.  You are now able to use Lync Server 2010 trusted application cmdlets to manage the existing contact objects and the new contact objects.

The following illustration shows the topology after the upgrade has taken place. The computers used in the UCMA 2.0 trusted application pool can be reused for other purposes, if desired.

**After upgrade**

![Migration Scenario 2 - after](images/Dn466131.Migr_Scenario2b(Office.16).jpg "Migration Scenario 2 - after")

## Scenario 3—In-place migration of a UCMA 2.0 application

After all users have been migrated from an pool to a Lync Server 2010 pool, consider decommissioning the pool. Before you decommission, consider routing to your applications through a Lync Server 2010 Registrar pool.

The **Move-CsApplicationEndpoint** cmdlet can be used to populate the necessary attributes of an existing application endpoint contact object in Active Directory so that routing occurs through the Lync Server 2010 Registrar.

The following illustration shows this topology after the UCMA 2.0 application has been migrated to run against Lync Server 2010.

**Migrating a UCMA 2.0 application to Lync Server 2010**

![Migration Scenario 3](images/Dn466131.Migr_Scenario3(Office.16).jpg "Migration Scenario 3")

1.  Verify that the steps in scenario 1 have completed successfully.

2.  Associate the existing backward-compatible UCMA 2.0 application pool with the Lync Server 2010 Registrar. See the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

3.  Run the **Move-CsApplicationEndpoint** cmdlet to update the contact object so that routing occurs through the Lync Server 2010 Registrar. Use the –**Force** parameter.
    
    You will need to run this cmdlet with the needed values for each contact object that your application uses.
    
    `Move-CsApplicationEndpoint -Identity sip:endpoint1@contoso.com -TargetApplicationPool ApplicationPool.contoso.com –Force`
    
    In the preceding example, no move is occurring, so the pool represented by the **TargetApplicationPool** parameter is also the source application pool.
    
    Each time you create a new contact object using the WMI tools (ApplicationProvisioner.exe), you must perform this step. If it is omitted, the contact object properties required by the Lync Server 2010 Registrar for routing are not populated. You cannot use Lync Server 2010 cmdlets to create new contact objects for your application, because the application pool is still a legacy pool.

4.  Verify that the **Move-CsApplicationEndpoint** cmdlet was successful by running the following cmdlet, and verifying that the output of the Registrar property points to the Lync Server 2010 Registrar.
    
    `Get-CsApplicationEndpoint sip:endpoint1@contoso.com | Select-Object *`
    
    Although the **New-CsTrustedApplicationEndpoint**, **Set-CsTrustedApplicationEndpoint**, and **Remove-CsTrustedApplicationEndpoint** cmdlets cannot be used to manage legacy application pools, the **Get-CsTrustedApplicationEndpoint** cmdlet can be used to verify that the contact object entries for the **ApplicationEndpoint** have been created correctly. For more information about any of these cmdlets, run `get-help <cmdlet>`.

5.  Verify that the application still works on each Front End, even when the Front End service is stopped.

6.  Continue to use WMI-based management tools (such as ApplicationProvisioner.exe) to add and manage the application contact objects. For more information, see [Modifying ApplicationProvisioner.exe sample code](modifying-applicationprovisioner-exe-sample-code.md).
    
   **Each time you add a new contact object, perform steps 3 and 4.**
    
   > [!NOTE]
   > This point is emphasized to remind you to perform the verifications in these steps when a new contact object is added using PowerShell. PowerShell might not be available locally on the computer from which you are running the WMI tool for activating new contact objects.

## Scenario 4—Direct deployment of UCMA 2.0 application against pure Lync Server 2010

**This scenario is supported, but not recommended.** A UCMA 2.0 application is connected directly to Lync Server 2010. Check with the application vendor to see whether such a deployment is certified.

The following illustration shows this topology.

**Deploying a UCMA 2.0 application against Lync Server 2010**
  
![Migration Scenario 4](images/Dn466131.Migr_Scenario4(Office.16).jpg "Migration Scenario 4")

1.  Get OCSCore.MSI from the installer. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

2.  Install the UCMA 2.0 application. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

3.  Add allowed domains using WBemTest. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

4.  Migrate the trusted service entries using Microsoft Lync Server 2010 Topology Builder or PowerShell cmdlets. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

5.  Run the **Move-CsApplicationEndpoint** cmdlet to update the contact object so that routing occurs through the Lync Server 2010 Registrar. Use the –**Force** parameter. You will need to the following step with the needed values for each contact object that your application uses.
    
    `Move-CsApplicationEndpoint -Identity sip:endpoint1@contoso.com -TargetApplicationPool ApplicationPool.contoso.com –Force`
    
    In the preceding example, no move is occurring, so the pool represented by the **TargetApplicationPool** parameter is also the source application pool.
    
    Each time you create a new contact object using the WMI tools (ApplicationProvisioner.exe), you must perform this step. If it is omitted, the contact object properties required by the Lync Server 2010 Registrar for routing are not populated. You cannot use Lync Server 2010 cmdlets to create new contact objects for your application, because the application pool is still a legacy pool.

6.  Verify that the **Move-CsApplicationEndpoint** cmdlet was successful by running the following cmdlet, and verifying that the output of the Registrar property points to the Lync Server 2010 Registrar.
    
    Get-CsApplicationEndpoint sip:endpoint1@contoso.com | Select-Object \*

7.  Verify the following.
    
    1.  Users are able to communicate with the application.
    
    2.  You are still able to use the WMI tool (ApplicationProvisioner.exe) to create new additional contact objects.
        
        Each time you add a new contact object, steps 4 and 5 must be performed.

## Scenario 5—Upgrading a UCMA 2.0 application to UCMA 3.0 after in-place migration or direct deployment of UCMA 2.0 application

This scenario is similar to scenario 2 except that the UCMA 2.0 pool points to a Lync Server 2010 Registrar. This poses a challenge in creating a new swap pool, because two application pools cannot have the same application pointing to the Registrar.

The following illustration shows the topology during the upgrade.

**Before upgrade**
  
![Migration Scenario 5](images/Dn466131.Migr_Scenario5(Office.16).jpg "Migration Scenario 5")

1.  Verify that the steps in scenario 3 or scenario 4 have completed successfully.

2.  Stop the UCMA 2.0 application so that configuration changes can be made.

3.  Disassociate the UCMA 2.0 application pool from the Lync Server 2010 Registrar, by running the **Set-CsTrustedApplicationPool** cmdlet, using the **Identity** of the UCMA 2.0 application pool and **Registrar** as **$null**.

4.  Create a new pool, either by using Microsoft Lync Server 2010 Topology Builder, or by using the **New-CsTrustedApplicationPool** cmdlet, followed by the **New-CsTrustedApplicationComputer** cmdlet.

5.  Create a new application by using the **New-CsTrustedApplication** cmdlet. For a UCMA 2.0 application, the **ApplicationId** should be the same as the **ApplicationName**.

6.  Run the **Enable-CsTopology** cmdlet to create the appropriate trusted service entries in Active Directory for interoperability with .
    
    `Enable-CsTopology`

7.  Run the **Move-CsApplicationEndpoint** cmdlet to move the contact object from the UCMA 2.0 application pool to the UCMA 3.0 application pool. Run this cmdlet with the appropriate values for each contact object that your application uses.
    
    `Move-CsApplicationEndpoint -Identity sip:endpoint1@contoso.com -TargetApplicationPool UCMA3.0ApplicationPool.contoso.com`

8.  Verify that the **Move-CsApplicationEndpoint** cmdlet was successful by running the following cmdlet, and verifying that the output of the Registrar property points to the Lync Server 2010 Registrar.
    
    `Get-CsApplicationEndpoint sip:endpoint1@contoso.com | Select-Object *`

9.  Stop and then restart the Lync Server 2010 Front End service before launching the UCMA 3.0 application.

10. Use the WMI-based management tools (ApplicationProvisioner.exe) to remove the UCMA 2.0 application servers. For more information, see [Modifying ApplicationProvisioner.exe sample code](modifying-applicationprovisioner-exe-sample-code.md).

11. Migrate the trusted service entries using Microsoft Lync Server 2010 Topology Builder or the PowerShell cmdlets. For more information, see the relevant section in [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md).

12. Verify the following.
    
    1.  Users are able to communicate with the new version of your application.
    
    2.  You are able to use Lync Server 2010 trusted application cmdlets to manage the existing contact objects and the new contact objects.

The following illustration shows the topology after the upgrade has taken place. The computers used in the UCMA 2.0 trusted application pool can be reused for other purposes, if desired.

**After upgrade**

![Migration Scenario 5 - after](images/Dn466131.Migr_Scenario5b(Office.16).jpg "Migration Scenario 5 - after")


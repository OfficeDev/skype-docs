---
description: Learn how to deactivate an application with best practice procedure.
title: Deactivation best practices
TOCTitle: Deactivation best practices
ms:assetid: ac1ed606-5d62-463f-9891-1308b3cf31dc
ms:mtpsurl: https://msdn.microsoft.com/library/Dn466141(v=office.16)
ms:contentKeyID: 65240054
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Deactivation best practices

**Applies to**: Skype for Business 2015

An application can be deactivated by following the steps in the following procedure.

### To deactivate an application

1. Running as a Skype for Business Server Administrator, launch Skype for Business Server Management Shell.
    
2. On the **Start** menu, select **All Programs**, select **Skype for Business Server 2015**, and then click **Skype for Business Server Management Shell**.

3. Remove the Active Directory contacts and [ApplicationEndpoint](/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?) instances that you no longer need by running the following PowerShell cmdlet:

   ```powershell
    Remove-CsTrustedApplicationEndpoint -Identity sip:ExternalApp@mydomain.com
   ```    

   > [!NOTE]
   > Use the appropriate identity for your **ApplicationEndpoint** instance.

4. Remove the application service port entries that you no longer need by running the following PowerShell cmdlet.
    
   ```powershell   
    Remove-CSTrustedApplication -Identity "TrustedApps.contoso.com/urn:application:ucmasampleapplication"
   ```

   > [!NOTE]
   > This cmdlet also cleans up any contact objects or **ApplicationEndpoint** instances tied to the application.

5. If the computer hosting your application from your topology is no longer needed in the topology, remove it using the following PowerShell cmdlet.
    
   ```powershell
    Remove-CsTrustedApplicationComputer -identity "MyAppBox.contoso.com"
   ```    

   > [!NOTE]
   > Use the appropriate identity for the computer being removed.

6. If the trusted application pool is no longer needed, remove it using the following PowerShell cmdlet.

   ```powershell    
     Remove-CsTrustedApplicationPool -identity "MyAppPool.contoso.com"
   ```
   
   > [!NOTE]
   > Use the appropriate identity for your pool.

   > [!NOTE]
   > This cmdlet also cleans up any contact objects or **ApplicationEndpoint** instances tied to the pool.

7. Run the Enable-CsTopology cmdlet to finalize the changes to the topology.

   ```powershell
     Enable-CsTopology
   ```

<br/>

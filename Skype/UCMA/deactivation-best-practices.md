---
title: Deactivation best practices
TOCTitle: Deactivation best practices
ms:assetid: ac1ed606-5d62-463f-9891-1308b3cf31dc
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466141(v=office.16)
ms:contentKeyID: 65240054
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Deactivation best practices


**Applies to**: Skype for Business 2015

An application can be deactivated by following the steps in the following procedure.

### To deactivate an application

1.  Running as a Skype for Business Server Administrator, launch Skype for Business Server Management Shell.
    
    On the **Start** menu, select **All Programs**, select **Skype for Business Server 2015**, and then click **Skype for Business Server Management Shell**.

2.  Remove the Active Directory contacts and [ApplicationEndpoint](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.applicationendpoint?view=ucma-api) instances that you no longer need by running following PowerShell cmdlet.
    
    Remove-CsTrustedApplicationEndpoint -Identity sip:ExternalApp@mydomain.com
    

    > [!NOTE]
    > <P>Use the appropriate identity for your <STRONG>ApplicationEndpoint</STRONG> instance.</P>



3.  Remove the application service port entries that you no longer need by running the following PowerShell cmdlet.
    
    Remove-CSTrustedApplication -Identity "TrustedApps.contoso.com/urn:application:ucmasampleapplication"
    

    > [!NOTE]
    > <P>This cmdlet also cleans up any contact objects or <STRONG>ApplicationEndpoint</STRONG> instances tied to the application.</P>



4.  If the computer hosting your application from your topology is no longer needed in the topology, remove it using the following PowerShell cmdlet.
    
    Remove-CsTrustedApplicationComputer -identity "MyAppBox.contoso.com"
    

    > [!NOTE]
    > <P>Use the appropriate identity for the computer being removed.</P>



5.  If the trusted application pool is no longer needed, remove it using the following PowerShell cmdlet.
    
    Remove-CsTrustedApplicationPool -identity "MyAppPool.contoso.com"
    

    > [!NOTE]
    > <P>Use the appropriate identity for your pool.</P>

    

    > [!NOTE]
    > <P>This cmdlet also cleans up any contact objects or <STRONG>ApplicationEndpoint</STRONG> instances tied to the pool.</P>



6.  Run the Enable-CsTopology cmdlet to finalize the changes to the topology.
    
    Enable-CsTopology


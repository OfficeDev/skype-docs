---
title: Activating a manually-provisioned application
TOCTitle: Activating a manually-provisioned application
ms:assetid: 26903c64-1b34-4473-9d9e-67070655890c
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466122(v=office.16)
ms:contentKeyID: 65240052
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Activating a manually-provisioned application


_**Applies to:** Skype for Business 2015_

Like auto-provisioned applications, manually-provisioned applications also require a Skype for Business Server 2015 topology and Active Directory contact object settings, but do not take advantage of UCMA auto-provisioning. Unlike auto-provisioned applications, manually-provisioned applications do not require a local Central Management Store replica, but instead provision the Microsoft Unified Communications Managed API (UCMA) platform and application endpoints using offline configuration. There is no need to install and enable Central Management Store replication on the computers running UCMA-based applications.

The following procedure shows the steps required to manually provision an application. This topic does not cover how to select a certificate for use with the application.

### To provision an application using manual provisioning

1.  Launch Skype for Business Server Management Shell.
    
    On the **Start** menu, select **All Programs**, **Skype for Business Server 2015**, and then click **Skype for Business Server Management Shell**.

2.  In Skype for Business Server Management Shell, run the following PowerShell cmdlet. It is assumed that you have already run the **New-CsTrustedApplication** cmdlet.
    
    $a = Get-CsTrustedApplication -identity "client.contoso.com/urn:application:ucmasampleapplication"
    

    > [!NOTE]
    > <P>Use the identity of your application.</P>



3.  In Skype for Business Server Management Shell, run the following PowerShell cmdlet.
    
    $a.ComputerGruus
    
    The output from the preceding cmdlet should be similar to the following.
    
        Fqdn                                    Gruu
        ----                                    ----
        client.contoso.com                      sip:client.contoso.com@contoso.com; gruu; Opaque=srvr:ucmasampleapplication:yDUhQWqi81WfJXFUvMAwtwAA

4.  Copy the GRUU string for the computer FQDN, taking care to remove any line breaks. You will need to provide this value and additional information when the [ServerPlatformSettings](https://msdn.microsoft.com/en-us/library/hh382156\(v=office.16\)) instance is constructed.
    

    > [!NOTE]
    > <P>A service GRUU can be used in a single-computer pool (a pool in which the computer FQDN is the same as the pool FQDN). However, if additional computers are added to that pool, the use of the service GRUU should be discontinued and the computer GRUU used instead.</P>



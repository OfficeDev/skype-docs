---
title: Deploying UCMA 4.0 and UCMA 5.0 Applications in a Skype for Business Server 2015 Environment
TOCTitle: Deploying UCMA 4.0 and UCMA 5.0 Applications in a Skype for Business Server 2015 Environment
ms:assetid: 798e1cf5-9ad5-4aaf-8d88-683052d86183
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn962207(v=office.16)
ms:contentKeyID: 65240061
ms.date: 03/07/2017
mtps_version: v=office.16
---

# Deploying UCMA 4.0 and UCMA 5.0 Applications in a Skype for Business Server 2015 Environment


Skype for Business Server 2015 supports applications built with UCMA 5.0, as well as legacy UCMA 4.0 applications. The process for deploying UCMA applications has not changed much from Lync Server 2013. This document describes some deployment scenarios to consider when planning for your new environment.

## Upgrading from Lync Server 2013 to Skype for Business Server 2015

### Existing UCMA 4.0 applications

When you are upgrading your existing Lync Server 2013 environment to Skype for Business Server 2015, you should not upgrade your existing Lync 2013 trusted application pools. You can go through the upgrade process for all Skype for Business Server 2015 components—except for the Lync 2013 trusted application pools. Lync Server 2013 trusted application pools will continue to function within your Skype for Business Server 2015 environment.

### UCMA 5.0 applications

When you upgrade your Lync Server 2013 environment, you can create new trusted application pools for Skype for Business Server 2015. Your UCMA 5.0 applications should only run on trusted application servers that are in your new Skype for Business Server 2015 application pools.

Remember to run the Skype for Business Server 2015 Deployment Wizard on all new trusted application servers in the pools. Or alternatively, you can use the Skype for Business Server Management Shell to run PowerShell commands that configure your pool and servers.

## Installing a clean Skype for Business Server 2015 environment

### Existing UCMA 4.0 Applications

When creating a new Skype for Business Server 2015 environment, you can run existing UCMA 4.0 applications by configuring a Lync Server 2013 trusted application pool and by configuring each server in the pool to be a trusted application server. Follow the directions below to first configure a trusted application pool of server, and then to configure each server.

### Configuring a Lync Server 2013 Trusted Application Pool

Configure a Lync Server 2013 trusted application pool by choosing one of these options. The first option uses a UI, and the second option uses PowerShell commands.

#### Option 1: Topology Builder

You can use the Skype for Business Topology Builder user interface to configure the trusted application pool. Follow these steps:

1.  Open Skype for Business Topology Builder with Skype for Business administration permissions, and browse to the Lync Server 2013 node.

2.  Under Trusted Application Pools, create a new application pool.

3.  Publish the topology.

#### Option 2: Management Shell

You can use a combination of the Lync Server Management Shell and the Skype for Business Server Management Shell to configure the trusted application pool. Follow these steps:

1.  Install the UCMA 4.0 Runtime on one of the servers that will be in the trusted application pool.

2.  Browse to the following directory: %SystemDrive%\\ProgramData\\Microsoft\\Lync Server\\Deployment\\cache\\5.0.8308.0\\Setup

3.  Run OCSCore.msi. This will install the Lync Server Management Shell

4.  Open the Lync Server Management Shell with Skype for Business administrator permissions, and run the following command. You can view the help documentation to find out more.
    
        New-CsTrustedApplicationPool -Identity <Name> -Registar <Pool name>

5.  Now that your Lync 2013 trusted application pool is created, you will need to publish the pool from a Skype for Business Server Management Shell. Open the shell with Skype for Business management permissions and run the following command:
    
        Publish-CsTopology

### Configuring Trusted Application Servers

There are two options for configuring the trusted application servers that will run your UCMA 4.0 application. You can either use a wizard with a user interface, or you can use a series of PowerShell commands. Choose one of the two options below:

#### Option 1: Lync Server 2013 Deployment Wizard

You can run the Lync Server 2013 Deployment Wizard to configure a trusted application server. This method is useful for a small number of servers, such as a developer environment.

Follow these steps for configuring a server environment with just the runtime. This is useful for a production environment without developer tools.

1.  Install the UCMA 4.0 Runtime.

2.  Browse to the following directory: %SystemDrive%\\ProgramData\\Microsoft\\Lync Server\\Deployment\\cache\\5.0.8308.0\\Setup

3.  Run OCSCore.msi. This will install the Lync Server 2013 Deployment Wizard, as well as other tools.

4.  Run the Lync Server 2013 Deployment Wizard with Skype for Business management permissions, following the steps for Configuring Active Directory, as well as all the steps for Install or Update Lync Server System.
    
    Note: The wizard may prompt you to reboot and run a second time.

Alternately, follow these steps for configuring a developer environment.

1.  Install the UCMA 4.0 SDK.

2.  Run the Lync Server 2013 Deployment Wizard with Skype for Business management permissions, following the steps for Configuring Active Directory, as well as all the steps for Install or Update Lync Server System.

#### Option 2: Management Shell

You can use the Lync Server Management Shell to configure a trusted application server. Consider automating these steps if configuring multiple servers.

1.  Install the UCMA 4.0 Runtime.

2.  Browse to the following directory: %SystemDrive%\\ProgramData\\Microsoft\\Lync Server\\Deployment\\cache\\5.0.8308.0\\Setup

3.  Run OCSCore.msi. This will install the Lync Server Management Shell, as well as other tools.

4.  Open the Lync Server Management Shell with Skype for Business management permissions, and run the following commands:
    
    1.  ``` 
        cd C:\Program Files\Microsoft Lync Server 2013\Deployment
        ```
    
    2.  ``` 
        Bootstrapper.exe /BootstrapLocalMgmt /MinCache
        ```
        
        NOTE: You may need to reboot and run bootstrapper a second time.
    
    3.  ``` 
        Enable-CSReplica
        ```
    
    4.  ``` 
        Start-Service CSREPLICA
        ```
    
    5.  ``` 
        Invoke-CsManagementStoreReplication -Force
        ```
    
    6.  ``` 
        Request-CSCertificate -New -Type default -CA "dc.contoso.com\<certificate authority>" –Verbose
        ```
        
        NOTE: From the resulting output, copy the thumbprint value.
    
    7.  ``` 
        Set-CsCertificate -Type Default –Thumbprint <thumbprint>
        ```

5.  Run Enable-CsTopology.

### UCMA 5.0 Applications

For UCMA 5.0 applications, you can follow the usual process for setting up a trusted application pool in Topology Builder under the Skype for Business Server 2015 node. Run Skype for Business Server 2015 Deployment Wizard on all trusted application servers, including requesting and installing a certificate. From a Skype for Business Server 2015 Management Shell, you can run New-CsTrustedApplication and New-CsTrustedApplicationEndpoint to finish configuring your application.

## Mixed Environments

You can have a mix of UCMA 4.0 and UCMA 5.0 applications by configuring trusted application pools servers, applications, and endpoints as described above. Make sure that your UCMA 4.0 applications are associated with Lync Server 2013 trusted application pools, and that your UCMA 5.0 applications are associated with Skype for Business Server 2015 application pools.

## Conclusion

Skype for Business Server 2015 supports the latest UCMA 5.0 applications, as well as legacy UCMA 4.0 applications. With a little planning, you do not need to rebuild UCMA 4.0 applications—they can still run in a Skype for Business Server 2015 environment.


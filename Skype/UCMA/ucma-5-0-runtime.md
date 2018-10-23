---
title: UCMA 5.0 Runtime
TOCTitle: UCMA 5.0 Runtime
ms:assetid: 90679514-7bec-4a91-be50-f1020080a60a
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466090(v=office.16)
ms:contentKeyID: 65240032
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA 5.0 Runtime

**Applies to**: Skype for Business 2015

An administrator who is preparing to install a third-party UCMA-enabled application on a deployment computer must download and install UcmaRuntimeSetup.exe. Installing Microsoft Unified Communications Managed API 5.0 Runtime provides all of the UCMA-related functionality needed by a UCMA-enabled application.

## UCMA 5.0 Runtime requirements

The following are the hardware and software requirements for installing UCMA 5.0 Runtime:

- The target platform must be a 64-bit computer.

- Supported operating systems:
    
  - Windows 2012 Server R2   
  - Windows 2012 Server   
  - Windows Server 2008 R2 Service Pack 1   
  - Windows 8.1   
  - Windows 8   
  - Windows 7 Service Pack 1 (SP1) (64-bit): Home Premium, Professional, or Ultimate editions
    
  > [!NOTE]
  > Auto-provisioned applications cannot be deployed on computers running Windows 8.1, Windows 8, Windows 7 or Windows Vista.

- Microsoft .NET Framework 4.5 (Full Profile)
    
  - Windows 8 and later versions include .NET Framework 4.5.    
  - Windows Server 2008 R2 Service Pack 1 includes .NET Framework 4.5.    
  - Windows 7 includes .NET Framework 3.5, so you must update to .NET Framework 4.5.

- Antivirus software
    
  Antivirus software can sometimes make such heavy demands on the CPU as to cause audio glitches. For this reason, antivirus software is not recommended for computers running Skype for Business Server 2015. For optimal performance for a server that must run antivirus software, include all Skype for Business Server 2015 computers in the antivirus software’s exception list.

## Installing UCMA 5.0 Runtime and related components

This section describes how to install the Microsoft Unified Communications Managed API 5.0 Runtime and related components.

You must have elevated permissions to install UCMA 5.0 Runtime.

> [!NOTE]
> UCMA 5.0 Runtime is installed automatically when you install UCMA 5.0 SDK. For more information, see [Installing UCMA 5.0 SDK](installing-ucma-5-0-sdk.md).

### Install UCMA 5.0 Runtime

1.  Download UcmaRuntimeSetup.exe.

2.  Launch UcmaRuntimeSetup.exe and follow the screens to accept the End-User License Agreement (EULA). The setup wizard will install all the necessary components.

3.  Follow the instructions on the screen to complete the installation.


> [!NOTE]
> - UcmaRuntimeSetup.exe installs the English versions of the Speech Recognition and Text-to-Speech engines. The final screen of the installer provides a link to download additional engines for other languages.
> - Additional steps are required to install Skype for Business Server 2015, Core Components (OCSCore.msi). These steps are described later in this topic.

### UCMA 5.0 Runtime components

UcmaRuntimeSetup.exe installs the following components:

- Microsoft  VC++ 2013 Redistributable (x64) 12.0.21005
    
  Microsoft Unified Communications Managed API 5.0 Runtime is built on .NET Framework 4.5 and Visual C++ 2013 Redistributable Package (x64).

- Microsoft Server Speech Platform Runtime (x64) package

- Microsoft Server Speech Recognition Language – TELE (en-US)

- Microsoft Server Speech Text To Speech Voice (en-US, Helen)

- Skype for Business Server 2015, Bootstrapper Prerequisites Installer Package
    
  Copies Skype for Business Server 2015 Core Components MSI (OCSCore.msi) and its prerequisites. OCSCore.msi enables running Lync Server-specific PowerShell cmdlets from the local computer. OCSCore.msi also enables the installation of the local Central Management Store replica, which is required by auto-provisioned UCMA applications.

> [!NOTE]
> Side-by-side installations of UCMA 3.0, UCMA 4.0, and UCMA 5.0 are not supported.

OCSCore.msi must be installed by using Bootstrapper Prerequisites Installer Package installed by UcmaRuntimeSetup.exe.

### Install Skype for Business Server 2015 components

1. Confirm that Microsoft PowerShell 3.0 RTM is installed on the deployment computer.
    
  - Windows Server 2008 R2 Service Pack 1
        
    Download WINDOWS6.0-KB2506146-x64.MSU from [Windows Management Framework 3.0](https://www.microsoft.com/download/details.aspx?id=34595).
    
  - Windows 7
        
    Download WINDOWS6.1-KB2506143-x64.MSU from [Windows Management Framework 3.0](https://www.microsoft.com/download/details.aspx?id=34595).
    
  - Windows 8/Windows Server 2012
        
    This operating system version comes with PowerShell 3.0.

2. Find OCSCore.msi by navigating to: 

   %Installation drive%\\ProgramData\\Microsoft\\Skype for Business Server\\Deployment\\cache\\\<Build Version\>\\Setup\\ .

3. Launch OCSCore.msi and use the default settings in the wizard.

For information about activating a UCMA application, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md).

## Uninstalling UCMA 5.0 Runtime and bootstrapper prerequisites installer

When a customer application is uninstalled, the application uninstall procedure must not automatically uninstall any of the required components (such as .NET Framework 4.5, Visual C++ 2013 Redistributable Package, or UCMA 5.0 Runtime), because other applications might require them. An administrator who has ensured that there are no dependencies can follow the instructions in the following procedure to uninstall UCMA 5.0 Runtime.

### To uninstall UCMA 5.0 Runtime and bootstrapper prerequisites installer

1.  Navigate to **Programs and Features** in **Control Panel**.

2.  On the right-click menu for the Unified Communications Managed API 5.0, Runtime entry, select **Uninstall**.

3.  A wizard titled "UCMA 5.0, Runtime Uninstallation" appears. Follow the instructions on the screen to complete uninstallation.

4.  On the right-click menu for the Skype for Business Server 2015,  Bootstrapper Prerequisites Installer Package entry, select **Uninstall** to remove the installer package.

> [!NOTE]
> Uninstalling the Bootstrapper Prerequisites Installer Package does not automatically uninstall software components that are individually installed by the user, such as Skype for Business Server 2015 Core Components (OCSCore.msi) and others.

## UCMA 5.0 Runtime detection

The following WiX code can be used to detect whether UCMA 5.0 Runtime has been installed on a computer.

```xml
    <Fragment>
      <Property Id="PROPERTY_UCMA_RUNTIME_INSTALLED" Secure="yes" />
      <Upgrade Id="{6F863046-E64C-453C-B28C-0BF9C10381DE}">
          <UpgradeVersion Minimum="6.0.9319.0" OnlyDetect="yes" Property="UCMA_PROPERTY_UCMA_RUNTIME_INSTALLED" />
      </Upgrade>
     
        <!-- Verify that the correct version of UCMA Runtime is installed -->
        <Condition Message="[ProductName] installation requires that Microsoft Unified Communications Managed API 5.0, [BuildNumber] already installed. Installation cannot continue.">
            <![CDATA[Installed Or PATCH Or MSIPATCHREMOVE Or EVALTOFULL Or REMOVE Or UCMA_PROPERTY_UCMA_RUNTIME_INSTALLED]]>
        </Condition>
    </Fragment>
```

## See also

- [Microsoft.Rtc.Collaboration Namespace](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration?view=ucma-api-5.0)
- [Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml Namespace](https://docs.microsoft.com/dotnet/api/Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml?view=ucma-voice)
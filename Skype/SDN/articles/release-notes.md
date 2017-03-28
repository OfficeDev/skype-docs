---
title: Skype for Business SDN Interface Release Notes
ms.prod: SKYPE
ms.assetid: 726f613b-4639-4433-85db-50a572778ab8
---


# Skype for Business SDN Interface Release Notes

 **Last modified:** February 27, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

The Microsoft Skype for Business SDN Interface provides an interface for network controllers or network management systems to receive call and quality data to monitor and analyze network traffic in order to optimize the Skype for Business media stream quality. 
  
    
    

The Skype for Business SDN Interface version 3.0 includes several new features over the previous releases. 
## New features in Skype for Business SDN Interface version 3.0, build 7.0.1459.1

Following is a summary of the key new features in Skype for Business SDN Interface version 3.0 
  
    
    

- **New WiFi metrics available in Schema D messages:** Additional Wi-Fi metrics have been added to QualityUpdate and IncallQuality messages. See sections Schema Extensions and [Subscriber configuration settings](configuring-sdn-interface-using-the-command-prompt.md#bk_subscriber) for details.
    
  
- **Relaxed Schema Validation:** Schema validation of messages from Dialog Listener to SDN Manager is now disabled by default. Minimal validation is performed on messages (specifically client QoE messages). Schema validation can be enabled by setting the SDN Manager parameter minimalschemavalidation=false. Schema validation errors found will only be logged. This setting's default has been changed to address an increasing number of schema validation errors due to recent changes in the SfB client's QoE structure.
    
  
- **Support for TLS 1.2:** SSL connections between SDN Manager and Dialog Listener now require TLS1.2 support. SSL connections between SDN manager and subscribers now support TLS1.2 and TLS1.1. SSL 3.0 is, for security reasons, rejected. Some older OS versions might need to adjustments.
    
  
- **Video-based Screen Sharing (VbSS) support:** Application sharing streams will continue to be measured based on the 500kbps/1Mbps limit for poor, bad, good in end of call stream quality. If VbSS is used, AppSharing data streams will check different AppliedBandwidthLimit limits.
    
  
- **Customer Experience reporting:** The SDN Interface will report anonymous usage data as part of the Customer Experience program. No user PII or organizational data is collected.
    
  
- **SDN Manager PowerShell additions:** New SDN Manager PowerShell commands have been added: Get-CsSdnCatalog, Get-CsSdnLog, and Get-CsSdnSubscriberStatus. See [PowerShell Provisioning](powershell-provisioning.md) for details..
    
  

## Bug Fixes in Skype for Business SDN Interface version 3.0

The following bugs have been fixed in SDN 3.0: 
  
    
    

-  In a Federation call scenario, some messages are not transmitted
    
  
-  During retransmission, some call ended events are incorrectly re-ordered
    
  
- SDN Manager Performance Counter '# calls completed' would not account for some calls leading to an incorrect counter value. 
    
  
-  Logs created for replay do not account for some fields in the message
    
  
-  Trunk field was inconsistently reported in QoE and Signaling messages were being filtered
    
  
- Log level Debug did not have sufficient detail to troubleshoot an issue 
    
  
-  Fixed association of protocols name in signaling messages with different capitalization.
    
  
- SDN Manager does not accept all 2xx code to indicate successful receipt of a message (only accepts 200 OK) 
    
  
-  AppliedBandwidth limits in VbSS scenario Is not being handled correctly
    
  

## Notes and known issues in Skype for Business SDN Interface version 3.0


- LDL Unattended install parameters are not validated. 
    
  
- There is no way to specify an output file for all Subscribers or Listeners. 
    
  
- Previous instances of REDIS allowed case-sensitive subscriber names. These are no longer supported, and are ignored. Users must either drop the entire REDIS data cache OR remove the case-sensitive subscriber name by matching the case-sensitivity. 
    
  
- Dialog Listener install, by default, configures a Service startup dependency on Skype for Business Server Front-End service. The installer then attempts to start the LDL service, which will attempt to start the FE service. If this is undesirable, the workaround is to add the installer parameter 'SKIPREGISTRATION=1' to the invocation of msiexec.exe. 
    
  
- The contents of the MSDiagnostics field reported to subscribers may contain PII, SDN Interface does not obfuscate this field. 
    
    If the new SDN Manager uses a new MSPL script it is not updated in active Dialog Listeners until that Dialog Listener settings are updated. Certain settings need to be updated in the Dialog Listeners after the SDN Manager has been upgraded to 3.0. Please either restart the Dialog Listener services, or as a simple trick, add a random Listener setting or change some setting. 
    
  
- LSM Unattended install does not check for missing required parameters Subsequent errors provide little information. 
    
    For example, a new SQL database install, but with 'ComputerName' is missing: 

```powershell 
msiexec /i SkypeForBusinessSDNManager.msi /quiet /lv* install.log LOGPATH=c:\\Temp TOPOLOGY=2 DATABASE_SERVER=dblneprod
  ```

  Result: 

  ```powershell
  MSI (s) (18:80) [14:02:27:052]: Invoking remote custom action. DLL: C:\\Windows\\Installer\\MSI1D29.tmp, Entrypoint: ExecuteSqlStrings
ExecuteSqlStrings:  Error 0x80040e14: failed to execute SQL string, error: An object or column name is missing or empty. For SELECT INTO statements, verify each column has a name. For other statements, look for empty alias names. Aliases defined as "" or are not allowed. Change the alias to a valid name., SQL key: CreateUserScript25 SQL string: BEGIN TRY CREATE USER  FROM LOGIN$ END TRY BEGIN CATCH END CATCH
MSI (s) (18!10) [14:02:27:364]: Product: Microsoft Skype for Business SDN Manager -- Error 26204. Error -2147217900: failed to execute SQL string, error detail: An object or column name is missing or empty. For SELECT INTO statements, verify each column has a name. For other statements, look for empty alias names. Aliases defined as "" or [] are not allowed. Change the alias to a valid name., SQL key: CreateUserScript25 SQL string: BEGIN TRY CREATE USER [] FROM LOGIN[\\$] END TRY BEGIN CATCH END CATCH

  ```


  Expected: 
    
    An actionable error, like: "Error: Aborting because COMPUTERNAME was not specified". 
    
  
- Connection attempts using SSL from Dialog Listener to an SDN Manager installed on WS2008R2 fails. WS2008R2 machines do not support TLS1.2 by default. When hosting SDN Manager, the host must enable TLS1.2 support in its registry. See  [TLS/SSL Settings](https://technet.microsoft.com/en-us/library/dn786418%28v=ws.11%29.aspx)
    
  
- Support for Lync 2010 is now deprecated. 
    
  
- After installing version 3.0 in an active deployment, there may be some errors reported in the logs and reports lost for some calls due to a change in how QoE reports are kept in the data store. Only reports for events during the time of the upgrade may be affected. This is still true if they jump from 2.2 to 3.0. 
    
  
- InCallQoE is referred to as InCallQoS in the SfB front-end powershell configuration, which can be misleading if you look for the setting to activate/deactivate. 
    
  
- Due to changes in how we store state for calls, during an upgrade, some failures may be reported for ongoing calls during the upgrade. 
    
  

## Additional resources
<a name="bk_addresources"> </a>


-  [Skype for Business SDN Interface schema reference](http://msdn.microsoft.com/library/b64912bd-27b1-40c6-99ab-8984f8706bd3.aspx)
    
  


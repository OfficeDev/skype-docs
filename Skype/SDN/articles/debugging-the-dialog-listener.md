---
title: Debugging the SDN Interface Dialog Listener
ms.prod: SKYPE
ms.assetid: 0dade195-3eee-4b8d-8510-33bd78927442
---


# Debugging the SDN Interface Dialog Listener

 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

  
    
    


## Troubleshooting installation of Dialog Listener

DialogListener.exe is a Skype for Business Server application and uses SPL (MSPL) to connect to Skype for Business Server. Therefore, it must be registered with the Skype for Business Server. When an installation of the Dialog Listener fails, it may be due to the Dialog Listener not being properly registered. To troubleshoot, verify that the application is registered by running the  `Get-CsServerApplication`Windows PowerShell cmdlet from the Skype for Business Server Management Shell.
  
    
    

- Start the Skype for Business Server Management Shell.
    
  
- Issue the following Windows PowerShell cmdlet in the Skype for Business Server Management Shell window: 
  
    
    
 `Get-CsServerApplication`
    
  
- Verify that the following entry (or a similar one) is displayed as the cmdlet the output: 
  
    
    

  
    
    

  
    
    
![Output of cmdlet execution](../images/a1dc6b90-0adf-485c-846a-1e859cea6233.png)
  
    
    

  
    
    

  
    
    
 If such an entry is not shown in the output, ensure that you are running in an administrator account that is also member of the *RTC Server Applications*  local group before you install Dialog Listener. You may manually configure a registration similar to the one shown.
    
  
- Check whether the current Skype for Business front-end server is synchronized by executing the  `Get-CsManagementStoreReplicationStatus`Windows PowerShell command. This operation may take several minutes and may time out if the synchronization fails.
    
  
- Check whether QoE reporting is activated for this particular Skype for Business Server pool by using the  `Get-CsQoEConfiguration`Windows PowerShell command.
    
  
- It is possible that the Dialog Listener service does not start due to incompatible or incorrect credentials. You can verify and update the credentials by starting the Dialog Listener service at the command prompt.
    
  

## Verifying operations of Dialog Listener service

To confirm that the installation is successful, do the following:
  
    
    

1. Verify that the DialogListener.exe service has started by opening Windows Task Manager.
    
  
2. After running the Setup, you can confirm that the installation is successful by checking the Diagnostics.log file and verifying that there are no errors reported in the log file. 
    
  


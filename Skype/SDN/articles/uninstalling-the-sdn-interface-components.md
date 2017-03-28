---
title: Uninstalling the SDN Interface components
ms.prod: SKYPE
ms.assetid: 54465ebc-dbf3-4788-9326-bb1cd34427fd
---


# Uninstalling the SDN Interface components

 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

This section describes how to uninstall Skype for Business SDN Interface components, the Dialog Listener and the SDN Manager services. 
  
    
    


## Uninstalling the Skype for Business SDN Interface

Removing the Skype for Business SDN Interface involves removing and unregistering the Dialog Listener, and uninstalling the SDN Manager. 
  
    
    

## Uninstall the Dialog Listener

1. On the server on which the Dialog Listener is installed, open the **Control Panel**, then open **Program and Features**. 
    
  
2. Select Skype for Business Server Dialog Listener. 
    
  
3. Right-click the selection and select **Uninstall**. 
    
  

## Uninstall the SDN Manager


1. On the server on which the SDN Manager is installed, open the **Control Panel**, then open **Program and Features**. 
    
  
2. Select Skype for Business ServerSDN Manager 
    
  
3. Right-click the selection and select **Uninstall**. 
    
  

## Unregister Dialog Listener

To unregister Dialog Listener as a Skype for Business Server application, do the following: 
  
    
    
Start a Skype for Business Management Shell and invoke the following Windows PowerShell cmdlet:   `Remove-CsServerApplication -Identity <app identity>` Where  `<app identity>` is the application identity string.
  
    
    
Example:  `Service:registrar:pool1.contoso.com/Diagnostics`. You can follow the example shown in  [Installing Skype for Business SDN Interface](installing-sdn-interface.md) for using `Get-CsServerApplication` cmdlet to discover the `<app identity>` value for the application.
  
    
    

> [!NOTE]
> Removing the registration will deactivate all Dialog Listener instances on any front-ends deployed in this pool. > Uninstalling all Dialog Listener instances in a pool won't unregister the SDN Interface application. You must to do this manually. 
  
    
    



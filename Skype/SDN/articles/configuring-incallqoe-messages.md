---
title: Configuring InCallQoE Messages
ms.assetid: b96a25eb-c900-4942-9384-2fbca6f84b05
---


# Configuring InCallQoE Messages
Learn how to configure in-call quality reporting for Skype for Business Server. 
 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

InCallQoE updates contain quality metrics and are sent during the call. The metrics within the quality update reports are trailing weighted averages since the beginning of the call. They are supported by Windows desktop W16 Skype for Business client (as of the writing on this note). These are different from midcall quality updates that are supported by certain web based clients. 
  
    
    


> [!NOTE]
> These are different from "midcall" quality updates that are reported by certain web based clients. 
  
    
    

 After you install the MSI package for the new Skype for Business client, follow the instructions below to examine or control generating and sending InCallQuality messages.
### Configuring the Skype for Business Server for in-call quality reporting

If using the latest official build of Skype for Business Server (build 7.0.1017.3) as well as the Skype for Business client (16.x.x.x), sending InCallQuality messages will be activated automatically once the dialog listener is installed. No manual configuration is necessary. 
  
    
    
Use the Skype for Business Powershell console and Get/Update-CsMediaConfiguration to verify and change the IncallQuality feature. Access the console by typing the following at the prompt:  `PS > Get-CsMediaConfiguration`. You will see the following: 
  
    
    



|**Attribute**|**Value**|
|:-----|:-----|
|Identity |Global |
|EnableQoS |False |
|EncryptionLevel |RequireEncryption |
|EnableSiren |False |
|MaxVideoRateAllowed |VGA600K |
|EnableInCallQos |True |
|InCallQoSIntervalSeconds |35 |
   
The EnableIncallQoS attribute enables/disables the ability for Skype for Business clients to send raw data needed to generate IncallQuality messages. 
  
    
    
The InCallQoSIntervalSeconds attribute is used to set the smallest period in which clients will send such raw data, allowing you to throttle messaging so you don't overburden the network when the stream quality is already impacted. The default value is 35. 
  
    
    

### Manually Configuring the Skype for Business Clients for in-call quality reporting
<a name="SkypeCDNReleaseNotes_ManuallyConfigureSkypeForIncallQuality"> </a>

If you are running Lync Server 2013 while using the Lync 2013 client, you must manually enable generating of IncallQuality messages on each client. However, this is practical only in lab situations. You may also use this procedure to enable inCallQoE messages on a specific W16 client if you do not want to enable it for all users on the server. To configure in-call quality reporting in a production environment, do the following: 
  
    
    

1. Generate the XML configuration file (ocapi_test.config ) and copy it to the installation path of each Skype for Business client that contains the Lync.exe (for example, C:\\Program Files (x86)\\Microsoft Office\\root\\Office16). You will need administrator privileges to copy a file to this directory. The XML file named ocapi_test.config.xml contains the following information. 
    
  - InCallQosEnabled = true 
    
  
  - InCallQoSPeriodInSec = 35 
    
  

    The literal contents of the ocapi_test.config.xml file is: 
    


  ```xml
  
<?xml version="1.0"?>
<settings>
     <InCallQosEnabled>true</InCallQosEnabled>
     <InCallQoSPeriodInSec>35</InCallQoSPeriodInSec>
</settings> 

  ```

2.  Restart the client by first exiting (File -> Exit) and then restarting the Skype for Business client.
    
  
3.  You can change the InCallQoSPeriodInSec to change the maximum frequency of receiving InCallQuality reports from the client and therefore, from the SDN Manager.
    
  

> [!NOTE]
> Both  *InCallQoSPeriodInSec*  and *InCallQoSIntervalSeconds*  control the frequency of receiving InCallQuality reports from the client. When manually configuring the in-call quality reporting , use *InCallQoSPeriodInSec*  . When using Powershell, use *InCallQoSIntervalSeconds*  .
  
    
    


## Additional resources
<a name="bk_addresources"> </a>


-  [In-call QoE algorithm and throttling](in-call-qoe-algorithm-and-throttling.md)
    
  


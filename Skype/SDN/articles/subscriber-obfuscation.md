---
title: Per Subscriber Obfuscation
ms.prod: SKYPE
ms.assetid: d7fee767-9dbb-4ec0-9d20-3de741e79079
---


# Per Subscriber Obfuscation
Learn how to set an individual obfuscation seed for a subscriber. 
 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

## Per Subscriber Obfuscation

Learn how to set an individual obfuscation seed for a subscriber. 
  
    
    
After ensuring that hidepii=true in the Manager settings, you can set an individual obfuscation seed for a subscriber by adding an obfuscationseed setting. 
  
    
    
All SIP URI user names and name and tel in the PAI field will be obfuscated in both signaling and QoE reports from SDN Manager. If an "obfuscationseed" subscriber setting is set (containing a string) this seed will be used for obfuscating the fields for this subscriber, otherwise the default seed is used. The seed used is supposed to be somewhat protected and unique as it allows re-obfuscation but is not an encryption key. 
  
    
    
SipObfuscator tool has been updated to accept a parameter to supply the subscriber obfuscation seed to calculate the hash code. For example: 
  
    
    



```powershell

SDNManager.exe p s mysubscriber obfuscationseed=mypersonalseed 
```


> [!NOTE]
> The Subscriber  `obfuscationseed` setting is ignored when Manager setting 'hidepii' is 'false'.
  
    
    



---
title: Using SIP Obfuscator for the SDN Interface
ms.prod: SKYPE
ms.assetid: d71f5c20-627e-43ed-8127-53e0d1c193ad
---


# Using SIP Obfuscator for the SDN Interface

 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

The Skype for Business SDN Interface includes a utility named SIPObfuscator.exe for use by authorized partners that you can use to obtain the obfuscated value of a user name. The resultant obfuscated name can be used to query for dialog data involving the specified user when privacy protection is turned on. SipObfuscator.exe requires that .NET Framework 4.5 be installed. 
  
    
    


## SIP Obfuscator

When privacy protection is turned on in the SDN Manager, personal identifiable information in Skype for Business call and quality data is obfuscated by replacing identifying information with a SIP alias. For example, the SIP Uri of  `sip:adama@contoso.com` might be replaced by `sip:CE6AF05C9705A05E@contoso.com`. When the network traffic patterns of a single user is specified, the SIP Obfuscator utility allows you to determine the obfuscated user's identity. 
  
    
    
SIP Obfuscator is a Windows console application that is installed along with installations of the SDN Manager and Dialog Listener. 
  
    
    
To use SIP Obfuscator, do the following: 
  
    
    

1. Select a known user name, for example, adama. 
    
  
2. Type the following command in a Windows console and press **Enter**. 
  
    
    
 `SIPObfuscator adama`
    
  
3. The obfuscated user name is displayed as output, for example: 
  
    
    
 `CE6AF05C9705A05E`
    
  
To search for the data records associated with this user (sip:adama@contoso.com), you can search for the records containing  `sip:CE6AF05C9705A05E@contoso.com`. An example of such an obfuscated data record is shown as follows: 
  
    
    



```xml

<Start Type="audio">
    <From>
      <Id>87c1bcf104</Id>
      <EPId>1579d442a7</EPId>
      <URI>sip:CE6AF05C9705A05E@contoso.com</URI>
      ……
    </From>
</Start>

```


> [!NOTE]
> IP addresses are never obfuscated in Skype for Business SDN Interface because they are essential information for identifying the streams in the network. 
  
    
    



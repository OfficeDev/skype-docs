---
title: Per Subscriber Obfuscation for Lync Server 2013 or Skype for Business 2015
description: Learn how to set an individual obfuscation seed for a subscriber.
TOCTitle: Per Subscriber Obfuscation
ms:assetid: d7fee767-9dbb-4ec0-9d20-3de741e79079
ms:mtpsurl: https://msdn.microsoft.com/library/Mt683864(v=office.16)
ms:contentKeyID: 72245470
ms.date: 02/27/2017
mtps_version: v=office.16
---

# Per Subscriber Obfuscationfor Lync Server 2013 or Skype for Business 2015

Learn how to set an individual obfuscation seed for a subscriber.


_**Applies to:** Lync Server 2013 | Skype for Business 2015_

## Per Subscriber Obfuscation

Learn how to set an individual obfuscation seed for a subscriber.

After ensuring that hidepii=true in the Manager settings, you can set an individual obfuscation seed for a subscriber by adding an obfuscationseed setting.

All SIP URI user names and name and tel in the PAI field will be obfuscated in both signaling and QoE reports from SDN Manager. If an "obfuscationseed" subscriber setting is set (containing a string) this seed will be used for obfuscating the fields for this subscriber, otherwise the default seed is used. The seed used is supposed to be somewhat protected and unique as it allows re-obfuscation but is not an encryption key.

SipObfuscator tool has been updated to accept a parameter to supply the subscriber obfuscation seed to calculate the hash code. For example:

`SDNManager.exe p s mysubscriber obfuscationseed=mypersonalseed`


> [!NOTE]
> The Subscriber obfuscationseed setting is ignored when Manager setting ‘hidepii’ is ‘false’.



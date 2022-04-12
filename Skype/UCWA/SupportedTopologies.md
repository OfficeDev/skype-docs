---
title: Supported topologies for Skype for Business 2015
description: " Supported topologies presents Microsoft Unified Communications Web API 2.0 features that are available in different Skype for Business Server topologies."
---
# Supported topologies
Learn about the Microsoft Unified Communications Web API 2.0 features that are available in different Skype for Business Server topologies.


 _**Applies to:** Skype for Business 2015_

Only users homed on a Skype for Business Server 2015 pool can take advantage of UCWA 2.0 capabilities. 

Features such as Contacts, HD Photos, Voicemail and meetings are dependent on the version of Exchange Server; in this case, the features require Exchange Server 2013.
For more information about how to leverage Microsoft Exchange Server 2013 integration, see [Planning for Exchange Server Integration](https://technet.microsoft.com/library/jj721919.aspx).

> [!NOTE] 
> Microsoft Exchange Server 2013 integration leverages server-to-server authentication. Server-to-server authentication between an on-premises server and a Office 365 component is not supported in this Skype for Business Server 2015 release. Among other things, this means that you cannot set up server-to-server authentication between an on-premises installation of Skype for Business Server 2015 and Microsoft Exchange Online.


## Skype for Business Server 2015 On-Premises

The following table summarizes the features that are supported in several Skype for Business Server 2015 On-Premises - Exchange Server scenarios. Skype for Business contacts &amp; photos are retrieved from Active Directory Domain Services (AD DS) instead of Exchange Server in cases where it is not supported.



|**Feature**|**Exchange Server On-Premises**|**Exchange Server 2013 On-Premises**|**Exchange 2010 Online**|**Exchange 2013 Online**|
|:-----|:-----|:-----|:-----|:-----|
|Unified Contacts and Groups|No|Yes|No|Yes|
|HD Photos|No|Yes|No|Yes|

> [!NOTE] 
> Server-to-server authentication between an on-premises server and an Office 365 component is not supported in Skype for Business Server 2015. Among other things, this means that you cannot set up server-to-server authentication between an on-premises installation of Skype for Business Server 2015 and Microsoft Exchange Online.


---
title: PSTN gateway and IP-PBX interoperability
TOCTitle: PSTN gateway and IP-PBX interoperability
ms:assetid: 393b1403-3f11-4574-9d1b-0ed38764333d
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465944(v=office.16)
ms:contentKeyID: 65239833
ms.date: 07/27/2015
mtps_version: v=office.16
---

# PSTN gateway and IP-PBX interoperability


_**Applies to:** Skype for Business 2015_

For IP-based real-time applications that must communicate with a PSTN network on one side and a unified communications network on the other side, Microsoft Unified Communications Managed API 5.0 provides an application the ability to interoperate with SIP PSTN gateways and IP-PBXs with the unified communications network.

The following SIP peers can connect to the UCMA application:

1.  SIP gateways.

2.  SIP-enabled IP-PBXs that support the following phones.
    
    1.  SIP phones
    
    2.  Soft phones that support SIP

SIP peers should support the following protocols:

  - SIP—RFC 3261

  - DTMF—RFC4733

  - SDP—RFC 3264

In addition, SIP peers:

  - Must provide media session parameter negotiation (endpoint, codec, and, if applicable, encryption parameters) through SDP, as defined in the SIP and related RFCs and used in SIP.

  - Must provide media/audio transport using RTP/RTCP over UDP.

  - Must meet the Supported IP Gateways requirements listed in [Telephony Advisor for Exchange 2010](http://technet.microsoft.com/en-us/library/ee364753.aspx).

The following illustration shows a typical setup.

![IP-PBX Gateway](images/Dn465944.UCMA-Gateway(Office.16).png "IP-PBX Gateway")

For more information, see [Trusted domains and SIP gateways](trusted-domains-and-sip-gateways.md).


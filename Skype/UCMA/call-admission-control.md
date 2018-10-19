---
title: Call admission control
TOCTitle: Call admission control
ms:assetid: 5536dd5c-1e64-4145-8691-94cc2b7be69e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466095(v=office.16)
ms:contentKeyID: 65240046
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Call admission control


_**Applies to:** Skype for Business 2015_

For IP-based real-time applications on wide-area network links between sites, bandwidth is a finite resource, which makes the appropriate provisioning of such links crucial for a Skype for Business Server 2015 administrator. To serve this need, UCMA 5.0 provides the ability to manage bandwidth in a wide-area network (WAN). This ability addresses bandwidth utilization in unified communications scenarios, and provides an infrastructure that allows policy decisions (whether or not sessions can be established) to be made when setting up real-time sessions.

Bandwidth management provides two important benefits, by enabling the following:

  - Skype for Business Server 2015 supports highly adaptable audio and video codecs that can adjust to varying network capacity. The RTAudio and RTVideo codecs make it possible to maintain good media quality in degraded network conditions. However, to prevent degradation of audio and video quality that users can perceive, Skype for Business Server 2015 introduces support for call admission control. It is now possible to prevent users from establishing calls that would result in quality degradations for everyone.

  - Call admission control offers more flexible control for IT professionals to architect their network traffic. This can help to prevent unexpected spikes in calls from affecting the entire network and line-of-business applications, and impacting the quality of existing calls. Call admission control protects the network and prevents Skype for Business Server 2015 traffic from consuming all the bandwidth available on the network. Unlike other call admission control solutions that are available from different vendors, call admission control in Skype for Business Server 2015 does not require additional hardware; it is built into Skype for Business Server 2015.

## In this section

  - [Call admission control usage scenarios](call-admission-control-usage-scenarios.md)

  - [MediaProvider recommendations](mediaprovider-recommendations.md)


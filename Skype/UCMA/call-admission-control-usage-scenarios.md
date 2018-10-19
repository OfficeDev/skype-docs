---
title: Call admission control usage scenarios
TOCTitle: Call admission control usage scenarios
ms:assetid: fa499ce2-b28a-4b66-8e37-f8ce0fd8961e
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466097(v=office.16)
ms:contentKeyID: 65240047
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Call admission control usage scenarios


_**Applies to:** Skype for Business 2015_

Bandwidth management can be applied in a number of scenarios. The following list presents several of the scenarios that occur most often.

### Basic success

John in Spokane places a Microsoft Skype for Business 2015 audio call to his company’s general information line, an interactive voice-response (IVR) application. The WAN link is free, so John’s call proceeds as normal, after checking with the Policy Distribution Point. The call is established and audio flows between John’s endpoint and the IVR.

### Basic failure

John in Spokane places a Skype for Business 2015 call to his company’s general information line, an IVR. The WAN link is overburdened, and John’s call cannot be redirected to PSTN, so it fails. John is informed that the call has failed due to the Call Admission Control’s failure to provision the WAN link. John’s network administrator, Bob, is able to determine that this failure occurs frequently. To provide better network management, Bob purchases a larger link. Additionally, the application is informed of the failure, and the reason (CAC failure).

### Redirect to PSTN

Gordon is on from the Zurich Office. He calls in to an audio-only conference hosted in Redmond. The WAN link from Zurich to Redmond is slightly overburdened, so Gordon’s session with the conference is redirected to PSTN through the Zurich Branch Office Appliance. The call completes successfully, and Gordon listens to the conference. (The user interface informs Gordon that he has been redirected, in a manner similar to that of the Skype for Business 2015 user interface.)


---
title: MediaProvider recommendations
TOCTitle: MediaProvider recommendations
ms:assetid: 11d654ce-5c01-4255-be80-1df7db15f9d4
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466113(v=office.16)
ms:contentKeyID: 65240048
ms.date: 07/27/2015
mtps_version: v=office.16
---

# MediaProvider recommendations


_**Applies to:** Skype for Business 2015_

To implement bandwidth management in a class derived from the [MediaProvider](https://msdn.microsoft.com/en-us/library/hh383767\(v=office.16\)) class, the derived provider class must carry out the following tasks:

  - Honor the bandwidth policy-check flag provided by the base media provider to determine whether a bandwidth check should be performed.

  - On the answerer side, if the bandwidth check returns with a call-redirect message, throw [OfferAnswerException](https://msdn.microsoft.com/en-us/library/hh382722\(v=office.16\)) with an [OfferAnswerFailureReason](https://msdn.microsoft.com/en-us/library/hh348371\(v=office.16\)) value of **RedirectDueToBandwidthPolicy**.
    
    If the bandwidth check returns with a call-fail message, throw [OfferAnswerException](https://msdn.microsoft.com/en-us/library/hh382722\(v=office.16\)) with an [OfferAnswerFailureReason](https://msdn.microsoft.com/en-us/library/hh348371\(v=office.16\)) value of **FailDueToBandwidthPolicy**.
    

    > [!NOTE]
    > <P>Even though a media provider returns <STRONG>RedirectDueToBandwidthPolicy</STRONG>, the signaling layer can override this value and terminate the call with a failure response code based on application preferences.</P>



  - In the event of a partial failure, populate the appropriate headers in the 200 OK message if some of the modalities were rejected due to bandwidth policy check.

  - Provide a way to inform the application of partial failures through MediaFlow.

  - Provide a way to inform the application of RE-INVITE bandwidth failures through MediaFlow.

  - On the offerer side, be prepared to handle a null answer with an [SdpAnswerStatus](https://msdn.microsoft.com/en-us/library/hh383245\(v=office.16\)) value of **NotAcceptableDueToBandwidthPolicy** when the answerer rejects the call with a 488 status code.

  - (Optional) In **FlowConfigurationRequested** event, expose a way to get application preferences on enabling and disabling bandwidth policy.


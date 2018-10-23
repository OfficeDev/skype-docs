---
title: UCMA 5.0 exception model
TOCTitle: UCMA 5.0 exception model
ms:assetid: 4978a9ab-3ef3-456c-b3ac-d12ad06c03d9
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466072(v=office.16)
ms:contentKeyID: 65239999
ms.date: 07/27/2015
mtps_version: v=office.16
---

# UCMA 5.0 exception model

**Applies to**: Skype for Business 2015

Microsoft Unified Communications Managed API 5.0 follows the standard .NET Framework exception model for reporting exceptions. Both the standard exception classes in the .NET Framework and custom exception classes specific to UCMA 5.0 are used. UCMA 5.0 applications should catch exceptions thrown by UCMA 5.0 methods. In addition we recommend that applications validate the data they pass to the API to avoid exceptions derived from **ArgumentException** from being thrown in released products.

## Standard .NET Framework exception classes

The following standard exception classes are adopted by UCMA 5.0:

- **ArgumentException**

  Thrown when an invalid argument is passed to a method call or an invalid value is set on a property.

- **ArgumentNullException** 

  Thrown when a null argument value is passed as a parameter that cannot be null. Also thrown for properties that cannot be set to null.

- **ArgumentOutOfRangeException** 

  Thrown when a parameter passed to a method call is not within the required range. Also thrown for properties that cannot be set to null.

- **InvalidOperationException** 

  Thrown when the current object state is not valid for a particular action. For example, a user tried to send a message on a terminated IM session.

## Exception classes specific to UCMA

The following custom exception classes are implemented in UCMA 5.0:

- **RealTimeException** 

  Except for **RealTimeInvalidOperationException**, this is the root class for the exceptions that are specific to UCMA 5.0 and later. It is thrown when the error condition cannot be mapped to any other exception types.

- **AuthenticationException** 

  Thrown when a 401 response was received in authentication. It exposes the failed response data (**ResponseData**) and parsed values of authentication-specific headers.

- **CallOperationFailureException**

  Thrown when failure occurs on a class derived from the [Call](https://msdn.microsoft.com/en-us/library/hh384235\(v=office.16\)) class. The **FailureReason** property indicates the cause of failure. The following example shows the preferred way to catch this exception.

  ```powershell  
        try
        {
          // Code that might throw an exception.
        }
        catch (CallOperationFailureException cofe)
        {
          // Handle exception.
        }
        catch (RealTimeException rte)
        {
          // Handle exception.
        }
  ```

- **ConferenceFailureException**  
  
  Thrown when an error associated with conference scheduling or management occurred when communicating with the server. Possible reasons for this exception being thrown include the following:
    
  - The conference could not be created, scheduled, retrieved, or removed.
    
  - The conference could not have its settings modified.
    
  - The check failed for whether a passcode is optional for a conference.
    
  - The conference passcode could not be verified.
    
  - A failure occurred in the attempt to get the available MCU types for a conference.
    
  > [!NOTE]
  > Failure to communicate with the server components for conferencing could result in **FailureResponseException**.

- **ConnectionFailureException**  
  
  Thrown when a network connection could not be made. Applications might consider prompting for a new URI (host and port) or a server name.

- **FailureRequestException**  
  
  Thrown when a failure message is received as a SIP request message.

- **FailureResponseException**  
  
  Thrown when a 4xx, 5xx, or 6xx response was received for a request. This exception contains the **ResponseData** property, which contains the complete response including response code, reason text, headers, and message body. In some rare cases, this may also be thrown when an error other than a 4xx, 5xx, or 6xx response occurred. In such cases the **ResponseData** property is null.

- **MessageParsingException**  
  
  Thrown when an incoming response or message could not be parsed. For example, an invalid custom signaling header was used.

- **OfferAnswerException**  
  
  Thrown when one of the following errors occurred in a media provider:
    
  - The SDP for the offer could not be created.
    
  - The answer received from the remote side could not be accepted.
    
  - A collision in the offer/answer interchange occurred because a previous negotiation is still pending.
    
  - An answer for a received offer could not be sent.

- **OperationFailureException**  
  
  Thrown when an operation fails. The **FailureReason** property indicates the cause of failure.

- **OperationTimeoutException**  
  
  Thrown when an operation could not be completed in a reasonable time. For example, a response from another endpoint or server was not received after a request was sent.

- **ProvisioningFailureException**  
  
  Thrown when an attempt to auto-provision an endpoint fails.

- **PublishSubscribeException**  
  
  Thrown when a **FailureResponseException** occurred for requests of the SERVICE and SUBSCRIBE types. This class inherits from **FailureResponseException** and adds diagnostic information about the message body and the fault code parsed from the body.

- **RegisterException**  
  
  Thrown when an error occurred in a registration-related operation.

- **ServerPolicyException**  
  
  Thrown when an operation is rejected due to server policy.

- **TlsFailureException**  
  
  Thrown when an error occurred during a TLS handshake. This error can occur when the remote server certificate is rejected. This can also occur when the MTLS local certificate is rejected by the remote server.

- **RealTimeInvalidOperationException**  
  
  Thrown when the operation is currently invalid, but the application might be able to take further action based on the reason.

The following object model lists the exceptions that are specific to UCMA 5.0.

![UCMA exceptions](images/Dn466072.UCMA2Exceptions(Office.16).jpg "UCMA exceptions")

## See also

- [Microsoft.Rtc.Collaboration Namespace](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration?view=ucma-api-5.0)
- [Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml Namespace](https://docs.microsoft.com/dotnet/api/Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml?view=ucma-voice)
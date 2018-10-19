---
title: Security considerations in UCMA 5.0
TOCTitle: Security considerations in UCMA 5.0
ms:assetid: 5d87e5b2-9d95-4d37-98c7-c5e58a6247e9
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466093(v=office.16)
ms:contentKeyID: 65240044
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Security considerations in UCMA 5.0


_**Applies to:** Skype for Business 2015_

## Security notes

Developers who create applications using UCMA 5.0 SDK are strongly advised to observe the following security recommendations:

  - Although the UCMA 5.0 SDK platform supports Transmission Control Protocol (TCP), applications should use Transport Layer Security (TLS) whenever possible, and should use mutual TLS (MTLS) when communicating with any server. This recommendation applies especially to middle-tier applications. TLS helps to mitigate spoofing, tampering, and repudiation. An application should not choose TCP when it runs in a networking environment where computers are not fully trusted (for example, any cloud-connected environment).

  - Use authentication for an application that acts as a client. Authentication helps to ensure that the server is legitimate and that messages have not been tampered with (through message signing). This process helps to mitigate spoofing, tampering, and repudiation.
    
    Authentication includes both user and far-end host authentication.

  - Use trusted service-grade authentication only when necessary. When possible, use client authentication as the lower trust grade, without server impersonation and other server-only capabilities.

  - Applications should avoid contacting servers based on received SIP extension headers, instant messages, or other sources. Contact should be made only to a single trusted server.

  - Referred-by headers should not be given full trust, as they are neither signed nor authenticated. A user can claim to be referred by anyone.

  - Applications should use care when following transfer requests. Auto-forwarding logic presents risks of impersonation and misdirection. For example, media stream redirects can lead an audio stream to a target unknown to the caller.

  - Applications should regard the contents of instant messages and any URLs contained in presence documents or instant messages as untrusted and potentially harmful.

  - Applications must mitigate attacks at their own entry points.

  - The administrator of the computer that hosts an application built on UCMA 5.0 SDK should be aware that the logging components that are shipped with the UCMA 5.0 SDK allow SIP logging and that the headers and contents of SIP messages can contain confidential data.

  - Application developers should be aware that additional entry points built on the extension APIs potentially can change the attack surface (for example, Remote Desktop Protocol (RDP) for application sharing offers its own inband threats). Extension-specific mitigations are left to the application.

  - To avoid malicious redirection, an application can limit the number of redirects allowed by a **UserEndpoint** instance.

  - Administrators should restrict access to the default.tmx file by using an access control entry (ACE). The default.tmx file is used to decode binary log files that can contain IP address/call duration tuples.

  - Media Stream encryption can be turned off by Skype for Business Server 2015 policy. The encryption value comes from inband provisioning. If a value is not supplied, the default value (**Supported**) is used.

  - Application developers should be aware that files recorded by a **Recorder** device do not support Digital Rights Management (DRM). This means that conversations recorded by a **Recorder** device can be played by any player that supports the Windows Media Audio (WMA) media format, potentially exposing personal or confidential information.

  - Although the UCMA 5.0 platform supports Real-time Transport Protocol (RTP), applications should use Secure Real-time Transport Protocol (SRTP) whenever possible. It is highly recommended that you use Encryption=Required so that audio packets cannot be tampered with or sniffed.
    

    > [!NOTE]
    > <P>If a UCMA 5.0 application interoperates with a non-Skype for Business Server 2015 proxy (for example, a gateway), ensure that the non-Skype for Business Server 2015 proxy supports SRTP (see RFC 3711). If Encryption=Required is used and the non-Skype for Business Server 2015 proxy does not support encryption, call failure will result.</P>



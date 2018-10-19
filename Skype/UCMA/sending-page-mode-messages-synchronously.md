---
title: Sending page-mode messages synchronously
TOCTitle: Sending page-mode messages synchronously
ms:assetid: 3d111eb4-69d6-4061-ae12-3d32baf6b67d
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466061(v=office.16)
ms:contentKeyID: 65239993
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Sending page-mode messages synchronously


_**Applies to:** Skype for Business 2015_

The following code example shows how to send a simple text message synchronously in page mode. It is assumed that the endpoint is properly created and registered, if it is registration-based. In this example, the message is sent using a call to the [SendMessage(MessageType, RealTimeAddress, ContentType, \[\])](https://msdn.microsoft.com/en-us/library/hh349067\(v=office.16\)) method on the endpoint.

``` csharp
RealTimeEndpoint endpoint = ...; // Assumed to be created elsewhere
RealTimeAddress target = new RealTimeAddress("sip:bob@contoso.com");
string msg = "Greetings!";
ContentType contentType = new ContentType("text/plain; charset=UTF-8");
byte[] msgBody = Encoding.UTF8.GetBytes(msg);
try
{
  endpoint.SendMessage(MessageType.Message,
                       target,
                       contentType,
                       msgBody);
}

catch (RealTimeException e)
{
  // Handle exception.
}
```


---
title: Sending page-mode messages asynchronously
TOCTitle: Sending page-mode messages asynchronously
ms:assetid: cfbc3cd0-f357-40e7-b6c3-8bc0d78493e1
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466050(v=office.16)
ms:contentKeyID: 65239994
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Sending page-mode messages asynchronously


**Applies to**: Skype for Business 2015

The asynchronous call to send a page-mode message uses the **BeginSendMessage** … **EndSendMessage** pattern adopted in the .NET Framework, and recommended in this documentation. The pattern requires an application to supply a callback method that is invoked before the asynchronous operation finishes. This sample calls the [BeginSendMessage(MessageType, RealTimeAddress, ContentType, \[\], AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh161734\(v=office.16\)) overloaded method. The [EndSendMessage(IAsyncResult)](https://msdn.microsoft.com/en-us/library/hh382471\(v=office.16\)) method is called in the callback method, *CompleteSendMessage*.

```csharp
RealTimeEndpoint endpoint = ...; // Assumed to be created elsewhere
RealTimeAddress target = new RealTimeAddress("sip:bob@contoso.com");
string msg = "Greetings!";
ContentType contentType = new ContentType("text/plain; charset=UTF-8");
byte[] msgBody = Encoding.UTF8.GetBytes(msg);
try
{
  endpoint.BeginSendMessage(MessageType.Message,
                            target,
                            contentType,
                            msgBody,
                            CompleteSendMessage,
                            endpoint);
}

catch (RealTimeException e)
{
  // Handle exception here.
}

// Asynchronous callback function to end the BeginSendMessage operation.
void CompleteSendMessage(AsyncCallback result)
{
  RealTimeEndpoint ep = ar.AsyncState as RealTimeEndpoint;
  SipResponseData data;
  try
  {
    data = ep.EndSendMessage(ar);
  }

  catch (FailureResponseException exp)
  {
    // Handle a failure response.
    Console.WriteLine("Failure response for {0}: {1}", ep.Uri, exp.ToString());
  }

  catch (OperationTimeoutException exp)
  {
    // The operation timed out.
    Console.WriteLine("Operation timed out for {0}: {1}", ep.Uri, exp.ToString());
  }

  catch (RealTimeException exp)
  {
    // Handle exception.
    Console.WriteLine("Invite Failure for {0}: {1}", ep.Uri, exp.ToString());
  }
}
```


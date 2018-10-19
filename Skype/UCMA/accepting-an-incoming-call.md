---
title: Accepting an incoming call
TOCTitle: Accepting an incoming call
ms:assetid: 22ccda70-841f-47c7-a6ed-c871dd9e71e0
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466004(v=office.16)
ms:contentKeyID: 65239931
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Accepting an incoming call


_**Applies to:** Skype for Business 2015_

To listen for incoming calls, an application must register the handler with [RegisterForIncomingCall\<TCall\>](https://msdn.microsoft.com/en-us/library/hh382399\(v=office.16\)). When an incoming INVITE is received on an endpoint, the endpoint uses the factory to create the appropriate type of call and raise the event to the application. The application can call [BeginAccept()](https://msdn.microsoft.com/en-us/library/hh383161\(v=office.16\)) to accept the call, or [Decline()](https://msdn.microsoft.com/en-us/library/hh348897\(v=office.16\)) to decline the call. If the incoming call contains MIME parts, the application must specify the accepted Content-ID values list on the call to **BeginAccept**.

``` csharp
//Register the event handler to receive the incoming AudioVideoCall.
endpoint.RegisterForIncomingCall<AudioVideoCall>(Endpoint_CallReceived);

// Handler to receive and accept the incoming AudioVideoCall.
private void Endpoint_CallReceived(object sender, CallReceivedEventArgs<AudioVideoCall> e)
{
  incomingCall = e.Call;
  
  //process MIME parts if interested.
  Collection<MimePartContentDescription> callMimeParts = e.CustomMimeParts;
  ...

  //Register call event handlers. 
  incomingCall.StateChanged += CallStateChanged;
  ...

  //Accept the call.
  //The following code specifies no custom headers or accepted content-ids when accepting the call.
  incomingCall.BeginAccept(null, IncomingCallEstablished, incomingCall);
}

//Call state changed handler.
private void CallStateChanged(object o, CallStateChangedEventArgs eArg)
{
  Console.WriteLine("Call state changed: {0} {1}", eArg.PreviousState, eArg.State);
}
```

An incoming invitation with any MIME part that specifies "handling=required" for a content type will be rejected if the application does not specify the associated content type as a type that is supported by the endpoint. If the parts are in a mime/alternate section, then the application must support at least one of the parts.


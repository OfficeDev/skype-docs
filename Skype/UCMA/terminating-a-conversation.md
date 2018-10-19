---
title: Terminating a conversation
TOCTitle: Terminating a conversation
ms:assetid: b39542d7-92a7-413c-9db4-665e63376541
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465982(v=office.16)
ms:contentKeyID: 65239925
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Terminating a conversation


_**Applies to:** Skype for Business 2015_

The recommended way for an application can terminate a conversation is as follows, by calling one of the [BeginTerminate()](https://msdn.microsoft.com/en-us/library/hh349607\(v=office.16\)) overloaded methods on the [Conversation](https://msdn.microsoft.com/en-us/library/hh349224\(v=office.16\)) instance.

The following code example demonstrates terminating an existing conversation.

``` csharp
Conversation conversation = new Conversation(endpoint);
...
conversation.BeginTerminate(Conversation_TerminateCompleted, conversation);

void Conversation_TerminateCompleted(IAsyncResult asyncResult)
{
    Conversation conversation = asyncResult.AsyncState as Conversation;
    conversation.EndTerminate(asyncResult);    // Should not throw.
}
```


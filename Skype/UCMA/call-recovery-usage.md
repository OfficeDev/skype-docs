---
title: Call recovery usage
TOCTitle: Call recovery usage
ms:assetid: 6891b977-0949-411a-8525-52414753d7d5
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466066(v=office.16)
ms:contentKeyID: 65240006
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Call recovery usage


_**Applies to:** Skype for Business 2015_

For an application that intends to wait for dialog healing, an event handler for the [RouteSetStatusChanged](https://msdn.microsoft.com/en-us/library/hh382697\(v=office.16\)) event should be patterned as shown in the following code example.

``` csharp
private void RouteSetStatusChangedHandler(object sender, RouteSetStatusChangedEventArgs e)
{
  if(e.RouteSetStatus == RouteSetStatus.Recovering)
  {
    // Hold off sending any messages, and potentially implement a shutdown timer (to ensure that calls eventually terminate).
  }
  
  if(e.RouteSetStatus == RouteSetStatus.Healthy)
  {
    // Send any pending messages and resume normal processing.
  }
  

}
```

If the application is less concerned about message order, there is no need to hold pending messages if the status received is **Recovering**. Simply continue retrying until the session is terminated, keeping in mind that recovery attempts continue indefinitely. The application should eventually terminate the call if the route does not recover. The application can terminate the call based on a timer or other criterion, such as when the media flow is terminated.


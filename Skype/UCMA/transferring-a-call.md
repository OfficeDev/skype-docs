---
title: Transferring a call
TOCTitle: Transferring a call
ms:assetid: 0ad597a8-d002-4708-9879-f7335a5f02f5
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465979(v=office.16)
ms:contentKeyID: 65239932
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Transferring a call


_**Applies to:** Skype for Business 2015_

**In this article**  
Attended transfer  
Supervised transfer  
Unattended transfer  
Customizing call transfer behaviors  

This topic discusses attended, supervised, and unattended call transfers for a two-party audio/video conversation.


> [!NOTE]
> <P>Only the <A href="https://msdn.microsoft.com/en-us/library/hh383901(v=office.16)">AudioVideoCall</A> class provides call transfer functionality.</P>



The process of a call transfer involves three participants:

  - The Transferor is the participant who initiates the transfer.

  - The Transferee is the participant who is transferred from the primary call to the transferred call.

  - The Transfer Target is the participant to whom the Transferee is transferred.

After the primary call is established, either participant can transfer the call. To accomplish a transfer, the Transferor sends a REFER message to the Transferee, causing the Transferee to send an INVITE to the Transfer Target participant.

## Attended transfer

The platform monitors the progress of an attended transfer and completes the transfer only after receiving a successful REFER notification.

The following code demonstrates an attended call transfer (also known as a basic transfer).

``` csharp
CallTransferOptions basicTransferOptions = new CallTransferOptions(CallTransferType.Attended);
call.BeginTransfer(target, basicTransferOptions, userCallback, state);
```

## Supervised transfer

A supervised transfer is a type of attended transfer in which the transferor establishes a call to the number being transferred to, and then provides that call as an argument to **BeginTransfer**. For a supervised transfer, the REFER message contains a **Replaces** header that specifies the call to be replaced. The following code demonstrates a supervised call transfer.


> [!NOTE]
> <P>Because the default <A href="https://msdn.microsoft.com/en-us/library/hh381449(v=office.16)">CallTransferType</A> is Attended, call transfers of this type can pass null as the second parameter of the <A href="https://msdn.microsoft.com/en-us/library/hh385216(v=office.16)">BeginTransfer(Call, CallTransferOptions, AsyncCallback, Object)</A> call.</P>



``` csharp
CallTransferOptions supervisedTransferOptions = new CallTransferOptions(CallTransferType.Attended);
call.BeginTransfer(callToReplace, supervisedTransferOptions, userCallback, state);
```

## Unattended transfer

For an unattended transfer, the platform completes the transfer and terminates the primary call immediately after receiving a "REFER Accepted" message from the Transferee. At this time, the Transferee has already sent an INVITE to the Transfer Target specified by the REFER message, and must independently go through the process of establishing a new call with the Transfer Target. The platform terminates the primary call without waiting for an INVITE success or failure notification from the Transfer Target. The following code demonstrates the unattended transfer (also known as a blind transfer).

``` csharp
CallTransferOptions unattendedTransferOptions = new CallTransferOptions(CallTransferType.UnAttended);
call.BeginTransfer(target, unattendedTransferOptions, userCallback, state);
```

## Customizing call transfer behaviors

The [Call](https://msdn.microsoft.com/en-us/library/hh384235\(v=office.16\)) base class exposes protected methods to handle the incoming transfer and monitor the progress of the transfer. Derived classes can support transfer functionality by overriding the methods and exposing the event handler to the application.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Transfer method</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh381395(v=office.16)">HandleTransferReceived(CallTransferReceivedEventArgs)</a></p></td>
<td><p>Handles a transfer received event in a class derived from the <strong>Call</strong> class.</p>
<p>protected abstract void HandleTransferReceived(CallTransferReceivedData e)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh366035(v=office.16)">HandleTransferNotificationReceived(TransferStateChangedEventArgs)</a></p></td>
<td><p>Handles a transfer notification received event in a class derived from the <strong>Call</strong> class.</p>
<p>protected abstract void HandleTransferNotification(ReferStateChangedEventArgs e)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh385216(v=office.16)">BeginTransfer(Call, CallTransferOptions, AsyncCallback, Object)</a></p></td>
<td><p>Initiates a transfer request to the remote participant of the current call to replace an existing call in Attended mode.</p>
<p>protected IAsyncResult BeginTransfer(Call callToReplace, CallTransferOptions callTransferOptions, AsyncCallback userCallback, object state)</p></td>
</tr>
<tr class="even">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh365622(v=office.16)">BeginTransfer(String, CallTransferOptions, AsyncCallback, Object)</a></p></td>
<td><p>Initiates a transfer request to the remote participant to transfer the given transfer target in Attended or Unattended mode.</p>
<p>protected IAsyncResult BeginTransfer(string targetUri, CallTransferOptions callTransferOptions, AsyncCallback userCallback, object state)</p></td>
</tr>
<tr class="odd">
<td><p><a href="https://msdn.microsoft.com/en-us/library/hh382131(v=office.16)">EndTransfer(IAsyncResult)</a></p></td>
<td><p>Determines whether the corresponding transfer operation completed successfully.</p>
<p>This method waits if the operation has not yet completed.</p>
<p>CallMessageData EndTransfer(IAsyncResult)</p></td>
</tr>
</tbody>
</table>


---
title: Call park
TOCTitle: Call park
ms:assetid: 555e8378-4124-49d8-bada-08dc636bde33
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466006(v=office.16)
ms:contentKeyID: 65239933
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Call park

**Applies to**: Skype for Business 2015

An application might find it necessary to place a caller on hold temporarily, and then have a different user or application retrieve the call later. The caller on hold is said to be "parked." The call parking concept is typically applied to audio calls, but can be extended to other media types when appropriate. 

When a user successfully parks a call, the far-end caller is connected to a music-on-hold server and is disconnected from the person who parks the call. The parked call, which is identified by string of decimal digits (an "orbit" number) that uniquely identifies the call, can be retrieved at a later time by any user who is able to provide the orbit number. 

After the parked call is retrieved, the connection to the music-on-hold server is broken, and the user whose call was parked is reconnected to the retriever. If a parked call is not retrieved within a specific amount of time, the call parking service informs the person who parked the call.

Use these two methods to park a call: 

- [BeginPark](https://msdn.microsoft.com/en-us/library/hh384250\(v=office.16\))
- [EndPark](https://msdn.microsoft.com/en-us/library/hh349305\(v=office.16\))

Use these two methods to retrieve a parked call:

- [BeginEstablish](https://msdn.microsoft.com/en-us/library/hh349055\(v=office.16\))
- [EndEstablish](https://msdn.microsoft.com/en-us/library/hh349248\(v=office.16\))


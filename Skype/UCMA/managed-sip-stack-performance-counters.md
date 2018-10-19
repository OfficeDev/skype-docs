---
title: Managed SIP stack performance counters
TOCTitle: Managed SIP stack performance counters
ms:assetid: 2e8ab47c-2c76-4eeb-b0fd-c48a384cc601
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466105(v=office.16)
ms:contentKeyID: 65240033
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Managed SIP stack performance counters


_**Applies to:** Skype for Business 2015_

**In this article**  
About the managed SIP stack performance counters  
Installing and configuring the managed SIP stack performance counters  
Accessing the managed SIP stack performance counters  
In this section  

This section summarizes the managed SIP stack performance counters that are available with UcmaRedist.msi (Skype for Business Server, Unified Communications Managed API 5.0 Redist).

## About the managed SIP stack performance counters

Performance counters allow you to monitor specific aspects of work performed by your application, and can help you identify and troubleshoot performance issues.

The managed SIP stack performance counters are provided by the following files, which can be found in the SDK\\Core\\Bin directory under the Microsoft UCMA 5.0 installation directory:

  - S4Perf.dll

  - S4Perf.ini

  - S4Perf.h

The counters listed in this topic fall into one of two categories: PERF\_COUNTER\_LARGE\_RAWCOUNT or PERF\_COUNTER\_LARGE\_BULK\_COUNT. Counters whose **CounterType** is PERF\_COUNTER\_LARGE\_RAWCOUNT show the last observed value, not an average. Counters of type PERF\_COUNTER\_BULK\_COUNT show an average number of operations per second. For more information, see [Counter Types](http://technet2.microsoft.com/windowsserver/en/library/2c455a3c-6964-432b-9402-40f439b980881033.mspx?mfr=true).

## Installing and configuring the managed SIP stack performance counters

The managed SIP stack performance objects and counters are installed when UcmaRedist.msi is executed. If necessary, an application can unregister these performance objects similar to the way shown in the following command line.

regsvr32 /u /n /i " C:\\Program Files\\Microsoft UCMA 5.0\\SDK\\Core\\Bin \\s4perf.dll"

## Accessing the managed SIP stack performance counters

The following procedure shows the steps that must be performed to access the managed SIP stack performance counters.

### To access one or more performance counters

1.  On the Desktop, click **Start**, point to **All Programs**, point to **Administrative Tools**, and then click **Reliability and Performance Monitor**.

2.  In the **Reliability and Performance Monitor** dialog box, click the **Add** button (a plus sign).

3.  In the **Add Counters** dialog box, from the **Performance** object drop-down list, select from the available LC:SipEps performance objects:
    
    1.  To select all of the performance counters listed, click **All counters**.
    
    2.  To select a specific performance counter from the list, click **Select counters from list**.

4.  Click **Add** and then click **Close**.

## In this section

  - [Dialogs performance counters](dialogs-performance-counters.md)

  - [Transactions performance counters](transactions-performance-counters.md)

  - [Connections performance counters](connections-performance-counters.md)

  - [Incoming messages performance counters](incoming-messages-performance-counters.md)

  - [Outgoing messages performance counters](outgoing-messages-performance-counters.md)


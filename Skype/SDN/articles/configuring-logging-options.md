---
title: Configuring SDN Interface logging options
ms.prod: SKYPE
ms.assetid: 0f604c7d-87a3-4526-b67c-25648c8427e7
---


# Configuring SDN Interface logging options

 **Last modified:** February 23, 2017
  
    
    

**Applies to:** Lync Server 2013 | Skype for Business 2015

Skype for Business SDN Interface version 2.2 and later uses the logging infrastructure of the  [Enterprise Libraries 5.0](https://msdn.microsoft.com/en-us/library/ff632023.aspx). You can inspect and modify the configuration file by using the Microsoft Enterprise Library Configuration Tool. For a complete documentation on the options and capabilities of the logging infrastructure, see  [Configuring the Logging Application Block](https://msdn.microsoft.com/en-us/library/ff664723%28v=pandp.50%29.aspx). 
  
    
    

The Enterprise Libraries also includes a configuration tool that provides an intuitive graphical user interface for adding and changing settings for the logging configuration. You can use this tool to configure the Dialog Listener logging options. This article explains how to configure the logging options in the DialogListener.exe.config and SDNManager.exe.config files directly. 
## Configure logging options

To manually set the most common logging configurations, do the following: 
  
    
    

1. Open the DialogListener.exe.config or SDNManager.exe.config file, as appropriate, using a text editor of choice. 
    
  
2. Search for the  `<loggingConfiguration>` section and make appropriate changes to entries under the `<listeners>`,  `<formatters>` and `<categorySource>` elements.
    
  - The  `<listeners>` element specifies the log output, including the log file location and file rotation policies. They refer to a particular formatter to be used. Each `<add>` element entry under `<listeners>` specifies a type of logging. You can add a new `<add>` entry to enable a specified type of logging, remove an `<add>` entry to disable a specified type of logging, or change an existing `<add>` entry to a different type of logging.
    
  
  - The  `<formatters>` element specifies the style for each log entry.
    
  
  - The  `<categorySource>` element specifiesDialog Listener and SDN Manager bindings to listener log channels. The **switchValue** attribute can have a value of either _Off_ or _All_, indicating whether the log channel is active. For a more thorough investigation of issues, you must start the debug channel by setting the **switchValue** to _All_, as shown in the example below. 
    
  
The default configuration ensures that at most 10 log files per type are written. Consequently, at most only the last 10 log files are available for each logging type. 
  
    
    

> [!NOTE]
> Modifying the log settings in the config files does NOT require a restart of the service. These changes are picked up automatically by the Enterprise Logging functionality. 
  
    
    

The examples illustrated updates to the logging configuration files. The first example shows setting the **switchValue** to _All_. The second example shows modifying the logging file path to "c:\\other" for the **LNEAppLog** logging type.
  
    
    



```xml

<add switchValue="All" name="Debug">
        <listeners>
             <add name="LNEAppLog" />
        </listeners>
 </add>

```




```xml

<listeners>
  <add name="LNEAppLog"
     type="…" 
     listenerDataType="…" 
     fileName="C:\\other\\DialogListener.log" 
     footer="" 
     formatter="LNEDetailFormatter" 
     header="" 
     rollFileExistsBehavior="Increment" 
     rollInterval="Day" 
     rollSizeKB="100000" 
     traceOutputOptions="LogicalOperationStack, 
     DateTime, Timestamp, ProcessId, ThreadId, Callstack"/>
</listeners>

```


## Logging configuration options for the Dialog Listener

The Dialog Listener default configuration supports the following types of logging: 
  
    
    


|**Logging type**|**Description**|
|:-----|:-----|
|LNEAppLog |Logs the application execution states, including Info and Error types of data as well as adding activated debug channel. The output is recorded in the DialogListener.log file. This file will not contain any personal identifying information. |
|AllDataLog |Logs all input data, including session initiation (SIP) messages with session description (SDP) headers, and raw quality of experience reports. |
|QoEInputDataLog |Logs the quality of experience raw data from the Skype for Business Server. |
|DialogDataLog |Logs the dialog data, including SIP messages and SDP headers. |
   

## Logging configuration options for the SDN Manager

The SDN Manager default configuration supports the following types of logging: 
  
    
    


|**Logging type**|**Description**|
|:-----|:-----|
|LNEAppLog |Logs the application execution states, including Info and Error types of data as well as adding activated debug channel. The output is recorded in the SDNManager.log file. This file will not contain any personal identifying information. |
|AllOutputLog |Logs all output data sent to subscribers. The output is recorded in the AllOutputManager.log file |
|AllInputLog |Logs all input data received from Dialog Listeners. This data can be replayed and the output is recorded in the AllInputManager.log file. |
|ErrorMsgLog |Logs individual messages about processing issues and the respective error messages for further investigation. The output is recorded in the ErrorMsgManager.log file. |
|LogOutputLog |Logs messages received at the Message Receiver Log Service. The output is recorded in the LogOutput.log file. For more information, see  [Debugging the SDN Interface SDN Manager](debugging-the-sdn-manager.md). |
|QoEInputDataLog |Logs the quality of experience raw data of the Skype for Business Server received from Dialog Listeners. The output is recorded in the QoEInputManager.log file. |
|QoEDataLog |Logs the raw QoE reports. The output is recorded in the QoEDataManager.log file. |
|DialogDataLog |Logs the dialog data, including SIP messages, received from the Dialog Listeners. The output is recorded in the DialogData.log file. |
   

> [!IMPORTANT]
> Both the DialogListener.log and SDNManager.log files do not contain person-identifiable information, even in debug mode; however, the other log files might contain un-obfuscated user aliases, names and telephone numbers. Activate these logs with caution. 
  
    
    


## Additional resources


-  [Configuring Skype for Business SDN Interface](configuring-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  

  
    
    


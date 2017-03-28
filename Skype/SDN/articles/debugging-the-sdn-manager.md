---
title: Debugging the SDN Interface SDN Manager
ms.prod: SKYPE
ms.assetid: 5567fd7c-4567-47c4-8aa2-456564fcd087
---


# Debugging the SDN Interface SDN Manager

 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

The Skype for Business SDN Interface provides several advanced features to improve security and to help you debug configuration issues. 
  
    
    


## Activating WCF service logging

The SDN Manager supports advanced debugging by using the Windows Communication Foundation (WCF). Instead of detailing how to use or change this, we provide an example as an illustration as follows. For complete information on this subject, see the  [WCF documentation](https://msdn.microsoft.com/en-us/library/dd456779%28v=vs.110%29.aspx). By uncommenting and customizing the section that follows, which is an excerpt from an SDNManager.exe.config file, you can receive low level debugging logs from the WCF service. 
  
    
    

```xml

<system.diagnostics>
    <trace autoflush="true" indentsize="4">
      <listeners>
        <add name="file" type="System.Diagnostics.TextWriterTraceListener" initializeData="trace.log"/>
      </listeners> 
    </trace>
    <sources>
      <source name="System.Net">
        <listeners>
          <add name="System.Net"/>
        </listeners>
      </source>
      <source name="System.Net.HttpListener">
        <listeners>
          <add name="System.Net"/>
        </listeners>
      </source>
      <source name="System.Net.Sockets">
        <listeners>
          <add name="System.Net"/>
        </listeners>
      </source>
      <source name="System.Net.Cache">
        <listeners>
          <add name="System.Net"/>
        </listeners>
      </source>
      <source name="System.ServiceModel"
                switchValue="Information, ActivityTracing"
                propagateActivity="true">
        <listeners>
          <add name="sdt"
              type="System.Diagnostics.XmlWriterTraceListener"
              initializeData= "c:\\logdir\\log.e2e" />
        </listeners>
      </source>
    </sources>
    <switches>
      <add name="System.Net" value="Verbose"/>
      <add name="System.Net.Sockets" value="Verbose"/>
      <add name="System.Net.Cache" value="Verbose"/>
      <add name="System.Net.HttpListener" value="Verbose" />
    </switches>
    <sharedListeners>
      <add name="System.Net"
        type="System.Diagnostics.TextWriterTraceListener"
              initializeData="C:\\logdir\\Tracing.log" traceOutputOptions = "DateTime" />
    </sharedListeners>
  </system.diagnostics>

```


## Using the Message Receiver log service

The SDN Manager contains a simple REST web service component that can act as a subscriber and receive messages and log them for later analysis. You can use this built-in service to simulate a SDN Interface-aware network controller or other subscriber. 
  
    
    
The log service can be started using both HTTP and HTTPS and will receive random XML messages and to log them to the "LogOutput.log" log file (RelayData - log channel). 
  
    
    



```xml

<service name="Microsoft.Rtc.Enlightenment.Hub.LogService">
        <endpoint address="http://localhost:9333/Log" behaviorConfiguration="webby" bindingConfiguration="wsHttpEndpointBindingNoSec"
          binding="webHttpBinding" name="ep1" contract="Microsoft.Rtc.Enlightenment.Hub.ILogService" />
        <endpoint address="https://localhost:9332/Log" behaviorConfiguration="webby" 
          binding="webHttpBinding" bindingConfiguration="wsHttpEndpointBindingNoCert"
          name="ep1" contract="Microsoft.Rtc.Enlightenment.Hub.ILogService">
          <identity>
            <dns value="ServerSideCert" />
          </identity>
        </endpoint>
      </service>
```

You can configure the subscriber to use the log service by setting the  `submitUri` key value to " `http://localhost:9333/Log/PostStuffHere`", instead of an actual network controller's web service address like  `SDNManager.exe p s logsrv submituri=http://localhost:9333/Log/PostStuffHere`. 
  
    
    

> [!NOTE]
> You can also view the last megabyte of messages received by browsing to  `http://localhost:9333/Log/GetStuffHere`. 
  
    
    


## Replaying logs

As another debugging feature, the SDN Manager allows you to replay recorded input logs. The SDN Manager can log all messages it receives from the Dialog Listener. By using the SDN Manager command line tool, you can send such a log to the SDN Manager service and have it replayed and executed. Furthermore, the logs can be replayed with or without the original timing. 
  
    
    
To replay logs, do the following: 
  
    
    

1. Activate logging  `AllInputLog` Listener in the SDNManager.exe.config file (see [Configuring SDN Interface logging options](http://msdn.microsoft.com/library/bd414d81-e05e-41ab-834f-7a97093a628d.aspx)). Only the logs generated by the SDN Manager can be replayed from this Listener. 
    
  
2. Launch the SDNManager.exe using the **/e** or **/et** parameters to replay a portion of recorded logs, for example, from the file AllInput.log.
    
  
3. Launch the DialogListener.exe using either the **/e** or **/et** parameters to replay recorded SIP log files either recorded by the SDN Manager or from Skype for Business Server itself using OcsLogger.exe or Snooper.exe. See [Centralized Logging Service in Skype for Business 2015](https://technet.microsoft.com/en-us/library/jj688145.aspx) for details.
    
  


---
title: Dialog Listener configuration file example
ms.prod: SKYPE
ms.assetid: 6c325272-f444-4b8e-a9ac-80ebec5e2bff
---


# Dialog Listener configuration file example

 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2010 | Lync Server 2013 | Skype for Business 2015

The following code sample is an example of the Dialog Listener configuration file. 
  
    
    


## A sample of DialogListener.exe.config file


```xml

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="loggingConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LoggingSettings, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" requirePermission="true"/>
  </configSections>
 
  <loggingConfiguration name="" tracingEnabled="true" defaultCategory="Error">
    <listeners>
      <add name="LNEAppLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\DialogListener.log" footer="" formatter="LNEDetailFormatter" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000" traceOutputOptions="LogicalOperationStack, DateTime, Timestamp, ProcessId, ThreadId, Callstack"/>
      <add name="AllDataLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\AllData.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
      <add name="QoEInputDataLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\QoEInputData.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
      <add name="DialogDataLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\DialogData.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
    </listeners>
    <formatters>
      <add type="Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" template="{timestamp(local:O)}{tab}{message}{tab}{dictionary({tab}{key}: {value})}" name="LNEOverviewFormatter"/>
      <add type="Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" template="{timestamp(local:O)}{tab}[{category}]{tab}{message}{tab}{dictionary({tab}{key}: {value})}" name="LNEDetailFormatter"/>
      <add type="Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" template="{message}{newline}" name="SimpleOutput"/>
    </formatters>
    <categorySources>
      <add switchValue="All" name="Debug">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </add>
      <add switchValue="All" name="Error">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </add>
      <add switchValue="Off" name="QoEInputData">
        <listeners>
          <add name="QoEInputDataLog"/>
          <add name="AllDataLog"/>
        </listeners>
      </add>
      <add switchValue="Off" name="DialogData">
        <listeners>
          <add name="AllDataLog"/>
          <add name="DialogDataLog"/>
        </listeners>
      </add>
      <add switchValue="All" name="Info">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </add>
      <add switchValue="All" name="TransmissionError">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </add>
    </categorySources>
    <specialSources>
      <allEvents switchValue="Off" name="All Events"/>
      <notProcessed switchValue="ActivityTracing" name="Unprocessed Category">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </notProcessed>
      <errors switchValue="Off" name="Logging Errors &amp;amp; Warnings">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </errors>
    </specialSources>
  </loggingConfiguration>
  <appSettings>
    <add key="configurationserviceuri" value="http://sdnpool.LNEPROD.contoso.com:9333/Settings"/>
    <add key="configurationcertificate" value=""/> <!-- thumbprint of a client certificate to use to authenticate the DL with the SM -->
    <add key="configurationrefresh" value="00:01:00"/> <!-- Period for refreshing the settings from the configuration service -->
    <add key="checkdns" value="True"/>   <!-- use a URI provided by the DNS SRV record for locating the configuration service -->
    <add key="msplidentifier" value="SDN22"/>
  </appSettings>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
  </startup>
  <system.net>
    <settings>
      <servicePointManager checkCertificateName="true" checkCertificateRevocationList="true"/>
    </settings>
    <connectionManagement>
      <add address="*" maxconnection="50"/> <!-- Allow 50 concurrent connections, default is 2-->
    </connectionManagement>
  </system.net>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="System.Xml.Linq" culture="neutral" publicKeyToken="b77a5c561934e089"/>
        <bindingRedirect oldVersion="3.5.0.0" newVersion="4.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Configuration.Install" culture="neutral" publicKeyToken="b03f5f7f11d50a3a"/>
        <bindingRedirect oldVersion="2.0.0.0" newVersion="4.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="Microsoft.VisualC" culture="neutral" publicKeyToken="b03f5f7f11d50a3a"/>
        <bindingRedirect oldVersion="8.0.0.0" newVersion="10.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="Microsoft.Rtc.Internal.Media" culture="neutral" publicKeyToken="31bf3856ad364e35"/>
        <bindingRedirect oldVersion="4.0.0.0" newVersion="5.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Core" culture="neutral" publicKeyToken="b77a5c561934e089"/>
        <bindingRedirect oldVersion="3.5.0.0" newVersion="4.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Xml" culture="neutral" publicKeyToken="b77a5c561934e089"/>
        <bindingRedirect oldVersion="2.0.0.0" newVersion="4.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="mscorlib" culture="neutral" publicKeyToken="b77a5c561934e089"/>
        <bindingRedirect oldVersion="2.0.0.0" newVersion="4.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System" culture="neutral" publicKeyToken="b77a5c561934e089"/>
        <bindingRedirect oldVersion="2.0.0.0" newVersion="4.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Configuration" culture="neutral" publicKeyToken="b03f5f7f11d50a3a"/>
        <bindingRedirect oldVersion="2.0.0.0" newVersion="4.0.0.0"/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Management" culture="neutral" publicKeyToken="b03f5f7f11d50a3a"/>
        <bindingRedirect oldVersion="2.0.0.0" newVersion="4.0.0.0"/>
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration> 

```



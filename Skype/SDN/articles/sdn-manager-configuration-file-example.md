---
title: SDN Manager configuration file example
ms.prod: SKYPE
ms.assetid: 2113f2b8-80b2-4110-a021-ad7df2d2b517
---


# SDN Manager configuration file example

 **Last modified:** February 23, 2017
  

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

The following is an example of the SDN Manager configuration file (SDNManager.exe.config). 
  

## A sample of the SDNManager.exe.config file


```xml

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="loggingConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LoggingSettings, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" requirePermission="true"/>
  </configSections>

  <loggingConfiguration name="" tracingEnabled="true" defaultCategory="Error">
    <listeners>
      <add name="LNEAppLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\SDNManager.log" footer="" formatter="LNEDetailFormatter" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000" traceOutputOptions="LogicalOperationStack, DateTime, Timestamp, ProcessId, ThreadId, Callstack"/>
      <add name="AllOutputLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\AllOutputManager.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
      <add name="AllInputLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\AllInputManager.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
      <add name="ErrorMsgLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\ErrorMsgManager.log" footer="---------------" formatter="LNEDetailFormatter" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
      <add name="LogOutputLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\LogOutput.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
      <add name="QoEInputDataLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\QoEInputManager.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
      <add name="QoEDataLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\QoEDataManager.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
      <add name="DialogDataLog" type="Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.RollingFlatFileTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.RollingFlatFileTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" fileName="c:\\temp\\DialogData.log" footer="" formatter="SimpleOutput" maxArchivedFiles="10" header="" rollFileExistsBehavior="Increment" rollInterval="Day" rollSizeKB="10000"/>
    </listeners>
    <formatters>
      <add type="Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" template="{timestamp(local:O)}{tab}{message}{tab}{dictionary({tab}{key}: {value})}" name="LNEOverviewFormatter"/>
      <add type="Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" template="{timestamp(local:O)}{tab}[{category}]{tab}{message}{tab}{dictionary({tab}{key}: {value})}" name="LNEDetailFormatter"/>
      <add type="Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" template="{message}{newline}" name="SimpleOutput"/>
    </formatters>
    <categorySources>
      <add switchValue="Off" name="Debug">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </add>
      <add switchValue="All" name="Error">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </add>
      <add switchValue="All" name="Info">
        <listeners>
          <add name="LNEAppLog"/>
        </listeners>
      </add>
      <add switchValue="Off" name="QoEInputData">
        <listeners>
          <add name="AllInputLog"/>
        </listeners>
      </add>
      <add switchValue="Off" name="QoEData">
        <listeners>
          <add name="QoEDataLog"/>
        </listeners>
      </add>
      <add switchValue="All" name="RelayData">
        <listeners>
          <add name="LogOutputLog"/>
        </listeners>
      </add>
      <add switchValue="Off" name="DialogData">
        <listeners>
          <add name="AllInputLog"/>
        </listeners>
      </add>
      <add switchValue="All" name="BreakingData">
        <listeners>
          <add name="ErrorMsgLog"/>
        </listeners>
      </add>
      <add switchValue="Off" name="OutputData">
        <listeners>
          <add name="AllOutputLog"/>
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
    <add key="configurationserviceuri" value="http://localhost:9333/Settings"/>
    <add key="configurationcertificate" value=""/>    <!-- thumbprint of a client certificate to use to authenticate the SDNManager with the configuration service -->
    <add key="configurationrefresh" value="00:00:50"/>    <!-- SDNManager: Period for refreshing the settings from the store -->
    <add key="checkdns" value="false"/>    <!-- Decide to choose the DNS record to locate the configuration service -->

    <add key="mode" value="Database"/>    <!-- Cache, Database, Redis - mode how to store call states and settings -->

    <add key="statedbserver" value="dblneprod"/>    <!-- Database server and credential for accessing call state and settings -->
    <add key="statedbname" value="SDNManager22"/>    <!-- Database name -->
    <add key="statedbusername" value=""/>    <!-- empty for integrated security -->
    <add key="statedbpassword" value=""/>
    <add key="redisconnectstring" value=""/>    <!-- Connect string to redis service -->
    <add key="redispassword" value=""/>    <!-- Password string to redis service -->
    <add key="usedapi" value="False"/>    <!-- Use password encryption -->
    <add key="identifier" value="MySDNDB"/>    <!-- Differentiator for SM configurations -->
  </appSettings>

  <system.net>
    <settings>
      <servicePointManager checkCertificateName="true" checkCertificateRevocationList="true"/>
    </settings>
    <connectionManagement>
      <add address="*" maxconnection="100"/>
      <!-- Allow 50 concurrent connections, default is 2-->
    </connectionManagement>
  </system.net>

  <system.web>
    <compilation debug="true" targetFramework="4.5"/>
    <httpRuntime targetFramework="4.5"/>
  </system.web>

  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5"/>
  </startup>

  <!--<system.diagnostics>
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
              initializeData= "c:\\logdir\\log.svclog" />
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
        initializeData="C:\\logdir\\Tracing.log" traceOutputOptions="DateTime"   />
    </sharedListeners>
  </system.diagnostics>
  -->

  <system.webServer>
    <httpCompression directory="c:\\temp" maxDiskSpaceUsage="500" minFileSizeForComp="0" noCompressionForProxies="false">
      <!--<httpCompression directory="%SystemDrive%\\inetpub\\temp\\IIS Temporary Compressed Files" maxDiskSpaceUsage="500" minFileSizeForComp="0" noCompressionForProxies="false">-->
      <staticTypes>
        <add mimeType="text/*" enabled="true"/>
        <add mimeType="message/*" enabled="true"/>
        <add mimeType="application/x-javascript" enabled="true"/>
        <add mimeType="application/atom+xml" enabled="true"/>
        <add mimeType="application/xaml+xml" enabled="true"/>
        <add mimeType="*/*" enabled="false"/>
        <add mimeType="application/json; charset=utf-8" enabled="true"/>
      </staticTypes>
      <dynamicTypes>
        <add mimeType="text/*" enabled="true"/>
        <add mimeType="message/*" enabled="true"/>
        <add mimeType="application/x-javascript" enabled="true"/>
        <add mimeType="*/*" enabled="false"/>
        <add mimeType="application/json;charset=utf-8" enabled="true"/>
      </dynamicTypes>
      <!--<scheme name="gzip" dll="c:\\temp\\gzip.dll" />-->
      <!--<scheme name="gzip" dll="%Windir%\\system32\\inetsrv\\gzip.dll" />-->
    </httpCompression>
  </system.webServer>
  <system.serviceModel>
    <bindings>
      <customBinding>
        <!-- order: TransactionFlow, ReliableSession, Security, CompositeDuplex, OneWay, StreamSecurity, MessageEncoding, Transport -->
        <binding name="CommunicationHub">
          <byteStreamMessageEncoding/>
          <httpTransport>
            <webSocketSettings transportUsage="Always"/>
          </httpTransport>
        </binding>
        <binding name="CommunicationHubSecure">
          <byteStreamMessageEncoding/>
          <httpsTransport requireClientCertificate="false">
            <webSocketSettings transportUsage="Always"/>
          </httpsTransport>
        </binding>
      </customBinding>
      <webHttpBinding>
        <binding name="wsHttpEndpointBindingNoSec" maxBufferSize="400000" maxBufferPoolSize="400000" maxReceivedMessageSize="400000" transferMode="Streamed">
          <readerQuotas maxDepth="32" maxStringContentLength="400000" maxArrayLength="400000"/>
        </binding>
        <binding name="wsHttpEndpointBinding" maxBufferSize="400000" maxBufferPoolSize="400000" maxReceivedMessageSize="400000" transferMode="Streamed">
          <readerQuotas maxDepth="32" maxStringContentLength="400000" maxArrayLength="400000"/>
          <security mode="Transport">
            <transport clientCredentialType="Certificate"/>
          </security>
        </binding>
        <binding name="wsHttpEndpointBindingNoCert" maxBufferSize="400000" maxBufferPoolSize="400000" maxReceivedMessageSize="400000" transferMode="Streamed">
          <readerQuotas maxDepth="32" maxStringContentLength="400000" maxArrayLength="400000"/>
          <security mode="Transport">
            <transport clientCredentialType="None"/>
          </security>
        </binding>
      </webHttpBinding>
    </bindings>
    <services>
      <service behaviorConfiguration="CustomValidator" name="Microsoft.Rtc.Enlightenment.Hub.LyncHandler">
        <endpoint address="http://localhost:9333/LDL" behaviorConfiguration="webby" binding="webHttpBinding" bindingConfiguration="wsHttpEndpointBindingNoSec" name="ep0" contract="Microsoft.Rtc.Enlightenment.Hub.ILyncHandler"/>
        <endpoint address="https://localhost:9332/LDL" behaviorConfiguration="webby" binding="webHttpBinding" bindingConfiguration="wsHttpEndpointBinding" name="ep1" contract="Microsoft.Rtc.Enlightenment.Hub.ILyncHandler">
          <identity>
            <dns value="ServerSideCert"/>
          </identity>
        </endpoint>
        <endpoint address="http://localhost:9333/Settings" behaviorConfiguration="webby" binding="webHttpBinding" bindingConfiguration="wsHttpEndpointBindingNoSec" name="ep2" contract="Microsoft.Rtc.Enlightenment.Hub.ILyncHandler"/>
        <endpoint address="https://localhost:9332/Settings" behaviorConfiguration="webby" binding="webHttpBinding" bindingConfiguration="wsHttpEndpointBinding" name="ep3" contract="Microsoft.Rtc.Enlightenment.Hub.ILyncHandler">
          <identity>
            <dns value="ServerSideCert"/>
          </identity>
        </endpoint>
      </service>
      <service name="Microsoft.Rtc.Enlightenment.Hub.LogService">
        <endpoint address="http://localhost:9333/Log" behaviorConfiguration="webby" binding="webHttpBinding" bindingConfiguration="wsHttpEndpointBindingNoSec" name="ep1" contract="Microsoft.Rtc.Enlightenment.Hub.ILogService"/>
        <endpoint address="https://localhost:9332/Log" behaviorConfiguration="webby" binding="webHttpBinding" bindingConfiguration="wsHttpEndpointBindingNoCert" name="ep1" contract="Microsoft.Rtc.Enlightenment.Hub.ILogService">
          <identity>
            <dns value="ServerSideCert"/>
          </identity>
        </endpoint>
      </service>
      <service name="Microsoft.Rtc.Enlightenment.Hub.ServerHandler" behaviorConfiguration="Websockets">
        <endpoint address="http://localhost:9333/WS" binding="customBinding" bindingConfiguration="CommunicationHub" name="epWS" contract="Microsoft.Rtc.Enlightenment.Hub.ISubscription"/>
        <endpoint address="https://localhost:9332/WS" binding="customBinding" bindingConfiguration="CommunicationHubSecure" name="epWSS" contract="Microsoft.Rtc.Enlightenment.Hub.ISubscription"/>
      </service>
    </services>
    <behaviors>
      <serviceBehaviors>
        <behavior name="Websockets">
          <serviceCredentials>
            <clientCertificate>
              <!-- <authentication certificateValidationMode="Custom" customCertificateValidatorType="Microsoft.Rtc.Enlightenment.Hub.CheckPinValidator, SDNManager" />-->
              <!--
              certificateValidationMode can take a value of ChainTrust, PeerTrust, ChainOrPeerTrust, None or Custom. None means that no certificate checking is done, Custom allows one to plug in a custom X509CertificateValidator (new, System.IdentityModel.Selectors namespace), PeerTrust forces a public key of the client certificate to be present in the 'Trusted People' certificate store on the service side and ChainTrust requests that the client cert can be validated against the root certificates on the server side. ChainOrPeerTrust just executes the OR operator on the last two.
              Remark: PeerTrust and ChainOrPeerTrust are also subjected to another attribute called trustedStoreLocation. If peer trust is demanded, one can specify where the public keys are present, meaning either in LocalMachine or CurrentUser store.

              revocationMode takes the following value list: None, Online or Cached. None is saying that CRL (Certificate Revocation List) is not checked. Online demands that service checks (at every request) whether the certificate is still valid and thus not revoked. Offline says that the certificate should only be checked against the cached CRL.
              Remark: Online does not mean that CRL will be downloaded from the CA CRL endpoint at every request. It means it will check a local copy of it at every request. Every CRL has a validity period, when it expires, it is downloaded again.
              
              <authentication certificateValidationMode="ChainTrust" revocationMode="Online"/>-->
            </clientCertificate>
          </serviceCredentials>
          <serviceDebug includeExceptionDetailInFaults="false"/>
        </behavior>
        <behavior name="CustomValidator">
          <serviceCredentials>
            <clientCertificate>
              <!-- <authentication certificateValidationMode="Custom" customCertificateValidatorType="Microsoft.Rtc.Enlightenment.Hub.AcceptAndLogValidator, SDNManager" /> -->
              <!--
              certificateValidationMode can take a value of ChainTrust, PeerTrust, ChainOrPeerTrust, None or Custom. None means that no certificate checking is done, Custom allows one to plug in a custom X509CertificateValidator (new, System.IdentityModel.Selectors namespace), PeerTrust forces a public key of the client certificate to be present in the 'Trusted People' certificate store on the service side and ChainTrust requests that the client cert can be validated against the root certificates on the server side. ChainOrPeerTrust just executes the OR operator on the last two.
              Remark: PeerTrust and ChainOrPeerTrust are also subjected to another attribute called trustedStoreLocation. If peer trust is demanded, one can specify where the public keys are present, meaning either in LocalMachine or CurrentUser store.

              revocationMode takes the following value list: None, Online or Cached. None is saying that CRL (Certificate Revocation List) is not checked. Online demands that service checks (at every request) whether the certificate is still valid and thus not revoked. Offline says that the certificate should only be checked against the cached CRL.
              Remark: Online does not mean that CRL will be downloaded from the CA CRL endpoint at every request. It means it will check a local copy of it at every request. Every CRL has a validity period, when it expires, it is downloaded again.
              
              <authentication certificateValidationMode="ChainTrust" revocationMode="Online"/>-->
            </clientCertificate>
          </serviceCredentials>
          <!-- To avoid disclosing metadata information, set the values below to false before deployment -->
          <serviceMetadata httpGetEnabled="false" httpsGetEnabled="false"/>
          <!-- To receive exception details in faults for debugging purposes, set the value below to true.  Set to false before deployment to avoid disclosing exception information -->
          <serviceDebug includeExceptionDetailInFaults="false"/>
          <serviceThrottling maxConcurrentCalls="300" maxConcurrentSessions="300" maxConcurrentInstances="600"/>
          <!-- maxconcurrentcalls: default is 16 * cpus, maxconcurrentsessions: default 100*cpus, maxconcurrentinstances= sum of others -->
        </behavior>
      </serviceBehaviors>
      <endpointBehaviors>
        <behavior name="webby">
          <webHttp/>
        </behavior>
      </endpointBehaviors>
    </behaviors>
    <protocolMapping>
      <add scheme="https" binding="webHttpBinding"/>
    </protocolMapping>
    <!--<serviceHostingEnvironment aspNetCompatibilityEnabled="true" multipleSiteBindingsEnabled="true" />-->
  </system.serviceModel>
</configuration>

```



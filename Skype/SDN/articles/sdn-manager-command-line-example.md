---
title: SDN Manager command-line example
ms.prod: SKYPE
ms.assetid: 7a7c63ad-4f64-46a1-9e2c-2dd51f261aeb
---


# SDN Manager command-line example

 **Last modified:** February 24, 2017
  
    
    

**Applies to:** Lync Server 2013 | Skype for Business 2015

The following code examples show the input and output resulting from running the SDN Manager console application. Examples are shown for the Manager, the Listener, and the Subscriber. 
  
    
    


## Manager settings


```xml

C:\\Program Files\\Microsoft Skype for Business Server\\Microsoft Skype for Business SDN Manager>SDNManager.exe d m
Response code: SUCCESS Detail: TimeStamp: 2016-12-30T19:14:37.0170985Z
<Configuration Version="2.0" culture="en-US" Kind="Manager" Identifier="DEFAULT">
  <parameter key="calltimeout">1.00:00:00</parameter>
  <parameter key="endedtimeout">00:00:15</parameter>
  <parameter key="invitetimeout">00:03:00</parameter>
  <parameter key="maxcachesize">20000</parameter>
  <parameter key="qoetimeout">00:00:05</parameter>
  <parameter key="timeouthandlerperiod">00:00:10</parameter>
  <parameter key="hidepii">False</parameter>
  <parameter key="applicationsharing-AppliedBandwidthLimitAcceptable">500000</parameter>
  <parameter key="applicationsharing-AppliedBandwidthLimitOptimal">1000000</parameter>
  <parameter key="applicationsharing-JitterInterArrivalAcceptable">100</parameter>
  <parameter key="applicationsharing-JitterInterArrivalOptimal">50</parameter>
  <parameter key="applicationsharing-RDPTileProcessingLatencyAverageAcceptable">400</parameter>
  <parameter key="applicationsharing-RDPTileProcessingLatencyAverageOptimal">200</parameter>
  <parameter key="applicationsharing-RDPTileProcessingLatencyBurstDensityAcceptable">200</parameter>
  <parameter key="applicationsharing-RDPTileProcessingLatencyBurstDensityOptimal">100</parameter>
  <parameter key="applicationsharing-RelativeOneWayAverageAcceptable">1.75</parameter>
  <parameter key="applicationsharing-RelativeOneWayAverageOptimal">1</parameter>
  <parameter key="applicationsharing-RelativeOneWayBurstDensityAcceptable">2000</parameter>
  <parameter key="applicationsharing-RelativeOneWayBurstDensityOptimal">1000</parameter>
  <parameter key="applicationsharing-SpoiledTilePercentTotalAcceptable">36</parameter>
  <parameter key="applicationsharing-SpoiledTilePercentTotalOptimal">11</parameter>
  <parameter key="audio-DegradationAvgAcceptable">1</parameter>
  <parameter key="audio-DegradationAvgOptimal">0.6</parameter>
  <parameter key="audio-DeviceCaptureNotFunctioningEventRatioAcceptable">0.3</parameter>
  <parameter key="audio-DeviceCaptureNotFunctioningEventRatioOptimal">0.1</parameter>
  <parameter key="audio-DeviceHalfDuplexAECEventRatioAcceptable">0.3</parameter>
  <parameter key="audio-DeviceHalfDuplexAECEventRatioOptimal">0.1</parameter>
  <parameter key="audio-DeviceRenderNotFunctioningEventRatioAcceptable">0.3</parameter>
  <parameter key="audio-DeviceRenderNotFunctioningEventRatioOptimal">0.1</parameter>
  <parameter key="audio-JitterInterArrivalAcceptable">25</parameter>
  <parameter key="audio-JitterInterArrivalOptimal">15</parameter>
  <parameter key="audio-PacketLossRateAcceptable">0.05</parameter>
  <parameter key="audio-PacketLossRateOptimal">0.02</parameter>
  <parameter key="audio-RatioCompressedSamplesAvgAcceptable">1</parameter>
  <parameter key="audio-RatioCompressedSamplesAvgOptimal">1</parameter>
  <parameter key="audio-RatioConcealedSamplesAvgAcceptable">0.07</parameter>
  <parameter key="audio-RatioConcealedSamplesAvgOptimal">0.03</parameter>
  <parameter key="audio-RatioStretchedSamplesAvgAcceptable">1</parameter>
  <parameter key="audio-RatioStretchedSamplesAvgOptimal">1</parameter>
  <parameter key="audio-RoundTripAcceptable">500</parameter>
  <parameter key="audio-RoundTripOptimal">200</parameter>
  <parameter key="video-DynamicCapabilityPercentAcceptable">10</parameter>
  <parameter key="video-DynamicCapabilityPercentOptimal">5</parameter>
  <parameter key="video-LowFrameRateCallPercentAcceptable">10</parameter>
  <parameter key="video-LowFrameRateCallPercentOptimal">5</parameter>
  <parameter key="video-LowResolutionCallPercentAcceptable">10</parameter>
  <parameter key="video-LowResolutionCallPercentOptimal">5</parameter>
  <parameter key="video-RecvFrameRateAverageAcceptable">7</parameter>
  <parameter key="video-RecvFrameRateAverageOptimal">12</parameter>
  <parameter key="video-VideoFrameRateAvgAcceptable">7</parameter>
  <parameter key="video-VideoFrameRateAvgOptimal">12</parameter>
  <parameter key="video-VideoLocalFrameLossPercentageAvgAcceptable">10</parameter>
  <parameter key="video-VideoLocalFrameLossPercentageAvgOptimal">5</parameter>
  <parameter key="video-VideoPacketLossRateAcceptable">0.1</parameter>
  <parameter key="video-VideoPacketLossRateOptimal">0.05</parameter>
  <parameter key="video-VideoPostFECPLRAcceptable">0.1</parameter>
  <parameter key="video-VideoPostFECPLROptimal">0.05</parameter>
  <parameter key="build">6.0.9323.0</parameter>
</Configuration>

```


## Listener settings


  
    
    

```xml

C:\\Program Files\\Microsoft Skype for Business Server\\Microsoft Skype for Business SDN Manager>SDNManager.exe d l pool1.enlightenment.contoso.com

Response code: SUCCESS Detail: TimeStamp: 2016-12-29T23:08:01.3041954Z
<Configuration Version="2.0" culture="en-US" Kind="Listener" Identifier="POOL1.LNEPROD.CONTOSO.COM" LastModified="2016-12-29T23:08:01.3041954Z">
  <parameter key="submituri">http://sdnpool.lneprod.contoso.com:9333/LDL</parameter>
  <parameter key="alternativeuri" />
  <parameter key="clientcertificateid" />
  <parameter key="submitqueuelen">1000</parameter>
  <parameter key="maxbackoff">30</parameter>
  <parameter key="maxdelaylimit">5</parameter>
  <parameter key="maxopen">100</parameter>
  <parameter key="maxretry">100</parameter>
  <parameter key="maxretrybeforefailover">10</parameter>
  <parameter key="requester">FE1LNEPROD.LNEPROD.contoso.com</parameter>
</Configuration> 

```


## Subscriber settings

```xml

C:\\Program Files\\Microsoft Skype for Business Server\\Microsoft Skype for Business SDN Manager>SDNManager.exe d s first

Response code: SUCCESS Detail: TimeStamp: 2015-05-06T06:45:10.2224428Z
<Configuration Version="2.0" culture="en-US" Kind="Subscriber" Identifier="first">
  <parameter key="submituri">http://localhost:9333/Log/poststuffhere</parameter>
  <parameter key="outputschema">D</parameter>
  <parameter key="clientcertificateid"></parameter>
  <parameter key="domainfilters"></parameter>
  <parameter key="subnetfilters"></parameter>
  <parameter key="tenantfilters"></parameter>
  <parameter key="trunkfilters"></parameter>
  <parameter key="quality">True</parameter>
  <parameter key="signaling">False</parameter>
  <parameter key="sendrawsdp">False</parameter>
  <parameter key="maxbackoff">30</parameter>
  <parameter key="maxdelaylimit">25</parameter>
  <parameter key="maxopen">100</parameter>
  <parameter key="maxretry">100</parameter>
  <parameter key="submitqueuelen">1000</parameter>
  <parameter key="obfuscationseed" />
  <parameter key="schemaextension">true</parameter>
</Configuration>

```



---
title: Using the Skype for Business SDN Interface
ms.prod: SKYPE
ms.assetid: 542be3ea-3144-4e21-b320-c479cb0397bd
---


# Using the Skype for Business SDN Interface

 **Last modified:** February 24, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

Skype for Business service providers and customers can use the Skype for Business SDN Interface to obtain call and quality data about the states of audio, video and app-sharing streams produced by Lync and Skype for Business clients on the network. The SDN Interface relies on the Dialog Listener component to capture call and quality data and then dispatches the captured data to the SDN Manager to process the raw data. The processed and aggregated data is then sent to subscribers, (for example, network management system, network controllers, or IT administration tools), which can in turn correlate the data with their own observations from the network, readjust policies, or reallocate network resources dynamically to improve the service quality. 
  
For example, a Skype for Business service provider or customer finds that conference calls initiated during a certain time frame in a given geographical region regularly experience audio or video quality issues. To find out what happens, they can install and configure the SDN Interface in their Skype for Business topology and connect it with an SDN-aware network controller. The combined information from Skype for Business clients and the network controller will enable the service provider or customer to resolve issues within the Skype for Business deployment by, for example, identifying bad server components, resolve issues within the network by, for example, identifying a bottleneck in a segment of their network or router configuration during specific load patterns. As a result, they may adjust the network resources accordingly, or, in other cases, the analysis might result in a need to add more server resources or allocate additional PSTN ports/sessions to reduce the incidence of call quality issues. 

## Parsing Skype for Business call and quality data

The following are examples of SDN Interface messages. 
  
    
    

```xml

<LyncDiagnostics Version="D">
    <ConnectionInfo>
          …
    </ConnectionInfo>
    <Start Type="audio">
        <From>
          …
        </From>
        <To>
          …
        </To>
        <Properties>
            <Protocol>UDP</Protocol>
            … 
        </Properties>
    </Start>
    <Start Type="audio">
       …
    </Start>
    <Start Type="video">
       …
    </Start>
    …
</LyncDiagnostics>

<LyncDiagnostics Version="D">
  <ConnectionInfo>
          …
  </ConnectionInfo>
  <QualityUpdate Type="audio">
    <From>……</From>
    <To>……</To>
    <Properties>
    …
    <PacketUtilization>1461</PacketUtilization>
    <JitterInterArrival>11</JitterInterArrival>
    <JitterInterArrivalMax>15</JitterInterArrivalMax>
    <DegradationAvg>0.06</DegradationAvg>
    <RatioConcealedSamplesAvg>0.000999001</RatioConcealedSamplesAvg>
    <RecvNoiseLevel>-51</RecvNoiseLevel>
    <RecvSignalLevel>-15</RecvSignalLevel>
    <BurstGapDuration>27720</BurstGapDuration>
    <DegradationMax>0.09</DegradationMax>
    <OverallMinNetworkMOS>4.15</OverallMinNetworkMOS>
    <OverallAvgNetworkMOS>4.19</OverallAvgNetworkMOS>
    …
    </Properties>
  </QualityUpdate>
    …
</LyncDiagnostics>

```

The XML output from the SDN Manager consists of a set of messages that have  `<LyncDiagnostics>` as their root element. Each message corresponds to a snapshot of the data streams in a call. A `<LyncDiagnostics>` element contains one `<ConnectionInfo>` child element and one or more of the `<Invite>`,  `<Start>`,  `<update>`,  `<Error>`,  `<InCallQuality>`,  `<Ended>`,  `<Bye>` and `<QualityUpdate>` elements. Optionally, a `<RawSDP>` element might be present to contain the SDP content of the SIP message that triggered this SDN message. These child elements fall into the following groups:
  
    
    

1. The first group, including  `<ConnectionInfo>`, and  `<RawSDP>`, describe attributes of the overall state of the call and connection between the endpoints involving the media streams. The group includes a call dialog identifier ( `<CallId>`) and conversation identifier ( `<ConversationId>`) among others, which can be used to correlate this call with other calls. 
    
  
2. The second group includes  `<Invite>`,  `<Error>`, and  `<Bye>`, which are used to forward signaling events about the overall call. These elements are optional. 
    
  
3. The third group includes  `<Start>`,  `<Update>`, and  `<Ended>`. They describe the individual states of the call media streams in real time. 
    
  
4. The last group, including  `<InCallQuality>` and `<QualityUpdate>`, provides insight into the quality of individual streams of the call. 
    
  
The **From** and **To** sections contain information related to the endpoints with the IP and Ports being most relevant for identifying the data streams (RTP). A quality update section provides numerous properties related to the end points and streams, as shown in the preceding code example. In the current Schema D and in most cases, only the first pair of From/To contains a complete set, while further instances only contain the data that may be different for the respective end points.
  
    
    

> [!NOTE]
> SDN Interface does not report the ports used for the RTCP traffic. 
  
    
    

For more information about SDN Manager-generated Skype for Business SDN Interface data, see  [Skype for Business SDN Interface reference](http://msdn.microsoft.com/library/553e325e-d48a-4e7b-b7ac-042f87253ed8.aspx). 
  
    
    

## Additional resources


-  [Understanding Skype for Business SDN Interface](understanding-sdn-interface.md)
    
  
-  [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)
    
  


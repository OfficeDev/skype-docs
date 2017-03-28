---
title: QualityPropertiesType complexType (Skype for Business SDN Interface 2.2, Schema "C")
ms.assetid: e2794aea-3eb8-573c-ae53-3b4c44faa480
---


# QualityPropertiesType complexType (Skype for Business SDN Interface 2.2, Schema "C")

 **Last modified:** October 07, 2015
  
    
    


## Type information


|||
|:-----|:-----|
|**Namespace**||
|**Schema file**|SDNInterface.Schema.C.xsd |
|**Extension base**|None |
   

## Definition


```XML

      <xs:complexType name="QualityPropertiesType">
      </xs:complexType>
      
```


## Elements and attributes

If the schema defines specific requirements, such as **sequence**, **minOccurs**, **maxOccurs**, and **choice**, see the definition section. 
  
    
    

### Child elements



|**Element**|**Type**|**Description**|
|:-----|:-----|:-----|
| [AppliedBandwidthLimit](appliedbandwidthlimit-element.md)|xs:unsignedInt |This is the actual bandwidth applied to the given send side stream given various policy settings (TURN, API, SDP, Policy Server, and so on). This is not to be confused with the effective bandwidth because there can be a lower effective bandwidth based on the bandwidth estimate. This is basically the maximum bandwidth the send stream can take barring limits imposed by the bandwidth estimate. |
| [AudioTimestampErrorMicMs](audiotimestamperrormicms-element.md)|Not defined |Speaking device clock drift rate, relative to CPU clock. Average error of microphone-captured-stream time stamp, in milliseconds, for the last 20 seconds of a call. |
| [AudioTimestampErrorSpkMs](audiotimestamperrorspkms-element.md)|Not defined |Average error of speech render stream time stamp, in milliseconds, or the last 20 seconds of the call. |
| [BitRateAvg](bitrateavg-element-1.md)|xs:string |Average bit rate, in bits per second, sent or received for a video stream and computed over the duration of the session. This includes raw video and transport bits. This metric is reported for video streams when available. (bits/s) |
| [BitRateMax](bitratemax-element.md)|xs:string |Maximum bit rate, in bits per second, sent or received for a video stream and computed over the duration of the session. This metric is reported for video streams when available. (bits/s) |
| [BurstDensity](burstdensity-element.md)|Not defined |Average burst density, as specified in [RFC3611] section 4.7.2, is computed with a Gmin=16 for the received RTP packets. This metric is reported for audio streams when available and measures the average density of packet Loss during bursts of losses during the call. This field MUST be populated and MUST be set to zero if no packets have been received. |
| [BurstDuration](burstduration-element.md)|Not defined |The average burst duration, as specified in [RFC3611] section 4.7.2, is computed with a Gmin=16 for the received RTP packets. This metric is reported for audio streams when available. (ms) |
| [BurstGapDensity](burstgapdensity-element.md)|Not defined |Average burst gap density, as specified in [RFC3611] section 4.7.2, computed with a Gmin=16 for the received RTP packets. This metric is reported for audio streams when available. |
| [BurstGapDuration](burstgapduration-element-1.md)|xs:string |Average burst gap duration (in microsecond, ms), as specified in [RFC3611] section 4.7.2, computed with a Gmin=16 for the received RTP packets. This metric is reported for audio streams when available. |
| [CaptureDevice](capturedevice-element-1.md)|Not defined |The name of a capture device used to produce the media of this stream. This device is in the FROM endpoint and usually represents a microphone. |
| [CaptureDeviceDriver](capturedevicedriver-element-1.md)|Not defined |Device driver name and version of the capture device used to produce the media of this stream |
| [Codec](codec-element-qualitypropertiestype-complextype-1.md)| [CodecType](codectype-complextype.md)|Describes the last codec used for the media. |
| [ConversationalMOS](conversationalmos-element.md)|xs:string |Conversational clarity index for remote party, as described in [ITUP.562] section 6.3. This metric is reported for all available modalities and media types. This field is unused and deprecated for Skype for Business clients 2013 and beyond. |
| [CPUInsufficientEventRatio](cpuinsufficienteventratio-element.md)|Not defined |Percentage of sessions where the insufficient CPU event was fired when CPU cycles are insufficient for processing with the current modalities and applications, establish causeing distortions in the audio channel. |
| [DegradationAvg](degradationavg-element-1.md)|Not defined |Difference between the OverallAvg value and the maximum possible MOS-LQO for the audio codec used in the session. This metric is reported for audio streams when available. |
| [DegradationJitterAvg](degradationjitteravg-element-1.md)|xs:string |Average fraction of the degradation jitter average applies to inter-arrival packet jitter. This metric is reported for audio streams when available. |
| [DegradationMax](degradationmax-element-1.md)|xs:string |Maximum degradation as the difference between the OverallMin and the maximum possible MOS-LQO for the audio codec used in the session. This metric is reported for audio streams when available. |
| [DegradationPacketLossAvg](degradationpacketlossavg-element-1.md)|Not defined |Average fraction of the DegradationAvg that was caused by packet loss. This metric is reported for audio streams when available. |
| [DeviceCaptureNotFunctioningEventRatio](devicecapturenotfunctioningeventratio-element-1.md)|Not defined |Percentage of sessions the DeviceCaptureNotFunctioning event was fired when the capture device currently being used for the session is not functioning correctly and, possibly, preventing one-way audio from working correctly. |
| [DeviceClippingEventRatio](deviceclippingeventratio-element.md)|Not defined |Percentage of sessions the DeviceClipping event was fired when a speaker clips the microphone, causing the remote listener receives clipping-induced distortions. It is important to avoid the microphone clipping. |
| [DeviceEchoEventRatio](deviceechoeventratio-element.md)|Not defined |Percentage of sessions the DeviceEchoEvent event was fired when a device or setup is causing echo beyond the compensatory ability of the system. |
| [DeviceHowlingEventCount](devicehowlingeventcount-element-1.md)|Not defined |Number of times during a session the DeviceHowlingEvent event was fired when audio feedback loop, caused by multiple endpoints sharing the audio path, is detected. |
| [DeviceNearEndToEchoRatioEventRatio](devicenearendtoechoratioeventratio-element.md)|Not defined |Percentage of sessions the DeviceNearEndToEcho event was fired when the user speech is too low compared to the echo being captured which impacts the users experience because it limits how easy it is to interrupt a user. The situation can be improved by reducing speaker volume or moving the microphone closer to the speaker. |
| [DeviceRenderNotFunctioningEventRatio](devicerendernotfunctioningeventratio-element.md)|Not defined |Percentage of sessions the DeviceRenderNotFunctioning event was fired when the render device currently being used for the session is not functioning correctly and, possibly, causing one-way audio issues. |
| [DynamicCapabilityPercent](dynamiccapabilitypercent-element.md)|Not defined |Percentage of time that the client is running under capability of less than 70% of expected capability for this type of CPU. Inbound and Outbound are identical because it measures the capability of the client instead of the channel. This metric is reported for video streams when available. (percent) |
| [EchoEventCauses](echoeventcauses-element-1.md)|Not defined |Reasons of device echo detection and reported for audio streams when available. The causes are coded with the following bit flags: "0x01" - Sample timestamps from capture or render device were poor quality. "0x04" - High level of echo remained after echo cancellation. "0x10" - Signal from capture device had significant instances of maximum signal level. |
| [EchoPercentMicIn](echopercentmicin-element.md)|xs:string |Percentage of time when echo is detected in the audio from the capture or microphone device prior to echo cancellation. This metric is reported for audio streams when available. |
| [EchoPercentSend](echopercentsend-element-1.md)|xs:string |Percentage of time when echo is detected in the audio from the capture or microphone device after echo cancellation. This metric is reported for audio streams when available. |
| [EchoReturn](echoreturn-element.md)|Not defined |Echo returns reported for audio streams, when available. |
| [FrameRate](framerate-element-1.md)|xs:decimal |Average frame rate (in frames per second). When available, this metric is only reported for application sharing streams and only for Skype for Business 2013. (frames/s) |
| [HDQualityRatio](hdqualityratio-element.md)|Not defined |Percentage of the duration of a call that is using the HD720 resolution. This metric is reported for video streams when available. (percent) |
| [HealerPacketDropRatio](healerpacketdropratio-element-1.md)|xs:string |Ratio of audio packets dropped by a healer over total number of audio packets received by the healer. This metric is reported for all modalities/media types when available. (percent) |
| [JitterInterArrival](jitterinterarrival-element.md)|Not defined |Average inter-arrival jitter, as specified in [RFC3550] section 6.4.1. This metric is reported for all available modalities/media types. (ms) |
| [JitterInterArrivalMax](jitterinterarrivalmax-element.md)|xs:string |Maximum inter-arrival jitter, as specified in [RFC3550] section 6.4.1. This metric is reported for all modalities/media types when available. (ms) |
| [LocalFrameLossPercentageAvg](localframelosspercentageavg-element.md)|xs:string |(Deprecated, use VideoLocalFrameLossPercentageAvg instead) Average percentage of video frames lost as they are displayed to the user, including frames recovered from network losses. This metric is reported for video streams when available. (percent) |
| [LowFrameRateCallPercent](lowframeratecallpercent-element-1.md)|Not defined |Percentage of time of the call where frame rate is less than 7.5 frames per second. This metric is reported for video streams when available. (percent) |
| [LowResolutionCallPercent](lowresolutioncallpercent-element-1.md)|Not defined |Percentage of time of the call where resolution is low. Threshold is 120 pixels for smaller dimension. This metric is reported for video streams when available. (percent) |
| [MicGlitchRate](micglitchrate-element.md)|Not defined |Average glitches per five minutes for the microphone capture. For good quality this should be less than one per five minutes. This will not be reported by audio/video conferencing servers, mediation servers, or IP phones. |
| [NetworkDelayEventRatio](networkdelayeventratio-element-1.md)|Not defined |Percentage of sessions the the NetworkDelayEvent event was fired when network latency is severe and impacting the experience by preventing interactive communication |
| [OverallAvgNetworkMOS](overallavgnetworkmos-element-1.md)|xs:string |Average of MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2, based on the audio codec used, the observed packet loss and inter-arrival packet jitter. This metric is reported for audio streams when available. |
| [OverallMinNetworkMOS](overallminnetworkmos-element.md)|xs:string |Minimum of MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2, based on the audio codec used, the observed packet loss and inter-arrival packet jitter. This metric is reported for audio streams when available. |
| [PacketLossRate](packetlossrate-element-1.md)|Not defined |Average fraction lost computed over the duration of the session, as specified in [RFC3550] section 6.4.1. This metric is reported for all available modalities and media types. (percent) |
| [PacketLossRateMax](packetlossratemax-element-1.md)|Not defined |Maximum fraction lost, as specified in [RFC3550] section 6.4.1, computed over the duration of the session. This metric is reported for all available modalities/media types. (percent) |
| [PacketUtilization](packetutilization-element.md)|xs:string |Number of Real-time Transport Protocol (RTP) packets received in the session. This metric is reported for all available modalities and media types. |
| [Protocol](protocol-element-qualitypropertiestype-complextype.md)|xs:string |Transmission protocol of the call such as TCP or UDP. |
| [RatioConcealedSamplesAvg](ratioconcealedsamplesavg-element-1.md)|Not defined |Ratio of the number of audio frames with samples generated by packet loss concealment to the total number of audio frames. This metric is reported for audio streams when available. |
| [RDPTileProcessingLatencyAverage](rdptileprocessinglatencyaverage-element.md)|Not defined |Average processing time for remote desktop protocol (RDP) tiles. A higher total value implies a longer delay in the viewing experience. When available, this metric is only reported for application sharing streams using Skype for Business 2013. (ms) |
| [RDPTileProcessingLatencyBurstDensity](rdptileprocessinglatencyburstdensity-element-1.md)|Not defined |Burst density in the processing time for remote desktop protocol (RDP) tiles. A "bursty" transmission is a transmission where data flows in unpredictable bursts as opposed to a steady stream. This metric is only reported for application sharing streams using Skype for Business 2013. |
| [RecvFrameRateAverage](recvframerateaverage-element-1.md)|Not defined |Average frames per second received for all video streams and computed over the duration of the session. This metric is reported for video streams when available. (frames/s) |
| [RecvListenMOS](recvlistenmos-element-1.md)|Not defined |MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2, for decoded audio received by the reporting entity during the session. This metric is reported for audio streams when available. |
| [RecvListenMOSMin](recvlistenmosmin-element-1.md)|Not defined |Minimum of the RecvListenMOS for the stream during the session. This metric is reported for audio streams when available. |
| [RecvNoiseLevel](recvnoiselevel-element.md)|xs:string |Received noise level in units of dB that is reported for audio streams when available. Average energy level of received audio is classified as noise, mono signal or the left channel of stereo signal. (dB) |
| [RecvSignalLevel](recvsignallevel-element.md)|xs:string |Received signal level in units of dB. This metric is reported for audio streams when available. Average energy level of received audio is classified as mono speech, or left channel of stereo speech. (dB) |
| [RelativeOneWayAverage](relativeonewayaverage-element.md)|Not defined |Average amount of one-way latency. Relative one-way latency measures the delay between the client and the server. This metric is only reported for application sharing streams using Skype for Business 2013. (ms) |
| [RelativeOneWayBurstDensity](relativeonewayburstdensity-element.md)|Not defined |Total one-way burst density involving unsteady transmission. An unsteady transmission is one where data flows in random bursts as opposed to a steady stream. This metric measures data flow between the client and the server and is only reported for application sharing streams using Skype for Business 2013. |
| [RenderDevice](renderdevice-element-1.md)|Not defined |The name of a render device used to provide the media to for this stream. This device is in the TO endpoint and usually represents a speaker. |
| [RenderDeviceDriver](renderdevicedriver-element.md)|Not defined |Device driver name and version of the render device used to consume the media of this call |
| [RoundTrip](roundtrip-element-1.md)|Not defined |Average network propagation round-trip time as specified in [RFC3550] section 6.4.1. This metric is reported for all modalities/media types when available. (ms) |
| [RoundTripMax](roundtripmax-element-1.md)|xs:string |Maximum network propagation round-trip time as specified in [RFC3550] section 6.4.1. This metric is reported for all modalities/media types when available. (ms) |
| [SendListenMOS](sendlistenmos-element.md)|Not defined |MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2, for pre-encoded audio sent by the reporting entity during the session. This metric is reported for audio streams when available. |
| [SendListenMOSMin](sendlistenmosmin-element.md)|Not defined |Minimum of the SendListenMOS for the stream over the duration of the session. This metric is reported for audio streams when available. |
| [SpeakerGlitchRate](speakerglitchrate-element-1.md)|Not defined |Average glitches per five minutes for the loudspeaker rendering. For good quality, this should be less than one per five minutes. This will not be reported by audio/video conferencing servers, mediation servers, or IP phones. |
| [SpoiledTilePercentAverage](spoiledtilepercentaverage-element.md)|xs:decimal |Average percentage of the content that did not reach the viewer but was instead discarded and overwritten by fresh content. When available, this metric is only reported for application sharing streams and only for Skype for Business 2013. (percent) |
| [SpoiledTilePercentTotal](spoiledtilepercenttotal-element-1.md)|Not defined |Total percentage of the content that did not reach the viewer but was instead discarded and overwritten by fresh content. When available, this metric is only reported for application sharing streams and only for Skype for Business 2013. (percent) |
| [StreamQuality](streamquality-element-1.md)|xs:string |Estimated quality of the stream: Good, Poor, Bad |
| [VGAQualityRatio](vgaqualityratio-element-1.md)|Not defined |Percentage of the duration of a call that is using the VGA resolution. This metric is reported for video streams when available. (percent) |
| [VideoFrameLossRate](videoframelossrate-element-1.md)|Not defined |Average fraction of frames lost on the video receiver side as computed over the duration of the session. This metric is reported for video streams when available. (frames/s) |
| [VideoLocalFrameLossPercentageAvg](videolocalframelosspercentageavg-element.md)|Not defined |Average percentage of video frames lost as they are displayed to the user, including frames recovered from network losses. This metric is reported for video streams when available. (percent) |
| [VideoPacketLossRate](videopacketlossrate-element.md)|Not defined |Average fraction lost, as specified in [RFC3550] section 6.4.1, computed over the duration of the session. This metric is reported for video streams when available. (packets/s) |
   

### Attributes

None. 
  
    
    


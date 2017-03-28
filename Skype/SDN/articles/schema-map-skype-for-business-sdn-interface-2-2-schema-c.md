---
title: Schema map (Skype for Business SDN Interface 2.2, Schema C)
ms.assetid: 74a95f25-c585-5b08-4d14-e3152a7207b9
---


# Schema map (Skype for Business SDN Interface 2.2, Schema C)
This topic shows the schema definition for **SDNInterface.Schema.C.xsd**.
 **Last modified:** October 08, 2015
  
    
    


```xsd

<?xml version="1.0" encoding="utf-8"?>
<xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema" version="C">
<!-- More documentation about the QoE metrics used: http://technet.microsoft.com/en-us/library/gg425961.aspx -->
  <xs:complexType name="CodecType">
    <xs:sequence>
      <xs:element minOccurs="0" name="Bandwidth" type="xs:string">
        <xs:annotation>
          <xs:documentation>Average estimated bandwidth.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="MaxBandwidth" type="xs:string">
        <xs:annotation>
          <xs:documentation>Upper limit of the estimated bandwidth.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
    <xs:attribute name="Name" type="xs:string" use="required">
      <xs:annotation>
        <xs:documentation>Name of the standard SIP codec used for the media stream.</xs:documentation>
      </xs:annotation>
    </xs:attribute>
  </xs:complexType>

  <xs:complexType name="BandwidthType">
    <xs:sequence>
      <xs:element minOccurs="0" name="Average" type="xs:long">
        <xs:annotation>
          <xs:documentation>Estimated average amount of the bandwidth.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Maximum" type="xs:long">
        <xs:annotation>
          <xs:documentation>Estimated upper limit of the bandwidth needed by this stream.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
    <xs:attribute name="Multiplexed" type="xs:int" use="optional">
      <xs:annotation>
        <xs:documentation>Number of times this stream is multiplexed (if > 1). This might mean the overall bandwidth requirement could be up to as many times.</xs:documentation>
      </xs:annotation>
    </xs:attribute>
  </xs:complexType>

  <xs:complexType name="QualityEndPointType">
    <xs:sequence>
      <xs:element minOccurs="0" name="Id" type="xs:string">
        <xs:annotation>
          <xs:documentation>Identifier of the endpoint.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="EPId" type="xs:string">
        <xs:annotation>
          <xs:documentation>Endpoint Id of the endpoint.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="URI" type="xs:anyURI">
        <xs:annotation>
          <xs:documentation>SIP URI of the user signed in via the endpoint as extracted from the SIP header.. 
          This field is obfuscated unless hidepii is set to false in the DialogListener configuration file.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Contact" type="xs:anyURI">
        <xs:annotation>
          <xs:documentation>
            SIP URI of the user as extracted from the Contact header of the underlying SIP message.
            This field is obfuscated unless hidepii is set to false in the DialogListener configuration file.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="IP" minOccurs="0" type="xs:string">
        <xs:annotation>
          <xs:documentation>IP address of the the media stream source or destination.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="Port" minOccurs="0" type="xs:unsignedInt">
        <xs:annotation>
          <xs:documentation>Port number of the destination or source of the media stream used by this endpoint.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="EPType" type="xs:string">
        <xs:annotation>
          <xs:documentation>
            Indicates that this endpoint is of the Skype for Business Room System type or not.
          </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Relay">
        <xs:annotation>
          <xs:documentation>IP Address of the first relay used in the media traffic. </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="RelayPort">
        <xs:annotation>
          <xs:documentation>Port number of the relay.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!-- end remove -->
      <xs:element minOccurs="0" name="Inside" type="xs:boolean">
        <xs:annotation>
          <xs:documentation>(Deprecated - since Skype for Business 2013, this field is not reliable anymore.) Indicates if the source is registered within the enterprise (True) or not (False).  </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="VPN" type="xs:boolean">
        <xs:annotation>
          <xs:documentation>Indicates if the user is on VPN (True) or not (False).</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Connection" type="xs:string">
        <xs:annotation>
          <xs:documentation>Connection type such as "wired" or "wireless".</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="BSSID">
        <xs:annotation>
          <xs:documentation>Id of an access point for a WiFi/wireless connection.</xs:documentation>
        </xs:annotation>
      </xs:element>
<!-- 2.1 -->
      <xs:element minOccurs="0" name="ReflexiveIP">
        <xs:annotation>
          <xs:documentation>IP used outside of the NAT.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="ReflexivePort">
        <xs:annotation>
          <xs:documentation>Port used on the NAT.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="UserAgent">
        <xs:annotation>
          <xs:documentation>Skype for Business client and version.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiDriverDeviceDesc">
        <xs:annotation>
          <xs:documentation>Wifi Driver Device description.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiDriverVersion">
        <xs:annotation>
          <xs:documentation>Wifi Driver Version.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="OS">
        <xs:annotation>
          <xs:documentation>Operating System used on the endpoint device.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="CPUName">
        <xs:annotation>
          <xs:documentation>Name of the CPU.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="CPUNumberOfCores">
        <xs:annotation>
          <xs:documentation>Number of CPU cores in the endpoint device.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="CPUProcessorSpeed">
        <xs:annotation>
          <xs:documentation>Processor speed rating.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Virtualization">
        <xs:annotation>
          <xs:documentation>Type of virtualization used.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="MACAddr">
        <xs:annotation>
          <xs:documentation>MAC address of the endpoint.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="DSCPInbound">
        <xs:annotation>
          <xs:documentation>QoS category marking when the stream is received on this endpoint. This field is populated only from Skype for Business clients newer than Skype for Business 2013. </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="DSCPOutbound">
        <xs:annotation>
          <xs:documentation>QoS category marking used on send the stream from this endpoint. This field is populated only from Skype for Business clients newer than Skype for Business 2013.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="LinkSpeed">
        <xs:annotation>
          <xs:documentation>Basic bandwidth of the connection.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
  </xs:complexType>
  
  
  <xs:complexType name="EndPointType">
    <xs:sequence>
      <xs:element minOccurs="0" name="Id" type="xs:string">
        <xs:annotation>
          <xs:documentation>Identifier of the endpoint.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="EPId" type="xs:string">
        <xs:annotation>
          <xs:documentation>Endpoint Id of the endpoint.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="URI" type="xs:anyURI">
        <xs:annotation>
          <xs:documentation>
            SIP URI of the user signed in via the endpoint as extracted from the SIP header..
            This field is obfuscated unless hidepii is set to false in the DialogListener configuration file.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Contact" type="xs:anyURI">
        <xs:annotation>
          <xs:documentation>
            SIP URI of the user as extracted from the Contact header of the underlying SIP message.
            This field is obfuscated unless hidepii is set to false in the DialogListener configuration file.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="IP" minOccurs="0" type="xs:string">
        <xs:annotation>
          <xs:documentation>IP address of the the media stream source or destination.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="Port" minOccurs="0" type="xs:unsignedInt">
        <xs:annotation>
          <xs:documentation>Port number of the destination or source of the media stream used by this endpoint.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="EPType" type="xs:string">
        <xs:annotation>
          <xs:documentation>
            Indicates that this endpoint is of the Skype for Business Room System type or not.
          </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Relay">
        <xs:annotation>
          <xs:documentation>IP Address of the first relay used in the media traffic. </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="RelayPort">
        <xs:annotation>
          <xs:documentation>Port number of the relay.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="ReflexiveIP">
        <xs:annotation>
          <xs:documentation>IP used outside of the NAT.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="ReflexivePort">
        <xs:annotation>
          <xs:documentation>Port used on the NAT.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="UserAgent" minOccurs="0" type="xs:string">
        <xs:annotation>
          <xs:documentation>Skype for Business client name and version.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="IncallEnabled" minOccurs="0" type="xs:boolean">
        <xs:annotation>
          <xs:documentation>Whether the endpoint (Skype for Business client) is capable to send incall quality update messages. This flag does not indicate whether the client is configured to send incall QoE reports.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
  </xs:complexType>

  <xs:complexType name="InviteType">
    <xs:sequence>
      <xs:element name="Caller" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Properties of the caller.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="Callee" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Properties of the callee.</xs:documentation>
        </xs:annotation>
      </xs:element>
    </xs:sequence>
  </xs:complexType>

  <xs:complexType name="StartPropertiesType">
    <xs:sequence>
      <xs:element name="Protocol" type="xs:string">
        <xs:annotation>
          <xs:documentation>Transmission protocol of the media stream such as TCP or UDP.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="Bandwidth" type="BandwidthType" minOccurs="0">
        <xs:annotation>
          <xs:documentation>
            Describes the maximum and average amount of bandwidth needed by this stream. It takes the possible 
            codecs and stream multiplexing into account.
          </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element maxOccurs="unbounded" name="Codec" type="CodecType" minOccurs="0">
        <xs:annotation>
          <xs:documentation>Codec and estimates for the bandwidth that the codecs will use. 
          This list contains all codecs that are agreed upon by the two endpoints. Both end-points may decide to 
          switch among these codecs at any time (without additional signalling).</xs:documentation>
        </xs:annotation>
      </xs:element>
    </xs:sequence>
  </xs:complexType>
  
  <xs:complexType name="StartOrUpdateType">
    <xs:sequence>
      
      <xs:element minOccurs="0" name="From" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Source of the media stream.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="To" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Destination of the media stream.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
      <xs:element minOccurs="0" name="Properties" type="StartPropertiesType">
        <xs:annotation>
          <xs:documentation>Properties of the started or updated media stream.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
    <xs:attribute name="Type" type="xs:string" use="required">
      <xs:annotation>
        <xs:documentation>Modality or media type for which the quality metrics are reported. 
        The valid options are audio, video and applicationsharing</xs:documentation>
      </xs:annotation>
    </xs:attribute>
  </xs:complexType>
  
  <xs:complexType name="QualityPropertiesType">
    <xs:sequence>
      <xs:choice maxOccurs="unbounded">
        <xs:element name="StreamQuality" type="xs:string">
          <xs:annotation>
            <xs:documentation>Estimated quality of the stream: Good, Poor, Bad</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="Protocol" type="xs:string">
          <xs:annotation>
            <xs:documentation>Transmission protocol of the call such as TCP or UDP.</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="Codec" type="CodecType">
          <xs:annotation>
            <xs:documentation>Describes the last codec used for the media.</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="ConversationalMOS" type="xs:string">
          <xs:annotation>
            <xs:documentation>Conversational clarity index for remote party, as described in [ITUP.562] section 6.3. 
            This metric is reported for all available modalities and media types.
            This field is unused and deprecated for Skype for Business clients 2013 and beyond.
          </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="PacketUtilization" type="xs:string">
          <xs:annotation>
            <xs:documentation>Number of Real-time Transport Protocol (RTP) packets received in the session. 
            This metric is reported for all available modalities and media types.</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="PacketLossRate">
          <xs:annotation>
            <xs:documentation>Average fraction lost computed over the duration of the session, as specified in [RFC3550] section 6.4.1. 
            This metric is reported for all available modalities and media types. (percent)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="PacketLossRateMax">
          <xs:annotation>
            <xs:documentation>Maximum fraction lost, as specified in [RFC3550] section 6.4.1, computed over the duration of the session.
            This metric is reported for all available modalities/media types. (percent)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="JitterInterArrival">
          <xs:annotation>
            <xs:documentation>Average inter-arrival jitter, as specified in [RFC3550] section 6.4.1. This metric
            is reported for all available modalities/media types. (ms)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="JitterInterArrivalMax" type="xs:string">
          <xs:annotation>
            <xs:documentation>Maximum inter-arrival jitter, as specified in [RFC3550] section 6.4.1. 
            This metric is reported for all modalities/media types when available. (ms)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RoundTrip">
          <xs:annotation>
            <xs:documentation>Average network propagation round-trip time as specified in [RFC3550] section 6.4.1. 
            This metric is reported for all modalities/media types when available. (ms)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RoundTripMax" type="xs:string">
          <xs:annotation>
            <xs:documentation>Maximum network propagation round-trip time as specified in [RFC3550] section 6.4.1. 
            This metric is reported for all modalities/media types when available. (ms)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="HealerPacketDropRatio" type="xs:string">
          <xs:annotation>
            <xs:documentation>Ratio of audio packets dropped by a healer over total number of audio packets received 
            by the healer. This metric is reported for all modalities/media types when available. (percent)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DegradationAvg">
          <xs:annotation>
            <xs:documentation>Difference between the OverallAvg value and the maximum possible MOS-LQO for the audio 
codec used in the session. This metric is reported for audio streams when available.</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RatioConcealedSamplesAvg">
          <xs:annotation>
            <xs:documentation>Ratio of the number of audio frames with samples generated by packet loss concealment to the total number of audio frames. This metric is reported for audio streams when available.</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="EchoPercentMicIn" type="xs:string">
          <xs:annotation>
            <xs:documentation>Percentage of time when echo is detected in the audio from the capture or 
            microphone device prior to echo cancellation. This metric is reported for audio streams when available.</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="EchoPercentSend" type="xs:string">
          <xs:annotation>
            <xs:documentation>Percentage of time when echo is detected in the audio from the capture or 
            microphone device after echo cancellation. This metric is reported for audio streams when available. </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="EchoReturn">
          <xs:annotation>
            <xs:documentation>Echo returns reported for audio streams, when available. </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="EchoEventCauses">
          <xs:annotation>
            <xs:documentation>Reasons of device echo detection and reported for audio streams when available. 
            The causes are coded with the following bit flags: 

"0x01" - Sample timestamps from capture or render device were poor quality.

"0x04" - High level of echo remained after echo cancellation.

"0x10" - Signal from capture device had significant instances of maximum signal level.
</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RecvNoiseLevel" type="xs:string">
          <xs:annotation>
            <xs:documentation>Received noise level in units of dB that is reported for audio streams when available.  
            Average energy level of received audio is classified as noise, mono signal or the left channel of stereo signal. (dB)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RecvSignalLevel" type="xs:string">
          <xs:annotation>
            <xs:documentation>Received signal level in units of dB.  
            This metric is reported for audio streams when available.  
            Average energy level of received audio is classified as mono speech, or left channel of stereo speech. (dB)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="BurstDuration">
          <xs:annotation>
            <xs:documentation>
              The average burst duration, as specified in [RFC3611] section 4.7.2, is computed with a Gmin=16 for the received RTP packets.
              This metric is reported for audio streams when available. (ms)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="BurstDensity">
          <xs:annotation>
            <xs:documentation>
              Average burst density, as specified in [RFC3611] section 4.7.2,
              is computed with a Gmin=16 for the received RTP packets.
              This metric is reported for audio streams when available and measures 
              the average density of packet Loss during bursts of losses during the call.
              This field MUST be populated and MUST be set to
              zero if no packets have been received.

            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="BurstGapDuration" type="xs:string">
          <xs:annotation>
            <xs:documentation>
              Average burst gap duration (in microsecond, ms), as specified in [RFC3611] section 4.7.2, computed with a Gmin=16 for the received RTP packets.
              This metric is reported for audio streams when available. 
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="BurstGapDensity">
          <xs:annotation>
            <xs:documentation>
              Average burst gap density, as specified in [RFC3611] section 4.7.2, computed with a Gmin=16 for the received RTP packets.
              This metric is reported for audio streams when available.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DegradationJitterAvg" type="xs:string">
          <xs:annotation>
            <xs:documentation>
              Average fraction of the degradation jitter average applies to inter-arrival packet jitter.
              This metric is reported for audio streams when available.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DegradationMax" type="xs:string">
          <xs:annotation>
            <xs:documentation>
              Maximum degradation as the difference between the OverallMin and the maximum possible MOS-LQO for the audio codec used in the session.
              This metric is reported for audio streams when available.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RecvListenMOSMin">
          <xs:annotation>
            <xs:documentation>
              Minimum of the RecvListenMOS for the stream during the session.  
              This metric is reported for audio streams when available.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="SendListenMOSMin">
          <xs:annotation>
            <xs:documentation>
              Minimum of the SendListenMOS for the stream over the duration of the session.
              This metric is reported for audio streams when available.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="SendListenMOS">
          <xs:annotation>
            <xs:documentation>MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2, for pre-encoded audio 
            sent by the reporting entity during the session. This metric is reported for audio streams when available.  </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RecvListenMOS">
          <xs:annotation>
            <xs:documentation>
              MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2, for decoded audio
              received by the reporting entity during the session. This metric is reported for audio streams when available.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="OverallMinNetworkMOS" type="xs:string">
          <xs:annotation>
            <xs:documentation>
              Minimum of MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2,
              based on the audio codec used, the observed packet loss and inter-arrival packet jitter.
              This metric is reported for audio streams when available.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="OverallAvgNetworkMOS" type="xs:string">
          <xs:annotation>
            <xs:documentation>
              Average of MOS-LQO wideband, as specified by [ITUP.800.1] section 2.1.2,
              based on the audio codec used, the observed packet loss and inter-arrival packet jitter.
              This metric is reported for audio streams when available.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DegradationPacketLossAvg">
          <xs:annotation>
            <xs:documentation>Average fraction of the DegradationAvg that was caused by packet loss. 
            This metric is reported for audio streams when available.
          </xs:documentation>
          </xs:annotation>
        </xs:element>
<!-- Added in 2.1 -->
        <xs:element name="CaptureDevice">
          <xs:annotation>
            <xs:documentation>
The name of a capture device used to produce the media of this stream. This device is in the FROM endpoint and usually represents a microphone.                  
          </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="CaptureDeviceDriver">
          <xs:annotation>
            <xs:documentation>
Device driver name and version of the capture device used to produce the media of this stream                    
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="MicGlitchRate">
          <xs:annotation>
            <xs:documentation>
              Average glitches per five minutes for the microphone capture. For good quality this should be less than one per five minutes.
              This will not be reported by audio/video conferencing servers, mediation servers, or IP phones.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="SpeakerGlitchRate">
          <xs:annotation>
            <xs:documentation>
              Average glitches per five minutes for the loudspeaker rendering. For good quality, this should be less than one per five minutes.
              This will not be reported by audio/video conferencing servers, mediation servers, or IP phones.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RenderDevice">
          <xs:annotation>
            <xs:documentation>
The name of a render device used to provide the media to for this stream. This device is in the TO endpoint and usually represents a speaker.                  
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RenderDeviceDriver">
          <xs:annotation>
            <xs:documentation>
Device driver name and version of the render device used to consume the media of this call                    
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="CPUInsufficientEventRatio">
          <xs:annotation>
            <xs:documentation>
Percentage of sessions where the insufficient CPU event was fired when 
CPU cycles are insufficient for processing with the current modalities and applications,  establish  
causeing distortions in the audio channel.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DeviceCaptureNotFunctioningEventRatio">
          <xs:annotation>
            <xs:documentation>
Percentage of sessions the DeviceCaptureNotFunctioning event was fired when 
the capture device currently being used for the session is not functioning correctly and, possibly, preventing one-way audio from working correctly.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DeviceRenderNotFunctioningEventRatio">
          <xs:annotation>
            <xs:documentation>
Percentage of sessions the DeviceRenderNotFunctioning event was fired when 
the render device currently being used for the session is not functioning correctly and, possibly, causing one-way audio issues.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DeviceClippingEventRatio">
          <xs:annotation>
            <xs:documentation>
Percentage of sessions the DeviceClipping event was fired

when a speaker clips the microphone, causing the remote listener receives clipping-induced distortions. It is important to avoid the microphone clipping.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DeviceHowlingEventCount">
          <xs:annotation>
            <xs:documentation>
Number of times during a session the DeviceHowlingEvent event was fired when audio feedback loop, caused by multiple endpoints sharing the audio path, is detected.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="NetworkDelayEventRatio">
          <xs:annotation>
            <xs:documentation>
Percentage of sessions the the NetworkDelayEvent event was fired when network latency is severe and impacting the experience by preventing interactive communication
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DeviceNearEndToEchoRatioEventRatio">
          <xs:annotation>
            <xs:documentation>
Percentage of sessions the DeviceNearEndToEcho event was fired when the user speech is too low compared to the echo being captured 
which impacts the users experience because it limits how easy it is to interrupt a user. 
The situation can be improved by reducing speaker volume or moving the microphone closer to the speaker.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DeviceEchoEventRatio">
          <xs:annotation>
            <xs:documentation>
Percentage of sessions the DeviceEchoEvent event was fired when a device or setup is causing echo beyond the compensatory ability of the system.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="AudioTimestampErrorMicMs">
          <xs:annotation>
            <xs:documentation>
Speaking device clock drift rate, relative to CPU clock.

Average error of microphone-captured-stream time stamp, in milliseconds, for the last 20 seconds of a call.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="AudioTimestampErrorSpkMs">
          <xs:annotation>
            <xs:documentation>
Average error of speech render stream time stamp, in milliseconds, or the last 20 seconds of the call.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        
        <xs:element name="VideoPacketLossRate">
          <xs:annotation>
            <xs:documentation>
              Average fraction lost, as specified in [RFC3550] section 6.4.1,
              computed over the duration of the session. This metric is reported for video streams when available. (packets/s)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RecvFrameRateAverage">
          <xs:annotation>
            <xs:documentation>Average frames per second received for all video streams and computed over the duration of the session. 
            This metric is reported for video streams when available. (frames/s)</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="VideoLocalFrameLossPercentageAvg">
          <xs:annotation>
            <xs:documentation>
              Average percentage of video frames lost as they are displayed to the user,
              including frames recovered from network losses. 
              This metric is reported for video streams when available. (percent) 
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="DynamicCapabilityPercent">
          <xs:annotation>
            <xs:documentation>
              Percentage of time that the client is running under capability of less
              than 70% of expected capability for this type of CPU. Inbound and Outbound are identical because it measures the capability of 
              the client instead of the channel. This metric is reported for video streams when available. (percent)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="LowFrameRateCallPercent">
          <xs:annotation>
            <xs:documentation>
              Percentage of time of the call where frame rate is less than 7.5 frames per second.
              This metric is reported for video streams when available.  (percent)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="LowResolutionCallPercent">
          <xs:annotation>
            <xs:documentation>
              Percentage of time of the call where resolution is low.
              Threshold is 120 pixels for smaller dimension.
              This metric is reported for video streams when available.  (percent)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="VideoFrameLossRate">
          <xs:annotation>
            <xs:documentation>
              Average fraction of frames lost on the video receiver side
              as computed over the duration of the session.
              This metric is reported for video streams when available. (frames/s)
            </xs:documentation>
          </xs:annotation>
        </xs:element>

        <!-- Attention: this field is obsolete and won't be filled anyomre. The content is the same as above VideoLocalFrameLossPercentageAvg -->
        <xs:element name="LocalFrameLossPercentageAvg" type="xs:string">
          <xs:annotation>
            <xs:documentation>
              (Deprecated, use VideoLocalFrameLossPercentageAvg instead)
              Average percentage of video frames lost as they are displayed to the user,
              including frames recovered from network losses.
              This metric is reported for video streams when available. (percent) 
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        
        <xs:element name="BitRateMax" type="xs:string">
          <xs:annotation>
            <xs:documentation>
              Maximum bit rate, in bits per second, sent or received for a video stream and 
              computed over the duration of the session.
              This metric is reported for video streams when available. (bits/s)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="BitRateAvg" type="xs:string">
          <xs:annotation>
            <xs:documentation>
              Average bit rate, in bits per second, sent or received for a video stream and
              computed over the duration of the session. This includes raw video and transport bits.
              This metric is reported for video streams when available. (bits/s)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="VGAQualityRatio">
          <xs:annotation>
            <xs:documentation>
              Percentage of the duration of a call
              that is using the VGA resolution.
              This metric is reported for video streams when available. (percent)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="HDQualityRatio">
          <xs:annotation>
            <xs:documentation>
              Percentage of the duration of a call
              that is using the HD720 resolution.
              This metric is reported for video streams when available. (percent)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RelativeOneWayBurstDensity">
          <xs:annotation>
            <xs:documentation>
              Total one-way burst density involving unsteady transmission. 
              An unsteady transmission is one where data flows in random bursts as opposed to a steady stream.
              This metric measures data flow between the client and the server and  
              is only reported for application sharing streams using Skype for Business 2013.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RelativeOneWayAverage">
          <xs:annotation>
            <xs:documentation>
              Average amount of one-way latency.
              Relative one-way latency measures the delay between the client and the server.
              This metric is only reported for application sharing streams using Skype for Business 2013. (ms)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RDPTileProcessingLatencyBurstDensity">
          <xs:annotation>
            <xs:documentation>
              Burst density in the processing time for remote desktop protocol (RDP) tiles.
              A "bursty" transmission is a transmission where data flows in unpredictable bursts as opposed to a steady stream.
              This metric is only reported for application sharing streams using Skype for Business 2013.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="RDPTileProcessingLatencyAverage">
          <xs:annotation>
            <xs:documentation>
              Average processing time for remote desktop protocol (RDP) tiles.
              A higher total value implies a longer delay in the viewing experience.
              When available, this metric is only reported for application sharing streams using Skype for Business 2013. (ms)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="SpoiledTilePercentTotal">
          <xs:annotation>
            <xs:documentation>
              Total percentage of the content that did not reach the viewer but was instead discarded and overwritten by fresh content.
              When available, this metric is only reported for application sharing streams and only for Skype for Business 2013. (percent)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="SpoiledTilePercentAverage" type="xs:decimal">
          <xs:annotation>
            <xs:documentation>
              Average percentage of the content that did not reach the viewer but was instead discarded and overwritten by fresh content.
              When available, this metric is only reported for application sharing streams and only for Skype for Business 2013. (percent)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="FrameRate" type="xs:decimal">
          <xs:annotation>
            <xs:documentation>
              Average frame rate (in frames per second).
              When available, this metric is only reported for application sharing streams and only for Skype for Business 2013. (frames/s)
            </xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element minOccurs="0" name="AppliedBandwidthLimit" type="xs:unsignedInt">
          <xs:annotation>
            <xs:documentation>
              This is the actual bandwidth applied to the given send side stream given various policy settings (TURN, API, SDP, Policy Server, and so on). This is not to be confused with the effective bandwidth because there can be a lower effective bandwidth based on the bandwidth estimate. This is basically the maximum bandwidth the send stream can take barring limits imposed by the bandwidth estimate.              
              </xs:documentation>
          </xs:annotation>
        </xs:element>
      </xs:choice>
    </xs:sequence>
  </xs:complexType>


  <xs:complexType name="QualityType">
    <xs:sequence>
      <xs:element name="From" type="QualityEndPointType">
        <xs:annotation>
          <xs:documentation>The source of the reported media stream. </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="To" type="QualityEndPointType">
        <xs:annotation>
          <xs:documentation>Destination of the media stream.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="Properties" type="QualityPropertiesType">
        <xs:annotation>
          <xs:documentation>Properties of the media stream, including a selected set of quality metrics reported and 
          thresholds that are used to determine a bad call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Route" type="RouteType">
        <xs:annotation>
          <xs:documentation>
            Network path of the media stream only provided in Skype for Business 2013 and when the traceRoute feature is activated in Skype for Business.
          </xs:documentation>
        </xs:annotation>
      </xs:element>
    </xs:sequence>
    <xs:attribute name="Type" type="xs:string" use="required">
      <xs:annotation>
        <xs:documentation>Modality or media type for which the quality metrics are reported. 
        The supported options are audio, video and applicationsharing. </xs:documentation>
      </xs:annotation>
    </xs:attribute>
  </xs:complexType>

  <xs:complexType name="RouteType">
    <xs:sequence>
      <xs:element maxOccurs="unbounded" name="Hop" type="xs:string">
        <xs:annotation>
          <xs:documentation>IP address of one hop (router, gateway, switch, etc) on the path from the source to the destination of the media stream.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
  </xs:complexType>

  <xs:complexType name="ConnectionInfoType">
    <xs:sequence>
      <xs:element name="CallId" type="xs:string">
        <xs:annotation>
          <xs:documentation>Unique identifier for the SIP call. This field should be used to correlate messages referring to the same call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="CorrelationId" type="xs:string">
        <xs:annotation>
          <xs:documentation>Identifier to correlate two SIP calls where mediation server is involved. Both SIP calls belong to the same conversation.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="CSEQ" type="xs:string">
        <xs:annotation>
          <xs:documentation>
            Call sequence number as part of SIP standard that needs to be used to filter for unrelated error messages.
            This field is not provided for QualityUpdates.
          </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="ConversationId" type="xs:string">
        <xs:annotation>
          <xs:documentation>Identifier to correlate different SIP calls involved in the same conversation. 
          In some cases Skype for Business uses different SIP calls for different modalities. 
          This identifier permits correlating these SIP calls in the same conversation. 
          </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="ConferenceId" type="xs:string">
        <xs:annotation>
          <xs:documentation>Identifier to correlate call legs that belong to the same conference.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="TimeStamp" type="xs:dateTime">
        <xs:annotation>
          <xs:documentation>UTC time when the report is processed. </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Connectivity" type="xs:string">
        <xs:annotation>
          <xs:documentation>(Obsolete) The inclusion of Relay Ip/port indicates that a particular endpoint uses a media relay (edge server) and if not access the remote address directly. It is provided only in QualityUpdate events.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="StartTime" type="xs:dateTime">
        <xs:annotation>
          <xs:documentation>Denotes the time when the conversation started.  It is provided only in QualityUpdate events.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="EndTime" type="xs:dateTime">
        <xs:annotation>
          <xs:documentation>Denotes then time when the conversation ended. It is provided only in QualityUpdate events. </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="ConferenceURI" type="xs:anyURI">
        <xs:annotation>
          <xs:documentation>(Deprecated - use ConferenceId instead) Sip URI used for the conference. This field is obfuscated unless hidepii is set to false in configuration.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="MediaBypass" type="xs:boolean">
        <xs:annotation>
          <xs:documentation>Denotes media bypass. It is provided only in QualityUpdate message when mediabypass was part of the call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="MediationServerLegPosition" type="xs:string">
        <xs:annotation>
          <xs:documentation>Indicates whether the call was incoming to a mediation server or outgoing from the medation server. 
          It is provided only in QualityUpdate events.  </xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="SourcePool" type="xs:string">
        <xs:annotation>
          <xs:documentation>Name of the Skype for Business pool this message originated. If a QualityUpdate message is merged and originated from two pools only one is included here. Currently, the FQDN of one sourcepool is provided, expect a comma delimited list in future releases.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
    <xs:attribute name="Originator" type="xs:string" use="optional">
      <xs:annotation>
        <xs:documentation>
          Indicates source endpoint (Endpoint Id) that provided the quality metrics used for this report.
          It is provided only in QualityUpdate events.
        </xs:documentation>
      </xs:annotation>
    </xs:attribute>
  </xs:complexType>

  <xs:complexType name="ByeType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="2" name="EndPoint" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Endpoint involved in the ended SIP call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
  </xs:complexType>

  <xs:complexType name="ErrorProperties">
    <xs:sequence>
      <xs:element name="ResponseCode" type="xs:int" minOccurs="0">
        <xs:annotation>
          <xs:documentation>SIP Error code.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="ResponsePhrase" type="xs:string" minOccurs="0">
        <xs:annotation>
          <xs:documentation>More info related to the error.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="MSDiagnostics" type="xs:string" minOccurs="0">
        <xs:annotation>
          <xs:documentation>More info related to the error.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="MSDiagnosticsClient" type="xs:string" minOccurs="0">
        <xs:annotation>
          <xs:documentation>Info about the error related to and reported by the client.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="MSDiagnosticsPublic" type="xs:string" minOccurs="0">
        <xs:annotation>
          <xs:documentation>Public info about the error.</xs:documentation>
        </xs:annotation>
      </xs:element>
    </xs:sequence>
  </xs:complexType>

  <xs:complexType name="ErrorType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="2" name="EndPoint" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Endpoint involved in the ended SIP call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="From" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Endpoint involved in the ended SIP call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="To" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Endpoint involved in the ended SIP call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="Properties" type="ErrorProperties">
        <xs:annotation>
          <xs:documentation>Properties of the Error.</xs:documentation>
        </xs:annotation>
      </xs:element>
    </xs:sequence>
    <xs:attribute name="Type" type="xs:string" use="optional">
      <xs:annotation>
        <xs:documentation>Modality or media type for which the quality metrics are reported. 
        The supported options are audio, video and applicationsharing. </xs:documentation>
      </xs:annotation>
    </xs:attribute>
  </xs:complexType>

  <xs:complexType name="EndedProperties">
    <xs:sequence>
      <xs:element name="MSDiagnostics" type="xs:string" minOccurs="0">
        <xs:annotation>
          <xs:documentation>More info related to the error.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="MSDiagnosticsClient" type="xs:string" minOccurs="0">
        <xs:annotation>
          <xs:documentation>Info about the error related to and reported by the client.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element name="MSDiagnosticsPublic" type="xs:string" minOccurs="0">
        <xs:annotation>
          <xs:documentation>Public info about the error.</xs:documentation>
        </xs:annotation>
      </xs:element>
    </xs:sequence>
  </xs:complexType>

  <xs:complexType name="EndedType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="2" name="EndPoint" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Endpoint involved in the ended SIP call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="From" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Endpoint involved in the ended SIP call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="To" type="EndPointType">
        <xs:annotation>
          <xs:documentation>Endpoint involved in the ended SIP call.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
      <xs:element minOccurs="0" name="Properties" type="EndedProperties">
        <xs:annotation>
          <xs:documentation>Properties of the Error.</xs:documentation>
        </xs:annotation>
      </xs:element>
    </xs:sequence>
    <xs:attribute name="Type" type="xs:string" use="required">
      <xs:annotation>
        <xs:documentation>Modality or media type for which the quality metrics are reported. 
        The supported options are audio, video and applicationsharing. </xs:documentation>
      </xs:annotation>
    </xs:attribute>
  </xs:complexType>

  <xs:complexType name="MessageType">
    <xs:sequence>
      <xs:element name="ConnectionInfo" type="ConnectionInfoType">
        <xs:annotation>
          <xs:documentation>Connection-related properties regardless of the media stream and end points.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:choice maxOccurs="unbounded">
  
        <xs:element name="Invite" type="InviteType">
          <xs:annotation>
            <xs:documentation>
              Event that an endpoint attempts to establish a call. DialogListener will include this element in its output if the sendcallinvites
              entry is set to True (activated) in the DialogListener configuration file. In addition, DialogListener will also notifies any SIP Invite messages (re-invites), not just the first one.
              Following this message Earlymedia may be flowing but this element is not intended to report on early media streams.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
  
        <xs:element name="Bye" type="ByeType">
          <xs:annotation>
            <xs:documentation>Event that a Sip call has ended and all media stream terminated.</xs:documentation>
          </xs:annotation>
        </xs:element>
  
        <xs:element name="InCallQuality" type="QualityType">
          <xs:annotation>
            <xs:documentation>
              Indicates that a significant quality related event occured in the client.
              Either the quality dropped into another level or improved. There are 3 levels: Good, Poor, Bad.
              The media stack determines the quality level. Furthermore, this event is also sent when a video stream is deescalated.
              Even in an issue free network at least one IncallQuality message is sent.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
  
        <xs:element name="QualityUpdate" type="QualityType">
          <xs:annotation>
            <xs:documentation>Specifies the event that a call has ended and contains a report of the quality metrics of individual media streams. 
            These quality metrics for a stream may include updates provided by both endpoints which are merged. 
            </xs:documentation>
          </xs:annotation>
        </xs:element>
  
        <xs:element name="Error" type="ErrorType">
          <xs:annotation>
            <xs:documentation>This event is optional. Error event that a SIP dialog has failed. 
              Error events are also sent for SIP calls that are terminated even before a media stream is started or for failed to be updated. 
            </xs:documentation>
          </xs:annotation>
        </xs:element>
  
        <xs:element name="Ended" type="EndedType">
          <xs:annotation>
            <xs:documentation>Event that a Sip call has ended and all media stream terminated.</xs:documentation>
          </xs:annotation>
        </xs:element>
  
        <xs:element name="Start" type="StartOrUpdateType">
          <xs:annotation>
            <xs:documentation>Event that a media stream is started. Every Start element contains a report about 
            a particular media stream. This event is raised when the call is established, 
            i.e., when the call is picked up and the SIP INVITE is answered with a 200 OK response.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
  
        <xs:element name="Update" type="StartOrUpdateType">
          <xs:annotation>
            <xs:documentation>
              Event that a media stream previously started has been updated. This event is raised when an update to core parameters of call have changed and the change is established,
              i.e., when the request is answered with a 200 OK response.
            </xs:documentation>
          </xs:annotation>
        </xs:element>
  
      </xs:choice>
      
      <xs:element minOccurs="0" name="Route" type="RouteType">
        <xs:annotation>
          <xs:documentation>Network path of the media stream only provided in Skype for Business 2013 and when the traceRoute feature is activated in Skype for Business.
          </xs:documentation>
        </xs:annotation>
      </xs:element>
      
      <xs:element minOccurs="0" name="Properties" type="MessageProperties">  <!-- for Error and ended -->
        <xs:annotation>
          <xs:documentation>Details of the Error or reason for ending the streams.</xs:documentation>
        </xs:annotation>
      </xs:element>
  
      <xs:element minOccurs="0" name="RawSDP" type="xs:string">
        <xs:annotation>
          <xs:documentation>Raw Session Description Protocol (SDP) data that is 
          included as the payload of the underlying SIP messages of the Invite, LRSInvite and StartOrUpdate type, if the sendrawsdp 
          entry is set to True in the DialogListener configuration file. 
          </xs:documentation>
        </xs:annotation>
      </xs:element>
      
    </xs:sequence>
    <xs:attribute name="Version" type="xs:string" use="optional">
      <xs:annotation>
        <xs:documentation>Version number of this data structure. It provides a simple distinction 
        between LyncDiagnostics messages formats. LyncDiagnostics message matching to this xsd use Version C.</xs:documentation>
      </xs:annotation>
    </xs:attribute>
  </xs:complexType>

  <xs:complexType name="ResponseCodeType">
    <xs:simpleContent>
      <xs:extension base="xs:string">
        <xs:attribute name="Code" type="xs:unsignedShort" use="required">
          <xs:annotation>
            <xs:documentation>SIP error code for this error.</xs:documentation>
          </xs:annotation>
        </xs:attribute>
      </xs:extension>
    </xs:simpleContent>
  </xs:complexType>

  <xs:complexType name="MessageProperties">
    <xs:sequence>
      <xs:element minOccurs="0" name="ResponseCode" type="ResponseCodeType">
        <xs:annotation>
          <xs:documentation>Message describing the error.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="MSDiagnostics" type="xs:string">
        <xs:annotation>
          <xs:documentation>Skype for Business-specific diagnostics message.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="MSDiagnosticsClient">
        <xs:annotation>
          <xs:documentation>Skype for Business-specific diagnostics message from the client.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="MSDiagnosticsPublic">
        <xs:annotation>
          <xs:documentation>Skype for Business-specific public diagnostics message.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <!--<xs:any namespace="##other" processContents="lax"/>-->
    </xs:sequence>
  </xs:complexType>

  <xs:element name="LyncDiagnostics" type="MessageType">
    <xs:annotation>
      <xs:documentation>The root element for output from the Skype for Business SDN Manager.</xs:documentation>
    </xs:annotation>
  </xs:element>
</xs:schema>
```



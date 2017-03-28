---
title: Schema Extensions
ms.assetid: d4fda314-3096-4a7a-a5aa-9ee10271efe5
---


# Schema Extensions
Learn about schema D for Skype for Business SDN Interface. 
 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015

## Schema Extensions

Learn about schema D for Skype for Business SDN Interface. 
  
    
    
New fields have been added to Schema D (see file in package). For backward compatibility, these fields can be suppressed on a per-subscriber basis by setting the Subscriber parameter 'schemaextension' to 'false'. The default value is 'true'. 
  
    
    
The new fields added (primarily to Audio InCallQuality messages) are: 
  
    
    



```xsd

<xs:element minOccurs="0" name="SSID" type="xs:string">
        <xs:annotation>
          <xs:documentation>Name of WiFi Service Set Identifier.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WiFiChannel" type="xs:int">
        <xs:annotation>
          <xs:documentation>WiFi Channel used during the session.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiHandovers" type="xs:int">
        <xs:annotation>
          <xs:documentation>Number of WiFi hand overs to other access points during the session.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiSignalStrength" type="DoubleBetween0And100">
        <xs:annotation>
          <xs:documentation>The WiFi signal strength (Percent).</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiRadioType" type="xs:byte">
        <xs:annotation>
          <xs:documentation>Average render speech level after dynamic range compression or analog gain control is applied.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiRSSI" type="DoubleBetween0And100">
        <xs:annotation>
          <xs:documentation>Average WiFi Received Signal Strength Indication value. (Percent)</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiChannelReassociations" type="xs:int">
        <xs:annotation>
          <xs:documentation>Number of WiFi channel reassociated during session</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiRadioFrequency" type="xs:int">
        <xs:annotation>
          <xs:documentation>WiFi frequency used during session</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiSupportFlags" type="xs:byte">
        <xs:annotation>
          <xs:documentation>A flag that indicates whether the IPv4 or IPv6 protocols are supported. SupportFlag_IPv4=1, IPv6=2.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiStatusFlags" type="xs:byte">
        <xs:annotation>
          <xs:documentation>A flag that indicates the current connection status. StatusFlag_VPN=1.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="WifiBatteryCharge" type="DoubleBetween0And100">
        <xs:annotation>
          <xs:documentation>The estimated remaining battery charge in percentage points [0-99], with 0 indicating that the device was plugged in.</xs:documentation>
        </xs:annotation>
      </xs:element>
      <xs:element minOccurs="0" name="DNSSuffix" type="xs:string">
        <xs:annotation>
          <xs:documentation>The DNS suffix associated with the network adapter</xs:documentation>
        </xs:annotation>

```



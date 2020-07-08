# mediaPolicies

 _**Applies to:** Skype for Business 2015_


Represents a resource that allows clients to fetch all media-related settings that
cannot be modeled as capability links or properties of individual resources. Most
of them are directly consumed by media stack manager on the client side.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

### Properties



|**Name**|**Description**|
|:-----|:-----|
|applicationSharingBitRate|Gets the application sharing bit rate.|
|applicationSharingEncryption|Gets whether encryption for application sharing is enabled.|
|audioBitRate|Gets the audio bit rate.|
|audioBypass|Gets whether audio bypass is enabled.|
|audioBypassId|Gets the audio bypass id.|
|audioVideoEncryption|Gets whether audio/video encryption is enabled between two applications.|
|bandwidthControl|Gets whether bandwidth control is enabled.|
|externalAudioBypassMode|Gets audio bypass mode when the client is communicating with external side of the server.|
|fipsCompliantMedia|Gets whether to use FIPS approved algorithm for media stack.|
|highPerformanceApplicationSharingInOnlineMeeting|Whether high performance application sharing is enabled in online meetings.|
|internalAudioBypassMode|Gets audio bypass mode when the client is communicating with internal side of the server.|
|maximumApplicationSharingPort|Gets the maximum port for application sharing.|
|maximumAudioPort|Gets the maximum port for audio.|
|maximumVideoPort|Gets the maximum port for video.|
|maximumVideoRateAllowed|Gets the maximum video rate allowed.|
|minimumApplicationSharingPort|Gets the minimum port for application sharing.|
|minimumAudioPort|Gets the minimum port for audio.|
|minimumVideoPort|Gets the minimum port for video.|
|multiViewJoin|Gets whether the clinet is enabled for multi-view join for video.|
|poorDeviceWarnings|Gets whether the client should disable poor device warnings.|
|poorNetworkWarnings|Gets whether the client should disable poor network warnings.|
|portRange|Gets whether the port range is enabled.|
|qualityOfService|Gets whether media quality of service should enabled for the media manager on the client.|
|totalReceivedVideoBitRateKB|Gets the maximunm bit rate for video.|
|video|Gets whether video is enabled for the client.|
|videoBitRate|Gets the video bit rate.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### GET




Operation description coming soon...

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/mediaPolicies HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 937
{
  "rel" : "mediaPolicies",
  "applicationSharingBitRate" : "samplevalue",
  "applicationSharingEncryption" : "Supported",
  "audioBitRate" : "samplevalue",
  "audioBypass" : "Disabled",
  "audioBypassId" : "samplevalue",
  "audioVideoEncryption" : "Enforced",
  "bandwidthControl" : "OnReportMode",
  "externalAudioBypassMode" : "samplevalue",
  "fipsCompliantMedia" : "Required",
  "highPerformanceApplicationSharingInOnlineMeeting" : "Disabled",
  "internalAudioBypassMode" : "samplevalue",
  "maximumApplicationSharingPort" : 41,
  "maximumAudioPort" : 76,
  "maximumVideoPort" : 13,
  "maximumVideoRateAllowed" : "samplevalue",
  "minimumApplicationSharingPort" : 2,
  "minimumAudioPort" : 16,
  "minimumVideoPort" : 91,
  "multiViewJoin" : "Disabled",
  "poorDeviceWarnings" : "Disabled",
  "poorNetworkWarnings" : "Disabled",
  "portRange" : "Disabled",
  "qualityOfService" : "Disabled",
  "totalReceivedVideoBitRateKB" : "samplevalue",
  "video" : "Disabled",
  "videoBitRate" : "samplevalue",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/mediaPolicies"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/mediaPolicies HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 1671
<?xml version="1.0" encoding="utf-8"?>
<resource rel="mediaPolicies" href="/ucwa/v1/applications/192/mediaPolicies" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">mediaPolicies</property>
  <property name="applicationSharingBitRate">samplevalue</property>
  <property name="applicationSharingEncryption">Supported</property>
  <property name="audioBitRate">samplevalue</property>
  <property name="audioBypass">Disabled</property>
  <property name="audioBypassId">samplevalue</property>
  <property name="audioVideoEncryption">Enforced</property>
  <property name="bandwidthControl">On</property>
  <property name="externalAudioBypassMode">samplevalue</property>
  <property name="fipsCompliantMedia">Required</property>
  <property name="highPerformanceApplicationSharingInOnlineMeeting">Disabled</property>
  <property name="internalAudioBypassMode">samplevalue</property>
  <property name="maximumApplicationSharingPort">74</property>
  <property name="maximumAudioPort">78</property>
  <property name="maximumVideoPort">97</property>
  <property name="maximumVideoRateAllowed">samplevalue</property>
  <property name="minimumApplicationSharingPort">56</property>
  <property name="minimumAudioPort">63</property>
  <property name="minimumVideoPort">69</property>
  <property name="multiViewJoin">Disabled</property>
  <property name="poorDeviceWarnings">Disabled</property>
  <property name="poorNetworkWarnings">Disabled</property>
  <property name="portRange">Disabled</property>
  <property name="qualityOfService">Disabled</property>
  <property name="totalReceivedVideoBitRateKB">samplevalue</property>
  <property name="video">Disabled</property>
  <property name="videoBitRate">samplevalue</property>
</resource>
```



# policies

 _**Applies to:** Skype for Business 2015_


Represents the admin policies that can apply to a user's application.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

Policies include information such as whether emoticons are allowed in messages or photos are enabled for [contact](contact_ref.md)s in the user's organization. Note that policies are set by the admin; they cannot be changed by the user.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|allowDeviceContactsSync|This mobile policy parameter allows mobile apps to sync device contacts.|
|audioOnlyOnWifi|The audioOnlyOnWifi policy.|
|callLogArchiving|Whether the admin has enabled client-side call logging by default.If disabled, the user should not be given the choice to enable call logging.|
|customerExperienceImprovementProgram|Whether Skype for Business mobile users can participate and publish data to Microsoft's Customer Experience Improvement Program.If customerExperienceImprovementProgram is enabled, the user can participate and publish data to Microsoft's Customer Experience Improvement Program. Note that this should not require a change in application behavior.|
|emergencyDialMask|An alternate number for emergency services.If emergencyDialMask is set to 555 and the emergencyDialString is set to 911, entering 555 will cause 911 to be dialed. Note that entering 911 will also cause 911 to be dialed in this scenario.|
|emergencyDialString|The emergency services number that will be dialed if the number in emergencyDialMask is entered.If emergencyDialMask is set to 555 and the emergencyDialString is set to 911, entering 555 will cause 911 to be dialed. Note that entering 911 will also cause 911 to be dialed in this scenario.|
|emergencyNumbers|A string of semicolon (;) separated emergency dial string and dialmask(s) combinations.If client supports this field then this takes preecedence over theemergencyDialString and value specified in EmergencyDialString isignored|
|emergencyServiceDisclaimer|Text entered by the administrator for the client to display anemergency services disclaimer if it could not fetch a location fromthe Location Information Service.This could happen in two cases:a. LisInternal Url is not availableb. Location could not be retrieved from LISNote: This will be honored only if the Location Required is set todisclaimer|
|emoticons|Whether the admin has enabled emoticons for the messaging modality.If disabled, emoticons will be turned into their text equivalents before delivery.|
|encryptAppData|This mobile policy parameter allows mobile apps to encrypt data.|
|clientExchangeConnectivity|This mobile policy parameter allows the mobile user to connect to Exchange from their mobile device.When ExchangeConnectivity is disabled, mobile users will not have the option to connect to Exchange from their client on the mobile device.The default value is True, meaning that mobile users cannot connect to Exchange from their client on the mobile device.Disabled|
|exchangeUnifiedMessaging|Whether the user is enabled for Microsoft Exchange Unified Messaging.If exchangeUnifiedMessaging is enabled, the user's contacts and voicemail are stored in Exchange rather than in Skype for Business. Note that this should not require a change in application behavior.|
|helpEnvironment|The helpenvironment parameter is an opaque string that the client may use to deterermine which help to displaySet to Office365g for Gallatin. Not set otherwise.Office365g|
|htmlMessaging|Whether the admin has enabled HTML messages for the messaging modality.If enabled, the application can choose to pass HTML to [sendMessage](sendMessage_ref.md).|
|locationRefreshInterval|Specifies the time interval in hours on expiry of which client shouldrefresh the location information from LIS|
|locationRequired|Location required, for possible values refer the enum type|
|logging|Whether the admin has enabled client-side logging by default.If enabled, the user should not be given the choice to disable logging.If disabled, the user should be given the choice to enable logging.|
|loggingLevel|The level of client-side logging that the admin expects.|
|messageArchiving|Whether the admin has enabled the archival of client-side message transcripts by default.If disabled, the user should not be given the choice to enable message transcript archival.|
|messagingUrls|Whether the admin has enabled clickable URLs for the messaging modality.|
|multiViewJoin|The multiViewJoin policy.|
|onlineFeedbackUrl|The onlineFeedbackURL policy.|
|photos|Whether photos are enabled for all [contact](contact_ref.md)s in this organization.|
|saveCallLogs|This mobile policy parameter allows saving the call logs on mobile device.When SavingCallLogs is disabled, call logs will not be saved locally on the mobile device.The default value is True, meaning that call logs can be saved locally on mobile device.Disabled|
|saveCredentials|This mobile policy parameter allows the mobile user to save their credentials locally on the mobile device.If savingCredentials is disabled, the user will not have the option to save his credentials locally on the mobile device.The default value is Enabled, meaning that user is allowed to save his credentialsEnabled|
|saveMessagingHistory|This mobile policy parameter allows saving the history of IM exchanged from the mobile device.If savingInstantMessagingHistory is disabled, IM history will not be saved locally on the mobile device.The default value is True, meaning that IM history can be saved locally on mobile device.Disabled|
|sendFeedbackUrl|The sendFeedbackURL policy.|
|sharingOnlyOnWifi|The sharingOnlyOnWifi policy.|
|softwareQualityMetrics|Whether the admin has enabled software quality metrics.Software quality metrics are anonymous metrics used by Microsoft to improve the product.|
|telephonyMode|Indicates which audio capabilities are possible for this user; for example, audioVideo or phoneAudio.This is an advanced API that indicates more granular capabities including whether the user can make a PSTN call.|
|useLocationForE911Only|It must be either true or false. If true, location is sent only inE911 call o.w. we may choose to publish location with presenceinformation|
|videoOnlyOnWifi|The videoOnlyOnWifi policy.|
|voicemailUri|The URI to call to check the user's voicemail.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|
|Meetings.ReadWrite|Create Skype Meetings|Allows the app to create Skype meetings on-behalf of the signed-in user|

## Events
<a name="sectionSection2"></a>

### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|policies|Medium|policies|Indicates that the policies resource has been updated.</p><p></p>|
Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
{
  "_links" : {
    "self" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=1"
    },
    "next" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=2"
    }
  },
  "sender" : [
    {
      "rel" : "policies",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/policies",
      "events" : [
        {
          "link" : {
            "rel" : "policies",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/policies"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}


## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the admin policies that can apply to a user's application.

#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|ProvisioningDataUnavailable|Failed to retrieve policies.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/policies HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 1121
{
  "rel" : "policies",
  "allowDeviceContactsSync" : "Disabled",
  "audioOnlyOnWifi" : "Disabled",
  "callLogArchiving" : "Disabled",
  "customerExperienceImprovementProgram" : "Disabled",
  "emergencyDialMask" : "555",
  "emergencyDialString" : "911",
  "emergencyNumbers" : [
    "samplevalue"
  ],
  "emergencyServiceDisclaimer" : "samplevalue",
  "emoticons" : "Disabled",
  "encryptAppData" : "Disabled",
  "clientExchangeConnectivity" : "Disabled",
  "exchangeUnifiedMessaging" : "Disabled",
  "helpEnvironment" : "samplevalue",
  "htmlMessaging" : "Disabled",
  "locationRefreshInterval" : 53,
  "locationRequired" : "Yes",
  "logging" : "Enabled",
  "loggingLevel" : "Full",
  "messageArchiving" : "Enabled",
  "messagingUrls" : "Disabled",
  "multiViewJoin" : "Disabled",
  "onlineFeedbackUrl" : "samplevalue",
  "photos" : "Enabled",
  "saveCallLogs" : "Disabled",
  "saveCredentials" : "Disabled",
  "saveMessagingHistory" : "Disabled",
  "sendFeedbackUrl" : "samplevalue",
  "sharingOnlyOnWifi" : "Disabled",
  "softwareQualityMetrics" : "Disabled",
  "telephonyMode" : "AudioVideo",
  "useLocationForE911Only" : "Disabled",
  "videoOnlyOnWifi" : "Disabled",
  "voicemailUri" : "sip:jdoe@contoso.com;opaque=app:voicemail",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/policies"
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/policies HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 2030
<?xml version="1.0" encoding="utf-8"?>
<resource rel="policies" href="/ucwa/v1/applications/192/policies" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">policies</property>
  <property name="allowDeviceContactsSync">Disabled</property>
  <property name="audioOnlyOnWifi">Disabled</property>
  <property name="callLogArchiving">Disabled</property>
  <property name="customerExperienceImprovementProgram">Disabled</property>
  <property name="emergencyDialMask">555</property>
  <property name="emergencyDialString">911</property>
  <propertyList name="emergencyNumbers">
    <item>samplevalue</item>
  </propertyList>
  <property name="emergencyServiceDisclaimer">samplevalue</property>
  <property name="emoticons">Disabled</property>
  <property name="encryptAppData">Disabled</property>
  <property name="clientExchangeConnectivity">Disabled</property>
  <property name="exchangeUnifiedMessaging">Disabled</property>
  <property name="helpEnvironment">samplevalue</property>
  <property name="htmlMessaging">Disabled</property>
  <property name="locationRefreshInterval">0</property>
  <property name="locationRequired">Yes</property>
  <property name="logging">Enabled</property>
  <property name="loggingLevel"> Light</property>
  <property name="messageArchiving">Enabled</property>
  <property name="messagingUrls">Disabled</property>
  <property name="multiViewJoin">Disabled</property>
  <property name="onlineFeedbackUrl">samplevalue</property>
  <property name="photos">Enabled</property>
  <property name="saveCallLogs">Disabled</property>
  <property name="saveCredentials">Disabled</property>
  <property name="saveMessagingHistory">Disabled</property>
  <property name="sendFeedbackUrl">samplevalue</property>
  <property name="sharingOnlyOnWifi">Disabled</property>
  <property name="softwareQualityMetrics">Disabled</property>
  <property name="telephonyMode">AudioVideo</property>
  <property name="useLocationForE911Only">Disabled</property>
  <property name="videoOnlyOnWifi">Disabled</property>
  <property name="voicemailUri">sip:jdoe@contoso.com;opaque=app:voicemail</property>
</resource>
```



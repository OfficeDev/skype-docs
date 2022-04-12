---
title: MakeMeAvailable2 for Skype for Business 2015
description: "This resource allows users to share their availability and lets them receive incoming invitations for the modalities of their choice."
---
# makeMeAvailable2

 _**Applies to:** Skype for Business 2015_


This resource lets users share their availability and allows them to receive incoming invitations for the modalities of their choice.
Please note that a user can initiate communications, such as joining an [onlineMeeting](onlineMeeting_ref.md) or starting a phone call, without invoking this capability.
This version will silently succeed if access is denied to one operation due to insufficient permissions. In the command channel we return what 
operations succeeded
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.

|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Makes the user available for incoming communications.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|audioPreference|Gets or sets the preferred mode for incoming audio, if any, supplied by the user.Voip is default Nullable (AudioPreference)PhoneAudio, or VoipAudio|No|
|inactiveTimeout|The length of time, in minutes, before the user's activity switches to inactive.The default value is 5 minutes.Nullable TimeSpan|No|
|phoneNumber|The phone number of the device the user wishes to be reached on.The phone number will be normalized by the server, but it will not check that the number actually rings.String|No|
|signInAs|The preferred availability ([PreferredAvailability](PreferredAvailability_ref.md)) of the user upon making herself available.Nullable (PreferredAvailability)Online, Busy, DoNotDisturb, BeRightBack, Away, or Offwork|No|
|supportedMessageFormats|The message formats ([MessageFormat](MessageFormat_ref.md)) that are supported by this application.Array of (MessageFormat)Plain, or Html|No|
|supportedModalities|The list of incoming modalities ([ModalityType](ModalityType_ref.md)) of interest to the user.Array of (ModalityType)Audio, Video, PhoneAudio, ApplicationSharing, Messaging, DataCollaboration, or PanoramicVideo|No|
|voipFallbackToPhoneAudioTimeOut|Sets the timeout used before falling back to PhoneAudio if client does notACK the Audio invitation.Nullable TimeSpan|No|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
||Provides information about which operations were accepted and rejected|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Conflict|409|AlreadyExists|Multiple invocations of this operation is not valid.|
|Forbidden|403|None|The application is attempting to use modalities other than Messaging and Phone Audio.|
|Forbidden|403|AnonymousNotAllowed|The application is attempting make an anonymous user available.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/makeMeAvailable HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Accept: application/json
Content-Length: 171
{
  &quot;audioPreference&quot; : &quot;PhoneAudio&quot;,
  &quot;phoneNumber&quot; : &quot;4255552222&quot;,
  &quot;signInAs&quot; : &quot;Online&quot;,
  &quot;supportedMessageFormats&quot; : [
    &quot;Plain&quot;,
    &quot;Html&quot;
  ],
  &quot;supportedModalities&quot; : [
    &quot;PhoneAudio&quot;,
    &quot;Messaging&quot;
  ]
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/json
Content-Length: 21
{
  "operations" : [
    null
  ]
}
```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/makeMeAvailable HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Accept: application/xml
Content-Length: 450
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;http://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;audioPreference&quot;&gt;PhoneAudio&lt;/property&gt;
  &lt;property name=&quot;phoneNumber&quot;&gt;4255552222&lt;/property&gt;
  &lt;property name=&quot;signInAs&quot;&gt;Online&lt;/property&gt;
  &lt;propertyList name=&quot;supportedMessageFormats&quot;&gt;
    &lt;item&gt;Plain&lt;/item&gt;
    &lt;item&gt;Html&lt;/item&gt;
  &lt;/propertyList&gt;
  &lt;propertyList name=&quot;supportedModalities&quot;&gt;
    &lt;item&gt;PhoneAudio&lt;/item&gt;
    &lt;item&gt;Messaging&lt;/item&gt;
  &lt;/propertyList&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/xml
Content-Length: 228
<?xml version="1.0"?>
<operationsQueued xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <operations>
    <operation xsi:nil="true" />
  </operations>
</operationsQueued>
```



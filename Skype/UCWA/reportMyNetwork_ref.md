# reportMyNetwork

 _**Applies to:** Skype for Business 2015_


Represents the reportMyNetwork resource.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

Clients need to report their network whenever they encounter anychange in the network identifiers - listed below.

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|
|Meetings.ReadWrite|Create Skype Meetings|Allows the app to create Skype meetings on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Report network changes.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|chassisID|Chassis IDString|No|
|clientNetworkType|Clients network location - could be Internal or external(ClientNetworkType)External, or Internal|Yes|
|ip|Internet Protocol Address to LocationsString|No|
|mac|Media Access Control AddressString|No|
|portID|Port IDString|No|
|rssi|Received Signal Strength IndicationString|No|
|subnetID|Subnet IDString|No|
|wapBSSID|Wireless Access Point Basic Service Set IdentifierString|No|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[reportMyNetwork](ReportMyNetwork_ref.md)|Represents the reportMyNetwork resource.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|ProvisioningDataUnavailable|Failed to retrieve location policy data.|
|Forbidden|403|AnonymousNotAllowed|Anonymous users are not permitted to access location policy data.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/reportMyNetwork HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 191
{
  &quot;chassisID&quot; : &quot;samplevalue&quot;,
  &quot;clientNetworkType&quot; : &quot;External&quot;,
  &quot;ip&quot; : &quot;samplevalue&quot;,
  &quot;mac&quot; : &quot;samplevalue&quot;,
  &quot;portID&quot; : &quot;samplevalue&quot;,
  &quot;rssi&quot; : &quot;samplevalue&quot;,
  &quot;subnetID&quot; : &quot;samplevalue&quot;,
  &quot;wapBSSID&quot; : &quot;samplevalue&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/reportMyNetwork HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Content-Length: 481
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;chassisID&quot;&gt;samplevalue&lt;/property&gt;
  &lt;property name=&quot;clientNetworkType&quot;&gt;External&lt;/property&gt;
  &lt;property name=&quot;ip&quot;&gt;samplevalue&lt;/property&gt;
  &lt;property name=&quot;mac&quot;&gt;samplevalue&lt;/property&gt;
  &lt;property name=&quot;portID&quot;&gt;samplevalue&lt;/property&gt;
  &lt;property name=&quot;rssi&quot;&gt;samplevalue&lt;/property&gt;
  &lt;property name=&quot;subnetID&quot;&gt;samplevalue&lt;/property&gt;
  &lt;property name=&quot;wapBSSID&quot;&gt;samplevalue&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created

```



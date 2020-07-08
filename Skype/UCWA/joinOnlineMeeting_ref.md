# joinOnlineMeeting

 _**Applies to:** Skype for Business 2015_


Joins an online meeting.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

joinOnlineMeeting enables an application to join an already scheduled online meeting using theassigned meeting URI. Use [myOnlineMeetings](myOnlineMeetings_ref.md) to schedule, retrieve, or update an online meeting. Note that if no onlineMeetingUri is supplied, the user will join a newly created [onlineMeeting](onlineMeeting_ref.md).

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Starts an [onlineMeetingInvitation](onlineMeetingInvitation_ref.md) in the event channel.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|onlineMeetingUri|The URI of the meeting to join.|No|


#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|onlineMeetingUri|The URI of the online meeting the user wishes to join.String|No|
|operationId|The ID that the application supplies to correlate its request with the corresponding operation started in the event channel.The maximum length is 50 characters. String|No|
|importance|The conversation's importance ([Importance](Importance_ref.md)): Normal, Urgent, or Emergency.An application can use this as a hint to inform the user.(Importance)Normal, Urgent, or Emergency|No|
|subject|The conversation's subject.The property has a maximum length of 250 characters.The maximum length is 250 characters. String|No|
|threadId|The conversation's thread ID.An application can use this ID to continue an existing conversation.String|No|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[onlineMeetingInvitation](OnlineMeetingInvitation_ref.md)|Represents an invitation to a new or existing [onlineMeeting](onlineMeeting_ref.md).|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|NotFound|404|DestinationNotFound|The meeting could not be found.|
|Forbidden|403|None|The target conference URI does not match the conference URI provisioned in anonymous web ticket.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/joinOnlineMeeting HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 191
{
  &quot;onlineMeetingUri&quot; : &quot;sip : john@contoso.com&quot;,
  &quot;operationId&quot; : &quot;74cb7404e0a247d5a2d4eb0376a47dbf&quot;,
  &quot;importance&quot; : &quot;Normal&quot;,
  &quot;subject&quot; : &quot;SkypeforBusiness&quot;,
  &quot;threadId&quot; : &quot;292e0aaef36c426a97757f43dda19d06&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Location: /ucwa/v1/applications/192/communication/invitations/711

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/joinOnlineMeeting HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Content-Length: 412
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;onlineMeetingUri&quot;&gt;sip:john@contoso.com&lt;/property&gt;
  &lt;property name=&quot;operationId&quot;&gt;74cb7404e0a247d5a2d4eb0376a47dbf&lt;/property&gt;
  &lt;property name=&quot;importance&quot;&gt;Urgent&lt;/property&gt;
  &lt;property name=&quot;subject&quot;&gt;Skype for Business&lt;/property&gt;
  &lt;property name=&quot;threadId&quot;&gt;292e0aaef36c426a97757f43dda19d06&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Location: /ucwa/v1/applications/192/communication/invitations/711

```



# startMessaging2

 _**Applies to:** Skype for Business 2015_


Starts a [messagingInvitation](messagingInvitation_ref.md) that adds the [messaging](messaging_ref.md) modality to a new [conversation](conversation_ref.md). 
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

 The startMessaging resource can be used to create a new peer-to-peer [messaging](messaging_ref.md) [conversation](conversation_ref.md) with a [contact](contact_ref.md).

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Starts a [messagingInvitation](messagingInvitation_ref.md) that adds the instant messaging modality to a new [conversation](conversation_ref.md).

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|to|The target of this invitation.|No|


#### Request body

|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|customContent|Custom contentExternalResource|No|
|importance|The importance of the Conversation.(Importance)Normal, Urgent, or Emergency|No|
|message|The first message to send along with the instant messaging invitation.ExternalResource|No|
|operationId|The operation ID that an application can use to correlate this operation with the corresponding invitation started in the event channel.The maximum length is 50 characters. String|Yes|
|subject|The subject of the Conversation.The maximum length is 250 characters. String|No|
|threadId|The thread ID of the conversation.The maximum length is 250 characters. String|No|
|to|The destination of this invitation. The input type is a SIP URI.String|No|


#### Response body

None


#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|The target URI does not match the target URI provisioned in anonymous token.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/startMessaging HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 331
{
  &quot;importance&quot; : &quot;Normal&quot;,
  &quot;operationId&quot; : &quot;74cb7404e0a247d5a2d4eb0376a47dbf&quot;,
  &quot;subject&quot; : &quot;SkypeforBusiness&quot;,
  &quot;threadId&quot; : &quot;292e0aaef36c426a97757f43dda19d06&quot;,
  &quot;to&quot; : &quot;sip : john@contoso.com&quot;,
  &quot;_links&quot; : {
    &quot;customContent&quot; : {
      &quot;href&quot; : &quot;data : application/sdp;base64,
      base64-encoded-sdp&quot;
    },
    &quot;message&quot; : {
      &quot;href&quot; : &quot;data : text/plain;base64,
      somebase64encodedmessage&quot;
    }
  }
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Location: /ucwa/v1/applications/192/communication/invitations/602

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/startMessaging HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Content-Length: 398
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;importance&quot;&gt;Urgent&lt;/property&gt;
  &lt;property name=&quot;operationId&quot;&gt;74cb7404e0a247d5a2d4eb0376a47dbf&lt;/property&gt;
  &lt;property name=&quot;subject&quot;&gt;Skype for Business&lt;/property&gt;
  &lt;property name=&quot;threadId&quot;&gt;292e0aaef36c426a97757f43dda19d06&lt;/property&gt;
  &lt;property name=&quot;to&quot;&gt;sip:john@contoso.com&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Location: /ucwa/v1/applications/192/communication/invitations/602

```



# addParticipant

 _**Applies to:** Skype for Business 2015_


Invites a [contact](contact_ref.md) to participate in a multiparty [conversation](conversation_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

When a conversation is in peer-to-peer mode, addParticipant will first escalate the conversationfrom two-party to conferencing, and will then invite the contact to the conversation.When a conversation is multiparty, addParticipant merely invites the contact to the conversation.

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




Starts a [participantInvitation](participantInvitation_ref.md) in the event channel.

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|operationId|An application-supplied identifier to track the participantInvitation created by the server.The maximum length is 50 characters. String|No|
|to|The URI of the contact who is invited to this conversation.String|Yes|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[participantInvitation](ParticipantInvitation_ref.md)|Represents an invitation to an existing [conversation](conversation_ref.md) for an additional participant.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/addParticipant HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 78
{
  &quot;operationId&quot; : &quot;74cb7404e0a247d5a2d4eb0376a47dbf&quot;,
  &quot;to&quot; : &quot;sip : john@contoso.com&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Location: /ucwa/v1/applications/192/communication/invitations/303

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/conversations/137/addParticipant HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Content-Length: 230
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;operationId&quot;&gt;74cb7404e0a247d5a2d4eb0376a47dbf&lt;/property&gt;
  &lt;property name=&quot;to&quot;&gt;sip:john@contoso.com&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Location: /ucwa/v1/applications/192/communication/invitations/303

```



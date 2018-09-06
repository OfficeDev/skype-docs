# startMessaging

_**Applies to:** Skype for Business 2015_

Starts a [messagingInvitation](messagingInvitation_ref.md) that adds the [messaging](messaging_ref.md) modality to a new [conversation](conversation_ref.md).

## Web Link

<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).

| **Name** | **Description**                                                                   |
| :------- | :-------------------------------------------------------------------------------- |
| rel      | The resource that this link points to. In JSON, this is the outer container.      |
| href     | The location of this resource on the server, and the target of an HTTP operation. |

## Resource description

<a name = "sectionSection1"> </a>

The startMessaging resource can be used to create a new peer-to-peer [messaging](messaging_ref.md) [conversation](conversation_ref.md) with a [contact](contact_ref.md).

### Properties

None

### Links

None

### Azure Active Directory scopes for online applications

The user must have at least one of these scopes for operations on the resource to be allowed.

| **Scope**              | **Permission**                           | **Description**                                                                                                                                 |
| :--------------------- | :--------------------------------------- | :---------------------------------------------------------------------------------------------------------------------------------------------- |
| Conversations.Initiate | Initiate conversations and join meetings | Allows the app to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user |
| Conversations.Receive  | Receive conversation invites             | Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user                       |

## Operations

<a name="sectionSection2"></a>

### POST

Starts a [messagingInvitation](messagingInvitation_ref.md) that adds the instant messaging modality to a new [conversation](conversation_ref.md).

#### Query parameters

| **Name** | **Description**                | **Required?** |
| :------- | :----------------------------- | :------------ |
| to       | The target of this invitation. | No            |

#### Request body

| **Name**    | **Description**                                                                                                                                                                                    | **Required?** |
| :---------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :------------ |
| operationId | The ID that the application supplies to correlate its request with the corresponding operation started in the event channel. The maximum length is 50 characters. String                           | Yes           |
| to          | The [contact](contact_ref.md) that this invitation is to be sent to.String                                                                                                                         | Yes           |
| importance  | The conversation's importance ([Importance](Importance_ref.md)): Normal, Urgent, or Emergency. An application can use this as a hint to inform the user. (Importance) Normal, Urgent, or Emergency | No            |
| subject     | The conversation's subject.The property has a maximum length of 250 characters.The maximum length is 250 characters. String                                                                        | No            |
| threadId    | The conversation's thread ID.An application can use this ID to continue an existing conversation. String                                                                                           | No            |
| message     | The message text to send along with the [messagingInvitation](messagingInvitation_ref.md). ExternalResource                                                                                        | No            |

#### Response body

| **Item**                                          | **Description**                                                                                                   |
| :------------------------------------------------ | :---------------------------------------------------------------------------------------------------------------- |
| [messagingInvitation](MessagingInvitation_ref.md) | Represents an invitation to a [conversation](conversation_ref.md) for the [messaging](messaging_ref.md) modality. |

#### Synchronous errors

The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

| **Error**      | **Code** | **Subcode**          | **Description**                                                              |
| :------------- | :------- | :------------------- | :--------------------------------------------------------------------------- |
| Forbidden      | 403      | None                 | The target URI does not match the target URI provisioned in anonymous token. |
| ServiceFailure | 500      | CallbackChannelError | The remote event channel is not reachable                                    |
| Conflict       | 409      | AlreadyExists        | The already exists error.                                                    |
| Conflict       | 409      | TooManyGroups        | The too many groups error.                                                   |
| Conflict       | 409      | None                 | Un-supported Service/Resource/API error.                                     |
| Gone           | 410      | CannotRedirect       | Cannot redirect since there is no back up pool configured.                   |

#### Examples

Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request

```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/communication/startMessaging HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 257
{
  "operationId" : "74cb7404e0a247d5a2d4eb0376a47dbf",
  "to" : "sip : john@contoso.com",
  "importance" : "Urgent",
  "subject" : "SkypeforBusiness",
  "threadId" : "292e0aaef36c426a97757f43dda19d06",
  "_links" : {
    "message" : {
      "href" : "data:text/plain;base64,c29tZWJhc2U2NGVuY29kZWRtZXNzYWdl"
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
<?xml version="1.0" encoding="utf-8"?>
<input xmlns="http://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="operationId">74cb7404e0a247d5a2d4eb0376a47dbf</property>
  <property name="to">sip:john@contoso.com</property>
  <property name="importance">Urgent</property>
  <property name="subject">Skype for Business</property>
  <property name="threadId">292e0aaef36c426a97757f43dda19d06</property>
</input>
```

#### XML Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.

```
HTTP/1.1 201 Created
Location: /ucwa/v1/applications/192/communication/invitations/602
```

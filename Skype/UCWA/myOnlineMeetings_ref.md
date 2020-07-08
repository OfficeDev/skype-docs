# myOnlineMeetings

**Applies to**: Skype for Business 2015

Represents the set of [myOnlineMeeting](myOnlineMeeting_ref.md)s currently on the user's calendar.
            

## Web link

For more information about web links, see [Web links](WebLinks.md).

|Name|Description|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description

This resource can be used to create new [myOnlineMeeting](myOnlineMeeting_ref.md)s as well as to modify and delete existing ones.

### Properties

None

### Links

This resource can have the following relationships.

|Link|Description|
|:-----|:-----|
|self|The link to the current resource.|
|myAssignedOnlineMeeting|Represents a user's [onlineMeeting](onlineMeeting_ref.md) that is commonly used for scheduled meetings with other [contact](contact_ref.md)s.|
|myOnlineMeeting|Represents a scheduled meeting on the user's calendar.|

### Azure Active Directory scopes for online applications

The user must have at least one of these scopes for operations on the resource to be allowed.

|Scope|Permission|Description|
|:-----|:-----|:-----|
|Meetings.ReadWrite|Create Skype Meetings|Allows the app to create Skype meetings on-behalf of the signed-in user|

The following sections describe the **Operations**.

## GET

Returns a representation of the set of [myOnlineMeeting](myOnlineMeeting_ref.md)s currently on the user's calendar.

### Request body

None

### Response body

The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

### Synchronous errors

The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|Error|Code|Subcode|Description|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

### Examples

#### JSON Request

```json
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```

#### JSON Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.

```json
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 3045
{
  "rel" : "myOnlineMeetings",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings"
    }
  },
  "_embedded" : {
    "myAssignedOnlineMeeting" : [
      {
        "rel" : "myAssignedOnlineMeeting",
        "accessLevel" : "Locked",
        "attendees" : [
          "sip:johndoe@contoso.com",
          "sip:janedoe@contoso.com"
        ],
        "automaticLeaderAssignment" : "Disabled",
        "conferenceId" : "12983487",
        "description" : "We\u0027ll be meeting to review the sales numbers for this past quarter and discuss projections for the next two quarters.",
        "entryExitAnnouncement" : "Disabled",
        "expirationTime" : "\/Date(1474932029004)\/",
        "joinUrl" : "https://meet.contoso.com/bmauldin/IB88RLLY",
        "leaders" : [
          "sip:aikc@contoso.com",
          "sip:lenea@contoso.com"
        ],
        "legacyOnlineMeetingUri" : "conf:sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY?conversation-id=cdc15173-e354-4d1b-9f4c-cf42ab746f3d",
        "lobbyBypassForPhoneUsers" : "Disabled",
        "onlineMeetingId" : "IB88RLLY",
        "onlineMeetingRel" : "myOnlineMeetings",
        "onlineMeetingUri" : "sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY",
        "organizerUri" : "sip:bmauldin@contoso.com",
        "phoneUserAdmission" : "Disabled",
        "subject" : "Quarterly sales numbers",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/600"
          },
          "onlineMeetingExtensions" : {
            "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions"
          }
        },
        "_embedded" : {
          "onlineMeetingExtension" : [
            {
              "rel" : "onlineMeetingExtension",
              "id" : "917823",
              "type" : "RoamedParticipantData",
              "_links" : {
                "self" : {
                  "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension"
                }
              }
            }
          ]
        }
      }
    ],
    "myOnlineMeeting" : [
      {
        "rel" : "myOnlineMeeting",
        "accessLevel" : "Locked",
        "attendees" : [
          "sip:johndoe@contoso.com",
          "sip:janedoe@contoso.com"
        ],
        "automaticLeaderAssignment" : "Disabled",
        "conferenceId" : "12983487",
        "description" : "We\u0027ll be meeting to review the sales numbers for this past quarter and discuss projections for the next two quarters.",
        "entryExitAnnouncement" : "Disabled",
        "expirationTime" : "\/Date(1474932029024)\/",
        "joinUrl" : "https://meet.contoso.com/bmauldin/IB88RLLY",
        "leaders" : [
          "sip:aikc@contoso.com",
          "sip:lenea@contoso.com"
        ],
        "legacyOnlineMeetingUri" : "conf:sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY?conversation-id=cdc15173-e354-4d1b-9f4c-cf42ab746f3d",
        "lobbyBypassForPhoneUsers" : "Disabled",
        "onlineMeetingId" : "IB88RLLY",
        "onlineMeetingRel" : "myOnlineMeetings",
        "onlineMeetingUri" : "sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY",
        "organizerUri" : "sip:bmauldin@contoso.com",
        "phoneUserAdmission" : "Disabled",
        "subject" : "Quarterly sales numbers",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943"
          },
          "onlineMeetingExtensions" : {
            "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions"
          }
        },
        "_embedded" : {
          "onlineMeetingExtension" : [
            {
              "rel" : "onlineMeetingExtension",
              "id" : "917823",
              "type" : "RoamedParticipantData",
              "_links" : {
                "self" : {
                  "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension"
                }
              }
            }
          ]
        }
      }
    ]
  }
}
```


#### XML Request

```xml
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```

#### XML Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.

```xml
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 4254
<?xml version="1.0" encoding="utf-8"?>
<resource rel="myOnlineMeetings" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">myOnlineMeetings</property>
  <resource rel="myAssignedOnlineMeeting" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/600">
    <link rel="onlineMeetingExtensions" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions" />
    <property name="rel">myAssignedOnlineMeeting</property>
    <property name="accessLevel">None</property>
    <propertyList name="attendees">
      <item>sip:johndoe@contoso.com</item>
      <item>sip:janedoe@contoso.com</item>
    </propertyList>
    <property name="automaticLeaderAssignment">SameEnterprise</property>
    <property name="conferenceId">12983487</property>
    <property name="description">We'll be meeting to review the sales numbers for this past quarter and discuss projections for the next two quarters.</property>
    <property name="entryExitAnnouncement">Unsupported</property>
    <property name="expirationTime">2016-09-26T16:20:29.0389049-07:00</property>
    <property name="joinUrl">https://meet.contoso.com/bmauldin/IB88RLLY</property>
    <propertyList name="leaders">
      <item>sip:aikc@contoso.com</item>
      <item>sip:lenea@contoso.com</item>
    </propertyList>
    <property name="legacyOnlineMeetingUri">conf:sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY?conversation-id=cdc15173-e354-4d1b-9f4c-cf42ab746f3d</property>
    <property name="lobbyBypassForPhoneUsers">Disabled</property>
    <property name="onlineMeetingId">IB88RLLY</property>
    <property name="onlineMeetingRel">myOnlineMeetings</property>
    <property name="onlineMeetingUri">sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY</property>
    <property name="organizerUri">sip:bmauldin@contoso.com</property>
    <property name="phoneUserAdmission">Disabled</property>
    <property name="subject">Quarterly sales numbers</property>
    <resource rel="onlineMeetingExtension" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension">
      <property name="rel">onlineMeetingExtension</property>
      <property name="id">917823</property>
      <property name="type">RoamedParticipantData</property>
    </resource>
  </resource>
  <resource rel="myOnlineMeeting" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943">
    <link rel="onlineMeetingExtensions" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions" />
    <property name="rel">myOnlineMeeting</property>
    <property name="accessLevel">None</property>
    <propertyList name="attendees">
      <item>sip:johndoe@contoso.com</item>
      <item>sip:janedoe@contoso.com</item>
    </propertyList>
    <property name="automaticLeaderAssignment">SameEnterprise</property>
    <property name="conferenceId">12983487</property>
    <property name="description">We'll be meeting to review the sales numbers for this past quarter and discuss projections for the next two quarters.</property>
    <property name="entryExitAnnouncement">Disabled</property>
    <property name="expirationTime">2016-09-26T16:20:29.0419053-07:00</property>
    <property name="joinUrl">https://meet.contoso.com/bmauldin/IB88RLLY</property>
    <propertyList name="leaders">
      <item>sip:aikc@contoso.com</item>
      <item>sip:lenea@contoso.com</item>
    </propertyList>
    <property name="legacyOnlineMeetingUri">conf:sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY?conversation-id=cdc15173-e354-4d1b-9f4c-cf42ab746f3d</property>
    <property name="lobbyBypassForPhoneUsers">Disabled</property>
    <property name="onlineMeetingId">IB88RLLY</property>
    <property name="onlineMeetingRel">myOnlineMeetings</property>
    <property name="onlineMeetingUri">sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY</property>
    <property name="organizerUri">sip:bmauldin@contoso.com</property>
    <property name="phoneUserAdmission">Disabled</property>
    <property name="subject">Quarterly sales numbers</property>
    <resource rel="onlineMeetingExtension" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension">
      <property name="rel">onlineMeetingExtension</property>
      <property name="id">917823</property>
      <property name="type">RoamedParticipantData</property>
    </resource>
  </resource>
</resource>
```

## POST

Creates a new [myOnlineMeeting](myOnlineMeeting_ref.md).

### Request body

|Name|Description|Required?|
|:-----|:-----|:-----|
|accessLevel|The policy that indicates which users are permitted to join the onlinemeeting without being placed in the online meeting lobby.Set this property to control access to the online meeting.An application should query the organizer's eligible access levels before setting this property.Nullable (AccessLevel)None, SameEnterprise, Locked, Invited, or Everyone|No|
|attendees|A list of the participants who have the attendee role. Attendees have to be part of the organization.Array of String|No|
|automaticLeaderAssignment|The policy that indicates which participants should be automatically promoted to leader when they join the online meeting.Leader assignments are applied when users join the online meeting. Such users are automatically promoted to the leader role.An application should query the automatic leader assignment options to see which are available to the organizer before setting this property.The request will fail if the application attempts to schedule an online meeting using a value that is not permitted.Nullable (AutomaticLeaderAssignment)Disabled, SameEnterprise, or Everyone|No|
|description|The long description of the online meeting.String|No|
|entryExitAnnouncement|The policy that indicates how entry/exit announcements will be used in the online meeting.When entry/exit announcements are enabled, the online meeting will announce the names of the participantswho join the online meeting through audio.An application should set this property to entryExitAnnouncement.Enabledonly if the online meeting supports modifying the entry/exit announcements status. See onlineMeetingPolicies.Nullable (EntryExitAnnouncement)Unsupported, Disabled, or Enabled|No|
|expirationTime|The absolute date and time after which the online meeting can be deleted.The day and time must fall between one year before and ten years after thecurrent date and time on the server.If no value is supplied, the expiry time is set to 8 hours.Nullable DateTime|No|
|leaders|A list of the participants who have the leader role.Organizers do not need to be added to the leaders list because they are automatically added to this list. Leaders have to be part of the organization.Array of String|No|
|lobbyBypassForPhoneUsers|The lobby bypass setting for the online meeting.An online meeting leader can allow specific types of users to bypass the lobby and be admitted directly into the online meeting.For example, an online meeting leader can allow participants who join by phone to bypass the lobby. Note that if the current online meeting access level is AccessLevel.Locked, all new users who join the online meeting -regardless of any bypass setting - are placed in the online meeting lobby.An application should query the lobby bypass options available to the organizer before setting this property.The request will fail if the application attempts to schedule an online meeting using a value that is not permitted.Nullable (LobbyBypassForPhoneUsers)Disabled, or Enabled|No|
|phoneUserAdmission|Whether participants can join the online meeting over the phone.Setting this property to true means that online meeting participants can join itby phone through the Conferencing Auto Attendant (CAA) service.Nullable (PhoneUserAdmission)Disabled, or Enabled|No|
|subject|The subject of the online meeting.String|No|

### Response body

|Item|Description|
|:-----|:-----|
|[myOnlineMeeting](OnlineMeeting_ref.md)|Represents a scheduled meeting on the user's calendar.|

### Synchronous errors

The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|Error|Code|Subcode|Description|
|:-----|:-----|:-----|:-----|
|Conflict|409|TooManyOnlineMeetings|Returned when this user has too many online meetings.|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

### Examples

#### JSON Request

```json
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Accept: application/json
Content-Length: 515
{
  &quot;accessLevel&quot; : &quot;None&quot;,
  &quot;attendees&quot; : [
    &quot;sip : johndoe@contoso.com&quot;,
    &quot;sip : janedoe@contoso.com&quot;
  ],
  &quot;automaticLeaderAssignment&quot; : &quot;SameEnterprise&quot;,
  &quot;description&quot; : &quot;We\u0027llbemeetingtoreviewthesalesnumbersforthispastquarteranddiscussprojectionsforthenexttwoquarters.&quot;,
  &quot;entryExitAnnouncement&quot; : &quot;Disabled&quot;,
  &quot;expirationTime&quot; : &quot;\/Date(1474932029050)\/&quot;,
  &quot;leaders&quot; : [
    &quot;sip : aikc@contoso.com&quot;,
    &quot;sip : lenea@contoso.com&quot;
  ],
  &quot;lobbyBypassForPhoneUsers&quot; : &quot;Disabled&quot;,
  &quot;phoneUserAdmission&quot; : &quot;Disabled&quot;,
  &quot;subject&quot; : &quot;Quarterlysalesnumbers&quot;
}
```


#### JSON Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.

```json
HTTP/1.1 201 Created
Etag: 758b980a-7b4b-4702-8490-f84c6d0ca3e3
Content-Type: application/json
Content-Length: 1434
{
  "rel" : "myOnlineMeeting",
  "accessLevel" : "None",
  "attendees" : [
    "sip:johndoe@contoso.com",
    "sip:janedoe@contoso.com"
  ],
  "automaticLeaderAssignment" : "SameEnterprise",
  "conferenceId" : "12983487",
  "description" : "We\u0027ll be meeting to review the sales numbers for this past quarter and discuss projections for the next two quarters.",
  "entryExitAnnouncement" : "Disabled",
  "expirationTime" : "\/Date(1474932029051)\/",
  "joinUrl" : "https://meet.contoso.com/bmauldin/IB88RLLY",
  "leaders" : [
    "sip:aikc@contoso.com",
    "sip:lenea@contoso.com"
  ],
  "legacyOnlineMeetingUri" : "conf:sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY?conversation-id=cdc15173-e354-4d1b-9f4c-cf42ab746f3d",
  "lobbyBypassForPhoneUsers" : "Disabled",
  "onlineMeetingId" : "IB88RLLY",
  "onlineMeetingRel" : "myOnlineMeetings",
  "onlineMeetingUri" : "sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY",
  "organizerUri" : "sip:bmauldin@contoso.com",
  "phoneUserAdmission" : "Disabled",
  "subject" : "Quarterly sales numbers",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943"
    },
    "onlineMeetingExtensions" : {
      "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions"
    }
  },
  "_embedded" : {
    "onlineMeetingExtension" : [
      {
        "rel" : "onlineMeetingExtension",
        "id" : "917823",
        "type" : "RoamedParticipantData",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension"
          }
        }
      }
    ]
  }
}
```


#### XML Request

```xml
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Accept: application/xml
Content-Length: 910
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;accessLevel&quot;&gt;Locked&lt;/property&gt;
  &lt;propertyList name=&quot;attendees&quot;&gt;
    &lt;item&gt;sip:johndoe@contoso.com&lt;/item&gt;
    &lt;item&gt;sip:janedoe@contoso.com&lt;/item&gt;
  &lt;/propertyList&gt;
  &lt;property name=&quot;automaticLeaderAssignment&quot;&gt;Disabled&lt;/property&gt;
  &lt;property name=&quot;description&quot;&gt;We&#39;ll be meeting to review the sales numbers for this past quarter and discuss projections for the next two quarters.&lt;/property&gt;
  &lt;property name=&quot;entryExitAnnouncement&quot;&gt;Disabled&lt;/property&gt;
  &lt;property name=&quot;expirationTime&quot;&gt;2016-09-26T16:20:29.0559053-07:00&lt;/property&gt;
  &lt;propertyList name=&quot;leaders&quot;&gt;
    &lt;item&gt;sip:aikc@contoso.com&lt;/item&gt;
    &lt;item&gt;sip:lenea@contoso.com&lt;/item&gt;
  &lt;/propertyList&gt;
  &lt;property name=&quot;lobbyBypassForPhoneUsers&quot;&gt;Disabled&lt;/property&gt;
  &lt;property name=&quot;phoneUserAdmission&quot;&gt;Disabled&lt;/property&gt;
  &lt;property name=&quot;subject&quot;&gt;Quarterly sales numbers&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response

This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.

```xml
HTTP/1.1 201 Created
Etag: f332107c-0713-4336-b066-416bd06be3fd
Content-Type: application/xml
Content-Length: 2089
<?xml version="1.0" encoding="utf-8"?>
<resource rel="myOnlineMeeting" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="onlineMeetingExtensions" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions" />
  <property name="rel">myOnlineMeeting</property>
  <property name="accessLevel">SameEnterprise</property>
  <propertyList name="attendees">
    <item>sip:johndoe@contoso.com</item>
    <item>sip:janedoe@contoso.com</item>
  </propertyList>
  <property name="automaticLeaderAssignment">Disabled</property>
  <property name="conferenceId">12983487</property>
  <property name="description">We'll be meeting to review the sales numbers for this past quarter and discuss projections for the next two quarters.</property>
  <property name="entryExitAnnouncement">Disabled</property>
  <property name="expirationTime">2016-09-26T16:20:29.0589061-07:00</property>
  <property name="joinUrl">https://meet.contoso.com/bmauldin/IB88RLLY</property>
  <propertyList name="leaders">
    <item>sip:aikc@contoso.com</item>
    <item>sip:lenea@contoso.com</item>
  </propertyList>
  <property name="legacyOnlineMeetingUri">conf:sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY?conversation-id=cdc15173-e354-4d1b-9f4c-cf42ab746f3d</property>
  <property name="lobbyBypassForPhoneUsers">Disabled</property>
  <property name="onlineMeetingId">IB88RLLY</property>
  <property name="onlineMeetingRel">myOnlineMeetings</property>
  <property name="onlineMeetingUri">sip:bmauldin@contoso.com;gruu;opaque=app:conf:focus:id:IB88RLLY</property>
  <property name="organizerUri">sip:bmauldin@contoso.com</property>
  <property name="phoneUserAdmission">Disabled</property>
  <property name="subject">Quarterly sales numbers</property>
  <resource rel="onlineMeetingExtension" href="/ucwa/v1/applications/192/onlineMeetings/myOnlineMeetings/943/onlineMeetingExtensions/onlineMeetingExtension">
    <property name="rel">onlineMeetingExtension</property>
    <property name="id">917823</property>
    <property name="type">RoamedParticipantData</property>
  </resource>
</resource>
```



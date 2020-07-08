# callForwardingSettings

 _**Applies to:** Skype for Business 2015_


Represents settings that govern call forwarding.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource can be used to set up rules on how to route audio calls for call forwarding and simultaneous ring.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|activePeriod|The time period in which these rules are in effect, either during workhours or always.|
|activeSetting|The currently active call forwarding setting.|
|unansweredCallHandling|Indicates whether unanswered calls will be forwarded.This setting is disabled when the active setting is ImmediateForward.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|turnOffCallForwarding|Turns off call forwarding.|
|immediateForwardSettings|Represents the settings for a user to immediately forward incoming calls to a specified target.|
|simultaneousRingSettings|Represents a user's settings to simultaneously send incoming calls to a specified target.|
|unansweredCallSettings|Represents a user's settings to send unanswered calls to a specified target.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

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
|Forbidden|403|None|Forbidden exception that occurs when the logged-on user is not enabled for enterprise voice.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json
if-none-match: c5a45d19-1c5b-41c4-8a6b-c7982b0448bd

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: 63d9c77b-d6e4-487d-8d2e-9c48a3391cde
Content-Type: application/json
Content-Length: 2359
{
  "rel" : "callForwardingSettings",
  "activePeriod" : "Workhours",
  "activeSetting" : "ImmediateForward",
  "unansweredCallHandling" : "Enabled",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings"
    },
    "turnOffCallForwarding" : {
      "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/turnOffCallForwarding"
    }
  },
  "_embedded" : {
    "immediateForwardSettings" : {
      "rel" : "immediateForwardSettings",
      "target" : "None",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings"
        },
        "contact" : {
          "href" : "/ucwa/v1/applications/192/people/282"
        },
        "immediateForwardToContact" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToContact"
        },
        "immediateForwardToDelegates" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToDelegates"
        },
        "immediateForwardToVoicemail" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToVoicemail"
        }
      }
    },
    "simultaneousRingSettings" : {
      "rel" : "simultaneousRingSettings",
      "ringDelay" : 5,
      "target" : "None",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings"
        },
        "contact" : {
          "href" : "/ucwa/v1/applications/192/people/282"
        },
        "simultaneousRingToContact" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToContact"
        },
        "simultaneousRingToDelegates" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToDelegates"
        },
        "simultaneousRingToTeam" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToTeam"
        }
      }
    },
    "unansweredCallSettings" : {
      "rel" : "unansweredCallSettings",
      "ringDelay" : 5,
      "target" : "None",
      "_links" : {
        "self" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings"
        },
        "contact" : {
          "href" : "/ucwa/v1/applications/192/people/282"
        },
        "resetUnansweredCallSettings" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/resetUnansweredCallSettings"
        },
        "unansweredCallToContact" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToContact"
        },
        "unansweredCallToVoicemail" : {
          "href" : "/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToVoicemail"
        }
      }
    }
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml
if-none-match: 8fb98d93-0182-44b8-835b-06262615748f

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Etag: d2b186e8-89ce-4e36-827d-51ae06eef732
Content-Type: application/xml
Content-Length: 2851
<?xml version="1.0" encoding="utf-8"?>
<resource rel="callForwardingSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="turnOffCallForwarding" href="/ucwa/v1/applications/192/me/callForwardingSettings/turnOffCallForwarding" />
  <property name="rel">callForwardingSettings</property>
  <property name="activePeriod">Workhours</property>
  <property name="activeSetting">ImmediateForward</property>
  <property name="unansweredCallHandling">Enabled</property>
  <resource rel="immediateForwardSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings">
    <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
    <link rel="immediateForwardToContact" href="/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToContact" />
    <link rel="immediateForwardToDelegates" href="/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToDelegates" />
    <link rel="immediateForwardToVoicemail" href="/ucwa/v1/applications/192/me/callForwardingSettings/immediateForwardSettings/immediateForwardToVoicemail" />
    <property name="rel">immediateForwardSettings</property>
    <property name="target">None</property>
  </resource>
  <resource rel="simultaneousRingSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings">
    <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
    <link rel="simultaneousRingToContact" href="/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToContact" />
    <link rel="simultaneousRingToDelegates" href="/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToDelegates" />
    <link rel="simultaneousRingToTeam" href="/ucwa/v1/applications/192/me/callForwardingSettings/simultaneousRingSettings/simultaneousRingToTeam" />
    <property name="rel">simultaneousRingSettings</property>
    <property name="ringDelay">5</property>
    <property name="target">None</property>
  </resource>
  <resource rel="unansweredCallSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings">
    <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
    <link rel="resetUnansweredCallSettings" href="/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/resetUnansweredCallSettings" />
    <link rel="unansweredCallToContact" href="/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToContact" />
    <link rel="unansweredCallToVoicemail" href="/ucwa/v1/applications/192/me/callForwardingSettings/unansweredCallSettings/unansweredCallToVoicemail" />
    <property name="rel">unansweredCallSettings</property>
    <property name="ringDelay">5</property>
    <property name="target">None</property>
  </resource>
</resource>
```



### PUT




Operation description coming soon...

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|activePeriod|The time period in which these rules are in effect, either during workhours or always.(ActivePeriod)Always, or Workhours|No|
|activeSetting|The currently active call forwarding setting.(CallForwardingState)None, ImmediateForward, or SimultaneousRing|No|
|unansweredCallHandling|Indicates whether unanswered calls will be forwarded.This setting is disabled when the active setting is ImmediateForward.(UnansweredCallHandling)Enabled, or Disabled|No|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|Forbidden exception that occurs when the logged-on user is not enabled for enterprise voice.|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
if-match: 8d93406a-2593-4133-bb42-610984a4d99f
Content-Length: 129
{
  &quot;rel&quot; : &quot;callForwardingSettings&quot;,
  &quot;activePeriod&quot; : &quot;Workhours&quot;,
  &quot;activeSetting&quot; : &quot;ImmediateForward&quot;,
  &quot;unansweredCallHandling&quot; : &quot;Enabled&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```


#### XML Request




```
Put https://fe1.contoso.com:443/ucwa/v1/applications/192/me/callForwardingSettings HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
if-match: 4e115d71-24b1-4d67-939e-dd2da85cb232
Content-Length: 333
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;resource xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;rel&quot;&gt;callForwardingSettings&lt;/property&gt;
  &lt;property name=&quot;activePeriod&quot;&gt;Workhours&lt;/property&gt;
  &lt;property name=&quot;activeSetting&quot;&gt;ImmediateForward&lt;/property&gt;
  &lt;property name=&quot;unansweredCallHandling&quot;&gt;Enabled&lt;/property&gt;
&lt;/resource&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK

```


